﻿namespace _2D_RPG_Negiramen.Specifications.TileCropPage;

using _2D_RPG_Negiramen.ViewHistory.TileCropPage;
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
    /// <param name="owner"></param>
    internal InnerCultureInfo(IItsSpec spec)
    {
        Spec = spec;
    }
    #endregion

    // - インターナル・プロパティ

    #region プロパティ（ロケール　関連）
    /// <summary>
    ///     現在選択中の文化情報。文字列形式
    /// </summary>
    internal CultureInfo Selected
    {
        get => LocalizationResourceManager.Instance.CultureInfo;
        set
        {
            if (LocalizationResourceManager.Instance.CultureInfo != value)
            {
                CultureInfo oldValue = LocalizationResourceManager.Instance.CultureInfo;
                CultureInfo newValue = value;

                LocalizationResourceManager.Instance.SetCulture(value);
                this.Spec.OutdoorInvalidateCultureInfo();

                // 再帰的
                App.History.Do(new SetCultureInfoProcessing(
                    spec: this.Spec,
                    oldValue: oldValue,
                    newValue: newValue));
            }
        }
    }
    #endregion

    // - プライベート・プロパティ

    IItsSpec Spec { get; }
}
