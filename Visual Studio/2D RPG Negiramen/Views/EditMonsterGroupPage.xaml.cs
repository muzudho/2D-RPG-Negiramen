﻿namespace _2D_RPG_Negiramen.Views;

/// <summary>
///     😁 編集モンスター・グループ・ページ
/// </summary>
public partial class EditMonsterGroupPage : ContentPage
{
    public EditMonsterGroupPage()
    {
        InitializeComponent();
    }

    async void HomeBtn_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//HomePage");
    }
}