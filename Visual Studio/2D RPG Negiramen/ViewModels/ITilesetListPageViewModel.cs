﻿namespace _2D_RPG_Negiramen.ViewModels;

/// <summary>
///     😁 ［タイルセット一覧］ページ・ビューモデル
/// </summary>
public interface ITilesetListPageViewModel
{
    // - プロパティ

    #region プロパティ（CollectionView の ItemsLayout プロパティ）
    /// <summary>
    ///     CollectionView の ItemsLayout プロパティ
    /// </summary>
    GridItemsLayout ItemsLayout { get; set; }
    #endregion

    #region プロパティ（［ファイル名をＵＵＩＤに変更する］ボタンの活性性）
    /// <summary>
    ///     ［ファイル名をＵＵＩＤに変更する］ボタンの活性性
    /// </summary>
    bool IsEnabledRenameFileNameToUUIDButton { get; set; }
    #endregion

    #region 変更通知プロパティ（選択ファイル・ステム）
    /// <summary>
    ///     選択ファイル・ステム
    /// </summary>
    public string SelectedFileStem { get; set; }
    #endregion

    // - メソッド

    #region メソッド（ロケール変更による再描画）
    /// <summary>
    ///     ロケール変更による再描画
    ///     
    ///     <list type="bullet">
    ///         <item>動的にテキストを変えている部分に対応するため</item>
    ///     </list>
    /// </summary>
    void InvalidateLocale();
    #endregion
}
