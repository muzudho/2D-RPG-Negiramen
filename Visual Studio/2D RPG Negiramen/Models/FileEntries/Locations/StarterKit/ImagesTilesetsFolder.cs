namespace _2D_RPG_Negiramen.Models.FileEntries.Locations.StarterKit;

using _2D_RPG_Negiramen;
using _2D_RPG_Negiramen.Models;

/// <summary>
///     😁 ネギラーメンの 📂 `Starter Kit/For Unity Assets/Images/Tilesets` フォルダ―の場所
///     
///     <list type="bullet">
///         <item>イミュータブル</item>
///         <item><see cref="App.Configuration"/></item>
///     </list>
/// </summary>
/// <example>
///     "C:\Users\むずでょ\Documents\GitHub\2D-RPG-Negiramen\Starter Kit\For Unity Assets\Images\Tilesets"
/// </example>
internal class ImagesTilesetsFolder : Its
{
    // - その他

    #region その他（生成　関連）
    /// <summary>
    ///     生成
    /// </summary>
    internal ImagesTilesetsFolder()
        : base()
    {
    }

    /// <summary>
    ///     生成
    /// </summary>
    internal ImagesTilesetsFolder(FileEntryPath parentPath)
        : base(pathSource: FileEntryPathSource.FromString(System.IO.Path.Combine(parentPath.AsStr, "Tilesets")),
               convert: (pathSource) => FileEntryPath.From(pathSource,
                                                           replaceSeparators: true))
    {
    }
    #endregion
}
