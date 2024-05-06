using AccuracyIndicator.Indicator;
using Il2CppFormulaBase;

namespace AccuracyIndicator.Patch;

[HarmonyPatch(typeof(StageBattleComponent), nameof(StageBattleComponent.GameStart))]
internal static class GameStartPatch
{
    private static void Postfix()
    {
        IndicatorCanvas = new GameObject("Accuracy Indicator Canvas");
        IndicatorCanvas.AddComponent<Canvas>();
        IndicatorCanvas.AddComponent<CanvasScaler>();
        IndicatorCanvas.AddComponent<GraphicRaycaster>();
        IndicatorCanvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;

        GameIndicatorGameObject = new GameObject("Game Indicator");
        GameIndicatorGameObject.transform.SetParent(IndicatorCanvas.transform);
        GameIndicator ??= GameIndicatorGameObject.AddComponent<InGameIndicator>();
    }
}