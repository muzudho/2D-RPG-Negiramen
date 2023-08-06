namespace _2D_RPG_Negiramen.Models.History;

/// <summary>
///     😁 処理
/// </summary>
internal interface IProcessing
{
    // パブリック・メソッド

    /// <summary>
    ///     ドゥ
    /// </summary>
    void Do();

    /// <summary>
    ///     アンドゥ
    /// </summary>
    void Undo();
}
