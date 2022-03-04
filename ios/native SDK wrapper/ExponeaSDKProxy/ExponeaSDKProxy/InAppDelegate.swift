//
//  InAppDelegate.swift
//  ExponeaSDKProxy
//
//  Created by Michaela Okruhlicová on 21/02/2022.
//

import Foundation
import ExponeaSDK

class InAppDelegate: InAppMessageActionDelegate {
    let overrideDefaultBehavior: Bool
    let trackActions: Bool
    let action: (_ message: SimpleInAppMessage, _ buttonText: String?, _ buttonUrl: String?, _ interaction: Bool)->()

    init(
        overrideDefaultBehavior: Bool,
        trackActions: Bool,
        action: @escaping (_ message: SimpleInAppMessage, _ buttonText: String?, _ buttonUrl: String?, _ interaction: Bool)->()
    ) {
        self.overrideDefaultBehavior = overrideDefaultBehavior
        self.trackActions = trackActions
        self.action = action
    }

    func inAppMessageAction(with message: InAppMessage, button: InAppMessageButton?, interaction: Bool) {
        self.action(SimpleInAppMessage(inAppMessage: message), button?.text, button?.url, interaction)
    }
}
