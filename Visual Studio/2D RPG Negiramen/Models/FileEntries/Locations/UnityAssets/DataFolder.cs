namespace _2D_RPG_Negiramen.Models.FileEntries.Locations.UnityAssets
{
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
        internal DataFolder(FileEntryPath parentPath)
            : base(pathSource: FileEntryPathSource.FromString(System.IO.Path.Combine(parentPath.AsStr, "Data")),
                   convert: (pathSource) => FileEntryPath.From(pathSource,
                                                               replaceSeparators: true))
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
                    csvFolder = new DataCsvFolder(Path);
                }

                return csvFolder;
            }
        }
        #endregion

        #region プロパティ（Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Data/JSON` フォルダの場所）
        /// <summary>
        ///     Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Data/JSON` フォルダの場所
        /// </summary>
        internal DataJsonFolder JsonFolder
        {
            get
            {
                if (jsonFolder == null)
                {
                    jsonFolder = new DataJsonFolder(Path);
                }

                return jsonFolder;
            }
        }
        #endregion

        // - プライベート・フィールド

        DataCsvFolder? csvFolder;
        DataJsonFolder? jsonFolder;
    }
}
