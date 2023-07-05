namespace _2D_RPG_Negiramen.Models.FileEntries
{
    /// <summary>
    ///     😁 構成ファイルの差分
    /// </summary>
    internal class ConfigurationBuffer
    {
        /// <summary>
        ///     ネギラーメンのワークスペース・フォルダーへのパス
        /// </summary>

/* プロジェクト '2D RPG Negiramen (net7.0-android)' からのマージされていない変更
前:
        internal Models.FileEntriesLocations.Negiramen.WorkspaceFolderPath NegiramenWorkspaceFolderPath { get; set; }
後:
        internal WorkspaceFolderPath NegiramenWorkspaceFolderPath { get; set; }
*/
        internal Locations.Negiramen.WorkspaceFolder NegiramenWorkspaceFolderPath { get; set; }

        /// <summary>
        ///     Unity の Assets フォルダーへのパス
        /// </summary>

/* プロジェクト '2D RPG Negiramen (net7.0-android)' からのマージされていない変更
前:
        internal Models.FileEntriesLocations.UnityAssetsFolderPath UnityAssetsFolderPath { get; set; }
後:
        internal UnityAssetsFolderPath UnityAssetsFolderPath { get; set; }
*/
        internal Locations.UnityAssetsFolder UnityAssetsFolderPath { get; set; }

        /// <summary>
        ///     ユーザー構成ファイルへのパス
        /// </summary>

/* プロジェクト '2D RPG Negiramen (net7.0-android)' からのマージされていない変更
前:
        internal Models.FileEntriesLocations.Negiramen.UserConfigurationFilePath UserConfigurationFilePath { get; set; }
後:
        internal UserConfigurationFilePath UserConfigurationFilePath { get; set; }
*/
        internal Locations.Negiramen.UserConfiguration UserConfigurationFilePath { get; set; }

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
