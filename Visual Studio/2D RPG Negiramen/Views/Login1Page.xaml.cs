using _2D_RPG_Negiramen.ViewModels;

namespace _2D_RPG_Negiramen.Views;

public partial class Login1Page : ContentPage
{
    // - ���̑�

    #region ���̑��i�����j
    /// <summary>
    ///     ����
    /// </summary>
    public Login1Page()
    {
        InitializeComponent();
    }
    #endregion

    // - �p�u���b�N�E�v���p�e�B

    #region �v���p�e�B�i�r���[���f���j
    /// <summary>
    ///     �r���[���f��
    /// </summary>
    internal ILogin1PageViewModel Login1PageVM => (ILogin1PageViewModel)this.BindingContext;
    #endregion

    // - �v���C�x�[�g�E�C�x���g�n���h��

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

    #region �C�x���g�n���h���i���P�[���ύX���j
    /// <summary>
    ///     ���P�[���ύX��
    /// </summary>
    /// <param name="sender">���̃C�x���g���Ăяo�����R���g���[��</param>
    /// <param name="e">���̔����C�x���g�̐���ϐ�</param>
    private void LocalePicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        // �w�`�l�k�ł͂Ȃ��A�b���œ��I�ɖ|����s���Ă���ꍇ�̂��߂̕ύX�ʒm
        //var context = this.TileCropPageVM;
        //context.InvalidateLocale();
    }
    #endregion

    #region �C�x���g�n���h���i�m�T�[�N�����n�ύX���j
    /// <summary>
    ///     �m�T�[�N�����n�ύX��
    /// </summary>
    /// <param name="sender">���̃C�x���g���Ăяo�����R���g���[��</param>
    /// <param name="e">���̔����C�x���g�̐���ϐ�</param>
    void YourCircleNameEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        Entry entry = (Entry)sender;

        this.Login1PageVM.YourCircleNameLength = entry.Text.Length;
    }
    #endregion

    #region �C�x���g�n���h���i�m��i���n�ύX���j
    /// <summary>
    ///     �m��i���n�ύX��
    /// </summary>
    /// <param name="sender">���̃C�x���g���Ăяo�����R���g���[��</param>
    /// <param name="e">���̔����C�x���g�̐���ϐ�</param>
    void YourWorkNameEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        Entry entry = (Entry)sender;

        this.Login1PageVM.YourWorkNameLength = entry.Text.Length;
    }
    #endregion
}