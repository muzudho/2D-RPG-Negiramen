﻿namespace _2D_RPG_Negiramen.Models.FileEntries.Locations.UnityAssets
{
    using _2D_RPG_Negiramen;
    using _2D_RPG_Negiramen.Coding;
    using _2D_RPG_Negiramen.Models;
    using _2D_RPG_Negiramen.Models.FileEntries.Locations;

    /// <summary>
    ///     😁 Unityの 📂 `Assets/{あなたのサークル・フォルダ名}` フォルダの場所
    ///     
    ///     <list type="bullet">
    ///         <item>イミュータブル</item>
    ///         <item><see cref="_2D_RPG_Negiramen.Models.FileEntries.Locations.UnityAssets.UnityAssets.ItsFolder"/></item>
    ///     </list>
    /// </summary>
    internal class YourCircleFolderNameFolder
        : Its
    {
        // - その他

        #region その他（生成　関連）
        /// <summary>
        ///     生成
        /// </summary>
        internal YourCircleFolderNameFolder()
            : base()
        {
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal YourCircleFolderNameFolder(FileEntryPathSource pathSource, Lazy.Convert<FileEntryPathSource, FileEntryPath> convert)
            : base(pathSource, convert)
        {
        }
        #endregion

        // - インターナル・プロパティ

        #region プロパティ（Unity の 📂 `Assets/｛あなたのサークル・フォルダ名｝/｛あなたの作品フォルダ名｝`フォルダの場所）
        /// <summary>
        ///     Unity の 📂 `Assets/｛あなたのサークル・フォルダ名｝/｛あなたの作品フォルダ名｝`フォルダの場所
        /// </summary>
        internal YourWorkFolderNameFolder YourWorkFolderNameFolder
        {
            get
            {
                if (yourWorkFolderNameFolder == null)
                {
                    yourWorkFolderNameFolder = new YourWorkFolderNameFolder(
                        pathSource: FileEntryPathSource.FromString(
                            System.IO.Path.Combine(Path.AsStr, App.GetOrLoadConfiguration().RememberYourWorkFolderName.AsStr)),
                        convert: (pathSource) => FileEntryPath.From(pathSource,
                                                                    replaceSeparators: true));
                }

                return yourWorkFolderNameFolder;
            }
        }
        #endregion

        // - プライベート・フィールド

        YourWorkFolderNameFolder? yourWorkFolderNameFolder;
    }
}
