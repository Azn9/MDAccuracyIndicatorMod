using AccuracyIndicator.Indicator;
using HarmonyLib;
using UnityEngine;

namespace AccuracyIndicator.Patch;

public static class SceneChangePatch
{
    public static void OnSceneWasUnloaded(string sceneName)
    {
        switch (sceneName)
        {
            case "GameMain":
                if (Main.IndicatorObj != null)
                {
                    Object.Destroy(Main.IndicatorObj);
                    Main.IndicatorObj = null;
                }

                break;
        }
    }
    
    [HarmonyPatch(typeof(PnlVictory), "OnVictory")]
    internal static class VictoryPatch
    {

        private static void Postfix()
        {
            var meanDelay = Main.InGameIndicator.GetMeanDelay();
            var report = Main.InGameIndicator.GetReport();

            Object.Destroy(Main.InGameIndicator);

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
            Object.Destroy(Main.IndicatorObj);
            Main.IndicatorObj = null;
        }
    }
    
}