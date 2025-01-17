﻿using System;
using System.Diagnostics;
using System.IO;
using System.Management.Automation;
using System.Net;

namespace ThisIsWin11.OpenTweaks.Assessment.System
{
    internal class InstallWSA : AssessmentBase
    {
        private static readonly ErrorHelper logger = ErrorHelper.Instance;

        public override string ID()
        {
            return "Install Microsoft Windows Subsystem for Android";
        }

        public override string Info()
        {
            return "Installation is currently ONLY possible in Windows Insider Dev or Beta channel.";
        }

        public override bool CheckAssessment()
        {
            var appPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Packages", "MicrosoftCorporationII.WindowsSubsystemForAndroid_8wekyb3d8bbwe");

            if (!Directory.Exists(appPath))
            {
                return true;
            }

            return false;
        }

        public override bool DoAssessment()
        {
            try
            {
                using (var client = new WebClient())
                {
                    logger.Log("- Installing WSA");
                    WindowsHelper.RunWT("winget install \"9p3395vx91nr\" --accept-source-agreements --accept-package-agreements");
                    logger.Log("Done.");
                    return true;
                }
            }
            catch (Exception ex)
            { logger.Log("{0}", ex.Message); }

            return false;
        }

        public override bool UndoAssessment()
        {
            try
            {
                using (var client = new WebClient())
                {
                    logger.Log("- Uninstalling WSA...");
                    WindowsHelper.RunWT("winget uninstall \"9p3395vx91nr\"");
                    return true;
                }
            }
            catch (Exception ex)
            { logger.Log("{0}", ex.Message); }

            return false;
        }
    }
    
}