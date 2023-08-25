﻿namespace _2D_RPG_Negiramen.ViewInnerModels.TileCropPage
{
    using _2D_RPG_Negiramen.Models;
    using _2D_RPG_Negiramen.Models.Geometric;
    using _2D_RPG_Negiramen.Models.Visually;
    using _2D_RPG_Negiramen.ViewHistory.TileCropPage;
    using _2D_RPG_Negiramen.ViewModels;
    using SkiaSharp;
    using SkiaSharp.Views.Maui.Controls;
    using System.Diagnostics;
    using TheFileEntryLocations = Models.FileEntries.Locations;
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
            Owner = owner;

            this.CultureInfo = new InnerCultureInfo(this);
        }
        #endregion

        // - パブリック変更通知プロパティ

        #region プロパティ（タイルセット設定ビューモデル）
        /// <summary>
        ///     タイルセット設定ビューモデル
        /// </summary>
        public TilesetDatatableVisually TilesetSettingsVM => Owner.TilesetSettingsVM;
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
            get => Owner.TilesetDatatableFileLocation;
            set
            {
                Owner.TilesetDatatableFileLocation = value;
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
            get => isMouseDragging;
            set
            {
                if (isMouseDragging != value)
                {
                    isMouseDragging = value;
                    Owner.InvalidateIsMouseDragging();
                }
            }
        }
        #endregion

        public WidthFloat CroppedCursorPointedTileWorkingWidthWithoutTrick
        {
            get => Owner.CroppedCursorPointedTileWorkingWidthWithoutTrick;
            set
            {
                Owner.CroppedCursorPointedTileWorkingWidthWithoutTrick = value;
            }
        }

        public HeightFloat CroppedCursorPointedTileWorkingHeight
        {
            get => Owner.CroppedCursorPointedTileWorkingHeight;
            set
            {
                Owner.CroppedCursorPointedTileWorkingHeight = value;
            }
        }

        #region 変更通知プロパティ（［切抜きカーソル］　関連）
        /// <summary>
        ///     トリック幅
        /// </summary>
        public WidthFloat TrickWidth
        {
            get => trickWidth;
            set => trickWidth = value;
        }
        #endregion

        #region 変更通知プロパティ（［切抜きカーソルが指すタイル］　関連）
        /// <summary>
        ///     ［切抜きカーソルが指すタイル］の元画像ベースの矩形
        ///     
        ///     <list type="bullet">
        ///         <item>カーソルが無いとき、大きさの無いカーソルを返す</item>
        ///     </list>
        /// </summary>
        public RectangleInt CroppedCursorPointedTileSourceRect
        {
            get => Owner.CroppedCursorPointedTileSourceRect;
            set
            {
                Owner.CroppedCursorPointedTileSourceRect = value;
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
            get => Owner.ZoomAsFloat;
            set
            {
                Owner.ZoomAsFloat = value;
            }
        }
        #endregion

        // - インターナル・プロパティ

        internal TileCropPageViewModel Owner { get; }

        /// <summary>
        ///     文化情報
        /// </summary>
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
            get => CroppedCursorPointedTileRecordVisually;
            set
            {
                var oldTileVisually = CroppedCursorPointedTileRecordVisually;

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
                        UpdateCroppedCursorPointedTileByDifference(
                            // タイトル
                            tileTitle: TileTitle.Empty);

                        // 末端にセット（変更通知を呼ぶために）
                        // Ｉｄ
                        CroppedCursorPointedTileIdOrEmpty = TileIdOrEmpty.Empty;

                        // 元画像の位置とサイズ
                        Owner.CroppedCursorPointedTileSourceRect = RectangleInt.Empty;

                        // 論理削除
                        Owner.CroppedCursorPointedTileLogicalDeleteAsBool = false;

                        // 空にする
                        CroppedCursorPointedTileRecordVisually = TileRecordVisually.CreateEmpty();
                    }
                }
                else
                {
                    var newValue = value;

                    if (oldTileVisually.IsNone)
                    {
                        // ［切抜きカーソル］の指すタイル無し時

                        // 新規作成
                        CroppedCursorPointedTileRecordVisually = TileRecordVisually.CreateEmpty();
                    }
                    else
                    {
                        // ［切抜きカーソル］の指すタイルが有るなら構わない
                    }

                    // （変更通知を送っている）
                    UpdateCroppedCursorPointedTileByDifference(
                        // タイトル
                        tileTitle: newValue.Title);

                    // （変更通知を送っている）
                    CroppedCursorPointedTileIdOrEmpty = newValue.Id;
                    Owner.CroppedCursorPointedTileSourceLeftAsInt = newValue.SourceRectangle.Location.X.AsInt;
                    Owner.CroppedCursorPointedTileSourceTopAsInt = newValue.SourceRectangle.Location.Y.AsInt;
                    Owner.CroppedCursorPointedTileSourceWidthAsInt = newValue.SourceRectangle.Size.Width.AsInt;
                    Owner.CroppedCursorPointedTileSourceHeightAsInt = newValue.SourceRectangle.Size.Height.AsInt;
                    // this.CroppedCursorPointedTileTitleAsStr = newValue.Title.AsStr;
                }

                // 変更通知を送りたい
                Owner.InvalidateTileIdChange();
            }
        }

        /// <summary>
        ///     ［切抜きカーソルが指すタイル］のＩｄ
        /// </summary>
        public TileIdOrEmpty CroppedCursorPointedTileIdOrEmpty
        {
            get
            {
                var contents = CroppedCursorPointedTileRecordVisually;

                // ［切抜きカーソル］の指すタイル無し時
                if (contents.IsNone)
                    return TileIdOrEmpty.Empty;

                return contents.Id;
            }
            set
            {
                if (CroppedCursorPointedTileRecordVisually.Id == value)
                    return;

                // 差分更新
                UpdateCroppedCursorPointedTileByDifference(
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
        internal PointFloat PointingDeviceStartPoint { get; set; }
        #endregion

        #region プロパティ（ポインティング・デバイス現在位置）
        /// <summary>
        ///     ポインティング・デバイス現在位置
        /// </summary>
        internal PointFloat PointingDeviceCurrentPoint { get; set; }
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
        public Zoom Zoom
        {
            get => zoom;
            set
            {
                if (zoom == value)
                    return;

                // TODO 循環参照しやすいから、良くないコード
                Owner.ZoomAsFloat = value.AsFloat;
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
            get => zoom.AsFloat;
            set
            {
                if (zoom.AsFloat != value)
                {
                    if (Owner.ZoomMinAsFloat <= value && value <= Owner.ZoomMaxAsFloat)
                    {
                        Zoom oldValue = zoom;
                        Zoom newValue = new Zoom(value);

                        zoom = newValue;
                        TrickRefreshCanvasOfTileCursor("[TileCropPageViewModel.cs ZoomAsFloat]");

                        // 再帰的にズーム再変更、かつ変更後の影響を処理
                        App.History.Do(new ZoomProcessing(this, oldValue, newValue));
                    }
                }
            }
        }

        /// <summary>
        ///     ズーム最大
        /// </summary>
        public float ZoomMaxAsFloat => zoomMax.AsFloat;

        /// <summary>
        ///     ズーム最小
        /// </summary>
        public float ZoomMinAsFloat => zoomMin.AsFloat;
        #endregion

        // - インターナル変更通知メソッド

        #region 変更通知メソッド（［選択タイル］　関連）
        /// <summary>
        ///     ［選択タイル］Ｉｄの再描画
        /// </summary>
        internal void InvalidateTileIdChange() => Owner.InvalidateTileIdChange();
        #endregion

        #region 変更通知メソッド（［タイルセット設定］　関連）
        /// <summary>
        ///     ［タイルセット設定］ビューモデルに変更あり
        /// </summary>
        internal void InvalidateTilesetSettingsVM() => Owner.InvalidateTilesetSettingsVM();
        #endregion

        #region 変更通知メソッド（［履歴］）
        /// <summary>
        ///     ［履歴］
        /// </summary>
        internal void InvalidateForHistory() => Owner.InvalidateForHistory();
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
        internal void InvalidateLocale() => RefreshAddsButton();
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
            if (Owner.TrickWidth.AsFloat == 1.0f)
            {
                Owner.TrickWidth = WidthFloat.Zero;
            }
            else
            {
                Owner.TrickWidth = WidthFloat.One;
            }

            // TRICK CODE:
            Owner.InvalidateWorkingTargetTile();
        }

        /// <summary>
        ///     ［切抜きカーソル］の再描画
        ///     
        ///     TODO ★ 設定ファイルからリロードしてる？
        /// </summary>
        internal void LoadCroppedCursorPointedTile()
        {
            TilesetSettingsVM.MatchByRectangle(
                sourceRect: CroppedCursorPointedTileSourceRect,
                some: (tileVisually) =>
                {
                    // Trace.WriteLine($"[TileCropPage.xml.cs TapGestureRecognizer_Tapped] タイルは登録済みだ。 Id:{tileVisually.Id.AsInt}, X:{tileVisually.SourceRectangle.Location.X.AsInt}, Y:{recordVM.SourceRectangle.Location.Y.AsInt}, Width:{recordVM.SourceRectangle.Size.Width.AsInt}, Height:{recordVM.SourceRectangle.Size.Height.AsInt}, Title:{recordVM.Title.AsStr}");

                    // タイルを指す（論理削除されているものも含む）
                    TargetTileRecordVisually = tileVisually;
                },
                none: () =>
                {
                    // Trace.WriteLine("[TileCropPage.xml.cs TapGestureRecognizer_Tapped] 未登録のタイルだ");

                    //
                    // 空欄にする
                    // ==========
                    //

                    // 選択中のタイルの矩形だけ維持し、タイル・コードと、コメントを空欄にする
                    TargetTileRecordVisually = TileRecordVisually.FromModel(
                        tileRecord: new TileRecord(
                            id: TileIdOrEmpty.Empty,
                            rect: CroppedCursorPointedTileSourceRect,
                            title: TileTitle.Empty,
                            logicalDelete: LogicalDelete.False),
                        zoom: Zoom
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
            var currentTileVisually = CroppedCursorPointedTileRecordVisually;

            // タイルＩｄ
            if (!(tileId is null) && currentTileVisually.Id != tileId)
            {
                CroppedCursorPointedTileRecordVisually.Id = tileId;

                // Ｉｄが入ることで、タイル登録扱いになる。いろいろ再描画する

                // ［追加／上書き］ボタン再描画
                RefreshAddsButton();

                // ［削除］ボタン再描画
                RefreshDeletesButton();
            }

            // タイル・タイトル
            if (!(tileTitle is null) && currentTileVisually.Title != tileTitle)
            {
                CroppedCursorPointedTileRecordVisually.Title = tileTitle;
            }

            // 論理削除フラグ
            if (!(logicalDelete is null) && currentTileVisually.LogicalDelete != logicalDelete)
            {
                CroppedCursorPointedTileRecordVisually.LogicalDelete = logicalDelete;
            }

            // 変更通知を送る
            Owner.InvalidateTileIdChange();

            Trace.WriteLine($"[TileCropPageViewModel.cs UpdateCroppedCursorPointedTileByDifference] CroppedCursorPointedTileRecordVisually.Dump(): {CroppedCursorPointedTileRecordVisually.Dump()}");
        }
        #endregion

        #region メソッド（［追加／上書き］　関連）
        /// <summary>
        ///     ［追加］
        /// </summary>
        internal void AddRegisteredTile()
        {
            var contents = TargetTileRecordVisually;

            TileIdOrEmpty tileIdOrEmpty;

            // Ｉｄが空欄
            // ［追加］（新規作成）だ

            // ［切抜きカーソル］にサイズがなければ、何もしない
            if (contents.IsNone)
                return;

            // 新しいタイルＩｄを発行
            tileIdOrEmpty = Owner.TilesetSettingsVM.UsableId;
            Owner.TilesetSettingsVM.IncreaseUsableId();

            // 追加でも、上書きでも、同じ処理でいける
            // ［登録タイル追加］処理
            App.History.Do(new AddRegisteredTileProcessing(
                inner: this,
                croppedCursorVisually: contents,
                tileIdOrEmpty: tileIdOrEmpty,
                workingRectangle: contents.SourceRectangle.Do(Zoom)));

            Owner.InvalidateForHistory();
        }

        /// <summary>
        ///     ［上書き］
        /// </summary>
        public void OverwriteRegisteredTile()
        {
            var contents = TargetTileRecordVisually;

            TileIdOrEmpty tileIdOrEmpty;

            // ［切抜きカーソル］にサイズがなければ、何もしない
            if (contents.IsNone)
                return;

            // Ｉｄが空欄でない
            // ［上書き］（更新）だ
            tileIdOrEmpty = CroppedCursorPointedTileIdOrEmpty;

            // 追加でも、上書きでも、同じ処理でいける
            // ［登録タイル追加］処理
            App.History.Do(new AddRegisteredTileProcessing(
                inner: Owner.Inner,
                croppedCursorVisually: contents,
                tileIdOrEmpty: tileIdOrEmpty,
                workingRectangle: contents.SourceRectangle.Do(Zoom)));

            Owner.InvalidateForHistory();
        }

        /// <summary>
        ///     ［登録タイル］削除
        /// </summary>
        public void RemoveRegisteredTile()
        {
            App.History.Do(new RemoveRegisteredTileProcessing(
                inner: this,
                tileIdOrEmpty: CroppedCursorPointedTileIdOrEmpty));

            Owner.InvalidateForHistory();
        }
        #endregion

        #region メソッド（画面遷移でこの画面に戻ってきた時）
        /// <summary>
        ///     画面遷移でこの画面に戻ってきた時
        /// </summary>
        internal void ReactOnVisited()
        {
            // ロケールが変わってるかもしれないので反映
            Owner.InvalidateCultureInfo();

            // グリッド・キャンバス
            {
                // グリッドの左上位置（初期値）
                Owner.GridPhaseSourceLocation = new PointInt(new XInt(0), new YInt(0));

                // グリッドのタイルサイズ（初期値）
                Owner.SourceGridUnit = new SizeInt(new WidthInt(32), new HeightInt(32));

                // グリッド・キャンバス画像の再作成
                RemakeGridCanvasImage();
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
            var temporaryBitmap = SKBitmap.FromImage(SKImage.FromBitmap(Owner.TilesetSourceBitmap));

            // 画像処理（明度を下げる）
            FeatSkia.ReduceBrightness.DoItInPlace(temporaryBitmap);

            // 作業画像のサイズ計算
            Owner.workingImageSize = new SizeInt(
                width: new WidthInt((int)(OwnerZoomAsFloat * TilesetSourceImageSize.Width.AsInt)),
                height: new HeightInt((int)(OwnerZoomAsFloat * TilesetSourceImageSize.Height.AsInt)));

            // 作業画像のリサイズ
            Owner.TilesetWorkingBitmap = temporaryBitmap.Resize(
                size: new SKSizeI(
                    width: Owner.workingImageSize.Width.AsInt,
                    height: Owner.workingImageSize.Height.AsInt),
                quality: SKFilterQuality.Medium);

            Owner.InvalidateTilesetWorkingImage();
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
            if (Owner.TilesetWorkingImageWidthAsInt % 2 == 1)
            {
                Owner.workingImageSize = new SizeInt(
                    width: new WidthInt(Owner.workingImageSize.Width.AsInt - 1),
                    height: new HeightInt(Owner.workingImageSize.Height.AsInt));
            }
            else
            {
                Owner.workingImageSize = new SizeInt(
                    width: new WidthInt(Owner.workingImageSize.Width.AsInt + 1),
                    height: new HeightInt(Owner.workingImageSize.Height.AsInt));
            }

            // タイル タイトル
            Owner.InvalidateTileTitle();

            // 追加・削除ボタンの表示状態を更新したい
            Owner.InvalidateAddsButton();

            // タイルセット作業画像
            Owner.InvalidateTilesetWorkingImage();
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
            Owner.GridCanvasImageSize = new SizeInt(
                width: new WidthInt((int)(OwnerZoomAsFloat * TilesetSourceImageSize.Width.AsInt) + 2 * Owner.HalfThicknessOfGridLineAsInt),
                height: new HeightInt((int)(OwnerZoomAsFloat * TilesetSourceImageSize.Height.AsInt) + 2 * Owner.HalfThicknessOfGridLineAsInt));
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
            Owner.WorkingGridTileWidthAsFloat = OwnerZoomAsFloat * Owner.sourceGridUnit.Width.AsInt;

            Owner.InvalidateWorkingGrid();
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
            Owner.WorkingGridTileHeightAsFloat = OwnerZoomAsFloat * Owner.sourceGridUnit.Height.AsInt;

            Owner.InvalidateWorkingGrid();
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
            if (CroppedCursorPointedTileSourceRect == RectangleInt.Empty)
            {
                // カーソルが無ければ、交差も無い。合同ともしない
                HasIntersectionBetweenCroppedCursorAndRegisteredTile = false;
                IsCongruenceBetweenCroppedCursorAndRegisteredTile = false;
                return;
            }

            // 軽くはない処理
            HasIntersectionBetweenCroppedCursorAndRegisteredTile = TilesetSettingsVM.HasIntersection(CroppedCursorPointedTileSourceRect);
            IsCongruenceBetweenCroppedCursorAndRegisteredTile = TilesetSettingsVM.IsCongruence(CroppedCursorPointedTileSourceRect);

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
                startPoint: PointingDeviceStartPoint,
                endPoint: PointingDeviceCurrentPoint,
                gridLeftTop: Owner.WorkingGridPhase,
                gridTile: Owner.WorkingGridUnit);

            // ズームを除去
            var sourceRect = new RectangleInt(
                location: new PointInt(
                    x: new XInt((int)(workingRect.Location.X.AsFloat / OwnerZoomAsFloat)),
                    y: new YInt((int)(workingRect.Location.Y.AsFloat / OwnerZoomAsFloat))),
                size: new SizeInt(
                    width: new WidthInt((int)(workingRect.Size.Width.AsFloat / OwnerZoomAsFloat)),
                    height: new HeightInt((int)(workingRect.Size.Height.AsFloat / OwnerZoomAsFloat))));

            //
            // 計算値の反映
            // ============
            //
            // Trace.WriteLine($"[TileCropPage.xaml.cs RefreshTileForm] context.IsMouseDragging: {context.IsMouseDragging}, context.HalfThicknessOfTileCursorLine.AsInt: {context.HalfThicknessOfTileCursorLine.AsInt}, rect x:{rect.Point.X.AsInt} y:{rect.Point.Y.AsInt} width:{rect.Size.Width.AsInt} height:{rect.Size.Height.AsInt}");
            CroppedCursorPointedTileSourceRect = sourceRect;

            //
            // 登録済みのタイルと被っていないか判定
            // ====================================
            //
            //      - （軽くない処理）
            //
            RecalculateBetweenCroppedCursorAndRegisteredTile();

            //
            // 切抜きカーソル更新
            // ==================
            //
            LoadCroppedCursorPointedTile();

            // （切抜きカーソル更新後）［追加／上書き］ボタン再描画
            RefreshAddsButton();

            // （切抜きカーソル更新後）［削除］ボタン活性化
            RefreshDeletesButton();

            // ［追加／復元］ボタン
            Owner.InvalidateAddsButton();

            // タイル・タイトル
            Owner.InvalidateTileTitle();
        }
        #endregion

        #region メソッド（［追加／上書き］ボタン　関連）
        /// <summary>
        ///     ［追加／上書き］ボタンの再描画
        /// </summary>
        internal void RefreshAddsButton()
        {
            // 切抜きカーソルが、登録済みタイルのいずれかと交差しているか？
            if (HasIntersectionBetweenCroppedCursorAndRegisteredTile)
            {
                // 合同のときは「交差中」とは表示しない
                if (!IsCongruenceBetweenCroppedCursorAndRegisteredTile)
                {
                    // 「交差中」
                    // Trace.WriteLine("[TileCropPage.xml.cs InvalidateAddsButton] 交差中だ");

                    Owner.AddsButtonText = (string)LocalizationResourceManager.Instance["Intersecting"];
                    return;
                }
            }

            var contents = CroppedCursorPointedTileRecordVisually;

            if (contents.IsNone)
            {
                // ［切抜きカーソル］の指すタイル無し時

                // 「追加」
                Owner.AddsButtonText = (string)LocalizationResourceManager.Instance["Add"];
            }
            else
            {
                // 切抜きカーソル有り時
                // Ｉｄ未設定時

                if (CroppedCursorPointedTileIdOrEmpty == TileIdOrEmpty.Empty)
                {
                    // Ｉｄが空欄
                    // ［追加］（新規作成）だ

                    // ［追加」
                    Owner.AddsButtonText = (string)LocalizationResourceManager.Instance["Add"];
                }
                else
                {
                    // ［復元」
                    Owner.AddsButtonText = (string)LocalizationResourceManager.Instance["Restore"];
                }
            }

            // ［追加／復元］ボタンの活性性
            Owner.InvalidateAddsButton();
        }
        #endregion

        #region メソッド（［削除］ボタン　関連）
        /// <summary>
        ///     ［削除］ボタンの再描画
        /// </summary>
        internal void RefreshDeletesButton()
        {
            var contents = CroppedCursorPointedTileRecordVisually;

            if (contents.IsNone)
            {
                // 切抜きカーソル無し時
                Owner.IsEnabledDeletesButton = false;
                return;
            }

            // 切抜きカーソル有り時
            if (contents.Id == TileIdOrEmpty.Empty)
            {
                // Ｉｄ未設定時
                Owner.IsEnabledDeletesButton = false;
            }
            else
            {
                // タイル登録済み時
                Owner.IsEnabledDeletesButton = true;
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

            ReactOnVisited();

            //
            // タイル設定ファイルの読込
            // ========================
            //
            if (TilesetDatatableVisually.LoadCSV(
                tilesetDatatableFileLocation: TilesetDatatableFileLocation,
                zoom: Zoom,
                tilesetDatatableVisually: out TilesetDatatableVisually tilesetDatatableVisually))
            {
                Owner.TilesetSettingsVM = tilesetDatatableVisually;

#if DEBUG
                // ファイルの整合性チェック（重い処理）
                if (TilesetSettingsVM.IsValid())
                {
                    Trace.WriteLine($"[TileCropPage.xaml.cs ContentPage_Loaded] ファイルの内容は妥当　File: {TilesetDatatableFileLocation.Path.AsStr}");
                }
                else
                {
                    Trace.WriteLine($"[TileCropPage.xaml.cs ContentPage_Loaded] ファイルの内容に異常あり　File: {TilesetDatatableFileLocation.Path.AsStr}");
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
            var tilesetImageFilePathAsStr = Owner.TilesetImageFilePathAsStr;

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
                            Owner.SetTilesetSourceBitmap(SKBitmap.Decode(memStream));

                            // 複製
                            Owner.TilesetWorkingBitmap = SKBitmap.FromImage(SKImage.FromBitmap(Owner.TilesetSourceBitmap));

                            // 画像処理（明度を下げる）
                            FeatSkia.ReduceBrightness.DoItInPlace(Owner.TilesetWorkingBitmap);
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
            if (CroppedCursorPointedTileIdOrEmpty == TileIdOrEmpty.Empty)
            {
                // Ｉｄが空欄
                // ［追加］（新規作成）だ

                // 登録タイル追加
                AddRegisteredTile();
            }
            else
            {
                // 上書きボタンだが、［上書き］処理をする
                OverwriteRegisteredTile();
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
            Owner.IsMouseDragging = !Owner.IsMouseDragging;

            if (Owner.IsMouseDragging)
            {
                //
                // 疑似マウス・ダウン
                // ==================
                //
                Trace.WriteLine("[TileCropPage.xml.cs TileImage_OnTapped] 疑似マウス・ダウン");

                // ポイントしている位置
                PointingDeviceCurrentPoint = PointingDeviceStartPoint = new PointFloat(
                    new XFloat((float)tappedPoint.X),
                    new YFloat((float)tappedPoint.Y));
                // Trace.WriteLine($"[TileCropPage TileImage_OnTapped] tapped x:{PointingDeviceStartPoint.X.AsInt} y:{PointingDeviceStartPoint.Y.AsInt}");

                // タイル・フォームの表示更新
                RefreshTileForm();

                TrickRefreshCanvasOfTileCursor(codePlace: "[TileCropPage.xml.cs TileImage_OnTapped 疑似マウスダウン]");
            }
            else
            {
                //
                // 疑似マウス・アップ
                // ==================
                //

                Trace.WriteLine("[TileCropPage.xml.cs TileImage_OnTapped] 疑似マウス・アップ");

                // ポイントしている位置
                PointingDeviceCurrentPoint = new PointFloat(
                    new XFloat((float)tappedPoint.X),
                    new YFloat((float)tappedPoint.Y));
                // Trace.WriteLine($"[TileCropPage PointerGestureRecognizer_PointerExited] exited x:{PointingDeviceCurrentPoint.X.AsInt} y:{PointingDeviceCurrentPoint.Y.AsInt}");

                // タイル・フォームの表示更新
                RefreshTileForm();

                TrickRefreshCanvasOfTileCursor(codePlace: "[TileCropPage.xml.cs TileImage_OnTapped 疑似マウスアップ]");
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
            if (Owner.IsMouseDragging)
            {
                //
                // 疑似マウス・ドラッグ
                // ====================
                //

                // ポイントしている位置
                PointingDeviceCurrentPoint = new PointFloat(
                    new XFloat((float)tappedPoint.X),
                    new YFloat((float)tappedPoint.Y));
                // Trace.WriteLine($"[TileCropPage PointerGestureRecognizer_PointerMoved] moved x:{PointingDeviceCurrentPoint.X.AsInt} y:{PointingDeviceCurrentPoint.Y.AsInt}");

                // タイル・フォームの表示更新
                RefreshTileForm();

                TrickRefreshCanvasOfTileCursor(codePlace: "[TileCropPage.xml.cs PointerGestureRecognizer_PointerMoved 疑似マウスドラッグ]");
            }
        }
        #endregion

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
        Zoom zoom = Zoom.IdentityElement;

        /// <summary>
        ///     ［ズーム］最大
        /// </summary>
        Zoom zoomMax = new(4.0f);

        /// <summary>
        ///     ［ズーム］最小
        /// </summary>
        Zoom zoomMin = new(0.5f);
        #endregion

        #region フィールド（［タイルセット元画像］　関連）
        /// <summary>
        ///     ［タイルセット元画像］サイズ
        /// </summary>
        SizeInt tilesetSourceImageSize = SizeInt.Empty;
        #endregion

        #region フィールド（［切抜きカーソル］　関連）
        WidthFloat trickWidth = WidthFloat.Zero;
        #endregion
    }
}
