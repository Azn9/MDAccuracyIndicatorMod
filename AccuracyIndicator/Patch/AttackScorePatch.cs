using Assets.Scripts.GameCore.HostComponent;
using Assets.Scripts.PeroTools.Commons;
using FormulaBase;
using GameLogic;
using HarmonyLib;
using Il2CppSystem;

namespace AccuracyIndicator.Patch;

[HarmonyPatch(typeof(BattleRoleAttributeComponent), "AttackScore", typeof(int), typeof(int), typeof(TimeNodeOrder))]
public static class AttackScorePatch
{
    private static void Prefix(int idx, TimeNodeOrder tno)
    {
        if (tno == null || Main.InGameIndicator == null)
            return;

        var musicData = Singleton<StageBattleComponent>.instance.GetMusicDataByIdx(idx);

        if (musicData.isLongPressing || musicData.isMul)
            return;

        var delay = (Decimal)GameGlobal.gTouch.tickTime - tno.md.tick;
        var floatDelay = Decimal.ToSingle(delay);

        Main.InGameIndicator.AddHit(floatDelay);
    }
}