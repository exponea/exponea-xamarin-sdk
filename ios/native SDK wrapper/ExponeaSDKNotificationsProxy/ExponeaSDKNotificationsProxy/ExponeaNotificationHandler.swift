//
//  ExponeaNotificationHandler.swift
//  ExponeaSDKExtensionProxy
//
//  Created by Michaela Okruhlicová on 11/11/2021.
//

import Foundation
import ExponeaSDKNotifications

@objc(ExponeaNotificationHandler)
public class ExponeaNotificationHandler : NSObject {
    
    @objc
    public init(appGroup: String) {
        self.appGroup = appGroup
        self.notificationService = ExponeaNotificationService(appGroup: appGroup)
        self.notificationContentService = ExponeaNotificationContentService()
    }
    
    private let notificationService: ExponeaNotificationService
    private let notificationContentService: ExponeaNotificationContentService
    private let appGroup: String
    
    @objc
    public func processNotificationRequest(request: UNNotificationRequest, contentHandler: @escaping (UNNotificationContent) -> Void) {
        notificationService.process(request: request, contentHandler: contentHandler)
    }
    
    @objc
    public func serviceExtensionTimeWillExpire() {
        notificationService.serviceExtensionTimeWillExpire()
    }
    
    @objc
    public func notificationReceived(_ notification: UNNotification, context: NSExtensionContext?, viewController: UIViewController) {
        notificationContentService.didReceive(notification, context: context, viewController: viewController)
    }
}
    
