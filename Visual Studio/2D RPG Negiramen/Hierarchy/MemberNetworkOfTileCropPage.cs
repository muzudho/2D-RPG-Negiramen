namespace _2D_RPG_Negiramen.Hierarchy;

using _2D_RPG_Negiramen.ViewModels;

/// <summary>
///     メンバー・ネットワーク
/// </summary>
internal class MemberNetworkOfTileCropPage
{
    internal MemberNetworkOfTileCropPage(TileCropPageViewModel tileCropPageViewModel)
    {
        PageVM = tileCropPageViewModel;
    }

    // - インターナル・プロパティ

    /// <summary>全体ページ・ビューモデル</summary>
    internal TileCropPageViewModel PageVM { get; }
}
