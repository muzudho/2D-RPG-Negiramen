namespace _2D_RPG_Negiramen.Models.Files
{
    using Tomlyn;
    using Tomlyn.Model;

    /// <summary>
    ///     😁 設定
    /// </summary>
    class Settings
    {
        // - 静的メソッド

        /// <summary>
        ///     <pre>
        ///         TOML形式ファイルの読取
        ///     
        ///         📖　[Tomlyn　＞　Documentation](https://github.com/xoofx/Tomlyn/blob/main/doc/readme.md)
        ///     </pre>
        /// </summary>
        /// <param name="settings">設定</param>
        /// <returns>TOMLテーブルまたはヌル</returns>
        internal static bool LoadTOML(out Settings settings)
        {
            //
            // 既定値
            // ======
            //
            // 全部ヌルなので、既定値を入れていきます
            SettingsBuffer buffer = new SettingsBuffer()
            {
                // 一辺が 2048 ピクセルのキャンバスを想定し、両端に太さが 2px のグリッドの線があって 1px ずつ食み出るから 2px 引いて 2046
                TileMaxSize = new Models.Size(new Models.Width(2046), new Models.Height(2046)),
            };

            try
            {
                // フォルダー名は自動的に与えられているので、これを使う
                string appDataDirAsStr = FileSystem.Current.AppDataDirectory;
                // Example: `C:\Users\むずでょ\AppData\Local\Packages\1802ca7b-559d-489e-8a13-f02ac4d27fcc_9zz4h110yvjzm\LocalState`

                // 読取たいファイルへのパス
                var settingsFilePath = System.IO.Path.Combine(appDataDirAsStr, "settings.toml");

                // 設定ファイルの読取
                var settingsText = System.IO.File.ReadAllText(settingsFilePath);

                // TOML
                TomlTable document = Toml.ToModel(settingsText);

                if (document != null)
                {
                    // [tile]
                    if (document.TryGetValue("tile", out object tileObj))
                    {
                        if (tileObj != null && tileObj is TomlTable tile)
                        {
                            // タイルの最大横幅
                            if (tile.TryGetValue("max_width", out object maxWidthObj))
                            {
                                if (maxWidthObj is int maxWidthAsInt)
                                {
                                    buffer.TileMaxSize = new Models.Size(new Models.Width(maxWidthAsInt), buffer.TileMaxSize.Height);
                                }
                            }

                            // タイルの最大縦幅
                            if (tile.TryGetValue("max_height", out object maxHeightObj))
                            {
                                if (maxHeightObj is int maxHeightAsInt)
                                {
                                    buffer.TileMaxSize = new Models.Size(buffer.TileMaxSize.Width, new Models.Height(maxHeightAsInt));
                                }
                            }
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                // TODO 例外対応、何したらいい（＾～＾）？
                return false;
            }
            finally
            {
                settings = new Settings(
                    buffer.TileMaxSize);
            }
        }

        /// <summary>
        ///     保存
        /// </summary>
        /// <param name="current">現在の設定</param>
        /// <param name="difference">現在の設定から更新した差分</param>
        /// <param name="newSettings">差分を反映した設定</param>
        /// <returns>完了した</returns>
        internal static bool SaveTOML(Settings current, SettingsBuffer difference, out Settings newSettings)
        {
            // フォルダー名は自動的に与えられているので、これを使う
            string appDataDirAsStr = FileSystem.Current.AppDataDirectory;
            // Example: `C:\Users\むずでょ\AppData\Local\Packages\1802ca7b-559d-489e-8a13-f02ac4d27fcc_9zz4h110yvjzm\LocalState`

            // 保存したいファイルへのパス
            var settingsFilePath = System.IO.Path.Combine(appDataDirAsStr, "settings.toml");

            var settingsBuffer = new SettingsBuffer();

            // 差分適用
            settingsBuffer.TileMaxSize = difference.TileMaxSize == null ? current.TileMaxSize : difference.TileMaxSize;

            var text = $@"[tile]

# 一辺が 2048 ピクセルのキャンバスを想定し、両端に太さが 2px のグリッドの線があって 1px ずつ食み出るから 2px 引いて 2046
max_width = {settingsBuffer.TileMaxSize.Width.AsInt}
max_height = {settingsBuffer.TileMaxSize.Height.AsInt}
";

            // 上書き
            System.IO.File.WriteAllText(settingsFilePath, text);

            // イミュータブル・オブジェクトを生成
            newSettings = new Settings(
                settingsBuffer.TileMaxSize);
            return true;
        }

        /// <summary>
        ///     タイルの最大サイズ
        /// </summary>
        internal Models.Size TileMaxSize { get; }

        ///// <summary>
        /////     生成
        ///// </summary>
        //internal Settings() : this(
        //    Models.Size.Empty)
        //{
        //}

        /// <summary>
        ///     生成
        /// </summary>
        /// <param name="tileMaxSize">タイルの最大サイズ</param>
        internal Settings(
            Models.Size tileMaxSize)
        {
            this.TileMaxSize = tileMaxSize;
        }
    }
}
