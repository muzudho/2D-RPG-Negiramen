namespace _2D_RPG_Negiramen.ViewModels;

using _2D_RPG_Negiramen.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Concurrent;
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
        this.TilesetRecordVMQueue = new ConcurrentQueue<TilesetRecordViewModel>();
    }
    #endregion

    // - パブリック・プロパティ

    #region プロパティ（タイルセット・レコード・ビューモデルのリスト）
    /// <summary>
    ///     タイルセット・レコード・ビューモデルのリスト
    /// </summary>
    public ObservableCollection<TilesetRecordViewModel> TilesetRecordVMCollection => new ObservableCollection<TilesetRecordViewModel>(this.TilesetRecordVMQueue.ToList());
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

    /// <summary>
    ///     タイルセット・レコード・ビューモデル追加
    /// </summary>
    /// <param name="element"></param>
    public void EnqueueTilesetRecordVM(TilesetRecordViewModel element)
    {
        this.TilesetRecordVMQueue.Enqueue(element);
        OnPropertyChanged(nameof(TilesetRecordVMCollection));
    }

    // - プライベート・プロパティ

    #region プロパティ（タイルセット・レコード・ビューモデルのリスト）
    /// <summary>
    ///     タイルセット・レコード・ビューモデルのリスト
    /// </summary>
    ConcurrentQueue<TilesetRecordViewModel> TilesetRecordVMQueue { get; set; } = new ConcurrentQueue<TilesetRecordViewModel>();
    #endregion
}
