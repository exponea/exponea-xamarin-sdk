//
//  ExponeaProxy.swift
//  ExponeaSDKProxy
//
//  Created by Michaela Okruhlicová on 27/07/2021.
//

import Foundation
import ExponeaSDK
import UserNotifications

// This protocol is used queried using reflection by native iOS SDK to see if it's run by Xamarin SDK
@objc(IsExponeaXamarinSDK)
protocol IsExponeaXamarinSDK {
}
@objc(ExponeaXamarinVersion)
public class ExponeaXamarinVersion: NSObject, ExponeaVersionProvider {
    required public override init() { }
    public func getVersion() -> String {
        "0.9.9"
    }
}

@objc(Exponea)
public class Exponea : NSObject {
    
    @objc
    public static let instance = Exponea()
    
    static let defaultFlushPeriod = 5 * 60
    static var shared: ExponeaType = ExponeaSDK.Exponea.shared
    
    @objc
    public func configure(configuration: NSDictionary) {
        let parser = ConfigurationParser(configuration)
        guard !isConfigured() else {
            print(ExponeaError.alreadyConfigured.description)
            return
        }
        do {
            Exponea.shared.configure(
                try parser.parseProjectSettings(),
                pushNotificationTracking: try parser.parsePushNotificationTracking(),
                automaticSessionTracking: try parser.parseSessionTracking(),
                defaultProperties: try parser.parseDefaultProperties(),
                flushingSetup: try parser.parseFlushingSetup()
            )
        } catch {
            print(ExponeaError.parsingError(error: error.localizedDescription).description)
        }
    }
    
    @objc
    public func trackEvent(
        eventType: String,
        properties: NSDictionary,
        timestamp: Double
    ) {
        guard isConfigured() else {
            print(ExponeaError.notConfigured.description)
            return
        }
        do {
            Exponea.shared.trackEvent(
                properties:  try JsonDataParser.parse(dictionary: properties),
                timestamp: timestamp,
                eventType: eventType
            )
        } catch {
            print(ExponeaError.parsingError(error: error.localizedDescription).description)
        }
    }
    
    @objc
    public func trackEvent(
        eventType: String,
        properties: NSDictionary
    ) {
        guard isConfigured() else {
            print(ExponeaError.notConfigured.description)
            return
        }
        do {
            Exponea.shared.trackEvent(
                properties:  try JsonDataParser.parse(dictionary: properties), timestamp: nil,
                eventType: eventType
            )
        } catch {
            print(ExponeaError.parsingError(error: error.localizedDescription).description)
        }
    }

    
    @objc
    public func anonymize(
        exponeaProjectDictionary: NSDictionary?,
        projectMappingDictionary: NSDictionary?
    ) {
        guard isConfigured() else {
            print(ExponeaError.notConfigured.description)
            return
        }
        do {
            var exponeaProject: ExponeaProject?
            var projectMapping: [EventType: [ExponeaProject]]?
            if let exponeaProjectValue: NSDictionary = exponeaProjectDictionary {
                if (exponeaProjectValue.count != 0) {
                    exponeaProject = try ConfigurationParser.parseExponeaProject(
                        dictionary: exponeaProjectValue,
                        defaultBaseUrl: Exponea.shared.configuration?.baseUrl ?? Constants.Repository.baseUrl
                    )
                }
            }
            if let projectMappingValue: NSDictionary = projectMappingDictionary {
                if (projectMappingValue.count != 0) {
                    projectMapping = try ConfigurationParser.parseProjectMapping(
                        dictionary: projectMappingValue,
                        defaultBaseUrl: Exponea.shared.configuration?.baseUrl ?? Constants.Repository.baseUrl
                    )
                }
            }
            if let exponeaProject = exponeaProject {
                Exponea.shared.anonymize(exponeaProject: exponeaProject, projectMapping: projectMapping)
            } else {
                if projectMapping != nil {
                    throw ExponeaError.notAvailableForPlatform(
                        name: "Changing project mapping in anonymize without changing project"
                    )
                } else {
                    Exponea.shared.anonymize()
                }
            }
        } catch {
            print(ExponeaError.parsingError(error: error.localizedDescription).description)
        }
    }
    
    @objc
    public func anonymize() {
        anonymize(exponeaProjectDictionary: nil, projectMappingDictionary: nil);
    }
    
    @objc
    public func anonymize(exponeaProjectDictionary: NSDictionary?) {
        anonymize(exponeaProjectDictionary: exponeaProjectDictionary, projectMappingDictionary: nil);
    }
    
    @objc
    public func handlePushNotificationToken(deviceToken: Data) {
        Exponea.shared.handlePushNotificationToken(deviceToken: deviceToken)
    }
    
    @objc
    public func handlePushNotificationOpened(response: UNNotificationResponse) {
        Exponea.shared.handlePushNotificationOpened(response: response)
    }
    
    @objc
    public func handlePushNotificationOpened(userInfo: [AnyHashable: Any], actionIdentifier: String? = nil) {
        Exponea.shared.handlePushNotificationOpened(userInfo: userInfo, actionIdentifier: actionIdentifier)
    }
    
    @objc
    public func handleCampaignClick(url: URL, timestamp: Double) {
        Exponea.shared.trackCampaignClick(url: url, timestamp: timestamp)
    }
    
    @objc
    public func flushData() {
        guard isConfigured() else {
            print(ExponeaError.notConfigured.description)
            return
        }
        Exponea.shared.flushData()
    }
    
    @objc
    public func flushData(done: @escaping (String)->()) {
        guard isConfigured() else {
            print(ExponeaError.notConfigured.description)
            return
        }
        Exponea.shared.flushData { result in
            var message = ""
            switch result {
            case .error(let error): message = "Error while flushing: \(error.localizedDescription)"
            case .success(let count): message = "Flush done, \(count) objects flushed."
            case .flushAlreadyInProgress: message = "Flush already in progress."
            case .noInternetConnection: message = "No internet connection."
            }
            done(message)
        }
    }
    
    @objc
    public func getCustomerCookie() -> String? {
        return Exponea.shared.customerCookie;
    }
    
    @objc
    public func isConfigured() -> Bool {
        return Exponea.shared.isConfigured;
    }
    
    @objc
    public func checkPushSetup() {
        if (!isConfigured()) {
            Exponea.shared.checkPushSetup = true
        }
    }
    
    @objc
    public func isSaveModeEnabled() -> Bool{
        return Exponea.shared.safeModeEnabled;
    }
    
    @objc
    public func isAutoSessionTrackingEnabled() -> Bool{
        guard isConfigured() else {
            print(ExponeaError.notConfigured.description)
            return false;
        }
        if let configuration = Exponea.shared.configuration{
            return configuration.automaticSessionTracking;
        } else {
            print(ExponeaError.notConfigured.description)
            return false
        }
    }
    
    @objc
    public func getFlushMode() -> String {
        switch Exponea.shared.flushingMode {
            case .automatic: return "APP_CLOSE"
            case .immediate: return "IMMEDIATE"
            case .periodic: return "PERIOD"
            case .manual: return "MANUAL"
        }
    }
    
    @objc
    public func setFlushMode(flushMode: String) {
        switch flushMode {
        case "APP_CLOSE":
            Exponea.shared.flushingMode = .automatic
        case "IMMEDIATE":
            Exponea.shared.flushingMode = .immediate
        case "PERIOD":
            Exponea.shared.flushingMode = .periodic(Exponea.defaultFlushPeriod)
        case "MANUAL":
            Exponea.shared.flushingMode = .manual
        default:
            let error = ExponeaDataError.invalidValue(for: "flush mode")
            print(error)
        }
    }
    
    @objc
    public func getFlushPeriod() -> Int{
        switch Exponea.shared.flushingMode {
        case .periodic(let period):
            return period
        default:
            print(ExponeaError.flushModeNotPeriodic.description)
            return 0;
        }
    }

    @objc
    public func setFlushPeriod(flushPeriod: NSNumber) {
        switch Exponea.shared.flushingMode {
        case .periodic:
            Exponea.shared.flushingMode = .periodic(flushPeriod.intValue)
        default:
            print(ExponeaError.flushModeNotPeriodic.description)
        }
    }
    
    @objc
    public func getLogLevel() -> String {
        switch ExponeaSDK.Exponea.logger.logLevel {
        case .none:
            return "OFF"
        case .verbose:
            return "VERBOSE"
        case .warning:
            return "WARN"
        case .error:
            return "ERROR"
        }
    }
    
    @objc
    public func setLogLevel(logLevel: String) {
        switch logLevel {
        case "OFF":
            ExponeaSDK.Exponea.logger.logLevel = .none
        case "ERROR":
            ExponeaSDK.Exponea.logger.logLevel = .error
        case "WARN":
            ExponeaSDK.Exponea.logger.logLevel = .warning
        case "INFO":
            print(ExponeaError.notAvailableForPlatform(name: "INFO log level").description)
        case "DEBUG":
            print(ExponeaError.notAvailableForPlatform(name: "DEBUG log level").description)
        case "VERBOSE":
            ExponeaSDK.Exponea.logger.logLevel = .verbose
        default:
            print(ExponeaDataError.invalidValue(for: "Log level").localizedDescription)
        }
    }
    
    @objc
    public func getDefaultProperties() -> String {
        guard let properties = Exponea.shared.defaultProperties else {
            return "{}"
        }
        do {
            if let data = String(
                data: try JSONSerialization.data(withJSONObject: properties),
                encoding: .utf8
            ) {
                return data
            } else {
                print("Error during default properties deserialization (getDefaultProperties)")
                return "{}"
            }
        } catch {
            print(error)
            return "{}"
        }
    }

    @objc
    public func setDefaultProperties(
        properties: NSDictionary
    ) {
        guard isConfigured() else {
            print(ExponeaError.notConfigured.description)
            return
        }
        do {
            let parsedProperties = try JsonDataParser.parse(dictionary: properties)
            Exponea.shared.defaultProperties = parsedProperties
        } catch {
            print(ExponeaError.parsingError(error: error.localizedDescription).description)
        }
    }
    
    @objc
    public func identifyCustomer(
        customerIds: NSDictionary,
        properties: NSDictionary
    ) {
        guard isConfigured() else {
            print(ExponeaError.notConfigured.description)
            return
        }
        do {
            Exponea.shared.identifyCustomer(
                customerIds: try JsonDataParser.parse(dictionary: customerIds).mapValues {
                    if case .string(let stringValue) = $0.jsonValue {
                        return stringValue
                    } else {
                        throw ExponeaDataError.invalidType(for: "customer id (only string values are supported)")
                    }
                },
                properties: try JsonDataParser.parse(dictionary: properties),
                timestamp: nil
            )
        } catch {
            print(ExponeaError.parsingError(error: error.localizedDescription).description)
        }
    }
    
    @objc
    public func trackSessionStart() {
        guard isConfigured() else {
            print(ExponeaError.notConfigured.description)
            return
        }
        Exponea.shared.trackSessionStart()
    }

    @objc
    public func trackSessionEnd() {
        guard isConfigured() else {
            print(ExponeaError.notConfigured.description)
            return
        }
        Exponea.shared.trackSessionEnd()
       
    }
    
    @objc
    public func trackPushOpened(userInfo: [AnyHashable: Any]) {
        Exponea.shared.trackPushOpened(with: userInfo)
    }
    
    @objc
    public func trackPushToken(token: String) {
        Exponea.shared.trackPushToken(token)
    }
    
    @objc
    public func trackPayment(
        properties: NSDictionary,
        timestamp: Double
    ) {
        guard isConfigured() else {
            print(ExponeaError.notConfigured.description)
            return
        }
        do {
            Exponea.shared.trackPayment(
                properties:  try JsonDataParser.parse(dictionary: properties),
                timestamp: timestamp
            )
        } catch {
            print(ExponeaError.parsingError(error: error.localizedDescription).description)
        }
    }
    
    @objc
    public func fetchConsents(
        success: @escaping (String)->(),
        fail: @escaping (String)->()
    ){
        guard isConfigured() else {
            fail(ExponeaError.notConfigured.description)
            return
        }
        Exponea.shared.fetchConsents { result in
            switch result {
            case .success(let response):
                let mappedConsents = response.consents.map { consent in
                    return [
                        "id": consent.id,
                        "legitimateInterest": consent.legitimateInterest,
                        "sources": [
                            "createdFromCRM": consent.sources.isCreatedFromCRM,
                            "imported": consent.sources.isImported,
                            "privateAPI": consent.sources.privateAPI,
                            "publicAPI": consent.sources.publicAPI,
                            "trackedFromScenario": consent.sources.isTrackedFromScenario
                        ],
                        "translations": consent.translations
                    ]
                }
                do {
                    if let data = String(data: try JSONSerialization.data(withJSONObject: mappedConsents), encoding: .utf8) {
                        success(data)
                    } else {
                        fail("Error during response deserialization (fetchConsents)")
                    }
                } catch {
                    fail(error.localizedDescription)
                }
            case .failure(let error):
                fail(error.localizedDescription)
            }
        }
    }
    
    @objc
    public func fetchRecommendations(
        optionsDictionary: NSDictionary,
        success: @escaping (String)->(),
        fail: @escaping (String)->()
    ){
        guard isConfigured() else {
            fail(ExponeaError.notConfigured.description)
            return
        }
        do {
            let options = RecommendationOptions(
                id: try optionsDictionary.getRequiredSafely(property: "id"),
                fillWithRandom: try optionsDictionary.getRequiredSafely(property: "fillWithRandom"),
                size: try optionsDictionary.getOptionalSafely(property: "size") ?? 10,
                items: try optionsDictionary.getOptionalSafely(property: "items"),
                noTrack: try optionsDictionary.getOptionalSafely(property: "noTrack") ?? false,
                catalogAttributesWhitelist: try optionsDictionary.getOptionalSafely(
                    property: "catalogAttributesWhitelist"
                )
            )
            
            Exponea.shared.fetchRecommendation(
                with: options,
                completion: {(result: Result<RecommendationResponse<AllRecommendationData>>) in
                    switch result {
                    case .success(let response):
                        guard let data = response.value else {
                            fail(" Data fetching failed: Empty result.")
                            return
                        }
                        let mappedRecommendations: [[String: Any?]] = data.map { recommendation in
                            return recommendation.userData.data
                        }
                        do {
                            if let data = String(
                                data: try JSONSerialization.data(withJSONObject: mappedRecommendations),
                                encoding: .utf8
                            ) {
                                success(data)
                            } else {
                                fail("Error during response deserialization (fetchRecommendations)")
                            }
                        } catch {
                            fail(error.localizedDescription)
                        }
                    case .failure(let error):
                        fail(error.localizedDescription)
                    }
                }
            )
        } catch {
            fail(error.localizedDescription)
        }
    }
    
    @objc
    public static func isExponeaNotification(userInfo: [AnyHashable: Any]) -> Bool {
        return ExponeaSDK.Exponea.isExponeaNotification(userInfo: userInfo)
    }
    
    @objc
    public func setInAppMessageDelegate(
        overrideDefaultBehavior: Bool,
        trackActions: Bool,
        action: @escaping (_ message: SimpleInAppMessage, _ buttonText: String?, _ buttonUrl: String?, _ interaction: Bool)->()) {
        Exponea.shared.inAppMessagesDelegate = InAppDelegate(overrideDefaultBehavior: overrideDefaultBehavior,
                                                             trackActions: trackActions,
                                                             action: action)
    }
    
    @objc
    public func trackInAppMessageClick(
        message: SimpleInAppMessage,
        buttonText: String?,
        buttonLink: String?) {
        guard isConfigured() else {
            print(ExponeaError.notConfigured.description)
            return
        }
        let inAppMessage = InAppMessage(
            id: message.id,
            name: message.name,
            rawMessageType: message.rawMessageType,
            rawFrequency: message.rawFrequency,
            variantId: message.variantId,
            variantName: message.variantName,
            trigger: EventFilter(eventType: message.eventType, filter: []),
            dateFilter: DateFilter(
                enabled: false,
                startDate: Date(timeIntervalSince1970: 1570744800),
                endDate: nil
            ),
            priority: message.priority,
            delayMS: message.delayMS,
            timeoutMS: message.timeoutMS,
            payloadHtml: nil,
            isHtml: message.rawMessageType == "freeform"
            )
            Exponea.shared.trackInAppMessageClick(message: inAppMessage, buttonText: buttonText, buttonLink: buttonLink)
            
    }
    @objc
    public func trackInAppMessageClose(
        message: SimpleInAppMessage
    ) {
        guard isConfigured() else {
            print(ExponeaError.notConfigured.description)
            return
        }
        let inAppMessage = InAppMessage(
            id: message.id,
            name: message.name,
            rawMessageType: message.rawMessageType,
            rawFrequency: message.rawFrequency,
            variantId: message.variantId,
            variantName: message.variantName,
            trigger: EventFilter(eventType: message.eventType, filter: []),
            dateFilter: DateFilter(
                enabled: false,
                startDate: Date(timeIntervalSince1970: 1570744800),
                endDate: nil
            ),
            priority: message.priority,
            delayMS: message.delayMS,
            timeoutMS: message.timeoutMS,
            payloadHtml: nil,
            isHtml: message.rawMessageType == "freeform"
            )
            Exponea.shared.trackInAppMessageClose(message: inAppMessage)
    }
}

