namespace _2D_RPG_Negiramen.Models.FileEntries.Locations.UnityAssets;

using _2D_RPG_Negiramen;
using _2D_RPG_Negiramen.Models;
using _2D_RPG_Negiramen.Models.FileEntries.Locations;

/// <summary>
///     😁 Unityの 📂 `Assets/{あなたのサークル・フォルダ名}` フォルダの場所
///     
///     <list type="bullet">
///         <item>イミュータブル</item>
///         <item><see cref="_2D_RPG_Negiramen.Models.FileEntries.Locations.UnityAssets.UnityAssets.ItsFolder"/></item>
///     </list>
/// </summary>
internal class YourCircleFolder
    : Its
{
    // - その他

    #region その他（生成　関連）
    /// <summary>
    ///     生成
    /// </summary>
    internal YourCircleFolder(FileEntryPath parentPath)
        : base(pathSource: FileEntryPathSource.FromString(System.IO.Path.Combine(parentPath.AsStr, App.GetOrLoadConfiguration().CurrentYourCircleFolderName.AsStr)),
               convert: (pathSource) => FileEntryPath.From(pathSource,
                                                           replaceSeparators: true))
    {
    }
    #endregion

    // - インターナル・プロパティ

    #region プロパティ（Unity の 📂 `Assets/｛あなたのサークル・フォルダ名｝/｛あなたの作品フォルダ名｝`フォルダの場所）
    /// <summary>
    ///     Unity の 📂 `Assets/｛あなたのサークル・フォルダ名｝/｛あなたの作品フォルダ名｝`フォルダの場所
    /// </summary>
    internal YourWorkFolder YourWorkFolder
    {
        get
        {
            if (yourWorkFolder == null)
            {
                yourWorkFolder = new YourWorkFolder(Path);
            }

            return yourWorkFolder;
        }
    }
    #endregion

    // - プライベート・フィールド

    YourWorkFolder? yourWorkFolder;
}
