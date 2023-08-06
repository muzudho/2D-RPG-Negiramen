namespace _2D_RPG_Negiramen.Models.History;

using System.Collections.Generic;

/// <summary>
///     😁 ヒストリー
/// </summary>
internal class Its
{
    // - インターナル・メソッド

    /// <summary>
    ///     ドゥ
    /// </summary>
    /// <param name="processing">処理</param>
    internal void Do(IProcessing processing)
    {
        if (this.State != State.Undoing && this.State != State.Redoing)
        {
            if (0 < this.FutureStack.Count)
            {
                this.FutureStack.Clear();
            }

            this.CompletionStack.Push(processing);

            // アンドゥ・リドゥの活性性を変更するために、完了リストに追加した後に実行する
            processing.Do();
        }
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

        try
        {
            this.State = State.Undoing;

            var done = this.CompletionStack.Pop();
            this.FutureStack.Push(done);

            // アンドゥ・リドゥの活性性を変更するために、リストから移動した後に実行する
            done.Undo();
        }
        finally
        {
            this.State = State.None;
        }
    }

    /// <summary>
    ///     リドゥ
    /// </summary>
    internal void Redo()
    {
        if (this.FutureStack.Count < 1)
        {
            return;
        }

        try
        {
            this.State = State.Redoing;

            var done = this.FutureStack.Pop();
            this.CompletionStack.Push(done);

            // アンドゥ・リドゥの活性性を変更するために、リストから移動した後に実行する
            done.Do();
        }
        finally
        {
            this.State = State.None;
        }
    }

    /// <summary>
    ///     アンドゥできるか？
    /// </summary>
    /// <returns>そうだ</returns>
    internal bool CanUndo() => 0 < this.CompletionStack.Count;

    /// <summary>
    ///     リドゥできるか？
    /// </summary>
    /// <returns>そうだ</returns>
    internal bool CanRedo() => 0 < this.FutureStack.Count;

    // - プライベート・プロパティ

    /// <summary>
    ///     完了スタック
    /// </summary>
    Stack<IProcessing> CompletionStack { get; } = new Stack<IProcessing>();

    /// <summary>
    ///     将来スタック
    /// </summary>
    Stack<IProcessing> FutureStack { get; } = new Stack<IProcessing>();

    /// <summary>
    ///     状態
    /// </summary>
    State State { get; set; } = State.None;
}
