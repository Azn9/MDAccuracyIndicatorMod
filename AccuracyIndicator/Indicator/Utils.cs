using UnityEngine.UI;

namespace AccuracyIndicator.Indicator;

public static class Utils
{
    public static float ConvertWidthFrom1920P(float x) => x / 1920f * Screen.width;
    public static float ConvertHeightFrom1080P(float x) => x / 1080f * Screen.height;

    public static void CreateBars(GameObject canvas, float x, float y, int greatBarWidth, int okBarWidth, int perfectBarWidth)
    {
        var greatBar = new GameObject("GreatBar");
        greatBar.transform.SetParent(canvas.transform);
        var greatRt = greatBar.AddComponent<RectTransform>();
        var greatRi = greatBar.AddComponent<RawImage>();
        greatRt.anchoredPosition = new Vector2(x, y);
        greatRi.color = new Color(1f, 0.59F, 0);
        greatRt.sizeDelta = new Vector2(ConvertWidthFrom1920P(greatBarWidth), ConvertHeightFrom1080P(10));

        var almostPerfectBar = new GameObject("AlmostPerfectBar");
        almostPerfectBar.transform.SetParent(canvas.transform);
        var almostPerfectRt = almostPerfectBar.AddComponent<RectTransform>();
        var almostPerfectRi = almostPerfectBar.AddComponent<RawImage>();
        almostPerfectRt.anchoredPosition = new Vector2(x, y);
        almostPerfectRi.color = new Color(0.29F, 0.68F, 0.31F);
        almostPerfectRt.sizeDelta = new Vector2(ConvertWidthFrom1920P(okBarWidth), ConvertHeightFrom1080P(10));

        var perfectBar = new GameObject("PerfectBar");
        perfectBar.transform.SetParent(canvas.transform);
        var perfectRt = perfectBar.AddComponent<RectTransform>();
        var perfectRi = perfectBar.AddComponent<RawImage>();
        perfectRt.anchoredPosition = new Vector2(x, y);
        perfectRi.color = new Color(0, 0.73F, 0.83F);
        perfectRt.sizeDelta = new Vector2(ConvertWidthFrom1920P(perfectBarWidth), ConvertHeightFrom1080P(10));

        var centerIndicator = new GameObject("CenterIndicator");
        centerIndicator.transform.SetParent(canvas.transform);
        var centerRt = centerIndicator.AddComponent<RectTransform>();
        var centerRi = centerIndicator.AddComponent<RawImage>();
        centerRt.anchoredPosition = new Vector2(x, y);
        centerRi.color = new Color(1, 1, 1);
        centerRt.sizeDelta = new Vector2(ConvertWidthFrom1920P(3), ConvertHeightFrom1080P(50));
    }
}