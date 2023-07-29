namespace _2D_RPG_Negiramen.Models.FileEntries
{
    using TheLocationOfUnityAssets = _2D_RPG_Negiramen.Models.FileEntries.Locations.UnityAssets;

    /// <summary>
    ///     😁 構成ファイルの差分
    /// </summary>
    internal class ConfigurationBuffer
    {
        /// <summary>
        ///     ネギラーメンの 📂 `Starter Kit` フォルダへのパス
        /// </summary>
        internal Locations.StarterKit.ItsFolder NegiramenStarterKitFolder { get; set; }

        /// <summary>
        ///     Unity の 📂 `Assets` フォルダへのパス
        /// </summary>
        internal TheLocationOfUnityAssets.ItsFolder UnityAssetsFolder { get; set; }

        ///// <summary>
        /////     ユーザー構成ファイルへのパス
        ///// </summary>
        //internal Locations.StarterKit.UserConfigurationFile UserConfigurationFile { get; set; }

        /// <summary>
        ///     あなたのサークル名
        /// </summary>
        internal YourCircleName YourCircleName { get; set; }

        /// <summary>
        ///     あなたの作品名
        /// </summary>
        internal YourWorkName YourWorkName { get; set; }
    }
}
