namespace _2D_RPG_Negiramen.ViewHistory.TileCropPage;

using _2D_RPG_Negiramen.Models.History;
using _2D_RPG_Negiramen.Specifications.TileCropPage;
using System.Globalization;

/// <summary>
///     ［文化情報設定］処理
/// </summary>
internal class SetCultureInfoProcessing : IProcessing
{
    // - その他

    /// <summary>
    ///     生成
    /// </summary>
    internal SetCultureInfoProcessing(
        ItsRoomsideDoors roomsideDoors,
        CultureInfo oldValue,
        CultureInfo newValue)
    {
        this.RoomsideDoors = roomsideDoors;
        this.OldValue = oldValue;
        this.NewValue = newValue;
    }

    /// <summary>
    ///     ドゥー
    /// </summary>
    public void Do()
    {
        this.RoomsideDoors.IndoorCultureInfo.Selected = this.NewValue;
    }

    /// <summary>
    ///     アンドゥ
    /// </summary>
    public void Undo()
    {
        this.RoomsideDoors.IndoorCultureInfo.Selected = this.OldValue;
    }

    // - プライベート・プロパティ

    /// <summary>内部クラス</summary>
    ItsRoomsideDoors RoomsideDoors { get; }

    CultureInfo OldValue { get; }

    CultureInfo NewValue { get; }
}
