namespace _2D_RPG_Negiramen.Specifications.TileCropPage;

using _2D_RPG_Negiramen.Models.Visually;
using _2D_RPG_Negiramen.ViewModels;

/// <summary>
///     屋外側ドア
/// </summary>
internal class ItsGardensideDoor
{
    internal ItsGardensideDoor(TileCropPageViewModel tileCropPageViewModel)
    {
        this.PageVM = tileCropPageViewModel;
    }

    // - パブリック・プロパティ

    /// <summary>タイルセット設定ビューモデル</summary>
    public TilesetDatatableVisually TilesetSettingsVM => PageVM.TilesetSettingsVM;

    // - インターナル・プロパティ

    /// <summary>全体ページ・ビューモデル</summary>
    internal TileCropPageViewModel PageVM { get; }
}
