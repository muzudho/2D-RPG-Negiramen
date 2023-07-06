﻿namespace _2D_RPG_Negiramen.Models.FileEntries.Locations.Negiramen
{
    /// <summary>
    ///     😁 ユーザー構成ファイルへのパス
    ///     
    ///     <list type="bullet">
    ///         <item>配置場所は、構成ファイルで変更可能。既定ではネギラーメン・ワークスペースの直下に置く想定</item>
    ///     </list>
    /// </summary>
    class UserConfigurationFile : _2D_RPG_Negiramen.Models.FileEntries.Locations.Its

    {
        // - 静的プロパティ

        /// <summary>
        ///     空オブジェクト
        /// </summary>
        internal static UserConfigurationFile Empty { get; } = new UserConfigurationFile();

        // - その他

        /// <summary>
        ///     生成
        /// </summary>
        internal UserConfigurationFile()
            : base()
        {
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal UserConfigurationFile(FileEntryPath path, FileEntryPathSource pathSource)
            : base(path, pathSource)
        {
        }
    }
}
