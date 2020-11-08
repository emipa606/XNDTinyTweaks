using System;
using System.Reflection;
using Verse;
using System.Xml;

namespace TinyTweaks
{

    public class PatchOperationCheckModSetting : PatchOperation
    {

        private readonly Type settingsType;
        private readonly string settingName;

        protected override bool ApplyWorker(XmlDocument xml)
        {
            if (settingsType == null)
            {
                LogPatchOperationError($"Could not find settings type {settingsType}");
                return false;
            }

            var settingInfo = settingsType.GetField(settingName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            if (settingInfo == null)
            {
                LogPatchOperationError($"{settingName} could not be found");
                return false;
            }
            return (bool)settingInfo.GetValue(null);
        }

        private void LogPatchOperationError(string message)
        {
            Log.Error($"Error with PatchOperationCheckModSetting in {sourceFile}: {message}");
        }
    }

}
