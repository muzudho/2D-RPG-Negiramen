namespace _2D_RPG_Negiramen.Models.FileEntries;

using Tomlyn;
using Tomlyn.Model;
using TheFileEntryLocations = _2D_RPG_Negiramen.Models.FileEntries.Locations;

/// <summary>
///     😁 プロジェクト構成
/// </summary>
class ProjectConfiguration
{
    // - その他

    #region その他（生成　関連）
    /// <summary>
    ///     <pre>
    ///         TOML形式ファイルの読取
    ///     
    ///         📖　[Tomlyn　＞　Documentation](https://github.com/xoofx/Tomlyn/blob/main/doc/readme.md)
    ///     </pre>
    /// </summary>
    /// <param name="projectConfiguration">構成</param>
    /// <returns>TOMLテーブルまたはヌル</returns>
    internal static bool TryLoadTOML(out ProjectConfiguration? projectConfiguration)
    {
        try
        {
            // 設定ファイルのテキスト読取
            var configurationText = System.IO.File.ReadAllText(App.DataFolder.YourCircleFolder.YourWorkFolder.ProjectConfigurationToml.Path.AsStr);

            var starterKitFolder = new TheFileEntryLocations.StarterKit.ItsFolder();
            var unityAssetsFolder = new TheFileEntryLocations.UnityAssets.ItsFolder();

            // TOML
            TomlTable document = Toml.ToModel(configurationText);

            if (document != null)
            {
                //
                // [paths]
                // =======
                //
                if (document.TryGetValue("paths", out object pathsObj))
                {
                    if (pathsObj != null && pathsObj is TomlTable paths)
                    {
                        // ネギラーメンの 📂 `Starter Kit` フォルダ―へのパス
                        if (paths.TryGetValue("starter_kit_folder", out object starterKitFolderPathObj))
                        {
                            if (starterKitFolderPathObj is string starterKitFolderPathAsStr)
                            {
                                starterKitFolder = new TheFileEntryLocations.StarterKit.ItsFolder(
                                    pathSource: FileEntryPathSource.FromString(starterKitFolderPathAsStr),
                                    convert: (pathSource) => FileEntryPath.From(pathSource,
                                                                                replaceSeparators: true));
                            }
                        }

                        // Unity の Assets フォルダ―へのパス
                        if (paths.TryGetValue("unity_assets_folder", out object unityAssetsFolderPathObj))
                        {
                            if (unityAssetsFolderPathObj is string unityAssetsFolderPathAsStr)
                            {
                                unityAssetsFolder = new TheFileEntryLocations.UnityAssets.ItsFolder(
                                    pathSource: FileEntryPathSource.FromString(unityAssetsFolderPathAsStr),
                                    convert: (pathSource) => FileEntryPath.From(pathSource,
                                                                                replaceSeparators: true));
                            }
                        }
                    }
                }
            }

            // ファイルを元に新規作成
            projectConfiguration = new ProjectConfiguration(
                starterKitFolder,
                unityAssetsFolder);

            return true;
        }
        catch (Exception ex)
        {
            // TODO 例外対応、何したらいい（＾～＾）？
            projectConfiguration = null;
            return false;
        }
    }

    /// <summary>
    ///     生成
    /// </summary>
    /// <param name="starterKitFolderLocation">ネギラーメン 📂 `Starter Kit` フォルダの場所</param>
    /// <param name="unityAssetsFolderLocation">Unity の Assets フォルダの場所</param>
    internal ProjectConfiguration(
        TheFileEntryLocations.StarterKit.ItsFolder starterKitFolderLocation,
        TheFileEntryLocations.UnityAssets.ItsFolder unityAssetsFolderLocation)
    {
        this.StarterKitFolderLocation = starterKitFolderLocation;
        this.UnityAssetsFolderLocation = unityAssetsFolderLocation;
    }
    #endregion

    // - インターナル静的プロパティー

    #region プロパティ（空オブジェクト）
    /// <summary>
    ///     空オブジェクト
    /// </summary>
    internal static ProjectConfiguration Empty = new(
        starterKitFolderLocation: TheFileEntryLocations.StarterKit.ItsFolder.Empty,
        unityAssetsFolderLocation: TheFileEntryLocations.UnityAssets.ItsFolder.Empty);
    #endregion

    // - インターナル静的メソッド

    #region メソッド（保存）
    /// <summary>
    ///     保存
    /// </summary>
    /// <param name="current">現在の構成</param>
    /// <param name="difference">現在の構成から更新した差分</param>
    /// <param name="newConfiguration">差分を反映した構成</param>
    /// <returns>完了した</returns>
    internal static bool SaveTOML(ProjectConfiguration current, ProjectConfigurationDifference difference, out ProjectConfiguration newConfiguration)
    {
        var configurationDifference2nd = new ProjectConfigurationDifference();

        // 差分適用
        configurationDifference2nd.StarterKitFolderLocation = difference.StarterKitFolderLocation ?? current.StarterKitFolderLocation;
        configurationDifference2nd.UnityAssetsFolderLocation = difference.UnityAssetsFolderLocation ?? current.UnityAssetsFolderLocation;

        // 差分をマージして、イミュータブルに変換
        newConfiguration = new ProjectConfiguration(
            configurationDifference2nd.StarterKitFolderLocation,
            configurationDifference2nd.UnityAssetsFolderLocation);

        // テキストファイル書出し
        WriteToml(newConfiguration);

        return true;
    }

    internal static void WriteToml(ProjectConfiguration configuration)
    {
        //
        // 注意：　変数展開後のパスではなく、変数展開前のパス文字列を保存すること
        //
        var text = $@"[paths]

# ネギラーメンの 📂 `Starter Kit` フォルダ―へのパス
starter_kit_folder = ""{configuration.StarterKitFolderLocation.Path.AsStr}""

# Unity の 📂 `Assets` フォルダ―へのパス
unity_assets_folder = ""{configuration.UnityAssetsFolderLocation.Path.AsStr}""
";

        // 上書き
        System.IO.File.WriteAllText(
            path: App.DataFolder.YourCircleFolder.YourWorkFolder.ProjectConfigurationToml.Path.AsStr,
            contents: text);
    }
    #endregion

    // - インターナル・プロパティー

    #region プロパティ（ネギラーメンに添付の 📂 `Starter Kit` フォルダの場所）
    /// <summary>
    ///     ネギラーメンに添付の 📂 `Starter Kit` フォルダの場所
    /// </summary>
    /// <example>"C:/Users/むずでょ/Documents/GitHub/2D-RPG-Negiramen/Starter Kit"</example>
    internal TheFileEntryLocations.StarterKit.ItsFolder StarterKitFolderLocation { get; }
    #endregion

    #region プロパティ（Unity の 📂 `Assets` フォルダの場所）
    /// <summary>
    ///     Unity の 📂 `Assets` フォルダの場所
    /// </summary>
    /// <example>"C:/Users/むずでょ/Documents/Unity Projects/Negiramen Practice/Assets"</example>
    internal TheFileEntryLocations.UnityAssets.ItsFolder UnityAssetsFolderLocation { get; }
    #endregion

    // - インターナル・メソッド

    #region メソッド（準備できているか？）
    /// <summary>
    ///     準備できているか？
    /// </summary>
    /// <returns>そうだ</returns>
    internal bool IsReady()
    {
        // スターターキットのディレクトリーが存在する
        bool isStarterKitFolderExists = this.StarterKitFolderLocation.IsExists();

        // Unity の Auto Generated フォルダーが存在する
        bool isUnityAssetsAutoGeneratedFolderExists = this.UnityAssetsFolderLocation.YourCircleFolder.YourWorkFolder.AutoGeneratedFolder.IsExists();

        return isStarterKitFolderExists && isUnityAssetsAutoGeneratedFolderExists;
    }
    #endregion
}
