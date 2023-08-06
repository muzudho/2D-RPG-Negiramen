namespace _2D_RPG_Negiramen.Models.History;

/// <summary>
///     😁 状態
/// </summary>
enum State
{
    None,

    /// <summary>
    ///     アンドゥ中だ
    /// </summary>
    Undoing,

    /// <summary>
    ///     リドゥ中だ
    /// </summary>
    Redoing,
}
