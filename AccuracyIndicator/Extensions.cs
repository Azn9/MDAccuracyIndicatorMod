using Object = UnityEngine.Object;

namespace AccuracyIndicator;

public static class Extensions
{
    public static void Destroy(this GameObject gameObject) => Object.Destroy(gameObject);

    public static void Destroy(this Component component) => Object.Destroy(component);
}