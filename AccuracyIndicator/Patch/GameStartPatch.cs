using AccuracyIndicator.Indicator;
using Il2CppFormulaBase;

namespace AccuracyIndicator.Patch;

[HarmonyPatch(typeof(StageBattleComponent), nameof(StageBattleComponent.GameStart))]
internal static class GameStartPatch
{
    private static void Postfix()
    {
        ResultIndicator ??= new GameObject("Indicator");
        GameIndicator ??= ResultIndicator.AddComponent<InGameIndicator>();
    }
}