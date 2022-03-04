//
//  SimpleInAppMessage.swift
//  ExponeaSDKProxy
//
//  Created by Michaela Okruhlicová on 02/03/2022.
//

import Foundation
import ExponeaSDK

@objc
public class SimpleInAppMessage: NSObject {
    
    @objc public let id: String
    @objc public let name: String
    @objc public let rawMessageType: String
    @objc public let rawFrequency: String
    @objc public let variantId: Int
    @objc public let variantName: String
    @objc public let eventType: String
    @objc public let priority: Int
    @objc public let delayMS: Int
    @objc public let timeoutMS: Int
    
    public init( inAppMessage: InAppMessage) {
        id = inAppMessage.id
        name = inAppMessage.name
        rawMessageType = inAppMessage.rawMessageType
        variantId = inAppMessage.variantId
        variantName = inAppMessage.variantName
        rawFrequency = inAppMessage.rawFrequency
        eventType = inAppMessage.trigger.eventType
        priority = inAppMessage.priority ?? 0
        delayMS = inAppMessage.delayMS ?? 0
        timeoutMS = inAppMessage.timeoutMS ?? 0
    }
    
    @objc
    public init(
        id: String,
        name: String,
        rawMessageType: String,
        variantId: Int,
        variantName: String,
        rawFrequency: String,
        eventType: String,
        priority: Int,
        delayMS: Int,
        timeoutMS: Int) {
            self.id = id
            self.name = name
            self.rawMessageType = rawMessageType
            self.variantId = variantId
            self.variantName = variantName
            self.rawFrequency = rawFrequency
            self.eventType = eventType
            self.priority = priority
            self.delayMS = delayMS
            self.timeoutMS = timeoutMS
    }
}
