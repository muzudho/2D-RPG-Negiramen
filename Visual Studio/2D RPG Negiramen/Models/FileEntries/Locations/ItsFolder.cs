﻿namespace _2D_RPG_Negiramen.Models.FileEntries.Locations;

using _2D_RPG_Negiramen.Coding;

/// <summary>
///     😁 フォルダ
/// </summary>
abstract class ItsFolder : Its
{
    // - その他

    #region その他（生成　関連）
    /// <summary>
    ///     未設定
    /// </summary>
    internal ItsFolder()
        : base()
    {
    }

    /// <summary>
    ///     生成
    /// </summary>
    internal ItsFolder(FileEntryPathSource pathSource, LazyArgs.Convert<FileEntryPathSource, FileEntryPath> evaluate)
        : base(pathSource, evaluate)
    {
    }
    #endregion

    // - インターナル・メソッド

    #region メソッド（このディレクトリーは存在するか？）
    /// <summary>
    ///     このディレクトリーは存在するか？
    /// </summary>
    internal bool IsExists() => Directory.Exists(this.Path.AsStr);
    #endregion

    #region メソッド（このディレクトリーが存在しないなら、作成する）
    /// <summary>
    ///     このディレクトリーが存在しないなら、作成する
    /// </summary>
    internal void CreateThisDirectoryIfItDoesNotExist()
    {
        if (!Directory.Exists(this.Path.AsStr))
        {
            Directory.CreateDirectory(this.Path.AsStr);
        }
    }
    #endregion
}

