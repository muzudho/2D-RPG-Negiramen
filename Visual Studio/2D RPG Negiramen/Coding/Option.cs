namespace _2D_RPG_Negiramen.Coding
{
    /// <summary>
    ///     ヌルか、ヌルでないかの確認を課す仕組み
    /// </summary>
    /// <typeparam name="T">任意の型</typeparam>
    class Option<T>
    {
        // - その他

        /// <summary>
        /// 生成
        /// </summary>
        /// <param name="some"></param>
        internal Option(T some)
        {
            this.Some = some;
        }

        // - インターナル・メソッド

        internal bool TryGetValue(out T some)
        {
            some = this.Some;
            return some != null;
        }

        // - プライベート・プロパティ

        /// <summary>
        /// 値
        /// </summary>
        T Some { get; set; }
    }
}
