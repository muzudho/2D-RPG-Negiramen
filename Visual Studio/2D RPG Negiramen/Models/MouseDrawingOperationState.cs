namespace _2D_RPG_Negiramen.Models;

/// <summary>
///     マウス描画操作状態
/// </summary>
enum MouseDrawingOperationState
{
    /// <summary>
    ///     なし
    /// </summary>
    None,

    /// <summary>
    ///     ボタン押下直後
    /// </summary>
    ButtonDown,

    /// <summary>
    ///     カーソル移動時
    /// </summary>
    PointerMove,

    /// <summary>
    ///     ボタンを放した直後
    /// </summary>
    ButtonUp,
}
