namespace _2D_RPG_Negiramen.Models.FileEntries.Locations.StarterKit;

using _2D_RPG_Negiramen;
using _2D_RPG_Negiramen.Models;

/// <summary>
///     😁 ネギラーメンの 📂 `Starter Kit/For Unity Assets` フォルダ―の場所
///     
///     <list type="bullet">
///         <item>イミュータブル</item>
///         <item><see cref="App.Configuration"/></item>
///     </list>
/// </summary>
/// <example>
///     "C:\Users\むずでょ\Documents\GitHub\2D-RPG-Negiramen\Starter Kit\For Unity Assets"
/// </example>
internal class ForUnityAssetsFolder : Its
{
    // - その他

    #region その他（生成　関連）
    /// <summary>
    ///     生成
    /// </summary>
    internal ForUnityAssetsFolder(FileEntryPath parentPath)
        : base(pathSource: FileEntryPathSource.FromString(System.IO.Path.Combine(parentPath.AsStr, "For Unity Assets")),
               convert: (pathSource) => FileEntryPath.From(pathSource,
                                                           replaceSeparators: true))
    {
    }
    #endregion

    // - インターナル・プロパティ

    #region プロパティ（ネギラーメンの 📂 `Starter Kit/For Unity Assets/Images` フォルダ―の場所）
    /// <summary>
    ///     ネギラーメンの 📂 `Starter Kit/For Unity Assets/Images` フォルダ―の場所
    /// </summary>
    internal ImagesFolder ImagesFolder
    {
        get
        {
            if (imagesFolder == null)
            {
                imagesFolder = new ImagesFolder(Path);
            }

            return imagesFolder;
        }
    }
    #endregion

    // - プライベート・フィールド

    ImagesFolder? imagesFolder;
}
