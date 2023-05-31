//
//  AppInboxMessage.swift
//  ExponeaSDKProxy
//
//  Created by Adam Mihalik on 31/05/2023.
//

import Foundation
import ExponeaSDK

@objc
public class AppInboxMessage: NSObject {
    @objc public let id: String
    @objc public let type: String
    @objc public var read: Bool
    @objc public let receivedTime: Int
    @objc public let content: NSDictionary
    
    @objc
    public init(id: String, type: String, read: Bool, receivedTime: Int, content: NSDictionary) {
        self.id = id
        self.type = type
        self.read = read
        self.receivedTime = receivedTime
        self.content = content
    }
    
    init(source: MessageItem) {
        self.id = source.id
        self.type = source.type
        self.read = source.read
        self.receivedTime = Int(source.rawReceivedTime ?? 0)
        let nonNullContent = source.rawContent ?? [:]
        self.content = nonNullContent.mapValues({ jsonValue in
            jsonValue.rawValue
        }) as NSDictionary
    }
}

extension AppInboxMessage {
    func toNativeMessage() -> MessageItem {
        return MessageItem(
            id: self.id,
            type: self.type,
            read: self.read,
            rawReceivedTime: Double(self.receivedTime),
            rawContent: ExponeaSDK.JSONValue.convert(self.content.swiftDictionary)
        )
    }
}

@objc
public class AppInboxAction: NSObject {
    @objc public let action: String?
    @objc public let title: String?
    @objc public let url: String?
    
    @objc
    public init(
        action: String?,
        title: String?,
        url: String?
    ) {
        self.action = action
        self.title = title
        self.url = url
    }
}
