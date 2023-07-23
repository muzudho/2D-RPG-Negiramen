namespace _2D_RPG_Negiramen.Models.FileEntries.Locations.UnityAssets
{
    using _2D_RPG_Negiramen.Coding;
    using _2D_RPG_Negiramen.Models;
    using _2D_RPG_Negiramen.Models.FileEntries.Locations;

    /// <summary>
    ///     😁 Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Data/CSV` フォルダーの場所
    ///     
    ///     <list type="bullet">
    ///         <item>イミュータブル</item>
    ///         <item><see cref="ItsFolder"/></item>
    ///     </list>
    /// </summary>
    class DataCsvFolder : Its
    {
        // - その他

        #region その他（生成　関連）
        /// <summary>
        ///     生成
        /// </summary>
        internal DataCsvFolder()
            : base()
        {
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal DataCsvFolder(FileEntryPathSource pathSource, Lazy.Convert<FileEntryPathSource, FileEntryPath> convert)
            : base(pathSource, convert)
        {
        }
        #endregion

        // - インターナル・プロパティ

        #region プロパティ（Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Data/CSV/Tileset` フォルダーの場所）
        /// <summary>
        ///     Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Data/CSV/Tileset` フォルダーの場所
        /// </summary>
        internal DataCsvTilesetFolder TilesetFolder
        {
            get
            {
                if (tilesetFolder == null)
                {
                    tilesetFolder = new DataCsvTilesetFolder(
                        pathSource: FileEntryPathSource.FromString(System.IO.Path.Combine(Path.AsStr, "Tileset")),
                        convert: (pathSource) => FileEntryPath.From(pathSource, replaceSeparators: true));
                }

                return tilesetFolder;
            }
        }
        #endregion

        // - プライベート・フィールド

        DataCsvTilesetFolder? tilesetFolder;
    }
}
