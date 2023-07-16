namespace _2D_RPG_Negiramen.ViewModels;

/// <summary>
///     ［メイン・ページ］ビューモデル
/// </summary>
public interface IMainPageViewModel
{
    #region メソッド（画面遷移でこの画面に戻ってきた時）
    /// <summary>
    ///     画面遷移でこの画面に戻ってきた時
    /// </summary>
    public void ReactOnVisited();
    #endregion
}
