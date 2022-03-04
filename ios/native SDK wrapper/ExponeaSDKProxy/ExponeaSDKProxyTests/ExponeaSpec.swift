//
//  ExponeaSpec.swift
//  Tests
//
//  Created by Panaxeo on  20/10/2021.
//  Copyright © 2020 Panaxeo. All rights reserved.
//

import Foundation
import Quick
import Nimble
@testable import ExponeaSDK

import enum ExponeaSDK.FlushingMode

@testable import ExponeaSDKProxy

class ExponeaSpec: QuickSpec {
    override func spec() {
        var mockExponea: MockExponea!
        var exponea: ExponeaSDKProxy.Exponea!
        
        beforeEach {
            mockExponea = MockExponea()
            ExponeaSDKProxy.Exponea.shared = mockExponea
            exponea = ExponeaSDKProxy.Exponea.instance
        }
        
        context("configuration") {
            it("should answer to isConfigured") {
                let configured = exponea.isConfigured()
                expect(mockExponea.calls[0].name).to(equal("isConfigured:get"))
                expect(configured).to(equal(false))
            }
            
            it("should configure") {
                let configurationDictionary = TestUtil.parseJson(
                    jsonString: TestUtil.loadFile(relativePath: "/test_data/configurationMinimal.json")
                )
                exponea.configure(configuration: configurationDictionary)
                expect(mockExponea.calls[0].name).to(equal("isConfigured:get"))
                expect(mockExponea.calls[1].name).to(equal("configure"))
            }
            
            it("should not configure if already configured") {
                let configurationDictionary = TestUtil.parseJson(
                    jsonString: TestUtil.loadFile(relativePath: "/test_data/configurationMinimal.json")
                )
                mockExponea.isConfiguredValue = true
                exponea.configure(configuration: configurationDictionary)
                expect(mockExponea.calls[0].name).to(equal("isConfigured:get"))
                expect(mockExponea.calls.count).to(equal(1))
            }
            
            it("should not configure with empty configuration") {
                exponea.configure(configuration: [:])
                expect(mockExponea.calls[0].name).to(equal("isConfigured:get"))
                expect(mockExponea.calls.count).to(equal(1))
                let configured = exponea.isConfigured()
                expect(configured).to(equal(false))
            }
        }
        
        context("getting customerCookie") {
            it("should fail if Exponea is not configured") {
                let cookie = exponea.getCustomerCookie()
                expect(cookie).to(beNil())
            }
            
            it("should return customer cookie") {
                mockExponea.isConfiguredValue = true
                mockExponea.customerCookieValue = "mock-cookie"
                let cookie = exponea.getCustomerCookie()
                expect(cookie).to(equal("mock-cookie"))
                expect(mockExponea.calls[0].name).to(equal("customerCookie:get"))
            }
        }
        
        context("checking push setup") {
            it("should set checkPushSetup value") {
                exponea.checkPushSetup()
                expect(mockExponea.calls[0].name).to(equal("isConfigured:get"))
                expect(mockExponea.calls[1].name).to(equal("checkPushSetup:set"))
                expect(mockExponea.checkPushSetupValue).to(beTrue())
            }
            
            it("should not set checkPushSetupValue after Exponea is configured") {
                mockExponea.isConfiguredValue = true
                exponea.checkPushSetup()
                expect(mockExponea.calls[0].name).to(equal("isConfigured:get"))
                expect(mockExponea.checkPushSetupValue).to(beFalse())
            }
        }
        
        context("flush mode") {
            it("should get flush mode") {
                let flushMode = exponea.getFlushMode()
                expect(flushMode).to(equal("APP_CLOSE"))
                expect(mockExponea.calls[0].name).to(equal("flushingMode:get"))
            }
            
            it("should set flush mode") {
                exponea.setFlushMode(flushMode: "IMMEDIATE")
                expect(mockExponea.calls[0].name).to(equal("flushingMode:set"))
                expect(exponea.getFlushMode()).to(equal("IMMEDIATE"))
            }
            
            it("should not set flush mode with invalid value") {
                exponea.setFlushMode(flushMode: "invalid")
                expect(mockExponea.calls.count).to(equal(0))
                expect(exponea.getFlushMode()).to(equal("APP_CLOSE"))
            }
        }
        
        context("flush period") {
            it("should get flush period") {
                mockExponea.flushingModeValue = .periodic(123)
                expect(exponea.getFlushPeriod()).to(equal(123))
                expect(mockExponea.calls[0].name).to(equal("flushingMode:get"))
            }
            
            it("should not get flush period when not in periodic flush mode") {
                expect(exponea.getFlushPeriod()).to(equal(0))
                expect(mockExponea.calls[0].name).to(equal("flushingMode:get"))
            }
            
            it("should set flush period") {
                mockExponea.flushingModeValue = .periodic(123)
                exponea.setFlushPeriod(flushPeriod: 456)
                expect(mockExponea.calls[0].name).to(equal("flushingMode:get"))
                expect(mockExponea.calls[1].name).to(equal("flushingMode:set"))
                if case .periodic(let period) = mockExponea.flushingModeValue {
                    expect(period).to(equal(456))
                } else {
                    fail("Periodic Flushing mode expected")
                }
            }
            
            it("should not set flush period when not in periodic flush mode") {
                exponea.setFlushPeriod(flushPeriod: 456)
                expect(exponea.getFlushPeriod()).to(equal(0))
            }
        }
        
        context("default properties") {
            it("should get default properties when Exponea is not initialized") {
                expect(exponea.getDefaultProperties()).to(equal("{}"))
                expect(mockExponea.calls[0].name).to(equal("defaultProperties:get"))
            }
            
            it("should get default properties when Exponea is initialized") {
                mockExponea.isConfiguredValue = true
                mockExponea.defaultPropertiesValue = ["key": "value", "int": 1]
                expect(TestUtil.getSortedKeysJson(exponea.getDefaultProperties())).to(
                    equal("{\"int\":1,\"key\":\"value\"}"))
                expect(mockExponea.calls[0].name).to(equal("defaultProperties:get"))
                
            }
            
            it("should set default properties when Exponea is initialized") {
                mockExponea.isConfiguredValue = true
                exponea.setDefaultProperties(properties: ["key": "value", "int": 1])
                expect(mockExponea.calls[0].name).to(equal("isConfigured:get"))
                expect(mockExponea.calls[1].name).to(equal("defaultProperties:set"))
                expect(mockExponea.calls[1].params[0] as? NSDictionary).to(
                    equal(["key": "value", "int": 1])
                )
            }
            
            it("should not set default if Exponea is not configured") {
                exponea.setDefaultProperties(properties: ["key": "value", "int": 1])
                expect(mockExponea.calls[0].name).to(equal("isConfigured:get"))
                expect(mockExponea.calls.count).to(equal(1))
                expect(exponea.getDefaultProperties()).to(equal("{}"))
            }
        }
        context("in-app message delegate") {
            it("should set in-app message delegate") {
                exponea.setInAppMessageDelegate(
                    overrideDefaultBehavior: false,
                    trackActions: true,
                    action: { message, text, link, interaction in
                        
                    })
                expect(mockExponea.calls[0].name).to(equal("inAppMessagesDelegate:set"))
                expect((mockExponea.calls[0].params[0] as! InAppMessageActionDelegate).trackActions).to(equal(true))
                expect((mockExponea.calls[0].params[0] as! InAppMessageActionDelegate).overrideDefaultBehavior).to(equal(false))
                exponea.setInAppMessageDelegate(
                    overrideDefaultBehavior: true,
                    trackActions: false,
                    action: { message, text, link, interaction in
                        
                    })
                expect(mockExponea.calls[1].name).to(equal("inAppMessagesDelegate:set"))
                expect((mockExponea.calls[1].params[0] as! InAppMessageActionDelegate).trackActions).to(equal(false))
                expect((mockExponea.calls[1].params[0] as! InAppMessageActionDelegate).overrideDefaultBehavior).to(equal(true))
            }
        }
    }
}
