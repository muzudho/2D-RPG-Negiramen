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
        ItsSpec inner,
        CultureInfo oldValue,
        CultureInfo newValue)
    {
        this.Inner = inner;
        this.OldValue = oldValue;
        this.NewValue = newValue;
    }

    /// <summary>
    ///     ドゥー
    /// </summary>
    public void Do()
    {
        this.Inner.CultureInfo.Selected = this.NewValue;
    }

    /// <summary>
    ///     アンドゥ
    /// </summary>
    public void Undo()
    {
        this.Inner.CultureInfo.Selected = this.OldValue;
    }

    // - プライベート・プロパティ

    /// <summary>
    ///     内部クラス
    /// </summary>
    ItsSpec Inner { get; }

    CultureInfo OldValue { get; }

    CultureInfo NewValue { get; }
}
