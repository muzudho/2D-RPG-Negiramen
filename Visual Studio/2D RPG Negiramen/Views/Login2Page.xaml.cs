namespace _2D_RPG_Negiramen.Views;

using _2D_RPG_Negiramen.ViewModels;
using System.Diagnostics;

public partial class Login2Page : ContentPage
{
    // - ���̑�

    #region ���̑��i�����j
    /// <summary>
    ///     ����
    /// </summary>
	public Login2Page()
	{
		InitializeComponent();
	}
    #endregion

    // - �C���^�[�i���E�v���p�e�B

    #region �v���p�e�B�i�r���[���f���j
    /// <summary>
    ///     �r���[���f��
    /// </summary>
    internal ILogin2PageViewModel Login2PageVM => (ILogin2PageViewModel)this.BindingContext;
    #endregion

    // - �v���C�x�[�g�E�C�x���g�n���h��

    #region �C�x���g�n���h���i�y�[�W�Ǎ��������j
    /// <summary>
    ///     �y�[�W�Ǎ�������
    /// </summary>
    /// <param name="sender">���̃C�x���g���Ăяo�����R���g���[��</param>
    /// <param name="e">���̔����C�x���g�̐���ϐ�</param>
    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        Trace.WriteLine($"[Login2Page ContentPage_Loaded] �y�[�W�Ǎ�����");

        this.Login2PageVM.StarterKitFolder = App.GetOrLoadConfiguration().StarterKitFolder;
        this.Login2PageVM.UnityAssetsFolder = App.GetOrLoadConfiguration().UnityAssetsFolder;
    }
    #endregion

    #region �C�x���g�n���h���i���P�[���ύX���j
    /// <summary>
    ///     ���P�[���ύX��
    /// </summary>
    /// <param name="sender">���̃C�x���g���Ăяo�����R���g���[��</param>
    /// <param name="e">���̔����C�x���g�̐���ϐ�</param>
    void LocalePicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        // �w�`�l�k�ł͂Ȃ��A�b���œ��I�ɖ|����s���Ă���ꍇ�̂��߂̕ύX�ʒm
        //var context = this.TileCropPageVM;
        //context.InvalidateLocale();
    }
    #endregion

    #region �C�x���g�n���h���i�m�z�[���n�{�^���E�N���b�N���j
    /// <summary>
    ///     �m�z�[���n�{�^���E�N���b�N��
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    async void HomeBtn_Clicked(object sender, EventArgs e)
    {
        await PolicyOfView.ReactOnPushed((Button)sender);

        await Shell.Current.GoToAsync("//MainPage");
    }
    #endregion
}