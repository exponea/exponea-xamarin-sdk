//
//  ImageViewStyle.swift
//  ExponeaSDKProxy
//
//  Created by Adam Mihalik on 31/05/2023.
//

import Foundation
import UIKit

class ImageViewStyle {
    var visible: Bool?
    var backgroundColor: String?

    init(visible: Bool? = nil, backgroundColor: String? = nil) {
        self.visible = visible
        self.backgroundColor = backgroundColor
    }

    func applyTo(_ target: UIView) {
        if let visible = visible {
            target.isHidden = !visible
        }
        if let backgroundColor = UIColor.parse(backgroundColor) {
            target.tintColor = backgroundColor
        }
    }

    func applyTo(_ target: UIImageView) {
        if let visible = visible {
            target.isHidden = !visible
        }
        if let backgroundColor = UIColor.parse(backgroundColor) {
            target.backgroundColor = backgroundColor
        }
    }
}
