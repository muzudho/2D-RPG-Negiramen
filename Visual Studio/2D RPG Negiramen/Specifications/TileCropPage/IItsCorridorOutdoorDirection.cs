namespace _2D_RPG_Negiramen.Specifications.TileCropPage;

/// <summary>
///     廊下の、屋外への向き
/// </summary>
interface IItsCorridorOutdoorDirection
{
    /// <summary>
    ///     屋外側のドア
    /// </summary>
    ItsGardensideDoor GardensideDoor { get; }

    void ObsoletedPageVMInvalidateAddsButton();
    void ObsoletedPageVMInvalidateTilesetSettingsVM();
    void ObsoletedPageVMInvalidateForHistory();

    #region 変更通知メソッド（［選択タイル］　関連）
    /// <summary>
    ///     ［選択タイル］Ｉｄの再描画
    /// </summary>
    void ObsoletedPageVMInvalidateTileIdChange();
    #endregion

    string ObsoletedPageVMAddsButtonText { get; set; }
    string ObsoletedPageVMAddsButtonHint { get; }
    void ObsoletedPageVMInvalidateWorkingTargetTile();

    float ObsoletedPageVMWorkingGridTileWidthAsFloat { set; }
    float ObsoletedPageVMWorkingGridTileHeightAsFloat { set; }

    bool ObsoletedPageVMCropTileLogicalDeleteAsBool { set; }

    int ObsoletedPageVMCropTileSourceLeftAsInt { set; }
    int ObsoletedPageVMCropTileSourceTopAsInt { set; }
    int ObsoletedPageVMCropTileSourceWidthAsInt { set; }
    int ObsoletedPageVMCropTileSourceHeightAsInt { set; }

    void ObsoletedPageVMInvalidateIsMouseDragging();

    float ObsoletedPageVMZoomMinAsFloat { get; }
    float ObsoletedPageVMZoomMaxAsFloat { get; }



    float ObsoletedPageVMZoomAsFloat { get; set; }

    void ObsoletedPageVMInvalidateDeletesButton();

    void ObsoletedPageVMInvalidateCultureInfo();
}
