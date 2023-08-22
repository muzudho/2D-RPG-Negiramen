namespace _2D_RPG_Negiramen.ViewHistory.TileCropPage;

using _2D_RPG_Negiramen.Models.History;
using _2D_RPG_Negiramen.ViewModels;
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
        TileCropPageViewModel owner,
        CultureInfo oldValue,
        CultureInfo newValue)
    {
        this.Owner = owner;
        this.OldValue = oldValue;
        this.NewValue = newValue;
    }

    /// <summary>
    ///     ドゥー
    /// </summary>
    public void Do()
    {
        this.Owner.SelectedCultureInfo = this.NewValue;
    }

    /// <summary>
    ///     アンドゥ
    /// </summary>
    public void Undo()
    {
        this.Owner.SelectedCultureInfo = this.OldValue;
    }

    // - プライベート・プロパティ

    /// <summary>
    ///     外側のクラス
    /// </summary>
    TileCropPageViewModel Owner { get; }

    CultureInfo OldValue { get; }

    CultureInfo NewValue { get; }
}
