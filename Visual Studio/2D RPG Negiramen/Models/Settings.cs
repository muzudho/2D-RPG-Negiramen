namespace _2D_RPG_Negiramen.Models
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
            try
            {
                // フォルダー名は自動的に与えられているので、これを使う
                string appDataDirAsStr = FileSystem.Current.AppDataDirectory;
                // Example: `C:\Users\むずでょ\AppData\Local\Packages\1802ca7b-559d-489e-8a13-f02ac4d27fcc_9zz4h110yvjzm\LocalState`

                // 読取たいファイルへのパス
                var settingsFilePath = System.IO.Path.Combine(appDataDirAsStr, "settings.toml");

                // 設定ファイルの読取
                var settingsText = System.IO.File.ReadAllText(settingsFilePath);

                //
                // 既定値
                // ======
                //

                // 一辺が 2048 ピクセルのキャンバスを想定し、両端に太さが 2px のグリッドの線があって 1px ずつ食み出るから 2px 引いて 2046
                Models.Width tileMaxWidth = new Models.Width(2046);
                Models.Height tileMaxHeight = new Models.Height(2046);

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
                                    tileMaxWidth = new Models.Width(maxWidthAsInt);
                                }
                            }

                            // タイルの最大縦幅
                            if (tile.TryGetValue("max_height", out object maxHeightObj))
                            {
                                if (maxHeightObj is int maxHeightAsInt)
                                {
                                    tileMaxHeight = new Models.Height(maxHeightAsInt);
                                }
                            }
                        }
                    }
                }

                settings = new Settings(
                    tileMaxWidth,
                    tileMaxHeight);
                return true;
            }
            catch (Exception ex)
            {
                // TODO 例外対応、何したらいい（＾～＾）？
                settings = null;
                return false;
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
            settingsBuffer.TileMaxWidth = difference.TileMaxWidth == null ? current.TileMaxWidth : difference.TileMaxWidth;
            settingsBuffer.TileMaxHeight = difference.TileMaxHeight == null ? current.TileMaxHeight : difference.TileMaxHeight;

            var text = $@"[tile]

# 一辺が 2048 ピクセルのキャンバスを想定し、両端に太さが 2px のグリッドの線があって 1px ずつ食み出るから 2px 引いて 2046
max_width = 2046
max_height = 2046
";

            // 上書き
            System.IO.File.WriteAllText(settingsFilePath, text);

            // イミュータブル・オブジェクトを生成
            newSettings = new Settings(
                settingsBuffer.TileMaxWidth,
                settingsBuffer.TileMaxHeight);
            return true;
        }

        /// <summary>
        ///     タイルの最大横幅
        /// </summary>
        internal Models.Width TileMaxWidth { get; }

        /// <summary>
        ///     タイルの最大縦幅
        /// </summary>
        internal Models.Height TileMaxHeight { get; }

        /// <summary>
        ///     生成
        /// </summary>
        internal Settings() : this(
            Models.Width.Empty,
            Models.Height.Empty)
        {
        }

        /// <summary>
        ///     生成
        /// </summary>
        /// <param name="tileMaxWidth">タイルの最大横幅</param>
        /// <param name="tileMaxHeight">タイルの最大縦幅</param>
        internal Settings(
            Models.Width tileMaxWidth,
            Models.Height tileMaxHeight)
        {
            this.TileMaxWidth = tileMaxWidth;
            this.TileMaxHeight = tileMaxHeight;
        }
    }
}
