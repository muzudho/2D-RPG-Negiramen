namespace _2D_RPG_Negiramen.Models.FileEntries.Locations.AppData
{
    using _2D_RPG_Negiramen;
    using _2D_RPG_Negiramen.Coding;
    using _2D_RPG_Negiramen.Models;

    /// <summary>
    ///     😁 OSの 📂 キャッシュ・ディレクトリー の `project_configuration.toml` ファイルの場所
    ///     
    ///     <list type="bullet">
    ///         <item>イミュータブル</item>
    ///         <item><see cref="App.Configuration"/></item>
    ///     </list>
    /// </summary>
    /// <example>
    ///     "C:\Users\むずでょ\AppData\Local\Packages\1802ca7b-559d-489e-8a13-f02ac4d27fcc_9zz4h110yvjzm\LocalState\Doujin Circle Negiramen\Negiramen Quest\project_configuration.toml"
    /// </example>
    internal class ProjectConfigurationToml : Its
    {
        // - その他

        #region その他（生成　関連）
        /// <summary>
        ///     生成
        /// </summary>
        internal ProjectConfigurationToml()
            : base()
        {
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal ProjectConfigurationToml(FileEntryPath parentPath)
            : base(pathSource: FileEntryPathSource.FromString(System.IO.Path.Combine(parentPath.AsStr, $"project_configuration.toml")),
                   convert: (pathSource) => FileEntryPath.From(pathSource,
                                                               replaceSeparators: true))
        {
        }
        #endregion

        // - インターナル静的プロパティ

        #region プロパティ（空オブジェクト）
        /// <summary>
        ///     空オブジェクト
        /// </summary>
        internal static ProjectConfigurationToml Empty { get; } = new ProjectConfigurationToml();
        #endregion
    }
}
