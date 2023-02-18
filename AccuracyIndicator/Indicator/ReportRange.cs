using Il2CppSystem;
using UnhollowerRuntimeLib;
using IntPtr = System.IntPtr;

namespace AccuracyIndicator.Indicator;

public class ReportRange : Object
{
    
    public int Amount { get; set; }
    
    public ReportRange(IntPtr intPtr) : base(intPtr) {}

    public ReportRange() : base(ClassInjector.DerivedConstructorPointer<ReportRange>())
    {
        ClassInjector.DerivedConstructorBody(this);
    }

}