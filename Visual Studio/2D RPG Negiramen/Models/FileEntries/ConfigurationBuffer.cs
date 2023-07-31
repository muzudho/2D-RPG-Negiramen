namespace _2D_RPG_Negiramen.Models.FileEntries
{
    using TheLocationOfUnityAssets = _2D_RPG_Negiramen.Models.FileEntries.Locations.UnityAssets;

    /// <summary>
    ///     😁 構成ファイルの差分
    ///     
    ///     <list type="bullet">
    ///         <item>ミュータブル</item>
    ///     </list>
    /// </summary>
    internal class ConfigurationBuffer
    {
        /// <summary>
        ///     ネギラーメンの 📂 `Starter Kit` フォルダへのパス
        /// </summary>
        internal Locations.StarterKit.ItsFolder? StarterKitFolder { get; set; }

        /// <summary>
        ///     Unity の 📂 `Assets` フォルダへのパス
        /// </summary>
        internal TheLocationOfUnityAssets.ItsFolder? UnityAssetsFolder { get; set; }

        ///// <summary>
        /////     ユーザー構成ファイルへのパス
        ///// </summary>
        //internal Locations.StarterKit.UserConfigurationFile? UserConfigurationFile { get; set; }

        /// <summary>
        ///     あなたのサークル・フォルダ名
        /// </summary>
        internal YourCircleFolderName? RememberYourCircleFolderName { get; set; }

        /// <summary>
        ///     あなたの作品フォルダ名
        /// </summary>
        internal YourWorkFolderName? RememberYourWorkFolderName { get; set; }

        #region プロパティ（エントリー・リスト）
        /// <summary>
        ///     エントリー・リスト
        /// </summary>
        internal List<ConfigurationEntry>? EntryList { get; set; }
        #endregion
    }
}
