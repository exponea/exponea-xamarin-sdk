//
//  Exponea+AnonymizeSpec.swift
//  Tests
//
//  Created by Panaxeo on  20/10/2021.
//  Copyright © 2020 Panaxeo. All rights reserved.
//

import Foundation
import Quick
import Nimble

import protocol ExponeaSDK.JSONConvertible
import struct ExponeaSDK.ExponeaProject
import enum ExponeaSDK.EventType

@testable import ExponeaSDKProxy

class ExponeaAnonymizeSpec: QuickSpec {
    override func spec() {
        var mockExponea: MockExponea!
        var exponea: ExponeaSDKProxy.Exponea!

        beforeEach {
            mockExponea = MockExponea()
            ExponeaSDKProxy.Exponea.shared = mockExponea
            exponea = ExponeaSDKProxy.Exponea.instance
        }

        it("should anonymize without changing anything") {
            mockExponea.isConfiguredValue = true
            exponea.anonymize(
                exponeaProjectDictionary: [:],
                projectMappingDictionary: [:]
            )
            expect(mockExponea.calls[0].name).to(equal("isConfigured:get"))
            expect(mockExponea.calls[1].name).to(equal("anonymize"))
            expect(mockExponea.calls[1].params.count).to(equal(0))
        }

        it("should anonymize and change project") {
            mockExponea.isConfiguredValue = true
            exponea.anonymize(
                exponeaProjectDictionary: [
                    "baseUrl": "mock-url",
                    "authorizationToken": "mock-auth-token",
                    "projectToken": "mock-project-token"
                ],
                projectMappingDictionary: [:]
            )
            expect(mockExponea.calls[0].name).to(equal("isConfigured:get"))
            expect(mockExponea.calls[1].name).to(equal("configuration:get"))
            expect(mockExponea.calls[2].name).to(equal("anonymize"))
            expect(mockExponea.calls[2].params[0] as? ExponeaProject)
                .to(equal(ExponeaProject(
                    baseUrl: "mock-url",
                    projectToken: "mock-project-token",
                    authorization: .token("mock-auth-token")
                )))
            expect(mockExponea.calls[2].params[1]).to(beNil())
        }

        it("should anonymize and change project mapping") {
            mockExponea.isConfiguredValue = true
            exponea.anonymize(
                exponeaProjectDictionary: [
                    "baseUrl": "mock-url",
                    "authorizationToken": "mock-auth-token",
                    "projectToken": "mock-project-token"
                ],
                projectMappingDictionary: [
                    "INSTALL": [[
                        "baseUrl": "install-mock-url",
                        "authorizationToken": "install-mock-auth-token",
                        "projectToken": "install-mock-project-token"
                    ]]
                ]
            )
            expect(mockExponea.calls[0].name).to(equal("isConfigured:get"))
            expect(mockExponea.calls[1].name).to(equal("configuration:get"))
            expect(mockExponea.calls[2].name).to(equal("configuration:get"))
            expect(mockExponea.calls[3].name).to(equal("anonymize"))
            expect(mockExponea.calls[3].params[0] as? ExponeaProject)
                .to(equal(ExponeaProject(
                    baseUrl: "mock-url",
                    projectToken: "mock-project-token",
                    authorization: .token("mock-auth-token")
                )))
            expect(mockExponea.calls[3].params[1] as? [EventType: [ExponeaProject]])
                .to(equal([
                    EventType.install: [
                        ExponeaProject(
                            baseUrl: "install-mock-url",
                            projectToken: "install-mock-project-token",
                            authorization: .token("install-mock-auth-token")
                        )
                    ]
                ]))

        }

        it("should not anonymize when Exponea is not configured") {
            exponea.anonymize(
                exponeaProjectDictionary: [:],
                projectMappingDictionary: [:]
            )
            expect(mockExponea.calls[0].name).to(equal("isConfigured:get"))
            expect(mockExponea.calls.count).to(equal(1))

        }
    }
}
