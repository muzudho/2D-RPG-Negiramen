namespace _2D_RPG_Negiramen.ViewModels;

using _2D_RPG_Negiramen.Models;
using _2D_RPG_Negiramen.Models.FileEntries;
using SkiaSharp;

/// <summary>
///     タイル切抜きページ・ビューモデル
/// </summary>
public interface ITileCropPageViewModel
{
    #region プロパティ（タイルセット設定　関連）
    /// <summary>
    ///     タイルセット設定
    /// </summary>
    TilesetSettings TilesetSettings { get; }
    #endregion

    #region プロパティ（タイルセット元画像　関連）
    /// <summary>
    ///     タイルセットの元画像。ビットマップ形式
    /// </summary>
    SKBitmap TilesetSourceBitmap { get; }
    #endregion

    #region プロパティ（タイルセット作業画像　関連）
    /// <summary>
    ///     タイルセットの作業画像。ビットマップ形式
    /// </summary>
    SKBitmap TilesetWorkingBitmap { get; }
    #endregion

    #region プロパティ（グリッド　関連）
    /// <summary>
    ///     グリッドの線の太さの半分
    /// </summary>
    int HalfThicknessOfGridLineAsInt { get; }

    /// <summary>
    ///     グリッド位相の左上表示位置
    /// </summary>
    Models.Geometric.PointInt SourceGridPhase { get; }

    /// <summary>
    ///     グリッド位相の左上表示位置
    /// </summary>
    Models.Geometric.PointInt WorkingGridPhase { get; }

    /// <summary>
    ///     グリッド・タイルのサイズ。元画像ベース
    /// </summary>
    Models.Geometric.SizeInt SourceGridTileSize { get; }

    /// <summary>
    ///     グリッド・タイルのサイズ。ズーム後
    /// </summary>
    Models.Geometric.SizeDouble WorkingGridTileSize { get; }
    #endregion

    #region プロパティ（選択タイル　関連）
    /// <summary>
    ///     タイル・カーソルの線の半分の太さ
    /// </summary>
    ThicknessOfLine HalfThicknessOfTileCursorLine { get; }

    /// <summary>
    ///     選択タイルのサイズ
    /// </summary>
    Models.Geometric.SizeInt SelectedTileSize { get; }
    #endregion

    #region プロパティ（ポインティング・デバイス押下中か？）
    /// <summary>
    ///     ポインティング・デバイス押下中か？
    /// 
    ///     <list type="bullet">
    ///         <item>タイルを選択開始していて、まだ未確定だ</item>
    ///     </list>
    /// </summary>
    bool SelectingOnPointingDevice { get; }
    #endregion

    // - メソッド

    #region メソッド（画面遷移でこの画面に戻ってきた時）
    /// <summary>
    ///     画面遷移でこの画面に戻ってきた時
    /// </summary>
    void ReactOnVisited();
    #endregion

    #region メソッド（ロケール変更による再描画）
    /// <summary>
    ///     ロケール変更による再描画
    ///     
    ///     <list type="bullet">
    ///         <item>動的にテキストを変えている部分に対応するため</item>
    ///     </list>
    /// </summary>
    void RefreshByLocaleChanged();
    #endregion
}
