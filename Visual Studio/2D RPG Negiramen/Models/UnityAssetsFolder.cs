namespace _2D_RPG_Negiramen.Models
{
    /// <summary>
    ///     Unity の Assets フォルダーを想定したもの
    /// </summary>
    internal class UnityAssetsFolder
    {
        /// <summary>
        ///     Unity の Assets フォルダーにファイルを送り込みます
        ///     
        ///     <pre>
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

            var yourProductFolderPath = Path.Combine(unityAssetsFolderPath, "Your Folder");

            if (!Directory.Exists(yourProductFolderPath))
            {
                // 無ければ作成
                Directory.CreateDirectory(yourProductFolderPath);
            }

            UnityAssetsFolder.PushStartupMemberToYourProductFolder(yourProductFolderPath);

            return true;
        }

        /// <summary>
        ///     Unity の Assets フォルダーにファイルを送り込みます
        ///     
        ///     <pre>
        ///         　　└─ 📂 Assets
        ///         👉　　　└─ 📂 {Your Circle Name}
        ///     </pre>
        /// </summary>
        /// <param name="yourCircleNameFolderPath">あなたのサークル名フォルダ―へのパス</param>
        static void PushStartupMemberToYourFolder(string yourCircleNameFolderPath)
        {
            var yourProductFolderPath = Path.Combine(yourCircleNameFolderPath, "Your Circle Name");

            if (!Directory.Exists(yourProductFolderPath))
            {
                // 無ければ作成
                Directory.CreateDirectory(yourProductFolderPath);
            }

            UnityAssetsFolder.PushStartupMemberToYourProductFolder(yourProductFolderPath);
        }

        /// <summary>
        ///     Unity の Assets フォルダーにファイルを送り込みます
        ///     
        ///     <pre>
        ///         　　└─ 📂 Assets
        ///         　　　　└─ 📂 {Your Circle Name}
        ///         👉　　　　　└─ 📂 {Your Work Name}
        ///     </pre>
        /// </summary>
        /// <param name="yourWorkNameFolderPath">あなたの作品名フォルダ―へのパス</param>
        static void PushStartupMemberToYourProductFolder(string yourWorkNameFolderPath)
        {
            var negiramenFolderPath = Path.Combine(yourWorkNameFolderPath, "Negiramen");

            if (!Directory.Exists(negiramenFolderPath))
            {
                // 無ければ作成
                Directory.CreateDirectory(negiramenFolderPath);
            }

            UnityAssetsFolder.PushStartupMemberToNegiramenFolder(negiramenFolderPath);
        }

        /// <summary>
        ///     Unity の Assets フォルダーにファイルを送り込みます
        ///     
        ///     <pre>
        ///         　　└─ 📂 Assets
        ///         　　　　└─ 📂 {Your Circle Name}
        ///         　　　　　　└─ 📂 {Your Work Name}
        ///         👉　　　　　　　└─ 📂 Negiramen
        ///     </pre>
        /// </summary>
        /// <param name="negiramenFolderPath">ネギラーメン・フォルダ―へのパス</param>
        static void PushStartupMemberToNegiramenFolder(string negiramenFolderPath)
        {
            // データ・フォルダー
            var dataFolderPath = Path.Combine(negiramenFolderPath, "Data");
            if (!Directory.Exists(dataFolderPath))
            {
                Directory.CreateDirectory(dataFolderPath);
            }
            UnityAssetsFolder.PushStartupMemberToDataFolder(dataFolderPath);

            // エディター・フォルダー
            var editorFolderPath = Path.Combine(negiramenFolderPath, "Editor");
            if (!Directory.Exists(editorFolderPath))
            {
                Directory.CreateDirectory(editorFolderPath);
            }

            // 画像フォルダー
            var imagesFolderPath = Path.Combine(negiramenFolderPath, "Images");
            if (!Directory.Exists(imagesFolderPath))
            {
                Directory.CreateDirectory(imagesFolderPath);
            }

            // 材質フォルダー
            var materialsFolderPath = Path.Combine(negiramenFolderPath, "Materials");
            if (!Directory.Exists(materialsFolderPath))
            {
                Directory.CreateDirectory(materialsFolderPath);
            }

            // 映像フォルダー
            var moviesFolderPath = Path.Combine(negiramenFolderPath, "Movies");
            if (!Directory.Exists(moviesFolderPath))
            {
                Directory.CreateDirectory(moviesFolderPath);
            }

            // プレファブ・フォルダー
            var prefabFolderPath = Path.Combine(negiramenFolderPath, "Prefabs");
            if (!Directory.Exists(prefabFolderPath))
            {
                Directory.CreateDirectory(prefabFolderPath);
            }

            // シーン・フォルダー
            var scenesFolderPath = Path.Combine(negiramenFolderPath, "Scenes");
            if (!Directory.Exists(scenesFolderPath))
            {
                Directory.CreateDirectory(scenesFolderPath);
            }

            // スクリプト・フォルダー
            var scriptsFolderPath = Path.Combine(negiramenFolderPath, "Scripts");
            if (!Directory.Exists(scriptsFolderPath))
            {
                Directory.CreateDirectory(scriptsFolderPath);
            }

            // スクリプティング・オブジェクト・フォルダー
            var scriptingObjectsFolderPath = Path.Combine(negiramenFolderPath, "Scripting Objects");
            if (!Directory.Exists(scriptingObjectsFolderPath))
            {
                Directory.CreateDirectory(scriptingObjectsFolderPath);
            }

            // 音フォルダー
            var soundsFolderPath = Path.Combine(negiramenFolderPath, "Sounds");
            if (!Directory.Exists(soundsFolderPath))
            {
                Directory.CreateDirectory(soundsFolderPath);
            }

            // システム・フォルダー
            var systemFolderPath = Path.Combine(negiramenFolderPath, "System");
            if (!Directory.Exists(systemFolderPath))
            {
                Directory.CreateDirectory(systemFolderPath);
            }

            // テキスト・フォルダー
            var textsFolderPath = Path.Combine(negiramenFolderPath, "Texts");
            if (!Directory.Exists(textsFolderPath))
            {
                Directory.CreateDirectory(textsFolderPath);
            }
        }

        /// <summary>
        ///     Unity の Assets フォルダーにファイルを送り込みます
        ///     
        ///     <pre>
        ///         　　└─ 📂 Assets
        ///         　　　　└─ 📂 {Your Circle Name}
        ///         　　　　　　└─ 📂 {Your Work Name}
        ///         　　　　　　　　└─ 📂 Negiramen
        ///         👉　　　　　　　　　└─ 📂 Data
        ///     </pre>
        /// </summary>
        /// <param name="dataFolderPath">データ・フォルダ―へのパス</param>
        static void PushStartupMemberToDataFolder(string dataFolderPath)
        {
            // JSON形式ファイル・フォルダー
            var jsonFolderPath = Path.Combine(dataFolderPath, "JSON");
            if (!Directory.Exists(jsonFolderPath))
            {
                Directory.CreateDirectory(jsonFolderPath);
            }
        }
    }
}
