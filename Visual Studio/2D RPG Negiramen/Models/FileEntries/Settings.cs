namespace _2D_RPG_Negiramen.Models.FileEntries;

using Tomlyn;
using Tomlyn.Model;

/// <summary>
///     😁 設定
///     
///     <list type="bullet">
///         <item>イミュータブル</item>
///     </list>
/// </summary>
class Settings
{
    // - その他

    /// <summary>
    ///     生成
    /// </summary>
    /// <param name="tileMaxSize">タイルの最大サイズ</param>
    internal Settings(
        Geometric.SizeInt tileMaxSize)
    {
        this.TileMaxSize = tileMaxSize;
    }

    // - インターナル静的メソッド

    #region メソッド（TOML形式ファイルの読取）
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
        var difference = new SettingsDifference()
        {
            // 一辺が 2048 ピクセルのキャンバスを想定し、両端に太さが 2px のグリッドの線があって 1px ずつ食み出るから 2px 引いて 2046
            TileMaxSize = new Models.Geometric.SizeInt(new Models.Geometric.WidthInt(2046), new Models.Geometric.HeightInt(2046)),
        };

        try
        {
            // フォルダ名は自動的に与えられているので、これを使う
            string appDataDirAsStr = FileSystem.Current.AppDataDirectory;
            // Example: `C:\Users\むずでょ\AppData\Local\Packages\1802ca7b-559d-489e-8a13-f02ac4d27fcc_9zz4h110yvjzm\LocalState`

            // 読取たいファイルへのパス
            var settingsFilePathAsStr = System.IO.Path.Combine(appDataDirAsStr, "settings.toml");

            // 設定ファイルの読取
            var settingsText = System.IO.File.ReadAllText(settingsFilePathAsStr);

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
                                difference.TileMaxSize = new Models.Geometric.SizeInt(new Models.Geometric.WidthInt(maxWidthAsInt), difference.TileMaxSize.Height);
                            }
                        }

                        // タイルの最大縦幅
                        if (tile.TryGetValue("max_height", out object maxHeightObj))
                        {
                            if (maxHeightObj is int maxHeightAsInt)
                            {
                                difference.TileMaxSize = new Models.Geometric.SizeInt(difference.TileMaxSize.Width, new Models.Geometric.HeightInt(maxHeightAsInt));
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
                difference.TileMaxSize);
        }
    }
    #endregion

    #region メソッド（保存）
    /// <summary>
    ///     保存
    /// </summary>
    /// <param name="current">現在の設定</param>
    /// <param name="difference">現在の設定から更新した差分</param>
    /// <param name="newSettings">差分を反映した設定</param>
    /// <returns>完了した</returns>
    internal static bool SaveTOML(Settings current, SettingsDifference difference, out Settings newSettings)
    {
        // フォルダ名は自動的に与えられているので、これを使う
        string appDataDirAsStr = FileSystem.Current.AppDataDirectory;
        // Example: `C:\Users\むずでょ\AppData\Local\Packages\1802ca7b-559d-489e-8a13-f02ac4d27fcc_9zz4h110yvjzm\LocalState`

        // 保存したいファイルへのパス
        var settingsFilePathAsStr = System.IO.Path.Combine(appDataDirAsStr, "settings.toml");

        var settingsDifference2nd = new SettingsDifference();

        // 差分適用
        settingsDifference2nd.TileMaxSize = difference.TileMaxSize ?? current.TileMaxSize;

        var text = $@"[tile]

# 一辺が 2048 ピクセルのキャンバスを想定し、両端に太さが 2px のグリッドの線があって 1px ずつ食み出るから 2px 引いて 2046
max_width = {settingsDifference2nd.TileMaxSize.Width.AsInt}
max_height = {settingsDifference2nd.TileMaxSize.Height.AsInt}
";

        // 上書き
        System.IO.File.WriteAllText(settingsFilePathAsStr, text);

        // イミュータブル・オブジェクトを生成
        newSettings = new Settings(
            settingsDifference2nd.TileMaxSize);
        return true;
    }
    #endregion

    // - プライベート・プロパティ

    #region プロパティ（タイルの最大サイズ）
    /// <summary>
    ///     タイルの最大サイズ
    /// </summary>
    internal Geometric.SizeInt TileMaxSize { get; }
    #endregion
}
