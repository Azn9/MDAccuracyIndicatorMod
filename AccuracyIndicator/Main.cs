using AccuracyIndicator.Indicator;

namespace AccuracyIndicator;

internal class Main : MelonMod
{
    internal static GameObject IndicatorCanvas { get; set; }
    internal static GameObject GameIndicatorGameObject { get; set; }
    internal static GameObject ResultIndicatorGameObject { get; set; }
    internal static InGameIndicator GameIndicator { get; set; }


    public override void OnInitializeMelon() => LoadData();

    public override void OnDeinitializeMelon() => SaveData();


    public override void OnSceneWasUnloaded(int buildIndex, string sceneName)
    {
        if (sceneName == "GameMain")
        {
            DestroyIndicators();
        }
    }

    internal static void DestroyIndicators()
    {
        if (GameIndicator != null)
        {
            GameIndicator.Destroy();
        }

        if (IndicatorCanvas != null)
        {
            IndicatorCanvas.Destroy();
        }
    }
}