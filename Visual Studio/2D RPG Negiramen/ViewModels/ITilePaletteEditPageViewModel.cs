using _2D_RPG_Negiramen.Models;

namespace _2D_RPG_Negiramen.ViewModels
{
    public interface ITilePaletteEditPageViewModel
    {
        #region 変更通知プロパティ（グリッドの線の太さの半分）
        /// <summary>
        ///     グリッドの線の太さの半分
        /// </summary>
        int HalfThicknessOfGridLineAsInt { get; }
        #endregion

        #region 変更通知プロパティ（グリッド全体の左上表示位置）
        /// <summary>
        ///     グリッド全体の左上表示位置
        /// </summary>
        Models.Point GridLeftTop { get; }
        #endregion

        #region 変更通知プロパティ（グリッド・タイルのサイズ）
        /// <summary>
        ///     グリッド・タイルのサイズ
        /// </summary>
        Models.Size GridTileSize { get; }
        #endregion

        #region 変更通知プロパティ（タイル・カーソルの線の半分の太さ）
        /// <summary>
        ///     タイル・カーソルの線の半分の太さ
        /// </summary>
        ThicknessOfLine HalfThicknessOfTileCursorLine { get; }
        #endregion

        #region 変更通知プロパティ（選択タイルのサイズ）
        /// <summary>
        ///     選択タイルのサイズ
        /// </summary>
        Models.Size SelectedTileSize { get; }
        #endregion

        #region 変更通知プロパティ（ポインティング・デバイス押下中か？）
        /// <summary>
        ///     ポインティング・デバイス押下中か？
        /// 
        ///     <list type="bullet">
        ///         <item>タイルを選択開始していて、まだ未確定だ</item>
        ///     </list>
        /// </summary>
        bool SelectingOnPointingDevice { get; }
        #endregion
    }
}
