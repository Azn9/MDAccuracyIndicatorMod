using UnityEngine;
using UnityEngine.UI;

namespace AccuracyIndicator.Indicator;

public static class Utils
{
    
    public static float ConvertWidthFrom1920P(float x) => x / 1920f * Screen.width;
    public static float ConvertHeightFrom1080P(float x) => x / 1080f * Screen.height;

    public static readonly Color ColorPerfect = new Color(0.2f, 0.74f, 0.91f);
    public static readonly Color ColorAlmostPerfect = new Color(0.34f, 0.89f, 0.07f);
    public static readonly Color ColorGreat = new Color(1, 0.59f, 0);

    public static GameObject CreateBars(GameObject canvas, float x, float y, int greatBarWidth, int okBarWidth, int perfectBarWidth)
    {
        var greatBar = new GameObject("GreatBar");
        greatBar.transform.SetParent(canvas.transform);
        var greatRt = greatBar.AddComponent<RectTransform>();
        var greatRi = greatBar.AddComponent<RawImage>();
        greatRt.anchoredPosition = new Vector2(x, y);
        greatRi.color = ColorGreat;
        greatRt.sizeDelta = new Vector2(ConvertWidthFrom1920P(greatBarWidth), ConvertHeightFrom1080P(10));

        var almostPerfectBar = new GameObject("AlmostPerfectBar");
        almostPerfectBar.transform.SetParent(canvas.transform);
        var almostPerfectRt = almostPerfectBar.AddComponent<RectTransform>();
        var almostPerfectRi = almostPerfectBar.AddComponent<RawImage>();
        almostPerfectRt.anchoredPosition = new Vector2(x, y);
        almostPerfectRi.color = ColorAlmostPerfect;
        almostPerfectRt.sizeDelta = new Vector2(ConvertWidthFrom1920P(okBarWidth), ConvertHeightFrom1080P(10));

        var perfectBar = new GameObject("PerfectBar");
        perfectBar.transform.SetParent(canvas.transform);
        var perfectRt = perfectBar.AddComponent<RectTransform>();
        var perfectRi = perfectBar.AddComponent<RawImage>();
        perfectRt.anchoredPosition = new Vector2(x, y);
        perfectRi.color = ColorPerfect;
        perfectRt.sizeDelta = new Vector2(ConvertWidthFrom1920P(perfectBarWidth), ConvertHeightFrom1080P(10));

        var centerIndicator = new GameObject("CenterIndicator");
        centerIndicator.transform.SetParent(canvas.transform);
        var centerRt = centerIndicator.AddComponent<RectTransform>();
        var centerRi = centerIndicator.AddComponent<RawImage>();
        centerRt.anchoredPosition = new Vector2(x, y);
        centerRi.color = new Color(1, 1, 1);
        centerRt.sizeDelta = new Vector2(ConvertWidthFrom1920P(3), ConvertHeightFrom1080P(50));
        return centerIndicator;
    }
    
}