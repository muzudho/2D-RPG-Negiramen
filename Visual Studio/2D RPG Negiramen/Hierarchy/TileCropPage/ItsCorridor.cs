namespace _2D_RPG_Negiramen.Hierarchy.TileCropPage
{
    using _2D_RPG_Negiramen.Models.Geometric;
    using _2D_RPG_Negiramen.ViewModels;
    using SkiaSharp;
    using _2D_RPG_Negiramen.Coding;
    using TheHierarchyTileCropPage = _2D_RPG_Negiramen.Hierarchy.TileCropPage;

#if IOS || ANDROID || MACCATALYST
    using Microsoft.Maui.Graphics.Platform;
    using _2D_RPG_Negiramen.Hierarchy.TileCropPage;
#elif WINDOWS
    using Microsoft.Maui.Graphics.Win2D;
    using System.Net;
    using _2D_RPG_Negiramen.Hierarchy.TileCropPage;
    using static _2D_RPG_Negiramen.Hierarchy.TileCropPage.AddsButton;
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
            TheHierarchyTileCropPage.ItsCommon common,
            TileCropPageViewModel ownerPageVM,
            LazyArgs.Set<string> setAddsButtonText)
        {
            this.OwnerPageVM = ownerPageVM;
            this.SetAddsButtonText = setAddsButtonText;

            this.TwoWayDoor = new ItsTwoWayDoor(this);
            this.MemberNetworkForSubordinate = new ItsMemberNetwork(
                hierarchyCommon: common);
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
        ///     メンバー・ネットワーク
        /// </summary>
        public ItsMemberNetwork MemberNetworkForSubordinate { get; }

        // - インターナル・メソッド

        #region 変更通知メソッド（ロケール変更による再描画）
        /// <summary>
        ///     ロケール変更による再描画
        ///     
        ///     <list type="bullet">
        ///         <item>動的にテキストを変えている部分に対応するため</item>
        ///     </list>
        /// </summary>
        internal void InvalidateByLocale() => this.MemberNetworkForSubordinate.AddsButton.MonitorStateOfAddsButton(
            setAddsButtonText: this.SetAddsButtonText);
        #endregion

        // - インターナル・プロパティ

        internal LazyArgs.Set<string> SetAddsButtonText { get; }

        // - プライベート・メソッド

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
                width: new WidthInt((int)(this.MemberNetworkForSubordinate.ZoomProperties.AsFloat * this.MemberNetworkForSubordinate.TilesetSourceImageSize.Width.AsInt) + 2 * this.OwnerPageVM.HalfThicknessOfGridLineAsInt),
                height: new HeightInt((int)(this.MemberNetworkForSubordinate.ZoomProperties.AsFloat * this.MemberNetworkForSubordinate.TilesetSourceImageSize.Height.AsInt) + 2 * this.OwnerPageVM.HalfThicknessOfGridLineAsInt));
        }
        #endregion
    }
}
