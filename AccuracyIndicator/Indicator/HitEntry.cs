using Il2CppInterop.Runtime.Injection;
using IntPtr = System.IntPtr;
using Object = Il2CppSystem.Object;

namespace AccuracyIndicator.Indicator;

[RegisterTypeInIl2Cpp]
public class HitEntry : Object
{
    public float Time { get; set; }
    public float Delay { get; }
    public GameObject? HitIndicator { get; }
    public RawImage? RawImage { get; }

    public HitEntry(IntPtr intPtr) : base(intPtr)
    {
    }

    public HitEntry(float time, float delay, GameObject? hitIndicator, RawImage? rawImage)
        : base(ClassInjector.DerivedConstructorPointer<HitEntry>())
    {
        ClassInjector.DerivedConstructorBody(this);

        Time = time;
        Delay = delay;
        HitIndicator = hitIndicator;
        RawImage = rawImage;
    }
}