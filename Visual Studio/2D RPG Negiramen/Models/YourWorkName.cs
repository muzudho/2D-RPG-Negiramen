namespace _2D_RPG_Negiramen.Models
{
    /// <summary>
    /// あなたの作品名
    /// </summary>
    class YourWorkName
    {
        /// <summary>
        /// 文字列を与えて初期化
        /// </summary>
        /// <param name="yourWorkName">あなたの作品名</param>
        /// <returns>実例</returns>
        internal static YourWorkName FromString(string yourWorkName)
        {
            if (yourWorkName == null)
            {
                throw new ArgumentNullException(nameof(yourWorkName));
            }

            return new YourWorkName(yourWorkName);
        }

        /// <summary>
        /// 生成
        /// </summary>
        internal YourWorkName()
        {
            this.AsStr = string.Empty;
        }

        /// <summary>
        /// 生成
        /// </summary>
        internal YourWorkName(string asStr)
        {
            this.AsStr = asStr;
        }

        /// <summary>
        /// 文字列形式
        /// </summary>
        internal string AsStr { get; }
    }
}
