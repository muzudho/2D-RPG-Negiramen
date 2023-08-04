namespace _2D_RPG_Negiramen.Models.FileEntries.Locations;

using _2D_RPG_Negiramen.Coding;

/// <summary>
///     😁 ファイル
/// </summary>
abstract class ItsFile : Its
{
    // - その他

    #region その他（生成　関連）
    /// <summary>
    ///     未設定
    /// </summary>
    internal ItsFile()
        : base()
    {
    }

    /// <summary>
    ///     生成
    /// </summary>
    internal ItsFile(FileEntryPathSource pathSource, LazyArgs.Convert<FileEntryPathSource, FileEntryPath> convert)
        : base(pathSource, convert)
    {
    }
    #endregion

    // - インターナル・メソッド

    #region メソッド（このファイルは存在するか？）
    /// <summary>
    ///     このファイルは存在するか？
    /// </summary>
    internal bool IsExists() => File.Exists(this.Path.AsStr);
    #endregion
}
