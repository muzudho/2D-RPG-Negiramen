namespace _2D_RPG_Negiramen;

public partial class MapExplorerPage : ContentPage
{
	public MapExplorerPage()
	{
		InitializeComponent();
	}

    async void HomeBtn_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }

    /// <summary>
    /// �m���ڂ��_�u���N���b�N�n�{�^��������
    /// </summary>
    /// <param name="sender">���̃C�x���g���Ăяo�����R���g���[��</param>
    /// <param name="e">���̔����C�x���g�̐���ϐ�</param>
    async void DoubleClickItemBtn_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//CreateMapViewPage");
    }
}