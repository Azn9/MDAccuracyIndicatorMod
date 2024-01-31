using Object = Il2CppSystem.Object;

namespace AccuracyIndicator.Indicator;

[RegisterTypeInIl2Cpp]
public class InGameIndicator(IntPtr intPtr) : MonoBehaviour(intPtr)
{
    private static readonly float DefaultY = -ConvertHeightFrom1080P(420);

    private readonly Il2CppObjectList _antiGC = new();
    private readonly Il2CppObjectList _hitEntries = new();
    private readonly Il2CppObjectList _hitReport = new();
    private RectTransform _arrowRt;
    private GameObject _canvas;
    private bool _inGame = true;
    private float _pausedTime;
    private float _totalDelay;
    private int _totalHits;

    private void Start()
    {
        _antiGC.Add(this);

        for (float i = -130; i < 130; i += 5)
        {
            _hitReport.Add(new ReportRange());
        }

        _canvas = new GameObject("Canvas");
        _canvas.AddComponent<Canvas>();
        _canvas.AddComponent<CanvasScaler>();
        _canvas.AddComponent<GraphicRaycaster>();
        _canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;

        // -0.13 | -0.05 | -0.025 | 0.025 | 0.05 | 0.13
        // Total length: 0.26 = 730px
        // Ok : 0.1 = 280px
        // Perfect : 0.05 = 140px

        CreateBars(_canvas, 0, DefaultY, 730, 280, 140);

        var arrow = new GameObject("Arrow");
        arrow.transform.SetParent(_canvas.transform);
        _arrowRt = arrow.AddComponent<RectTransform>();
        var arrow1 = new GameObject("Arrow1");
        var arrow2 = new GameObject("Arrow2");
        arrow1.transform.SetParent(arrow.transform);
        arrow2.transform.SetParent(arrow.transform);
        var arrow1Rt = arrow1.AddComponent<RectTransform>();
        var arrow2Rt = arrow2.AddComponent<RectTransform>();
        var arrow1Ri = arrow1.AddComponent<RawImage>();
        var arrow2Ri = arrow2.AddComponent<RawImage>();
        arrow1Ri.color = new Color(1, 1, 1);
        arrow2Ri.color = new Color(1, 1, 1);
        _arrowRt.anchoredPosition = new Vector2(0, DefaultY + ConvertHeightFrom1080P(40));
        arrow1Rt.anchoredPosition = new Vector2(ConvertWidthFrom1920P(-7), 0);
        arrow2Rt.anchoredPosition = new Vector2(ConvertWidthFrom1920P(7), 0);
        arrow1Rt.sizeDelta = new Vector2(ConvertWidthFrom1920P(3), ConvertHeightFrom1080P(20));
        arrow2Rt.sizeDelta = new Vector2(ConvertWidthFrom1920P(3), ConvertHeightFrom1080P(20));
        arrow1Rt.rotation = Quaternion.Euler(0, 0, 45);
        arrow2Rt.rotation = Quaternion.Euler(0, 0, -45);
        _arrowRt.sizeDelta = new Vector2(ConvertWidthFrom1920P(2), ConvertHeightFrom1080P(20));
    }

    private void Update()
    {
        if (!_inGame)
        {
            return;
        }

        var dt = Time.deltaTime;
        var time = Time.time;
        var totalDelay = 0f;
        var countDelay = 0;

        List<Object> toRemove = [];

        foreach (var o in _hitEntries)
        {
            var hitEntry = o.Cast<HitEntry>();
            var since = time - hitEntry.Time;

            if (since > 5f)
            {
                toRemove.Add(hitEntry);
                Destroy(hitEntry.HitIndicator);
                continue;
            }

            // 1 for since between 0 and two, linear after
            float alpha;
            if (since < 2f)
            {
                alpha = 1f;
            }
            else
            {
                alpha = 1f - (since - 2f) / 3f;
            }

            var color = hitEntry.RawImage!.color;
            color.a = alpha;
            hitEntry.RawImage.color = color;

            totalDelay += hitEntry.Delay;
            countDelay++;
        }

        foreach (var o in toRemove)
        {
            _hitEntries.Remove(o);
        }

        var meanDelay = countDelay == 0 ? 0 : totalDelay / countDelay;
        var x = ConvertWidthFrom1920P(meanDelay / 0.26f * 730f);

        var position = _arrowRt!.anchoredPosition;
        var newPosition = new Vector2(x, position.y);

        position = Vector2.Lerp(position, newPosition, dt * 3);
        _arrowRt.anchoredPosition = position;
    }

    private void OnDestroy()
    {
        _antiGC.Remove(this);
        Destroy(_canvas);
    }

    public void AddHit(float time)
    {
        if (time is < -0.13f or > 0.13f)
        {
            return;
        }

        _totalDelay += time;
        _totalHits++;

        var x = ConvertWidthFrom1920P(time / 0.26f * 730f);

        var color = time switch
        {
            < 0.025f and > -0.025f => new Color(0, 0.73F, 0.83F),
            < 0.05f and > -0.05f => new Color(0.29F, 0.68F, 0.31F),
            _ => new Color(1f, 0.59F, 0)
        };

        var hitIndicator = new GameObject("Hit");
        hitIndicator.transform.SetParent(_canvas!.transform);
        var hitRt = hitIndicator.AddComponent<RectTransform>();
        var hitRi = hitIndicator.AddComponent<RawImage>();
        hitRi.color = color;
        hitRt.anchoredPosition = new Vector2(x, DefaultY);
        hitRt.sizeDelta = new Vector2(ConvertWidthFrom1920P(5), ConvertHeightFrom1080P(40));

        _hitEntries.Add(new HitEntry(Time.time, time, hitIndicator, hitRi));

        // -130 = 0
        // -125 = 1
        // ...

        // time * 1000 is >= -130 and <= 130
        // (time * 1000) / 5 is >= -26 and <= 26
        // (time * 1000) / 5 + 26 is >= 0 and <= 52

        var timeRange = (int)(time * 1000) / 5 + 26;
        _hitReport[timeRange].Cast<ReportRange>().Amount++;
    }

    public float GetMeanDelay()
    {
        if (_totalHits == 0)
        {
            return 0;
        }

        return _totalDelay / _totalHits;
    }

    public Il2CppObjectList GetReport() => _hitReport;

    public void Pause()
    {
        _inGame = false;
        _pausedTime = Time.time;
    }

    public void Resume()
    {
        var delayTime = Time.time - _pausedTime;

        foreach (var o in _hitEntries)
        {
            var hitEntry = o.Cast<HitEntry>();

            hitEntry.Time += delayTime;
        }

        _inGame = true;
    }
}