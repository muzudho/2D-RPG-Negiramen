namespace _2D_RPG_Negiramen.Models.FileEntries.Deployments;

using TheFileEntryLocations = _2D_RPG_Negiramen.Models.FileEntries.Locations;

/// <summary>
///     😁 アプリケーション・フォルダ内のプロジェクト別のフォルダを想定したもの
/// </summary>
internal class ApplicationProjectDeployment
{
    // - インターナル静的メソッド

    #region メソッド（アプリケーション・フォルダにファイルを送り込みます）
    /// <summary>
    ///     <pre>
    ///         アプリケーション・フォルダにファイルを送り込みます
    ///     
    ///             📂 例: `C:\Users\むずでょ\AppData\Local\Packages\1802ca7b-559d-489e-8a13-f02ac4d27fcc_9zz4h110yvjzm\LocalState`
    ///         👉　└─ 📂 Assets
    ///     </pre>
    /// </summary>
    /// <returns>完了した</returns>
    internal static bool MakeFolder()
    {
        if (!App.DataFolder.IsDirectoryExists())
        {
            // 存在しなければ失敗
            return false;
        }

        // 無ければ作成
        App.DataFolder.YourCircleFolder.CreateThisDirectoryIfItDoesNotExist();
        MakeYourCircleFolder(App.DataFolder.YourCircleFolder);

        return true;
    }
    #endregion

    // - プライベート静的メソッド

    #region メソッド（アプリケーション・フォルダ の 📂 `｛あなたのサークル・フォルダ名｝` フォルダにファイルを送り込みます）
    /// <summary>
    ///     <pre>
    ///          アプリケーション・フォルダ の 📂 `｛あなたのサークル・フォルダ名｝` フォルダにファイルを送り込みます
    ///     
    ///             📂 アプリケーション・フォルダ
    ///         👉　└─ 📂 {あなたのサークル・フォルダ名}
    ///     </pre>
    /// </summary>
    /// <param name="yourCircleFolder">あなたのサークル名フォルダ―へのパス</param>
    static void MakeYourCircleFolder(TheFileEntryLocations.AppData.YourCircleFolder yourCircleFolder)
    {
        // 無ければ作成
        yourCircleFolder.YourWorkFolder.CreateThisDirectoryIfItDoesNotExist();

        // プロジェクト構成ファイルの作成
        MakeProjectConfigurationToml(yourCircleFolder.YourWorkFolder);
    }
    #endregion

    /// <summary>
    ///     TODO ★ プロジェクト構成ファイルの作成
    /// </summary>
    /// <param name="yourWorkFolder"></param>
    static void MakeProjectConfigurationToml(TheFileEntryLocations.AppData.YourWorkFolder yourWorkFolder)
    {
        // App.DataFolder.YourCircleFolder.YourWorkFolder

    }
}
