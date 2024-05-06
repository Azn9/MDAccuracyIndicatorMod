namespace AccuracyIndicator;

public static class Utils
{
    public static float ConvertWidthFrom1920P(float x) => x / 1920f * Screen.width;
    public static float ConvertHeightFrom1080P(float x) => x / 1080f * Screen.height;

    public static void CreateResultBars()
    {
        var resultIndicatorX = ConvertWidthFrom1920P(-650);
        var resultIndicatorY = ConvertHeightFrom1080P(230);
        CreateBar("Great Result Bar", ResultIndicatorGameObject, resultIndicatorX, resultIndicatorY, new Color(1f, 0.59f, 0),
            ConvertWidthFrom1920P(600), ConvertHeightFrom1080P(10));
    }

    public static void CreateInGameBars()
    {
        var gameIndicatorY = -ConvertHeightFrom1080P(420);
        CreateBar("Great Bar", GameIndicatorGameObject, 0, gameIndicatorY, new Color(1f, 0.59f, 0),
            ConvertWidthFrom1920P(Settings.GreatBarWidth), ConvertHeightFrom1080P(10));

        CreateBar("Early/Late Perfect Bar", GameIndicatorGameObject, 0, gameIndicatorY, new Color(0.29f, 0.68f, 0.31f),
            ConvertWidthFrom1920P(Settings.ELPerfectBarWidth), ConvertHeightFrom1080P(10));

        CreateBar("Perfect Bar", GameIndicatorGameObject, 0, gameIndicatorY, new Color(0, 0.73f, 0.83f),
            ConvertWidthFrom1920P(Settings.PerfectBarWidth), ConvertHeightFrom1080P(10));

        CreateBar("Center Indicator", GameIndicatorGameObject, 0, gameIndicatorY, new Color(1, 1, 1),
            ConvertWidthFrom1920P(3), ConvertHeightFrom1080P(50));
    }


    private static void CreateBar(string name, GameObject parent, float x, float y, Color color, float width, float height)
    {
        var bar = new GameObject(name);
        bar.transform.SetParent(parent.transform);
        var rt = bar.AddComponent<RectTransform>();
        var ri = bar.AddComponent<RawImage>();
        ri.color = color;
        rt.anchoredPosition = new Vector2(x, y);
        rt.sizeDelta = new Vector2(width, height);
    }
}