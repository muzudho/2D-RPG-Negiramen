namespace _2D_RPG_Negiramen.Models.Drawing;

using _2D_RPG_Negiramen.ViewModels;
using System.Diagnostics;

/// <summary>
///     カラーマップ
/// </summary>
internal class ColoredMap : BindableObject, IDrawable
{
    // - パブリック束縛可能プロパティ

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
        Trace.WriteLine($"[ColoredMap.cs Draw] 開始");

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
                Trace.WriteLine($"[TileCropPage.xaml.cs ContentPage_Loaded] 論理削除されてるから無視　recordVM: {recordVM.Dump()}");
                continue;
            }
            else
            {
                Trace.WriteLine($"[TileCropPage.xaml.cs ContentPage_Loaded] recordVM: {recordVM.Dump()}");
            }

            // 枠の線の太さの半分
            int halfFrameThickness = 2;

            // ランダム色作成
            {
                int minValue = 160;
                int maxValue = 255;
                int[] values = new int[3] { minValue, maxValue, Random.Shared.Next(minValue, maxValue) };

                // シャッフル
                MathEx.FisherYatesShuffle(values);

                canvas.StrokeColor = new Color(
                    red: values[0],
                    green: values[1],
                    blue: values[2]);
            }

            //
            // TODO キャンバスの該当矩形をクリアー
            // ===================================
            //

            //
            // TODO 元画像から部分を切り抜く
            // =============================
            //

            //
            // TODO 角丸のマスクを作成
            // =======================
            //

            //
            // TODO 元画像の切抜きにマスクを当てて、枠の外を透過
            // =================================================
            //

            //
            // TODO 角丸の画像を描画
            // =====================
            //

            //
            // 角丸の矩形を引く
            // ================
            //
            canvas.StrokeSize = 2 * halfFrameThickness;
            canvas.DrawRoundedRectangle(
                // 枠の線の太さの半分だけサイズを縮める
                rect: recordVM.WorkingRectangle.AsGraphis().Inflate(-halfFrameThickness, -halfFrameThickness),
                cornerRadius: 16.0d);

            //canvas.FillColor = new Color(220, 220, 220, 96);
            //canvas.FillRectangle(record.Rectangle.AsGraphis());


        }
    }
}

