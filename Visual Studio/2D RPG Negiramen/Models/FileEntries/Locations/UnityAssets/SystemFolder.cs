namespace _2D_RPG_Negiramen.Models.FileEntries.Locations.UnityAssets
{
    using _2D_RPG_Negiramen.Models;
    using _2D_RPG_Negiramen.Models.FileEntries.Locations;

    /// <summary>
    ///     😁 Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/System` フォルダの場所
    ///     
    ///     <list type="bullet">
    ///         <item>イミュータブル</item>
    ///         <item><see cref="_2D_RPG_Negiramen.Models.FileEntries.Locations.UnityAssets.ItsFolder"/></item>
    ///     </list>
    /// </summary>
    internal class SystemFolder : Its
    {
        // - その他

        #region その他（生成　関連）
        /// <summary>
        ///     生成
        /// </summary>
        internal SystemFolder(FileEntryPath parentPath)
            : base(pathSource: FileEntryPathSource.FromString(System.IO.Path.Combine(parentPath.AsStr, "System")),
                   convert: (pathSource) => FileEntryPath.From(pathSource,
                                                               replaceSeparators: true))
        {
        }
        #endregion
    }
}
