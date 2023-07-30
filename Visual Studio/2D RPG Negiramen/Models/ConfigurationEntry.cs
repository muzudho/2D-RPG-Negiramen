namespace _2D_RPG_Negiramen.Models
{
    /// <summary>
    ///     構成ファイルの entry 配列の要素
    ///     
    ///     <list type="bullet">
    ///         <item>イミュータブル</item>
    ///     </list>
    /// </summary>
    internal class ConfigurationEntry
    {
        // - その他

        #region その他（生成）
        /// <summary>
        ///     生成
        /// </summary>
        internal ConfigurationEntry(
            YourCircleName yourCircleName,
            YourWorkName yourWorkName)
        {
            this.YourCircleName = yourCircleName;
            this.YourWorkName = yourWorkName;
        }
        #endregion

        // - パブリック・プロパティ

        #region プロパティ（表示用文字列）
        /// <summary>
        ///     表示用文字列
        /// </summary>
        public string PresentableTextAsStr => $"{this.YourCircleName.AsStr}/{this.YourWorkName.AsStr}";
        #endregion

        // - インターナル・プロパティ

        #region プロパティ（あなたのサークル名）
        /// <summary>
        ///     あなたのサークル名
        /// </summary>
        internal YourCircleName YourCircleName { get; }
        #endregion

        #region プロパティ（あなたの作品名）
        /// <summary>
        ///     あなたの作品名
        /// </summary>
        internal YourWorkName YourWorkName { get; }
        #endregion
    }
}
