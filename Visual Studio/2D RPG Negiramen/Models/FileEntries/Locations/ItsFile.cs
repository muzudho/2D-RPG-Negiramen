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

    #region メソッド（ファイル・ステム取得）
    /// <summary>
    ///     ファイル・ステム取得
    /// </summary>
    /// <returns></returns>
    internal FileStem GetStem() => new FileStem(System.IO.Path.GetFileNameWithoutExtension(this.Path.AsStr));
    #endregion

    #region メソッド（拡張子取得）
    /// <summary>
    ///     拡張子取得
    /// </summary>
    /// <returns></returns>
    internal FileExtension GetExtension() => new FileExtension(System.IO.Path.GetExtension(this.Path.AsStr));
    #endregion

    #region メソッド（親ディレクトリー・パス取得）
    /// <summary>
    ///     親ディレクトリー・パス取得
    ///     
    ///     <list type="bullet">
    ///         <item>ルート要素は親が無いのでヌルを返す</item>
    ///     </list>
    /// </summary>
    /// <returns></returns>
    internal string? GetParentDirectoryAsStr() => System.IO.Path.GetDirectoryName(this.Path.AsStr);
    #endregion
}
