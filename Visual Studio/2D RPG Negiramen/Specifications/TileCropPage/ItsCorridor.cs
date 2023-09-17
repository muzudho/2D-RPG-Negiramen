namespace _2D_RPG_Negiramen.Specifications.TileCropPage
{
    using _2D_RPG_Negiramen.Models.Geometric;
    using _2D_RPG_Negiramen.ViewModels;
    using SkiaSharp;
    using _2D_RPG_Negiramen.Coding;

#if IOS || ANDROID || MACCATALYST
    using Microsoft.Maui.Graphics.Platform;
    using _2D_RPG_Negiramen.Specifications.TileCropPage;
#elif WINDOWS
    using Microsoft.Maui.Graphics.Win2D;
    using System.Net;
    using _2D_RPG_Negiramen.Specifications.TileCropPage;
    using static _2D_RPG_Negiramen.Specifications.TileCropPage.AddsButton;
#endif

    /// <summary>
    ///     コーリダー（Corridor；廊下）アーキテクチャー
    ///     
    ///     <list type="bullet">
    ///         <item>ミュータブル</item>
    ///     </list>
    /// </summary>
    class ItsCorridor : IItsTwoWayDoor
    {
        // - その他

        #region その他（生成）
        /// <summary>
        ///     生成
        /// </summary>
        /// <param name="ownerPageVM">全体ページビュー・モデル</param>
        internal ItsCorridor(
            TileCropPageViewModel ownerPageVM,
            LazyArgs.Set<string> setAddsButtonText)
        {
            this.OwnerPageVM = ownerPageVM;
            this.SetAddsButtonText = setAddsButtonText;

            this.TwoWayDoor = new ItsTwoWayDoor(this);
            this.RoomsideDoors = new ItsRoomsideDoors(this);
        }
        #endregion

        // - インターナル・プロパティ

        /// <summary>全体ページ・ビューモデル</summary>
        internal TileCropPageViewModel OwnerPageVM { get; }

        /// <summary>
        ///     双方向ドア
        /// </summary>
        public ItsTwoWayDoor TwoWayDoor { get; }

        /// <summary>
        ///     屋内側のドア
        /// </summary>
        public ItsRoomsideDoors RoomsideDoors { get; }

        // - インターナル・メソッド

        #region 変更通知メソッド（ロケール変更による再描画）
        /// <summary>
        ///     ロケール変更による再描画
        ///     
        ///     <list type="bullet">
        ///         <item>動的にテキストを変えている部分に対応するため</item>
        ///     </list>
        /// </summary>
        internal void InvalidateByLocale() => this.RoomsideDoors.AddsButton.MonitorState(
            setAddsButtonText: this.SetAddsButtonText);
        #endregion

        // - インターナル・プロパティ

        internal LazyArgs.Set<string> SetAddsButtonText { get; }

        // - プライベート・メソッド

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
            var temporaryBitmap = SKBitmap.FromImage(SKImage.FromBitmap(this.OwnerPageVM.TilesetSourceBitmap));

            // 画像処理（明度を下げる）
            FeatSkia.ReduceBrightness.DoItInPlace(temporaryBitmap);

            // 作業画像のサイズ計算
            this.OwnerPageVM.workingImageSize = new SizeInt(
                width: new WidthInt((int)(this.OwnerPageVM.ZoomAsFloat * this.RoomsideDoors.TilesetSourceImageSize.Width.AsInt)),
                height: new HeightInt((int)(this.OwnerPageVM.ZoomAsFloat * this.RoomsideDoors.TilesetSourceImageSize.Height.AsInt)));

            // 作業画像のリサイズ
            this.OwnerPageVM.TilesetWorkingBitmap = temporaryBitmap.Resize(
                size: new SKSizeI(
                    width: this.OwnerPageVM.workingImageSize.Width.AsInt,
                    height: this.OwnerPageVM.workingImageSize.Height.AsInt),
                quality: SKFilterQuality.Medium);

            this.OwnerPageVM.InvalidateTilesetWorkingImage();
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
            // ズームが屋外
            this.OwnerPageVM.GridCanvasImageSize = new SizeInt(
                width: new WidthInt((int)(this.RoomsideDoors.ZoomProperties.AsFloat * this.RoomsideDoors.TilesetSourceImageSize.Width.AsInt) + 2 * this.OwnerPageVM.HalfThicknessOfGridLineAsInt),
                height: new HeightInt((int)(this.RoomsideDoors.ZoomProperties.AsFloat * this.RoomsideDoors.TilesetSourceImageSize.Height.AsInt) + 2 * this.OwnerPageVM.HalfThicknessOfGridLineAsInt));
        }
        #endregion
    }
}
