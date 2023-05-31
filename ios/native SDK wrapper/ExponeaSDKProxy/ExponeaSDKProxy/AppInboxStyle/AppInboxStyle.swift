//
//  AppInboxStyle.swift
//  ExponeaSDKProxy
//
//  Created by Adam Mihalik on 31/05/2023.
//

import Foundation
class AppInboxStyle {
    var appInboxButton: ButtonStyle?
    var detailView: DetailViewStyle?
    var listView: ListScreenStyle?

    init(appInboxButton: ButtonStyle? = nil, detailView: DetailViewStyle? = nil, listView: ListScreenStyle? = nil) {
        self.appInboxButton = appInboxButton
        self.detailView = detailView
        self.listView = listView
    }
}
