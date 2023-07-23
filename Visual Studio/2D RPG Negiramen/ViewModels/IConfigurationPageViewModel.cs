using _2D_RPG_Negiramen.Models;

namespace _2D_RPG_Negiramen.ViewModels;

/// <summary>
///     😁 ［構成］ページ・ビューモデル
/// </summary>
public interface IConfigurationPageViewModel
{
    // - パブリック変更通知プロパティ

    #region 変更通知プロパティ（ネギラーメン・ワークスペース・フォルダー）
    /// <summary>
    ///     ネギラーメン・ワークスペース・フォルダーへのパス。文字列形式
    /// </summary>
    string NegiramenWorkspaceFolderPathAsStr { get; }
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
