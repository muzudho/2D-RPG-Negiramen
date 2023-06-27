namespace _2D_RPG_Negiramen.Models
{
    using System.Text;
    using Tomlyn;
    using Tomlyn.Model;

    /// <summary>
    /// 構成
    /// </summary>
    internal class Configuration
    {
        // - 静的メソッド

        /// <summary>
        ///     <pre>
        ///         TOML形式ファイルの読取
        ///     
        ///         📖　[Tomlyn　＞　Documentation](https://github.com/xoofx/Tomlyn/blob/main/doc/readme.md)
        ///     </pre>
        /// </summary>
        /// <returns>TOMLテーブルまたはヌル</returns>
        internal static bool LoadTOML(out Configuration configuration)
        {
            try
            {
                // フォルダー名は自動的に与えられているので、これを使う
                string appDataDirAsStr = FileSystem.Current.AppDataDirectory;
                // Example: `C:\Users\むずでょ\AppData\Local\Packages\1802ca7b-559d-489e-8a13-f02ac4d27fcc_9zz4h110yvjzm\LocalState`

                // 読取たいファイルへのパス
                var configurationFilePath = System.IO.Path.Combine(appDataDirAsStr, "configuration.toml");

                // 設定ファイルの読取
                var configurationText = System.IO.File.ReadAllText(configurationFilePath);

                UnityAssetsFolderPath unityAssetsFolderPath = new UnityAssetsFolderPath();
                YourCircleName yourCircleName = new YourCircleName();
                YourWorkName yourWorkName = new YourWorkName();

                // TOML
                TomlTable document = Toml.ToModel(configurationText);

                if (document != null)
                {
                    if (document.TryGetValue("paths", out object pathsObj))
                    {
                        if (pathsObj != null && pathsObj is TomlTable paths)
                        {
                            // Unity の Assets フォルダ―へのパス
                            var unityAssetsFolderPathObj = paths["unity_assets_folder_path"];
                            if (unityAssetsFolderPathObj != null && unityAssetsFolderPathObj is string unityAssetsFolderPathAsStr)
                            {
                                unityAssetsFolderPath = UnityAssetsFolderPath.FromString(unityAssetsFolderPathAsStr);
                            }
                        }
                    }

                    if (document.TryGetValue("profile", out object profileObj))
                    {
                        if (profileObj != null && profileObj is TomlTable profile)
                        {
                            // あなたのサークル名
                            if (profile.TryGetValue("your_circle_name", out object yourCircleNameObj))
                            {
                                if (yourCircleNameObj != null && yourCircleNameObj is string yourCircleNameAsStr)
                                {
                                    yourCircleName = YourCircleName.FromString(yourCircleNameAsStr);
                                }
                            }

                            // あなたの作品名
                            if (profile.TryGetValue("your_work_name", out object yourWorkNameObj))
                            {
                                if (yourWorkNameObj != null && yourWorkNameObj is string yourWorkNameAsStr)
                                {
                                    yourWorkName = YourWorkName.FromString(yourWorkNameAsStr);
                                }
                            }
                        }
                    }
                }

                configuration = new Configuration(
                    unityAssetsFolderPath,
                    yourCircleName,
                    yourWorkName);
                return true;
            }
            catch (Exception ex)
            {
                // TODO 例外対応、何したらいい（＾～＾）？
                configuration = null;
                return false;
            }
        }

        internal static bool SaveTOML(Configuration current, ConfigurationBuffer difference, out Configuration newConfiguration)
        {
            //
            // マルチプラットフォームの MAUI では、
            // パソコンだけではなく、スマホなどのサンドボックス環境などでの使用も想定されている
            // 
            // そのため、設定の保存／読込の操作は最小限のものしかない
            //
            // 📖　[Where to save .Net MAUI user settings](https://stackoverflow.com/questions/70599331/where-to-save-net-maui-user-settings)
            //
            // // getter
            // var value = Preferences.Get("nameOfSetting", "defaultValueForSetting");
            //
            // // setter
            // Preferences.Set("nameOfSetting", value);
            //
            //
            // しかし、2D RPG は　Windows PC で開発すると想定する。
            // そこで、 MAUI の範疇を外れ、Windows 固有のファイル・システムの API を使用することにする
            //
            // 📖　[File system helpers](https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/storage/file-system-helpers?tabs=windows)
            //

            // フォルダー名は自動的に与えられているので、これを使う
            string appDataDirAsStr = FileSystem.Current.AppDataDirectory;
            // Example: `C:\Users\むずでょ\AppData\Local\Packages\1802ca7b-559d-489e-8a13-f02ac4d27fcc_9zz4h110yvjzm\LocalState`

            // 保存したいファイルへのパス
            var configurationFilePath = System.IO.Path.Combine(appDataDirAsStr, "configuration.toml");

            var configurationBuffer = new ConfigurationBuffer();

            // 差分適用
            configurationBuffer.UnityAssetsFolderPath = difference.UnityAssetsFolderPath == null ? current.UnityAssetsFolderPath : difference.UnityAssetsFolderPath;
            configurationBuffer.YourCircleName = difference.YourCircleName == null ? current.YourCircleName : difference.YourCircleName;
            configurationBuffer.YourWorkName = difference.YourWorkName == null ? current.YourWorkName : difference.YourWorkName;

            var text = $@"[paths]

# Unity の Assets フォルダ―へのパス
unity_assets_folder_path = ""{configurationBuffer.UnityAssetsFolderPath.AsStr}""

[profile]

# あなたのサークル名
your_circle_name = ""{configurationBuffer.YourCircleName.AsStr}""

# あなたの作品名
your_work_name = ""{configurationBuffer.YourWorkName.AsStr}""
";

            // 上書き
            System.IO.File.WriteAllText(configurationFilePath, text);

            // イミュータブル・オブジェクトを生成
            newConfiguration = new Configuration(
                configurationBuffer.UnityAssetsFolderPath,
                configurationBuffer.YourCircleName,
                configurationBuffer.YourWorkName);
            return true;
        }

        /// <summary>
        /// Unity の Assets フォルダーへのパス
        /// </summary>
        internal UnityAssetsFolderPath UnityAssetsFolderPath { get; }

        /// <summary>
        /// あなたのサークル名
        /// </summary>
        internal YourCircleName YourCircleName { get; }

        /// <summary>
        /// あなたの作品名
        /// </summary>
        internal YourWorkName YourWorkName { get; }

        /// <summary>
        /// 生成
        /// </summary>
        internal Configuration() : this(
            UnityAssetsFolderPath.Empty,
            YourCircleName.Empty,
            YourWorkName.Empty)
        {
        }

        /// <summary>
        /// 生成
        /// </summary>
        /// <param name="unityAssetsFolderPath">Unity の Assets フォルダーへのパス</param>
        /// <param name="yourCircleName">あなたのサークル名</param>
        /// <param name="yourWorkName">あなたの作品名</param>
        internal Configuration(
            UnityAssetsFolderPath unityAssetsFolderPath,
            YourCircleName yourCircleName,
            YourWorkName yourWorkName)
        {
            this.UnityAssetsFolderPath = unityAssetsFolderPath;
            this.YourCircleName = yourCircleName;
            this.YourWorkName = yourWorkName;
        }

        // - メソッド

        /// <summary>
        /// 📂{Unity の Assets}/{Your Circle Name}/{Your Work Name}/Negiramen フォルダ―は存在するか？
        /// </summary>
        /// <returns>そうだ</returns>
        internal bool ExistsNegiramenFolder()
        {
            var path = System.IO.Path.Combine(
                this.UnityAssetsFolderPath.AsStr,
                this.YourCircleName.AsStr,
                this.YourWorkName.AsStr);

            return System.IO.Directory.Exists(path);
        }
    }
}
