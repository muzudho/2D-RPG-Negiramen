namespace _2D_RPG_Negiramen.Models.FileEntries
{
    using System.IO;

    /// <summary>
    ///     <pre>
    ///         😁 ネギラーメンのワークスペース・フォルダーの内容を確認します
    ///         
    ///             📂 例: C:/Users/むずでょ/Documents/GitHub/2D-RPG-Negiramen/
    ///         👉　└─ 📂 Workspace
    ///     </pre>
    /// </summary>
    internal class NegiramenWorkspaceDeployment

    {
        /// <summary>
        ///     <pre>
        ///         ユニティの Assets フォルダーにファイルをコピーするために、
        ///         ネギラーメンのワークスペース・フォルダーの内容を確認します
        ///         
        ///             📂 例: C:/Users/むずでょ/Documents/GitHub/2D-RPG-Negiramen/
        ///         👉　└─ 📂 Workspace
        ///         　　　　　└─ 📂 For Unity Assets
        ///     </pre>
        /// </summary>
        internal static bool CheckForUnityAssets()
        {
            var workspaceFolder = App.GetOrLoadConfiguration().NegiramenWorkspaceFolder;
            var workspaceInfo = new DirectoryInfo(workspaceFolder.Path.AsStr);

            // 📂 `For Unity Assets` が含まれていれば OK
            DirectoryInfo assetsInfo = null;

            foreach (var dirInfo in workspaceInfo.EnumerateDirectories())
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

            // 📂 `For Unity Assets` フォルダーのチェック
            bool isOk = CheckForUnityAssetsFolder(assetsInfo);

            return isOk;
        }

        /// <summary>
        ///     <pre>
        ///         `For Unity Assets` フォルダーの内容を確認します
        ///         
        ///             📂 例: C:/Users/むずでょ/Documents/GitHub/2D-RPG-Negiramen/
        ///         　　└─ 📂 Workspace
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

            // 📂 `Images` フォルダーのチェック
            bool isOk = CheckImagesFolder(imagesInfo);

            return isOk;
        }

        /// <summary>
        ///     <pre>
        ///         Images フォルダーの内容を確認します
        ///         
        ///             📂 例: C:/Users/むずでょ/Documents/GitHub/2D-RPG-Negiramen/
        ///         　　└─ 📂 Workspace
        ///         　　　　└─ 📂 For Unity Assets
        ///         👉 　　　　└─ 📂 Images
        ///         　　　　　　　　└─ 📂 Tileset
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
                if (dirInfo.Name == "Tileset")
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

            // 📂 `Tileset` フォルダーのチェック
            bool isOk = CheckTilesetFolder(tileSetInfo);

            return isOk;
        }

        /// <summary>
        ///     <pre>
        ///         Tileset フォルダーの内容を確認します
        ///         
        ///             📂 例: C:/Users/むずでょ/Documents/GitHub/2D-RPG-Negiramen/
        ///         　　└─ 📂 Workspace
        ///         　　　　└─ 📂 For Unity Assets
        ///         　　　　　　└─ 📂 Images
        ///         👉 　　　　　　└─ 📂 Tileset
        ///         　　　　　　　　　　└─ 📄 adventure_field.png
        ///     </pre>
        /// </summary>
        /// <param name="tilesetInfo"></param>
        /// <returns></returns>
        static bool CheckTilesetFolder(DirectoryInfo tilesetInfo)
        {
            // 📄 `adventure_field.png` が含まれていれば OK
            FileInfo adventureFieldPngFileInfo = null;

            foreach (var fileInfo in tilesetInfo.EnumerateFiles())
            {
                if (fileInfo.Name == "adventure_field.png")
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
}
