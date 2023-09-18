namespace _2D_RPG_Negiramen.Hierarchy.Pages.TileCrop;

using _2D_RPG_Negiramen.Models;
using _2D_RPG_Negiramen.Models.Visually;

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
    }
    #endregion

    // - インターナル・デリゲート

    internal delegate void OnUpdateByDifference(TileTitle tileTitle);

    internal delegate void OnTileIdOrEmpty(TileIdOrEmpty tileIdOrEmpty);

    // - インターナル・プロパティ

    #region プロパティ（［切抜きカーソルが指すタイル］　関連）
    /// <summary>
    ///     ［切抜きカーソル］が指すタイル
    ///     
    ///     <list type="bullet">
    ///         <item>［切抜きカーソル］が未確定のときも、指しているタイルにアクセスできることに注意</item>
    ///         <item>TODO ★ ［切抜きカーソル］が指すタイルが無いとき、無いということをセットするのを忘れている？</item>
    ///     </list>
    /// </summary>
    public TileRecordVisually RecordVisually
    {
        get => recordVisually;
    }
    #endregion

    #region プロパティ（Ｉｄ）
    /// <summary>
    ///     Ｉｄ
    /// </summary>
    public TileIdOrEmpty IdOrEmpty
    {
        get
        {
            var contents = this.RecordVisually;

            // ［切抜きカーソル］の指すタイル無し時
            if (contents.IsNone)
                return TileIdOrEmpty.Empty;

            return contents.Id;
        }
    }
    #endregion

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
    internal void SetRecordVisually(
        TileRecordVisually value,
        Action onVanished,
        Action onUpdated,
        OnUpdateByDifference onUpdateByDifference,
        OnTileIdOrEmpty onTileIdOrEmpty)
    {
        var oldTileVisually = this.recordVisually;

        // 値に変化がない
        if (oldTileVisually == value)
            return;

        if (value.IsNone)
        {
            // ［切抜きカーソルが指すタイル］を無しに設定する

            if (oldTileVisually.IsNone)
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
                this.recordVisually = TileRecordVisually.CreateEmpty();

                onVanished();
            }
        }
        else
        {
            var newValue = value;

            if (oldTileVisually.IsNone)
            {
                // ［切抜きカーソル］の指すタイル無し時

                // 新規作成
                this.recordVisually = TileRecordVisually.CreateEmpty();
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
    internal void SetRecordVisuallyNoGuiUpdate(TileRecordVisually value)
    {
        this.recordVisually = value;
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
        if (this.RecordVisually.Id == value)
            return;

        // 差分更新
        onTileIdOrEmpty(
            tileIdOrEmpty: value);
    }
    #endregion

    // - プライベート・プロパティ

    #region プロパティ（保存データ）
    /// <summary>
    ///     保存データ
    ///     
    ///     <list type="bullet">
    ///         <item>★循環参照しやすいので注意</item>
    ///         <item>［切抜きカーソル］が指すタイルが未確定のときも、指しているタイルにアクセスできることに注意</item>
    ///     </list>
    /// </summary>
    TileRecordVisually recordVisually = TileRecordVisually.CreateEmpty();
    #endregion
}
