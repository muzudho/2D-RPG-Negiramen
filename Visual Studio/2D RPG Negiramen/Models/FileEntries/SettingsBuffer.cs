namespace _2D_RPG_Negiramen.Models.FileEntries
{
    using TheGeometric = _2D_RPG_Negiramen.Models.Geometric;

    /// <summary>
    ///     😁 設定ファイルの差分
    /// </summary>
    internal class SettingsBuffer
    {
        /// <summary>
        ///     タイルの最大サイズ
        /// </summary>

        internal TheGeometric.SizeInt? TileMaxSize { get; set; }
    }
}
