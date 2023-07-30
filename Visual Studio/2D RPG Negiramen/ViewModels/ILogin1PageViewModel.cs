using _2D_RPG_Negiramen.Models;

namespace _2D_RPG_Negiramen.ViewModels;

/// <summary>
///     😁 ［ログイン１］ページ・ビューモデル
/// </summary>
internal interface ILogin1PageViewModel
{
    // - パブリック・プロパティ

    #region プロパティ（あなたのサークル名　関連）
    /// <summary>
    ///     あなたのサークル名
    /// </summary>
    YourCircleName YourCircleName { get; set; }

    /// <summary>
    ///     あなたのサークル名
    /// </summary>
    string YourCircleNameAsStr { get; set; }

    /// <summary>
    ///     ［あなたのサークル名］の文字数
    /// </summary>
    int YourCircleNameLength { get; set; }
    #endregion

    #region プロパティ（あなたの作品名　関連）
    /// <summary>
    ///     あなたの作品名
    /// </summary>
    YourWorkName YourWorkName { get; set; }

    /// <summary>
    ///     あなたの作品名
    /// </summary>
    string YourWorkNameAsStr { get; set; }

    /// <summary>
    ///     ［あなたの作品名］の文字数
    /// </summary>
    int YourWorkNameLength { get; set; }
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
}
