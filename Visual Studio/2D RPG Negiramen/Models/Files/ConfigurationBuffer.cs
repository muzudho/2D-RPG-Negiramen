﻿namespace _2D_RPG_Negiramen.Models.Files
{
    /// <summary>
    ///     😁 構成ファイルの差分
    /// </summary>
    internal class ConfigurationBuffer
    {
        /// <summary>
        ///     ネギラーメンのワークスペース・フォルダーへのパス
        /// </summary>
        internal Models.FileSpace.Negiramen.WorkspaceFolderPath NegiramenWorkspaceFolderPath { get; set; }

        /// <summary>
        ///     Unity の Assets フォルダーへのパス
        /// </summary>
        internal Models.FileSpace.UnityAssetsFolderPath UnityAssetsFolderPath { get; set; }

        /// <summary>
        ///     ユーザー構成ファイルへのパス
        /// </summary>
        internal Models.FileSpace.Negiramen.UserConfigurationFilePath UserConfigurationFilePath { get; set; }

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
