namespace _2D_RPG_Negiramen.Models.History;

using System.Collections.Generic;

/// <summary>
///     😁 ヒストリー
/// </summary>
internal class History
{
    // - インターナル・メソッド

    /// <summary>
    ///     ドゥ
    /// </summary>
    /// <param name="processing">処理</param>
    internal void Do(IProcessing processing)
    {
        if (0 < this.FuturedStack.Count)
        {
            this.FuturedStack.Clear();
        }

        processing.Do();

        this.CompletionStack.Push(processing);
    }

    /// <summary>
    ///     アンドゥ
    /// </summary>
    internal void Undo()
    {
        if (this.CompletionStack.Count < 1)
        {
            return;
        }

        var done = this.CompletionStack.Pop();
        done.Undo();

        this.FuturedStack.Push(done);
    }

    /// <summary>
    ///     リドゥ
    /// </summary>
    internal void Redo()
    {
        if (this.FuturedStack.Count < 1)
        {
            return;
        }

        var done = this.FuturedStack.Pop();
        done.Do();

        this.CompletionStack.Push(done);
    }

    // - プライベート・プロパティ

    /// <summary>
    ///     完了スタック
    /// </summary>
    Stack<IProcessing> CompletionStack { get; } = new Stack<IProcessing>();

    /// <summary>
    ///     将来スタック
    /// </summary>
    Stack<IProcessing> FuturedStack { get; } = new Stack<IProcessing>();
}
