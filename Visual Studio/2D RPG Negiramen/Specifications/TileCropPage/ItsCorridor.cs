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
    using System.Globalization;

#if IOS || ANDROID || MACCATALYST
    using Microsoft.Maui.Graphics.Platform;
#elif WINDOWS
    using Microsoft.Maui.Graphics.Win2D;
    using System.Net;
#endif

    /// <summary>
    ///     内部モデル
    ///     廊下
    ///     
    ///     <list type="bullet">
    ///         <item>ミュータブル</item>
    ///     </list>
    /// </summary>
    class ItsCorridor : IItsIndoor, IItsCorridorOutdoorDirection
    {
        public ItsSiblingDoors SiblingDoors { get; }

        /// <summary>
        ///     双方向ドア
        /// </summary>
        public ItsTwoWayDoor TwoWayDoor { get; }

        /// <summary>
        ///     屋外側のドア
        /// </summary>
        public ItsGardensideDoor GardensideDoor { get; }

        /// <summary>
        ///     屋内側のドア
        /// </summary>
        public ItsRoomsideDoors RoomsideDoors { get; }

        // - その他

        #region その他（生成）
        /// <summary>
        ///     生成
        /// </summary>
        /// <param name="wholePageVM">全体ページビュー・モデル</param>
        internal ItsCorridor(TileCropPageViewModel wholePageVM)
        {
            this.PageVM = wholePageVM;

            this.TwoWayDoor = new ItsTwoWayDoor(this);
            this.GardensideDoor = new ItsGardensideDoor(this);
            this.SiblingDoors = new ItsSiblingDoors(this);

            this.RoomsideDoors = new ItsRoomsideDoors(this);

            this.GridUnit = new GridUnit(this);
            this.PointingDevice = new InnerPointingDevice(this.GardensideDoor, this);
            this.CropCursor = new CropCursor(this.GardensideDoor, this);
            this.CropTile = new CropTile(this.GardensideDoor, this);
            this.AddsButton = new AddsButton(this.GardensideDoor, this.SiblingDoors, this);
            this.DeletesButton = new DeletesButton(this.GardensideDoor, this);
        }
        #endregion

        // - パブリック変更通知プロパティ

        public int TilesetSourceImageWidthAsInt => this.IndoorTilesetSourceImageSize.Width.AsInt;
        public int TilesetSourceImageHeightAsInt => this.IndoorTilesetSourceImageSize.Height.AsInt;

        public void CropCursorRefreshCanvasTrick(string codePlace)
        {
            this.CropCursor.RefreshCanvasTrick(codePlace);
        }

        public int GridUnitSourceValueWidthAsInt
        {
            get
            {
                return this.GridUnit.SourceValue.Width.AsInt;
            }
        }

        public int GridUnitSourceValueHeightAsInt
        {
            get
            {
                return this.GridUnit.SourceValue.Height.AsInt;
            }
        }

        // - インターナル・プロパティ

        /// <summary>全体ページ・ビューモデル</summary>
        internal TileCropPageViewModel PageVM { get; }
        internal TileCropPageViewModel ObsoletedOutdoorPageVM => this.PageVM;




        public CultureInfo CultureInfoSelected
        {
            get => this.RoomsideDoors.IndoorCultureInfo.Selected;
            set => this.RoomsideDoors.IndoorCultureInfo.Selected = value;
        }


        #region プロパティ（［タイルセット元画像］　関連）
        /// <summary>
        ///     ［タイルセット元画像］のサイズ
        /// </summary>
        internal SizeInt IndoorTilesetSourceImageSize
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
        public string IndoorTilesetWorkingImageFilePathAsStr => App.CacheFolder.YourCircleFolder.YourWorkFolder.ImagesFolder.WorkingTilesetPng.Path.AsStr;
        #endregion

        /// <summary>切抜きカーソル</summary>
        internal CropCursor CropCursor { get; }

        #region プロパティ（切抜きカーソルが指すタイル）
        /// <summary>切抜きカーソルが指すタイル</summary>
        internal CropTile CropTile { get; }
        #endregion

        public TileRecordVisually CropTileSavesRecordVisually
        {
            get
            {
                return this.CropTile.SavesRecordVisually;
            }
        }

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
        public bool HasIntersectionBetweenCroppedCursorAndRegisteredTile { get; set; }
        #endregion

        #region プロパティ（切抜きカーソルと、既存タイルは合同か？）
        /// <summary>
        ///     切抜きカーソルと、既存タイルは合同か？
        /// </summary>
        /// <returns>そうだ</returns>
        public bool IsCongruenceBetweenCroppedCursorAndRegisteredTile { get; set; }
        #endregion

        /// <summary>グリッド単位</summary>
        internal GridUnit GridUnit { get; }

        /// <summary>ポインティング・デバイス</summary>
        internal InnerPointingDevice PointingDevice { get; }

        /// <summary>追加ボタン</summary>
        internal AddsButton AddsButton { get; }

        public void AddsButtonRefresh()
        {
            this.AddsButton.Refresh();
        }

        /// <summary>削除ボタン</summary>
        internal DeletesButton DeletesButton { get; }
        public void DeletesButtonRefresh()
        {
            this.DeletesButton.Refresh();
        }

        // - インターナル変更通知メソッド





        public void ObsoletedPageVMInvalidateDeletesButton() => ObsoletedOutdoorPageVM.InvalidateDeletesButton();

        public void ObsoletedPageVMInvalidateCultureInfo() => this.ObsoletedOutdoorPageVM.InvalidateCultureInfo();




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
            this.GardensideDoor.TilesetSettingsVM.MatchByRectangle(
                sourceRect: this.GardensideDoor.PageVM.CroppedCursorPointedTileSourceRect,
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
                            rect: this.GardensideDoor.PageVM.CroppedCursorPointedTileSourceRect,
                            title: TileTitle.Empty,
                            logicalDelete: LogicalDelete.False),
                        zoom: this.RoomsideDoors.Zoom.Value
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
            ObsoletedOutdoorPageVM.InvalidateCultureInfo();

            // グリッド・キャンバス
            {
                // グリッドの左上位置（初期値）
                ObsoletedOutdoorPageVM.GridPhaseSourceLocation = new PointInt(new XInt(0), new YInt(0));

                // グリッドのタイルサイズ（初期値）
                ObsoletedOutdoorPageVM.SourceGridUnit = new SizeInt(new WidthInt(32), new HeightInt(32));

                // グリッド・キャンバス画像の再作成
                this.TwoWayDoor.RemakeGridCanvasImage();
            }
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
            if (this.GardensideDoor.PageVM.CroppedCursorPointedTileSourceRect == RectangleInt.Empty)
            {
                // カーソルが無ければ、交差も無い。合同ともしない
                HasIntersectionBetweenCroppedCursorAndRegisteredTile = false;
                IsCongruenceBetweenCroppedCursorAndRegisteredTile = false;
                return;
            }

            // 軽くはない処理
            HasIntersectionBetweenCroppedCursorAndRegisteredTile = this.GardensideDoor.TilesetSettingsVM.HasIntersection(this.GardensideDoor.PageVM.CroppedCursorPointedTileSourceRect);
            IsCongruenceBetweenCroppedCursorAndRegisteredTile = this.GardensideDoor.TilesetSettingsVM.IsCongruence(this.GardensideDoor.PageVM.CroppedCursorPointedTileSourceRect);

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
                gridLeftTop: ObsoletedOutdoorPageVM.WorkingGridPhase,
                gridTile: ObsoletedOutdoorPageVM.WorkingGridUnit);

            // ズームを除去
            var sourceRect = new RectangleInt(
                location: new PointInt(
                    x: new XInt((int)(workingRect.Location.X.AsFloat / this.GardensideDoor.PageVM.ZoomAsFloat)),
                    y: new YInt((int)(workingRect.Location.Y.AsFloat / this.GardensideDoor.PageVM.ZoomAsFloat))),
                size: new SizeInt(
                    width: new WidthInt((int)(workingRect.Size.Width.AsFloat / this.GardensideDoor.PageVM.ZoomAsFloat)),
                    height: new HeightInt((int)(workingRect.Size.Height.AsFloat / this.GardensideDoor.PageVM.ZoomAsFloat))));

            //
            // 計算値の反映
            // ============
            //
            // Trace.WriteLine($"[TileCropPage.xaml.cs RefreshTileForm] context.IsMouseDragging: {context.IsMouseDragging}, context.HalfThicknessOfTileCursorLine.AsInt: {context.HalfThicknessOfTileCursorLine.AsInt}, rect x:{rect.Point.X.AsInt} y:{rect.Point.Y.AsInt} width:{rect.Size.Width.AsInt} height:{rect.Size.Height.AsInt}");
            this.GardensideDoor.PageVM.CroppedCursorPointedTileSourceRect = sourceRect;

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
            ObsoletedOutdoorPageVM.InvalidateAddsButton();

            // タイル・タイトル
            ObsoletedOutdoorPageVM.InvalidateTileTitle();
        }
        #endregion

        public void CropCursorRecalculateWorkingGridTileWidth()
        {
            this.CropCursor.RecalculateWorkingGridTileWidth();
        }
        public void CropCursorRecalculateWorkingGridTileHeight()
        {
            this.CropCursor.RecalculateWorkingGridTileHeight();
        }

        public TheGeometric.WidthFloat CropCursorWorkingWidthWithoutTrick
        {
            set
            {
                this.CropCursor.WorkingWidthWithoutTrick = value;
            }
        }

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
                tilesetDatatableFileLocation: this.GardensideDoor.PageVM.TilesetDatatableFileLocation,
                zoom: this.RoomsideDoors.Zoom.Value,
                tilesetDatatableVisually: out TilesetDatatableVisually tilesetDatatableVisually))
            {
                ObsoletedOutdoorPageVM.TilesetSettingsVM = tilesetDatatableVisually;

#if DEBUG
                // ファイルの整合性チェック（重い処理）
                if (this.GardensideDoor.TilesetSettingsVM.IsValid())
                {
                    Trace.WriteLine($"[TileCropPage.xaml.cs ContentPage_Loaded] ファイルの内容は妥当　File: {this.GardensideDoor.PageVM.TilesetDatatableFileLocation.Path.AsStr}");
                }
                else
                {
                    Trace.WriteLine($"[TileCropPage.xaml.cs ContentPage_Loaded] ファイルの内容に異常あり　File: {this.GardensideDoor.PageVM.TilesetDatatableFileLocation.Path.AsStr}");
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
            var tilesetImageFilePathAsStr = ObsoletedOutdoorPageVM.TilesetImageFilePathAsStr;

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
                            ObsoletedOutdoorPageVM.SetTilesetSourceBitmap(SKBitmap.Decode(memStream));

                            // 複製
                            ObsoletedOutdoorPageVM.TilesetWorkingBitmap = SKBitmap.FromImage(SKImage.FromBitmap(ObsoletedOutdoorPageVM.TilesetSourceBitmap));

                            // 画像処理（明度を下げる）
                            FeatSkia.ReduceBrightness.DoItInPlace(ObsoletedOutdoorPageVM.TilesetWorkingBitmap);
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
            ObsoletedOutdoorPageVM.IsMouseDragging = !ObsoletedOutdoorPageVM.IsMouseDragging;

            if (ObsoletedOutdoorPageVM.IsMouseDragging)
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
            if (ObsoletedOutdoorPageVM.IsMouseDragging)
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
