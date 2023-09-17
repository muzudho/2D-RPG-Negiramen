namespace _2D_RPG_Negiramen.Hierarchy.HistoryOfPages.TileCrop;

using _2D_RPG_Negiramen.Models.History;
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
        MemberNetworkOfTileCropPage memberNetwork,
        CultureInfo oldValue,
        CultureInfo newValue)
    {
        this.MemberNetwork = memberNetwork;
        this.OldValue = oldValue;
        this.NewValue = newValue;
    }

    /// <summary>
    ///     ドゥー
    /// </summary>
    public void Do()
    {
        this.MemberNetwork.PageVM.SelectedCultureInfo = this.NewValue;
    }

    /// <summary>
    ///     アンドゥ
    /// </summary>
    public void Undo()
    {
        this.MemberNetwork.PageVM.SelectedCultureInfo = this.OldValue;
    }

    // - プライベート・プロパティ

    /// <summary>メンバー・ネットワーク</summary>
    MemberNetworkOfTileCropPage MemberNetwork { get; }

    CultureInfo OldValue { get; }

    CultureInfo NewValue { get; }
}
