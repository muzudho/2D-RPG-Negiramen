namespace _2D_RPG_Negiramen.Models.FileEntries.Locations.UnityAssets
{
    using _2D_RPG_Negiramen.Coding;
    using _2D_RPG_Negiramen.Models;
    using _2D_RPG_Negiramen.Models.FileEntries.Locations;

    /// <summary>
    ///     😁 Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Data` フォルダの場所
    ///     
    ///     <list type="bullet">
    ///         <item>イミュータブル</item>
    ///         <item><see cref="_2D_RPG_Negiramen.Models.FileEntries.Locations.UnityAssets.ItsFolder"/></item>
    ///     </list>
    /// </summary>
    class DataFolder : Its
    {
        // - その他

        #region その他（生成　関連）
        /// <summary>
        ///     生成
        /// </summary>
        internal DataFolder()
            : base()
        {
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal DataFolder(FileEntryPathSource pathSource, Lazy.Convert<FileEntryPathSource, FileEntryPath> convert)
            : base(pathSource, convert)
        {
        }
        #endregion

        // - インターナル・プロパティ

        #region プロパティ（Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Data/CSV` フォルダの場所）
        /// <summary>
        ///     Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Data/CSV` フォルダの場所
        /// </summary>
        internal DataCsvFolder CsvFolder
        {
            get
            {
                if (csvFolder == null)
                {
                    csvFolder = new DataCsvFolder(
                        pathSource: FileEntryPathSource.FromString(System.IO.Path.Combine(Path.AsStr, "CSV")),
                        convert: (pathSource) => FileEntryPath.From(pathSource, replaceSeparators: true));
                }

                return csvFolder;
            }
        }
        #endregion

        // - プライベート・フィールド

        DataCsvFolder? csvFolder;
    }
}
