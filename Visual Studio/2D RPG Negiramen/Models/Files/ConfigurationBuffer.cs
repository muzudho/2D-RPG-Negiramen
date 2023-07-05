namespace _2D_RPG_Negiramen.Models.Files
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
        internal Models.FileSpace.NegiramenWorkspaceFolderPath NegiramenWorkspaceFolderPath { get; set; }
後:
        internal NegiramenWorkspaceFolderPath NegiramenWorkspaceFolderPath { get; set; }
*/
        internal FileSpace.Negiramen.WorkspaceFolderPath NegiramenWorkspaceFolderPath { get; set; }

        /// <summary>
        ///     Unity の Assets フォルダーへのパス
        /// </summary>
        internal Models.FileSpace.UnityAssetsFolderPath UnityAssetsFolderPath { get; set; }

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
