using HarmonyLib;
using Il2CppAssets.Scripts.GameCore.HostComponent;
using Il2CppAssets.Scripts.PeroTools.Commons;
using Il2CppFormulaBase;
using Il2CppGameLogic;
using Decimal = Il2CppSystem.Decimal;

namespace AccuracyIndicator.Patch;

[HarmonyPatch(typeof(BattleRoleAttributeComponent), "AttackScore", typeof(int), typeof(int), typeof(TimeNodeOrder))]
public static class AttackScorePatch
{
    private static void Prefix(int idx, TimeNodeOrder? tno)
    {
        if (tno is null || Main.InGameIndicator == null)
        {
            return;
        }

        var musicData = Singleton<StageBattleComponent>.instance.GetMusicDataByIdx(idx);

        if (musicData.isLongPressing || musicData.isMul)
        {
            return;
        }

        var delay = (Decimal)GameGlobal.gTouch.tickTime - tno.md.tick;
        var floatDelay = Decimal.ToSingle(delay);

        Main.InGameIndicator.AddHit(floatDelay);
    }
}