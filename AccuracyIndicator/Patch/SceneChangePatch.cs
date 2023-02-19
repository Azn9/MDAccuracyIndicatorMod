using AccuracyIndicator.Indicator;
using HarmonyLib;
using MelonLoader;
using UnityEngine;

namespace AccuracyIndicator.Patch;

public static class SceneChangePatch
{
    public static void OnSceneWasUnloaded(string sceneName)
    {
        if (sceneName == "GameMain")
            Destroy();
    }

    [HarmonyPatch(typeof(PnlVictory), "OnVictory")]
    internal static class VictoryPatch
    {
        private static void Postfix()
        {
            if (Main.InGameIndicator is null) // Patch may be called twice
                return;
            
            var meanDelay = Main.InGameIndicator.GetMeanDelay();
            var report = Main.InGameIndicator.GetReport();

            Object.Destroy(Main.InGameIndicator);
            Main.InGameIndicator = null;

            var victoryIndicator = Main.IndicatorObj.AddComponent<VictoryIndicator>();

            victoryIndicator.SetMeanDelay(meanDelay);
            victoryIndicator.SetReport(report);
        }
    }

    [HarmonyPatch(typeof(PnlFail), "Fail")]
    internal static class FailPatch
    {
        private static void Postfix()
        {
            Destroy();
        }
    }

    private static void Destroy()
    {
        if (Main.InGameIndicator is not null)
        {
            Object.Destroy(Main.InGameIndicator);
            Main.InGameIndicator = null;
        }

        if (Main.IndicatorObj is not null)
        {
            Object.Destroy(Main.IndicatorObj);
            Main.IndicatorObj = null;
        }
    }
}