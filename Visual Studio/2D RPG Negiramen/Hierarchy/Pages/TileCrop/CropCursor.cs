namespace _2D_RPG_Negiramen.Hierarchy.Pages.TileCrop;

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
    /// <param name="memberNetwork"></param>
    internal CropCursor(
        ItsMemberNetwork memberNetwork)
    {
        this.MemberNetwork = memberNetwork;
    }
    #endregion

    // - インターナル・デリゲート

    internal delegate void SetValue(float value);

    // - インターナル・プロパティ

    #region プロパティ（トリック幅）
    /// <summary>
    ///     トリック幅
    /// </summary>
    internal WidthFloat TrickWidth { get; set; } = WidthFloat.Zero;
    #endregion

    public WidthFloat WorkingWidthWithoutTrick
    {
        get => workingWidthWithoutTrick;
        set => workingWidthWithoutTrick = value;
    }

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
        if (TrickWidth.AsFloat == 1.0f)
        {
            TrickWidth = WidthFloat.Zero;
        }
        else
        {
            TrickWidth = WidthFloat.One;
        }
    }

    #region メソッド（［作業グリッド］　関連）
    /// <summary>
    ///     ［作業グリッド］タイル横幅の再計算
    ///     
    ///     <list type="bullet">
    ///         <item>アンドゥ・リドゥで利用</item>
    ///     </list>
    /// </summary>
    internal void RecalculateWorkingGridTileWidth(
        SetValue setValue)
    {
        setValue(MemberNetwork.GridUnit.SourceValue.Width.AsInt);
    }

    /// <summary>
    ///     ［作業グリッド］タイル縦幅の再計算
    ///     
    ///     <list type="bullet">
    ///         <item>アンドゥ・リドゥで利用</item>
    ///     </list>
    /// </summary>
    internal void RecalculateWorkingGridTileHeight(
        SetValue setValue)
    {
        setValue(MemberNetwork.GridUnit.SourceValue.Height.AsInt);
    }
    #endregion

    // - プライベート・フィールド

    /// <summary>
    ///     ［切抜きカーソル］ズーム済みのサイズ
    ///         
    ///     <list type="bullet">
    ///         <item>カーソルの線の幅を含まない</item>
    ///         <item>TODO ★ 現在、範囲選択は、この作業用のサイズを使っているが、ソースの方のサイズを変更するようにできないか？ ワーキングは変数にしないようにしたい</item>
    ///         <item>仕様変更するときは、TRICK CODE に注意</item>
    ///     </list>
    /// </summary>
    internal WidthFloat workingWidthWithoutTrick = WidthFloat.Zero;

    // - プライベート・プロパティ

    ItsMemberNetwork MemberNetwork { get; }
}
