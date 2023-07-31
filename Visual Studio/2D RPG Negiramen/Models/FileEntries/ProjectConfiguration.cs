namespace _2D_RPG_Negiramen.Models.FileEntries;

using Tomlyn;
using Tomlyn.Model;
using TheLocationOfUnityAssets = _2D_RPG_Negiramen.Models.FileEntries.Locations.UnityAssets;

/// <summary>
///     😁 プロジェクト構成
/// </summary>
class ProjectConfiguration
{
    // - 静的メソッド

    /// <summary>
    ///     <pre>
    ///         TOML形式ファイルの読取
    ///     
    ///         📖　[Tomlyn　＞　Documentation](https://github.com/xoofx/Tomlyn/blob/main/doc/readme.md)
    ///     </pre>
    /// </summary>
    /// <param name="configuration">構成</param>
    /// <returns>TOMLテーブルまたはヌル</returns>
    internal static bool TryLoadTOML(out ProjectConfiguration? configuration)
    {
        try
        {
            var configurationFilePathAsStr = App.ApplicationFolder.YourCircleFolder.YourWorkFolder.ProjectConfigurationToml.Path.AsStr;

            // 設定ファイルの読取
            var configurationText = System.IO.File.ReadAllText(configurationFilePathAsStr);

            TheLocationOfUnityAssets.ItsFolder unityAssetsFolder = new TheLocationOfUnityAssets.ItsFolder();

            // TOML
            TomlTable document = Toml.ToModel(configurationText);

            if (document != null)
            {
                // 準備
            }

            configuration = new ProjectConfiguration();
            return true;
        }
        catch (Exception ex)
        {
            // TODO 例外対応、何したらいい（＾～＾）？
            configuration = null;
            return false;
        }
    }

    /// <summary>
    ///     保存
    /// </summary>
    /// <param name="current">現在の構成</param>
    /// <param name="difference">現在の構成から更新した差分</param>
    /// <param name="newConfiguration">差分を反映した構成</param>
    /// <returns>完了した</returns>
    internal static bool SaveTOML(ProjectConfiguration current, ProjectConfigurationBuffer difference, out ProjectConfiguration newConfiguration)
    {
        var configurationBuffer = new ProjectConfigurationBuffer();

        //
        // 注意：　変数展開後のパスではなく、変数展開前のパス文字列を保存すること
        //
        var text = $@"# 準備
";

        // 上書き
        System.IO.File.WriteAllText(
            // 保存したいファイルへのパス
            path: App.GetOrLoadConfiguration().StarterKitFolder.StarterKitConfigurationFile.Path.AsStr,
            contents: text);

        // イミュータブル・オブジェクトを生成
        newConfiguration = new ProjectConfiguration();
        return true;
    }

    // - その他

    /// <summary>
    ///     生成
    /// </summary>
    internal ProjectConfiguration()
    {
    }
}
