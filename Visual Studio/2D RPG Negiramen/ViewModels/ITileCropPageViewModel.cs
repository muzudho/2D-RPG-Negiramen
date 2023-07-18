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
    Models.Geometric.PointFloat WorkingGridPhase { get; }

    /// <summary>
    ///     グリッド単位。元画像ベース
    /// </summary>
    Models.Geometric.SizeInt SourceGridUnit { get; }

    /// <summary>
    ///     グリッド単位。ズーム後
    /// </summary>
    Models.Geometric.SizeFloat WorkingGridUnit { get; }
    #endregion

    #region プロパティ（切抜きカーソル。共通　関連）
    /// <summary>
    ///     切抜きカーソルの線の半分の太さ
    /// </summary>
    ThicknessOfLine HalfThicknessOfTileCursorLine { get; }
    #endregion

    #region プロパティ（切抜きカーソル。元画像ベース　関連）
    /// <summary>
    ///     切抜きカーソルのサイズ。元画像ベース
    /// </summary>
    Models.Geometric.SizeInt SourceCroppedCursorSize { get; }
    #endregion

    #region プロパティ（切抜きカーソル。ズーム済み　関連）
    /// <summary>
    ///     矩形カーソル。ズーム済みの位置（マージンとして）
    /// </summary>
    public Thickness WorkingCroppedCursorPointAsMargin { get; }

    /// <summary>
    ///     矩形カーソル。ズーム済みの位置ｘ（マージンとして）
    /// </summary>
    public float WorkingCroppedCursorLeftAsFloat { get; }

    /// <summary>
    ///     矩形カーソル。ズーム済みの位置ｙ（マージンとして）
    /// </summary>
    public float WorkingCroppedCursorTopAsFloat { get; }

    /// <summary>
    ///     切抜きカーソル。ズーム済みのサイズ
    ///     
    ///     <list type="bullet">
    ///         <item>線の太さを含まない</item>
    ///     </list>
    /// </summary>
    Models.Geometric.SizeFloat WorkingCroppedCursorSize { get; }

    /// <summary>
    ///     矩形カーソル。ズーム済みの横幅。線の太さを含まない
    /// </summary>
    public float WorkingCroppedCursorWidthAsFloat { get; }

    /// <summary>
    ///     矩形カーソル。ズーム済みの縦幅。線の太さを含まない
    /// </summary>
    public float WorkingCroppedCursorHeightAsFloat { get; }

    /// <summary>
    ///     矩形カーソル。ズーム済みの横幅。線の太さを含む
    /// </summary>
    public float WorkingCroppedCursorCanvasWidthAsFloat { get; }

    /// <summary>
    ///     矩形カーソル。ズーム済みの縦幅。線の太さを含む
    /// </summary>
    public float WorkingCroppedCursorCanvasHeightAsFloat { get; }
    #endregion

    #region プロパティ（ポインティング・デバイス押下中か？）
    /// <summary>
    ///     ポインティング・デバイス押下中か？
    /// 
    ///     <list type="bullet">
    ///         <item>タイルを選択開始していて、まだ未確定だ</item>
    ///         <item>マウスじゃないと思うけど</item>
    ///     </list>
    /// </summary>
    bool IsMouseDragging { get; }
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
