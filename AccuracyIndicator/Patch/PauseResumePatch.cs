using Il2CppFormulaBase;

namespace AccuracyIndicator.Patch;

internal static class PauseResumePatch
{
    [HarmonyPatch(typeof(StageBattleComponent), nameof(StageBattleComponent.Pause))]
    internal static class PausePatch
    {
        private static void Postfix()
        {
            if (GameIndicator != null)
            {
                GameIndicator.Pause();
            }
        }
    }

    [HarmonyPatch(typeof(StageBattleComponent), nameof(StageBattleComponent.Resume))]
    internal static class ResumePatch
    {
        private static void Postfix()
        {
            if (GameIndicator != null)
            {
                GameIndicator.Resume();
            }
        }
    }
}