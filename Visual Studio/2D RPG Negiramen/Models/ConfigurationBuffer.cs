﻿/// <summary>
///     😁 モデル
/// </summary>
namespace _2D_RPG_Negiramen.Models
{
    /// <summary>
    ///     構成ファイルの差分
    /// </summary>
    internal class ConfigurationBuffer
    {
        /// <summary>
        ///     ネギラーメンのワークスペース・フォルダーへのパス
        /// </summary>
        internal NegiramenWorkspaceFolderPath NegiramenWorkspaceFolderPath { get; set; }

        /// <summary>
        ///     Unity の Assets フォルダーへのパス
        /// </summary>
        internal UnityAssetsFolderPath UnityAssetsFolderPath { get; set; }

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
