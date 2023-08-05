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
    ///     
    ///     <list type="bullet">
    ///         <item>１回値をセットすると、上書きしても無視されるという不可解な振る舞いをする</item>
    ///     </list>
    /// </summary>
    GridItemsLayout? ItemsLayout { get; set; }
    #endregion

    #region プロパティ（［タイル切抜き］ボタン　関連）
    /// <summary>
    ///     ［タイル切抜き］ボタンの活性性
    /// </summary>
    bool IsEnabledTileCropButton { get; set; }
    #endregion

    #region プロパティ（［ファイル・ステムをＵＵＩＤに変更する］ボタン　関連）
    /// <summary>
    ///     ［ファイル・ステムをＵＵＩＤに変更する］ボタンの活性性
    /// </summary>
    bool IsEnabledRenameFileNameToUUIDButton { get; set; }
    #endregion

    #region 変更通知プロパティ（［タイルセット削除］ボタン　関連）
    /// <summary>
    ///     ［タイルセット削除］ボタンの活性性
    /// </summary>
    bool IsEnabledTilesetRemoveButton { get; set; }
    #endregion

    #region 変更通知プロパティ（選択ファイル・ステム）
    /// <summary>
    ///     選択ファイル・ステム
    /// </summary>
    string SelectedFileStemAsStr { get; set; }
    #endregion

    #region 変更通知プロパティ（選択ファイル拡張子）
    /// <summary>
    ///     選択ファイル拡張子
    /// </summary>
    string SelectedFileExtensionAsStr { get; set; }
    #endregion

    #region 変更通知プロパティ（選択タイルセット・タイトル）
    /// <summary>
    ///     選択タイルセット・タイトル
    /// </summary>
    string SelectedTilesetTitleAsStr { get; set; }
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

    #region メソッド（タイルセット・レコード・ビューモデル　関連）
    /// <summary>
    ///     タイルセット・レコード・ビューモデル追加
    /// </summary>
    /// <param name="element"></param>
    void AddTilesetRecord(TilesetRecordViewModel element);

    /// <summary>
    ///     タイルセット・レコード・ビューモデル削除
    /// </summary>
    /// <param name="fileStem">ファイル・ステム</param>
    void DeleteTilesetRecordByFileStem(FileStem fileStem);
    #endregion

    #region メソッド（選択タイルセット設定）
    /// <summary>
    ///     選択タイルセット設定
    /// </summary>
    /// <param name="selectedTilesetRecord"></param>
    void SetSelectedTileset(TilesetRecordViewModel? selectedTilesetRecord);
    #endregion

    #region メソッド（選択タイルセットのタイトル設定）
    /// <summary>
    ///     選択タイルセットのタイトル設定
    /// </summary>
    void SetSelectedTilesetTitleAsStr(string title);
    #endregion
}
