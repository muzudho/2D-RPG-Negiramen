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
    #endregion

    #region プロパティ（［サークル名］の文字数）
    /// <summary>
    ///     ［サークル名］の文字数
    /// </summary>
    int YourCircleNameLength { get; set; }
    #endregion

    #region プロパティ（［作品名］の文字数）
    /// <summary>
    ///     ［作品名］の文字数
    /// </summary>
    int YourWorkNameLength { get; set; }
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
    bool IsVisibleOfContinueButton { get; set; }
    #endregion

    #region プロパティ（［次へ］ボタンの可視性）
    /// <summary>
    ///     ［次へ］ボタンの可視性
    /// </summary>
    bool IsVisibleOfNextButton { get; set; }
    #endregion

    #region プロパティ（［次へ］ボタンの活性性）
    /// <summary>
    ///     ［次へ］ボタンの活性性
    /// </summary>
    bool IsEnabledOfNextButton { get; set; }
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
