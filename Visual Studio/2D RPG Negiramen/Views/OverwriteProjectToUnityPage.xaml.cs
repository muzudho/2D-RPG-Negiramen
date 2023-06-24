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
    private void OverwriteProjectToUnityBtn_Clicked(object sender, EventArgs e)
    {
        // TODO テキスト・ボックスの値を取得
        var folderPath = UnityAssetsFolderPath.Text;

        if (!Directory.Exists(folderPath))
        {
            // TODO ディレクトリー・パスでなければ失敗
            return;
        }

        // `Assets/Muzudho/2D RPG Negiramen` ディレクトリーの有無をチェック
        folderPath = Path.Combine(folderPath, "Muzudho");

        if (!Directory.Exists(folderPath))
        {
            // 無ければ作成
            Directory.CreateDirectory(folderPath);
        }

        folderPath = Path.Combine(folderPath, "2D RPG Negiramen");

        if (!Directory.Exists(folderPath))
        {
            // 無ければ作成
            Directory.CreateDirectory(folderPath);
        }

        // TODO Unityへプロジェクトを上書き
    }
}