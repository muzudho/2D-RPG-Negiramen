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
            YourCircleFolderName yourCircleName,
            YourWorkFolderName yourWorkName)
        {
            this.YourCircleFolderName = yourCircleName;
            this.YourWorkFolderName = yourWorkName;
        }
        #endregion

        // - パブリック・プロパティ

        #region プロパティ（表示用文字列）
        /// <summary>
        ///     表示用文字列
        /// </summary>
        public string PresentableTextAsStr => $"{this.YourCircleFolderName.AsStr}/{this.YourWorkFolderName.AsStr}";
        #endregion

        // - インターナル・プロパティ

        #region プロパティ（あなたのサークル・フォルダ名）
        /// <summary>
        ///     あなたのサークル・フォルダ名
        /// </summary>
        internal YourCircleFolderName YourCircleFolderName { get; }
        #endregion

        #region プロパティ（あなたの作品名）
        /// <summary>
        ///     あなたの作品名
        /// </summary>
        internal YourWorkFolderName YourWorkFolderName { get; }
        #endregion
    }
}
