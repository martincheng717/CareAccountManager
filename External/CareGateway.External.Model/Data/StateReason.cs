using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CareGateway.External.Model.Data
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum StateReason
    {
        [EnumMember(Value = "new")]
        New = 1,
        [EnumMember(Value = "nonUs")]
        NonUs = 2,
        [EnumMember(Value = "pendingOfac")]
        PendingOfac = 3,
        [EnumMember(Value = "standaloneOfacFailed")]
        StandaloneOfacFailed = 4,
        [EnumMember(Value = "ofacPass")]
        OfacPass = 5,
        [EnumMember(Value = "kyc1")]
        PendingKyc1 = 6,
        [EnumMember(Value = "actv")]
        PendingKyc1Actv = 7,
        [EnumMember(Value = "kyc1Passed")]
        Kyc1Passed = 8,
        [EnumMember(Value = "kyc1PassCipPassOfacFail")]
        Kyc1PassCipPassOfacFail = 9,
        [EnumMember(Value = "fkyc1")]
        Kyc1FailCipFailOfacPass = 10,
        [EnumMember(Value = "ofacfkyc1")]
        Kyc1FailCipFailOfacFail = 11,
        [EnumMember(Value = "ofac-h")]
        OfacHardMatch = 12,
        [EnumMember(Value = "fraudSuspect")]
        FraudSuspect = 13,
        [EnumMember(Value = "fraudConfirmed")]
        FraudConfirmed = 14,
        [EnumMember(Value = "pendingKyc2")]
        PendingKyc2 = 15,
        [EnumMember(Value = "cipPrereq")]
        CipPrereq = 16,
        [EnumMember(Value = "kyc2FailCipPassOfacFail")]
        Kyc2FailCipPassOfacFail = 17,
        [EnumMember(Value = "noid")]
        Kyc2FailCipFailOfacPassNonCurable = 18,
        [EnumMember(Value = "kyc2FailCipFailOfacPassCurableThinfile")]
        Kyc2FailCipFailOfacPassCurableThinfile = 19,
        [EnumMember(Value = "kyc2FailCipFailOfacPassNonThinfile")]
        Kyc2FailCipFailOfacPassNonThinfile = 20,
        [EnumMember(Value = "kyc2FailCipFailOfacFail")]
        Kyc2FailCipFailOfacFail = 21,
        [EnumMember(Value = "kyc2Passed")]
        Kyc2Passed = 22,
        [EnumMember(Value = "ofacMatch")]
        OfacMatch = 23,
        [EnumMember(Value = "fraudSuspectPersonalized")]
        FraudSuspectPersonalized = 24,
        [EnumMember(Value = "fraudConfirmedPersonalized")]
        FraudConfirmedPersonalized = 25,
        [EnumMember(Value = "pendingIdentityQuestions")]
        PendingIdentityQuestions = 26,
        [EnumMember(Value = "oowPassed")]
        OowPassed = 27,
        [EnumMember(Value = "needBonusQuestions")]
        NeedBonusQuestions = 28,
        [EnumMember(Value = "foow")]
        OowFailed = 29,
        [EnumMember(Value = "pendingIdv")]
        PendingIdv = 30,
        [EnumMember(Value = "fidv1")]
        BadIdvImage = 31,
        [EnumMember(Value = "fidv2")]
        IncorrectIdForIdv = 32,
        [EnumMember(Value = "fidv3")]
        IdvFail = 33,
        [EnumMember(Value = "idvFuzzyLogicMatchPassNoPriorOfacFlag")]
        IdvFuzzyLogicMatchPassNoPriorOfacFlag = 34,
        [EnumMember(Value = "idvFuzzyLogicNoMatch")]
        IdvFuzzyLogicNoMatch = 35,
        [EnumMember(Value = "fidv3Ofac")]
        IdvFuzzyLogicMatchPassPriorOfacFlag = 36,
        [EnumMember(Value = "idManualMatchPassNoPriorOfacFlag")]
        IdManualMatchPassNoPriorOfacFlag = 37,
        [EnumMember(Value = "idManualMatchPassPriorOfacFlag")]
        IdManualMatchPassPriorOfacFlag = 38,
        [EnumMember(Value = "fidv")]
        IdManualMatchFailNoPriorOfacFlag = 39,
        [EnumMember(Value = "fidvOfac")]
        IdManualMatchFailPriorOfacFlag = 40,
        [EnumMember(Value = "cust")]
        CustomerInitiatedClose = 41,
        [EnumMember(Value = "gdot")]
        FwInitiatedClose = 42,
        [EnumMember(Value = "ref")]
        ClosedFefundProcessed = 43,
        [EnumMember(Value = "fproc")]
        ProcessorDetectedFraud = 44,
        [EnumMember(Value = "comp")]
        AccountCompromised = 45,
        [EnumMember(Value = "dls")]
        DeviceLostStolen = 46,
        [EnumMember(Value = "frcip1")]
        SuspiciousCip1 = 47,
        [EnumMember(Value = "frcip2")]
        SuspiciousCip2 = 48,
        [EnumMember(Value = "frfund")]
        SuspiciousFunding = 49,
        [EnumMember(Value = "frtxrs")]
        SuspiciousSendTransfer = 50,
        [EnumMember(Value = "frtxrr")]
        SuspiciousReceiveTransfer = 51,
        [EnumMember(Value = "fracho")]
        SuspiciousAchOut = 52,
        [EnumMember(Value = "frtapo")]
        SuspiciousSpend = 53,
        [EnumMember(Value = "fmr")]
        AgentManualReview = 54,
    }
}
