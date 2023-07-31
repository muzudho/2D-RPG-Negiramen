namespace _2D_RPG_Negiramen.Models.FileEntries;

using _2D_RPG_Negiramen.Models.FileEntries.Locations.AppData;
using System.Text;
using Tomlyn;
using Tomlyn.Model;
using TheFileEntryLocations = _2D_RPG_Negiramen.Models.FileEntries.Locations;

/// <summary>
///     😁 構成
/// </summary>
class Configuration
{
    // - その他

    #region その他（生成　関連）
    /// <summary>
    ///     生成
    /// </summary>
    /// <param name="rememberYourCircleFolderName">（選択中の）あなたのサークル・フォルダ名</param>
    /// <param name="rememberYourWorkFolderName">（選択中の）あなたの作品フォルダ名</param>
    /// <param name="projectIdList">プロジェクトＩｄリスト</param>
    internal Configuration(
        YourCircleFolderName rememberYourCircleFolderName,
        YourWorkFolderName rememberYourWorkFolderName,
        List<ProjectId> projectIdList)
    {
        this.RememberYourCircleFolderName = rememberYourCircleFolderName;
        this.RememberYourWorkFolderName = rememberYourWorkFolderName;
        this.ProjectIdList = projectIdList;
    }
    #endregion

    // - インターナル静的プロパティー

    #region プロパティ（空オブジェクト）
    /// <summary>
    ///     空オブジェクト
    /// </summary>
    internal static Configuration Empty = new(
        rememberYourCircleFolderName: YourCircleFolderName.Empty,
        rememberYourWorkFolderName: YourWorkFolderName.Empty,
        projectIdList: new List<ProjectId>());
    #endregion

    // - インターナル静的メソッド

    #region メソッド（TOML形式ファイルの読取）
    /// <summary>
    ///     <pre>
    ///         TOML形式ファイルの読取
    ///     
    ///         📖　[Tomlyn　＞　Documentation](https://github.com/xoofx/Tomlyn/blob/main/doc/readme.md)
    ///     </pre>
    /// </summary>
    /// <param name="configuration">構成</param>
    /// <returns>TOMLテーブルまたはヌル</returns>
    internal static bool TryLoadTOML(out Configuration? configuration)
    {
        try
        {
            // 設定ファイルの読取
            var configurationText = System.IO.File.ReadAllText(TheFileEntryLocations.AppData.ConfigurationToml.Instance.Path.AsStr);

            var projectIdList = new List<ProjectId>();
            var yourCircleFolderName = new YourCircleFolderName();
            var yourWorkFolderName = new YourWorkFolderName();

            // TOML
            TomlTable document = Toml.ToModel(configurationText);

            if (document != null)
            {
                //
                // [remember]
                // ==========
                //
                if (document.TryGetValue("remember", out object rememberTomlObj))
                {
                    if (rememberTomlObj != null && rememberTomlObj is TomlTable rememberTomlTable)
                    {
                        // あなたのサークル名
                        if (rememberTomlTable.TryGetValue("your_circle_folder_name", out object yourCircleFolderNameObj))
                        {
                            if (yourCircleFolderNameObj != null && yourCircleFolderNameObj is string yourCircleFolderNameAsStr)
                            {
                                yourCircleFolderName = YourCircleFolderName.FromString(yourCircleFolderNameAsStr);
                            }
                        }

                        // あなたの作品名
                        if (rememberTomlTable.TryGetValue("your_work_folder_name", out object yourWorkFolderNameObj))
                        {
                            if (yourWorkFolderNameObj != null && yourWorkFolderNameObj is string yourWorkFolderNameAsStr)
                            {
                                yourWorkFolderName = YourWorkFolderName.FromString(yourWorkFolderNameAsStr);
                            }
                        }
                    }
                }

                //
                // [[project_id]]
                // =========
                //
                if (document.TryGetValue("project_id", out object projectIdElement))
                {
                    if (projectIdElement != null && projectIdElement is Tomlyn.Model.TomlTableArray projectIdTableArray)
                    {
                        foreach (var projectIdTable in projectIdTableArray)
                        {
                            YourCircleFolderName yourCircleFolderName2 = YourCircleFolderName.Empty;
                            YourWorkFolderName yourWorkFolderName2 = YourWorkFolderName.Empty;

                            // あなたのサークル名
                            if (projectIdTable.TryGetValue("your_circle_folder_name", out object yourCircleFolderNameObj))
                            {
                                if (yourCircleFolderNameObj != null && yourCircleFolderNameObj is string yourCircleFolderNameAsStr)
                                {
                                    yourCircleFolderName2 = YourCircleFolderName.FromString(yourCircleFolderNameAsStr);
                                }
                            }

                            // あなたの作品名
                            if (projectIdTable.TryGetValue("your_work_folder_name", out object yourWorkFolderNameObj))
                            {
                                if (yourWorkFolderNameObj != null && yourWorkFolderNameObj is string yourWorkFolderNameAsStr)
                                {
                                    yourWorkFolderName2 = YourWorkFolderName.FromString(yourWorkFolderNameAsStr);
                                }
                            }

                            projectIdList.Add(new ProjectId(yourCircleFolderName2, yourWorkFolderName2));
                        }
                    }
                }
            }

            // ファイルを元に新規作成
            configuration = new Configuration(
                yourCircleFolderName,
                yourWorkFolderName,
                projectIdList);

            return true;
        }
        catch (Exception ex)
        {
            // TODO 例外対応、何したらいい（＾～＾）？
            configuration = null;
            return false;
        }
    }
    #endregion

    #region メソッド（保存）
    /// <summary>
    ///     保存
    /// </summary>
    /// <param name="current">現在の構成</param>
    /// <param name="difference">現在の構成から更新した差分</param>
    /// <param name="newConfiguration">差分を反映した構成</param>
    /// <returns>完了した</returns>
    internal static bool SaveTOML(Configuration current, ConfigurationBuffer difference, out Configuration newConfiguration)
    {
        //
        // マルチプラットフォームの MAUI では、
        // パソコンだけではなく、スマホなどのサンドボックス環境などでの使用も想定されている
        // 
        // そのため、設定の保存／読込の操作は最小限のものしかない
        //
        // 📖　[Where to save .Net MAUI user settings](https://stackoverflow.com/questions/70599331/where-to-save-net-maui-user-settings)
        //
        // // getter
        // var value = Preferences.Get("nameOfSetting", "defaultValueForSetting");
        //
        // // setter
        // Preferences.Set("nameOfSetting", value);
        //
        //
        // しかし、2D RPG は　Windows PC で開発すると想定する。
        // そこで、 MAUI の範疇を外れ、Windows 固有のファイル・システムの API を使用することにする
        //
        // 📖　[File system helpers](https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/storage/file-system-helpers?tabs=windows)
        //

        var configurationBuffer = new ConfigurationBuffer();

        // 差分適用
        configurationBuffer.RememberYourCircleFolderName = difference.RememberYourCircleFolderName?? current.RememberYourCircleFolderName;
        configurationBuffer.RememberYourWorkFolderName = difference.RememberYourWorkFolderName?? current.RememberYourWorkFolderName;
        configurationBuffer.ProjectIdList = difference.ProjectIdList ?? current.ProjectIdList;

        var strBuilder = new StringBuilder();
        strBuilder.AppendLine($@"[remember]

# あなたのサークル・フォルダ名
your_circle_folder_name = ""{configurationBuffer.RememberYourCircleFolderName.AsStr}""

# あなたの作品フォルダ名
your_work_folder_name = ""{configurationBuffer.RememberYourWorkFolderName.AsStr}""
");

        foreach (var projectId in configurationBuffer.ProjectIdList)
        {
            strBuilder.AppendLine($@"[[project_id]]

# あなたのサークル・フォルダ名
your_circle_folder_name = ""{projectId.YourCircleFolderName.AsStr}""

# あなたの作品フォルダ名
your_work_folder_name = ""{projectId.YourWorkFolderName.AsStr}""
");
        }

        // 上書き
        System.IO.File.WriteAllText(
            path: ConfigurationToml.Instance.Path.AsStr,
            contents: strBuilder.ToString());

        // 差分をマージして、イミュータブルに変換
        newConfiguration = new Configuration(
            configurationBuffer.RememberYourCircleFolderName,
            configurationBuffer.RememberYourWorkFolderName,
            configurationBuffer.ProjectIdList);

        return true;
    }
    #endregion

    // - インターナル・プロパティー

    #region プロパティ（選択中のあなたのサークル・フォルダ名）
    /// <summary>
    ///     選択中のあなたのサークル・フォルダ・名
    /// </summary>
    internal YourCircleFolderName RememberYourCircleFolderName { get; }
    #endregion

    #region プロパティ（選択中のあなたの作品フォルダ名）
    /// <summary>
    ///     選択中のあなたの作品フォルダ名
    /// </summary>
    internal YourWorkFolderName RememberYourWorkFolderName { get; }
    #endregion

    #region プロパティ（プロジェクトＩｄリスト）
    /// <summary>
    ///     プロジェクトＩｄリスト
    /// </summary>
    internal List<ProjectId> ProjectIdList { get; } = new List<ProjectId>();
    #endregion

    // - インターナル・メソッド

    #region メソッド（構成ファイルは有効か？）
    /// <summary>
    ///     TODO ★ ヘルパー関数に移動したい
    ///     構成ファイルは有効か？
    /// </summary>
    /// <returns>そうだ</returns>
    internal bool IsReady()
    {
        return App.GetOrLoadProjectConfiguration().StarterKitFolder.IsDirectoryExists() && App.GetOrLoadProjectConfiguration().UnityAssetsFolder.YourCircleFolder.YourWorkFolder.AutoGeneratedFolder.IsExists();
    }
    #endregion
}
