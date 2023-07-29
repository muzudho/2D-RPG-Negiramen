namespace _2D_RPG_Negiramen.ViewModels;

/// <summary>
///     😁 ［ログイン１］ページ・ビューモデル
/// </summary>
internal interface ILogin1PageViewModel
{
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
