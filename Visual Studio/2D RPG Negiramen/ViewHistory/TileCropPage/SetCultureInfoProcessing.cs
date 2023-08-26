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
        IItsSpec spec,
        CultureInfo oldValue,
        CultureInfo newValue)
    {
        this.Spec = spec;
        this.OldValue = oldValue;
        this.NewValue = newValue;
    }

    /// <summary>
    ///     ドゥー
    /// </summary>
    public void Do()
    {
        this.Spec.IndoorCultureInfoSelected = this.NewValue;
    }

    /// <summary>
    ///     アンドゥ
    /// </summary>
    public void Undo()
    {
        this.Spec.IndoorCultureInfoSelected = this.OldValue;
    }

    // - プライベート・プロパティ

    /// <summary>
    ///     内部クラス
    /// </summary>
    IItsSpec Spec { get; }

    CultureInfo OldValue { get; }

    CultureInfo NewValue { get; }
}
