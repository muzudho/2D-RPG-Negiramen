namespace _2D_RPG_Negiramen.ViewModels;

using _2D_RPG_Negiramen.Models;
using _2D_RPG_Negiramen.Models.FileEntries;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls;
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
        this.TilesetRecordVMList = new List<TilesetRecordViewModel>();
        this.ItemsLayout = itemsLayout;
    }
    #endregion

    // - パブリック・プロパティ

    #region プロパティ（コレクション・ビューのアイテム・ソース）
    /// <summary>
    ///     コレクション・ビューのアイテム・ソース
    /// </summary>
    // public ObservableCollection<TilesetRecordViewModel> TilesetRecordVMCollection => new(this.TilesetRecordList);
    public ObservableCollection<TilesetRecordViewModel> TilesetRecordVMCollection => new(this.TilesetRecordVMList.ToList());
    #endregion

    // - パブリック変更通知プロパティ

    #region 変更通知プロパティ（ロケール　関連）
    /// <summary>
    ///     現在選択中の文化情報。文字列形式
    /// </summary>
    public CultureInfo SelectedCultureInfo
    {
        get => LocalizationResourceManager.Instance.CultureInfo;
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
        get => this.itemsLayout;
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
    public string SelectedTilesetFileStemAsStr
    {
        get => this.selectedTilesetFileStemAsStr;
        set
        {
            if (this.selectedTilesetFileStemAsStr == value)
                return;

            this.selectedTilesetFileStemAsStr = value;
            OnPropertyChanged(nameof(SelectedTilesetFileStemAsStr));

            // TODO ファイルへの書出し
        }
    }
    #endregion

    #region 変更通知プロパティ（選択ファイル拡張子）
    /// <summary>
    ///     選択ファイル拡張子
    /// </summary>
    public string SelectedTilesetFileExtensionAsStr
    {
        get => this.selectedTilesetFileExtensionAsStr;
        set
        {
            if (this.selectedTilesetFileExtensionAsStr == value)
                return;

            this.selectedTilesetFileExtensionAsStr = value;
            OnPropertyChanged(nameof(SelectedTilesetFileExtensionAsStr));
        }
    }
    #endregion

    #region 変更通知プロパティ（選択タイルセット・タイトル）
    /// <summary>
    ///     選択タイルセット・タイトル
    /// </summary>
    public string SelectedTilesetTitleAsStr
    {
        get => this.selectedTilesetTitleAsStr;
        set
        {
            if (this.selectedTilesetTitleAsStr == value)
                return;

            this.selectedTilesetTitleAsStr = value;
            OnPropertyChanged(nameof(SelectedTilesetTitleAsStr));
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
        this.InvalidateCollectionViewByLocaleChanged();
    }
    #endregion

    #region メソッド（タイルセット・レコード・ビューモデル・キュー　関連）
    /// <summary>
    ///     タイルセット・レコード・ビューモデル追加
    /// </summary>
    /// <param name="element"></param>
    public void AddTilesetRecord(TilesetRecordViewModel element)
    {
        this.TilesetRecordVMList.Add(element);
        OnPropertyChanged(nameof(TilesetRecordVMCollection));
    }

    /// <summary>
    ///     タイルセット・レコード・ビューモデル削除
    /// </summary>
    /// <param name="element"></param>
    public void DeleteTilesetRecordByFileStem(FileStem fileStem)
    {
        var hitIndex = -1;

        for (int i = 0; i < this.TilesetRecordVMList.Count; i++)
        {
            var record = this.TilesetRecordVMList[i];

            if (System.IO.Path.GetFileNameWithoutExtension(record.PngFilePathAsStr) == fileStem.AsStr)
            {
                hitIndex = i;
                break;
            }
        }

        // 任意の位置の要素を削除
        if (0 <= hitIndex)
        {
            this.TilesetRecordVMList.RemoveAt(hitIndex);
            OnPropertyChanged(nameof(TilesetRecordVMCollection));
        }
    }
    #endregion

    #region メソッド（選択タイルセット設定）
    /// <summary>
    ///     選択タイルセット設定
    /// </summary>
    /// <param name="selectedTilesetRecord"></param>
    public void SetSelectedTileset(TilesetRecordViewModel? selectedTilesetRecord)
    {
        this.SelectedTilesetRecord = selectedTilesetRecord;

        // 未選択なら
        if (selectedTilesetRecord == null)
        {
            this.IsEnabledTileCropButton = false;
            this.IsEnabledRenameFileNameToUUIDButton = false;
            this.IsEnabledTilesetRemoveButton = false;

            return;
        }

        // 選択ファイル・ステム
        this.SelectedTilesetFileStemAsStr = System.IO.Path.GetFileNameWithoutExtension(selectedTilesetRecord.PngFilePathAsStr);

        if (UUIDHelper.IsMatch(this.SelectedTilesetFileStemAsStr))
        {
            // UUID だ
            this.IsEnabledTileCropButton = true;
            this.IsEnabledRenameFileNameToUUIDButton = false;
        }
        else
        {
            // UUID ではない
            this.IsEnabledTileCropButton = false;
            this.IsEnabledRenameFileNameToUUIDButton = true;
        }

        // タイルセット削除ボタンの活性性
        this.IsEnabledTilesetRemoveButton = true;

        // 選択ファイル拡張子
        this.SelectedTilesetFileExtensionAsStr = System.IO.Path.GetExtension(selectedTilesetRecord.PngFilePathAsStr);

        // 選択タイルセット・タイトル
        this.SelectedTilesetTitleAsStr = selectedTilesetRecord.TitleAsStr;
    }
    #endregion

    #region メソッド（選択タイルセットのタイトル設定）
    /// <summary>
    ///     選択タイルセットのタイトル設定
    ///     
    ///     <list type="bullet">
    ///         <item>テキストボックスの１文字変えるたびにファイルに保存していると重いので、保存があるものは分けてある</item>
    ///     </list>
    /// </summary>
    public void SetSelectedTilesetTitleAsStr(string titleAsStr)
    {
        // 選択要素の更新
        if (this.SelectedTilesetRecord != null)
        {
            this.SelectedTilesetRecord.TitleAsStr = titleAsStr;

            this.SaveSelectedTileset(
                difference: new TilesetLocalConfigurationBuffer()
                {
                    Title = TilesetTitle.FromString(titleAsStr),
                });

            // コレクション・ビューの更新
            OnPropertyChanged(nameof(TilesetRecordVMCollection));
        }
    }
    #endregion

    #region メソッド（選択中のタイルセットを保存）
    /// <summary>
    ///     選択中のタイルセットを保存
    /// </summary>
    public void SaveSelectedTileset(TilesetLocalConfigurationBuffer difference)
    {
        var tilesetPngFileStem = FileStem.FromString(this.SelectedTilesetFileStemAsStr);

        // ファイルへ保存
        if (TilesetLocalConfiguration.TryLoadOrAdd(
            tilesetPngFileStem: tilesetPngFileStem,
            out TilesetLocalConfiguration? newConfiguration,
            out bool isNew))
        {
            // タイルセット・ローカル構成ファイルの場所（TOML）
            var tilesetLocalConfigurationLocation = App.GetOrLoadProjectConfiguration().UnityAssetsFolderLocation.YourCircleFolder.YourWorkFolder.AutoGeneratedFolder.ImagesFolder.TilesetsFolder.LocalesFolder.CreateSelectedLocaleFolder().CreateTilesetLocalToml(tilesetPngFileStem);

            TilesetLocalConfiguration.SaveTOML(
                tilesetLocalConfigurationLocation: tilesetLocalConfigurationLocation,
                current: newConfiguration ?? throw new Exception(),
                difference: difference,
                out newConfiguration);
        }
    }
    #endregion

    #region メソッド（ロケールの変更により、コレクション・ビューを更新する）
    /// <summary>
    ///     ロケールの変更により、コレクション・ビューを更新する
    ///     
    ///     <list type="bullet">
    ///         <item>すごく重い処理。設定ファイルを開けまくる</item>
    ///     </list>
    /// </summary>
    void InvalidateCollectionViewByLocaleChanged()
    {
        foreach (var recordVM in this.TilesetRecordVMList)
        {
            FileStem fileStem = FileStem.FromString(recordVM.UuidAsStr);
            var tilesetLocalTomlLocation = App.GetOrLoadProjectConfiguration().UnityAssetsFolderLocation.YourCircleFolder.YourWorkFolder.AutoGeneratedFolder.ImagesFolder.TilesetsFolder.LocalesFolder.CreateSelectedLocaleFolder().CreateTilesetLocalToml(fileStem);

            if (TilesetLocalConfiguration.TryLoadOrAdd(
                fileStem,
                out TilesetLocalConfiguration? newConfiguration,
                out bool isNew))
            {
                if (newConfiguration != null)
                {
                    recordVM.TitleAsStr = newConfiguration.Title.AsStr;
                }
            }
        }

        OnPropertyChanged(nameof(TilesetRecordVMCollection));
    }
    #endregion

    // - プライベート・プロパティ

    #region プロパティ（タイルセット・レコードのリスト）
    /// <summary>
    ///     タイルセット・レコードのリスト
    /// </summary>
    List<TilesetRecordViewModel> TilesetRecordVMList { get; set; } = new List<TilesetRecordViewModel>();
    #endregion

    /// <summary>
    ///     選択タイルセット
    /// </summary>
    TilesetRecordViewModel? SelectedTilesetRecord { get; set; }

    // - プライベート・フィールド

    GridItemsLayout itemsLayout;

    bool isEnabledTileCropButton;
    bool isEnabledRenameFileNameToUUIDButton;
    bool isEnabledTilesetRemoveButton;

    string selectedTilesetFileStemAsStr;
    string selectedTilesetFileExtensionAsStr;
    string selectedTilesetTitleAsStr = string.Empty;
}
