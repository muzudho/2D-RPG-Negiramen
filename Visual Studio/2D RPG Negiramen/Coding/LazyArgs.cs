﻿namespace _2D_RPG_Negiramen.Coding
{
    /// <summary>
    ///     😁 引数に使える様々な関数
    /// </summary>
    internal class LazyArgs
    {
        /// <summary>
        ///     セット
        /// </summary>
        /// <typeparam name="IN"></typeparam>
        /// <param name="value"></param>
        internal delegate void Set<IN>(IN value);

        /// <summary>
        ///     ゲット
        /// </summary>
        /// <typeparam name="OUT"></typeparam>
        /// <returns></returns>
        internal delegate OUT Get<OUT>();

        /// <summary>
        ///     コンバート
        /// </summary>
        /// <typeparam name="OUT"></typeparam>
        /// <returns></returns>
        internal delegate OUT Convert<IN, OUT>(IN value);
    }
}
