using Tomlet.Attributes;

namespace AccuracyIndicator;

public class Data
{
    [TomlPrecedingComment("Whether show Mean Delay in Victory screen")]
    internal bool ShowMeanDelay { get; set; } = true;

    [TomlPrecedingComment("Great bar width")]

    internal int GreatBarWidth { get; set; } = 730;

    [TomlPrecedingComment("Early/Late Perfect bar width")]
    internal int ELPerfectBarWidth { get; set; } = 280;

    [TomlPrecedingComment("Perfect bar width")]
    internal int PerfectBarWidth { get; set; } = 140;
}