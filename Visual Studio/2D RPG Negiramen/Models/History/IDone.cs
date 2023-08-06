namespace _2D_RPG_Negiramen.Models.History;

/// <summary>
///     😁 ダン
/// </summary>
internal interface IDone
{
    // パブリック・メソッド

    /// <summary>
    ///     アンドゥ
    /// </summary>
    void Undo();

    /// <summary>
    ///     リドゥ
    /// </summary>
    void Redo();
}
