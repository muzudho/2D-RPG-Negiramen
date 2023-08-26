using Microsoft.Maui.Controls.Shapes;

namespace _2D_RPG_Negiramen.Specifications.TileCropPage
{
    using _2D_RPG_Negiramen.Models;
    using _2D_RPG_Negiramen.Models.Geometric;
    using _2D_RPG_Negiramen.Models.Visually;
    using _2D_RPG_Negiramen.ViewModels;
    using SkiaSharp;
    using SkiaSharp.Views.Maui.Controls;
    using System.Diagnostics;
    using TheFileEntryLocations = Models.FileEntries.Locations;
    using TheGraphics = Microsoft.Maui.Graphics;
    using TheGeometric = _2D_RPG_Negiramen.Models.Geometric;

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
    class ItsSpec : IItsSpec
    {
        // - その他

        #region その他（生成）
        /// <summary>
        ///     生成
        /// </summary>
        /// <param name="wholePageVM">全体ページビュー・モデル</param>
        internal ItsSpec(TileCropPageViewModel wholePageVM)
        {
            WholePageVM = wholePageVM;

            this.CultureInfo = new InnerCultureInfo(this);
            this.Zoom = new InnerZoom(this);
            this.GridUnit = new GridUnit(this);
            this.PointingDevice = new InnerPointingDevice(this);
            this.CropCursor = new CropCursor(this);
            this.CropTile = new CropTile(this);
            this.AddsButton = new AddsButton(this, this);
            this.DeletesButton = new DeletesButton(this);
        }
        #endregion

        // - パブリック変更通知プロパティ

        #region プロパティ（タイルセット設定ビューモデル）
        /// <summary>
        ///     タイルセット設定ビューモデル
        /// </summary>
        public TilesetDatatableVisually WholeTilesetSettingsVM => WholePageVM.TilesetSettingsVM;

        public bool WholeTilesetSettingsVMSaveCsv(TheFileEntryLocations.UnityAssets.DataCsvTilesetCsv tileSetSettingsFile)
        {
            return this.WholeTilesetSettingsVM.SaveCSV(tileSetSettingsFile);
        }
        public bool WholeTilesetSettingsVMTryGetTileById(TileIdOrEmpty tileId, out TileRecordVisually? resultVisuallyOrNull)
        {
            return this.WholeTilesetSettingsVM.TryGetTileById(tileId, out resultVisuallyOrNull);
        }
        public void WholeTilesetSettingsVMAddTileVisually(TileIdOrEmpty id,
            TheGeometric.RectangleInt rect,
            Zoom zoom,
            TileTitle title,
            LogicalDelete logicalDelete)
        {
            this.WholeTilesetSettingsVM.AddTileVisually(id, rect, zoom, title, logicalDelete);
        }
        public bool WholeTilesetSettingsVMTryRemoveTileById(TileIdOrEmpty tileId, out TileRecordVisually? resultVisuallyOrNull)
        {
            return this.WholeTilesetSettingsVM.TryRemoveTileById(tileId, out resultVisuallyOrNull);
        }
        #endregion

        #region プロパティ（［タイルセット・データテーブル］　関連）
        /// <summary>
        ///     ［タイルセット・データテーブル］ファイルの場所
        ///     <list type="bullet">
        ///         <item>ページの引数として使用</item>
        ///     </list>
        /// </summary>
        public TheFileEntryLocations.UnityAssets.DataCsvTilesetCsv WholeTilesetDatatableFileLocation
        {
            get => WholePageVM.TilesetDatatableFileLocation;
            set
            {
                WholePageVM.TilesetDatatableFileLocation = value;
            }
        }
        #endregion

        public HeightFloat WholeCroppedCursorPointedTileWorkingHeight
        {
            get => WholePageVM.CroppedCursorPointedTileWorkingHeight;
            set
            {
                WholePageVM.CroppedCursorPointedTileWorkingHeight = value;
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
        public RectangleInt WholeCroppedCursorPointedTileSourceRect
        {
            get => WholePageVM.CroppedCursorPointedTileSourceRect;
            set
            {
                WholePageVM.CroppedCursorPointedTileSourceRect = value;
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
        public float WholeZoomAsFloat
        {
            get => WholePageVM.ZoomAsFloat;
            set
            {
                WholePageVM.ZoomAsFloat = value;
            }
        }
        #endregion

        // - インターナル・プロパティ

        /// <summary>全体ページ・ビューモデル</summary>
        internal TileCropPageViewModel WholePageVM { get; }
        public string WholePageVMAddsButtonText
        {
            get => this.WholePageVM.AddsButtonText;
            set => this.WholePageVM.AddsButtonText = value;
        }
        public string WholePageVMAddsButtonHint
        {
            get => this.WholePageVM.AddsButtonHint;
        }

        /// <summary>文化情報</summary>
        internal InnerCultureInfo CultureInfo { get; }

        #region プロパティ（［タイルセット元画像］　関連）
        /// <summary>
        ///     ［タイルセット元画像］のサイズ
        /// </summary>
        internal SizeInt TilesetSourceImageSize
        {
            get => tilesetSourceImageSize;
            set
            {
                tilesetSourceImageSize = value;
            }
        }
        #endregion

        #region プロパティ（［タイルセット作業画像］　関連）
        /// <summary>
        ///     ［タイルセット作業画像］ファイルへのパス（文字列形式）
        /// </summary>
        public string TilesetWorkingImageFilePathAsStr => App.CacheFolder.YourCircleFolder.YourWorkFolder.ImagesFolder.WorkingTilesetPng.Path.AsStr;
        #endregion

        /// <summary>切抜きカーソル</summary>
        internal CropCursor CropCursor { get; }

        #region プロパティ（切抜きカーソルが指すタイル）
        /// <summary>切抜きカーソルが指すタイル</summary>
        internal CropTile CropTile { get; }
        #endregion

        public TileRecordVisually CropTileTargetTileRecordVisually => this.CropTile.TargetTileRecordVisually;
        public TileIdOrEmpty CropTileIdOrEmpty
        {
            get
            {
                return this.CropTile.IdOrEmpty;
            }
            set
            {
                this.CropTile.IdOrEmpty = value;
            }
        }

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

        /// <summary>ズーム</summary>
        internal InnerZoom Zoom { get; }

        public Zoom ZoomValue
        {
            get
            {
                return this.Zoom.Value;
            }
        }

        /// <summary>グリッド単位</summary>
        internal GridUnit GridUnit { get; }

        /// <summary>ポインティング・デバイス</summary>
        internal InnerPointingDevice PointingDevice { get; }

        /// <summary>追加ボタン</summary>
        internal AddsButton AddsButton { get; }

        /// <summary>削除ボタン</summary>
        internal DeletesButton DeletesButton { get; }
        public void DeletesButtonRefresh()
        {
            this.DeletesButton.Refresh();
        }

        // - インターナル変更通知メソッド

        #region パブリック変更通知メソッド（［選択タイル］　関連）
        /// <summary>
        ///     ［選択タイル］Ｉｄの再描画
        /// </summary>
        public void WholeInvalidateTileIdChange() => WholePageVM.InvalidateTileIdChange();
        #endregion

        #region 変更通知メソッド（［タイルセット設定］　関連）
        /// <summary>
        ///     ［タイルセット設定］ビューモデルに変更あり
        /// </summary>
        internal void WholeInvalidateTilesetSettingsVM() => WholePageVM.InvalidateTilesetSettingsVM();
        #endregion

        #region 変更通知メソッド（［履歴］）
        /// <summary>
        ///     ［履歴］
        /// </summary>
        internal void WholeInvalidateForHistory() => WholePageVM.InvalidateForHistory();
        #endregion

        // - インターナル・メソッド

        #region 変更通知メソッド（ロケール変更による再描画）
        /// <summary>
        ///     ロケール変更による再描画
        ///     
        ///     <list type="bullet">
        ///         <item>動的にテキストを変えている部分に対応するため</item>
        ///     </list>
        /// </summary>
        internal void InvalidateByLocale() => this.AddsButton.Refresh();
        #endregion

        #region メソッド（［切抜きカーソル］と［切抜きカーソルが指すタイル］　関連）
        /// <summary>
        ///     ［切抜きカーソルが指すタイル］の読込
        /// </summary>
        internal void LoadCroppedCursorPointedTile()
        {
            WholeTilesetSettingsVM.MatchByRectangle(
                sourceRect: WholeCroppedCursorPointedTileSourceRect,
                some: (tileVisually) =>
                {
                    // Trace.WriteLine($"[TileCropPage.xml.cs TapGestureRecognizer_Tapped] タイルは登録済みだ。 Id:{tileVisually.Id.AsInt}, X:{tileVisually.SourceRectangle.Location.X.AsInt}, Y:{recordVM.SourceRectangle.Location.Y.AsInt}, Width:{recordVM.SourceRectangle.Size.Width.AsInt}, Height:{recordVM.SourceRectangle.Size.Height.AsInt}, Title:{recordVM.Title.AsStr}");

                    // タイルを指す（論理削除されているものも含む）
                    this.CropTile.TargetTileRecordVisually = tileVisually;
                },
                none: () =>
                {
                    // Trace.WriteLine("[TileCropPage.xml.cs TapGestureRecognizer_Tapped] 未登録のタイルだ");

                    //
                    // 空欄にする
                    // ==========
                    //

                    // 選択中のタイルの矩形だけ維持し、タイル・コードと、コメントを空欄にする
                    this.CropTile.TargetTileRecordVisually = TileRecordVisually.FromModel(
                        tileRecord: new TileRecord(
                            id: TileIdOrEmpty.Empty,
                            rect: WholeCroppedCursorPointedTileSourceRect,
                            title: TileTitle.Empty,
                            logicalDelete: LogicalDelete.False),
                        zoom: this.Zoom.Value
#if DEBUG
                        , hint: "[TileCropPageViewModel.cs LoadCroppedCursorPointedTile]"
#endif
                        );
                },
                // 論理削除されているものも選択できることとする（復元、論理削除の解除のため）
                includeLogicalDelete: true);
        }
        #endregion

        #region メソッド（画面遷移でこの画面に戻ってきた時）
        /// <summary>
        ///     画面遷移でこの画面に戻ってきた時
        /// </summary>
        internal void ReactOnVisited()
        {
            // ロケールが変わってるかもしれないので反映
            WholePageVM.InvalidateCultureInfo();

            // グリッド・キャンバス
            {
                // グリッドの左上位置（初期値）
                WholePageVM.GridPhaseSourceLocation = new PointInt(new XInt(0), new YInt(0));

                // グリッドのタイルサイズ（初期値）
                WholePageVM.SourceGridUnit = new SizeInt(new WidthInt(32), new HeightInt(32));

                // グリッド・キャンバス画像の再作成
                WholeRemakeGridCanvasImage();
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
        internal void WholeRemakeWorkingTilesetImage()
        {
            // 元画像をベースに、作業画像を複製
            var temporaryBitmap = SKBitmap.FromImage(SKImage.FromBitmap(WholePageVM.TilesetSourceBitmap));

            // 画像処理（明度を下げる）
            FeatSkia.ReduceBrightness.DoItInPlace(temporaryBitmap);

            // 作業画像のサイズ計算
            WholePageVM.workingImageSize = new SizeInt(
                width: new WidthInt((int)(WholeZoomAsFloat * TilesetSourceImageSize.Width.AsInt)),
                height: new HeightInt((int)(WholeZoomAsFloat * TilesetSourceImageSize.Height.AsInt)));

            // 作業画像のリサイズ
            WholePageVM.TilesetWorkingBitmap = temporaryBitmap.Resize(
                size: new SKSizeI(
                    width: WholePageVM.workingImageSize.Width.AsInt,
                    height: WholePageVM.workingImageSize.Height.AsInt),
                quality: SKFilterQuality.Medium);

            WholePageVM.InvalidateTilesetWorkingImage();
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
        public void WholeRefreshForTileAdd()
        {
            if (WholePageVM.TilesetWorkingImageWidthAsInt % 2 == 1)
            {
                WholePageVM.workingImageSize = new SizeInt(
                    width: new WidthInt(WholePageVM.workingImageSize.Width.AsInt - 1),
                    height: new HeightInt(WholePageVM.workingImageSize.Height.AsInt));
            }
            else
            {
                WholePageVM.workingImageSize = new SizeInt(
                    width: new WidthInt(WholePageVM.workingImageSize.Width.AsInt + 1),
                    height: new HeightInt(WholePageVM.workingImageSize.Height.AsInt));
            }

            // タイル タイトル
            WholePageVM.InvalidateTileTitle();

            // 追加・削除ボタンの表示状態を更新したい
            WholePageVM.InvalidateAddsButton();

            // タイルセット作業画像
            WholePageVM.InvalidateTilesetWorkingImage();
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
        internal void WholeRemakeGridCanvasImage()
        {
            WholePageVM.GridCanvasImageSize = new SizeInt(
                width: new WidthInt((int)(WholeZoomAsFloat * TilesetSourceImageSize.Width.AsInt) + 2 * WholePageVM.HalfThicknessOfGridLineAsInt),
                height: new HeightInt((int)(WholeZoomAsFloat * TilesetSourceImageSize.Height.AsInt) + 2 * WholePageVM.HalfThicknessOfGridLineAsInt));
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
        internal void WholeRecalculateBetweenCropCursorAndRegisteredTile()
        {
            if (WholeCroppedCursorPointedTileSourceRect == RectangleInt.Empty)
            {
                // カーソルが無ければ、交差も無い。合同ともしない
                HasIntersectionBetweenCroppedCursorAndRegisteredTile = false;
                IsCongruenceBetweenCroppedCursorAndRegisteredTile = false;
                return;
            }

            // 軽くはない処理
            HasIntersectionBetweenCroppedCursorAndRegisteredTile = WholeTilesetSettingsVM.HasIntersection(WholeCroppedCursorPointedTileSourceRect);
            IsCongruenceBetweenCroppedCursorAndRegisteredTile = WholeTilesetSettingsVM.IsCongruence(WholeCroppedCursorPointedTileSourceRect);

            Trace.WriteLine($"[TileCropPageViewModel.cs RecalculateBetweenCroppedCursorAndRegisteredTile] HasIntersectionBetweenCroppedCursorAndRegisteredTile: {HasIntersectionBetweenCroppedCursorAndRegisteredTile}, IsCongruenceBetweenCroppedCursorAndRegisteredTile: {IsCongruenceBetweenCroppedCursorAndRegisteredTile}");
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
            RectangleFloat workingRect = CoordinateHelper.GetCursorRectangle(
                startPoint: this.PointingDevice.StartPoint,
                endPoint: this.PointingDevice.CurrentPoint,
                gridLeftTop: WholePageVM.WorkingGridPhase,
                gridTile: WholePageVM.WorkingGridUnit);

            // ズームを除去
            var sourceRect = new RectangleInt(
                location: new PointInt(
                    x: new XInt((int)(workingRect.Location.X.AsFloat / WholeZoomAsFloat)),
                    y: new YInt((int)(workingRect.Location.Y.AsFloat / WholeZoomAsFloat))),
                size: new SizeInt(
                    width: new WidthInt((int)(workingRect.Size.Width.AsFloat / WholeZoomAsFloat)),
                    height: new HeightInt((int)(workingRect.Size.Height.AsFloat / WholeZoomAsFloat))));

            //
            // 計算値の反映
            // ============
            //
            // Trace.WriteLine($"[TileCropPage.xaml.cs RefreshTileForm] context.IsMouseDragging: {context.IsMouseDragging}, context.HalfThicknessOfTileCursorLine.AsInt: {context.HalfThicknessOfTileCursorLine.AsInt}, rect x:{rect.Point.X.AsInt} y:{rect.Point.Y.AsInt} width:{rect.Size.Width.AsInt} height:{rect.Size.Height.AsInt}");
            WholeCroppedCursorPointedTileSourceRect = sourceRect;

            //
            // 登録済みのタイルと被っていないか判定
            // ====================================
            //
            //      - （軽くない処理）
            //
            WholeRecalculateBetweenCropCursorAndRegisteredTile();

            //
            // 切抜きカーソル更新
            // ==================
            //
            LoadCroppedCursorPointedTile();

            // （切抜きカーソル更新後）［追加／上書き］ボタン再描画
            this.AddsButton.Refresh();

            // （切抜きカーソル更新後）［削除］ボタン活性化
            this.DeletesButton.Refresh();

            // ［追加／復元］ボタン
            WholePageVM.InvalidateAddsButton();

            // タイル・タイトル
            WholePageVM.InvalidateTileTitle();
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

            ReactOnVisited();

            //
            // タイル設定ファイルの読込
            // ========================
            //
            if (TilesetDatatableVisually.LoadCSV(
                tilesetDatatableFileLocation: WholeTilesetDatatableFileLocation,
                zoom: this.Zoom.Value,
                tilesetDatatableVisually: out TilesetDatatableVisually tilesetDatatableVisually))
            {
                WholePageVM.TilesetSettingsVM = tilesetDatatableVisually;

#if DEBUG
                // ファイルの整合性チェック（重い処理）
                if (WholeTilesetSettingsVM.IsValid())
                {
                    Trace.WriteLine($"[TileCropPage.xaml.cs ContentPage_Loaded] ファイルの内容は妥当　File: {WholeTilesetDatatableFileLocation.Path.AsStr}");
                }
                else
                {
                    Trace.WriteLine($"[TileCropPage.xaml.cs ContentPage_Loaded] ファイルの内容に異常あり　File: {WholeTilesetDatatableFileLocation.Path.AsStr}");
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
            var tilesetImageFilePathAsStr = WholePageVM.TilesetImageFilePathAsStr;

            //
            // タイルセット画像の読込、作業中タイルセット画像の書出
            // ====================================================
            //
            var task = Task.Run(async () =>
            {
                try
                {
                    // タイルセット読込（読込元：　ウィンドウズ・ローカルＰＣ）
                    using (Stream inputFileStream = File.OpenRead(tilesetImageFilePathAsStr))
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
                            using (Stream outputFileStream = File.Open(folder.WorkingTilesetPng.Path.AsStr, FileMode.OpenOrCreate))
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
                    using (Stream inputFileStream = File.OpenRead(tilesetImageFilePathAsStr))
                    {
                        // ↓ １つのストリームが使えるのは、１回切り
                        using (var memStream = new MemoryStream())
                        {
                            await inputFileStream.CopyToAsync(memStream);
                            memStream.Seek(0, SeekOrigin.Begin);

                            // 元画像
                            WholePageVM.SetTilesetSourceBitmap(SKBitmap.Decode(memStream));

                            // 複製
                            WholePageVM.TilesetWorkingBitmap = SKBitmap.FromImage(SKImage.FromBitmap(WholePageVM.TilesetSourceBitmap));

                            // 画像処理（明度を下げる）
                            FeatSkia.ReduceBrightness.DoItInPlace(WholePageVM.TilesetWorkingBitmap);
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
            if (this.CropTile.IdOrEmpty == TileIdOrEmpty.Empty)
            {
                // Ｉｄが空欄
                // ［追加］（新規作成）だ

                // 登録タイル追加
                this.AddsButton.AddTile();
            }
            else
            {
                // 上書きボタンだが、［上書き］処理をする
                this.AddsButton.OverwriteTile();
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
            WholePageVM.IsMouseDragging = !WholePageVM.IsMouseDragging;

            if (WholePageVM.IsMouseDragging)
            {
                //
                // 疑似マウス・ダウン
                // ==================
                //
                Trace.WriteLine("[TileCropPage.xml.cs TileImage_OnTapped] 疑似マウス・ダウン");

                // ポイントしている位置
                this.PointingDevice.CurrentPoint = this.PointingDevice.StartPoint = new PointFloat(
                    new XFloat((float)tappedPoint.X),
                    new YFloat((float)tappedPoint.Y));
                // Trace.WriteLine($"[TileCropPage TileImage_OnTapped] tapped x:{PointingDeviceStartPoint.X.AsInt} y:{PointingDeviceStartPoint.Y.AsInt}");

                // タイル・フォームの表示更新
                RefreshTileForm();

                this.CropCursor.RefreshCanvasTrick(codePlace: "[TileCropPage.xml.cs TileImage_OnTapped 疑似マウスダウン]");
            }
            else
            {
                //
                // 疑似マウス・アップ
                // ==================
                //

                Trace.WriteLine("[TileCropPage.xml.cs TileImage_OnTapped] 疑似マウス・アップ");

                // ポイントしている位置
                this.PointingDevice.CurrentPoint = new PointFloat(
                    new XFloat((float)tappedPoint.X),
                    new YFloat((float)tappedPoint.Y));
                // Trace.WriteLine($"[TileCropPage PointerGestureRecognizer_PointerExited] exited x:{PointingDeviceCurrentPoint.X.AsInt} y:{PointingDeviceCurrentPoint.Y.AsInt}");

                // タイル・フォームの表示更新
                RefreshTileForm();

                this.CropCursor.RefreshCanvasTrick(codePlace: "[TileCropPage.xml.cs TileImage_OnTapped 疑似マウスアップ]");
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
            if (WholePageVM.IsMouseDragging)
            {
                //
                // 疑似マウス・ドラッグ
                // ====================
                //

                // ポイントしている位置
                this.PointingDevice.CurrentPoint = new PointFloat(
                    new XFloat((float)tappedPoint.X),
                    new YFloat((float)tappedPoint.Y));
                // Trace.WriteLine($"[TileCropPage PointerGestureRecognizer_PointerMoved] moved x:{PointingDeviceCurrentPoint.X.AsInt} y:{PointingDeviceCurrentPoint.Y.AsInt}");

                // タイル・フォームの表示更新
                RefreshTileForm();

                this.CropCursor.RefreshCanvasTrick(codePlace: "[TileCropPage.xml.cs PointerGestureRecognizer_PointerMoved 疑似マウスドラッグ]");
            }
        }
        #endregion

        // - プライベート・フィールド

        #region フィールド（［タイルセット元画像］　関連）
        /// <summary>
        ///     ［タイルセット元画像］サイズ
        /// </summary>
        SizeInt tilesetSourceImageSize = SizeInt.Empty;
        #endregion
    }
}
