﻿namespace _2D_RPG_Negiramen.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using System.Globalization;

/// <summary>
///     😁 ［ログイン１］ページ・ビューモデル
/// </summary>
internal class Login1PageViewModel : ObservableObject, ILogin1PageViewModel
{
    // - パブリック変更通知プロパティ

    #region 変更通知プロパティ（ロケール　関連）
    /// <summary>
    ///     現在選択中の文化情報。文字列形式
    /// </summary>
    public string CultureInfoAsStr
    {
        get
        {
            return LocalizationResourceManager.Instance.CultureInfo.Name;
        }
        set
        {
            if (LocalizationResourceManager.Instance.CultureInfo.Name != value)
            {
                LocalizationResourceManager.Instance.SetCulture(new CultureInfo(value));
                OnPropertyChanged(nameof(CultureInfoAsStr));
            }
        }
    }
    #endregion

    // - パブリック・メソッド

    #region メソッド（ロケール変更による再描画）
    /// <summary>
    ///     ロケール変更による再描画
    ///     
    ///     <list type="bullet">
    ///         <item>動的にテキストを変えている部分に対応するため</item>
    ///     </list>
    /// </summary>
    public void InvalidateLocale()
    {
        // this.InvalidateAddsButton();
    }
    #endregion
}
