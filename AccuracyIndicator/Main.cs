using AccuracyIndicator.Indicator;
using AccuracyIndicator.Patch;
using Tomlet;

namespace AccuracyIndicator;

internal class Main : MelonMod
{
    internal static GameObject? IndicatorObj { get; set; }
    internal static InGameIndicator? InGameIndicator { get; set; }

    public override void OnInitializeMelon()
    {
        Save.Load();
    }

    public override void OnDeinitializeMelon()
    {
        File.WriteAllText(Path.Combine("UserData", "Accuracy Indicator.cfg"), TomletMain.TomlStringFrom(Save.Settings));
    }

    public override void OnSceneWasUnloaded(int buildIndex, string sceneName) =>
        SceneChangePatch.OnSceneWasUnloaded(sceneName);
}