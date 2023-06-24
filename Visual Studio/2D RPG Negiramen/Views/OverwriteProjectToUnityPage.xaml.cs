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
    /// �mUnity�փv���W�F�N�g���㏑���n�{�^��������
    /// </summary>
    /// <param name="sender">���̃C�x���g���Ăяo�����R���g���[��</param>
    /// <param name="e">���̔����C�x���g�̐���ϐ�</param>
    private void OverwriteProjectToUnityBtn_Clicked(object sender, EventArgs e)
    {
        // TODO �e�L�X�g�E�{�b�N�X�̒l���擾
        var folderPath = UnityAssetsFolderPath.Text;

        if (!Directory.Exists(folderPath))
        {
            // TODO �f�B���N�g���[�E�p�X�łȂ���Ύ��s
            return;
        }

        // `Assets/Muzudho/2D RPG Negiramen` �f�B���N�g���[�̗L�����`�F�b�N
        folderPath = Path.Combine(folderPath, "Muzudho");

        if (!Directory.Exists(folderPath))
        {
            // ������΍쐬
            Directory.CreateDirectory(folderPath);
        }

        folderPath = Path.Combine(folderPath, "2D RPG Negiramen");

        if (!Directory.Exists(folderPath))
        {
            // ������΍쐬
            Directory.CreateDirectory(folderPath);
        }

        // TODO Unity�փv���W�F�N�g���㏑��
    }
}