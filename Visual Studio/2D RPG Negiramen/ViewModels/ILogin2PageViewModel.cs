namespace _2D_RPG_Negiramen.ViewModels;

using System.Diagnostics;
using TheFileEntryLocations = _2D_RPG_Negiramen.Models.FileEntries.Locations;

/// <summary>
///     😁 ［ログイン２］ページ・ビューモデル
/// </summary>
internal interface ILogin2PageViewModel
{
    // - パブリック・プロパティ

    #region プロパティ（ネギラーメンの 📂 `Starter Kit` フォルダの場所）
    /// <summary>
    ///     ネギラーメンの 📂 `Starter Kit` フォルダの場所
    /// </summary>
    /// <example>"C:/Users/むずでょ/Documents/GitHub/2D-RPG-Negiramen/Starter Kit"</example>
    TheFileEntryLocations.StarterKit.ItsFolder StarterKitFolderLocation { get; set; }
    #endregion

    #region プロパティ（Unity の 📂 `Assets` フォルダの場所）
    /// <summary>
    ///     Unity の 📂 `Assets` フォルダの場所
    /// </summary>
    /// <example>"C:/Users/むずでょ/Documents/Unity Projects/Negiramen Practice/Assets"</example>
    TheFileEntryLocations.UnityAssets.ItsFolder UnityAssetsFolderLocation { get; set; }
    #endregion

    #region プロパティ（［新しく作る］ボタンの活性性）
    /// <summary>
    ///     <pre>
    ///         ［新しく作る］ボタンの活性性
    ///         
    ///         以下の条件を満たしたとき活性にする
    ///     </pre>
    ///     <list type="bullet">
    ///         <item>スターターキット・フォルダへのパスが入力されており、フォルダーが実在する</item>
    ///         <item>Unity の Assets フォルダへのパスが入力されており、フォルダーが実在する</item>
    ///     </list>
    /// </summary>
    bool IsEnabledOfNewProjectButton { get; }
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
