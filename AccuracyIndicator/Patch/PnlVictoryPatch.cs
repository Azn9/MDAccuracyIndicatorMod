using AccuracyIndicator.Indicator;
using Il2Cpp;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Object = Il2CppSystem.Object;

namespace AccuracyIndicator.Patch;

[HarmonyPatch(typeof(PnlVictory), nameof(PnlVictory.OnVictory), typeof(Object), typeof(Object), typeof(Il2CppReferenceArray<Object>))]
internal static class PnlVictoryPatch
{
    private static void Postfix()
    {
        if (GameIndicator == null)
        {
            return;
        }

        var meanDelay = GameIndicator.GetMeanDelay();
        var report = GameIndicator.GetReport();

        GameIndicator.Destroy();
        GameIndicator = null;

        if (!Settings.ShowMeanDelay)
        {
            return;
        }

        var victoryIndicator = IndicatorCanvas.AddComponent<VictoryIndicator>();

        victoryIndicator.SetMeanDelay(meanDelay);
        victoryIndicator.SetReport(report);
    }
}