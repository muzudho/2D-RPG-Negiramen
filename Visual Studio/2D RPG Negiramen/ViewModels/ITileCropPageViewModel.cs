﻿namespace _2D_RPG_Negiramen.ViewModels;

using _2D_RPG_Negiramen.Models;
using SkiaSharp;

/// <summary>
///     😁 ［タイル切抜き］ページ・ビューモデル
/// </summary>
public interface ITileCropPageViewModel
{
    // - パブリック・プロパティ

    #region プロパティ（タイルセット設定ビューモデル　関連）
    /// <summary>
    ///     タイルセット設定ビューモデル
    /// </summary>
    TilesetDatatable TilesetSettingsVM { get; }
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
    Models.Geometric.PointInt GridPhaseSourceLocation { get; }

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
    ThicknessOfLine TileCursor_HalfThicknessOfLine { get; }
    #endregion

    #region プロパティ（切抜きカーソル。ズーム済み　関連）
    /// <summary>
    ///     矩形カーソル。ズーム済みの位置（マージンとして）
    /// </summary>
    public Thickness TileCursor_WorkingPointAsMargin { get; }

    /// <summary>
    ///     矩形カーソル。ズーム済みの位置ｘ（マージンとして）
    /// </summary>
    public float SelectedTile_WorkingLeftAsFloat { get; }

    /// <summary>
    ///     矩形カーソル。ズーム済みの位置ｙ（マージンとして）
    /// </summary>
    public float SelectedTile_WorkingTopAsFloat { get; }

    /// <summary>
    ///     切抜きカーソル。ズーム済みのサイズ
    ///     
    ///     <list type="bullet">
    ///         <item>線の太さを含まない</item>
    ///     </list>
    /// </summary>
    Models.Geometric.SizeFloat SelectedTile_WorkingSizeWithTrick { get; }

    /// <summary>
    ///     矩形カーソル。ズーム済みの横幅。線の太さを含まない
    /// </summary>
    public float SelectedTile_WorkingWidthAsFloat { get; }

    /// <summary>
    ///     矩形カーソル。ズーム済みの横幅。線の太さを含む
    /// </summary>
    public float CanvasOfTileCursor_WorkingWidthAsFloat { get; }

    /// <summary>
    ///     矩形カーソル。ズーム済みの縦幅。線の太さを含む
    /// </summary>
    public float CanvasOfTileCursor_WorkingHeightAsFloat { get; }

    /// <summary>
    ///     矩形カーソル。ズーム済みの位置ｘ
    ///         
    ///     <list type="bullet">
    ///         <item>カーソルの線の幅を含まない</item>
    ///         <item>表示用テキスト</item>
    ///     </list>
    /// </summary>
    string SelectedTile_WorkingLeftAsPresentableText { get; }

    /// <summary>
    ///     矩形カーソル。ズーム済みの位置ｙ
    ///         
    ///     <list type="bullet">
    ///         <item>カーソルの線の幅を含まない</item>
    ///         <item>表示用テキスト</item>
    ///     </list>
    /// </summary>
    string SelectedTile_WorkingTopAsPresentableText { get; }

    /// <summary>
    ///     矩形カーソル。ズーム済みの横幅
    ///         
    ///     <list type="bullet">
    ///         <item>カーソルの線の幅を含まない</item>
    ///         <item>表示用テキスト</item>
    ///     </list>
    /// </summary>
    string SelectedTile_WorkingWidthAsPresentableText { get; }

    /// <summary>
    ///     矩形カーソル。ズーム済みの縦幅
    ///         
    ///     <list type="bullet">
    ///         <item>カーソルの線の幅を含まない</item>
    ///         <item>表示用テキスト</item>
    ///     </list>
    /// </summary>
    string SelectedTile_GetWorkingHeightAsPresentableText { get; }
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
}
