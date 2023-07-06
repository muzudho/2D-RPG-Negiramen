namespace _2D_RPG_Negiramen.Models.FileEntries.Locations.Negiramen
{
    /// <summary>
    ///     😁 ネギラーメン・ワークスペース・フォルダーのパス
    /// </summary>
    class WorkspaceFolder : _2D_RPG_Negiramen.Models.FileEntries.Locations.Its

    {
        // - 静的プロパティ

        /// <summary>
        ///     空オブジェクト
        /// </summary>
        internal static WorkspaceFolder Empty { get; } = new WorkspaceFolder();

        /// <summary>
        ///     生成
        /// </summary>
        internal WorkspaceFolder()
            : base()
        {
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal WorkspaceFolder(FileEntryPath path, FileEntryPathSource pathSource)
            : base(path, pathSource)
        {
        }
    }
}
