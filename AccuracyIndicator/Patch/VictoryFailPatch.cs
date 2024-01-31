using AccuracyIndicator.Indicator;
using Il2Cpp;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Object = UnityEngine.Object;

namespace AccuracyIndicator.Patch;

internal static class VictoryFailPatch
{
    [HarmonyPatch(typeof(PnlVictory), nameof(PnlVictory.OnVictory),
        typeof(Il2CppSystem.Object), typeof(Il2CppSystem.Object), typeof(Il2CppReferenceArray<Il2CppSystem.Object>))]
    internal static class VictoryPatch
    {
        private static void Postfix()
        {
            if (GameIndicator is null) // Patch may be called twice
            {
                return;
            }

            var meanDelay = GameIndicator.GetMeanDelay();
            var report = GameIndicator.GetReport();

            Object.Destroy(GameIndicator);
            GameIndicator = null;

            if (!Save.Settings.ShowMeanDelay)
            {
                return;
            }

            var victoryIndicator = ResultIndicator!.AddComponent<VictoryIndicator>();

            victoryIndicator.SetMeanDelay(meanDelay);
            victoryIndicator.SetReport(report);
        }
    }

    [HarmonyPatch(typeof(PnlFail), nameof(PnlFail.Fail))]
    internal static class FailPatch
    {
        private static void Postfix()
        {
            DestroyIndicators();
        }
    }
}