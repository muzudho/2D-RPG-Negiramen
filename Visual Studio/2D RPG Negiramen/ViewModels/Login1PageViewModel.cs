﻿namespace _2D_RPG_Negiramen.ViewModels;

using _2D_RPG_Negiramen.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
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
        get => LocalizationResourceManager.Instance.CultureInfo.Name;
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

    #region 変更通知プロパティ（あなたのサークル・フォルダ名）
    /// <summary>
    ///     あなたのサークル・フォルダ名
    /// </summary>
    public string YourCircleFolderNameAsStr
    {
        get => this.yourCircleFolderName.AsStr;
        set
        {
            if (this.yourCircleFolderName.AsStr == value)
                return;

            this.yourCircleFolderName = new YourCircleFolderName(value);
            OnPropertyChanged(nameof(YourCircleFolderNameAsStr));

            // 重たい処理
            this.IsExistsProjectIdInList = this.ProjectIdList.Contains(new ProjectId(this.YourCircleFolderName, this.YourWorkFolderName));
            OnPropertyChanged(nameof(IsVisibleOfNextButton));
            OnPropertyChanged(nameof(IsVisibleOfContinueButton));
            OnPropertyChanged(nameof(IsEnabledOfNextButton));
        }
    }
    #endregion

    #region 変更通知プロパティ（あなたの作品フォルダ名）
    /// <summary>
    ///     あなたの作品フォルダ名
    /// </summary>
    public string YourWorkFolderNameAsStr
    {
        get => this.yourWorkFolderName.AsStr;
        set
        {
            if (this.yourWorkFolderName.AsStr == value)
                return;

            this.yourWorkFolderName = new YourWorkFolderName(value);
            OnPropertyChanged(nameof(YourWorkFolderNameAsStr));

            // 重たい処理
            this.IsExistsProjectIdInList = this.ProjectIdList.Contains(new ProjectId(this.YourCircleFolderName, this.YourWorkFolderName));
            OnPropertyChanged(nameof(IsVisibleOfNextButton));
            OnPropertyChanged(nameof(IsVisibleOfContinueButton));
            OnPropertyChanged(nameof(IsEnabledOfNextButton));
        }
    }
    #endregion

    #region 変更通知プロパティ（［文字数］）
    /// <summary>
    ///     ［文字数］
    /// </summary>
    public int NumberOfCharacters => this.yourCircleFolderNameLength + this.yourWorkFolderNameLength;
    #endregion

    #region 変更通知プロパティ（［続きから］ボタン　関連）
    /// <summary>
    ///     ［続きから］ボタンの可視性
    ///     
    ///     <list type="bullet">
    ///         <item>［次へ］ボタンの逆</item>
    ///     </list>
    /// </summary>
    public bool IsVisibleOfContinueButton
    {
        get => !this.IsVisibleOfNextButton;
    }
    #endregion

    #region 変更通知プロパティ（［次へ］ボタン　関連）
    /// <summary>
    ///     ［次へ］ボタンの可視性
    ///     
    ///     <list type="bullet">
    ///         <item>サークル名と作品名が、エントリー・リストに既存なら偽</item>
    ///     </list>
    /// </summary>
    public bool IsVisibleOfNextButton => !this.IsExistsProjectIdInList;

    /// <summary>
    ///     ［次へ］ボタンの活性性
    ///     
    ///     <list type="bullet">
    ///         <item>サークル名と作品名が入力されているか？</item>
    ///     </list>
    /// </summary>
    public bool IsEnabledOfNextButton => this.YourCircleFolderName != YourCircleFolderName.Empty && this.YourWorkFolderName != YourWorkFolderName.Empty;
    #endregion

    #region 変更通知プロパティ（プロジェクトＩｄリスト　関連）
    /// <summary>
    ///     プロジェクトＩｄリスト
    /// </summary>
    public List<ProjectId> ProjectIdList => App.GetOrLoadConfiguration().ProjectIdList;

    /// <summary>
    ///     選択プロジェクトＩｄ
    /// </summary>
    public ProjectId? SelectedProjectId
    {
        get => this.selectedProjectId;
        set
        {
            if (this.selectedProjectId == value)
                return;

            this.selectedProjectId = value;
            OnPropertyChanged(nameof(SelectedProjectId));
        }
    }
    #endregion

    // - パブリック・プロパティ

    #region プロパティ（あなたのサークル・フォルダ名）
    /// <summary>
    ///     あなたのサークル・フォルダ名
    /// </summary>
    public YourCircleFolderName YourCircleFolderName
    {
        get => this.yourCircleFolderName;
        set
        {
            if (this.yourCircleFolderName == value)
                return;

            this.YourCircleFolderNameAsStr = value.AsStr;
        }
    }
    #endregion

    #region プロパティ（あなたの作品フォルダ名）
    /// <summary>
    ///     あなたの作品フォルダ名
    /// </summary>
    public YourWorkFolderName YourWorkFolderName
    {
        get => this.yourWorkFolderName;
        set
        {
            if (this.yourWorkFolderName == value)
                return;

            this.YourWorkFolderNameAsStr = value.AsStr;
        }
    }
    #endregion

    #region プロパティ（［サークル名］の文字数）
    /// <summary>
    ///     ［サークル名］の文字数
    /// </summary>
    public int YourCircleFolderNameLength
    {
        get => this.yourCircleFolderNameLength;
        set
        {
            if (this.yourCircleFolderNameLength == value)
                return;

            this.yourCircleFolderNameLength = value;
            OnPropertyChanged(nameof(NumberOfCharacters));
        }
    }
    #endregion

    #region プロパティ（［作品名］の文字数）
    /// <summary>
    ///     ［作品名］の文字数
    /// </summary>
    public int YourWorkFolderNameLength
    {
        get => this.yourWorkFolderNameLength;
        set
        {
            if (this.yourWorkFolderNameLength == value)
                return;

            this.yourWorkFolderNameLength = value;
            OnPropertyChanged(nameof(NumberOfCharacters));
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

    #region メソッド（ページの再読込）
    /// <summary>
    ///     ページの再読込
    /// </summary>
    public void InvalidatePage()
    {
        OnPropertyChanged(nameof(ProjectIdList));
    }
    #endregion

    // - プライベート・フィールド

    YourCircleFolderName yourCircleFolderName = YourCircleFolderName.Empty;
    YourWorkFolderName yourWorkFolderName = YourWorkFolderName.Empty;

    int yourCircleFolderNameLength;
    int yourWorkFolderNameLength;

    bool isExistsProjectIdInList;

    ProjectId? selectedProjectId;

    // - プライベート・プロパティ

    #region プロパティ（入力したサークル名と作品名は、エントリー・リストに既存か？）
    /// <summary>
    ///     入力したサークル名と作品名は、エントリー・リストに既存か？
    /// </summary>
    bool IsExistsProjectIdInList
    {
        get => this.isExistsProjectIdInList;
        set
        {
            if (this.isExistsProjectIdInList == value)
                return;

            this.isExistsProjectIdInList = value;

            // ボタンの可視性
            OnPropertyChanged(nameof(IsVisibleOfNextButton));
        }
    }
    #endregion
}
