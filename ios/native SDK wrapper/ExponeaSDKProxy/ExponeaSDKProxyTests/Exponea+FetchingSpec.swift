//
//  Exponea+FetchingSpec.swift
//  Tests
//
//  Created by Panaxeo on  20/10/2021.
//  Copyright © 2020 Panaxeo. All rights reserved.
//

import Foundation
import Quick
import Nimble

import enum ExponeaSDK.Result
import struct ExponeaSDK.ConsentsResponse
import struct ExponeaSDK.RecommendationResponse
import struct ExponeaSDK.EmptyRecommendationData

@testable import ExponeaSDKProxy

@available(iOS 11.0, *)
class ExponeaFetchingSpec: QuickSpec {
    let consentsResponse = """
        {
            "results": [{
                "id": "TestCategory",
                "legitimate_interest": false,
                "sources": {
                    "crm": true,
                    "import": true,
                    "page": true,
                    "private_api": true,
                    "public_api": false,
                    "scenario": true
                },
                "translations": {
                    "en": {
                        "description": "test",
                        "name": "My Test Consents"
                    }
                }
            }],
            "success": true
        }
    """

    let recommendationsResponse = """
        {
          "success": true,
          "value": [
            {
              "description": "an awesome book",
              "engine_name": "random",
              "image": "no image available",
              "item_id": "1",
              "name": "book",
              "price": 19.99,
              "product_id": "1",
              "recommendation_id": "5dd6af3d147f518cb457c63c",
              "recommendation_variant_id": null
            },
            {
              "description": "super awesome off-brand phone",
              "engine_name": "random",
              "image": "just google one",
              "item_id": "3",
              "name": "mobile phone",
              "price": 499.99,
              "product_id": "3",
              "recommendation_id": "5dd6af3d147f518cb457c63c",
              "recommendation_variant_id": "mock id"
            }
          ]
        }
    """

    // swiftlint:disable line_length
    let consentsJSPayload = """
    [{"id":"TestCategory","legitimateInterest":false,"sources":{"createdFromCRM":true,"imported":true,"privateAPI":true,"publicAPI":false,"trackedFromScenario":true},"translations":{"en":{"description":"test","name":"My Test Consents"}}}]
    """

    let recommendationJSPayload = """
    [{"description":"an awesome book","engine_name":"random","image":"no image available","item_id":"1","name":"book","price":19.989999999999998,"product_id":"1","recommendation_id":"5dd6af3d147f518cb457c63c","recommendation_variant_id":null},{"description":"super awesome off-brand phone","engine_name":"random","image":"just google one","item_id":"3","name":"mobile phone","price":499.99000000000001,"product_id":"3","recommendation_id":"5dd6af3d147f518cb457c63c","recommendation_variant_id":"mock id"}]
    """
    // swiftlint:enable line_length

    override func spec() {
        var mockExponea: MockExponea!
        var exponea: ExponeaSDKProxy.Exponea!
        
        beforeEach {
            mockExponea = MockExponea()
            ExponeaSDKProxy.Exponea.shared = mockExponea
            exponea = ExponeaSDKProxy.Exponea.instance
        }

        context("consents") {
            it("should fetch consents") {
                mockExponea.isConfiguredValue = true
                waitUntil { done in
                    exponea.fetchConsents(
                        success: { result in
                            expect(TestUtil.getSortedKeysJson(result)).to(equal(self.consentsJSPayload))
                            done()
                        },
                        fail: { result in
                            fail()
                        }
                    )
                    expect(mockExponea.calls[0].name).to(equal("isConfigured:get"))
                    expect(mockExponea.calls[1].name).to(equal("fetchConsents"))
                    let callback = mockExponea.calls[1].params[0] as? (Result<ConsentsResponse>) -> Void

                    let jsonDecoder = JSONDecoder()
                    jsonDecoder.dateDecodingStrategy = .secondsSince1970
                    guard let data = self.consentsResponse.data(using: .utf8),
                          let consents = try? jsonDecoder.decode(ConsentsResponse.self, from: data) else {
                        fail("Unable to parse consents")
                        return
                    }
                    callback?(Result<ConsentsResponse>.success(consents))
                }
            }

            it("should forward error when fetching consents fails") {
                mockExponea.isConfiguredValue = true
                waitUntil { done in
                    exponea.fetchConsents(
                        success: { _ in fail()},
                        fail: { error in
                            expect(error).to(equal("The operation couldn’t be completed. (ExponeaSDKProxy.ExponeaError error 2.)"))
                            done()
                        }
                    )
                    let callback = mockExponea.calls[1].params[0] as? (Result<ConsentsResponse>) -> Void
                    callback?(Result<ConsentsResponse>.failure(ExponeaError.fetchError(description: "something")))
                }
            }

            it("should not fetch consents when Exponea is not configured") {
                waitUntil { done in
                    exponea.fetchConsents(
                        success: { _ in },
                        fail: { error in
                            expect(error).to(equal(ExponeaError.notConfigured.description))
                            done()
                        }
                    )
                }
            }
        }

        context("recommendations") {
            it("should fetch recommendations") {
                mockExponea.isConfiguredValue = true
                waitUntil { done in
                    exponea.fetchRecommendations(
                        optionsDictionary: ["id": "mock-id", "fillWithRandom": false],
                        success: { result in
                            expect(TestUtil.getSortedKeysJson(result)).to(equal(self.recommendationJSPayload))
                            done()
                        },
                        fail: { _ in }
                    )
                    expect(mockExponea.calls[0].name).to(equal("isConfigured:get"))
                    expect(mockExponea.calls[1].name).to(equal("fetchRecommendation"))
                    let callback = mockExponea.calls[1].params[1]
                        as? (Result<RecommendationResponse<AllRecommendationData>>) -> Void

                    let jsonDecoder = JSONDecoder()
                    jsonDecoder.dateDecodingStrategy = .secondsSince1970
                    guard let data = self.recommendationsResponse.data(using: .utf8),
                          let recommendations = try? jsonDecoder.decode(
                              RecommendationResponse<AllRecommendationData>.self,
                              from: data
                          ) else {
                        fail("Unable to parse recommendations")
                        return
                    }
                    callback?(Result<RecommendationResponse<AllRecommendationData>>.success(recommendations))
                }
            }

            it("should not fetch recommendations without required properties") {
                mockExponea.isConfiguredValue = true
                waitUntil { done in
                    exponea.fetchRecommendations(
                        optionsDictionary: [:],
                        success: { _ in },
                        fail: { error in
                            expect(error).to(equal("Property id is required."))
                            done()
                        }
                    )
                }
            }

            it("should forward error when fetching recommendations fails") {
                mockExponea.isConfiguredValue = true
                waitUntil { done in
                    exponea.fetchRecommendations(
                        optionsDictionary: ["id": "mock-id", "fillWithRandom": false],
                        success: { _ in },
                        fail: { error in
                            expect(error).to(equal("The operation couldn’t be completed. (ExponeaSDKProxy.ExponeaError error 2.)"))
                            done()
                        }
                    )
                    let callback = mockExponea.calls[1].params[1]
                        as? (Result<RecommendationResponse<AllRecommendationData>>) -> Void
                    callback?(Result.failure(ExponeaError.fetchError(description: "something")))
                }
            }

            it("should not fetch recommendations when Exponea is not configured") {
                waitUntil { done in
                    exponea.fetchRecommendations(
                        optionsDictionary: [:],
                        success: { _ in },
                        fail: { error in
                            expect(error).to(equal(ExponeaError.notConfigured.description))
                            done()
                        }
                    )
                }
            }
        }
    }
}
