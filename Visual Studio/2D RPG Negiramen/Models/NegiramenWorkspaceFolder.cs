﻿namespace _2D_RPG_Negiramen.Models
{
    using System.IO;

    /// <summary>
    ///     <pre>
    ///         ネギラーメンのワークスペース・フォルダーの内容を確認します
    ///         
    ///             📂 例: C:/Users/むずでょ/Documents/GitHub/2D-RPG-Negiramen/
    ///         👉　└─ 📂 Workspace
    ///     </pre>
    /// </summary>
    internal class NegiramenWorkspaceFolder
    {
        /// <summary>
        ///     <pre>
        ///         ネギラーメンのワークスペース・フォルダーの内容を確認します
        ///         
        ///             📂 例: C:/Users/むずでょ/Documents/GitHub/2D-RPG-Negiramen/
        ///         👉　└─ 📂 Workspace
        ///         　　　　　└─ 📂 Assets
        ///     </pre>
        /// </summary>
        internal static bool Check()
        {
            var workspacePath = App.GetOrLoadConfiguration().NegiramenWorkspaceFolderPath;

            var workspaceInfo = new DirectoryInfo(workspacePath.AsStr);

            // 📂 `Assets` が含まれていれば OK
            DirectoryInfo assetsInfo = null;

            foreach(var dirInfo in workspaceInfo.EnumerateDirectories())
            {
                if(dirInfo.Name == "Assets")
                {
                    assetsInfo = dirInfo;
                    break;
                }
            }

            if (assetsInfo==null)
            {
                // TODO エラー処理
                return false;
            }

            // 📂 `Assets` フォルダーのチェック
            bool isOk = CheckAssetsFolder(assetsInfo);

            return isOk;
        }

        /// <summary>
        ///     <pre>
        ///         Assets フォルダーの内容を確認します
        ///         
        ///             📂 例: C:/Users/むずでょ/Documents/GitHub/2D-RPG-Negiramen/
        ///         　　└─ 📂 Workspace
        ///         👉 　　　└─ 📂 Assets
        ///         　　　　　　　└─ 📂 Images
        ///     </pre>
        /// </summary>
        /// <param name="assetsInfo"></param>
        /// <returns></returns>
        static bool CheckAssetsFolder(DirectoryInfo assetsInfo)
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
        ///         　　　　└─ 📂 Assets
        ///         👉 　　　　└─ 📂 Images
        ///         　　　　　　　　└─ 📂 Tile Set
        ///     </pre>
        /// </summary>
        /// <param name="imagesInfo"></param>
        /// <returns></returns>
        static bool CheckImagesFolder(DirectoryInfo imagesInfo)
        {
            // 📂 `Tile Set` が含まれていれば OK
            DirectoryInfo tileSetInfo = null;

            foreach (var dirInfo in imagesInfo.EnumerateDirectories())
            {
                if (dirInfo.Name == "Tile Set")
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

            // 📂 `Tile Set` フォルダーのチェック
            bool isOk = CheckTileSetFolder(tileSetInfo);

            return isOk;
        }

        /// <summary>
        ///     <pre>
        ///         Tile Set フォルダーの内容を確認します
        ///         
        ///             📂 例: C:/Users/むずでょ/Documents/GitHub/2D-RPG-Negiramen/
        ///         　　└─ 📂 Workspace
        ///         　　　　└─ 📂 Assets
        ///         　　　　　　└─ 📂 Images
        ///         👉 　　　　　　└─ 📂 Tile Set
        ///         　　　　　　　　　　└─ 📄 adventure_field.png
        ///     </pre>
        /// </summary>
        /// <param name="tileSetInfo"></param>
        /// <returns></returns>
        static bool CheckTileSetFolder(DirectoryInfo tileSetInfo)
        {
            // 📄 `adventure_field.png` が含まれていれば OK
            FileInfo adventureFieldPngFileInfo = null;

            foreach (var fileInfo in tileSetInfo.EnumerateFiles())
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