﻿namespace _2D_RPG_Negiramen.Specifications.TileCropPage;

interface IItsTwoWayDoor
{
    #region メソッド（［タイルセット作業画像］　関連）
    /// <summary>
    ///     ［タイルセット作業画像］の再作成
    ///     
    ///     <list type="bullet">
    ///         <item>アンドゥ・リドゥで利用</item>
    ///     </list>
    /// </summary>
    void RemakeWorkingTilesetImage();
    #endregion

    #region メソッド（［元画像グリッド］　関連）
    /// <summary>
    ///     ［元画像グリッド］のキャンバス画像の再作成
    ///     
    ///     <list type="bullet">
    ///         <item>アンドゥ・リドゥで利用</item>
    ///         <item>グリッドの線の太さを 2px と想定しているので、グリッドの線が画像の端っこで切れないように、グリッドの内部的キャンバス・サイズを 2px 広げる</item>
    ///     </list>
    /// </summary>
    public void RemakeGridCanvasImage();
    #endregion
}