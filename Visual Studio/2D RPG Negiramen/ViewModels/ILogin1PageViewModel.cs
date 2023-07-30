using _2D_RPG_Negiramen.Models;

namespace _2D_RPG_Negiramen.ViewModels;

/// <summary>
///     😁 ［ログイン１］ページ・ビューモデル
/// </summary>
internal interface ILogin1PageViewModel
{
    // - パブリック・プロパティ

    #region プロパティ（あなたのサークル・フォルダ名　関連）
    /// <summary>
    ///     あなたのサークル・フォルダ名
    /// </summary>
    YourCircleFolderName YourCircleFolderName { get; set; }

    /// <summary>
    ///     あなたのサークル・フォルダ名
    /// </summary>
    string YourCircleFolderNameAsStr { get; set; }

    /// <summary>
    ///     ［あなたのサークル・フォルダ名］の文字数
    /// </summary>
    int YourCircleFolderNameLength { get; set; }
    #endregion

    #region プロパティ（あなたの作品フォルダ名　関連）
    /// <summary>
    ///     あなたの作品フォルダ名
    /// </summary>
    YourWorkFolderName YourWorkFolderName { get; set; }

    /// <summary>
    ///     あなたの作品フォルダ名
    /// </summary>
    string YourWorkFolderNameAsStr { get; set; }

    /// <summary>
    ///     ［あなたの作品フォルダ名］の文字数
    /// </summary>
    int YourWorkFolderNameLength { get; set; }
    #endregion

    #region 変更通知プロパティ（エントリー・リスト　関連）
    /// <summary>
    ///     選択エントリー
    /// </summary>
    public ConfigurationEntry? SelectedEntry { get; set; }
    #endregion

    #region プロパティ（［文字数］）
    /// <summary>
    ///     ［文字数］
    /// </summary>
    int NumberOfCharacters { get; }
    #endregion

    #region プロパティ（［続きから］ボタンの可視性）
    /// <summary>
    ///     ［続きから］ボタンの可視性
    /// </summary>
    bool IsVisibleOfContinueButton { get; }
    #endregion

    #region プロパティ（［次へ］ボタンの可視性）
    /// <summary>
    ///     ［次へ］ボタンの可視性
    /// </summary>
    bool IsVisibleOfNextButton { get; }
    #endregion

    #region プロパティ（［次へ］ボタンの活性性）
    /// <summary>
    ///     ［次へ］ボタンの活性性
    /// </summary>
    bool IsEnabledOfNextButton { get; }
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
    void InvalidateLocale();
    #endregion
    
    #region メソッド（ページの再読込）
    /// <summary>
    ///     ページの再読込
    /// </summary>
    void InvalidatePage();
    #endregion
}
