﻿using _2D_RPG_Negiramen.ViewModels;

namespace _2D_RPG_Negiramen.Views;

/// <summary>
///     😁 タイルセット一覧ページ
/// </summary>
public partial class TilesetListPage : ContentPage
{
    // - その他

    #region その他（生成）
    /// <summary>
    ///     生成
    /// </summary>
    public TilesetListPage()
	{
		InitializeComponent();
	}
    #endregion

    // - パブリック・プロパティ

    #region プロパティ（ビューモデル）
    /// <summary>
    ///     ビューモデル
    /// </summary>
    public ITilesetListPageViewModel TilesetListPageVM => (ITilesetListPageViewModel)this.BindingContext;
    #endregion

    // - プライベート・イベントハンドラ

    #region イベントハンドラ（［ホーム］ボタン・クリック時）
    /// <summary>
    ///     ［ホーム］ボタン・クリック時
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    async void HomeBtn_Clicked(object sender, EventArgs e)
    {
        await PolicyOfView.ReactOnPushed((Button)sender);

        await Shell.Current.GoToAsync("//MainPage");
    }
    #endregion

    #region イベントハンドラ（ボタンにマウスカーソル進入時）
    /// <summary>
    ///     ボタンにマウスカーソル進入時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    void Button_PointerGestureRecognizer_PointerEntered(object sender, PointerEventArgs e)
    {
        PolicyOfView.ReactOnMouseEntered((Button)sender);
    }
    #endregion

    #region イベントハンドラ（ボタンからマウスカーソル退出時）
    /// <summary>
    ///     ボタンからマウスカーソル退出時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    void Button_PointerGestureRecognizer_PointerExited(object sender, PointerEventArgs e)
    {
        PolicyOfView.ReactOnMouseLeaved((Button)sender);
    }
    #endregion

    #region イベントハンドラ（ロケール変更時）
    /// <summary>
    ///     ロケール変更時
    /// </summary>
    /// <param name="sender">このイベントを呼び出したコントロール</param>
    /// <param name="e">この発生イベントの制御変数</param>
    void LocalePicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var context = this.TilesetListPageVM;
        context.InvalidateLocale();
    }
    #endregion
}