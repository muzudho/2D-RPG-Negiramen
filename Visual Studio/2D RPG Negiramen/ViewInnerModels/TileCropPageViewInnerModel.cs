namespace _2D_RPG_Negiramen.ViewInnerModels
{
    using _2D_RPG_Negiramen.Models;
    using _2D_RPG_Negiramen.Models.Geometric;
    using _2D_RPG_Negiramen.Models.Visually;
    using _2D_RPG_Negiramen.ViewHistory.TileCropPage;
    using _2D_RPG_Negiramen.ViewModels;
    using SkiaSharp;
    using SkiaSharp.Views.Maui.Controls;
    using System.Diagnostics;
    using System.Globalization;
    using TheFileEntryLocations = _2D_RPG_Negiramen.Models.FileEntries.Locations;
    using TheGeometric = _2D_RPG_Negiramen.Models.Geometric;
    using TheGraphics = Microsoft.Maui.Graphics;

#if IOS || ANDROID || MACCATALYST
    using Microsoft.Maui.Graphics.Platform;
#elif WINDOWS
    using Microsoft.Maui.Graphics.Win2D;
    using System.Net;
#endif

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

        // - パブリック変更通知プロパティ

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

        #region 変更通知プロパティ（ロケール　関連）
        /// <summary>
        ///     現在選択中の文化情報。文字列形式
        /// </summary>
        public CultureInfo SelectedCultureInfo
        {
            get => this.Owner.SelectedCultureInfo;
            set
            {
                this.Owner.SelectedCultureInfo = value;
            }
        }
        #endregion

        #region 変更通知プロパティ（ポインティング・デバイス押下中か？）
        /// <summary>
        ///     ポインティング・デバイス押下中か？
        /// 
        ///     <list type="bullet">
        ///         <item>タイルを選択開始していて、まだ未確定だ</item>
        ///         <item>マウスじゃないと思うけど</item>
        ///     </list>
        /// </summary>
        public bool IsMouseDragging
        {
            get => this.isMouseDragging;
            set
            {
                if (this.isMouseDragging != value)
                {
                    this.isMouseDragging = value;
                    this.Owner.InvalidateIsMouseDragging();
                }
            }
        }
        #endregion

        public Models.Geometric.WidthFloat CroppedCursorPointedTileWorkingWidthWithoutTrick
        {
            get => this.Owner.CroppedCursorPointedTileWorkingWidthWithoutTrick;
            set
            {
                this.Owner.CroppedCursorPointedTileWorkingWidthWithoutTrick = value;
            }
        }

        public Models.Geometric.HeightFloat CroppedCursorPointedTileWorkingHeight
        {
            get => this.Owner.CroppedCursorPointedTileWorkingHeight;
            set
            {
                this.Owner.CroppedCursorPointedTileWorkingHeight = value;
            }
        }

        #region 変更通知プロパティ（［切抜きカーソルが指すタイル］　関連）
        /// <summary>
        ///     ［切抜きカーソルが指すタイル］の元画像ベースの矩形
        ///     
        ///     <list type="bullet">
        ///         <item>カーソルが無いとき、大きさの無いカーソルを返す</item>
        ///     </list>
        /// </summary>
        public Models.Geometric.RectangleInt CroppedCursorPointedTileSourceRect
        {
            get => this.Owner.CroppedCursorPointedTileSourceRect;
            set
            {
                this.Owner.CroppedCursorPointedTileSourceRect = value;
            }
        }
        #endregion

        #region 変更通知プロパティ（［ズーム］　関連）
        /// <summary>
        ///     ［ズーム］整数形式
        ///     
        ///     <list type="bullet">
        ///         <item>セッターは画像を再生成する重たい処理なので、スパムしないように注意</item>
        ///     </list>
        /// </summary>
        public float OwnerZoomAsFloat
        {
            get => this.Owner.ZoomAsFloat;
            set
            {
                this.Owner.ZoomAsFloat = value;
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
            get => this.zoom;
            set
            {
                if (this.zoom == value)
                    return;

                // TODO 循環参照しやすいから、良くないコード
                this.Owner.ZoomAsFloat = value.AsFloat;
            }
        }

        /// <summary>
        ///     ［ズーム］整数形式
        ///     
        ///     <list type="bullet">
        ///         <item>セッターは画像を再生成する重たい処理なので、スパムしないように注意</item>
        ///     </list>
        /// </summary>
        public float ZoomAsFloat
        {
            get => this.zoom.AsFloat;
            set
            {
                if (this.zoom.AsFloat != value)
                {
                    if (this.Owner.ZoomMinAsFloat <= value && value <= this.Owner.ZoomMaxAsFloat)
                    {
                        Zoom oldValue = this.zoom;
                        Zoom newValue = new Models.Geometric.Zoom(value);

                        this.zoom = newValue;
                        this.TrickRefreshCanvasOfTileCursor("[TileCropPageViewModel.cs ZoomAsFloat]");

                        // 再帰的にズーム再変更、かつ変更後の影響を処理
                        App.History.Do(new ZoomProcessing(this, oldValue, newValue));
                    }
                }
            }
        }

        /// <summary>
        ///     ズーム最大
        /// </summary>
        public float ZoomMaxAsFloat => this.zoomMax.AsFloat;

        /// <summary>
        ///     ズーム最小
        /// </summary>
        public float ZoomMinAsFloat => this.zoomMin.AsFloat;
        #endregion

        // - インターナル変更通知メソッド

        #region 変更通知メソッド（［選択タイル］　関連）
        /// <summary>
        ///     ［選択タイル］Ｉｄの再描画
        /// </summary>
        internal void InvalidateTileIdChange() => this.Owner.InvalidateTileIdChange();
        #endregion

        #region 変更通知メソッド（［タイルセット設定］　関連）
        /// <summary>
        ///     ［タイルセット設定］ビューモデルに変更あり
        /// </summary>
        internal void InvalidateTilesetSettingsVM() => this.Owner.InvalidateTilesetSettingsVM();
        #endregion

        #region 変更通知メソッド（［履歴］）
        /// <summary>
        ///     ［履歴］
        /// </summary>
        internal void InvalidateForHistory() => this.Owner.InvalidateForHistory();
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
        internal void InvalidateLocale() => this.RefreshAddsButton();
        #endregion

        #region メソッド（［切抜きカーソル］　関連）
        /// <summary>
        ///     <pre>
        ///         ［切抜きカーソル］ズーム済みのキャンバスの再描画
        /// 
        ///         TRICK:  GraphicsView を再描画させたいが、ビューモデルから要求する方法が分からない。
        ///                 そこで、内部的なグリッド画像の横幅が偶数のときは +1、奇数のときは -1 して
        ///                 振動させることで、再描画を呼び起こすことにする
        ///     </pre>
        /// </summary>
        internal void TrickRefreshCanvasOfTileCursor(string codePlace = "[TileCropPageViewModel RefreshCanvasOfTileCursor]")
        {
            if (this.Owner.TrickWidth.AsFloat == 1.0f)
            {
                this.Owner.TrickWidth = WidthFloat.Zero;
            }
            else
            {
                this.Owner.TrickWidth = WidthFloat.One;
            }

            // TRICK CODE:
            this.Owner.InvalidateWorkingTargetTile();
        }

        /// <summary>
        ///     ［切抜きカーソル］の再描画
        ///     
        ///     TODO ★ 設定ファイルからリロードしてる？
        /// </summary>
        internal void LoadCroppedCursorPointedTile()
        {
            this.TilesetSettingsVM.MatchByRectangle(
                sourceRect: this.CroppedCursorPointedTileSourceRect,
                some: (tileVisually) =>
                {
                    // Trace.WriteLine($"[TileCropPage.xml.cs TapGestureRecognizer_Tapped] タイルは登録済みだ。 Id:{tileVisually.Id.AsInt}, X:{tileVisually.SourceRectangle.Location.X.AsInt}, Y:{recordVM.SourceRectangle.Location.Y.AsInt}, Width:{recordVM.SourceRectangle.Size.Width.AsInt}, Height:{recordVM.SourceRectangle.Size.Height.AsInt}, Title:{recordVM.Title.AsStr}");

                    // タイルを指す（論理削除されているものも含む）
                    this.TargetTileRecordVisually = tileVisually;
                },
                none: () =>
                {
                    // Trace.WriteLine("[TileCropPage.xml.cs TapGestureRecognizer_Tapped] 未登録のタイルだ");

                    //
                    // 空欄にする
                    // ==========
                    //

                    // 選択中のタイルの矩形だけ維持し、タイル・コードと、コメントを空欄にする
                    this.TargetTileRecordVisually = TileRecordVisually.FromModel(
                        tileRecord: new Models.TileRecord(
                            id: Models.TileIdOrEmpty.Empty,
                            rect: this.CroppedCursorPointedTileSourceRect,
                            title: Models.TileTitle.Empty,
                            logicalDelete: Models.LogicalDelete.False),
                        zoom: this.Zoom
#if DEBUG
                        , hint: "[TileCropPageViewModel.cs LoadCroppedCursorPointedTile]"
#endif
                        );
                },
                // 論理削除されているものも選択できることとする（復元、論理削除の解除のため）
                includeLogicalDelete: true);
        }
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
                this.RefreshAddsButton();

                // ［削除］ボタン再描画
                this.RefreshDeletesButton();
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
                workingRectangle: contents.SourceRectangle.Do(this.Zoom)));

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
                workingRectangle: contents.SourceRectangle.Do(this.Zoom)));

            this.Owner.InvalidateForHistory();
        }

        /// <summary>
        ///     ［登録タイル］削除
        /// </summary>
        public void RemoveRegisteredTile()
        {
            App.History.Do(new RemoveRegisteredTileProcessing(
                inner: this,
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
                this.RemakeGridCanvasImage();
            }
        }
        #endregion

        #region メソッド（［タイルセット作業画像］　関連）
        /// <summary>
        ///     ［タイルセット作業画像］の再作成
        ///     
        ///     <list type="bullet">
        ///         <item>アンドゥ・リドゥで利用</item>
        ///     </list>
        /// </summary>
        internal void RemakeWorkingTilesetImage()
        {
            // 元画像をベースに、作業画像を複製
            var temporaryBitmap = SkiaSharp.SKBitmap.FromImage(SkiaSharp.SKImage.FromBitmap(this.Owner.TilesetSourceBitmap));

            // 画像処理（明度を下げる）
            FeatSkia.ReduceBrightness.DoItInPlace(temporaryBitmap);

            // 作業画像のサイズ計算
            this.Owner.workingImageSize = new Models.Geometric.SizeInt(
                width: new Models.Geometric.WidthInt((int)(this.OwnerZoomAsFloat * this.TilesetSourceImageSize.Width.AsInt)),
                height: new Models.Geometric.HeightInt((int)(this.OwnerZoomAsFloat * this.TilesetSourceImageSize.Height.AsInt)));

            // 作業画像のリサイズ
            this.Owner.TilesetWorkingBitmap = temporaryBitmap.Resize(
                size: new SKSizeI(
                    width: this.Owner.workingImageSize.Width.AsInt,
                    height: this.Owner.workingImageSize.Height.AsInt),
                quality: SKFilterQuality.Medium);

            this.Owner.InvalidateTilesetWorkingImage();
        }

        /// <summary>
        ///     <pre>
        ///         ［元画像グリッド］のキャンバスの再描画
        /// 
        ///         TRICK:  GraphicsView を再描画させたいが、ビューモデルから要求する方法が分からない。
        ///                 そこで、内部的なグリッド画像の横幅が偶数のときは +1、奇数のときは -1 して
        ///                 振動させることで、再描画を呼び起こすことにする
        ///     </pre>
        /// </summary>
        internal void RefreshForTileAdd()
        {
            if (this.Owner.TilesetWorkingImageWidthAsInt % 2 == 1)
            {
                this.Owner.workingImageSize = new SizeInt(
                    width: new WidthInt(this.Owner.workingImageSize.Width.AsInt - 1),
                    height: new HeightInt(this.Owner.workingImageSize.Height.AsInt));
            }
            else
            {
                this.Owner.workingImageSize = new SizeInt(
                    width: new WidthInt(this.Owner.workingImageSize.Width.AsInt + 1),
                    height: new HeightInt(this.Owner.workingImageSize.Height.AsInt));
            }

            // タイル タイトル
            this.Owner.InvalidateTileTitle();

            // 追加・削除ボタンの表示状態を更新したい
            this.Owner.InvalidateAddsButton();

            // タイルセット作業画像
            this.Owner.InvalidateTilesetWorkingImage();
        }
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
        internal void RemakeGridCanvasImage()
        {
            this.Owner.GridCanvasImageSize = new Models.Geometric.SizeInt(
                width: new Models.Geometric.WidthInt((int)(this.OwnerZoomAsFloat * this.TilesetSourceImageSize.Width.AsInt) + (2 * this.Owner.HalfThicknessOfGridLineAsInt)),
                height: new Models.Geometric.HeightInt((int)(this.OwnerZoomAsFloat * this.TilesetSourceImageSize.Height.AsInt) + (2 * this.Owner.HalfThicknessOfGridLineAsInt)));
        }
        #endregion

        #region メソッド（［作業グリッド］　関連）
        /// <summary>
        ///     ［作業グリッド］タイル横幅の再計算
        ///     
        ///     <list type="bullet">
        ///         <item>アンドゥ・リドゥで利用</item>
        ///     </list>
        /// </summary>
        internal void RefreshWorkingGridTileWidth()
        {
            this.Owner.WorkingGridTileWidthAsFloat = this.OwnerZoomAsFloat * this.Owner.sourceGridUnit.Width.AsInt;

            this.Owner.InvalidateWorkingGrid();
        }

        /// <summary>
        ///     ［作業グリッド］タイル縦幅の再計算
        ///     
        ///     <list type="bullet">
        ///         <item>アンドゥ・リドゥで利用</item>
        ///     </list>
        /// </summary>
        internal void RefreshWorkingGridTileHeight()
        {
            this.Owner.WorkingGridTileHeightAsFloat = this.OwnerZoomAsFloat * this.Owner.sourceGridUnit.Height.AsInt;

            this.Owner.InvalidateWorkingGrid();
        }
        #endregion

        #region メソッド（切抜きカーソルと、既存タイルが交差しているか？合同か？　を再計算）
        /// <summary>
        ///     切抜きカーソルと、既存タイルが交差しているか？合同か？　を再計算
        ///     
        ///     <list type="bullet">
        ///         <item>軽くはない処理</item>
        ///     </list>
        /// </summary>
        internal void RecalculateBetweenCroppedCursorAndRegisteredTile()
        {
            if (this.CroppedCursorPointedTileSourceRect == TheGeometric.RectangleInt.Empty)
            {
                // カーソルが無ければ、交差も無い。合同ともしない
                this.HasIntersectionBetweenCroppedCursorAndRegisteredTile = false;
                this.IsCongruenceBetweenCroppedCursorAndRegisteredTile = false;
                return;
            }

            // 軽くはない処理
            this.HasIntersectionBetweenCroppedCursorAndRegisteredTile = this.TilesetSettingsVM.HasIntersection(this.CroppedCursorPointedTileSourceRect);
            this.IsCongruenceBetweenCroppedCursorAndRegisteredTile = this.TilesetSettingsVM.IsCongruence(this.CroppedCursorPointedTileSourceRect);

            Trace.WriteLine($"[TileCropPageViewModel.cs RecalculateBetweenCroppedCursorAndRegisteredTile] HasIntersectionBetweenCroppedCursorAndRegisteredTile: {this.HasIntersectionBetweenCroppedCursorAndRegisteredTile}, IsCongruenceBetweenCroppedCursorAndRegisteredTile: {this.IsCongruenceBetweenCroppedCursorAndRegisteredTile}");
        }
        #endregion

        #region メソッド（タイル・フォーム更新）
        /// <summary>
        ///     タイル・フォーム更新
        /// </summary>
        internal void RefreshTileForm()
        {
            //
            // ポインティング・デバイスの２箇所のタップ位置から、タイルの矩形を算出
            // ====================================================================
            //

            // ズームしたまま
            RectangleFloat workingRect = Models.CoordinateHelper.GetCursorRectangle(
                startPoint: this.PointingDeviceStartPoint,
                endPoint: this.PointingDeviceCurrentPoint,
                gridLeftTop: this.Owner.WorkingGridPhase,
                gridTile: this.Owner.WorkingGridUnit);

            // ズームを除去
            var sourceRect = new RectangleInt(
                location: new PointInt(
                    x: new XInt((int)(workingRect.Location.X.AsFloat / this.OwnerZoomAsFloat)),
                    y: new YInt((int)(workingRect.Location.Y.AsFloat / this.OwnerZoomAsFloat))),
                size: new SizeInt(
                    width: new WidthInt((int)(workingRect.Size.Width.AsFloat / this.OwnerZoomAsFloat)),
                    height: new HeightInt((int)(workingRect.Size.Height.AsFloat / this.OwnerZoomAsFloat))));

            //
            // 計算値の反映
            // ============
            //
            // Trace.WriteLine($"[TileCropPage.xaml.cs RefreshTileForm] context.IsMouseDragging: {context.IsMouseDragging}, context.HalfThicknessOfTileCursorLine.AsInt: {context.HalfThicknessOfTileCursorLine.AsInt}, rect x:{rect.Point.X.AsInt} y:{rect.Point.Y.AsInt} width:{rect.Size.Width.AsInt} height:{rect.Size.Height.AsInt}");
            this.CroppedCursorPointedTileSourceRect = sourceRect;

            //
            // 登録済みのタイルと被っていないか判定
            // ====================================
            //
            //      - （軽くない処理）
            //
            this.RecalculateBetweenCroppedCursorAndRegisteredTile();

            //
            // 切抜きカーソル更新
            // ==================
            //
            this.LoadCroppedCursorPointedTile();

            // （切抜きカーソル更新後）［追加／上書き］ボタン再描画
            this.RefreshAddsButton();

            // （切抜きカーソル更新後）［削除］ボタン活性化
            this.RefreshDeletesButton();

            // ［追加／復元］ボタン
            this.Owner.InvalidateAddsButton();

            // タイル・タイトル
            this.Owner.InvalidateTileTitle();
        }
        #endregion

        #region メソッド（［追加／上書き］ボタン　関連）
        /// <summary>
        ///     ［追加／上書き］ボタンの再描画
        /// </summary>
        internal void RefreshAddsButton()
        {
            // 切抜きカーソルが、登録済みタイルのいずれかと交差しているか？
            if (this.HasIntersectionBetweenCroppedCursorAndRegisteredTile)
            {
                // 合同のときは「交差中」とは表示しない
                if (!this.IsCongruenceBetweenCroppedCursorAndRegisteredTile)
                {
                    // 「交差中」
                    // Trace.WriteLine("[TileCropPage.xml.cs InvalidateAddsButton] 交差中だ");

                    this.Owner.AddsButtonText = (string)LocalizationResourceManager.Instance["Intersecting"];
                    return;
                }
            }

            var contents = this.CroppedCursorPointedTileRecordVisually;

            if (contents.IsNone)
            {
                // ［切抜きカーソル］の指すタイル無し時

                // 「追加」
                this.Owner.AddsButtonText = (string)LocalizationResourceManager.Instance["Add"];
            }
            else
            {
                // 切抜きカーソル有り時
                // Ｉｄ未設定時

                if (this.CroppedCursorPointedTileIdOrEmpty == Models.TileIdOrEmpty.Empty)
                {
                    // Ｉｄが空欄
                    // ［追加］（新規作成）だ

                    // ［追加」
                    this.Owner.AddsButtonText = (string)LocalizationResourceManager.Instance["Add"];
                }
                else
                {
                    // ［復元」
                    this.Owner.AddsButtonText = (string)LocalizationResourceManager.Instance["Restore"];
                }
            }

            // ［追加／復元］ボタンの活性性
            this.Owner.InvalidateAddsButton();
        }
        #endregion

        #region メソッド（［削除］ボタン　関連）
        /// <summary>
        ///     ［削除］ボタンの再描画
        /// </summary>
        internal void RefreshDeletesButton()
        {
            var contents = this.CroppedCursorPointedTileRecordVisually;

            if (contents.IsNone)
            {
                // 切抜きカーソル無し時
                this.Owner.IsEnabledDeletesButton = false;
                return;
            }

            // 切抜きカーソル有り時
            if (contents.Id == TileIdOrEmpty.Empty)
            {
                // Ｉｄ未設定時
                this.Owner.IsEnabledDeletesButton = false;
            }
            else
            {
                // タイル登録済み時
                this.Owner.IsEnabledDeletesButton = true;
            }
        }
        #endregion

        // - インターナル・イベントハンドラ

        #region イベントハンドラ（別ページから、このページに訪れたときに呼び出される）
        /// <summary>
        ///     別ページから、このページに訪れたときに呼び出される
        /// </summary>
        internal void OnNavigatedTo(SKCanvasView skiaTilesetCanvas1)
        {
            Trace.WriteLine($"[TileCropPage.xaml.cs ContentPage_Loaded] ページ来訪時");

            this.ReactOnVisited();

            //
            // タイル設定ファイルの読込
            // ========================
            //
            if (TilesetDatatableVisually.LoadCSV(
                tilesetDatatableFileLocation: this.TilesetDatatableFileLocation,
                zoom: this.Zoom,
                tilesetDatatableVisually: out TilesetDatatableVisually tilesetDatatableVisually))
            {
                this.Owner.TilesetSettingsVM = tilesetDatatableVisually;

#if DEBUG
                // ファイルの整合性チェック（重い処理）
                if (this.TilesetSettingsVM.IsValid())
                {
                    Trace.WriteLine($"[TileCropPage.xaml.cs ContentPage_Loaded] ファイルの内容は妥当　File: {this.TilesetDatatableFileLocation.Path.AsStr}");
                }
                else
                {
                    Trace.WriteLine($"[TileCropPage.xaml.cs ContentPage_Loaded] ファイルの内容に異常あり　File: {this.TilesetDatatableFileLocation.Path.AsStr}");
                }
#endif

                //// 登録タイルのデバッグ出力
                //foreach (var record in context.TilesetSettings.RecordList)
                //{
                //    Trace.WriteLine($"[TileCropPage.xaml.cs ContentPage_Loaded] Record: {record.Dump()}");
                //}
            }

            //
            // タイルセット画像ファイルへのパスを取得
            //
            var tilesetImageFilePathAsStr = this.Owner.TilesetImageFilePathAsStr;

            //
            // タイルセット画像の読込、作業中タイルセット画像の書出
            // ====================================================
            //
            var task = Task.Run(async () =>
            {
                try
                {
                    // タイルセット読込（読込元：　ウィンドウズ・ローカルＰＣ）
                    using (Stream inputFileStream = System.IO.File.OpenRead(tilesetImageFilePathAsStr))
                    {
#if IOS || ANDROID || MACCATALYST
                    // PlatformImage isn't currently supported on Windows.
                    
                    TheGraphics.IImage image = PlatformImage.FromStream(inputFileStream);
#elif WINDOWS
                        TheGraphics.IImage image = new W2DImageLoadingService().FromStream(inputFileStream);
#endif

                        //
                        // 作業中のタイルセット画像の保存
                        //
                        if (image != null)
                        {
                            // ディレクトリーが無ければ作成する
                            var folder = App.CacheFolder.YourCircleFolder.YourWorkFolder.ImagesFolder;
                            folder.CreateThisDirectoryIfItDoesNotExist();

                            // 書出先（ウィンドウズ・ローカルＰＣ）
                            using (Stream outputFileStream = System.IO.File.Open(folder.WorkingTilesetPng.Path.AsStr, FileMode.OpenOrCreate))
                            {
                                image.Save(outputFileStream);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // TODO エラー対応どうする？
                }

                // ↓ SkiaSharp の流儀
                try
                {
                    // タイルセット読込（読込元：　ウィンドウズ・ローカルＰＣ）
                    using (Stream inputFileStream = System.IO.File.OpenRead(tilesetImageFilePathAsStr))
                    {
                        // ↓ １つのストリームが使えるのは、１回切り
                        using (var memStream = new MemoryStream())
                        {
                            await inputFileStream.CopyToAsync(memStream);
                            memStream.Seek(0, SeekOrigin.Begin);

                            // 元画像
                            this.Owner.SetTilesetSourceBitmap(SkiaSharp.SKBitmap.Decode(memStream));

                            // 複製
                            this.Owner.TilesetWorkingBitmap = SkiaSharp.SKBitmap.FromImage(SkiaSharp.SKImage.FromBitmap(this.Owner.TilesetSourceBitmap));

                            // 画像処理（明度を下げる）
                            FeatSkia.ReduceBrightness.DoItInPlace(this.Owner.TilesetWorkingBitmap);
                        };

                        // 再描画
                        skiaTilesetCanvas1.InvalidateSurface();
                    }
                }
                catch (Exception ex)
                {
                    // TODO エラー対応どうする？
                }
            });

            Task.WaitAll(new Task[] { task });
        }
        #endregion

        #region イベントハンドラ（［追加］ボタン　クリック時）
        /// <summary>
        ///     ［追加］ボタン　クリック時
        /// </summary>
        internal void OnAddsButtonClicked()
        {
            if (this.CroppedCursorPointedTileIdOrEmpty == Models.TileIdOrEmpty.Empty)
            {
                // Ｉｄが空欄
                // ［追加］（新規作成）だ

                // 登録タイル追加
                this.AddRegisteredTile();
            }
            else
            {
                // 上書きボタンだが、［上書き］処理をする
                this.OverwriteRegisteredTile();
            }
        }
        #endregion

        #region イベントハンドラ（タイルセット画像上でタップ時）
        /// <summary>
        ///     タイルセット画像上でタップ時
        /// </summary>
        /// <param name="tappedPoint"></param>
        public void OnTilesetImageTapped(Point tappedPoint)
        {
            // 反転
            this.Owner.IsMouseDragging = !this.Owner.IsMouseDragging;

            if (this.Owner.IsMouseDragging)
            {
                //
                // 疑似マウス・ダウン
                // ==================
                //
                Trace.WriteLine("[TileCropPage.xml.cs TileImage_OnTapped] 疑似マウス・ダウン");

                // ポイントしている位置
                this.PointingDeviceCurrentPoint = this.PointingDeviceStartPoint = new Models.Geometric.PointFloat(
                    new Models.Geometric.XFloat((float)tappedPoint.X),
                    new Models.Geometric.YFloat((float)tappedPoint.Y));
                // Trace.WriteLine($"[TileCropPage TileImage_OnTapped] tapped x:{PointingDeviceStartPoint.X.AsInt} y:{PointingDeviceStartPoint.Y.AsInt}");

                // タイル・フォームの表示更新
                this.RefreshTileForm();

                this.TrickRefreshCanvasOfTileCursor(codePlace: "[TileCropPage.xml.cs TileImage_OnTapped 疑似マウスダウン]");
            }
            else
            {
                //
                // 疑似マウス・アップ
                // ==================
                //

                Trace.WriteLine("[TileCropPage.xml.cs TileImage_OnTapped] 疑似マウス・アップ");

                // ポイントしている位置
                this.PointingDeviceCurrentPoint = new Models.Geometric.PointFloat(
                    new Models.Geometric.XFloat((float)tappedPoint.X),
                    new Models.Geometric.YFloat((float)tappedPoint.Y));
                // Trace.WriteLine($"[TileCropPage PointerGestureRecognizer_PointerExited] exited x:{PointingDeviceCurrentPoint.X.AsInt} y:{PointingDeviceCurrentPoint.Y.AsInt}");

                // タイル・フォームの表示更新
                this.RefreshTileForm();

                this.TrickRefreshCanvasOfTileCursor(codePlace: "[TileCropPage.xml.cs TileImage_OnTapped 疑似マウスアップ]");
            }
        }
        #endregion

        #region イベントハンドラ（タイルセット画像上でポインター移動）
        /// <summary>
        ///     タイルセット画像上でポインター移動
        /// </summary>
        /// <param name="tappedPoint"></param>
        public void OnTilesetImagePointerMove(Point tappedPoint)
        {
            if (this.Owner.IsMouseDragging)
            {
                //
                // 疑似マウス・ドラッグ
                // ====================
                //

                // ポイントしている位置
                this.PointingDeviceCurrentPoint = new Models.Geometric.PointFloat(
                    new Models.Geometric.XFloat((float)tappedPoint.X),
                    new Models.Geometric.YFloat((float)tappedPoint.Y));
                // Trace.WriteLine($"[TileCropPage PointerGestureRecognizer_PointerMoved] moved x:{PointingDeviceCurrentPoint.X.AsInt} y:{PointingDeviceCurrentPoint.Y.AsInt}");

                // タイル・フォームの表示更新
                this.RefreshTileForm();

                this.TrickRefreshCanvasOfTileCursor(codePlace: "[TileCropPage.xml.cs PointerGestureRecognizer_PointerMoved 疑似マウスドラッグ]");
            }
        }
        #endregion

        // - プライベート・プロパティ

        TileCropPageViewModel Owner { get; }

        // - プライベート変更通知フィールド

        #region 変更通知フィールド（ポインティング・デバイス押下中か？）
        /// <summary>
        ///     ポインティング・デバイス押下中か？
        /// </summary>
        bool isMouseDragging;
        #endregion

        // - プライベート・フィールド

        #region フィールド（［ズーム］　関連）
        /// <summary>
        ///     ［ズーム］
        /// </summary>
        Models.Geometric.Zoom zoom = Models.Geometric.Zoom.IdentityElement;

        /// <summary>
        ///     ［ズーム］最大
        /// </summary>
        Models.Geometric.Zoom zoomMax = new(4.0f);

        /// <summary>
        ///     ［ズーム］最小
        /// </summary>
        Models.Geometric.Zoom zoomMin = new(0.5f);
        #endregion

        #region フィールド（［タイルセット元画像］　関連）
        /// <summary>
        ///     ［タイルセット元画像］サイズ
        /// </summary>
        Models.Geometric.SizeInt tilesetSourceImageSize = Models.Geometric.SizeInt.Empty;
        #endregion
    }
}
