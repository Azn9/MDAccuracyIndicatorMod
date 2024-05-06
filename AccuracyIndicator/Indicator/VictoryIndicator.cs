using UnityEngine.AddressableAssets;

namespace AccuracyIndicator.Indicator;

[RegisterTypeInIl2Cpp]
internal class VictoryIndicator(IntPtr intPtr) : MonoBehaviour(intPtr)
{
    private float _meanDelay;
    private bool _rendered;
    private Il2CppObjectList _report;
    private bool _setMeanDelay;

    private void Update()
    {
        if (_rendered)
        {
            return;
        }

        if (IndicatorCanvas is null || !_setMeanDelay || _report is null)
        {
            return;
        }

        Render();

        _rendered = true;
    }

    private void Render()
    {
        var textObject = new GameObject("Text");
        textObject.transform.SetParent(IndicatorCanvas!.transform);
        var textRt = textObject.AddComponent<RectTransform>();
        textRt.anchoredPosition = new Vector2(ConvertWidthFrom1920P(-650), ConvertHeightFrom1080P(200));
        textRt.sizeDelta = new Vector2(ConvertWidthFrom1920P(600), ConvertHeightFrom1080P(40));

        var text = textObject.AddComponent<Text>();
        text.text = $"Mean delay: {(int)(_meanDelay! * 1000)} ms";

        var font = Addressables.LoadAssetAsync<Font>("Snaps Taste").WaitForCompletion();

        text.font = font;
        text.fontSize = 40;
        text.horizontalOverflow = HorizontalWrapMode.Overflow;
        text.verticalOverflow = VerticalWrapMode.Overflow;
        text.color = Color.white;

        CreateResultBars();

        var max = 0;

        for (float i = -130; i < 130; i += 5)
        {
            var time = i / 1000f;
            var timeRange = (int)(time * 1000) / 5 + 26;
            var hits = _report![timeRange].Cast<ReportRange>();

            if (hits.Amount > max)
            {
                max = hits.Amount;
            }
        }

        for (float i = -130; i < 130; i += 5)
        {
            var time = i / 1000f;
            var timeRange = (int)(time * 1000) / 5 + 26;
            var hits = _report![timeRange].Cast<ReportRange>();

            var bar = new GameObject($"Bar {time}");
            bar.transform.SetParent(IndicatorCanvas.transform);
            var rt = bar.AddComponent<RectTransform>();
            var ri = bar.AddComponent<RawImage>();

            ri.color = time switch
            {
                < 0.025f and > -0.025f => new Color(0, 0.73F, 0.83F),
                < 0.05f and > -0.05f => new Color(0.29F, 0.68F, 0.31F),
                _ => new Color(1f, 0.59F, 0)
            };

            var height = (int)(hits.Amount / (float)max * 300);
            var y = 230 + height / 2;

            rt.sizeDelta = new Vector2(ConvertWidthFrom1920P(10), ConvertHeightFrom1080P(height));
            rt.anchoredPosition = new Vector2(ConvertWidthFrom1920P(-650 + i * 2), ConvertHeightFrom1080P(y));
        }

        var centerIndicator = new GameObject("CenterIndicator2");
        centerIndicator.transform.SetParent(IndicatorCanvas.transform);
        var centerRt = centerIndicator.AddComponent<RectTransform>();
        var centerRi = centerIndicator.AddComponent<RawImage>();
        centerRi.color = new Color(1, 1, 1);
        centerRt.sizeDelta = new Vector2(ConvertWidthFrom1920P(3), ConvertHeightFrom1080P(300));
        centerRt.anchoredPosition = new Vector2(ConvertWidthFrom1920P(-650), ConvertHeightFrom1080P(390));
    }

    public void SetMeanDelay(float meanDelay)
    {
        _meanDelay = meanDelay;
        _setMeanDelay = true;
    }

    public void SetReport(Il2CppObjectList report)
    {
        _report = report;
    }
}