namespace _2D_RPG_Negiramen.Models.FileEntries.Locations.UnityAssets
{
    using _2D_RPG_Negiramen.Coding;
    using _2D_RPG_Negiramen.Models;
    using _2D_RPG_Negiramen.Models.FileEntries.Locations;

    /// <summary>
    ///     😁 Unity の 📄 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Data/CSV/Tileset/{名前}.csv` ファイルの場所
    ///     
    ///     <list type="bullet">
    ///         <item>イミュータブル</item>
    ///         <item><see cref="ItsFolder"/></item>
    ///     </list>
    /// </summary>
    class DataCsvTilesetCsv : Its
    {
        // - その他

        #region その他（生成　関連）
        /// <summary>
        ///     生成
        /// </summary>
        internal DataCsvTilesetCsv()
            : base()
        {
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal DataCsvTilesetCsv(FileEntryPathSource pathSource, Lazy.Convert<FileEntryPathSource, FileEntryPath> convert)
            : base(pathSource, convert)
        {
        }
        #endregion

        // - インターナル静的プロパティ

        #region プロパティ（）
        /// <summary>
        ///     空オブジェクト
        /// </summary>
        internal static DataCsvTilesetCsv Empty { get; } = new DataCsvTilesetCsv();
        #endregion
    }
}
