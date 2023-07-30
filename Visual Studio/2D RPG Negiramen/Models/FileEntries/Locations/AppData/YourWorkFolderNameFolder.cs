﻿namespace _2D_RPG_Negiramen.Models.FileEntries.Locations.AppData
{
    using _2D_RPG_Negiramen;
    using _2D_RPG_Negiramen.Coding;
    using _2D_RPG_Negiramen.Models;

    /// <summary>
    ///     😁 OSの 📂 アプリケーション・ディレクトリー の `{あたなのサークル・フォルダ名}/{あなたの作品フォルダ名}` フォルダ―の場所
    ///     
    ///     <list type="bullet">
    ///         <item>イミュータブル</item>
    ///         <item><see cref="App.Configuration"/></item>
    ///     </list>
    /// </summary>
    /// <example>
    ///     "C:\Users\むずでょ\AppData\Local\Packages\1802ca7b-559d-489e-8a13-f02ac4d27fcc_9zz4h110yvjzm\LocalState\Doujin Circle Negiramen\Negiramen Quest"
    /// </example>
    internal class YourWorkFolderNameFolder : Its
    {
        // - その他

        #region その他（生成　関連）
        /// <summary>
        ///     生成
        /// </summary>
        internal YourWorkFolderNameFolder()
            : base()
        {
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal YourWorkFolderNameFolder(FileEntryPathSource pathSource, Lazy.Convert<FileEntryPathSource, FileEntryPath> convert)
            : base(pathSource, convert)
        {
        }
        #endregion

        // - インターナル・メソッド

        #region メソッド（OSの 📂 アプリケーション・ディレクトリー の 📄 `{あたなのサークル・フォルダ名}/{あなたの作品フォルダ名}/project.toml` ファイルの場所）
        /// <summary>
        ///     OSの 📂 アプリケーション・ディレクトリー の 📄 `{あたなのサークル・フォルダ名}/{あなたの作品フォルダ名}/project.toml` ファイルの場所
        /// </summary>
        internal ProjectToml ProjectToml
        {
            get
            {
                if (this.projectToml == null)
                {
                    this.projectToml = new ProjectToml(
                        pathSource: FileEntryPathSource.FromString(
                            System.IO.Path.Combine(Path.AsStr, $"project.toml")),
                        convert: (pathSource) => FileEntryPath.From(pathSource,
                                                                    replaceSeparators: true));
                }

                return this.projectToml;
            }
        }
        #endregion

        // - プライベート・フィールド

        ProjectToml? projectToml;
    }
}
