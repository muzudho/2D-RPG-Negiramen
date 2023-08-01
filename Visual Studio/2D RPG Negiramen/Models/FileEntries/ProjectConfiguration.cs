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
    ///     生成
    /// </summary>
    /// <param name="negiramenStarterKitFolderPath">ネギラーメン 📂 `Starter Kit` フォルダへのパス</param>
    /// <param name="unityAssetsFolderPath">Unity の Assets フォルダへのパス</param>
    internal ProjectConfiguration(
        TheFileEntryLocations.StarterKit.ItsFolder negiramenStarterKitFolderPath,
        TheFileEntryLocations.UnityAssets.ItsFolder unityAssetsFolderPath)
    {
        this.StarterKitFolder = negiramenStarterKitFolderPath;
        this.UnityAssetsFolder = unityAssetsFolderPath;
    }
    #endregion

    // - インターナル静的プロパティー

    #region プロパティ（空オブジェクト）
    /// <summary>
    ///     空オブジェクト
    /// </summary>
    internal static ProjectConfiguration Empty = new(
        negiramenStarterKitFolderPath: TheFileEntryLocations.StarterKit.ItsFolder.Empty,
        unityAssetsFolderPath: TheFileEntryLocations.UnityAssets.ItsFolder.Empty);
    #endregion

    // - インターナル静的メソッド

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
            // 設定ファイルの読取
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
    ///     保存
    /// </summary>
    /// <param name="current">現在の構成</param>
    /// <param name="difference">現在の構成から更新した差分</param>
    /// <param name="newConfiguration">差分を反映した構成</param>
    /// <returns>完了した</returns>
    internal static bool SaveTOML(ProjectConfiguration current, ProjectConfigurationBuffer difference, out ProjectConfiguration newConfiguration)
    {
        var configurationBuffer = new ProjectConfigurationBuffer();

        // 差分適用
        configurationBuffer.StarterKitFolder = difference.StarterKitFolder ?? current.StarterKitFolder;
        configurationBuffer.UnityAssetsFolder = difference.UnityAssetsFolder ?? current.UnityAssetsFolder;

        //
        // 注意：　変数展開後のパスではなく、変数展開前のパス文字列を保存すること
        //
        var text = $@"[paths]

# ネギラーメンの 📂 `Starter Kit` フォルダ―へのパス
starter_kit_folder = ""{configurationBuffer.StarterKitFolder.Path.AsStr}""

# Unity の 📂 `Assets` フォルダ―へのパス
unity_assets_folder = ""{configurationBuffer.UnityAssetsFolder.Path.AsStr}""
";

        // 上書き
        System.IO.File.WriteAllText(
            path: App.DataFolder.YourCircleFolder.YourWorkFolder.ProjectConfigurationToml.Path.AsStr,
            contents: text);

        // 差分をマージして、イミュータブルに変換
        newConfiguration = new ProjectConfiguration(
            configurationBuffer.StarterKitFolder,
            configurationBuffer.UnityAssetsFolder);

        return true;
    }

    // - インターナル・プロパティー

    #region プロパティ（ネギラーメンに添付の 📂 `Starter Kit` フォルダの場所）
    /// <summary>
    ///     TODO ★ 廃止予定
    ///     ネギラーメンに添付の 📂 `Starter Kit` フォルダの場所
    /// </summary>
    /// <example>"C:/Users/むずでょ/Documents/GitHub/2D-RPG-Negiramen/Starter Kit"</example>
    internal TheFileEntryLocations.StarterKit.ItsFolder StarterKitFolder { get; }
    #endregion

    #region プロパティ（Unity の 📂 `Assets` フォルダの場所）
    /// <summary>
    ///     TODO ★ 廃止予定
    ///     Unity の 📂 `Assets` フォルダの場所
    /// </summary>
    /// <example>"C:/Users/むずでょ/Documents/Unity Projects/Negiramen Practice/Assets"</example>
    internal TheFileEntryLocations.UnityAssets.ItsFolder UnityAssetsFolder { get; }
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
        bool isStarterKitFolderExists = this.StarterKitFolder.IsExists();

        // Unity の Auto Generated フォルダーが存在する
        bool isUnityAssetsAutoGeneratedFolderExists = this.UnityAssetsFolder.YourCircleFolder.YourWorkFolder.AutoGeneratedFolder.IsExists();

        return isStarterKitFolderExists && isUnityAssetsAutoGeneratedFolderExists;
    }
    #endregion
}
