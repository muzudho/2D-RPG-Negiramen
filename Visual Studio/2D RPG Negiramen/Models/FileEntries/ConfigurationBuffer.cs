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
        internal Locations.StarterKit.ItsFolder NegiramenStarterKitFolder { get; set; } = Locations.StarterKit.ItsFolder.Empty;

        /// <summary>
        ///     Unity の 📂 `Assets` フォルダへのパス
        /// </summary>
        internal TheLocationOfUnityAssets.ItsFolder UnityAssetsFolder { get; set; } = TheLocationOfUnityAssets.ItsFolder.Empty;

        ///// <summary>
        /////     ユーザー構成ファイルへのパス
        ///// </summary>
        //internal Locations.StarterKit.UserConfigurationFile UserConfigurationFile { get; set; }

        /// <summary>
        ///     あなたのサークル名
        /// </summary>
        internal YourCircleName RememberYourCircleName { get; set; } = YourCircleName.Empty;

        /// <summary>
        ///     あなたの作品名
        /// </summary>
        internal YourWorkName RememberYourWorkName { get; set; } = YourWorkName.Empty;

        #region プロパティ（エントリー・リスト）
        /// <summary>
        ///     エントリー・リスト
        /// </summary>
        internal List<ConfigurationEntry> EntryList { get; set; } = new List<ConfigurationEntry>();
        #endregion
    }
}
