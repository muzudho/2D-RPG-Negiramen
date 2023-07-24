﻿namespace _2D_RPG_Negiramen.Models.FileEntries.Locations.Cache
{
    using _2D_RPG_Negiramen;
    using _2D_RPG_Negiramen.Coding;
    using _2D_RPG_Negiramen.Models;

    /// <summary>
    ///     😁 OSの 📂 キャッシュ・ディレクトリー の `{あたなのサークル名}/{あなたの作品名}/Images` フォルダの場所
    ///     
    ///     <list type="bullet">
    ///         <item>イミュータブル</item>
    ///         <item><see cref="App.Configuration"/></item>
    ///     </list>
    /// </summary>
    /// <example>
    ///     "C:\Users\むずでょ\AppData\Local\Packages\1802ca7b-559d-489e-8a13-f02ac4d27fcc_9zz4h110yvjzm\LocalCache\Doujin Circle Negiramen\Negiramen Quest\Images"
    /// </example>
    internal class ImagesFolder : Its
    {
        // - その他

        #region その他（生成　関連）
        /// <summary>
        ///     生成
        /// </summary>
        internal ImagesFolder()
            : base()
        {
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal ImagesFolder(FileEntryPathSource pathSource, Lazy.Convert<FileEntryPathSource, FileEntryPath> convert)
            : base(pathSource, convert)
        {
        }
        #endregion

        // - インターナル・プロパティ

        #region プロパティ（OSの 📂 キャッシュ・ディレクトリー の `{あたなのサークル名}/{あなたの作品名}/Images/Tileset` フォルダの場所）
        /// <summary>
        ///     OSの 📂 キャッシュ・ディレクトリー の `{あたなのサークル名}/{あなたの作品名}/Images/Tilesets` フォルダの場所
        /// </summary>
        internal ImagesTilesetsFolder TilesetFolder
        {
            get
            {
                if (tilesetsFolder == null)
                {
                    tilesetsFolder = new ImagesTilesetsFolder(
                        pathSource: FileEntryPathSource.FromString(
                            System.IO.Path.Combine(Path.AsStr, "Tilesets")),
                        convert: (pathSource) => FileEntryPath.From(pathSource,
                                                                    replaceSeparators: true));
                }

                return tilesetsFolder;
            }
        }
        #endregion

        #region プロパティ（OSの 📂 キャッシュ・ディレクトリー の `{あたなのサークル名}/{あなたの作品名}/Images/working_tileset.png` ファイルの場所）
        /// <summary>
        ///     OSの 📂 キャッシュ・ディレクトリー の `{あたなのサークル名}/{あなたの作品名}/Images/working_tileset.png` ファイルの場所
        /// </summary>
        internal ImagesWorkingTilesetPng WorkingTilesetPng
        {
            get
            {
                if (workingTilesetPng == null)
                {
                    workingTilesetPng = new ImagesWorkingTilesetPng(
                        pathSource: FileEntryPathSource.FromString(
                            System.IO.Path.Combine(Path.AsStr, "working_tileset.png")),
                        convert: (pathSource) => FileEntryPath.From(pathSource,
                                                                    replaceSeparators: true));
                }

                return workingTilesetPng;
            }
        }
        #endregion

        // - プライベート・フィールド

        ImagesTilesetsFolder? tilesetsFolder;
        ImagesWorkingTilesetPng? workingTilesetPng;
    }
}
