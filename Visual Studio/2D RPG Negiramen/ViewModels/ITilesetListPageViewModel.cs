namespace _2D_RPG_Negiramen.ViewModels;

using _2D_RPG_Negiramen.Models;

/// <summary>
///     😁 ［タイルセット一覧］ページ・ビューモデル
/// </summary>
public interface ITilesetListPageViewModel
{
    // - パブリック・プロパティ

    #region プロパティ（CollectionView の ItemsLayout プロパティ）
    /// <summary>
    ///     CollectionView の ItemsLayout プロパティ
    /// </summary>
    GridItemsLayout ItemsLayout { get; set; }
    #endregion

    #region 変更通知プロパティ（［タイル切抜き］ボタンの活性性）
    /// <summary>
    ///     ［タイル切抜き］ボタンの活性性
    /// </summary>
    bool IsEnabledTileCropButton { get; set; }
    #endregion

    #region プロパティ（［ファイル・ステムをＵＵＩＤに変更する］ボタンの活性性）
    /// <summary>
    ///     ［ファイル・ステムをＵＵＩＤに変更する］ボタンの活性性
    /// </summary>
    bool IsEnabledRenameFileNameToUUIDButton { get; set; }
    #endregion

    #region 変更通知プロパティ（選択ファイル・ステム）
    /// <summary>
    ///     選択ファイル・ステム
    /// </summary>
    string SelectedFileStem { get; set; }
    #endregion

    #region 変更通知プロパティ（選択ファイル拡張子）
    /// <summary>
    ///     選択ファイル拡張子
    /// </summary>
    string SelectedFileExtension { get; set; }
    #endregion

    // - パブリック・メソッド

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

    #region メソッド（タイルセット・レコード・ビューモデル追加）
    /// <summary>
    ///     タイルセット・レコード・ビューモデル追加
    /// </summary>
    /// <param name="element"></param>
    void EnqueueTilesetRecordVM(TilesetRecordViewModel element);
    #endregion
}
