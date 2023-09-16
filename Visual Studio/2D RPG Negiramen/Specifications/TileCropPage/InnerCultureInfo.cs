namespace _2D_RPG_Negiramen.Specifications.TileCropPage;

using System.Globalization;

/// <summary>
///     文化情報
/// </summary>
internal class InnerCultureInfo
{
    // - その他

    #region その他（生成）
    /// <summary>
    ///     生成
    /// </summary>
    internal InnerCultureInfo()
    {
    }
    #endregion

    // - インターナル・デリゲート

    //internal delegate void DoSetCultureInfoProcessing(
    //    CultureInfo oldValue,
    //    CultureInfo newValue);

    // - インターナル・プロパティ

    #region プロパティ（ロケール　関連）
    /// <summary>
    ///     現在選択中の文化情報。文字列形式
    /// </summary>
    internal CultureInfo Selected => LocalizationResourceManager.Instance.CultureInfo;
    #endregion

    // TODO これが未使用なのはおかしくない？
    //internal void SetSelected(
    //    CultureInfo value)
    //{
    //    if (LocalizationResourceManager.Instance.CultureInfo != value)
    //    {
    //        CultureInfo oldValue = LocalizationResourceManager.Instance.CultureInfo;
    //        CultureInfo newValue = value;

    //        LocalizationResourceManager.Instance.SetCulture(value);
    //        this.ＧardensideDoor.PageVM.InvalidateCultureInfo();

    //        // 再帰的
    //        App.History.Do(new SetCultureInfoProcessing(
    //            roomsideDoors: this.RoomsideDoors,
    //            oldValue: oldValue,
    //            newValue: newValue));
    //    }
    //}
}
