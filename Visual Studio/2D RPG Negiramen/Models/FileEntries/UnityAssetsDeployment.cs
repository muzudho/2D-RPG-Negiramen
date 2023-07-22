﻿namespace _2D_RPG_Negiramen.Models.FileEntries
{
    /// <summary>
    ///     😁 Unity の Assets フォルダーを想定したもの
    /// </summary>
    internal class UnityAssetsDeployment
    {
        /// <summary>
        ///     <pre>
        ///         Unity の Assets フォルダーにファイルを送り込みます
        ///     
        ///             📂 例: C:/Users/むずでょ/Documents/Unity Projects/Negiramen Practice/
        ///         👉　└─ 📂 Assets
        ///     </pre>
        /// </summary>
        /// <param name="unityAssetsFolderPath">Unityのアセット・フォルダーへのパス</param>
        /// <returns>完了した</returns>
        internal static bool PushStartupMemberToUnityAssetsFolder(string unityAssetsFolderPath)
        {

            if (!Directory.Exists(unityAssetsFolderPath))
            {
                // TODO Unity の Assets フォルダ―へのパスでなければ失敗
                return false;
            }

            var yourCircleNameFolderPath = Path.Combine(unityAssetsFolderPath, App.GetOrLoadConfiguration().YourCircleName.AsStr);

            if (!Directory.Exists(yourCircleNameFolderPath))
            {
                // 無ければ作成
                Directory.CreateDirectory(yourCircleNameFolderPath);
            }

            PushStartupMemberToYourCircleNameFolder(yourCircleNameFolderPath);

            return true;
        }

        /// <summary>
        ///     <pre>
        ///         ｛あなたのサークル名｝フォルダーにファイルを送り込みます
        ///     
        ///             📂 例: C:/Users/むずでょ/Documents/Unity Projects/Negiramen Practice/
        ///         　　└─ 📂 Assets
        ///         👉　　　└─ 📂 {Your Circle Name}
        ///     </pre>
        /// </summary>
        /// <param name="yourCircleNameFolderPath">あなたのサークル名フォルダ―へのパス</param>
        static void PushStartupMemberToYourCircleNameFolder(string yourCircleNameFolderPath)
        {
            var yourWorkNameFolderPath = Path.Combine(yourCircleNameFolderPath, App.GetOrLoadConfiguration().YourWorkName.AsStr);

            if (!Directory.Exists(yourWorkNameFolderPath))
            {
                // 無ければ作成
                Directory.CreateDirectory(yourWorkNameFolderPath);
            }

            PushStartupMemberToYourWorkNameFolder(yourWorkNameFolderPath);
        }

        /// <summary>
        ///     <pre>
        ///         ｛あなたの作品名｝フォルダーにファイルを送り込みます
        ///     
        ///             📂 例: C:/Users/むずでょ/Documents/Unity Projects/Negiramen Practice/
        ///         　　└─ 📂 Assets
        ///         　　　　└─ 📂 {Your Circle Name}
        ///         👉　　　　　└─ 📂 {Your Work Name}
        ///     </pre>
        /// </summary>
        /// <param name="yourWorkNameFolderPath">あなたの作品名フォルダ―へのパス</param>
        static void PushStartupMemberToYourWorkNameFolder(string yourWorkNameFolderPath)
        {
            var autoGeneratedFolderPath = Path.Combine(yourWorkNameFolderPath, "Auto Generated");

            if (!Directory.Exists(autoGeneratedFolderPath))
            {
                // 無ければ作成
                Directory.CreateDirectory(autoGeneratedFolderPath);
            }

            PushStartupMemberToAutoGeneratedFolder(autoGeneratedFolderPath);
        }

        // - ４階層目

        /// <summary>
        ///     <pre>
        ///         `Auto Generated` フォルダーにファイルを送り込みます
        ///     
        ///             📂 例: C:/Users/むずでょ/Documents/Unity Projects/Negiramen Practice/
        ///         　　└─ 📂 Assets
        ///         　　　　└─ 📂 {Your Circle Name}
        ///         　　　　　　└─ 📂 {Your Work Name}
        ///         👉　　　　　　　└─ 📂 {Auto Generated}
        ///     </pre>
        /// </summary>
        /// <param name="autoGeneratedFolderPath">自動生成フォルダ―へのパス</param>
        static void PushStartupMemberToAutoGeneratedFolder(string autoGeneratedFolderPath)
        {
            // データ・フォルダー
            var dataFolderPath = Path.Combine(autoGeneratedFolderPath, "Data");
            if (!Directory.Exists(dataFolderPath))
            {
                Directory.CreateDirectory(dataFolderPath);
            }
            PushStartupMemberToDataFolder(dataFolderPath);

            // エディター・フォルダー
            var editorFolderPath = Path.Combine(autoGeneratedFolderPath, "Editor");
            if (!Directory.Exists(editorFolderPath))
            {
                Directory.CreateDirectory(editorFolderPath);
            }

            // 画像フォルダー
            var imagesFolderPath = Path.Combine(autoGeneratedFolderPath, "Images");
            if (!Directory.Exists(imagesFolderPath))
            {
                Directory.CreateDirectory(imagesFolderPath);
            }
            PushStartupMemberToImagesFolder(imagesFolderPath);

            // 材質フォルダー
            var materialsFolderPath = Path.Combine(autoGeneratedFolderPath, "Materials");
            if (!Directory.Exists(materialsFolderPath))
            {
                Directory.CreateDirectory(materialsFolderPath);
            }

            // 映像フォルダー
            var moviesFolderPath = Path.Combine(autoGeneratedFolderPath, "Movies");
            if (!Directory.Exists(moviesFolderPath))
            {
                Directory.CreateDirectory(moviesFolderPath);
            }

            // プレファブ・フォルダー
            var prefabFolderPath = Path.Combine(autoGeneratedFolderPath, "Prefabs");
            if (!Directory.Exists(prefabFolderPath))
            {
                Directory.CreateDirectory(prefabFolderPath);
            }

            // シーン・フォルダー
            var scenesFolderPath = Path.Combine(autoGeneratedFolderPath, "Scenes");
            if (!Directory.Exists(scenesFolderPath))
            {
                Directory.CreateDirectory(scenesFolderPath);
            }

            // スクリプト・フォルダー
            var scriptsFolderPath = Path.Combine(autoGeneratedFolderPath, "Scripts");
            if (!Directory.Exists(scriptsFolderPath))
            {
                Directory.CreateDirectory(scriptsFolderPath);
            }

            // スクリプティング・オブジェクト・フォルダー
            var scriptingObjectsFolderPath = Path.Combine(autoGeneratedFolderPath, "Scripting Objects");
            if (!Directory.Exists(scriptingObjectsFolderPath))
            {
                Directory.CreateDirectory(scriptingObjectsFolderPath);
            }

            // 音フォルダー
            var soundsFolderPath = Path.Combine(autoGeneratedFolderPath, "Sounds");
            if (!Directory.Exists(soundsFolderPath))
            {
                Directory.CreateDirectory(soundsFolderPath);
            }

            // システム・フォルダー
            var systemFolderPath = Path.Combine(autoGeneratedFolderPath, "System");
            if (!Directory.Exists(systemFolderPath))
            {
                Directory.CreateDirectory(systemFolderPath);
            }

            // テキスト・フォルダー
            var textsFolderPath = Path.Combine(autoGeneratedFolderPath, "Texts");
            if (!Directory.Exists(textsFolderPath))
            {
                Directory.CreateDirectory(textsFolderPath);
            }
        }

        // - ５階層目

        /// <summary>
        ///     <pre>
        ///         Images フォルダーにファイルを送り込みます
        ///     
        ///             📂 例: C:/Users/むずでょ/Documents/Unity Projects/Negiramen Practice/
        ///         　　└─ 📂 Assets
        ///         　　　　└─ 📂 {Your Circle Name}
        ///         　　　　　　└─ 📂 {Your Work Name}
        ///         　　　　　　　　└─ 📂 {Auto Generated}
        ///         👉　　　　　　　　　└─ 📂 Images
        ///     </pre>
        /// </summary>
        /// <param name="imagesFolderPath">画像フォルダ―へのパス</param>
        static void PushStartupMemberToImagesFolder(string imagesFolderPath)
        {
            // 📂 `Tileset` フォルダー
            var tileSetFolderPath = Path.Combine(imagesFolderPath, "Tileset");
            if (!Directory.Exists(tileSetFolderPath))
            {
                Directory.CreateDirectory(tileSetFolderPath);
            }

            CopyTileset("adventure_field.png");
            CopyTileset("map-tileset-format-8x19.png");

            //var configuration = App.GetOrLoadConfiguration();
            //var source = Path.Combine(configuration.NegiramenWorkspaceFolderPath.AsStr, "Assets", "Images", "Tileset", "adventure_field.png");
            //var destination = Path.Combine(
            //        configuration.UnityAssetsFolderPath.AsStr,
            //        configuration.YourCircleName.AsStr,
            //        configuration.YourWorkName.AsStr,
            //        "Auto Generated",
            //        "Images",
            //        "Tileset",
            //        "adventure_field.png");

            //if (!File.Exists(destination))
            //{
            //    // 📄 `Tileset` ファイルを複写
            //    File.Copy(
            //        sourceFileName: source,
            //        destFileName: destination);
            //}
        }

        /// <summary>
        ///     タイルセットをコピー
        /// </summary>
        /// <param name="fileName">ファイル名</param>
        static void CopyTileset(string fileName)
        {
            var configuration = App.GetOrLoadConfiguration();

            // 画像ファイルのコピー
            {
                var source = Path.Combine(configuration.NegiramenWorkspaceFolder.Path.AsStr, "For Unity Assets", "Images", "Tileset", fileName);
                var destination = Path.Combine(
                        configuration.UnityAssetsFolder.Path.AsStr,
                        configuration.YourCircleName.AsStr,
                        configuration.YourWorkName.AsStr,
                        "Auto Generated",
                        "Images",
                        "Tileset",
                        fileName);

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
                var source = Path.Combine(configuration.NegiramenWorkspaceFolder.Path.AsStr, "For Unity Assets", "Images", "Tileset", $"{fileStem}.toml");
                var destination = Path.Combine(
                        configuration.UnityAssetsFolder.Path.AsStr,
                        configuration.YourCircleName.AsStr,
                        configuration.YourWorkName.AsStr,
                        "Auto Generated",
                        "Images",
                        "Tileset",
                        $"{fileStem}.toml");

                if (!File.Exists(destination))
                {
                    // ファイルを複写
                    File.Copy(
                        sourceFileName: source,
                        destFileName: destination);
                }
            }
        }

        /// <summary>
        ///     <pre>
        ///         Data フォルダーにファイルを送り込みます
        ///     
        ///             📂 例: C:/Users/むずでょ/Documents/Unity Projects/Negiramen Practice/
        ///         　　└─ 📂 Assets
        ///         　　　　└─ 📂 {Your Circle Name}
        ///         　　　　　　└─ 📂 {Your Work Name}
        ///         　　　　　　　　└─ 📂 {Auto Generated}
        ///         👉　　　　　　　　　└─ 📂 Data
        ///     </pre>
        /// </summary>
        /// <param name="dataFolderPath">データ・フォルダ―へのパス</param>
        static void PushStartupMemberToDataFolder(string dataFolderPath)
        {
            // CSVN形式ファイル・フォルダーへのパス
            var csv = Path.Combine(dataFolderPath, "CSV");
            if (!Directory.Exists(csv))
            {
                Directory.CreateDirectory(csv);
            }
            PushStartupMemberToDataCSVFolder(csv);

            // JSON形式ファイル・フォルダーへのパス
            var json = Path.Combine(dataFolderPath, "JSON");
            if (!Directory.Exists(json))
            {
                Directory.CreateDirectory(json);
            }
        }

        // - ６階層目

        /// <summary>
        ///     <pre>
        ///         CSV フォルダーのメンバーを準備します
        ///     
        ///             📂 例: C:/Users/むずでょ/Documents/Unity Projects/Negiramen Practice/
        ///         　　└─ 📂 Assets
        ///         　　　　└─ 📂 {Your Circle Name}
        ///         　　　　　　└─ 📂 {Your Work Name}
        ///         　　　　　　　　└─ 📂 {Auto Generated}
        ///         　　　　　　　　　　└─ 📂 Data
        ///         👉　　　　　　　　　　　└─ 📂 CSV
        ///     </pre>
        /// </summary>
        /// <param name="csvFolderPath">ＣＳＶフォルダ―へのパス</param>
        static void PushStartupMemberToDataCSVFolder(string csvFolderPath)
        {
            // タイルセット
            var tileSet = Path.Combine(csvFolderPath, "Tileset");
            if (!Directory.Exists(tileSet))
            {
                Directory.CreateDirectory(tileSet);
            }
        }
    }
}
