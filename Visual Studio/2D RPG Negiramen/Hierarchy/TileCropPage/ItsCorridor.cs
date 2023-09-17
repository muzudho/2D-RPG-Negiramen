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
    class ItsCorridor
    {
        // - その他

        #region その他（生成）
        /// <summary>
        ///     生成
        /// </summary>
        /// <param name="ownerPageVM">全体ページビュー・モデル</param>
        internal ItsCorridor(
            TheHierarchyTileCropPage.ItsCommon common,
            TileCropPageViewModel ownerPageVM)
        {
            this.OwnerPageVM = ownerPageVM;

            this.MemberNetworkForSubordinate = new ItsMemberNetwork(
                hierarchyCommon: common);
        }
        #endregion

        // - インターナル・プロパティ

        /// <summary>全体ページ・ビューモデル</summary>
        internal TileCropPageViewModel OwnerPageVM { get; }

        /// <summary>
        ///     メンバー・ネットワーク
        /// </summary>
        public ItsMemberNetwork MemberNetworkForSubordinate { get; }
    }
}
