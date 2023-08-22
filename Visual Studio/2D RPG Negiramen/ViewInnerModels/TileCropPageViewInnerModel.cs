namespace _2D_RPG_Negiramen.ViewInnerModels
{
    using _2D_RPG_Negiramen.Models;
    using _2D_RPG_Negiramen.Models.Geometric;
    using _2D_RPG_Negiramen.Models.Visually;
    using _2D_RPG_Negiramen.ViewHistory.TileCropPage;
    using _2D_RPG_Negiramen.ViewModels;
    using System.Diagnostics;
    using TheFileEntryLocations = _2D_RPG_Negiramen.Models.FileEntries.Locations;

    /// <summary>
    ///     内部モデル
    ///     
    ///     <list type="bullet">
    ///         <item>ミュータブル</item>
    ///     </list>
    /// </summary>
    class TileCropPageViewInnerModel
    {
        // - その他

        #region その他（生成）
        /// <summary>
        ///     生成
        /// </summary>
        /// <param name="owner"></param>
        internal TileCropPageViewInnerModel(TileCropPageViewModel owner)
        {
            this.Owner = owner;
        }
        #endregion

        // - インターナル変更通知プロパティ

        #region プロパティ（タイルセット設定ビューモデル）
        /// <summary>
        ///     タイルセット設定ビューモデル
        /// </summary>
        public TilesetDatatableVisually TilesetSettingsVM => this.Owner.TilesetSettingsVM;
        #endregion

        #region プロパティ（［タイルセット・データテーブル］　関連）
        /// <summary>
        ///     ［タイルセット・データテーブル］ファイルの場所
        ///     <list type="bullet">
        ///         <item>ページの引数として使用</item>
        ///     </list>
        /// </summary>
        public TheFileEntryLocations.UnityAssets.DataCsvTilesetCsv TilesetDatatableFileLocation
        {
            get => this.Owner.TilesetDatatableFileLocation;
            set
            {
                this.Owner.TilesetDatatableFileLocation = value;
            }
        }
        #endregion

        // - インターナル・プロパティ

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
                this.Owner.InvalidateTileIdChange();
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

        #region プロパティ（切抜きカーソルと、既存タイルが交差しているか？）
        /// <summary>
        ///     切抜きカーソルと、既存タイルが交差しているか？
        /// </summary>
        /// <returns>そうだ</returns>
        internal bool HasIntersectionBetweenCroppedCursorAndRegisteredTile { get; set; }
        #endregion

        #region プロパティ（切抜きカーソルと、既存タイルは合同か？）
        /// <summary>
        ///     切抜きカーソルと、既存タイルは合同か？
        /// </summary>
        /// <returns>そうだ</returns>
        internal bool IsCongruenceBetweenCroppedCursorAndRegisteredTile { get; set; }
        #endregion

        #region プロパティ（ポインティング・デバイス押下開始位置）
        /// <summary>
        ///     ポインティング・デバイス押下開始位置
        /// </summary>
        internal Models.Geometric.PointFloat PointingDeviceStartPoint { get; set; }
        #endregion

        #region プロパティ（ポインティング・デバイス現在位置）
        /// <summary>
        ///     ポインティング・デバイス現在位置
        /// </summary>
        internal Models.Geometric.PointFloat PointingDeviceCurrentPoint { get; set; }
        #endregion

        #region プロパティ（［ズーム］　関連）
        /// <summary>
        ///     ズーム
        ///     
        ///     <list type="bullet">
        ///         <item>セッターは画像を再生成する重たい処理なので、スパムしないように注意</item>
        ///         <item>コード・ビハインドで使用</item>
        ///     </list>
        /// </summary>
        public Models.Geometric.Zoom Zoom
        {
            get => this.Owner.Zoom;
            set
            {
                this.Owner.Zoom = value;
            }
        }
        #endregion

        // - インターナル変更通知メソッド

        #region 変更通知メソッド（［選択タイル］　関連）
        /// <summary>
        ///     ［選択タイル］Ｉｄの再描画
        /// </summary>
        internal void InvalidateTileIdChange() => this.Owner.InvalidateTileIdChange();
        #endregion

        /// <summary>
        ///     <pre>
        ///         ［元画像グリッド］のキャンバスの再描画
        /// 
        ///         TRICK:  GraphicsView を再描画させたいが、ビューモデルから要求する方法が分からない。
        ///                 そこで、内部的なグリッド画像の横幅が偶数のときは +1、奇数のときは -1 して
        ///                 振動させることで、再描画を呼び起こすことにする
        ///     </pre>
        /// </summary>
        internal void InvalidateForTileAdd() => this.Owner.InvalidateForTileAdd();

        #region メソッド（［削除］ボタン　関連）
        /// <summary>
        ///     ［削除］ボタンの再描画
        /// </summary>
        internal void InvalidateDeletesButton() => this.Owner.InvalidateDeletesButton();
        #endregion

        // - インターナル・メソッド

        #region メソッド（ロケール変更による再描画）
        /// <summary>
        ///     ロケール変更による再描画
        ///     
        ///     <list type="bullet">
        ///         <item>動的にテキストを変えている部分に対応するため</item>
        ///     </list>
        /// </summary>
        internal void InvalidateLocale() => this.Owner.InvalidateAddsButton();
        #endregion

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

                // ［追加／上書き］ボタン再描画
                this.Owner.InvalidateAddsButton();

                // ［削除］ボタン再描画
                this.Owner.InvalidateDeletesButton();
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
            this.Owner.InvalidateTileIdChange();

            Trace.WriteLine($"[TileCropPageViewModel.cs UpdateCroppedCursorPointedTileByDifference] CroppedCursorPointedTileRecordVisually.Dump(): {this.CroppedCursorPointedTileRecordVisually.Dump()}");
        }
        #endregion

        #region メソッド（［追加／上書き］　関連）
        /// <summary>
        ///     ［追加］
        /// </summary>
        internal void AddRegisteredTile()
        {
            var contents = this.TargetTileRecordVisually;

            TileIdOrEmpty tileIdOrEmpty;

            // Ｉｄが空欄
            // ［追加］（新規作成）だ

            // ［切抜きカーソル］にサイズがなければ、何もしない
            if (contents.IsNone)
                return;

            // 新しいタイルＩｄを発行
            tileIdOrEmpty = this.Owner.TilesetSettingsVM.UsableId;
            this.Owner.TilesetSettingsVM.IncreaseUsableId();

            // 追加でも、上書きでも、同じ処理でいける
            // ［登録タイル追加］処理
            App.History.Do(new AddRegisteredTileProcessing(
                inner: this,
                croppedCursorVisually: contents,
                tileIdOrEmpty: tileIdOrEmpty,
                workingRectangle: contents.SourceRectangle.Do(this.Owner.Zoom)));

            this.Owner.InvalidateForHistory();
        }

        /// <summary>
        ///     ［上書き］
        /// </summary>
        public void OverwriteRegisteredTile()
        {
            var contents = this.TargetTileRecordVisually;

            TileIdOrEmpty tileIdOrEmpty;

            // ［切抜きカーソル］にサイズがなければ、何もしない
            if (contents.IsNone)
                return;

            // Ｉｄが空欄でない
            // ［上書き］（更新）だ
            tileIdOrEmpty = this.CroppedCursorPointedTileIdOrEmpty;

            // 追加でも、上書きでも、同じ処理でいける
            // ［登録タイル追加］処理
            App.History.Do(new AddRegisteredTileProcessing(
                inner: this.Owner.Inner,
                croppedCursorVisually: contents,
                tileIdOrEmpty: tileIdOrEmpty,
                workingRectangle: contents.SourceRectangle.Do(this.Owner.Zoom)));

            this.Owner.InvalidateForHistory();
        }

        /// <summary>
        ///     ［登録タイル］削除
        /// </summary>
        public void RemoveRegisteredTile()
        {
            App.History.Do(new RemoveRegisteredTileProcessing(
                owner: this.Owner,
                tileIdOrEmpty: this.CroppedCursorPointedTileIdOrEmpty));

            this.Owner.InvalidateForHistory();
        }
        #endregion

        #region メソッド（画面遷移でこの画面に戻ってきた時）
        /// <summary>
        ///     画面遷移でこの画面に戻ってきた時
        /// </summary>
        internal void ReactOnVisited()
        {
            // ロケールが変わってるかもしれないので反映
            this.Owner.InvalidateCultureInfo();

            // グリッド・キャンバス
            {
                // グリッドの左上位置（初期値）
                this.Owner.GridPhaseSourceLocation = new Models.Geometric.PointInt(new Models.Geometric.XInt(0), new Models.Geometric.YInt(0));

                // グリッドのタイルサイズ（初期値）
                this.Owner.SourceGridUnit = new Models.Geometric.SizeInt(new Models.Geometric.WidthInt(32), new Models.Geometric.HeightInt(32));

                // グリッド・キャンバス画像の再作成
                this.Owner.RemakeGridCanvasImage();
            }
        }
        #endregion

        // - プライベート・プロパティ

        TileCropPageViewModel Owner { get; }

        // - プライベート・フィールド

        #region フィールド（［タイルセット元画像］　関連）
        /// <summary>
        ///     ［タイルセット元画像］サイズ
        /// </summary>
        Models.Geometric.SizeInt tilesetSourceImageSize = Models.Geometric.SizeInt.Empty;
        #endregion
    }
}
