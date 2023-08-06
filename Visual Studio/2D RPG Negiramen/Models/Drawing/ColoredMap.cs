﻿namespace _2D_RPG_Negiramen.Models.Drawing;

using _2D_RPG_Negiramen.ViewModels;
using System.Diagnostics;

/// <summary>
///     カラーマップ
/// </summary>
internal class ColoredMap : BindableObject, IDrawable
{
    // - パブリック束縛可能プロパティ

    #region 束縛可能プロパティ（［ダーティー回数］　関連）
    /// <summary>
    ///     ［ダーティー回数］
    /// </summary>
    public byte DirtyCount
    {
        get => (byte)GetValue(DirtyCountProperty);
        set => SetValue(DirtyCountProperty, value);
    }

    /// <summary>
    ///     <see cref="IsDirty"/> に対応
    /// </summary>
    public static BindableProperty DirtyCountProperty = BindableProperty.Create(
        // プロパティ名
        propertyName: nameof(DirtyCount),
        // 返却型
        returnType: typeof(byte),
        // これを含んでいるクラス
        declaringType: typeof(ColoredMap),
        defaultValue: (byte)0);
    #endregion

    #region 束縛可能プロパティ（タイルセット設定ビューモデル）
    /// <summary>
    ///     タイルセット設定ビューモデル
    /// </summary>
    public TilesetSettingsViewModel TilesetSettingsVM
    {
        get => (TilesetSettingsViewModel)GetValue(TilesetSettingsVMProperty);
        set => SetValue(TilesetSettingsVMProperty, value);
    }

    /// <summary>
    ///     <see cref="TilesetSettingsVM"/>
    /// </summary>
    public static BindableProperty TilesetSettingsVMProperty = BindableProperty.Create(
        // プロパティ名
        propertyName: nameof(TilesetSettingsVM),
        // 返却型
        returnType: typeof(TilesetSettingsViewModel),
        // これを含んでいるクラス
        declaringType: typeof(ColoredMap));
    #endregion

    /// <summary>
    ///     描画
    /// </summary>
    /// <param name="canvas">キャンバス</param>
    /// <param name="dirtyRect">位置とサイズ</param>
    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        Trace.WriteLine($"[ColoredMap.cs Draw] 開始 this.DirtyCount: {this.DirtyCount}");

        if (this.TilesetSettingsVM == null)
        {
            return;
        }

        // 各登録タイル
        foreach (var recordVM in this.TilesetSettingsVM.RecordViewModelList)
        {
            if (recordVM.LogicalDelete == Models.LogicalDelete.True)
            {
                // 論理削除されてるから無視
                // Trace.WriteLine($"[TileCropPage.xaml.cs ContentPage_Loaded] 論理削除されてるから無視　recordVM: {recordVM.Dump()}");
                continue;
            }
            //else
            //{
            //    Trace.WriteLine($"[TileCropPage.xaml.cs ContentPage_Loaded] recordVM: {recordVM.Dump()}");
            //}

            // 枠の線の太さの半分
            int halfFrameThickness = 2;

            // 線の色はランダム。自然数を散らしているだけ。色相は黄緑になるところが多いので、黄緑が沢山出てくるのは仕方ない。
            float hue = ((int)((float)recordVM.Id.AsInt / 7.0f * 100.0f) % 100) / 100.0f;
            canvas.StrokeColor = Color.FromHsv(
                h: hue,                                     // ヒュー（Hue；色相）
                                                            // h: (float)Random.Shared.NextDouble(),    // ヒュー（Hue；色相）
                s: 0.3f,                                    // サチュレーション（Saturation；彩度）
                v: 1.0f);                                   // バリュー（Value；明度）

            //
            // 角丸の矩形を引く
            // ================
            //
            canvas.StrokeSize = 2 * halfFrameThickness;
            canvas.DrawRoundedRectangle(
                // 枠の線の太さの半分だけサイズを縮める
                rect: recordVM.WorkingRectangle.AsGraphis().Inflate(-halfFrameThickness, -halfFrameThickness),
                cornerRadius: 16.0d);
        }
    }
}

