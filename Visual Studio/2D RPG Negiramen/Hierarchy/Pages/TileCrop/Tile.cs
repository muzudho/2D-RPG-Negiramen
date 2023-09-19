namespace _2D_RPG_Negiramen.Hierarchy.Pages.TileCrop;

using _2D_RPG_Negiramen.Models;
using TheGeometric = _2D_RPG_Negiramen.Models.Geometric;

/// <summary>
///     切抜きカーソルが指すタイル
/// </summary>
internal class Tile
{
    // - その他

    #region その他（生成）
    /// <summary>
    ///     生成
    /// </summary>
    internal Tile()
    {
        this.WorkingWidthWithoutTrick = TheGeometric.WidthFloat.Zero;
    }
    #endregion

    // - インターナル・デリゲート

    internal delegate void OnUpdateByDifference(TileTitle tileTitle);

    internal delegate void OnTileIdOrEmpty(TileIdOrEmpty tileIdOrEmpty);

    // - インターナル・プロパティ

    #region プロパティ（［切抜きカーソルが指すタイル］　関連）
    /// <summary>
    ///     保存データ
    ///     
    ///     <list type="bullet">
    ///         <item>［切抜きカーソル］が指すタイルが未確定のときも、指しているタイルにアクセスできることに注意</item>
    ///         <item>TODO ★ ［切抜きカーソル］が指すタイルが無いとき、無いということをセットするのを忘れている？</item>
    ///     </list>
    /// </summary>
    public TileRecord Record { get; set; } = TileRecord.Empty;
    #endregion

    #region プロパティ（Ｉｄ）
    /// <summary>
    ///     Ｉｄ
    /// </summary>
    public TileIdOrEmpty IdOrEmpty
    {
        get
        {
            var contents = this.Record;

            // ［切抜きカーソル］の指すタイル無し時
            if (contents.Rectangle_IsNotNormal)
                return TileIdOrEmpty.Empty;

            return contents.Id;
        }
    }
    #endregion

    #region プロパティ（トリック幅）
    /// <summary>
    ///     トリック幅
    /// </summary>
    internal TheGeometric.WidthFloat TrickWidth { get; set; } = TheGeometric.WidthFloat.Zero;
    #endregion

    /// <summary>
    ///     ［切抜きカーソル］ズーム済みのサイズ
    ///         
    ///     <list type="bullet">
    ///         <item>カーソルの線の幅を含まない</item>
    ///         <item>TODO ★ 現在、範囲選択は、この作業用のサイズを使っているが、ソースの方のサイズを変更するようにできないか？ ワーキングは変数にしないようにしたい</item>
    ///         <item>仕様変更するときは、TRICK CODE に注意</item>
    ///     </list>
    /// </summary>
    internal TheGeometric.WidthFloat WorkingWidthWithoutTrick { get; set; }

    /// <summary>
    ///     ［切抜きカーソルが指すタイル］の元画像ベースの矩形
    ///     
    ///     <list type="bullet">
    ///         <item>カーソルが無いとき、大きさの無いカーソルを返す</item>
    ///     </list>
    /// </summary>
    internal TheGeometric.RectangleInt SourceRectangle
    {
        get
        {
            if (this.Record.Rectangle_IsNotNormal)
            {
                // ［切抜きカーソル］の指すタイル無し時
                return TheGeometric.RectangleInt.Empty;
            }

            return this.Record.Rectangle;
        }
    }

    /// <summary>
    ///     ［切抜きカーソル］元画像ベースのサイズ
    ///     
    ///     <list type="bullet">
    ///         <item>線の太さを含まない</item>
    ///     </list>
    /// </summary>
    public TheGeometric.SizeInt SourceSize
    {
        get
        {
            // ［切抜きカーソル］無し時
            if (this.Record.Rectangle_IsNotNormal)
                return TheGeometric.SizeInt.Empty;

            return this.Record.Rectangle.Size;
        }
    }

    // - インターナル・メソッド

    #region メソッド（セット・データ）
    /// <summary>
    ///     セット・データ
    /// </summary>
    /// <param name="value"></param>
    /// <param name="onVanished"></param>
    /// <param name="onUpdated"></param>
    /// <param name="onUpdateByDifference"></param>
    /// <param name="onTileIdOrEmpty"></param>
    internal void SetRecord(
        TileRecord value,
        Action onVanished,
        Action onUpdated,
        OnUpdateByDifference onUpdateByDifference,
        OnTileIdOrEmpty onTileIdOrEmpty)
    {
        var oldTile = this.Record;

        // 値に変化がない
        if (oldTile == value)
            return;

        if (value.Rectangle_IsNotNormal)
        {
            // ［切抜きカーソルが指すタイル］を無しに設定する

            if (oldTile.Rectangle_IsNotNormal)
            {
                // ［切抜きカーソルが指すタイル］がもともと無く、［切抜きカーソルが指すタイル］を無しに設定するのだから、何もしなくてよい
            }
            else
            {
                // ［切抜きカーソルが指すタイル］がもともと有って、［切抜きカーソルが指すタイル］を無しに設定するのなら、消すという操作がいる
                onUpdateByDifference(
                    tileTitle: TileTitle.Empty);

                // 末端にセット（変更通知を呼ぶために）
                // Ｉｄ
                SetIdOrEmpty(
                    value: TileIdOrEmpty.Empty,
                    onTileIdOrEmpty: onTileIdOrEmpty);

                // 空にする
                this.Record = TileRecord.Empty;

                onVanished();
            }
        }
        else
        {
            var newValue = value;

            if (oldTile.Rectangle_IsNotNormal)
            {
                // ［切抜きカーソル］の指すタイル無し時

                // 新規作成
                this.Record = TileRecord.Empty;
            }
            else
            {
                // ［切抜きカーソル］の指すタイルが有るなら構わない
            }

            // （変更通知を送っている）
            onUpdateByDifference(
                tileTitle: newValue.Title);

            SetIdOrEmpty(
                value: newValue.Id,
                onTileIdOrEmpty: onTileIdOrEmpty);
            // this.SelectedTile_TitleAsStr = newValue.Title.AsStr;

            onUpdated();
        }
    }
    #endregion

    #region メソッド（セット・データ（GUI非更新））
    /// <summary>
    ///     セット・データ（GUI非更新）
    /// </summary>
    /// <param name="value"></param>
    internal void SetRecordNoGuiUpdate(TileRecord value)
    {
        this.Record = value;
    }
    #endregion

    #region メソッド（Ｉｄ設定）
    /// <summary>
    ///     Ｉｄ設定
    /// </summary>
    /// <param name="value"></param>
    /// <param name="onTileIdOrEmpty"></param>
    internal void SetIdOrEmpty(
        TileIdOrEmpty value,
        OnTileIdOrEmpty onTileIdOrEmpty)
    {
        if (this.Record.Id == value)
            return;

        // 差分更新
        onTileIdOrEmpty(
            tileIdOrEmpty: value);
    }
    #endregion

    #region メソッド（［切抜きカーソル］ズーム済みのキャンバスの再描画）
    /// <summary>
    ///     <pre>
    ///         ［切抜きカーソル］ズーム済みのキャンバスの再描画
    /// 
    ///         TRICK:  GraphicsView を再描画させたいが、ビューモデルから要求する方法が分からない。
    ///                 そこで、内部的なグリッド画像の横幅が偶数のときは +1、奇数のときは -1 して
    ///                 振動させることで、再描画を呼び起こすことにする
    ///     </pre>
    /// </summary>
    internal void RefreshCanvasTrick(string codePlace = "[TileCropPageViewModel RefreshCanvasOfTileCursor]")
    {
        if (TrickWidth.AsFloat == 1.0f)
        {
            TrickWidth = TheGeometric.WidthFloat.Zero;
        }
        else
        {
            TrickWidth = TheGeometric.WidthFloat.One;
        }
    }
    #endregion
}
