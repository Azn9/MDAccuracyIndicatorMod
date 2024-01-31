using AccuracyIndicator.Indicator;
using Il2CppFormulaBase;

namespace AccuracyIndicator.Patch;

[HarmonyPatch(typeof(StageBattleComponent), nameof(StageBattleComponent.GameStart))]
internal static class GameStartPatch
{
    private static void Postfix()
    {
        /*
        Unity rewrites the == operator for GameObjects, so if we want use the ??= operator
        we need to make sure the Main.DestroyIndicators() method uses is not null instead of != null
        if do not, then (GameIndicator is null) will be false so this method won't work
        */
        ResultIndicator ??= new GameObject("Indicator");
        GameIndicator ??= ResultIndicator.AddComponent<InGameIndicator>();
    }
}