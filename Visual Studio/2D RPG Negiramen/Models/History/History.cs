namespace _2D_RPG_Negiramen.Models.History;

using System.Collections.Generic;

/// <summary>
///     😁 ヒストリー
/// </summary>
internal class History
{
    // - インターナル・メソッド

    /// <summary>
    ///     ダン
    /// </summary>
    /// <param name="done"></param>
    internal void Done(IDone done)
    {
        if (0 < this.FuturedStack.Count)
        {
            this.FuturedStack.Clear();
        }

        this.CompletionStack.Push(done);
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
    Stack<IDone> CompletionStack { get; } = new Stack<IDone>();

    /// <summary>
    ///     将来スタック
    /// </summary>
    Stack<IDone> FuturedStack { get; } = new Stack<IDone>();
}
