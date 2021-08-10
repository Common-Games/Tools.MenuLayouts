using System;
using JetBrains.Annotations;

namespace CGTK.Tools.CustomizableMenus
{

    [PublicAPI]
    internal static class PackageConstants
    {
        public const String PACKAGE_COMPANY = "com.common-games.";
        public const String PACKAGE_GROUP   = "tools.";
        
        public const String PACKAGE_NAME    = PACKAGE_COMPANY + PACKAGE_GROUP + "custom-menu-layouts";
        public const String PACKAGE_PATH    = "Packages/" + PACKAGE_NAME + "/";
        
        public const String FOLDER_EDITOR    = PACKAGE_PATH + "Editor/";

        public const String PREFERENCE_PATH = "Preferences/CGTK/Tools/Custom Menu Layouts";

        public const String TEST_ASSEMBLY = "CGTK.Tools.CustomMenuLayouts.Tests";
    }
}
