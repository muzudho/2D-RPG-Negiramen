﻿namespace _2D_RPG_Negiramen.Models
{
    /// <summary>
    ///     タイル・セット記録
    /// </summary>
    class TileSetRecord
    {
        // - パブリック・プロパティ

        /// <summary>
        ///     Ｉｄ
        /// </summary>
        internal Models.TileId Id { get; }

        /// <summary>
        ///     矩形
        /// </summary>
        internal Models.Rectangle Rectangle { get; }

        /// <summary>
        ///     コメント
        /// </summary>
        internal Models.Comment Comment { get; }

        // - その他

        /// <summary>
        ///     生成
        /// </summary>
        public TileSetRecord()
        {
        }

        /// <summary>
        ///     生成
        /// </summary>
        /// <param name="rectangle">レクタングル</param>
        internal TileSetRecord(
            Models.TileId id,
            Models.Rectangle rectangle,
            Models.Comment comment)
        {
            this.Id = id;
            this.Rectangle = rectangle;
            this.Comment = comment;
        }
    }
}
