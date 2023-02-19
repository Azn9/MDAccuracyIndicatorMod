using AccuracyIndicator.Indicator;
using AccuracyIndicator.Patch;
using MelonLoader;
using UnhollowerRuntimeLib;
using UnityEngine;

namespace AccuracyIndicator;

public class Main : MelonMod
{
    public static GameObject IndicatorObj;
    public static InGameIndicator InGameIndicator;

    public override void OnInitializeMelon()
    {
        ClassInjector.RegisterTypeInIl2Cpp<InGameIndicator>();
        ClassInjector.RegisterTypeInIl2Cpp<VictoryIndicator>();
        ClassInjector.RegisterTypeInIl2Cpp<HitEntry>();
        ClassInjector.RegisterTypeInIl2Cpp<ReportRange>();

        HarmonyInstance.PatchAll();
    }

    public override void OnSceneWasUnloaded(int buildIndex, string sceneName) =>
        SceneChangePatch.OnSceneWasUnloaded(sceneName);

}