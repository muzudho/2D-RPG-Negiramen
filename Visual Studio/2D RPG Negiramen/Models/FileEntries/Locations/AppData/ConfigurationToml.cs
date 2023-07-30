namespace _2D_RPG_Negiramen.Models.FileEntries.Locations.AppData
{
    using _2D_RPG_Negiramen;
    using _2D_RPG_Negiramen.Models;

    /// <summary>
    ///     😁 OSの 📂 アプリケーション・ディレクトリー の `configuration.toml` ファイルの場所
    ///     
    ///     <list type="bullet">
    ///         <item>イミュータブル</item>
    ///         <item><see cref="App.Configuration"/></item>
    ///     </list>
    /// </summary>
    /// <example>
    ///     "C:\Users\むずでょ\AppData\Local\Packages\1802ca7b-559d-489e-8a13-f02ac4d27fcc_9zz4h110yvjzm\LocalState\configuration.toml"
    /// </example>
    internal class ConfigurationToml : Its
    {
        // - その他

        #region その他（生成　関連）
        /// <summary>
        ///     生成
        /// </summary>
        ConfigurationToml()
            : base(
                  pathSource: FileEntryPathSource.FromString(System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, "configuration.toml")),
                  convert: (pathSource) => FileEntryPath.From(pathSource,
                                                              replaceSeparators: true))
        {
        }
        #endregion

        // - インターナル静的プロパティ

        #region プロパティ（インスタンス）
        /// <summary>
        ///     インスタンス
        /// </summary>
        internal static ConfigurationToml Instance { get; } = new ConfigurationToml();
        #endregion
    }
}
