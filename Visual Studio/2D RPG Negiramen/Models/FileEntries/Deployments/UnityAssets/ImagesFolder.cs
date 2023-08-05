namespace _2D_RPG_Negiramen.Models.FileEntries.Deployments.UnityAssets;

using TheFileEntryLocations = Locations;

/// <summary>
///     😁 画像フォルダ
/// </summary>
internal static class ImagesFolder
{
    // - インターナル静的メソッド

    #region メソッド（Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Images` フォルダにファイルを送り込みます）
    /// <summary>
    ///     <pre>
    ///         Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Images` フォルダにファイルを送り込みます
    ///     
    ///             📂 例: C:/Users/むずでょ/Documents/Unity Projects/Negiramen Practice/
    ///         　　└─ 📂 Assets
    ///         　　　　└─ 📂 {あなたのサークル・フォルダ名}
    ///         　　　　　　└─ 📂 {あなたの作品フォルダ名}
    ///         　　　　　　　　└─ 📂 {Auto Generated}
    ///         👉　　　　　　　　　└─ 📂 Images
    ///     </pre>
    /// </summary>
    /// <param name="imagesFolder">画像フォルダ―の場所</param>
    internal static void Make(TheFileEntryLocations.UnityAssets.Images.ItsFolder imagesFolder)
    {
        // 📂 `Tileset` フォルダ
        imagesFolder.TilesetsFolder.CreateThisDirectoryIfItDoesNotExist();

        MakeTilesetsFolder(imagesFolder.TilesetsFolder);
    }
    #endregion

    internal static void MakeTilesetsFolder(TheFileEntryLocations.UnityAssets.Images.TilesetsFolder tilesetsFolder)
    {
        // 📂 `Locales` フォルダ
        tilesetsFolder.LocalesFolder.CreateThisDirectoryIfItDoesNotExist();
        MakeLocalesFolder(tilesetsFolder.LocalesFolder);

        // スターターキットのタイルセットをコピー
        ImagesFolder.CopyStarterKitTileset(tilesetsFolder, "86A25699-E391-4D61-85A5-356BA8049881.png");
        ImagesFolder.CopyStarterKitTileset(tilesetsFolder, "E7911DAD-15AC-44F4-A95D-74AB940A19FB.png");
    }

    internal static void MakeLocalesFolder(TheFileEntryLocations.UnityAssets.Images.LocalesFolder localesFolder)
    {
        // 選択中のロケールのフォルダ
        var selectedLocaleFolder = localesFolder.CreateSelectedLocaleFolder();
        selectedLocaleFolder.CreateThisDirectoryIfItDoesNotExist();
    }

    #region メソッド（タイルセットをコピー）
    /// <summary>
    ///     タイルセットをコピー
    /// </summary>
    /// <param name="fileName">ファイル名</param>
    internal static void CopyStarterKitTileset(TheFileEntryLocations.UnityAssets.Images.TilesetsFolder tilesetFolder, string fileName)
    {
        var projectConfiguration = App.GetOrLoadProjectConfiguration();

        // 画像ファイルのコピー
        {
            var source = Path.Combine(projectConfiguration.StarterKitFolderLocation.ForUnityAssetsFolder.ImagesFolder.TilesetsFolder.Path.AsStr, fileName);
            var destination = Path.Combine(tilesetFolder.Path.AsStr, fileName);

            if (!File.Exists(destination))
            {
                // ファイルを複写
                File.Copy(
                    sourceFileName: source,
                    destFileName: destination);
            }
        }

        // 添付の TOML ファイルのコピー
        {
            var fileStem = Path.GetFileNameWithoutExtension(fileName);
            var source = Path.Combine(projectConfiguration.StarterKitFolderLocation.ForUnityAssetsFolder.ImagesFolder.TilesetsFolder.Path.AsStr, $"{fileStem}.toml");
            var destination = Path.Combine(tilesetFolder.Path.AsStr, $"{fileStem}.toml");

            if (!File.Exists(destination))
            {
                // ファイルを複写
                File.Copy(
                    sourceFileName: source,
                    destFileName: destination);
            }
        }
    }
    #endregion
}
