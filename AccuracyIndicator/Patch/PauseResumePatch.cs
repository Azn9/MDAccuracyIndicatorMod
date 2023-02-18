using FormulaBase;
using HarmonyLib;

namespace AccuracyIndicator.Patch;

public abstract class PauseResumePatch
{
    [HarmonyPatch(typeof(StageBattleComponent), "Pause")]
    public static class PausePatch
    {
        private static void Postfix()
        {
            if (Main.InGameIndicator != null)
                Main.InGameIndicator.Pause();
        }
    }

    [HarmonyPatch(typeof(StageBattleComponent), "Resume")]
    public static class ResumePatch
    {
        private static void Postfix()
        {
            if (Main.InGameIndicator != null)
                Main.InGameIndicator.Resume();
        }
    }
}