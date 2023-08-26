namespace _2D_RPG_Negiramen.Specifications.TileCropPage;

using _2D_RPG_Negiramen.Models;
using _2D_RPG_Negiramen.Models.Geometric;
using _2D_RPG_Negiramen.Models.Visually;
using System.Globalization;
using TheFileEntryLocations = Models.FileEntries.Locations;
using TheGeometric = _2D_RPG_Negiramen.Models.Geometric;

/// <summary>
///     タイル切抜きページ
/// </summary>
interface IItsSpec
{
    // - パブリック・プロパティ

    bool OutdoorTilesetSettingsVMDeleteLogical(TileIdOrEmpty id);
    bool OutdoorTilesetSettingsVMUndeleteLogical(TileIdOrEmpty id);

    CultureInfo IndoorCultureInfoSelected { set; }

    TileRecordVisually IndoorCropTileSavesRecordVisually { get; }

    TileRecordVisually IndoorCropTileTargetTileRecordVisually { get; }

    TileIdOrEmpty IndoorCropTileIdOrEmpty { set; get; }

    Zoom IndoorZoomValue { get; set; }

    /// <summary>
    ///     ［タイルセット・データテーブル］ファイルの場所
    ///     <list type="bullet">
    ///         <item>ページの引数として使用</item>
    ///     </list>
    /// </summary>
    TheFileEntryLocations.UnityAssets.DataCsvTilesetCsv OutdoorTilesetDatatableFileLocation { get; }

    #region 変更通知メソッド（［選択タイル］　関連）
    /// <summary>
    ///     ［選択タイル］Ｉｄの再描画
    /// </summary>
    void WholeInvalidateTileIdChange();
    #endregion

    void OutdoorTilesetSettingsVMIncreaseUsableId();
    TileIdOrEmpty OutdoorTilesetSettingsVMUsableId { get; }

    bool OutdoorTilesetSettingsVMSaveCsv(TheFileEntryLocations.UnityAssets.DataCsvTilesetCsv tileSetSettingsFile);

    bool OutdoorTilesetSettingsVMTryGetTileById(TileIdOrEmpty tileId, out TileRecordVisually? resultVisuallyOrNull);

    void OutdoorTilesetSettingsVMAddTileVisually(TileIdOrEmpty id,
        TheGeometric.RectangleInt rect,
        Zoom zoom,
        TileTitle title,
        LogicalDelete logicalDelete);

    bool OutdoorTilesetSettingsVMTryRemoveTileById(TileIdOrEmpty tileId, out TileRecordVisually? resultVisuallyOrNull);

    string OutdoorAddsButtonText { get; set; }
    string OutdoorAddsButtonHint { get; }
    void OutdoorInvalidateWorkingTargetTile();

    float OutdorrWorkingGridTileWidthAsFloat { set; }
    float OutdoorWorkingGridTileHeightAsFloat { set; }

    bool OutdoorCropTileLogicalDeleteAsBool { set; }

    int OutdoorCropTileSourceLeftAsInt { set; }
    int OutdoorCropTileSourceTopAsInt { set; }
    int OutdoorCropTileSourceWidthAsInt { set; }
    int OutdoorCropTileSourceHeightAsInt { set; }

    void OutdoorInvalidateIsMouseDragging();

    float OutdoorZoomMinAsFloat { get; }
    float OutdoorZoomMaxAsFloat { get; }

    void IndoorCropCursorRefreshCanvasTrick(string codePlace);



    float OutdoorZoomAsFloat { get; set; }

    int IndoorGridUnitSourceValueWidthAsInt { get; }
    int IndoorGridUnitSourceValueHeightAsInt { get; }


    /// <summary>
    ///     ［切抜きカーソルが指すタイル］の元画像ベースの矩形
    ///     
    ///     <list type="bullet">
    ///         <item>カーソルが無いとき、大きさの無いカーソルを返す</item>
    ///     </list>
    /// </summary>
    RectangleInt OutdoorCroppedCursorPointedTileSourceRect { get; set; }

    void OutdoorInvalidateDeletesButton();

    void OutdoorInvalidateCultureInfo();


    bool HasIntersectionBetweenCroppedCursorAndRegisteredTile { get; set; }

    bool IsCongruenceBetweenCroppedCursorAndRegisteredTile { get; set; }

    // - パブリック・メソッド

    void WholeRefreshForTileAdd();

    void AddsButtonRefresh();

    void DeletesButtonRefresh();



    void WholeRemakeWorkingTilesetImage();
    void WholeRemakeGridCanvasImage();

    void CropCursorRecalculateWorkingGridTileWidth();
    void CropCursorRecalculateWorkingGridTileHeight();

    TheGeometric.WidthFloat CropCursorWorkingWidthWithoutTrick { set; }
    HeightFloat OutdoorCroppedCursorPointedTileWorkingHeight { set; }

    List<TileRecordVisually> OutdoorTilesetSettingsVMTileRecordVisuallyList { get; }

}
