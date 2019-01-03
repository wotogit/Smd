using System;
using System.Collections.Generic;
using System.Text;

namespace Smd.Runtime.Caching
{
    /// <summary>
    /// Names of standard caches used in Smd.
    /// </summary>
    public static class SmdCacheNames
    {
        /// <summary>
        /// Application settings cache: SmdApplicationSettingsCache.
        /// </summary>
        public const string ApplicationSettings = "SmdApplicationSettingsCache";

        /// <summary>
        /// Tenant settings cache: SmdTenantSettingsCache.
        /// </summary>
        public const string TenantSettings = "SmdTenantSettingsCache";

        /// <summary>
        /// User settings cache: SmdUserSettingsCache.
        /// </summary>
        public const string UserSettings = "SmdUserSettingsCache";

        /// <summary>
        /// Localization scripts cache: SmdLocalizationScripts.
        /// </summary>
        public const string LocalizationScripts = "SmdLocalizationScripts";
    }
}
