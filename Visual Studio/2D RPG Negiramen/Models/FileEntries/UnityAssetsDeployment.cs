﻿namespace _2D_RPG_Negiramen.Models.FileEntries
{
    using _2D_RPG_Negiramen.Models.FileEntries.Locations;
    using _2D_RPG_Negiramen.Models.FileEntries.Locations.UnityAssets;
    using TheLocationOfUnityAssets = _2D_RPG_Negiramen.Models.FileEntries.Locations.UnityAssets;

    /// <summary>
    ///     😁 Unity の Assets フォルダーを想定したもの
    /// </summary>
    internal class UnityAssetsDeployment
    {
        // - インターナル静的メソッド

        #region メソッド（Unity の Assets フォルダーにファイルを送り込みます）
        /// <summary>
        ///     <pre>
        ///         Unity の 📂 `Assets` フォルダーにファイルを送り込みます
        ///     
        ///             📂 例: C:/Users/むずでょ/Documents/Unity Projects/Negiramen Practice/
        ///         👉　└─ 📂 Assets
        ///     </pre>
        /// </summary>
        /// <param name="unityAssetsFolder">Unityの 📂 `Assets` フォルダーの場所</param>
        /// <returns>完了した</returns>
        internal static bool PushStartupMemberToUnityAssetsFolder(TheLocationOfUnityAssets.ItsFolder unityAssetsFolder)
        {
            if (!Directory.Exists(unityAssetsFolder.Path.AsStr))
            {
                // TODO Unity の Assets フォルダ―へのパスでなければ失敗
                return false;
            }

            if (!Directory.Exists(unityAssetsFolder.YourCircleNameFolder.Path.AsStr))
            {
                // 無ければ作成
                Directory.CreateDirectory(unityAssetsFolder.YourCircleNameFolder.Path.AsStr);
            }

            PushStartupMemberToYourCircleNameFolder(unityAssetsFolder.YourCircleNameFolder);

            return true;
        }
        #endregion

        /// <summary>
        ///     <pre>
        ///          Unity の 📂 `Assets/｛あなたのサークル名｝` フォルダーにファイルを送り込みます
        ///     
        ///             📂 例: C:/Users/むずでょ/Documents/Unity Projects/Negiramen Practice/
        ///         　　└─ 📂 Assets
        ///         👉　　　└─ 📂 {Your Circle Name}
        ///     </pre>
        /// </summary>
        /// <param name="yourCircleNameFolderPath">あなたのサークル名フォルダ―へのパス</param>
        static void PushStartupMemberToYourCircleNameFolder(TheLocationOfUnityAssets.YourCircleNameFolder yourCircleNameFolder)
        {
            if (!Directory.Exists(yourCircleNameFolder.YourWorkNameFolder.Path.AsStr))
            {
                // 無ければ作成
                Directory.CreateDirectory(yourCircleNameFolder.YourWorkNameFolder.Path.AsStr);
            }

            PushStartupMemberToYourWorkNameFolder(yourCircleNameFolder.YourWorkNameFolder);
        }

        /// <summary>
        ///     <pre>
        ///         Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝` フォルダーにファイルを送り込みます
        ///     
        ///             📂 例: C:/Users/むずでょ/Documents/Unity Projects/Negiramen Practice/
        ///         　　└─ 📂 Assets
        ///         　　　　└─ 📂 {Your Circle Name}
        ///         👉　　　　　└─ 📂 {Your Work Name}
        ///     </pre>
        /// </summary>
        /// <param name="yourWorkNameFolderPath">あなたの作品名フォルダ―へのパス</param>
        static void PushStartupMemberToYourWorkNameFolder(TheLocationOfUnityAssets.YourWorkNameFolder yourWorkNameFolder)
        {
            if (!Directory.Exists(yourWorkNameFolder.AutoGeneratedFolder.Path.AsStr))
            {
                // 無ければ作成
                Directory.CreateDirectory(yourWorkNameFolder.AutoGeneratedFolder.Path.AsStr);
            }

            PushStartupMemberToAutoGeneratedFolder(yourWorkNameFolder.AutoGeneratedFolder);
        }

        // - ４階層目

        /// <summary>
        ///     <pre>
        ///         Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated` フォルダーにファイルを送り込みます
        ///     
        ///             📂 例: C:/Users/むずでょ/Documents/Unity Projects/Negiramen Practice/
        ///         　　└─ 📂 Assets
        ///         　　　　└─ 📂 {Your Circle Name}
        ///         　　　　　　└─ 📂 {Your Work Name}
        ///         👉　　　　　　　└─ 📂 {Auto Generated}
        ///     </pre>
        /// </summary>
        /// <param name="autoGeneratedFolderPath">自動生成フォルダ―へのパス</param>
        static void PushStartupMemberToAutoGeneratedFolder(UnityAssetsAutoGeneratedFolder autoGeneratedFolder)
        {
            // データ・フォルダー
            var dataFolderPath = Path.Combine(autoGeneratedFolder.Path.AsStr, "Data");
            if (!Directory.Exists(dataFolderPath))
            {
                Directory.CreateDirectory(dataFolderPath);
            }
            PushStartupMemberToDataFolder(dataFolderPath);

            // エディター・フォルダー
            var editorFolderPath = Path.Combine(autoGeneratedFolder.Path.AsStr, "Editor");
            if (!Directory.Exists(editorFolderPath))
            {
                Directory.CreateDirectory(editorFolderPath);
            }

            // 画像フォルダー
            if (!Directory.Exists(autoGeneratedFolder.ImagesFolder.Path.AsStr))
            {
                Directory.CreateDirectory(autoGeneratedFolder.ImagesFolder.Path.AsStr);
            }
            PushStartupMemberToImagesFolder(autoGeneratedFolder.ImagesFolder);

            // 材質フォルダー
            var materialsFolderPath = Path.Combine(autoGeneratedFolder.Path.AsStr, "Materials");
            if (!Directory.Exists(materialsFolderPath))
            {
                Directory.CreateDirectory(materialsFolderPath);
            }

            // 映像フォルダー
            var moviesFolderPath = Path.Combine(autoGeneratedFolder.Path.AsStr, "Movies");
            if (!Directory.Exists(moviesFolderPath))
            {
                Directory.CreateDirectory(moviesFolderPath);
            }

            // プレファブ・フォルダー
            var prefabFolderPath = Path.Combine(autoGeneratedFolder.Path.AsStr, "Prefabs");
            if (!Directory.Exists(prefabFolderPath))
            {
                Directory.CreateDirectory(prefabFolderPath);
            }

            // シーン・フォルダー
            var scenesFolderPath = Path.Combine(autoGeneratedFolder.Path.AsStr, "Scenes");
            if (!Directory.Exists(scenesFolderPath))
            {
                Directory.CreateDirectory(scenesFolderPath);
            }

            // スクリプト・フォルダー
            var scriptsFolderPath = Path.Combine(autoGeneratedFolder.Path.AsStr, "Scripts");
            if (!Directory.Exists(scriptsFolderPath))
            {
                Directory.CreateDirectory(scriptsFolderPath);
            }

            // スクリプティング・オブジェクト・フォルダー
            var scriptingObjectsFolderPath = Path.Combine(autoGeneratedFolder.Path.AsStr, "Scripting Objects");
            if (!Directory.Exists(scriptingObjectsFolderPath))
            {
                Directory.CreateDirectory(scriptingObjectsFolderPath);
            }

            // 音フォルダー
            var soundsFolderPath = Path.Combine(autoGeneratedFolder.Path.AsStr, "Sounds");
            if (!Directory.Exists(soundsFolderPath))
            {
                Directory.CreateDirectory(soundsFolderPath);
            }

            // システム・フォルダー
            var systemFolderPath = Path.Combine(autoGeneratedFolder.Path.AsStr, "System");
            if (!Directory.Exists(systemFolderPath))
            {
                Directory.CreateDirectory(systemFolderPath);
            }

            // テキスト・フォルダー
            var textsFolderPath = Path.Combine(autoGeneratedFolder.Path.AsStr, "Texts");
            if (!Directory.Exists(textsFolderPath))
            {
                Directory.CreateDirectory(textsFolderPath);
            }
        }

        // - ５階層目

        /// <summary>
        ///     <pre>
        ///         Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Images` フォルダーにファイルを送り込みます
        ///     
        ///             📂 例: C:/Users/むずでょ/Documents/Unity Projects/Negiramen Practice/
        ///         　　└─ 📂 Assets
        ///         　　　　└─ 📂 {Your Circle Name}
        ///         　　　　　　└─ 📂 {Your Work Name}
        ///         　　　　　　　　└─ 📂 {Auto Generated}
        ///         👉　　　　　　　　　└─ 📂 Images
        ///     </pre>
        /// </summary>
        /// <param name="imagesFolder">画像フォルダ―の場所</param>
        static void PushStartupMemberToImagesFolder(UnityAssetsImagesFolder imagesFolder)
        {
            // 📂 `Tileset` フォルダー
            if (!Directory.Exists(imagesFolder.TilesetFolder.Path.AsStr))
            {
                Directory.CreateDirectory(imagesFolder.TilesetFolder.Path.AsStr);
            }

            CopyTileset(imagesFolder.TilesetFolder, "adventure_field.png");
            CopyTileset(imagesFolder.TilesetFolder, "map-tileset-format-8x19.png");

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
        static void CopyTileset(UnityAssetsImagesTilesetFolder tilesetFolder, string fileName)
        {
            var configuration = App.GetOrLoadConfiguration();

            // 画像ファイルのコピー
            {
                var source = Path.Combine(configuration.NegiramenWorkspaceFolder.Path.AsStr, "For Unity Assets", "Images", "Tileset", fileName);
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
                var source = Path.Combine(configuration.NegiramenWorkspaceFolder.Path.AsStr, "For Unity Assets", "Images", "Tileset", $"{fileStem}.toml");
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
