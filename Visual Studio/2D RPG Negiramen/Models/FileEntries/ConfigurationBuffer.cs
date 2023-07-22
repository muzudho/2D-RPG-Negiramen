namespace _2D_RPG_Negiramen.Models.FileEntries
{
    using TheLocationOfUnityAssets = _2D_RPG_Negiramen.Models.FileEntries.Locations.UnityAssets;

    /// <summary>
    ///     😁 構成ファイルの差分
    /// </summary>
    internal class ConfigurationBuffer
    {
        /// <summary>
        ///     ネギラーメンのワークスペース・フォルダーへのパス
        /// </summary>
        internal Locations.Negiramen.WorkspaceFolder NegiramenWorkspaceFolder { get; set; }

        /// <summary>
        ///     Unity の Assets フォルダーへのパス
        /// </summary>
        internal TheLocationOfUnityAssets.ItsFolder UnityAssetsFolder { get; set; }

        /// <summary>
        ///     ユーザー構成ファイルへのパス
        /// </summary>
        internal Locations.Negiramen.UserConfigurationFile UserConfigurationFile { get; set; }

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
