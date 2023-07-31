﻿namespace _2D_RPG_Negiramen.Models.FileEntries.Deployments
{
    using TheFileEntryLocations = _2D_RPG_Negiramen.Models.FileEntries.Locations;

    /// <summary>
    ///     😁 Unity の Assets フォルダを想定したもの
    /// </summary>
    internal class UnityAssetsDeployment
    {
        // - インターナル静的メソッド

        #region メソッド（Unity の Assets フォルダにファイルを送り込みます）
        /// <summary>
        ///     <pre>
        ///         Unity の 📂 `Assets` フォルダにファイルを送り込みます
        ///     
        ///             📂 例: C:/Users/むずでょ/Documents/Unity Projects/Negiramen Practice/
        ///         👉　└─ 📂 Assets
        ///     </pre>
        /// </summary>
        /// <param name="unityAssetsFolder">Unityの 📂 `Assets` フォルダの場所</param>
        /// <returns>完了した</returns>
        internal static bool PushStartupMemberToUnityAssetsFolder(TheFileEntryLocations.UnityAssets.ItsFolder unityAssetsFolder)
        {
            if (!unityAssetsFolder.IsDirectoryExists())
            {
                // TODO Unity の Assets フォルダ―へのパスでなければ失敗
                return false;
            }

            // 無ければ作成
            unityAssetsFolder.YourCircleFolder.CreateThisDirectoryIfItDoesNotExist();
            PushStartupMemberToYourCircleFolder(unityAssetsFolder.YourCircleFolder);

            return true;
        }
        #endregion

        // - プライベート静的メソッド

        #region メソッド（Unity の 📂 `Assets/｛あなたのサークル・フォルダ名｝` フォルダにファイルを送り込みます）
        /// <summary>
        ///     <pre>
        ///          Unity の 📂 `Assets/｛あなたのサークル・フォルダ名｝` フォルダにファイルを送り込みます
        ///     
        ///             📂 例: C:/Users/むずでょ/Documents/Unity Projects/Negiramen Practice/
        ///         　　└─ 📂 Assets
        ///         👉　　　└─ 📂 {あなたのサークル・フォルダ名}
        ///     </pre>
        /// </summary>
        /// <param name="yourCircleFolder">あなたのサークル名フォルダ―へのパス</param>
        static void PushStartupMemberToYourCircleFolder(TheFileEntryLocations.UnityAssets.YourCircleFolder yourCircleFolder)
        {
            // 無ければ作成
            yourCircleFolder.YourWorkFolder.CreateThisDirectoryIfItDoesNotExist();
            PushStartupMemberToYourWorkFolder(yourCircleFolder.YourWorkFolder);
        }
        #endregion

        #region メソッド（Unity の 📂 `Assets/｛あなたのサークル・フォルダ名｝/｛あなたの作品フォルダ名｝` フォルダにファイルを送り込みます）
        /// <summary>
        ///     <pre>
        ///         Unity の 📂 `Assets/｛あなたのサークル・フォルダ名｝/｛あなたの作品フォルダ名｝` フォルダにファイルを送り込みます
        ///     
        ///             📂 例: C:/Users/むずでょ/Documents/Unity Projects/Negiramen Practice/
        ///         　　└─ 📂 Assets
        ///         　　　　└─ 📂 {あなたのサークル・フォルダ名}
        ///         👉　　　　　└─ 📂 {あなたの作品フォルダ名}
        ///     </pre>
        /// </summary>
        /// <param name="yourWorkFolder">あなたの作品フォルダ名フォルダの場所</param>
        static void PushStartupMemberToYourWorkFolder(TheFileEntryLocations.UnityAssets.YourWorkFolder yourWorkFolder)
        {
            // 無ければ作成
            yourWorkFolder.AutoGeneratedFolder.CreateThisDirectoryIfItDoesNotExist();
            PushStartupMemberToAutoGeneratedFolder(yourWorkFolder.AutoGeneratedFolder);
        }
        #endregion

        // - ４階層目

        #region メソッド（Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated` フォルダにファイルを送り込みます）
        /// <summary>
        ///     <pre>
        ///         Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated` フォルダにファイルを送り込みます
        ///     
        ///             📂 例: C:/Users/むずでょ/Documents/Unity Projects/Negiramen Practice/
        ///         　　└─ 📂 Assets
        ///         　　　　└─ 📂 {あなたのサークル・フォルダ名}
        ///         　　　　　　└─ 📂 {あなたの作品フォルダ名}
        ///         👉　　　　　　　└─ 📂 {Auto Generated}
        ///     </pre>
        /// </summary>
        /// <param name="autoGeneratedFolderPath">自動生成フォルダ―へのパス</param>
        static void PushStartupMemberToAutoGeneratedFolder(TheFileEntryLocations.UnityAssets.AutoGeneratedFolder autoGeneratedFolder)
        {
            // データ・フォルダ
            autoGeneratedFolder.DataFolder.CreateThisDirectoryIfItDoesNotExist();
            PushStartupMemberToDataFolder(autoGeneratedFolder.DataFolder);

            // エディター・フォルダ
            autoGeneratedFolder.EditorFolder.CreateThisDirectoryIfItDoesNotExist();

            // 画像フォルダ
            autoGeneratedFolder.ImagesFolder.CreateThisDirectoryIfItDoesNotExist();
            PushStartupMemberToImagesFolder(autoGeneratedFolder.ImagesFolder);

            // 材質フォルダ
            autoGeneratedFolder.MaterialsFolder.CreateThisDirectoryIfItDoesNotExist();

            // 映像フォルダ
            autoGeneratedFolder.MoviesFolder.CreateThisDirectoryIfItDoesNotExist();

            // プレファブ・フォルダ
            autoGeneratedFolder.PrefabsFolder.CreateThisDirectoryIfItDoesNotExist();

            // シーン・フォルダ
            autoGeneratedFolder.ScenesFolder.CreateThisDirectoryIfItDoesNotExist();

            // スクリプト・フォルダ
            autoGeneratedFolder.ScriptsFolder.CreateThisDirectoryIfItDoesNotExist();

            // スクリプティング・オブジェクト・フォルダ
            autoGeneratedFolder.ScriptingObjectsFolder.CreateThisDirectoryIfItDoesNotExist();

            // 音フォルダ
            autoGeneratedFolder.SoundsFolder.CreateThisDirectoryIfItDoesNotExist();

            // システム・フォルダ
            autoGeneratedFolder.SystemFolder.CreateThisDirectoryIfItDoesNotExist();

            // テキスト・フォルダ
            autoGeneratedFolder.TextsFolder.CreateThisDirectoryIfItDoesNotExist();
        }
        #endregion

        // - ５階層目

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
        static void PushStartupMemberToImagesFolder(TheFileEntryLocations.UnityAssets.ImagesFolder imagesFolder)
        {
            // 📂 `Tileset` フォルダ
            imagesFolder.TilesetsFolder.CreateThisDirectoryIfItDoesNotExist();

            CopyTileset(imagesFolder.TilesetsFolder, "86A25699-E391-4D61-85A5-356BA8049881.png");
            CopyTileset(imagesFolder.TilesetsFolder, "E7911DAD-15AC-44F4-A95D-74AB940A19FB.png");
        }
        #endregion

        #region メソッド（タイルセットをコピー）
        /// <summary>
        ///     タイルセットをコピー
        /// </summary>
        /// <param name="fileName">ファイル名</param>
        static void CopyTileset(TheFileEntryLocations.UnityAssets.ImagesTilesetsFolder tilesetFolder, string fileName)
        {
            var configuration = App.GetOrLoadConfiguration();

            // 画像ファイルのコピー
            {
                var source = Path.Combine(configuration.StarterKitFolder.Path.AsStr, "For Unity Assets", "Images", "Tilesets", fileName);
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
                var source = Path.Combine(configuration.StarterKitFolder.Path.AsStr, "For Unity Assets", "Images", "Tilesets", $"{fileStem}.toml");
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

        #region メソッド（Data フォルダにファイルを送り込みます）
        /// <summary>
        ///     <pre>
        ///         Data フォルダにファイルを送り込みます
        ///     
        ///             📂 例: C:/Users/むずでょ/Documents/Unity Projects/Negiramen Practice/
        ///         　　└─ 📂 Assets
        ///         　　　　└─ 📂 {あなたのサークル・フォルダ名}
        ///         　　　　　　└─ 📂 {あなたの作品フォルダ名}
        ///         　　　　　　　　└─ 📂 {Auto Generated}
        ///         👉　　　　　　　　　└─ 📂 Data
        ///     </pre>
        /// </summary>
        /// <param name="dataFolderPath">データ・フォルダ―へのパス</param>
        static void PushStartupMemberToDataFolder(TheFileEntryLocations.UnityAssets.DataFolder dataFolder)
        {
            // CSVN形式ファイル・フォルダへのパス
            dataFolder.CsvFolder.CreateThisDirectoryIfItDoesNotExist();
            PushStartupMemberToDataCSVFolder(dataFolder.CsvFolder);

            // JSON形式ファイル・フォルダへのパス
            dataFolder.JsonFolder.CreateThisDirectoryIfItDoesNotExist();
        }
        #endregion

        // - ６階層目

        #region メソッド（CSV フォルダのメンバーを準備します）
        /// <summary>
        ///     <pre>
        ///         CSV フォルダのメンバーを準備します
        ///     
        ///             📂 例: C:/Users/むずでょ/Documents/Unity Projects/Negiramen Practice/
        ///         　　└─ 📂 Assets
        ///         　　　　└─ 📂 {あなたのサークル・フォルダ名}
        ///         　　　　　　└─ 📂 {あなたの作品フォルダ名}
        ///         　　　　　　　　└─ 📂 {Auto Generated}
        ///         　　　　　　　　　　└─ 📂 Data
        ///         👉　　　　　　　　　　　└─ 📂 CSV
        ///     </pre>
        /// </summary>
        /// <param name="csvFolderPath">ＣＳＶフォルダ―へのパス</param>
        static void PushStartupMemberToDataCSVFolder(TheFileEntryLocations.UnityAssets.DataCsvFolder csvFolder)
        {
            // タイルセット
            csvFolder.TilesetsFolder.CreateThisDirectoryIfItDoesNotExist();
        }
        #endregion
    }
}