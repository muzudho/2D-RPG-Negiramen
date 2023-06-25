namespace _2D_RPG_Negiramen.ViewModels
{
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using System.Windows.Input;

    /// <summary>
    /// ［Unityへプロジェクトを上書きする］ページ用のビューモデル
    /// </summary>
    class OverwriteProjectToUnityPageViewModel : ObservableObject
    {
        /// <summary>
        /// Unity の Assets フォルダーへのパス。文字列形式
        /// </summary>
        private Models.UnityAssetsFolderPath _unityAssetsFolderPath;

        /// <summary>
        /// それをするコマンド
        /// </summary>
        public ICommand DoItCommand { get; }

        /// <summary>
        /// 生成
        /// 
        /// <list type="bullet">
        ///     <item>ビュー・モデルのデフォルト・コンストラクターは public 修飾にする必要がある</item>
        /// </list>
        /// </summary>
        public OverwriteProjectToUnityPageViewModel()
        {
            _unityAssetsFolderPath = Models.UnityAssetsFolderPath.FromString(string.Empty);

            DoItCommand = new AsyncRelayCommand(DoIt);
        }

        // - 変更通知プロパティ

        /// <summary>
        /// Unity の Assets フォルダーへのパス。文字列形式
        /// </summary>
        public string UnityAssetsFolderPathAsStr
        {
            get => _unityAssetsFolderPath.AsStr;
            set
            {
                if (_unityAssetsFolderPath.AsStr != value)
                {
                    _unityAssetsFolderPath = Models.UnityAssetsFolderPath.FromString(value);
                    OnPropertyChanged();
                }
            }
        }

        // - コマンド

        /// <summary>
        /// ［それをする］コマンドを実行
        /// </summary>
        /// <returns></returns>
        async Task DoIt()
        {
            await Task.Run(() =>
            {
                // テキスト・ボックスから、Unity エディターの Assets フォルダーへのパスを取得
                var assetsFolderPath = this.UnityAssetsFolderPathAsStr;

                if (!Directory.Exists(assetsFolderPath))
                {
                    // TODO ディレクトリー・パスでなければ失敗
                    return;
                }

                // `Assets/Doujin Circle Grayscale/2D RPG Negiramen` ディレクトリーの有無をチェック
                var doujinCircleGrayscaleFolderPath = Path.Combine(assetsFolderPath, "Doujin Circle Grayscale");

                if (!Directory.Exists(doujinCircleGrayscaleFolderPath))
                {
                    // 無ければ作成
                    Directory.CreateDirectory(doujinCircleGrayscaleFolderPath);
                }

                CreateDoujinCircleGrayscaleFolderMember(doujinCircleGrayscaleFolderPath);

                // TODO Unityへプロジェクトを上書き

                // ここまでくれば成功
                // ==================

                //
                // AppData フォルダー下は、直接操作してはいけない
                //
                // 📖　[C# で AppData 以下にデータを保存するとき](https://teratail.com/questions/65648)
                // 📖　[■「AppData」フォルダとLocalFolder](http://libro.tuyano.com/index3?id=2596003&page=3)

                // TODO Unity エディターの Assets フォルダーへのパスをユーザー・データへ保存
                // 📖　[特殊ディレクトリのパスを取得する](https://dobon.net/vb/dotnet/file/getfolderpath.html)
                var localApplicationDataFolderPath = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

                // Directory.Exists() メソッドは、キャッシュのせいで失敗することがあるかもしれない。 DirectoryInfo を使うことにする。
                //
                // 📖　[存在しないファイルでもFile.Existsがtrueを返すことがある](https://qiita.com/kyamawaki/items/4c8eff5f085b0cf6ffa0)
                //
                DirectoryInfo localApplicationDataFolderPathInfo = new DirectoryInfo(localApplicationDataFolderPath);

                var localDoujinCircleGrayscaleFolderPath = System.IO.Path.Combine(localApplicationDataFolderPath, "Doujin Circle Grayscale");

                // ユーザー・データ・フォルダーの下なので、パーミッションを設定しておく
                // 対応してなかった
                // Directory.CreateDirectory(localDoujinCircleGrayscaleFolderPath, UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.GroupRead | UnixFileMode.GroupWrite | UnixFileMode.OtherRead | UnixFileMode.OtherWrite);

                bool isExists = false;
                foreach (var member in localApplicationDataFolderPathInfo.GetDirectories())
                {
                    System.Diagnostics.Trace.WriteLine($"member.Name: {member.Name}");
                    if (member.Name == "Doujin Circle Grayscale")
                    {
                        isExists = true;

                        DirectoryInfo localDoujinCircleGrayscaleFolderPathInfo = new DirectoryInfo(localDoujinCircleGrayscaleFolderPath);
                    }
                }

                if (!isExists)
                {
                    // 無ければ作成
                    Directory.CreateDirectory(localDoujinCircleGrayscaleFolderPath, UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.GroupRead | UnixFileMode.GroupWrite | UnixFileMode.OtherRead | UnixFileMode.OtherWrite);
                }

                //DirectoryInfo localDoujinCircleGrayscaleFolderPathInfo = new DirectoryInfo(localDoujinCircleGrayscaleFolderPath);
                //if (!localDoujinCircleGrayscaleFolderPathInfo.Exists)
                //{
                //    // 無ければ作成
                //    Directory.CreateDirectory(localDoujinCircleGrayscaleFolderPath);
                //}

                var local2DRPGNegiramenFolderPath = System.IO.Path.Combine(localDoujinCircleGrayscaleFolderPath, "2D RPG Negiramen");
                DirectoryInfo local2DRPGNegiramenFolderPathInfo = new DirectoryInfo(local2DRPGNegiramenFolderPath);
                if (!local2DRPGNegiramenFolderPathInfo.Exists)
                {
                    // 無ければ作成
                    Directory.CreateDirectory(local2DRPGNegiramenFolderPath);
                }

                var configurationFilePath = System.IO.Path.Combine(local2DRPGNegiramenFolderPath, "configuration.toml");

                // 設定ファイルの保存
                System.IO.File.WriteAllText(configurationFilePath, $@"[Paths]
unity_assets_folder_path = ""{assetsFolderPath}""");

            });
        }

        // - メソッド

        void CreateDoujinCircleGrayscaleFolderMember(string doujinCircleGrayscaleFolderPath)
        {
            var o2dRPGNegiramenFolderPath = Path.Combine(doujinCircleGrayscaleFolderPath, "2D RPG Negiramen");

            if (!Directory.Exists(o2dRPGNegiramenFolderPath))
            {
                // 無ければ作成
                Directory.CreateDirectory(o2dRPGNegiramenFolderPath);
            }

            Create2DRPGNegiramenFolderMember(o2dRPGNegiramenFolderPath);
        }

        void Create2DRPGNegiramenFolderMember(string o2dRPGNegiramenFolderPath)
        {
            // データ・フォルダー
            var dataFolderPath = Path.Combine(o2dRPGNegiramenFolderPath, "Data");
            if (!Directory.Exists(dataFolderPath))
            {
                Directory.CreateDirectory(dataFolderPath);
            }
            CreateDataFolderMember(dataFolderPath);

            // エディター・フォルダー
            var editorFolderPath = Path.Combine(o2dRPGNegiramenFolderPath, "Editor");
            if (!Directory.Exists(editorFolderPath))
            {
                Directory.CreateDirectory(editorFolderPath);
            }
            CreateDataFolderMember(dataFolderPath);

            // 画像フォルダー
            var imagesFolderPath = Path.Combine(o2dRPGNegiramenFolderPath, "Images");
            if (!Directory.Exists(imagesFolderPath))
            {
                Directory.CreateDirectory(imagesFolderPath);
            }

            // 材質フォルダー
            var materialsFolderPath = Path.Combine(o2dRPGNegiramenFolderPath, "Materials");
            if (!Directory.Exists(materialsFolderPath))
            {
                Directory.CreateDirectory(materialsFolderPath);
            }

            // 映像フォルダー
            var moviesFolderPath = Path.Combine(o2dRPGNegiramenFolderPath, "Movies");
            if (!Directory.Exists(moviesFolderPath))
            {
                Directory.CreateDirectory(moviesFolderPath);
            }

            // プレファブ・フォルダー
            var prefabFolderPath = Path.Combine(o2dRPGNegiramenFolderPath, "Prefabs");
            if (!Directory.Exists(prefabFolderPath))
            {
                Directory.CreateDirectory(prefabFolderPath);
            }

            // シーン・フォルダー
            var scenesFolderPath = Path.Combine(o2dRPGNegiramenFolderPath, "Scenes");
            if (!Directory.Exists(scenesFolderPath))
            {
                Directory.CreateDirectory(scenesFolderPath);
            }

            // スクリプト・フォルダー
            var scriptsFolderPath = Path.Combine(o2dRPGNegiramenFolderPath, "Scripts");
            if (!Directory.Exists(scriptsFolderPath))
            {
                Directory.CreateDirectory(scriptsFolderPath);
            }

            // スクリプティング・オブジェクト・フォルダー
            var scriptingObjectsFolderPath = Path.Combine(o2dRPGNegiramenFolderPath, "Scripting Objects");
            if (!Directory.Exists(scriptingObjectsFolderPath))
            {
                Directory.CreateDirectory(scriptingObjectsFolderPath);
            }

            // 音フォルダー
            var soundsFolderPath = Path.Combine(o2dRPGNegiramenFolderPath, "Sounds");
            if (!Directory.Exists(soundsFolderPath))
            {
                Directory.CreateDirectory(soundsFolderPath);
            }

            // システム・フォルダー
            var systemFolderPath = Path.Combine(o2dRPGNegiramenFolderPath, "System");
            if (!Directory.Exists(systemFolderPath))
            {
                Directory.CreateDirectory(systemFolderPath);
            }

            // テキスト・フォルダー
            var textsFolderPath = Path.Combine(o2dRPGNegiramenFolderPath, "Texts");
            if (!Directory.Exists(textsFolderPath))
            {
                Directory.CreateDirectory(textsFolderPath);
            }
        }

        void CreateDataFolderMember(string dataFolderPath)
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
