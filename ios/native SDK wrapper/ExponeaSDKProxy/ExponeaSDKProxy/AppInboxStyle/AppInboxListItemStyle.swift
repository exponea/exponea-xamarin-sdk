//
//  AppInboxListItemStyle.swift
//  ExponeaSDKProxy
//
//  Created by Adam Mihalik on 31/05/2023.
//

import Foundation
import UIKit

class AppInboxListItemStyle {
    var backgroundColor: String?
    var readFlag: ImageViewStyle?
    var receivedTime: TextViewStyle?
    var title: TextViewStyle?
    var content: TextViewStyle?
    var image: ImageViewStyle?

    init(
        backgroundColor: String? = nil,
        readFlag: ImageViewStyle? = nil,
        receivedTime: TextViewStyle? = nil,
        title: TextViewStyle? = nil,
        content: TextViewStyle? = nil,
        image: ImageViewStyle? = nil
    ) {
        self.backgroundColor = backgroundColor
        self.readFlag = readFlag
        self.receivedTime = receivedTime
        self.title = title
        self.content = content
        self.image = image
    }

    func applyTo(_ target: MessageItemCell) {
        if let backgroundColor = UIColor.parse(backgroundColor) {
            target.backgroundColor = backgroundColor
        }
        if let readFlagStyle = readFlag {
            readFlagStyle.applyTo(target.readFlag)
        }
        if let receivedTime = receivedTime {
            receivedTime.applyTo(target.receivedTime)
        }
        if let title = title {
            title.applyTo(target.titleLabel)
        }
        if let content = content {
            content.applyTo(target.messageLabel)
        }
        if let image = image {
            image.applyTo(target.messageImage)
        }
    }
}
