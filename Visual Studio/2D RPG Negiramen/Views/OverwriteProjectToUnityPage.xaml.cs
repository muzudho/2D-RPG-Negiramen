namespace _2D_RPG_Negiramen;

using System.IO;

public partial class OverwriteProjectToUnityPage : ContentPage
{
    public OverwriteProjectToUnityPage()
    {
        InitializeComponent();
    }

    async void HomeBtn_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }

    /// <summary>
    /// ［Unityへプロジェクトを上書き］ボタン押下時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    void OverwriteProjectToUnityBtn_Clicked(object sender, EventArgs e)
    {
        // TODO テキスト・ボックスの値を取得
        var assetsFolderPath = UnityAssetsFolderPath.Text;

        if (!Directory.Exists(assetsFolderPath))
        {
            // TODO ディレクトリー・パスでなければ失敗
            return;
        }

        // `Assets/Muzudho/2D RPG Negiramen` ディレクトリーの有無をチェック
        var muzudhoFolderPath = Path.Combine(assetsFolderPath, "Muzudho");

        if (!Directory.Exists(muzudhoFolderPath))
        {
            // 無ければ作成
            Directory.CreateDirectory(muzudhoFolderPath);
        }

        CreateMuzudhoFolderMember(muzudhoFolderPath);

        // TODO Unityへプロジェクトを上書き
    }

    void CreateMuzudhoFolderMember(string muzudhoFolderPath)
    {
        var o2dRPGNegiramenFolderPath = Path.Combine(muzudhoFolderPath, "2D RPG Negiramen");

        if (!Directory.Exists(o2dRPGNegiramenFolderPath))
        {
            // 無ければ作成
            Directory.CreateDirectory(o2dRPGNegiramenFolderPath);
        }

        Create2DRPGNegiramenFolderMember(o2dRPGNegiramenFolderPath);
    }

    void Create2DRPGNegiramenFolderMember(string o2dRPGNegiramenFolderPath)
    {
        // データ・フォルダー
        var dataFolderPath = Path.Combine(o2dRPGNegiramenFolderPath, "Data");
        if (!Directory.Exists(dataFolderPath))
        {
            Directory.CreateDirectory(dataFolderPath);
        }
        CreateDataFolderMember(dataFolderPath);

        // エディター・フォルダー
        var editorFolderPath = Path.Combine(o2dRPGNegiramenFolderPath, "Editor");
        if (!Directory.Exists(editorFolderPath))
        {
            Directory.CreateDirectory(editorFolderPath);
        }
        CreateDataFolderMember(dataFolderPath);

        // 画像フォルダー
        var imagesFolderPath = Path.Combine(o2dRPGNegiramenFolderPath, "Images");
        if (!Directory.Exists(imagesFolderPath))
        {
            Directory.CreateDirectory(imagesFolderPath);
        }

        // 材質フォルダー
        var materialsFolderPath = Path.Combine(o2dRPGNegiramenFolderPath, "Materials");
        if (!Directory.Exists(materialsFolderPath))
        {
            Directory.CreateDirectory(materialsFolderPath);
        }

        // 映像フォルダー
        var moviesFolderPath = Path.Combine(o2dRPGNegiramenFolderPath, "Movies");
        if (!Directory.Exists(moviesFolderPath))
        {
            Directory.CreateDirectory(moviesFolderPath);
        }

        // プレファブ・フォルダー
        var prefabFolderPath = Path.Combine(o2dRPGNegiramenFolderPath, "Prefabs");
        if (!Directory.Exists(prefabFolderPath))
        {
            Directory.CreateDirectory(prefabFolderPath);
        }

        // シーン・フォルダー
        var scenesFolderPath = Path.Combine(o2dRPGNegiramenFolderPath, "Scenes");
        if (!Directory.Exists(scenesFolderPath))
        {
            Directory.CreateDirectory(scenesFolderPath);
        }

        // スクリプト・フォルダー
        var scriptsFolderPath = Path.Combine(o2dRPGNegiramenFolderPath, "Scripts");
        if (!Directory.Exists(scriptsFolderPath))
        {
            Directory.CreateDirectory(scriptsFolderPath);
        }

        // スクリプティング・オブジェクト・フォルダー
        var scriptingObjectsFolderPath = Path.Combine(o2dRPGNegiramenFolderPath, "Scripting Objects");
        if (!Directory.Exists(scriptingObjectsFolderPath))
        {
            Directory.CreateDirectory(scriptingObjectsFolderPath);
        }

        // 音フォルダー
        var soundsFolderPath = Path.Combine(o2dRPGNegiramenFolderPath, "Sounds");
        if (!Directory.Exists(soundsFolderPath))
        {
            Directory.CreateDirectory(soundsFolderPath);
        }

        // システム・フォルダー
        var systemFolderPath = Path.Combine(o2dRPGNegiramenFolderPath, "System");
        if (!Directory.Exists(systemFolderPath))
        {
            Directory.CreateDirectory(systemFolderPath);
        }

        // テキスト・フォルダー
        var textsFolderPath = Path.Combine(o2dRPGNegiramenFolderPath, "Texts");
        if (!Directory.Exists(textsFolderPath))
        {
            Directory.CreateDirectory(textsFolderPath);
        }
    }

    void CreateDataFolderMember(string dataFolderPath)
    {
        // JSON形式ファイル・フォルダー
        var jsonFolderPath = Path.Combine(dataFolderPath, "JSON");
        if (!Directory.Exists(jsonFolderPath))
        {
            Directory.CreateDirectory(jsonFolderPath);
        }
    }
}