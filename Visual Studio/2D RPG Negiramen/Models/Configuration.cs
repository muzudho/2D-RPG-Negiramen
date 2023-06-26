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
        internal static Configuration LoadTOML()
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

                UnityAssetsFolderPath unityAssetsFolderPath = default(UnityAssetsFolderPath);
                YourCircleName yourCircleName = default(YourCircleName);
                YourWorkName yourWorkName = default(YourWorkName);

                // TOML
                TomlTable document = Toml.ToModel(configurationText);

                if (document != null)
                {
                    var pathsObj = document["paths"];
                    if (pathsObj != null && pathsObj is TomlTable paths)
                    {
                        // Unity の Assets フォルダ―へのパス
                        var unityAssetsFolderPathObj = paths["unity_assets_folder_path"];
                        if (unityAssetsFolderPathObj != null && unityAssetsFolderPathObj is string unityAssetsFolderPathAsStr)
                        {
                            unityAssetsFolderPath = UnityAssetsFolderPath.FromString(unityAssetsFolderPathAsStr);
                        }
                    }

                    var profileObj = document["profile"];
                    if (profileObj != null && profileObj is TomlTable profile)
                    {
                        // あなたのサークル名
                        var yourCircleNameObj = profile["your_circle_name"];
                        if (yourCircleNameObj != null && yourCircleNameObj is string yourCircleNameAsStr)
                        {
                            yourCircleName = YourCircleName.FromString(yourCircleNameAsStr);
                        }

                        // あなたの作品名
                        var yourWorkNameObj = profile["your_work_name"];
                        if (yourWorkNameObj != null && yourWorkNameObj is string yourWorkNameAsStr)
                        {
                            yourWorkName = YourWorkName.FromString(yourWorkNameAsStr);
                        }
                    }
                }

                return new Configuration(
                    unityAssetsFolderPath,
                    yourCircleName,
                    yourWorkName);
            }
            catch (Exception ex)
            {
                // TODO 例外対応、何したらいい（＾～＾）？
                return null;
            }
        }

        internal static void SaveTOML(Configuration current, ConfigurationDifference difference)
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

            StringBuilder builder = new StringBuilder();

            builder.AppendLine("[paths]");

            if (difference.UnityAssetsFolderPath == null)
            {
                builder.AppendLine($"unity_assets_folder_path = \"{current.UnityAssetsFolderPath.AsStr}\"");
            }
            else
            {
                builder.AppendLine($"unity_assets_folder_path = \"{difference.UnityAssetsFolderPath.AsStr}\"");
            }

            // 上書き
            System.IO.File.WriteAllText(configurationFilePath, builder.ToString());
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

        Configuration(
            UnityAssetsFolderPath unityAssetsFolderPath,
            YourCircleName yourCircleName,
            YourWorkName yourWorkName)
        {

        }
    }
}
