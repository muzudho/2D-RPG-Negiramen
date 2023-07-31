namespace _2D_RPG_Negiramen.ViewModels;

using _2D_RPG_Negiramen.Models;

/// <summary>
///     😁 ［構成］ページ・ビューモデル
/// </summary>
public interface IConfigurationPageViewModel
{
    // - パブリック変更通知プロパティ

    #region 変更通知プロパティ（ネギラーメン 📂 `Starter Kit` フォルダ）
    /// <summary>
    ///     ネギラーメン 📂 `Starter Kit` フォルダへのパス。文字列形式
    /// </summary>
    string NegiramenStarterKitFolderPathAsStr { get; }
    #endregion

    // - パブリック・プロパティ

    #region 変更通知プロパティ（キャッシュ・ディレクトリー）
    /// <summary>
    ///     キャッシュ・ディレクトリー
    /// </summary>
    string CacheDirectoryAsStr { get; }
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
