namespace _2D_RPG_Negiramen.Specifications.TileCropPage;

using _2D_RPG_Negiramen.Models;
using _2D_RPG_Negiramen.Models.Geometric;
using _2D_RPG_Negiramen.Models.Visually;
using TheFileEntryLocations = Models.FileEntries.Locations;
using TheGeometric = _2D_RPG_Negiramen.Models.Geometric;

interface IItsOutdoor
{
    void InvalidateAddsButton();
    void InvalidateTilesetSettingsVM();
    void InvalidateForHistory();
    bool TilesetSettingsVMDeleteLogical(TileIdOrEmpty id);
    bool TilesetSettingsVMUndeleteLogical(TileIdOrEmpty id);

    /// <summary>
    ///     ［タイルセット・データテーブル］ファイルの場所
    ///     <list type="bullet">
    ///         <item>ページの引数として使用</item>
    ///     </list>
    /// </summary>
    TheFileEntryLocations.UnityAssets.DataCsvTilesetCsv TilesetDatatableFileLocation { get; }

    #region 変更通知メソッド（［選択タイル］　関連）
    /// <summary>
    ///     ［選択タイル］Ｉｄの再描画
    /// </summary>
    void InvalidateTileIdChange();
    #endregion

    void TilesetSettingsVMIncreaseUsableId();
    TileIdOrEmpty TilesetSettingsVMUsableId { get; }
    bool TilesetSettingsVMSaveCsv(TheFileEntryLocations.UnityAssets.DataCsvTilesetCsv tileSetSettingsFile);
    bool TilesetSettingsVMTryGetTileById(TileIdOrEmpty tileId, out TileRecordVisually? resultVisuallyOrNull);
    void TilesetSettingsVMAddTileVisually(TileIdOrEmpty id,
        TheGeometric.RectangleInt rect,
        Zoom zoom,
        TileTitle title,
        LogicalDelete logicalDelete);

    bool TilesetSettingsVMTryRemoveTileById(TileIdOrEmpty tileId, out TileRecordVisually? resultVisuallyOrNull);

    string AddsButtonText { get; set; }
    string AddsButtonHint { get; }
    void InvalidateWorkingTargetTile();

    float WorkingGridTileWidthAsFloat { set; }
    float WorkingGridTileHeightAsFloat { set; }

    bool CropTileLogicalDeleteAsBool { set; }

    int CropTileSourceLeftAsInt { set; }
    int CropTileSourceTopAsInt { set; }
    int CropTileSourceWidthAsInt { set; }
    int CropTileSourceHeightAsInt { set; }

    void InvalidateIsMouseDragging();

    float ZoomMinAsFloat { get; }
    float ZoomMaxAsFloat { get; }



    float ZoomAsFloat { get; set; }

    /// <summary>
    ///     ［切抜きカーソルが指すタイル］の元画像ベースの矩形
    ///     
    ///     <list type="bullet">
    ///         <item>カーソルが無いとき、大きさの無いカーソルを返す</item>
    ///     </list>
    /// </summary>
    RectangleInt CroppedCursorPointedTileSourceRect { get; set; }

    void InvalidateDeletesButton();

    void InvalidateCultureInfo();

    HeightFloat CroppedCursorPointedTileWorkingHeight { set; }

    List<TileRecordVisually> TilesetSettingsVMTileRecordVisuallyList { get; }

    void RefreshForTileAdd();

    void RemakeWorkingTilesetImage();
    void RemakeGridCanvasImage();
}
