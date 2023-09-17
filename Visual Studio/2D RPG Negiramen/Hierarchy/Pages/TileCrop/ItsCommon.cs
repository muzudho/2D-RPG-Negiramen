namespace _2D_RPG_Negiramen.Hierarchy.Pages.TileCrop;

/// <summary>
///     共有
///     
///     <list type="bullet">
///         <item>Mutable</item>
///     </list>
/// </summary>
internal class ItsCommon
{
    // - インターナル・プロパティ

    #region プロパティ（切抜きカーソルと、既存タイルが交差しているか？）
    /// <summary>
    ///     切抜きカーソルと、既存タイルが交差しているか？
    /// </summary>
    /// <returns>そうだ</returns>
    internal bool HasIntersectionBetweenCroppedCursorAndRegisteredTile { get; set; }
    #endregion

    #region プロパティ（切抜きカーソルと、既存タイルは合同か？）
    /// <summary>
    ///     切抜きカーソルと、既存タイルは合同か？
    /// </summary>
    /// <returns>そうだ</returns>
    internal bool IsCongruenceBetweenCroppedCursorAndRegisteredTile { get; set; }
    #endregion
}
