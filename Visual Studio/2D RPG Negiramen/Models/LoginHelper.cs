namespace _2D_RPG_Negiramen.Models
{
    using TheFileEntryDeployments = _2D_RPG_Negiramen.Models.FileEntries.Deployments;

    /// <summary>
    ///     😁 ログイン関連のヘルパー
    /// </summary>
    static class LoginHelper
    {
        /// <summary>
        ///     TODO フォルダー作成
        /// </summary>
        internal static void MakeFolders()
        {
            // アプリケーション・フォルダへ初期設定をコピー
            if (!TheFileEntryDeployments.AppData.AppDataProjectDeployment.MakeFolder())
            {
                // TODO 異常時の処理
                return;
            }

            // Unity の Assets フォルダへ初期設定をコピー
            if (!TheFileEntryDeployments.UnityAssets.ItsDeployment.MakeFolder(App.GetOrLoadProjectConfiguration().UnityAssetsFolderLocation))
            {
                // TODO 異常時の処理
                return;
            }
        }
    }
}
