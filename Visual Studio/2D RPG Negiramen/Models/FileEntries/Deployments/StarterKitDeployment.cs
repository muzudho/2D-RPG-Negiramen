namespace _2D_RPG_Negiramen.Models.FileEntries.Deployments;

using System.IO;

/// <summary>
///     <pre>
///         😁 ネギラーメンの 📂 `Starter Kit` フォルダの内容を確認します
///         
///             📂 例: C:/Users/むずでょ/Documents/GitHub/2D-RPG-Negiramen/
///         👉　└─ 📂 Starter Kit
///     </pre>
/// </summary>
internal class StarterKitDeployment

{
    /// <summary>
    ///     <pre>
    ///         ユニティの Assets フォルダにファイルをコピーするために、
    ///         ネギラーメンのワークスペース・フォルダの内容を確認します
    ///         
    ///             📂 例: C:/Users/むずでょ/Documents/GitHub/2D-RPG-Negiramen/
    ///         👉　└─ 📂 Starter Kit
    ///         　　　　　└─ 📂 For Unity Assets
    ///     </pre>
    /// </summary>
    internal static bool CheckForUnityAssets()
    {
        var starterKitFolder = App.GetOrLoadConfiguration().StarterKitFolder;
        var starterKitInfo = new DirectoryInfo(starterKitFolder.Path.AsStr);

        // 📂 `For Unity Assets` が含まれていれば OK
        DirectoryInfo assetsInfo = null;

        foreach (var dirInfo in starterKitInfo.EnumerateDirectories())
        {
            if (dirInfo.Name == "For Unity Assets")
            {
                assetsInfo = dirInfo;
                break;
            }
        }

        if (assetsInfo == null)
        {
            // TODO エラー処理
            return false;
        }

        // 📂 `For Unity Assets` フォルダのチェック
        bool isOk = CheckForUnityAssetsFolder(assetsInfo);

        return isOk;
    }

    /// <summary>
    ///     <pre>
    ///         `For Unity Assets` フォルダの内容を確認します
    ///         
    ///             📂 例: C:/Users/むずでょ/Documents/GitHub/2D-RPG-Negiramen/
    ///         　　└─ 📂 Starter Kit
    ///         👉 　　　└─ 📂 For Unity Assets
    ///         　　　　　　　└─ 📂 Images
    ///     </pre>
    /// </summary>
    /// <param name="assetsInfo"></param>
    /// <returns></returns>
    static bool CheckForUnityAssetsFolder(DirectoryInfo assetsInfo)
    {
        // 📂 `Images` が含まれていれば OK
        DirectoryInfo imagesInfo = null;

        foreach (var dirInfo in assetsInfo.EnumerateDirectories())
        {
            if (dirInfo.Name == "Images")
            {
                imagesInfo = dirInfo;
                break;
            }
        }

        if (imagesInfo == null)
        {
            // TODO エラー処理
            return false;
        }

        // 📂 `Images` フォルダのチェック
        bool isOk = CheckImagesFolder(imagesInfo);

        return isOk;
    }

    /// <summary>
    ///     <pre>
    ///         Images フォルダの内容を確認します
    ///         
    ///             📂 例: C:/Users/むずでょ/Documents/GitHub/2D-RPG-Negiramen/
    ///         　　└─ 📂 Starter Kit
    ///         　　　　└─ 📂 For Unity Assets
    ///         👉 　　　　└─ 📂 Images
    ///         　　　　　　　　└─ 📂 Tilesets
    ///     </pre>
    /// </summary>
    /// <param name="imagesInfo"></param>
    /// <returns></returns>
    static bool CheckImagesFolder(DirectoryInfo imagesInfo)
    {
        // 📂 `Tileset` が含まれていれば OK
        DirectoryInfo tileSetInfo = null;

        foreach (var dirInfo in imagesInfo.EnumerateDirectories())
        {
            if (dirInfo.Name == "Tilesets")
            {
                tileSetInfo = dirInfo;
                break;
            }
        }

        if (tileSetInfo == null)
        {
            // TODO エラー処理
            return false;
        }

        // 📂 `Tilesets` フォルダのチェック
        bool isOk = CheckTilesetsFolder(tileSetInfo);

        return isOk;
    }

    /// <summary>
    ///     <pre>
    ///         Tileset フォルダの内容を確認します
    ///         
    ///             📂 例: C:/Users/むずでょ/Documents/GitHub/2D-RPG-Negiramen/
    ///         　　└─ 📂 Starter Kit
    ///         　　　　└─ 📂 For Unity Assets
    ///         　　　　　　└─ 📂 Images
    ///         👉 　　　　　　└─ 📂 Tilesets
    ///         　　　　　　　　　　└─ 📄 86A25699-E391-4D61-85A5-356BA8049881.png
    ///     </pre>
    /// </summary>
    /// <param name="tilesetInfo"></param>
    /// <returns></returns>
    static bool CheckTilesetsFolder(DirectoryInfo tilesetInfo)
    {
        // 📄 `86A25699-E391-4D61-85A5-356BA8049881.png` が含まれていれば OK
        FileInfo adventureFieldPngFileInfo = null;

        foreach (var fileInfo in tilesetInfo.EnumerateFiles())
        {
            if (fileInfo.Name == "86A25699-E391-4D61-85A5-356BA8049881.png")
            {
                adventureFieldPngFileInfo = fileInfo;
                break;
            }
        }

        if (adventureFieldPngFileInfo == null)
        {
            // TODO エラー処理
            return false;
        }

        return true;
    }
}
