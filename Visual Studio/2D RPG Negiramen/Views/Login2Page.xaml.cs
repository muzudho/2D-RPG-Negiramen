namespace _2D_RPG_Negiramen.Views;

public partial class Login2Page : ContentPage
{
	public Login2Page()
	{
		InitializeComponent();
	}

    // - �v���C�x�[�g�E�C�x���g�n���h��

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