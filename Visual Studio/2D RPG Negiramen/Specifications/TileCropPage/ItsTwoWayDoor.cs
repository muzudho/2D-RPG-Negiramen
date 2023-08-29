﻿namespace _2D_RPG_Negiramen.Specifications.TileCropPage;

using _2D_RPG_Negiramen.Models;
using _2D_RPG_Negiramen.Models.Geometric;
using _2D_RPG_Negiramen.Models.Visually;
using _2D_RPG_Negiramen.ViewModels;
using SkiaSharp;
using SkiaSharp.Views.Maui.Controls;
using System.Diagnostics;
using TheFileEntryLocations = Models.FileEntries.Locations;
using TheGraphics = Microsoft.Maui.Graphics;
using TheGeometric = _2D_RPG_Negiramen.Models.Geometric;
using System.Globalization;

#if IOS || ANDROID || MACCATALYST
    using Microsoft.Maui.Graphics.Platform;
#elif WINDOWS
using Microsoft.Maui.Graphics.Win2D;
using System.Net;
#endif

/// <summary>
///     双方向ドア
/// </summary>
class ItsTwoWayDoor
{
    internal ItsTwoWayDoor(ItsCorridor corridor)
    {
        this.Corridor = corridor;
    }

    /// <summary>廊下</summary>
    ItsCorridor Corridor { get; }

    #region メソッド（［タイルセット作業画像］　関連）
    /// <summary>
    ///     ［タイルセット作業画像］の再作成
    ///     
    ///     <list type="bullet">
    ///         <item>アンドゥ・リドゥで利用</item>
    ///     </list>
    /// </summary>
    public void RemakeWorkingTilesetImage()
    {
        // 元画像をベースに、作業画像を複製
        var temporaryBitmap = SKBitmap.FromImage(SKImage.FromBitmap(this.Corridor.OwnerPageVM.TilesetSourceBitmap));

        // 画像処理（明度を下げる）
        FeatSkia.ReduceBrightness.DoItInPlace(temporaryBitmap);

        // 作業画像のサイズ計算
        this.Corridor.OwnerPageVM.workingImageSize = new SizeInt(
            width: new WidthInt((int)(this.Corridor.OwnerPageVM.ZoomAsFloat * this.Corridor.RoomsideDoors.TilesetSourceImageSize.Width.AsInt)),
            height: new HeightInt((int)(this.Corridor.OwnerPageVM.ZoomAsFloat * this.Corridor.RoomsideDoors.TilesetSourceImageSize.Height.AsInt)));

        // 作業画像のリサイズ
        this.Corridor.OwnerPageVM.TilesetWorkingBitmap = temporaryBitmap.Resize(
            size: new SKSizeI(
                width: this.Corridor.OwnerPageVM.workingImageSize.Width.AsInt,
                height: this.Corridor.OwnerPageVM.workingImageSize.Height.AsInt),
            quality: SKFilterQuality.Medium);

        this.Corridor.OwnerPageVM.InvalidateTilesetWorkingImage();
    }
    #endregion

    #region メソッド（［元画像グリッド］　関連）
    /// <summary>
    ///     ［元画像グリッド］のキャンバス画像の再作成
    ///     
    ///     <list type="bullet">
    ///         <item>アンドゥ・リドゥで利用</item>
    ///         <item>グリッドの線の太さを 2px と想定しているので、グリッドの線が画像の端っこで切れないように、グリッドの内部的キャンバス・サイズを 2px 広げる</item>
    ///     </list>
    /// </summary>
    public void RemakeGridCanvasImage()
    {
        this.Corridor.OwnerPageVM.GridCanvasImageSize = new SizeInt(
            width: new WidthInt((int)(this.Corridor.OwnerPageVM.ZoomAsFloat * this.Corridor.RoomsideDoors.TilesetSourceImageSize.Width.AsInt) + 2 * this.Corridor.OwnerPageVM.HalfThicknessOfGridLineAsInt),
            height: new HeightInt((int)(this.Corridor.OwnerPageVM.ZoomAsFloat * this.Corridor.RoomsideDoors.TilesetSourceImageSize.Height.AsInt) + 2 * this.Corridor.OwnerPageVM.HalfThicknessOfGridLineAsInt));
    }
    #endregion
}
