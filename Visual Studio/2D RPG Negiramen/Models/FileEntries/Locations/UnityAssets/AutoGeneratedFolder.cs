﻿namespace _2D_RPG_Negiramen.Models.FileEntries.Locations.UnityAssets;

using _2D_RPG_Negiramen.Models;
using _2D_RPG_Negiramen.Models.FileEntries.Locations;

/// <summary>
///     😁 Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated` フォルダの場所
///     
///     <list type="bullet">
///         <item>イミュータブル</item>
///         <item><see cref="_2D_RPG_Negiramen.Models.FileEntries.Locations.UnityAssets.ItsFolder"/></item>
///     </list>
/// </summary>
class AutoGeneratedFolder : Its
{
    // - その他

    #region その他（生成　関連）
    /// <summary>
    ///     生成
    /// </summary>
    internal AutoGeneratedFolder(FileEntryPath parentPath)
        : base(pathSource: FileEntryPathSource.FromString(System.IO.Path.Combine(parentPath.AsStr, "Auto Generated")),
               convert: (pathSource) => FileEntryPath.From(pathSource,
                                                           replaceSeparators: true))
    {
    }
    #endregion

    // - インターナル・プロパティ

    #region プロパティ（Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Data` フォルダの場所）
    /// <summary>
    ///     Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Data` フォルダの場所
    /// </summary>
    internal DataFolder DataFolder
    {
        get
        {
            if (dataFolder == null)
            {
                dataFolder = new DataFolder(Path);
            }

            return dataFolder;
        }
    }
    #endregion

    #region プロパティ（Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Editor` フォルダの場所）
    /// <summary>
    ///     Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Editor` フォルダの場所
    /// </summary>
    internal EditorFolder EditorFolder
    {
        get
        {
            if (editorFolder == null)
            {
                editorFolder = new EditorFolder(Path);
            }

            return editorFolder;
        }
    }
    #endregion

    #region プロパティ（Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Images` フォルダの場所）
    /// <summary>
    ///     Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Images` フォルダの場所
    /// </summary>
    internal ImagesFolder ImagesFolder
    {
        get
        {
            if (imagesFolder == null)
            {
                imagesFolder = new ImagesFolder(Path);
            }

            return imagesFolder;
        }
    }
    #endregion

    #region プロパティ（Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Materials` フォルダの場所）
    /// <summary>
    ///     Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Materials` フォルダの場所
    /// </summary>
    internal MaterialsFolder MaterialsFolder
    {
        get
        {
            if (materialsFolder == null)
            {
                materialsFolder = new MaterialsFolder(Path);
            }

            return materialsFolder;
        }
    }
    #endregion

    #region プロパティ（Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Movies` フォルダの場所）
    /// <summary>
    ///     Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Movies` フォルダの場所
    /// </summary>
    internal MoviesFolder MoviesFolder
    {
        get
        {
            if (moviesFolder == null)
            {
                moviesFolder = new MoviesFolder(Path);
            }

            return moviesFolder;
        }
    }
    #endregion

    #region プロパティ（Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Prefabs` フォルダの場所）
    /// <summary>
    ///     Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Prefabs` フォルダの場所
    /// </summary>
    internal PrefabsFolder PrefabsFolder
    {
        get
        {
            if (prefabsFolder == null)
            {
                prefabsFolder = new PrefabsFolder(Path);
            }

            return prefabsFolder;
        }
    }
    #endregion

    #region プロパティ（Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Scenes` フォルダの場所）
    /// <summary>
    ///     Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Scenes` フォルダの場所
    /// </summary>
    internal ScenesFolder ScenesFolder
    {
        get
        {
            if (scenesFolder == null)
            {
                scenesFolder = new ScenesFolder(Path);
            }

            return scenesFolder;
        }
    }
    #endregion

    #region プロパティ（Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Scripts` フォルダの場所）
    /// <summary>
    ///     Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Scripts` フォルダの場所
    /// </summary>
    internal ScriptsFolder ScriptsFolder
    {
        get
        {
            if (scriptsFolder == null)
            {
                scriptsFolder = new ScriptsFolder(Path);
            }

            return scriptsFolder;
        }
    }
    #endregion

    #region プロパティ（Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Scripting Objects` フォルダの場所）
    /// <summary>
    ///     Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Scripting Objects` フォルダの場所
    /// </summary>
    internal ScriptingObjectsFolder ScriptingObjectsFolder
    {
        get
        {
            if (scriptingObjectsFolder == null)
            {
                scriptingObjectsFolder = new ScriptingObjectsFolder(Path);
            }

            return scriptingObjectsFolder;
        }
    }
    #endregion

    #region プロパティ（Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Sounds` フォルダの場所）
    /// <summary>
    ///     Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Sounds` フォルダの場所
    /// </summary>
    internal SoundsFolder SoundsFolder
    {
        get
        {
            if (soundsFolder == null)
            {
                soundsFolder = new SoundsFolder(Path);
            }

            return soundsFolder;
        }
    }
    #endregion

    #region プロパティ（Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/System` フォルダの場所）
    /// <summary>
    ///     Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/System` フォルダの場所
    /// </summary>
    internal SystemFolder SystemFolder
    {
        get
        {
            if (systemFolder == null)
            {
                systemFolder = new SystemFolder(Path);
            }

            return systemFolder;
        }
    }
    #endregion

    #region プロパティ（Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Texts` フォルダの場所）
    /// <summary>
    ///     Unity の 📂 `Assets/｛あなたのサークル名｝/｛あなたの作品名｝/Auto Generated/Texts` フォルダの場所
    /// </summary>
    internal TextsFolder TextsFolder
    {
        get
        {
            if (textsFolder == null)
            {
                textsFolder = new TextsFolder(Path);
            }

            return textsFolder;
        }
    }
    #endregion

    // - プライベート・フィールド

    DataFolder? dataFolder;
    EditorFolder? editorFolder;
    ImagesFolder? imagesFolder;
    MaterialsFolder? materialsFolder;
    MoviesFolder? moviesFolder;
    PrefabsFolder? prefabsFolder;
    ScenesFolder? scenesFolder;
    ScriptsFolder? scriptsFolder;
    ScriptingObjectsFolder? scriptingObjectsFolder;
    SoundsFolder? soundsFolder;
    SystemFolder? systemFolder;
    TextsFolder? textsFolder;
}
