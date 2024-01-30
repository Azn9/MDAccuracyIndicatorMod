using Tomlet;
using Tomlet.Attributes;

namespace AccuracyIndicator;

internal static class Save
{
    internal static Data Settings { get; private set; } = new();

    public static void Load()
    {
        if (!File.Exists(Path.Combine("UserData", "AccuracyIndicator.cfg")))
        {
            var defaultConfig = TomletMain.TomlStringFrom(new Data(true));
            File.WriteAllText(Path.Combine("UserData", "AccuracyIndicator.cfg"), defaultConfig);
        }

        var data = File.ReadAllText(Path.Combine("UserData", "AccuracyIndicator.cfg"));
        Settings = TomletMain.To<Data>(data);
    }
}

public class Data
{
    [TomlPrecedingComment("Whether show Mean Delay in Victory screen")]
    internal readonly bool ShowMeanDelay;

    public Data()
    {
    }

    internal Data(bool showMeanDelay) => ShowMeanDelay = showMeanDelay;
}