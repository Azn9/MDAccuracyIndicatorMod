using AccuracyIndicator.Indicator;
using HarmonyLib;
using Il2CppFormulaBase;

namespace AccuracyIndicator.Patch;

[HarmonyPatch(typeof(StageBattleComponent), "GameStart")]
internal static class GameStartPatch
{
    private static void Postfix()
    {
        Main.IndicatorObj ??= new GameObject("Indicator");
        Main.InGameIndicator ??= Main.IndicatorObj.AddComponent<InGameIndicator>();
    }
}