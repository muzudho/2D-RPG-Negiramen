namespace _2D_RPG_Negiramen.ViewModels;

/// <summary>
///     😁 ［ログイン１］ページ・ビューモデル
/// </summary>
internal interface ILogin1PageViewModel
{
    // - パブリック・プロパティ

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

    #region 変更通知プロパティ（［続きから］ボタンの可視性）
    /// <summary>
    ///     ［続きから］ボタンの可視性
    /// </summary>
    public bool IsVisibleOfContinueButton { get; set; }
    #endregion

    #region 変更通知プロパティ（［次へ］ボタンの可視性）
    /// <summary>
    ///     ［次へ］ボタンの可視性
    /// </summary>
    public bool IsVisibleOfNextButton { get; set; }
    #endregion

    #region 変更通知プロパティ（［次へ］ボタンの活性性）
    /// <summary>
    ///     ［次へ］ボタンの活性性
    /// </summary>
    public bool IsEnabledOfNextButton { get; set; }
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
