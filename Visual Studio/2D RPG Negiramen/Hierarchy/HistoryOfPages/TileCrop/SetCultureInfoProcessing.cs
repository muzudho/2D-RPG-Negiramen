namespace _2D_RPG_Negiramen.Hierarchy.HistoryOfPages.TileCrop;

using _2D_RPG_Negiramen.Models.History;
using System.Globalization;
using _2D_RPG_Negiramen.Hierarchy.Pages.TileCrop;

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
        ItsGardensideDoor gardensideDoor,
        CultureInfo oldValue,
        CultureInfo newValue)
    {
        this.GardensideDoor = gardensideDoor;
        this.OldValue = oldValue;
        this.NewValue = newValue;
    }

    /// <summary>
    ///     ドゥー
    /// </summary>
    public void Do()
    {
        this.GardensideDoor.PageVM.SelectedCultureInfo = this.NewValue;
    }

    /// <summary>
    ///     アンドゥ
    /// </summary>
    public void Undo()
    {
        this.GardensideDoor.PageVM.SelectedCultureInfo = this.OldValue;
    }

    // - プライベート・プロパティ

    /// <summary>内部モデル</summary>
    ItsGardensideDoor GardensideDoor { get; }

    CultureInfo OldValue { get; }

    CultureInfo NewValue { get; }
}
