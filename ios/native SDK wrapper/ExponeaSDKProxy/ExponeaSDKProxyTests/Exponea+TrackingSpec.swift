//
//  Exponea+TrackingSpec.swift
//  Tests
//
//  Created by Panaxeo on  20/10/2021.
//  Copyright © 2020 Panaxeo. All rights reserved.
//

import Foundation
import Quick
import Nimble

import protocol ExponeaSDK.JSONConvertible

@testable import ExponeaSDKProxy
@testable import ExponeaSDK

class ExponeaTrackingSpec: QuickSpec {
    override func spec() {
        var mockExponea: MockExponea!
        var exponea: ExponeaSDKProxy.Exponea!
        
        beforeEach {
            mockExponea = MockExponea()
            ExponeaSDKProxy.Exponea.shared = mockExponea
            exponea = ExponeaSDKProxy.Exponea.instance
        }
        
        context("event tracking") {
            it("should track event with timestamp") {
                mockExponea.isConfiguredValue = true
                exponea.trackEvent(
                    eventType: "mock-event",
                    properties: ["key": "value", "otherKey": true],
                    timestamp: 12345
                )
                expect(mockExponea.calls[0].name).to(equal("isConfigured:get"))
                expect(mockExponea.calls[1].name).to(equal("trackEvent"))
                let params = mockExponea.calls[1].params[0] as? [String: JSONConvertible]
                expect(params?["key"] as? String).to(equal("value"))
                expect(params?["otherKey"] as? Bool).to(equal(true))
                expect(mockExponea.calls[1].params[1] as? Double).to(equal(12345))
                expect(mockExponea.calls[1].params[2] as? String).to(equal("mock-event"))
            }
            
            it("should track event without timestamp") {
                mockExponea.isConfiguredValue = true
                exponea.trackEvent(
                    eventType: "mock-event",
                    properties: ["key": "value", "otherKey": false]
                )
                expect(mockExponea.calls[0].name).to(equal("isConfigured:get"))
                expect(mockExponea.calls[1].name).to(equal("trackEvent"))
                let params = mockExponea.calls[1].params[0] as? [String: JSONConvertible]
                expect(params?["key"] as? String).to(equal("value"))
                expect(params?["otherKey"] as? Bool).to(equal(false))
                expect(mockExponea.calls[1].params[1]).to(beNil())
                expect(mockExponea.calls[1].params[2] as? String).to(equal("mock-event"))
            }
            
            it("should not track event when Exponea is not configured") {
                exponea.trackEvent(
                    eventType: "mock-event",
                    properties: ["key": "value", "otherKey": false],
                    timestamp: 0
                )
                expect(mockExponea.calls[0].name).to(equal("isConfigured:get"))
                expect(mockExponea.calls.count).to(equal(1))
            }
        }
        
        context("customer identification") {
            it("should identify customer") {
                mockExponea.isConfiguredValue = true
                exponea.identifyCustomer(
                    customerIds: ["id": "some_id"],
                    properties: ["key": "value", "otherKey": false]
                )
                expect(mockExponea.calls[0].name).to(equal("isConfigured:get"))
                expect(mockExponea.calls[1].name).to(equal("identifyCustomer"))
                let ids = mockExponea.calls[1].params[0] as? [String: JSONConvertible]
                expect(ids?["id"] as? String).to(equal("some_id"))
                let params = mockExponea.calls[1].params[1] as? [String: JSONConvertible]
                expect(params?["key"] as? String).to(equal("value"))
                expect(params?["otherKey"] as? Bool).to(equal(false))
            }
            
            it("should not identify customer when exponea is not configured") {
                exponea.identifyCustomer(
                    customerIds: ["id": "some_id"],
                    properties: ["key": "value", "otherKey": false]
                )
                expect(mockExponea.calls[0].name).to(equal("isConfigured:get"))
                expect(mockExponea.calls.count).to(equal(1))
            }
            
            it("should not identify customer when customer id is not a string") {
                mockExponea.isConfiguredValue = true
                exponea.identifyCustomer(
                    customerIds: ["id": 1234],
                    properties: ["key": "value", "otherKey": false]
                )
                expect(mockExponea.calls[0].name).to(equal("isConfigured:get"))
                expect(mockExponea.calls.count).to(equal(1))
                
                
            }
            
            context("data flushing") {
                it("should flush data") {
                    mockExponea.isConfiguredValue = true
                    exponea.flushData()
                    expect(mockExponea.calls[0].name).to(equal("isConfigured:get"))
                    expect(mockExponea.calls[1].name).to(equal("flushData"))
                }
                
                it("should not flush data when Exponea is not configured") {
                    exponea.flushData()
                    expect(mockExponea.calls[0].name).to(equal("isConfigured:get"))
                    expect(mockExponea.calls.count).to(equal(1))
                }
            }
            
            context("session tracking") {
                it("should track session start") {
                    mockExponea.isConfiguredValue = true
                    exponea.trackSessionStart()
                    expect(mockExponea.calls[0].name).to(equal("isConfigured:get"))
                    expect(mockExponea.calls[1].name).to(equal("trackSessionStart"))
                    
                }
                
                it("should not track session start when Exponea is not configured") {
                    exponea.trackSessionStart()
                    expect(mockExponea.calls[0].name).to(equal("isConfigured:get"))
                    expect(mockExponea.calls.count).to(equal(1))
                }
                
                it("should track session end") {
                    mockExponea.isConfiguredValue = true
                    exponea.trackSessionEnd()
                    expect(mockExponea.calls[0].name).to(equal("isConfigured:get"))
                    expect(mockExponea.calls[1].name).to(equal("trackSessionEnd"))
                }
                
                it("should not track session end when Exponea is not configured") {
                    exponea.trackSessionEnd()
                    expect(mockExponea.calls[0].name).to(equal("isConfigured:get"))
                    expect(mockExponea.calls.count).to(equal(1))
                }
            }
            context("in-app action tracking") {
                it("should track in-app message click event") {
                    mockExponea.isConfiguredValue = true
                    exponea.trackInAppMessageClick(
                        message: SimpleInAppMessage(
                            id: "mock-id",
                            name: "mock-name",
                            rawMessageType: "mock-raw-meessage-type",
                            variantId: 12345,
                            variantName: "mock-variant-name",
                            rawFrequency: "mock-raw_frequency",
                            eventType: "mock-event-type",
                            priority: 123,
                            delayMS: 321,
                            timeoutMS: 987),
                        buttonText: "mock-button-text",
                        buttonLink: "mock-button-link"
                    )
                    expect(mockExponea.calls[0].name).to(equal("isConfigured:get"))
                    expect(mockExponea.calls[1].name).to(equal("trackInAppMessageClick"))
                    let message = mockExponea.calls[1].params[0] as! InAppMessage
                    expect(message.id).to(equal("mock-id"))
                    expect(message.name).to(equal("mock-name"))
                    expect(message.rawMessageType).to(equal("mock-raw-meessage-type"))
                    expect(message.variantId).to(equal(12345))
                    expect(message.variantName).to(equal("mock-variant-name"))
                    expect(message.rawFrequency).to(equal("mock-raw_frequency"))
                    expect(message.trigger.eventType).to(equal("mock-event-type"))
                    expect(message.priority).to(equal(123))
                    expect(message.delayMS).to(equal(321))
                    expect(message.timeoutMS).to(equal(987))
                    expect(mockExponea.calls[1].params[1] as? String).to(equal("mock-button-text"))
                    expect(mockExponea.calls[1].params[2] as? String).to(equal("mock-button-link"))
                }
                it("should track in-app message close event") {
                    mockExponea.isConfiguredValue = true
                    exponea.trackInAppMessageClose(
                        message: SimpleInAppMessage(
                            id: "mock-id",
                            name: "mock-name",
                            rawMessageType: "mock-raw-meessage-type",
                            variantId: 12345,
                            variantName: "mock-variant-name",
                            rawFrequency: "mock-raw_frequency",
                            eventType: "mock-event-type",
                            priority: 123,
                            delayMS: 321,
                            timeoutMS: 987)
                    )
                    expect(mockExponea.calls[0].name).to(equal("isConfigured:get"))
                    expect(mockExponea.calls[1].name).to(equal("trackInAppMessageClose"))
                    let message = mockExponea.calls[1].params[0] as! InAppMessage
                    expect(message.id).to(equal("mock-id"))
                    expect(message.name).to(equal("mock-name"))
                    expect(message.rawMessageType).to(equal("mock-raw-meessage-type"))
                    expect(message.variantId).to(equal(12345))
                    expect(message.variantName).to(equal("mock-variant-name"))
                    expect(message.rawFrequency).to(equal("mock-raw_frequency"))
                    expect(message.trigger.eventType).to(equal("mock-event-type"))
                    expect(message.priority).to(equal(123))
                    expect(message.delayMS).to(equal(321))
                    expect(message.timeoutMS).to(equal(987))
                }
            }
        }
    }
}
