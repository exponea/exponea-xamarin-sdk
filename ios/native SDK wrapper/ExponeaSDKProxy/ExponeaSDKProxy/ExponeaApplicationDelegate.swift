//
//  ExponeaAppDelegate.swift
//  ExponeaSDKProxy
//
//  Created by Michaela Okruhlicová on 21/09/2021.
//

import Foundation
import ExponeaSDK

@objc(ExponeaApplicationDelegate)
public extension class ExponeaApplicationDelegate : NSObject {
    
    @objc
     public func application(
           _ application: UIApplication,
           didFinishLaunchingWithOptions launchOptions: [UIApplication.LaunchOptionsKey: Any]?
       ) -> Bool {
//           super.application(
//               application,
//               didFinishLaunchingWithOptions: launchOptions
//           )
        return true
       }

    
}
