namespace _2D_RPG_Negiramen.ViewInnerModels.TileCropPage;

using _2D_RPG_Negiramen.Models.Geometric;

/// <summary>
///     切抜きカーソル
/// </summary>
internal class CropCursor
{
    // - その他

    #region その他（生成）
    /// <summary>
    ///     生成
    /// </summary>
    /// <param name="owner"></param>
    internal CropCursor(TileCropPageViewInnerModel owner)
    {
        this.Owner = owner;
    }
    #endregion

    // - インターナル・プロパティ

    #region プロパティ（トリック幅）
    /// <summary>
    ///     トリック幅
    /// </summary>
    internal WidthFloat TrickWidth { get; set; } = WidthFloat.Zero;
    #endregion

    // - インターナル・メソッド

    /// <summary>
    ///     <pre>
    ///         ［切抜きカーソル］ズーム済みのキャンバスの再描画
    /// 
    ///         TRICK:  GraphicsView を再描画させたいが、ビューモデルから要求する方法が分からない。
    ///                 そこで、内部的なグリッド画像の横幅が偶数のときは +1、奇数のときは -1 して
    ///                 振動させることで、再描画を呼び起こすことにする
    ///     </pre>
    /// </summary>
    internal void RefreshCanvasTrick(string codePlace = "[TileCropPageViewModel RefreshCanvasOfTileCursor]")
    {
        if (this.TrickWidth.AsFloat == 1.0f)
        {
            this.TrickWidth = WidthFloat.Zero;
        }
        else
        {
            this.TrickWidth = WidthFloat.One;
        }

        // TRICK CODE:
        this.Owner.Owner.InvalidateWorkingTargetTile();
    }

    // - プライベート・プロパティ

    TileCropPageViewInnerModel Owner { get; }
}
