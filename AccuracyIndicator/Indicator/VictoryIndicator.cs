using System;
using Il2CppSystem.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using Object = Il2CppSystem.Object;

namespace AccuracyIndicator.Indicator;

public class VictoryIndicator : MonoBehaviour
{
    public VictoryIndicator(IntPtr intPtr) : base(intPtr)
    {
    }

    private readonly List<Object> _antiGC = new();
    private GameObject _canvas;
    private Text _text;
    private bool _rendered;
    private float? _meanDelay;
    private List<Object> _report;

    private void Start()
    {
        _antiGC.Add(this);
        
        _canvas = new GameObject("Canvas");
        _canvas.AddComponent<Canvas>();
        _canvas.AddComponent<CanvasScaler>();
        _canvas.AddComponent<GraphicRaycaster>();
        _canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
    }

    private void Update()
    {
        if (_rendered)
            return;

        if (_canvas == null || _meanDelay == null || _report == null)
            return;

        Render();

        _rendered = true;
    }

    private void OnDestroy()
    {
        _antiGC.Remove(this);
        Destroy(_canvas);
    }

    private void Render()
    {
        var textObject = new GameObject("Text");
        textObject.transform.SetParent(_canvas.transform);
        var textRt = textObject.AddComponent<RectTransform>();
        textRt.anchoredPosition = new Vector2(Utils.ConvertWidthFrom1920P(-650), Utils.ConvertHeightFrom1080P(200));
        textRt.sizeDelta = new Vector2(Utils.ConvertWidthFrom1920P(600), Utils.ConvertHeightFrom1080P(40));
        
        _text = textObject.AddComponent<Text>();
        _text.text = $"Mean delay: {(int)(_meanDelay! * 1000)} ms";
        
        var snapstaste = Addressables.LoadAssetAsync<Font>("Snaps Taste");
        var font = snapstaste.WaitForCompletion();
        
        _text.font = font;
        _text.fontSize = 40;
        _text.horizontalOverflow = HorizontalWrapMode.Overflow;
        _text.verticalOverflow = VerticalWrapMode.Overflow;
        _text.color = Color.white;

        Utils.CreateBars(_canvas, Utils.ConvertWidthFrom1920P(-650), Utils.ConvertHeightFrom1080P(230), 600, 171, 106);

        int max = 0;

        for (float i = -140; i < 140; i += 5)
        {
            var time = i / 1000f;
            var timeRange = (int)(time * 1000) / 5 + 28;
            var hits = _report[timeRange].Cast<ReportRange>();

            if (hits.Amount > max)
                max = hits.Amount;
        }

        for (float i = -140; i < 140; i += 5)
        {
            var time = i / 1000f;
            var timeRange = (int)(time * 1000) / 5 + 28;
            var hits = _report[timeRange].Cast<ReportRange>();

            var bar = new GameObject($"Bar {time}");
            bar.transform.SetParent(_canvas.transform);
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

            rt.sizeDelta = new Vector2(Utils.ConvertWidthFrom1920P(10), Utils.ConvertHeightFrom1080P(height));
            rt.anchoredPosition = new Vector2(Utils.ConvertWidthFrom1920P(-650 + i * 2), Utils.ConvertHeightFrom1080P(y));
        }

        var centerIndicator = new GameObject("CenterIndicator2");
        centerIndicator.transform.SetParent(_canvas.transform);
        var centerRt = centerIndicator.AddComponent<RectTransform>();
        var centerRi = centerIndicator.AddComponent<RawImage>();
        centerRi.color = new Color(1, 1, 1);
        centerRt.sizeDelta = new Vector2(Utils.ConvertWidthFrom1920P(3), Utils.ConvertHeightFrom1080P(300));
        centerRt.anchoredPosition = new Vector2(Utils.ConvertWidthFrom1920P(-650), Utils.ConvertHeightFrom1080P(390));
    }
    
    public void SetMeanDelay(float meanDelay)
    {
        _meanDelay = meanDelay;
    }

    public void SetReport(List<Object> report)
    {
        _report = report;
    }
}