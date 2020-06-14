using System;
using System.IO;
using System.Security;

namespace Core
{
    public static class Constants
    {
        public static string Database => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "database.db");
        public static string Images => $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}/localImages";
        public static string Audios => $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}/localAudios";

        public const string FingerprintKey = "fpk";
        public const string DateTokenKey = "dtk";
        public const string AuthSessionKey = "ask";
        public const string LoggedUserKey = "luk";
        public const string UserModelKey = "umk";
        public const string LoginCompleteKey = "lck";
        public const string DeviceIdKey = "dik";
        public const string AuthorizationModelKey = "amk";
        public const string SecureEncryptKey = "sek";
        public const string UserEmailKey = "uek";
        public const string UpdateUser = "us";
        public const string PinKey = "pk";
        public const string PinBlockedTimeKey = "pbtk";
        public const string FirstLauch = "flk";
        public const string MessageUnSended = "mus";
        public const string BadgeConversationCount = "bcc";

        public const string TempPushKitToken = "tpkt";
        public const string PushKitToken = "pkt";

        public const string TempPushKitTokenChannel = "tpktc";
        public const string PushKitTokenChannel = "pktc";
    }
}
