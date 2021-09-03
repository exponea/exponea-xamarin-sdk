//
//  ExponeaError.swift
//  Exponea
//
//  Created by Panaxeo on 19/06/2020.
//  Copyright © 2020 Panaxeo. All rights reserved.
//

import Foundation

enum ExponeaError: LocalizedError {
    case notConfigured
    case alreadyConfigured
    case flushModeNotPeriodic
    case notAvailableForPlatform(name: String)
    case parsingError(error: String)
    case fetchError(description: String)

    public var description: String {
        switch self {
        case .notConfigured:
            return "Exponea SDK is not configured. Call Exponea.configure() before calling functions of the SDK"
        case .alreadyConfigured:
            return "Exponea SDK was already configured."
        case .flushModeNotPeriodic:
            return "Flush mode is not periodic."
        case .notAvailableForPlatform(let name):
            return "\(name) is not available for iOS platform."
        case .parsingError(let error):
            return "Error when parsing:  \(error)"
        case .fetchError(let description):
            return "Data fetching failed: \(description)"
        }
    }
    
}
