using Il2Cpp;

namespace AccuracyIndicator.Patch;

[HarmonyPatch(typeof(PnlFail), nameof(PnlFail.Fail))]
internal static class PnlFailPatch
{
    private static void Postfix()
    {
        DestroyIndicators();
    }
}