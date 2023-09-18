namespace _2D_RPG_Negiramen.Hierarchy.Pages.TileCrop;

using _2D_RPG_Negiramen.Models.Geometric;

/// <summary>
///     切抜きカーソル
/// </summary>
internal class TileCursor
{
    // - その他

    #region その他（生成）
    /// <summary>
    ///     生成
    /// </summary>
    internal TileCursor()
    {
        this.SelectedTile_WorkingWidthWithoutTrick = WidthFloat.Zero;
    }
    #endregion

    // - インターナル・プロパティ

    /// <summary>
    ///     ［切抜きカーソル］ズーム済みのサイズ
    ///         
    ///     <list type="bullet">
    ///         <item>カーソルの線の幅を含まない</item>
    ///         <item>TODO ★ 現在、範囲選択は、この作業用のサイズを使っているが、ソースの方のサイズを変更するようにできないか？ ワーキングは変数にしないようにしたい</item>
    ///         <item>仕様変更するときは、TRICK CODE に注意</item>
    ///     </list>
    /// </summary>
    internal WidthFloat SelectedTile_WorkingWidthWithoutTrick { get; set; }
}
