namespace _2D_RPG_Negiramen;

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

        // TODO Unity�փv���W�F�N�g���㏑��
    }
}