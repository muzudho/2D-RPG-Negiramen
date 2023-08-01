namespace _2D_RPG_Negiramen.Models.FileEntries.Locations;

/// <summary>
///     😁 ファイル
/// </summary>
internal class ItsFile : Its
{
    // - インターナル・メソッド

    #region メソッド（このファイルは存在するか？）
    /// <summary>
    ///     このファイルは存在するか？
    /// </summary>
    internal bool IsFileExists() => File.Exists(this.Path.AsStr);
    #endregion
}
