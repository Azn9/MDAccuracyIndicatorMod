using Il2CppInterop.Runtime.Injection;
using IntPtr = System.IntPtr;
using Object = Il2CppSystem.Object;

namespace AccuracyIndicator.Indicator;

[RegisterTypeInIl2Cpp]
public class ReportRange : Object
{
    public int Amount { get; set; }

    public ReportRange(IntPtr intPtr) : base(intPtr)
    {
    }

    public ReportRange() : base(ClassInjector.DerivedConstructorPointer<ReportRange>())
    {
        ClassInjector.DerivedConstructorBody(this);
    }
}