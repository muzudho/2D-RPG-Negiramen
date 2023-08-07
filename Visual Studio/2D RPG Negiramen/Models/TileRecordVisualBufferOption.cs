namespace _2D_RPG_Negiramen.Models;

using _2D_RPG_Negiramen.Coding;

/// <summary>
///     
/// </summary>
internal class TileRecordVisualBufferOption
{
    internal static TileRecordVisualBufferOption CreateNone()
    {
        return new TileRecordVisualBufferOption()
        {
            selectedTileRecordVisualBufferOption = new(null),
        };
    }

    internal static TileRecordVisualBufferOption CreateEmpty()
    {
        return new TileRecordVisualBufferOption()
        {
            selectedTileRecordVisualBufferOption = new(TileRecordVisualBuffer.CreateEmpty()),
        };
    }

    internal static TileRecordVisualBufferOption CreateSome(TileRecordVisualBuffer tileRecordVisualBuffer)
    {
        return new TileRecordVisualBufferOption()
        {
            selectedTileRecordVisualBufferOption = new(tileRecordVisualBuffer),
        };
    }

    TileRecordVisualBufferOption()
    {

    }

    // - インターナル・プロパティ

    internal bool IsNone
    {
        get
        {
            bool isNone = false;

            this.selectedTileRecordVisualBufferOption.Unwrap(
                some: (contents) =>
                {

                },
                none: () =>
                {
                    isNone = true;
                });

            return isNone;
        }
    }

    // - インターナル・プロパティ

    /// <summary>
    ///     中身があるか？
    /// </summary>
    /// <returns>そうだ</returns>
    internal bool IsSome => this.selectedTileRecordVisualBufferOption.IsSome;

    // - インターナル・メソッド

    /// <summary>
    ///     中身を取り出す
    /// </summary>
    /// <param name="contents"></param>
    /// <returns></returns>
    internal void Unwrap(LazyArgs.Set<TileRecordVisualBuffer> some, Action none)
    {
        this.selectedTileRecordVisualBufferOption.Unwrap(some, none);
    }

    // - プライベート・プロパティ

    Option<TileRecordVisualBuffer> selectedTileRecordVisualBufferOption = new(TileRecordVisualBuffer.CreateEmpty());
}
