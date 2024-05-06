using Tomlet;

namespace AccuracyIndicator;

internal static class Save
{
    private static readonly string ConfigPath = Path.Combine("UserData", "AccuracyIndicator.cfg");
    internal static Data Settings { get; private set; } = new();

    public static void LoadData()
    {
        if (!File.Exists(ConfigPath))
        {
            var defaultConfig = TomletMain.TomlStringFrom(new Data());
            File.WriteAllText(ConfigPath, defaultConfig);
        }

        var data = File.ReadAllText(ConfigPath);
        Settings = TomletMain.To<Data>(data);
    }

    public static void SaveData() => File.WriteAllText(ConfigPath, TomletMain.TomlStringFrom(Settings));
}