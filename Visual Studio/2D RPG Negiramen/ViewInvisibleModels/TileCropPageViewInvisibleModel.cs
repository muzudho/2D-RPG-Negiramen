namespace _2D_RPG_Negiramen.ViewInvisibleModels
{
    using _2D_RPG_Negiramen.Models.Geometric;
    using _2D_RPG_Negiramen.Models;
    using _2D_RPG_Negiramen.Models.Visually;
    using _2D_RPG_Negiramen.ViewModels;
    using System.Diagnostics;

    /// <summary>
    ///     見えないモデル
    ///     
    ///     <list type="bullet">
    ///         <item>ミュータブル</item>
    ///     </list>
    /// </summary>
    class TileCropPageViewInvisibleModel
    {
        // - その他

        #region その他（生成）
        /// <summary>
        ///     生成
        /// </summary>
        /// <param name="owner"></param>
        internal TileCropPageViewInvisibleModel(TileCropPageViewModel owner)
        {
            this.Owner = owner;
        }
        #endregion

        // - インターナル・プロパティ

        TileCropPageViewModel Owner { get; }

        #region プロパティ（［タイルセット元画像］　関連）
        /// <summary>
        ///     ［タイルセット元画像］のサイズ
        /// </summary>
        internal Models.Geometric.SizeInt TilesetSourceImageSize
        {
            get => tilesetSourceImageSize;
            set
            {
                this.tilesetSourceImageSize = value;
            }
        }
        #endregion

        #region プロパティ（［タイルセット作業画像］　関連）
        /// <summary>
        ///     ［タイルセット作業画像］ファイルへのパス（文字列形式）
        /// </summary>
        public string TilesetWorkingImageFilePathAsStr => App.CacheFolder.YourCircleFolder.YourWorkFolder.ImagesFolder.WorkingTilesetPng.Path.AsStr;
        #endregion

        #region プロパティ（［切抜きカーソル］　関連）
        /// <summary>
        ///     ［切抜きカーソル］が指すタイル
        ///     
        ///     <list type="bullet">
        ///         <item>★循環参照しやすいので注意</item>
        ///         <item>［切抜きカーソル］が指すタイルが未確定のときも、指しているタイルにアクセスできることに注意</item>
        ///     </list>
        /// </summary>
        internal TileRecordVisually CroppedCursorPointedTileRecordVisually { get; set; } = TileRecordVisually.CreateEmpty();
        #endregion

        #region プロパティ（［切抜きカーソルが指すタイル］　関連）
        /// <summary>
        ///     ［切抜きカーソル］が指すタイル
        ///     
        ///     <list type="bullet">
        ///         <item>［切抜きカーソル］が未確定のときも、指しているタイルにアクセスできることに注意</item>
        ///         <item>TODO ★ ［切抜きカーソル］が指すタイルが無いとき、無いということをセットするのを忘れている？</item>
        ///     </list>
        /// </summary>
        public TileRecordVisually TargetTileRecordVisually
        {
            get => this.CroppedCursorPointedTileRecordVisually;
            set
            {
                var oldTileVisually = this.CroppedCursorPointedTileRecordVisually;

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
                        this.UpdateCroppedCursorPointedTileByDifference(
                            // タイトル
                            tileTitle: TileTitle.Empty);

                        // 末端にセット（変更通知を呼ぶために）
                        // Ｉｄ
                        this.CroppedCursorPointedTileIdOrEmpty = TileIdOrEmpty.Empty;

                        // 元画像の位置とサイズ
                        this.Owner.CroppedCursorPointedTileSourceRect = RectangleInt.Empty;

                        // 論理削除
                        this.Owner.CroppedCursorPointedTileLogicalDeleteAsBool = false;

                        // 空にする
                        this.CroppedCursorPointedTileRecordVisually = TileRecordVisually.CreateEmpty();
                    }
                }
                else
                {
                    var newValue = value;

                    if (oldTileVisually.IsNone)
                    {
                        // ［切抜きカーソル］の指すタイル無し時

                        // 新規作成
                        this.CroppedCursorPointedTileRecordVisually = TileRecordVisually.CreateEmpty();
                    }
                    else
                    {
                        // ［切抜きカーソル］の指すタイルが有るなら構わない
                    }

                    // （変更通知を送っている）
                    this.UpdateCroppedCursorPointedTileByDifference(
                        // タイトル
                        tileTitle: newValue.Title);

                    // （変更通知を送っている）
                    this.CroppedCursorPointedTileIdOrEmpty = newValue.Id;
                    this.Owner.CroppedCursorPointedTileSourceLeftAsInt = newValue.SourceRectangle.Location.X.AsInt;
                    this.Owner.CroppedCursorPointedTileSourceTopAsInt = newValue.SourceRectangle.Location.Y.AsInt;
                    this.Owner.CroppedCursorPointedTileSourceWidthAsInt = newValue.SourceRectangle.Size.Width.AsInt;
                    this.Owner.CroppedCursorPointedTileSourceHeightAsInt = newValue.SourceRectangle.Size.Height.AsInt;
                    // this.CroppedCursorPointedTileTitleAsStr = newValue.Title.AsStr;
                }

                // 変更通知を送りたい
                this.Owner.NotifyTileIdChange();
            }
        }

        /// <summary>
        ///     ［切抜きカーソルが指すタイル］のＩｄ
        /// </summary>
        public Models.TileIdOrEmpty CroppedCursorPointedTileIdOrEmpty
        {
            get
            {
                var contents = this.CroppedCursorPointedTileRecordVisually;

                // ［切抜きカーソル］の指すタイル無し時
                if (contents.IsNone)
                    return Models.TileIdOrEmpty.Empty;

                return contents.Id;
            }
            set
            {
                if (this.CroppedCursorPointedTileRecordVisually.Id == value)
                    return;

                // 差分更新
                this.UpdateCroppedCursorPointedTileByDifference(
                    tileId: value);
            }
        }
        #endregion

        // - インターナル・メソッド

        #region メソッド（［切抜きカーソルが指すタイル］を差分更新）
        /// <summary>
        ///     ［切抜きカーソルが指すタイル］を差分更新
        /// </summary>
        /// <returns></returns>
        public void UpdateCroppedCursorPointedTileByDifference(
            TileIdOrEmpty? tileId = null,
            TileTitle? tileTitle = null,
            LogicalDelete? logicalDelete = null)
        {
            var currentTileVisually = this.CroppedCursorPointedTileRecordVisually;

            // タイルＩｄ
            if (!(tileId is null) && currentTileVisually.Id != tileId)
            {
                this.CroppedCursorPointedTileRecordVisually.Id = tileId;

                // Ｉｄが入ることで、タイル登録扱いになる。いろいろ再描画する
                // this.InvalidateLocale();
                // this.InvalidateAddsButton();

                // ［追加／上書き］ボタン再描画
                this.Owner.InvalidateAddsButton();

                // ［削除］ボタン再描画
                this.Owner.InvalidateDeletesButton();

                // NotifyTileIdChange();
            }

            // タイル・タイトル
            if (!(tileTitle is null) && currentTileVisually.Title != tileTitle)
            {
                this.CroppedCursorPointedTileRecordVisually.Title = tileTitle;
            }

            // 論理削除フラグ
            if (!(logicalDelete is null) && currentTileVisually.LogicalDelete != logicalDelete)
            {
                this.CroppedCursorPointedTileRecordVisually.LogicalDelete = logicalDelete;
            }

            // 変更通知を送る
            this.Owner.NotifyTileIdChange();

            Trace.WriteLine($"[TileCropPageViewModel.cs UpdateCroppedCursorPointedTileByDifference] CroppedCursorPointedTileRecordVisually.Dump(): {this.CroppedCursorPointedTileRecordVisually.Dump()}");
        }
        #endregion

        // - プライベート・フィールド

        #region フィールド（［タイルセット元画像］　関連）
        /// <summary>
        ///     ［タイルセット元画像］サイズ
        /// </summary>
        Models.Geometric.SizeInt tilesetSourceImageSize = Models.Geometric.SizeInt.Empty;
        #endregion
    }
}
