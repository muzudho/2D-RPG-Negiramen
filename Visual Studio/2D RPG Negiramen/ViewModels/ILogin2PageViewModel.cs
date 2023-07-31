namespace _2D_RPG_Negiramen.ViewModels;

using _2D_RPG_Negiramen.Models;
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
    TheFileEntryLocations.StarterKit.ItsFolder StarterKitFolder { get; set; }
    #endregion

    #region プロパティ（Unity の 📂 `Assets` フォルダの場所）
    /// <summary>
    ///     Unity の 📂 `Assets` フォルダの場所
    /// </summary>
    /// <example>"C:/Users/むずでょ/Documents/Unity Projects/Negiramen Practice/Assets"</example>
    TheFileEntryLocations.UnityAssets.ItsFolder UnityAssetsFolder { get; set; }
    #endregion

    #region プロパティ（あなたのサークル・フォルダ名）
    /// <summary>
    ///     あなたのサークル・フォルダ名
    /// </summary>
    YourCircleFolderName YourCircleFolderName { get; set; }
    #endregion

    #region プロパティ（あなたの作品フォルダ名）
    /// <summary>
    ///     あなたの作品フォルダ名
    /// </summary>
    YourWorkFolderName YourWorkFolderName { get; set; }
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
