namespace _2D_RPG_Negiramen.Models.FileEntries;

using TheFileEntryLocations = _2D_RPG_Negiramen.Models.FileEntries.Locations;

/// <summary>
///     😁 タイルセット・グローバル構成
///     
///     <list type="bullet">
///         <item>イミュータブル</item>
///     </list>
/// </summary>
internal class TilesetGlobalConfiguration
{
    // - その他

    #region その他（生成　関連）
    /// <summary>
    ///     読込。なければ作成
    /// </summary>
    /// <param name="location">タイルセット・グローバル構成ファイルの場所</param>
    internal TilesetGlobalConfiguration LoadOrAdd(
        TheFileEntryLocations.UnityAssets.ImagesTilesetToml location)
    {
        // ファイルの存在確認
        if (location.IsExists())
        {
            // TODO あれば読込
            return new TilesetGlobalConfiguration(location);
        }
        else
        {
            // なければ新規作成
            var config = new TilesetGlobalConfiguration(
                location: location);

            // ファイル書出し
            WriteTOML(config);

            return config;
        }
    }

    /// <summary>
    ///     生成
    /// </summary>
    /// <param name="location">タイルセット・グローバル構成ファイルの場所</param>
    TilesetGlobalConfiguration(TheFileEntryLocations.UnityAssets.ImagesTilesetToml location)
    {
        this.Location = location;
    }
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
    internal static bool SaveTOML(TilesetGlobalConfiguration current, TilesetGlobalConfigurationBuffer difference, out TilesetGlobalConfiguration newConfiguration)
    {
        var configurationBuffer = new TilesetGlobalConfigurationBuffer();

        // 差分適用
        configurationBuffer.Location = difference.Location ?? current.Location;

        // 差分をマージして、イミュータブルに変換
        newConfiguration = new TilesetGlobalConfiguration(
            location: configurationBuffer.Location);

        WriteTOML(newConfiguration);

        return true;
    }

    internal static void WriteTOML(TilesetGlobalConfiguration configuration)
    {
        //
        // 注意：　変数展開後のパスではなく、変数展開前のパス文字列を保存すること
        //
        var text = $@"# 準備中
";

        // 上書き
        System.IO.File.WriteAllText(
            path: App.DataFolder.YourCircleFolder.YourWorkFolder.ProjectConfigurationToml.Path.AsStr,
            contents: text);
    }
    #endregion

    // - インターナル・プロパティ

    #region プロパティ（タイルセット・グローバル構成ファイルの場所）
    /// <summary>
    ///     タイルセット・グローバル構成ファイルの場所
    /// </summary>
    /// <example>"C:\Users\むずでょ\Documents\Unity Projects\Negiramen Practice\Assets\Doujin Circle Negiramen\Negiramen Quest\Auto Generated\Images\Tilesets\86A25699-E391-4D61-85A5-356BA8049881.toml"</example>
    internal TheFileEntryLocations.UnityAssets.ImagesTilesetToml Location { get; }
    #endregion

    #region プロパティ（拡張子）
    /// <summary>
    ///     拡張子
    /// </summary>
    internal FileExtension ExtensionObj { get; set; } = FileExtension.Empty;
    #endregion

    #region プロパティ（ファイル・ステム）
    /// <summary>
    ///     ファイル・ステム
    ///     
    ///     <list type="bullet">
    ///         <item><see cref="UUIDObj"/>が分かっているときは、ファイル・ステムは使わない</item>
    ///     </list>
    /// </summary>
    internal FileStem FileStemObj { get; set; } = FileStem.Empty;
    #endregion

    #region プロパティ（公開日）
    /// <summary>
    ///     公開日
    /// </summary>
    internal DateTime PublishDate { get; set; } = DateTime.MinValue;
    #endregion

    #region プロパティ（UUID）
    /// <summary>
    ///     UUID
    /// </summary>
    internal UUID UUIDObj { get; set; } = UUID.Empty;
    #endregion
}
