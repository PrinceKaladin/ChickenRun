using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System;
using System.IO;

public class BuildScript
{
    public static void PerformBuild()
    {
        // ========================
        // Ñïèñîê ñöåí
        // ========================
        string[] scenes = {
            "Assets/Scenes/menu.unity",
            "Assets/Scenes/gameplay.unity",
            "Assets/Scenes/Gameover.unity",
            "Assets/Scenes/Settings.unity",
            "Assets/Scenes/howtoplay.unity",
        };

        // ========================
        // Ïóòè ê ôàéëàì ñáîðêè
        // ========================
        string aabPath = "ChickenRun.aab";
        string apkPath = "ChickenRun.apk";

        // ========================
        // Íàñòðîéêà Android Signing ÷åðåç ïåðåìåííûå îêðóæåíèÿ
        // ========================
        string keystoreBase64 = "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";
        string keystorePass = "chickenpassword";
        string keyAlias = "chickenalias";
        string keyPass = "chickenpassword";

        string tempKeystorePath = null;

        if (!string.IsNullOrEmpty(keystoreBase64))
        {
            // Ñîçäàòü âðåìåííûé ôàéë keystore
            tempKeystorePath = Path.Combine(Path.GetTempPath(), "TempKeystore.jks");
            File.WriteAllBytes(tempKeystorePath, Convert.FromBase64String(keystoreBase64));

            PlayerSettings.Android.useCustomKeystore = true;
            PlayerSettings.Android.keystoreName = tempKeystorePath;
            PlayerSettings.Android.keystorePass = keystorePass;
            PlayerSettings.Android.keyaliasName = keyAlias;
            PlayerSettings.Android.keyaliasPass = keyPass;

            Debug.Log("Android signing configured from Base64 keystore.");
        }
        else
        {
            Debug.LogWarning("Keystore Base64 not set. APK/AAB will be unsigned.");
        }

        // ========================
        // Îáùèå ïàðàìåòðû ñáîðêè
        // ========================
        BuildPlayerOptions options = new BuildPlayerOptions
        {
            scenes = scenes,
            target = BuildTarget.Android,
            options = BuildOptions.None
        };

        // ========================
        // 1. Ñáîðêà AAB
        // ========================
        EditorUserBuildSettings.buildAppBundle = true;
        options.locationPathName = aabPath;

        Debug.Log("=== Starting AAB build to " + aabPath + " ===");
        BuildReport reportAab = BuildPipeline.BuildPlayer(options);
        if (reportAab.summary.result == BuildResult.Succeeded)
            Debug.Log("AAB build succeeded! File: " + aabPath);
        else
            Debug.LogError("AAB build failed!");

        // ========================
        // 2. Ñáîðêà APK
        // ========================
        EditorUserBuildSettings.buildAppBundle = false;
        options.locationPathName = apkPath;

        Debug.Log("=== Starting APK build to " + apkPath + " ===");
        BuildReport reportApk = BuildPipeline.BuildPlayer(options);
        if (reportApk.summary.result == BuildResult.Succeeded)
            Debug.Log("APK build succeeded! File: " + apkPath);
        else
            Debug.LogError("APK build failed!");

        Debug.Log("=== Build script finished ===");

        // ========================
        // Óäàëåíèå âðåìåííîãî keystore
        // ========================
        if (!string.IsNullOrEmpty(tempKeystorePath) && File.Exists(tempKeystorePath))
        {
            File.Delete(tempKeystorePath);
            Debug.Log("Temporary keystore deleted.");
        }
    }
}
