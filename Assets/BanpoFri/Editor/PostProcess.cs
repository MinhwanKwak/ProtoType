#if UNITY_IOS || UNITY_IPHONE
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using System.Collections.Generic;
using UnityEngine;

namespace BanpoFri{
    public class PostProcess
    {
        private const string AppLovinMaxResourcesDirectoryName = "AppLovinMAXResources";
        private const bool UseMax = true;

        [PostProcessBuildAttribute(int.MaxValue)]
        public static void OnPostProcessBuild(BuildTarget buildTarget, string buildPath)
        {
            var plistPath = Path.Combine(buildPath, "Info.plist");
            var plist = new PlistDocument();
            plist.ReadFromFile(plistPath);

            plist.root.SetBoolean("ITSAppUsesNonExemptEncryption", false);
            plist.root.SetString("NSAdvertisingAttributionReportEndpoint", "https://appsflyer-skadnetwork.com/");
            plist.root.SetString("SKAdNetworkIdentifier","4w7y6s5ca2.skadnetwork");
            plist.root.SetString("NSUserTrackingUsageDescription", "This identifier will be used to deliver personalized ads to you.");
            //plist.root.SetString("FacebookAppID", "580882013229001");

            // var array = plist.root["CFBundleURLTypes"].AsArray();
            // var dic = array.AddDict();
            // var urlInnerArray = dic.CreateArray("CFBundleURLSchemes");
            // urlInnerArray.AddString("com.googleusercontent.apps.815128155902-l5pcruie723i96dg1rqgsk2fv2rqic01");
            
            var ids = new string[] {
                "488r3q3dtq",
                "5a6flpkh64",
                "x44k69ngh6",
                "22mmun2rn5",
                "wg4vff78zm",
                "f73kdq92p3",
                "zmvfpc5aq8",
                "yclnxrl5pm",
                "mp6xlyr22a",
                "hs6bdukanm",
                "v72qych5uu",
                "k674qkevps",
                "prcb7njmu6",
                "5lm9lj6jb7",
                "3rd42ekr43",
                "5tjdwbrq8w",
                "4468km3ulz",
                "424m5254lk",
                "f7s53z58qe",
                "ppxm28t8ap",
                "ydx93a7ass",
                "mlmmfzh3r3",
                "32z4fx6l9h",
                "w9q455wk68",
                "cstr6suwn9",
                "m8dbw4sv7c",
                "3qy4746246",
                "zq492l623r",
                "8s468mfl3y",
                "av6w8kgt66",
                "t38b2kh725",
                "lr83yxwka7",
                "9t245vhmpl",
                "s39g8k73mm",
                "v79kvwwj4g",
                "2u9pt9hc89",
                "4dzt52r2t5",
                "9rd848q2bz",
                "f38h382jlk",
                "tl55sbb4fm",
                "4pfyvq9l8r",
                "4fzdc2evr5",
                "glqzh8vgby",
                "578prtvx9j",
                "7ug5zh24hu",
                "wzmmz9fp6w",
                "3sh42y64q3",
                "c6k4g5qg8m",
                "kbd757ywx3",
                "238da6jt44",
                "44jx6755aq",
                "v9wttpbfk9",
                "n38lu8286q",
                "294l99pt4k",
            };

            var arraySKAdNetworkItems = plist.root.CreateArray("SKAdNetworkItems");

            foreach(var id in ids)
            {
                var dictSKAdNetworkIdentifier_FAN = arraySKAdNetworkItems.AddDict();
                dictSKAdNetworkIdentifier_FAN.SetString("SKAdNetworkIdentifier", $"{id}.skadnetwork");
            }

            File.WriteAllText(plistPath, plist.WriteToString());

            var projectPath = PBXProject.GetPBXProjectPath(buildPath);
            var project = new PBXProject();
            project.ReadFromFile(projectPath);

            var targetGuid = project.GetUnityMainTargetGuid();
            project.AddCapability(targetGuid, PBXCapabilityType.InAppPurchase);
            project.SetBuildProperty(targetGuid, "ENABLE_BITCODE", "NO");
            
            //var fileGuid = project.AddFile(Path.Combine(buildPath, "GoogleService-Info.plist"), Path.Combine(buildPath, "GoogleService-Info.plist"));
            //project.AddFileToBuild(targetGuid, fileGuid);

            foreach (var tg in new[] { targetGuid, project.GetUnityFrameworkTargetGuid() })
            {
                project.SetBuildProperty(tg, "ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES", "NO");
            }
           
            project.SetBuildProperty(targetGuid, "ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES", "YES");
 


            if(UseMax)
            {
                var loadInst = AssetDatabase.LoadAssetAtPath<AppNameLocalize>("Assets/BanpoFri/Editor/AppNameLocalize.asset");
                if (loadInst != null)
                {
                    System.Action<string, string> SaveLocalFile = (localizeString, localeCode) => {
                        var path = GetLocalFilePath(buildPath, localeCode, project, targetGuid);
                        var localizedDescriptionLine = "\"CFBundleDisplayName\" = \"" + localizeString + "\";\n";
                        // File already exists, update it in case the value changed between builds.
                        if (File.Exists(path))
                        {
                            var output = new List<string>();
                            var lines = File.ReadAllLines(path);
                            var keyUpdated = false;
                            foreach (var line in lines)
                            {
                                if (line.Contains("CFBundleDisplayName"))
                                {
                                    output.Add(localizedDescriptionLine);
                                    keyUpdated = true;
                                }
                                else
                                {
                                    output.Add(line);
                                }
                            }

                            if (!keyUpdated)
                            {
                                output.Add(localizedDescriptionLine);
                            }

                            File.WriteAllText(path, string.Join("\n", output.ToArray()) + "\n");
                        }
                        else
                        {
                            File.WriteAllText(path, "/* Localized versions of Info.plist keys - Generated by AL MAX plugin */\n" + localizedDescriptionLine);
                        }
                    };
                    SaveLocalFile(loadInst.en, "en");
                    SaveLocalFile(loadInst.ko, "ko");
                    SaveLocalFile(loadInst.ja, "ja");
                    SaveLocalFile(loadInst.de, "de");
                    SaveLocalFile(loadInst.es, "es");
                    SaveLocalFile(loadInst.fr, "fr");
                    SaveLocalFile(loadInst.zhHans, "zh-Hans");
                    SaveLocalFile(loadInst.zhHant, "zh-Hant");
                }

                var loadAttInst = AssetDatabase.LoadAssetAtPath<IosAttLocalize>("Assets/BanpoFri/Editor/IosAttLocalize.asset");
                if (loadAttInst != null)
                {
                    System.Action<string, string> SaveLocalFile = (localizeString, localeCode) =>
                    {
                        var path = GetLocalFilePath(buildPath, localeCode, project, targetGuid);
                        var localizedDescriptionLine = "\"NSUserTrackingUsageDescription\" = \"" + localizeString + "\";\n";
                        // File already exists, update it in case the value changed between builds.
                        if (File.Exists(path))
                        {
                            var output = new List<string>();
                            var lines = File.ReadAllLines(path);
                            var keyUpdated = false;
                            foreach (var line in lines)
                            {
                                if (line.Contains("NSUserTrackingUsageDescription"))
                                {
                                    output.Add(localizedDescriptionLine);
                                    keyUpdated = true;
                                }
                                else
                                {
                                    output.Add(line);
                                }
                            }

                            if (!keyUpdated)
                            {
                                output.Add(localizedDescriptionLine);
                            }

                            File.WriteAllText(path, string.Join("\n", output.ToArray()) + "\n");
                        }
                    };
                    SaveLocalFile(loadAttInst.en, "en");
                    SaveLocalFile(loadAttInst.ko, "ko");
                    SaveLocalFile(loadAttInst.ja, "ja");
                    SaveLocalFile(loadAttInst.de, "de");
                    SaveLocalFile(loadAttInst.es, "es");
                    SaveLocalFile(loadAttInst.fr, "fr");
                    SaveLocalFile(loadAttInst.zhHans, "zh-Hans");
                    SaveLocalFile(loadAttInst.zhHant, "zh-Hant");
                }
            }
            else
            {
                var localizeList = new List<string>();
                localizeList.Add("Base.lproj");
                localizeList.Add("ko.lproj");
                localizeList.Add("ja.lproj");
                localizeList.Add("en.lproj");

                Dictionary<string, string> folders = new Dictionary<string, string>();
                foreach (var data in localizeList)
                {
                    if (!Directory.Exists(buildPath + data))
                        Directory.CreateDirectory(buildPath + "/" + data);
                    else
                    {
                        FileUtil.DeleteFileOrDirectory(buildPath + data);
                        Directory.CreateDirectory(buildPath + "/" + data);
                    }

                    FileUtil.CopyFileOrDirectory(Application.dataPath + $"/BanpoFri/IosAppName/{data}/InfoPlist.strings", buildPath + "/" + data + "/InfoPlist.strings");
                    folders.Add(data, "./");
                }

                foreach(var folder in folders)
                {
                    string guid = project.AddFolderReference(folder.Value + folder.Key, folder.Key, PBXSourceTree.Source);
                    project.AddFileToBuild(targetGuid, guid);
                }
            }
            

            project.WriteToFile (projectPath);

            // get entitlements path
            string[] idArray = Application.identifier.Split('.');
            var entitlementsPath = $"Unity-iPhone/{idArray[idArray.Length - 1]}.entitlements";
    
            // create capabilities manager
            var capManager = new ProjectCapabilityManager(projectPath, entitlementsPath, null, targetGuid);
    
            // Add necessary capabilities
            capManager.AddPushNotifications(true);
            capManager.AddSignInWithApple();
    
            // Write to file
            capManager.WriteToFile();
        }

        private static string GetLocalFilePath(string buildPath, string localeCode, PBXProject project, string targetguid)
        {
            var resourcesDirectoryPath = Path.Combine(buildPath, "Localize");
            var localeSpecificDirectoryName = localeCode + ".lproj";
            var localeSpecificDirectoryPath = Path.Combine(resourcesDirectoryPath, localeSpecificDirectoryName);
            var infoPlistStringsFilePath = Path.Combine(localeSpecificDirectoryPath, "InfoPlist.strings");

            if (!Directory.Exists(resourcesDirectoryPath))
            {
                Directory.CreateDirectory(resourcesDirectoryPath);
            }

            if (!Directory.Exists(localeSpecificDirectoryPath))
            {
                Directory.CreateDirectory(localeSpecificDirectoryPath);

                var localeSpecificDirectoryRelativePath = Path.Combine("Localize", localeSpecificDirectoryName);
                var guid = project.AddFolderReference(localeSpecificDirectoryRelativePath, localeSpecificDirectoryRelativePath);
                project.AddFileToBuild(targetguid, guid);

            }

            return infoPlistStringsFilePath;
        }

        private static string GetMaxLocalFilePath(string buildPath, string localeCode)
        {
            var resourcesDirectoryPath = Path.Combine(buildPath, AppLovinMaxResourcesDirectoryName);
            var localeSpecificDirectoryName = localeCode + ".lproj";
            var localeSpecificDirectoryPath = Path.Combine(resourcesDirectoryPath, localeSpecificDirectoryName);
            return Path.Combine(localeSpecificDirectoryPath, "InfoPlist.strings");
        }
    }
}

#endif