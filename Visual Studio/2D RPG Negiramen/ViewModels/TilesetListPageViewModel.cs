namespace _2D_RPG_Negiramen.ViewModels;

using _2D_RPG_Negiramen.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls;
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
    public TilesetListPageViewModel(GridItemsLayout itemsLayout)
    {
        this.TilesetRecordList = new List<TilesetRecordViewModel>();
        this.ItemsLayout = itemsLayout;
    }
    #endregion

    // - パブリック・プロパティ

    #region プロパティ（タイルセット・レコード・ビューモデルのリスト）
    /// <summary>
    ///     タイルセット・レコード・ビューモデルのリスト
    /// </summary>
    // public ObservableCollection<TilesetRecordViewModel> TilesetRecordVMCollection => new(this.TilesetRecordList);
    public ObservableCollection<TilesetRecordViewModel> TilesetRecordVMCollection => new(this.TilesetRecordList.ToList());
    #endregion

    // - パブリック変更通知プロパティ

    #region 変更通知プロパティ（ロケール　関連）
    /// <summary>
    ///     現在選択中の文化情報。文字列形式
    /// </summary>
    public CultureInfo SelectedCultureInfo
    {
        get
        {
            return LocalizationResourceManager.Instance.CultureInfo;
        }
        set
        {
            if (LocalizationResourceManager.Instance.CultureInfo != value)
            {
                LocalizationResourceManager.Instance.SetCulture(value);
                OnPropertyChanged(nameof(SelectedCultureInfo));
            }
        }
    }

    /// <summary>
    ///     文化情報のリスト
    /// </summary>
    public ObservableCollection<CultureInfo> CultureInfoCollection => App.CultureInfoCollection;
    #endregion

    #region 変更通知プロパティ（CollectionView の ItemsLayout プロパティ）
    /// <summary>
    ///     CollectionView の ItemsLayout プロパティ
    /// </summary>
    public GridItemsLayout ItemsLayout
    {
        get =>
            this.itemsLayout;
        set
        {
            if (this.itemsLayout == value)
                return;

            this.itemsLayout = value;
            OnPropertyChanged(nameof(ItemsLayout));
        }
    }
    #endregion

    #region 変更通知プロパティ（［タイル切抜き］ボタン　関連）
    /// <summary>
    ///     ［タイル切抜き］ボタンの活性性
    /// </summary>
    public bool IsEnabledTileCropButton
    {
        get => this.isEnabledTileCropButton;
        set
        {
            if (this.isEnabledTileCropButton == value)
                return;

            this.isEnabledTileCropButton = value;
            OnPropertyChanged(nameof(IsEnabledTileCropButton));
        }
    }
    #endregion

    #region 変更通知プロパティ（［ファイル・ステムをＵＵＩＤに変更する］ボタン　関連）
    /// <summary>
    ///     ［ファイル・ステムをＵＵＩＤに変更する］ボタンの活性性
    /// </summary>
    public bool IsEnabledRenameFileNameToUUIDButton
    {
        get => this.isEnabledRenameFileNameToUUIDButton;
        set
        {
            if (this.isEnabledRenameFileNameToUUIDButton == value)
                return;

            this.isEnabledRenameFileNameToUUIDButton = value;
            OnPropertyChanged(nameof(IsEnabledRenameFileNameToUUIDButton));
        }
    }
    #endregion

    #region 変更通知プロパティ（［タイルセット削除］ボタン　関連）
    /// <summary>
    ///     ［タイルセット削除］ボタンの活性性
    /// </summary>
    public bool IsEnabledTilesetRemoveButton
    {
        get => this.isEnabledTilesetRemoveButton;
        set
        {
            if (this.isEnabledTilesetRemoveButton == value)
                return;

            this.isEnabledTilesetRemoveButton = value;
            OnPropertyChanged(nameof(IsEnabledTilesetRemoveButton));
        }
    }
    #endregion

    #region 変更通知プロパティ（選択ファイル・ステム）
    /// <summary>
    ///     選択ファイル・ステム
    /// </summary>
    public string SelectedFileStemAsStr
    {
        get => this.selectedFileStem;
        set
        {
            if (this.selectedFileStem == value)
                return;

            this.selectedFileStem = value;
            OnPropertyChanged(nameof(SelectedFileStemAsStr));
        }
    }
    #endregion

    #region 変更通知プロパティ（選択ファイル拡張子）
    /// <summary>
    ///     選択ファイル拡張子
    /// </summary>
    public string SelectedFileExtension
    {
        get => this.selectedFileExtension;
        set
        {
            if (this.selectedFileExtension == value)
                return;

            this.selectedFileExtension = value;
            OnPropertyChanged(nameof(SelectedFileExtension));
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

    #region メソッド（タイルセット・レコード・ビューモデル・キュー　関連）
    /// <summary>
    ///     タイルセット・レコード・ビューモデル追加
    /// </summary>
    /// <param name="element"></param>
    public void AddTilesetRecord(TilesetRecordViewModel element)
    {
        this.TilesetRecordList.Add(element);
        OnPropertyChanged(nameof(TilesetRecordVMCollection));
    }

    /// <summary>
    ///     タイルセット・レコード・ビューモデル削除
    /// </summary>
    /// <param name="element"></param>
    public void DeleteTilesetRecordByFileStem(FileStem fileStem)
    {
        var hitIndex = -1;

        for (int i = 0; i < this.TilesetRecordList.Count; i++)
        {
            var record = this.TilesetRecordList[i];

            if (System.IO.Path.GetFileNameWithoutExtension(record.PngFilePathAsStr)==fileStem.AsStr)
            {
                hitIndex = i;
                break;
            }
        }

        // 任意の位置の要素を削除
        if (0 <= hitIndex)
        {
            this.TilesetRecordList.RemoveAt(hitIndex);
            OnPropertyChanged(nameof(TilesetRecordVMCollection));
        }
    }
    #endregion

    // - プライベート・プロパティ

    #region プロパティ（タイルセット・レコードのリスト）
    /// <summary>
    ///     タイルセット・レコードのリスト
    /// </summary>
    List<TilesetRecordViewModel> TilesetRecordList { get; set; } = new List<TilesetRecordViewModel>();
    #endregion

    GridItemsLayout itemsLayout;

    bool isEnabledTileCropButton;
    bool isEnabledRenameFileNameToUUIDButton;
    bool isEnabledTilesetRemoveButton;

    string selectedFileStem;
    string selectedFileExtension;
}
