﻿namespace _2D_RPG_Negiramen.Specifications.TileCropPage
{
    using _2D_RPG_Negiramen.Models;
    using _2D_RPG_Negiramen.Models.Geometric;
    using _2D_RPG_Negiramen.Models.Visually;
    using _2D_RPG_Negiramen.ViewModels;
    using SkiaSharp;
    using SkiaSharp.Views.Maui.Controls;
    using System.Diagnostics;
    using TheGraphics = Microsoft.Maui.Graphics;
    using _2D_RPG_Negiramen.Coding;
    using _2D_RPG_Negiramen.ViewHistory.TileCropPage;

#if IOS || ANDROID || MACCATALYST
    using Microsoft.Maui.Graphics.Platform;
    using _2D_RPG_Negiramen.Specifications.TileCropPage;
#elif WINDOWS
    using Microsoft.Maui.Graphics.Win2D;
    using System.Net;
    using _2D_RPG_Negiramen.Specifications.TileCropPage;
    using static _2D_RPG_Negiramen.Specifications.TileCropPage.AddsButton;
#endif

    /// <summary>
    ///     コーリダー（Corridor；廊下）アーキテクチャー
    ///     
    ///     <list type="bullet">
    ///         <item>ミュータブル</item>
    ///     </list>
    /// </summary>
    class ItsCorridor : IItsTwoWayDoor
    {
        // - その他

        #region その他（生成）
        /// <summary>
        ///     生成
        /// </summary>
        /// <param name="ownerPageVM">全体ページビュー・モデル</param>
        internal ItsCorridor(TileCropPageViewModel ownerPageVM)
        {
            this.OwnerPageVM = ownerPageVM;

            this.TwoWayDoor = new ItsTwoWayDoor(this);
            this.GardensideDoor = new ItsGardensideDoor(this);
            this.RoomsideDoors = new ItsRoomsideDoors(this);
        }
        #endregion

        // - インターナル・プロパティ

        /// <summary>全体ページ・ビューモデル</summary>
        internal TileCropPageViewModel OwnerPageVM { get; }

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

        // - インターナル・メソッド

        #region 変更通知メソッド（ロケール変更による再描画）
        /// <summary>
        ///     ロケール変更による再描画
        ///     
        ///     <list type="bullet">
        ///         <item>動的にテキストを変えている部分に対応するため</item>
        ///     </list>
        /// </summary>
        internal void InvalidateByLocale() => this.RoomsideDoors.AddsButton.Refresh(
            setAddsButtonText: (text) =>
            {
                this.GardensideDoor.PageVM.AddsButtonText = text;
                this.GardensideDoor.PageVM.InvalidateAddsButton();
            });
        #endregion

        // - インターナル・イベントハンドラ

        #region イベントハンドラ（別ページから、このページに訪れたときに呼び出される）
        /// <summary>
        ///     別ページから、このページに訪れたときに呼び出される
        /// </summary>
        internal void OnNavigatedTo(SKCanvasView skiaTilesetCanvas1)
        {
            // Trace.WriteLine($"[TileCropPage.xaml.cs ContentPage_Loaded] ページ来訪時");

            this.ReactOnVisited();

            //
            // タイル設定ファイルの読込
            // ========================
            //
            if (TilesetDatatableVisually.LoadCSV(
                tilesetDatatableFileLocation: this.GardensideDoor.PageVM.TilesetDatatableFileLocation,
                zoom: this.RoomsideDoors.ZoomProperties.Value,
                tilesetDatatableVisually: out TilesetDatatableVisually tilesetDatatableVisually))
            {
                this.OwnerPageVM.TilesetSettingsVM = tilesetDatatableVisually;

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
            var tilesetImageFilePathAsStr = this.OwnerPageVM.TilesetImageFilePathAsStr;

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
                            this.OwnerPageVM.SetTilesetSourceBitmap(SKBitmap.Decode(memStream));

                            // 複製
                            this.OwnerPageVM.TilesetWorkingBitmap = SKBitmap.FromImage(SKImage.FromBitmap(this.OwnerPageVM.TilesetSourceBitmap));

                            // 画像処理（明度を下げる）
                            FeatSkia.ReduceBrightness.DoItInPlace(this.OwnerPageVM.TilesetWorkingBitmap);
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
            if (this.RoomsideDoors.CropTile.IdOrEmpty == TileIdOrEmpty.Empty)
            {
                // Ｉｄが空欄
                // ［追加］（新規作成）だ

                // 登録タイル追加
                this.RoomsideDoors.AddsButton.AddTile(
                    doAddRegisteredTIle: (TileRecordVisually contents) =>
                    {
                        // Ｉｄが空欄
                        // ［追加］（新規作成）だ

                        // ［切抜きカーソル］にサイズがなければ、何もしない
                        if (contents.IsNone)
                            return;

                        // 新しいタイルＩｄを発行
                        TileIdOrEmpty tileIdOrEmpty = this.GardensideDoor.TilesetSettingsVM.UsableId;
                        this.GardensideDoor.TilesetSettingsVM.IncreaseUsableId();

                        // 追加でも、上書きでも、同じ処理でいける
                        // ［登録タイル追加］処理
                        App.History.Do(new AddRegisteredTileProcessing(
                            gardensideDoor: this.GardensideDoor,
                            roomsideDoors: this.RoomsideDoors,
                            croppedCursorVisually: contents,
                            tileIdOrEmpty: tileIdOrEmpty,
                            workingRectangle: contents.SourceRectangle.Do(this.RoomsideDoors.ZoomProperties.Value)));

                        this.GardensideDoor.PageVM.InvalidateForHistory();
                    });
            }
            else
            {
                // 上書きボタンだが、［上書き］処理をする
                this.OverwriteTile();
            }
        }
        #endregion

        /// <summary>
        ///     上書きボタンだが、［上書き］処理をする
        /// </summary>
        internal void OverwriteTile()
        {
            this.RoomsideDoors.AddsButton.OverwriteTile(
                doAddRegisteredTIle: (TileRecordVisually contents) =>
                {
                    TileIdOrEmpty tileIdOrEmpty;

                    // ［切抜きカーソル］にサイズがなければ、何もしない
                    if (contents.IsNone)
                        return;

                    // Ｉｄが空欄でない
                    // ［上書き］（更新）だ
                    tileIdOrEmpty = this.RoomsideDoors.CropTile.IdOrEmpty;

                    // 追加でも、上書きでも、同じ処理でいける
                    // ［登録タイル追加］処理
                    App.History.Do(new AddRegisteredTileProcessing(
                        // 上位の権限を委譲する
                        gardensideDoor: this.GardensideDoor,
                        roomsideDoors: this.RoomsideDoors,
                        croppedCursorVisually: contents,
                        tileIdOrEmpty: tileIdOrEmpty,
                        workingRectangle: contents.SourceRectangle.Do(this.RoomsideDoors.ZoomProperties.Value)));

                    this.GardensideDoor.PageVM.InvalidateForHistory();
                });
        }

        #region イベントハンドラ（タイルセット画像上でタップ時）
        /// <summary>
        ///     タイルセット画像上でタップ時
        /// </summary>
        /// <param name="tappedPoint"></param>
        public void OnTilesetImageTapped(Point tappedPoint)
        {
            // 反転
            this.OwnerPageVM.IsMouseDragging = !this.OwnerPageVM.IsMouseDragging;

            if (this.OwnerPageVM.IsMouseDragging)
            {
                //
                // 疑似マウス・ダウン
                // ==================
                //
                Trace.WriteLine("［操作］　疑似マウス・ダウン");

                // ポイントしている位置
                this.RoomsideDoors.PointingDevice.CurrentPoint = this.RoomsideDoors.PointingDevice.StartPoint = new PointFloat(
                    new XFloat((float)tappedPoint.X),
                    new YFloat((float)tappedPoint.Y));
                // Trace.WriteLine($"[TileCropPage TileImage_OnTapped] tapped x:{PointingDeviceStartPoint.X.AsInt} y:{PointingDeviceStartPoint.Y.AsInt}");

                // タイル・フォームの表示更新
                RefreshTileForm(
                    mouseDrawingOperationState: MouseDrawingOperationState.ButtonDown,
                    setAddsButtonText: (text) =>
                    {
                        this.GardensideDoor.PageVM.AddsButtonText = text;
                        this.GardensideDoor.PageVM.InvalidateAddsButton();
                    });

                this.RoomsideDoors.CropCursor.RefreshCanvasTrick(codePlace: "[TileCropPage.xml.cs TileImage_OnTapped 疑似マウスダウン]");
                // TRICK CODE:
                this.GardensideDoor.PageVM.InvalidateWorkingTargetTile();
            }
            else
            {
                //
                // 疑似マウス・アップ
                // ==================
                //

                Trace.WriteLine("［操作］　疑似マウス・アップ");

                // ポイントしている位置
                this.RoomsideDoors.PointingDevice.CurrentPoint = new PointFloat(
                    new XFloat((float)tappedPoint.X),
                    new YFloat((float)tappedPoint.Y));
                // Trace.WriteLine($"[TileCropPage PointerGestureRecognizer_PointerExited] exited x:{PointingDeviceCurrentPoint.X.AsInt} y:{PointingDeviceCurrentPoint.Y.AsInt}");

                // タイル・フォームの表示更新
                RefreshTileForm(
                    mouseDrawingOperationState: MouseDrawingOperationState.ButtonUp,
                    setAddsButtonText: (text) =>
                    {
                        this.GardensideDoor.PageVM.AddsButtonText = text;
                        this.GardensideDoor.PageVM.InvalidateAddsButton();
                    });

                this.RoomsideDoors.CropCursor.RefreshCanvasTrick(codePlace: "[TileCropPage.xml.cs TileImage_OnTapped 疑似マウスアップ]");
                // TRICK CODE:
                this.GardensideDoor.PageVM.InvalidateWorkingTargetTile();
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
            if (this.OwnerPageVM.IsMouseDragging)
            {
                //
                // 疑似マウス・ドラッグ
                // ====================
                //

                // ポイントしている位置
                this.RoomsideDoors.PointingDevice.CurrentPoint = new PointFloat(
                    new XFloat((float)tappedPoint.X),
                    new YFloat((float)tappedPoint.Y));
                // Trace.WriteLine($"[TileCropPage PointerGestureRecognizer_PointerMoved] moved x:{PointingDeviceCurrentPoint.X.AsInt} y:{PointingDeviceCurrentPoint.Y.AsInt}");

                // タイル・フォームの表示更新
                RefreshTileForm(
                    mouseDrawingOperationState: MouseDrawingOperationState.PointerMove,
                    setAddsButtonText: (text) =>
                    {
                        this.GardensideDoor.PageVM.AddsButtonText = text;
                        this.GardensideDoor.PageVM.InvalidateAddsButton();
                    });

                this.RoomsideDoors.CropCursor.RefreshCanvasTrick(codePlace: "[TileCropPage.xml.cs PointerGestureRecognizer_PointerMoved 疑似マウスドラッグ]");
                // TRICK CODE:
                this.GardensideDoor.PageVM.InvalidateWorkingTargetTile();
            }
        }
        #endregion

        // - プライベート・メソッド

        #region メソッド（画面遷移でこの画面に戻ってきた時）
        /// <summary>
        ///     画面遷移でこの画面に戻ってきた時
        /// </summary>
        void ReactOnVisited()
        {
            // ロケールが変わってるかもしれないので反映
            this.OwnerPageVM.InvalidateCultureInfo();

            // グリッド・キャンバス
            {
                // グリッドの左上位置（初期値）
                this.OwnerPageVM.GridPhaseSourceLocation = new PointInt(new XInt(0), new YInt(0));

                // グリッドのタイルサイズ（初期値）
                this.OwnerPageVM.SourceGridUnit = new SizeInt(new WidthInt(32), new HeightInt(32));

                // グリッド・キャンバス画像の再作成
                this.RemakeGridCanvasImage();
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
        void RecalculateBetweenCropCursorAndRegisteredTile()
        {
            if (this.GardensideDoor.PageVM.CroppedCursorPointedTileSourceRect == RectangleInt.Empty)
            {
                // カーソルが無ければ、交差も無い。合同ともしない
                this.RoomsideDoors.HasIntersectionBetweenCroppedCursorAndRegisteredTile = false;
                this.RoomsideDoors.IsCongruenceBetweenCroppedCursorAndRegisteredTile = false;
                return;
            }

            // 軽くはない処理
            this.RoomsideDoors.HasIntersectionBetweenCroppedCursorAndRegisteredTile = this.GardensideDoor.TilesetSettingsVM.HasIntersection(this.GardensideDoor.PageVM.CroppedCursorPointedTileSourceRect);
            this.RoomsideDoors.IsCongruenceBetweenCroppedCursorAndRegisteredTile = this.GardensideDoor.TilesetSettingsVM.IsCongruence(this.GardensideDoor.PageVM.CroppedCursorPointedTileSourceRect);

            // Trace.WriteLine($"[TileCropPageViewModel.cs RecalculateBetweenCroppedCursorAndRegisteredTile] HasIntersectionBetweenCroppedCursorAndRegisteredTile: {this.RoomsideDoors.HasIntersectionBetweenCroppedCursorAndRegisteredTile}, IsCongruenceBetweenCroppedCursorAndRegisteredTile: {this.RoomsideDoors.IsCongruenceBetweenCroppedCursorAndRegisteredTile}");
        }
        #endregion

        #region メソッド（タイル・フォーム更新）
        /// <summary>
        ///     タイル・フォーム更新
        /// </summary>
        void RefreshTileForm(
            MouseDrawingOperationState mouseDrawingOperationState,
            LazyArgs.Set<string> setAddsButtonText)
        {
            //
            // ポインティング・デバイスの２箇所のタップ位置から、タイルの矩形を算出
            // ====================================================================
            //

            // ズームしたままの矩形
            RectangleFloat workingRect = CoordinateHelper.GetCursorRectangle(
                startPoint: this.RoomsideDoors.PointingDevice.StartPoint,
                endPoint: this.RoomsideDoors.PointingDevice.CurrentPoint,
                gridLeftTop: this.OwnerPageVM.WorkingGridPhase,
                gridTile: this.OwnerPageVM.WorkingGridUnit);

            // ズームを除去した矩形
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
            this.GardensideDoor.PageVM.CroppedCursorPointedTileSourceRect = sourceRect;
            if (mouseDrawingOperationState == MouseDrawingOperationState.ButtonUp)
            {
                Trace.WriteLine($"［状態遷移］　矩形確定　x:{sourceRect.Location.X.AsInt} y:{sourceRect.Location.Y.AsInt} width:{sourceRect.Size.Width.AsInt} height:{sourceRect.Size.Height.AsInt}");
            }

            //
            // 登録済みのタイルと被っていないか判定
            // ====================================
            //
            //      - （軽くない処理）
            //
            RecalculateBetweenCropCursorAndRegisteredTile();

            //
            // 切抜きカーソル更新
            // ==================
            //
            this.GardensideDoor.TilesetSettingsVM.MatchByRectangle(
                sourceRect: this.GardensideDoor.PageVM.CroppedCursorPointedTileSourceRect,
                some: (tileVisually) =>
                {
                    if (mouseDrawingOperationState == MouseDrawingOperationState.ButtonUp)
                    {
                        Trace.WriteLine($"［選択タイル調査］　タイル登録済　Id:{tileVisually.Id.AsInt}, {tileVisually.Id.AsBASE64} Title:{tileVisually.Title.AsStr}");
                    }

                    // タイルを指す（論理削除されているものも含む）
                    this.RoomsideDoors.CropTile.SetRecordVisually(
                        tileVisually,
                        onVanished: () =>
                        {
                            Debug.Fail("ここには来ない");
                        },
                        onUpdated: () =>
                        {
                            // （変更通知を送っている）
                            this.GardensideDoor.PageVM.CropTileSourceLeftAsInt = tileVisually.SourceRectangle.Location.X.AsInt;
                            this.GardensideDoor.PageVM.CropTileSourceTopAsInt = tileVisually.SourceRectangle.Location.Y.AsInt;
                            this.GardensideDoor.PageVM.CropTileSourceWidthAsInt = tileVisually.SourceRectangle.Size.Width.AsInt;
                            this.GardensideDoor.PageVM.CropTileSourceHeightAsInt = tileVisually.SourceRectangle.Size.Height.AsInt;

                            // 変更通知を送りたい
                            this.GardensideDoor.PageVM.InvalidateTileIdChange();
                        },
                        setAddsButtonText: setAddsButtonText,
                        onDeleteButtonEnableChanged: () =>
                        {
                            this.GardensideDoor.PageVM.InvalidateDeletesButton();
                        });
                },
                none: () =>
                {
                    if (mouseDrawingOperationState == MouseDrawingOperationState.ButtonUp)
                    {
                        Trace.WriteLine("［選択タイル調査］　タイル未登録");
                    }

                    //
                    // 空欄にする
                    // ==========
                    //

                    var sourceRectangle = this.GardensideDoor.PageVM.CroppedCursorPointedTileSourceRect;

                    // 選択中のタイルの矩形だけ維持し、タイル・コードと、コメントを空欄にする
                    this.RoomsideDoors.CropTile.SetRecordVisually(TileRecordVisually.FromModel(
                        tileRecord: new TileRecord(
                            id: TileIdOrEmpty.Empty,
                            rect: sourceRectangle,
                            title: TileTitle.Empty,
                            logicalDelete: LogicalDelete.False),
                        zoom: this.RoomsideDoors.ZoomProperties.Value
#if DEBUG
                        , hint: "[TileCropPageViewModel.cs LoadCroppedCursorPointedTile]"
#endif
                        ),
                        onVanished: () =>
                        {
                            Debug.Fail("ここには来ない");
                            // 元画像の位置とサイズ
                            this.GardensideDoor.PageVM.CroppedCursorPointedTileSourceRect = RectangleInt.Empty;

                            // 論理削除
                            this.GardensideDoor.PageVM.CropTileLogicalDeleteAsBool = false;

                            // 変更通知を送りたい
                            this.GardensideDoor.PageVM.InvalidateTileIdChange();
                        },
                        onUpdated: () =>
                        {
                            // ここにはくる

                            // （変更通知を送っている）
                            this.GardensideDoor.PageVM.CropTileSourceLeftAsInt = sourceRectangle.Location.X.AsInt;
                            this.GardensideDoor.PageVM.CropTileSourceTopAsInt = sourceRectangle.Location.Y.AsInt;
                            this.GardensideDoor.PageVM.CropTileSourceWidthAsInt = sourceRectangle.Size.Width.AsInt;
                            this.GardensideDoor.PageVM.CropTileSourceHeightAsInt = sourceRectangle.Size.Height.AsInt;

                            // 変更通知を送りたい
                            this.GardensideDoor.PageVM.InvalidateTileIdChange();
                        },
                        setAddsButtonText: setAddsButtonText,
                        onDeleteButtonEnableChanged: () =>
                        {
                            this.GardensideDoor.PageVM.InvalidateDeletesButton();
                        });
                },
                // 論理削除されているものも選択できることとする（復元、論理削除の解除のため）
                includeLogicalDelete: true);

            // （切抜きカーソル更新後）［追加／上書き］ボタン再描画
            this.RoomsideDoors.AddsButton.Refresh(
                setAddsButtonText: setAddsButtonText);

            // （切抜きカーソル更新後）［削除］ボタン活性化
            this.RoomsideDoors.DeletesButton.Refresh(
                onEnableChanged: () =>
                {
                    this.GardensideDoor.PageVM.InvalidateDeletesButton();
                });

            // ［追加／復元］ボタン
            this.OwnerPageVM.InvalidateAddsButton();

            // タイル・タイトル
            this.OwnerPageVM.InvalidateTileTitle();
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
        public void RemakeWorkingTilesetImage()
        {
            // 元画像をベースに、作業画像を複製
            var temporaryBitmap = SKBitmap.FromImage(SKImage.FromBitmap(this.OwnerPageVM.TilesetSourceBitmap));

            // 画像処理（明度を下げる）
            FeatSkia.ReduceBrightness.DoItInPlace(temporaryBitmap);

            // 作業画像のサイズ計算
            this.OwnerPageVM.workingImageSize = new SizeInt(
                width: new WidthInt((int)(this.OwnerPageVM.ZoomAsFloat * this.RoomsideDoors.TilesetSourceImageSize.Width.AsInt)),
                height: new HeightInt((int)(this.OwnerPageVM.ZoomAsFloat * this.RoomsideDoors.TilesetSourceImageSize.Height.AsInt)));

            // 作業画像のリサイズ
            this.OwnerPageVM.TilesetWorkingBitmap = temporaryBitmap.Resize(
                size: new SKSizeI(
                    width: this.OwnerPageVM.workingImageSize.Width.AsInt,
                    height: this.OwnerPageVM.workingImageSize.Height.AsInt),
                quality: SKFilterQuality.Medium);

            this.OwnerPageVM.InvalidateTilesetWorkingImage();
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
        public void RemakeGridCanvasImage()
        {
            // ズームが屋外
            this.OwnerPageVM.GridCanvasImageSize = new SizeInt(
                width: new WidthInt((int)(this.RoomsideDoors.ZoomProperties.AsFloat * this.RoomsideDoors.TilesetSourceImageSize.Width.AsInt) + 2 * this.OwnerPageVM.HalfThicknessOfGridLineAsInt),
                height: new HeightInt((int)(this.RoomsideDoors.ZoomProperties.AsFloat * this.RoomsideDoors.TilesetSourceImageSize.Height.AsInt) + 2 * this.OwnerPageVM.HalfThicknessOfGridLineAsInt));
        }
        #endregion
    }
}
