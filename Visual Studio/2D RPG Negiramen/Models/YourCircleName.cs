/// <summary>
///     😁 モデル
/// </summary>
namespace _2D_RPG_Negiramen.Models
{
    /// <summary>
    ///     あなたのサークル名
    /// </summary>
    class YourCircleName
    {
        // - 静的プロパティ

        /// <summary>
        ///     空オブジェクト
        /// </summary>
        internal static YourCircleName Empty { get; } = new YourCircleName();

        /// <summary>
        ///     文字列を与えて初期化
        /// </summary>
        /// <param name="yourCircleName">あなたのサークル名</param>
        /// <returns>実例</returns>
        internal static YourCircleName FromString(string yourCircleName)
        {
            if (yourCircleName == null)
            {
                throw new ArgumentNullException(nameof(yourCircleName));
            }

            return new YourCircleName(yourCircleName);
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal YourCircleName()
        {
            this.AsStr = string.Empty;
        }

        /// <summary>
        ///     生成
        /// </summary>
        internal YourCircleName(string asStr)
        {
            this.AsStr = asStr;
        }

        /// <summary>
        ///     文字列形式
        /// </summary>
        internal string AsStr { get; }

        /// <summary>
        ///     暗黙的な文字列形式
        /// </summary>
        public override string ToString() => AsStr;
    }
}
