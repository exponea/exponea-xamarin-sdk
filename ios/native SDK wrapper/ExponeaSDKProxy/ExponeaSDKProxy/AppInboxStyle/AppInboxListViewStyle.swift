//
//  AppInboxListViewStyle.swift
//  ExponeaSDKProxy
//
//  Created by Adam Mihalik on 31/05/2023.
//

import Foundation
import UIKit

class AppInboxListViewStyle {
    var backgroundColor: String?
    var item: AppInboxListItemStyle?

    init(backgroundColor: String? = nil, item: AppInboxListItemStyle? = nil) {
        self.backgroundColor = backgroundColor
        self.item = item
    }

    func applyTo(_ target: UITableView) {
        if let backgroundColor = UIColor.parse(backgroundColor) {
            target.backgroundColor = backgroundColor
        }
        // note: 'item' style is used elsewhere due to performance reasons
    }
}
