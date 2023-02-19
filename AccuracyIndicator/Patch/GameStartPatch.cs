using AccuracyIndicator.Indicator;
using FormulaBase;
using HarmonyLib;
using UnityEngine;

namespace AccuracyIndicator.Patch
{
    [HarmonyPatch(typeof(StageBattleComponent), "GameStart")]
    public static class GameStartPatch
    {
        private static void Postfix()
        {
            Main.IndicatorObj = new GameObject("Indicator");
            Main.InGameIndicator = Main.IndicatorObj.AddComponent<InGameIndicator>();
        }
    }
}
