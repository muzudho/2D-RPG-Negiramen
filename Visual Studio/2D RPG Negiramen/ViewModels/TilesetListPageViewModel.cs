namespace _2D_RPG_Negiramen.ViewModels;

using _2D_RPG_Negiramen.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Globalization;

/// <summary>
///     😁 ［タイルセット一覧］ページ・ビューモデル
/// </summary>
class TilesetListPageViewModel : ObservableObject, ITilesetListPageViewModel
{
    // - その他

    #region その他（生成）
    /// <summary>
    ///     生成
    /// </summary>
    public TilesetListPageViewModel()
    {
        this.TilesetRecordVMCollection = new ObservableCollection<TilesetRecordViewModel>()
        {
            new TilesetRecordViewModel(
                uuidAsStr: "１１１１てきとう",
                filePathAsStr: "てきとう",
                widthAsInt: 256,
                heightAsInt: 512,
                thumbnailFilePathAsStr: "てきとう",
                thumbnailWidthAsInt: 256,
                thumbnailHeightAsInt: 256,
                name: "なまえ１"),
            new TilesetRecordViewModel(
                uuidAsStr: "２２２２てきとう",
                filePathAsStr: "てきとう",
                widthAsInt: 256,
                heightAsInt: 512,
                thumbnailFilePathAsStr: "てきとう",
                thumbnailWidthAsInt: 256,
                thumbnailHeightAsInt: 256,
                name: "なまえ２"),
            new TilesetRecordViewModel(
                uuidAsStr: "３３３３てきとう",
                filePathAsStr: "てきとう",
                widthAsInt: 256,
                heightAsInt: 512,
                thumbnailFilePathAsStr: "てきとう",
                thumbnailWidthAsInt: 256,
                thumbnailHeightAsInt: 256,
                name: "なまえ３"),
        };
    }
    #endregion

    // - パブリック・プロパティ

    #region プロパティ（タイルセット・レコード・ビューモデルのリスト）
    /// <summary>
    ///     タイルセット・レコード・ビューモデルのリスト
    /// </summary>
    public ObservableCollection<TilesetRecordViewModel> TilesetRecordVMCollection { get; set; } = new ObservableCollection<TilesetRecordViewModel>();
    #endregion

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

    /// <summary>
    ///     ロケールＩｄのリスト
    /// </summary>
    public ObservableCollection<string> LocaleIdCollection => App.LocaleIdCollection;
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
