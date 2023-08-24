namespace _2D_RPG_Negiramen.ViewModels
{
    using _2D_RPG_Negiramen.Models;
    using _2D_RPG_Negiramen.Models.Geometric;
    using _2D_RPG_Negiramen.Models.Visually;
    using _2D_RPG_Negiramen.ViewInnerModels;
    using CommunityToolkit.Mvvm.ComponentModel;
    using SkiaSharp;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Globalization;
    using TheFileEntryLocations = _2D_RPG_Negiramen.Models.FileEntries.Locations;
    using TheGraphics = Microsoft.Maui.Graphics;

#if IOS || ANDROID || MACCATALYST
    using Microsoft.Maui.Graphics.Platform;
    using SkiaSharp.Views.Maui.Controls;
    using _2D_RPG_Negiramen.Models.Geometric;
    using _2D_RPG_Negiramen.Models.Visually;
    using _2D_RPG_Negiramen.ViewHistory.TileCropPage;
#elif WINDOWS
    using Microsoft.Maui.Graphics.Win2D;
    using System.Net;
    using SkiaSharp.Views.Maui.Controls;
    using _2D_RPG_Negiramen.ViewHistory.TileCropPage;
#endif

    /// <summary>
    ///     😁 ［タイル切抜きページ］ビューモデル
    /// </summary>
    [QueryProperty(nameof(TilesetImageFile), queryId: "TilesetImageFile")]
    [QueryProperty(nameof(TilesetDatatableFileLocation), queryId: "TilesetSettingsFile")]
    class TileCropPageViewModel : ObservableObject, ITileCropPageViewModel
    {
        // - その他

        #region その他（生成）
        /// <summary>
        ///     生成
        ///     
        ///     <list type="bullet">
        ///         <item>ビュー・モデルのデフォルト・コンストラクターは public 修飾にする必要がある</item>
        ///     </list>
        /// </summary>
        public TileCropPageViewModel()
        {
            this.Inner = new TileCropPageViewInnerModel(this);

            // 循環参照しないように注意
            this.HalfThicknessOfTileCursorLine = new Models.ThicknessOfLine(2 * this.HalfThicknessOfGridLine.AsInt);
        }
        #endregion

        // - パブリック変更通知プロパティ

        #region 変更通知プロパティ（ロケール　関連）
        /// <summary>
        ///     現在選択中の文化情報。文字列形式
        /// </summary>
        public CultureInfo SelectedCultureInfo
        {
            get => LocalizationResourceManager.Instance.CultureInfo;
            set
            {
                if (LocalizationResourceManager.Instance.CultureInfo != value)
                {
                    CultureInfo oldValue = LocalizationResourceManager.Instance.CultureInfo;
                    CultureInfo newValue = value;

                    LocalizationResourceManager.Instance.SetCulture(value);
                    OnPropertyChanged(nameof(SelectedCultureInfo));

                    // 再帰的
                    App.History.Do(new SetCultureInfoProcessing(
                        inner: this.Inner,
                        oldValue: oldValue,
                        newValue: newValue));
                }
            }
        }

        /// <summary>
        ///     文化情報のリスト
        /// </summary>
        public ObservableCollection<CultureInfo> CultureInfoCollection => App.CultureInfoCollection;
        #endregion

        #region 変更通知プロパティ（履歴　関連）
        /// <summary>
        ///     アンドゥできるか？
        /// </summary>
        public bool CanUndo => App.History.CanUndo();

        /// <summary>
        ///     リドゥできるか？
        /// </summary>
        public bool CanRedo => App.History.CanRedo();
        #endregion

        #region 変更通知プロパティ（タイルセット設定ビューモデル）
        /// <summary>
        ///     タイルセット設定ビューモデル
        /// </summary>
        public TilesetDatatableVisually TilesetSettingsVM
        {
            get => this._tilesetSettingsVM;
            set
            {
                if (this._tilesetSettingsVM != value)
                {
                    this._tilesetSettingsVM = value;
                    OnPropertyChanged(nameof(TilesetSettingsVM));

                    // TODO これ要るか？ 再描画
                    InvalidateTileIdChange();
                }
            }
        }
        #endregion

        #region 変更通知プロパティ（［タイルセット元画像］　関連）
        /// <summary>
        ///     ［タイルセット元画像］の横幅。読取専用
        /// </summary>
        public int TilesetSourceImageWidthAsInt => this.Inner.TilesetSourceImageSize.Width.AsInt;

        /// <summary>
        ///     ［タイルセット元画像］の縦幅。読取専用
        /// </summary>
        public int TilesetSourceImageHeightAsInt => this.Inner.TilesetSourceImageSize.Height.AsInt;
        #endregion

        #region 変更通知プロパティ（［タイルセット作業画像］　関連）
        /// <summary>
        ///     ［タイルセット作業画像］の横幅。読取専用
        /// </summary>
        public int TilesetWorkingImageWidthAsInt => workingImageSize.Width.AsInt;

        /// <summary>
        ///     ［タイルセット作業画像］の縦幅。読取専用
        /// </summary>
        public int TilesetWorkingImageHeightAsInt => workingImageSize.Height.AsInt;
        #endregion

        #region 変更通知プロパティ（［ズーム］　関連）
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
                this.Inner.TrickRefreshCanvasOfTileCursor("[TileCropPageViewModel.cs ZoomAsFloat]");

                if (this.zoom.AsFloat != value)
                {
                    if (this.ZoomMinAsFloat <= value && value <= this.ZoomMaxAsFloat)
                    {
                        Zoom oldValue = this.zoom;
                        Zoom newValue = new Models.Geometric.Zoom(value);

                        this.zoom = newValue;

                        // 再帰的にズーム再変更、かつ変更後の影響を処理
                        App.History.Do(new ZoomProcessing(this.Inner, oldValue, newValue));
                    }
                }
            }
        }
        #endregion

        #region 変更通知プロパティ（［元画像グリッド］　関連）
        /// <summary>
        ///     ［元画像グリッド］のキャンバスの画像サイズ
        ///         
        ///     <list type="bullet">
        ///         <item>グリッドの線の太さを 2px と想定しているので、グリッドの線が画像の端っこで切れないように、グリッドの内部的なキャンバス・サイズを 2px 広げる</item>
        ///     </list>
        /// </summary>
        public Models.Geometric.SizeInt GridCanvasImageSize
        {
            get => this.gridCanvasImageSize;
            set
            {
                if (this.gridCanvasImageSize != value)
                {
                    this.GridCanvasImageWidthAsInt = value.Width.AsInt;
                    this.GridCanvasImageHeightAsInt = value.Height.AsInt;
                }
            }
        }

        /// <summary>
        ///     ［元画像グリッド］のキャンバスの横幅
        ///     
        ///     <list type="bullet">
        ///         <item>グリッドの線の太さを 2px と想定しているので、グリッドの線が画像の端っこで切れないように、グリッドのキャンバス・サイズを 2px 広げる</item>
        ///     </list>
        /// </summary>
        public int GridCanvasImageWidthAsInt
        {
            get => this.GridCanvasImageSize.Width.AsInt;
            set
            {
                if (this.gridCanvasImageSize.Width.AsInt != value)
                {
                    this.gridCanvasImageSize = new Models.Geometric.SizeInt(new Models.Geometric.WidthInt(value), this.gridCanvasImageSize.Height);
                    OnPropertyChanged(nameof(GridCanvasImageWidthAsInt));
                    OnPropertyChanged(nameof(GridCanvasImageSize));
                }
            }
        }

        /// <summary>
        ///     ［元画像グリッド］のキャンバスの縦幅
        ///
        ///     <list type="bullet">
        ///         <item>グリッドの線の太さを 2px と想定しているので、グリッドの線が画像の端っこで切れないように、グリッドのキャンバス・サイズを 2px 広げる</item>
        ///     </list>
        /// </summary>
        public int GridCanvasImageHeightAsInt
        {
            get => this.GridCanvasImageSize.Height.AsInt;
            set
            {
                if (this.gridCanvasImageSize.Height.AsInt != value)
                {
                    this.gridCanvasImageSize = new Models.Geometric.SizeInt(this.gridCanvasImageSize.Width, new Models.Geometric.HeightInt(value));
                    OnPropertyChanged(nameof(GridCanvasImageHeightAsInt));
                    OnPropertyChanged(nameof(GridCanvasImageSize));
                }
            }
        }

        /// <summary>
        ///     ［元画像グリッド］の線の太さの半分
        /// </summary>
        public int HalfThicknessOfGridLineAsInt => this.HalfThicknessOfGridLine.AsInt;

        /// <summary>
        ///     ［元画像グリッド］の線の半分の太さ
        /// </summary>
        internal ThicknessOfLine HalfThicknessOfGridLine
        {
            get => this.halfThicknessOfGridLine;
            set
            {
                if (this.halfThicknessOfGridLine != value)
                {
                    this.halfThicknessOfGridLine = value;
                    OnPropertyChanged(nameof(HalfThicknessOfGridLineAsInt));
                    OnPropertyChanged(nameof(HalfThicknessOfGridLine));
                }
            }
        }

        /// <summary>
        ///     ［元画像グリッド］の位相の左上表示位置。元画像ベース
        /// </summary>
        public Models.Geometric.PointInt GridPhaseSourceLocation
        {
            get => this.gridPhaseSourceLocation;
            set
            {
                if (this.gridPhaseSourceLocation != value)
                {
                    this.GridPhaseSourceLeftAsInt = value.X.AsInt;
                    this.GridPhaseSourceTopAsInt = value.Y.AsInt;
                }
            }
        }

        /// <summary>
        ///     ［元画像グリッド］の位相の左上表示位置ｘ。元画像ベース
        /// </summary>
        public int GridPhaseSourceLeftAsInt
        {
            get => this.gridPhaseSourceLocation.X.AsInt;
            set
            {
                if (this.gridPhaseSourceLocation.X.AsInt != value)
                {
                    this.gridPhaseSourceLocation = new Models.Geometric.PointInt(new Models.Geometric.XInt(value), this.gridPhaseSourceLocation.Y);
                    this.WorkingGridPhaseLeftAsFloat = this.ZoomAsFloat * this.gridPhaseSourceLocation.X.AsInt;

                    // キャンバスを再描画
                    InvalidateGraphicsViewOfGrid();

                    // キャンバスを再描画後に変更通知
                    OnPropertyChanged(nameof(GridPhaseSourceLeftAsInt));
                    OnPropertyChanged(nameof(GridPhaseSourceLocation));

                    OnPropertyChanged(nameof(WorkingGridPhaseLeftAsFloat));
                    OnPropertyChanged(nameof(WorkingGridPhase));
                }
            }
        }

        /// <summary>
        ///     ［元画像グリッド］の位相の左上表示位置ｙ。元画像ベース
        /// </summary>
        public int GridPhaseSourceTopAsInt
        {
            get => this.gridPhaseSourceLocation.Y.AsInt;
            set
            {
                if (this.gridPhaseSourceLocation.Y.AsInt != value)
                {
                    this.gridPhaseSourceLocation = new Models.Geometric.PointInt(this.gridPhaseSourceLocation.X, new Models.Geometric.YInt(value));
                    this.WorkingGridPhaseTopAsFloat = (float)(this.ZoomAsFloat * this.gridPhaseSourceLocation.Y.AsInt);

                    // キャンバスを再描画
                    InvalidateGraphicsViewOfGrid();

                    // キャンバスを再描画後に変更通知
                    OnPropertyChanged(nameof(GridPhaseSourceTopAsInt));
                    OnPropertyChanged(nameof(GridPhaseSourceLocation));

                    OnPropertyChanged(nameof(WorkingGridPhaseTopAsFloat));
                    OnPropertyChanged(nameof(WorkingGridPhase));
                }
            }
        }

        /// <summary>
        ///     ［元画像グリッド］のタイルのサイズ。元画像ベース
        /// </summary>
        public Models.Geometric.SizeInt SourceGridUnit
        {
            get => this.sourceGridUnit;
            set
            {
                if (this.sourceGridUnit != value)
                {
                    this.SourceGridTileWidthAsInt = value.Width.AsInt;
                    this.SourceGridTileHeightAsInt = value.Height.AsInt;
                }
            }
        }

        /// <summary>
        ///     ［元画像グリッド］のタイルの横幅。元画像ベース
        /// </summary>
        public int SourceGridTileWidthAsInt
        {
            get => this.sourceGridUnit.Width.AsInt;
            set
            {
                if (this.sourceGridUnit.Width.AsInt != value &&
                    // バリデーション
                    0 < value && value <= this.TileMaxWidthAsInt)
                {
                    this.sourceGridUnit = new Models.Geometric.SizeInt(new Models.Geometric.WidthInt(value), this.sourceGridUnit.Height);

                    // 作業グリッド・タイル横幅の再計算
                    this.Inner.RefreshWorkingGridTileWidth();

                    // カーソルの線の幅を含まない
                    this.CroppedCursorPointedTileWorkingWidthAsFloat = this.ZoomAsFloat * this.sourceGridUnit.Width.AsInt;

                    // キャンバスを再描画
                    InvalidateGraphicsViewOfGrid();
                    // RefreshCanvasOfTileCursor(codePlace: "[TileCropPageViewModel SourceGridTileWidthAsInt set]");

                    // キャンバスを再描画後に変更通知
                    OnPropertyChanged(nameof(SourceGridTileWidthAsInt));
                    OnPropertyChanged(nameof(SourceGridUnit));
                }
            }
        }

        /// <summary>
        ///     ［元画像グリッド］のタイルの縦幅。元画像ベース
        /// </summary>
        public int SourceGridTileHeightAsInt
        {
            get => this.sourceGridUnit.Height.AsInt;
            set
            {
                if (this.sourceGridUnit.Height.AsInt != value &&
                    // バリデーション
                    0 < value && value <= this.TileMaxHeightAsInt)
                {
                    this.sourceGridUnit = new Models.Geometric.SizeInt(this.sourceGridUnit.Width, new Models.Geometric.HeightInt(value));

                    // 作業グリッド・タイル横幅の再計算
                    this.Inner.RefreshWorkingGridTileHeight();

                    // カーソルの線の幅を含まない
                    this.CroppedCursorPointedTileWorkingHeightAsFloat = this.ZoomAsFloat * this.sourceGridUnit.Height.AsInt;

                    // キャンバスを再描画
                    InvalidateGraphicsViewOfGrid();
                    // RefreshCanvasOfTileCursor(codePlace: "[TileCropPageViewModel SourceGridTileHeightAsInt set]");

                    // キャンバスを再描画後に変更通知
                    OnPropertyChanged(nameof(SourceGridTileHeightAsInt));
                    OnPropertyChanged(nameof(SourceGridUnit));
                }
            }
        }

        /// <summary>
        ///     ［元画像グリッド］のタイルの最大横幅
        /// </summary>
        public int TileMaxWidthAsInt => App.GetOrLoadSettings().TileMaxSize.Width.AsInt;

        /// <summary>
        ///     ［元画像グリッド］のタイルの最大縦幅
        /// </summary>
        public int TileMaxHeightAsInt => App.GetOrLoadSettings().TileMaxSize.Height.AsInt;
        #endregion

        #region 変更通知プロパティ（［作業グリッド］　関連）
        /// <summary>
        ///     ［作業グリッド］の位相の左上表示位置
        /// </summary>
        public Models.Geometric.PointFloat WorkingGridPhase
        {
            get => this.workingGridPhase;
            set
            {
                if (this.workingGridPhase != value)
                {
                    this.WorkingGridPhaseLeftAsFloat = value.X.AsFloat;
                    this.WorkingGridPhaseTopAsFloat = value.Y.AsFloat;
                }
            }
        }

        /// <summary>
        ///     ［作業グリッド］の位相の左上表示位置ｘ（読取専用）
        /// </summary>
        public float WorkingGridPhaseLeftAsFloat
        {
            get => this.workingGridPhase.X.AsFloat;
            set
            {
                if (this.workingGridPhase.X.AsFloat != value)
                {
                    this.workingGridPhase = new Models.Geometric.PointFloat(
                        x: new Models.Geometric.XFloat(value),
                        y: this.workingGridPhase.Y);

                    OnPropertyChanged(nameof(WorkingGridPhaseLeftAsFloat));
                    OnPropertyChanged(nameof(WorkingGridPhase));
                }
            }
        }

        /// <summary>
        ///     ［作業グリッド］の位相の左上表示位置ｙ（読取専用）
        /// </summary>
        public float WorkingGridPhaseTopAsFloat
        {
            get => this.workingGridPhase.Y.AsFloat;
            set
            {
                if (this.workingGridPhase.Y.AsFloat != value)
                {
                    this.workingGridPhase = new Models.Geometric.PointFloat(
                        x: this.workingGridPhase.X,
                        y: new Models.Geometric.YFloat(value));

                    OnPropertyChanged(nameof(WorkingGridPhaseTopAsFloat));
                    OnPropertyChanged(nameof(WorkingGridPhase));
                }
            }
        }

        /// <summary>
        ///     ［作業グリッド］のタイルのサイズ（読取専用）
        /// </summary>
        public Models.Geometric.SizeFloat WorkingGridUnit
        {
            get => this.workingGridUnit;
            set
            {
                if (this.workingGridUnit != value)
                {
                    this.WorkingGridTileWidthAsFloat = value.Width.AsFloat;
                    this.WorkingGridTileHeightAsFloat = value.Height.AsFloat;
                }
            }
        }

        /// <summary>
        ///     ［作業グリッド］のタイルの横幅（読取専用）
        /// </summary>
        public float WorkingGridTileWidthAsFloat
        {
            get => this.workingGridUnit.Width.AsFloat;
            set
            {
                if (this.workingGridUnit.Width.AsFloat != value)
                {
                    this.workingGridUnit = new Models.Geometric.SizeFloat(
                        width: new Models.Geometric.WidthFloat(value),
                        height: this.WorkingGridUnit.Height);

                    OnPropertyChanged(nameof(WorkingGridTileWidthAsFloat));
                    OnPropertyChanged(nameof(WorkingGridUnit));
                }
            }
        }

        /// <summary>
        ///     ［作業グリッド］のタイルの縦幅（読取専用）
        /// </summary>
        public float WorkingGridTileHeightAsFloat
        {
            get => this.workingGridUnit.Height.AsFloat;
            set
            {
                if (this.workingGridUnit.Height.AsFloat != value)
                {
                    this.workingGridUnit = new Models.Geometric.SizeFloat(
                        width: this.WorkingGridUnit.Width,
                        height: new Models.Geometric.HeightFloat(value));

                    OnPropertyChanged(nameof(WorkingGridTileHeightAsFloat));
                    OnPropertyChanged(nameof(WorkingGridUnit));
                }
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
                    OnPropertyChanged(nameof(IsMouseDragging));
                }
            }
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
        public Models.Geometric.RectangleInt CroppedCursorPointedTileSourceRect
        {
            get
            {
                var contents = this.Inner.CroppedCursorPointedTileRecordVisually;

                if (contents.IsNone)
                {
                    // ［切抜きカーソル］の指すタイル無し時
                    return Models.Geometric.RectangleInt.Empty;
                }

                return contents.SourceRectangle;
            }
            set
            {
                var contents = this.Inner.CroppedCursorPointedTileRecordVisually;

                if (contents.IsNone)
                {
                    // ［切抜きカーソル］の指すタイル無し時

                }
                else
                {
                    // 値に変化がない
                    if (contents.SourceRectangle == value)
                        return;
                }

                this.CroppedCursorPointedTileSourceLeftAsInt = value.Location.X.AsInt;
                this.CroppedCursorPointedTileSourceTopAsInt = value.Location.Y.AsInt;
                this.CroppedCursorPointedTileSourceSize = value.Size;

                // 切抜きカーソル。ズーム済み
                // this.CroppedCursorPointedTileWorkingLeftAsFloat = this.ZoomAsFloat * this.CroppedCursorPointedTileSourceLeftAsInt;
                OnPropertyChanged(nameof(CroppedCursorPointedTileWorkingLeftAsFloat));
                OnPropertyChanged(nameof(CroppedCursorWorkingPointAsMargin));
                OnPropertyChanged(nameof(CroppedCursorPointedTileWorkingLeftAsPresentableText));

                // this.CroppedCursorPointedTileWorkingTopAsFloat = this.ZoomAsFloat * this.CroppedCursorPointedTileSourceTopAsInt;
                OnPropertyChanged(nameof(CroppedCursorPointedTileWorkingTopAsFloat));
                OnPropertyChanged(nameof(CroppedCursorWorkingPointAsMargin));
                OnPropertyChanged(nameof(CroppedCursorPointedTileWorkingTopAsPresentableText));

                this.CroppedCursorPointedTileWorkingWidthWithoutTrick = new Models.Geometric.WidthFloat(this.ZoomAsFloat * value.Size.Width.AsInt);
                this.CroppedCursorPointedTileWorkingHeight = new Models.Geometric.HeightFloat(this.ZoomAsFloat * value.Size.Height.AsInt);
            }
        }

        /// <summary>
        ///     ［切抜きカーソルが指すタイル］の元画像ベースの位置ｘ
        /// </summary>
        public int CroppedCursorPointedTileSourceLeftAsInt
        {
            get
            {
                var contents = this.Inner.CroppedCursorPointedTileRecordVisually;

                if (contents.IsNone)
                {
                    // ［切抜きカーソル］無し時
                    return 0;
                }

                return contents.SourceRectangle.Location.X.AsInt;
            }
            set
            {
                var currentTileVisually = this.Inner.CroppedCursorPointedTileRecordVisually;

                if (currentTileVisually.IsNone)
                {
                    // ［切抜きカーソル］無し時

                    this.Inner.CroppedCursorPointedTileRecordVisually = TileRecordVisually.FromModel(
                        tileRecord: new Models.TileRecord(
                            id: Models.TileIdOrEmpty.Empty,
                            // 元画像ベース
                            rect: new Models.Geometric.RectangleInt(
                                location: new Models.Geometric.PointInt(new Models.Geometric.XInt(value), Models.Geometric.YInt.Empty),
                                size: Models.Geometric.SizeInt.Empty),
                            title: Models.TileTitle.Empty,
                            logicalDelete: Models.LogicalDelete.False),
                        zoom: this.Zoom
#if DEBUG
                        , hint: "[TileCropPageViewModel.cs CroppedCursorPointedTileSourceLeftAsInt 1]"
#endif
                        );
                }
                else
                {
                    // 値に変化がない
                    if (currentTileVisually.SourceRectangle.Location.X.AsInt == value)
                        return;

                    this.Inner.CroppedCursorPointedTileRecordVisually = TileRecordVisually.FromModel(
                        tileRecord: new Models.TileRecord(
                            id: currentTileVisually.Id,
                            // 元画像ベース
                            rect: new Models.Geometric.RectangleInt(
                                location: new Models.Geometric.PointInt(new Models.Geometric.XInt(value), currentTileVisually.SourceRectangle.Location.Y),
                                size: currentTileVisually.SourceRectangle.Size),
                            title: currentTileVisually.Title,
                            logicalDelete: currentTileVisually.LogicalDelete),
                        zoom: this.Zoom
#if DEBUG
                        , hint: "[TileCropPageViewModel.cs CroppedCursorPointedTileSourceLeftAsInt 2]"
#endif
                        );
                }

                // 切抜きカーソル。ズーム済み
                // this.CroppedCursorPointedTileWorkingLeftAsFloat = this.ZoomAsFloat * this.CroppedCursorPointedTileSourceLeftAsInt;
                OnPropertyChanged(nameof(CroppedCursorPointedTileWorkingLeftAsFloat));
                OnPropertyChanged(nameof(CroppedCursorWorkingPointAsMargin));
                OnPropertyChanged(nameof(CroppedCursorPointedTileWorkingLeftAsPresentableText));

                // this.CroppedCursorPointedTileWorkingTopAsFloat = this.ZoomAsFloat * this.CroppedCursorPointedTileSourceTopAsInt;
                OnPropertyChanged(nameof(CroppedCursorPointedTileWorkingTopAsFloat));
                OnPropertyChanged(nameof(CroppedCursorWorkingPointAsMargin));
                OnPropertyChanged(nameof(CroppedCursorPointedTileWorkingTopAsPresentableText));

                // TODO サイズは変化無しか？

                OnPropertyChanged(nameof(CroppedCursorPointedTileSourceLeftAsInt));
                OnPropertyChanged(nameof(CroppedCursorPointedTileSourceRect));
            }
        }

        /// <summary>
        ///     ［切抜きカーソルが指すタイル］の元画像ベースの位置ｙ
        /// </summary>
        public int CroppedCursorPointedTileSourceTopAsInt
        {
            get
            {
                var contents = this.Inner.CroppedCursorPointedTileRecordVisually;

                // ［切抜きカーソル］無し時
                if (contents.IsNone)
                    return 0;

                return contents.SourceRectangle.Location.Y.AsInt;
            }
            set
            {
                var currentTileVisually = this.Inner.CroppedCursorPointedTileRecordVisually;

                if (currentTileVisually.IsNone)
                {
                    // ［切抜きカーソル］無し時

                    this.Inner.CroppedCursorPointedTileRecordVisually = TileRecordVisually.FromModel(
                        tileRecord: new Models.TileRecord(
                            id: Models.TileIdOrEmpty.Empty,
                            // 元画像ベース
                            rect: new Models.Geometric.RectangleInt(
                            location: new Models.Geometric.PointInt(Models.Geometric.XInt.Empty, new Models.Geometric.YInt(value)),
                            size: Models.Geometric.SizeInt.Empty),
                            title: Models.TileTitle.Empty,
                            logicalDelete: Models.LogicalDelete.False),
                        zoom: this.Zoom
#if DEBUG
                        , hint: "[TileCropPageViewModel.cs CroppedCursorPointedTileSourceTopAsInt 1]"
#endif
                        );
                }
                else
                {
                    // 値に変化がない
                    if (currentTileVisually.SourceRectangle.Location.Y.AsInt == value)
                        return;

                    this.Inner.CroppedCursorPointedTileRecordVisually = TileRecordVisually.FromModel(
                        tileRecord: new Models.TileRecord(
                            id: currentTileVisually.Id,
                            // 元画像ベース
                            rect: new Models.Geometric.RectangleInt(
                            location: new Models.Geometric.PointInt(currentTileVisually.SourceRectangle.Location.X, new Models.Geometric.YInt(value)),
                            size: currentTileVisually.SourceRectangle.Size),
                            title: currentTileVisually.Title,
                            logicalDelete: currentTileVisually.LogicalDelete),
                        zoom: this.Zoom
#if DEBUG
                        , hint: "[TileCropPageViewModel.cs CroppedCursorPointedTileSourceTopAsInt 2]"
#endif
                        );
                }

                // 切抜きカーソル。ズーム済み
                // this.CroppedCursorPointedTileWorkingLeftAsFloat = this.ZoomAsFloat * this.CroppedCursorPointedTileSourceLeftAsInt;
                OnPropertyChanged(nameof(CroppedCursorPointedTileWorkingLeftAsFloat));
                OnPropertyChanged(nameof(CroppedCursorWorkingPointAsMargin));
                OnPropertyChanged(nameof(CroppedCursorPointedTileWorkingLeftAsPresentableText));

                // this.CroppedCursorPointedTileWorkingTopAsFloat = this.ZoomAsFloat * this.CroppedCursorPointedTileSourceTopAsInt;
                OnPropertyChanged(nameof(CroppedCursorPointedTileWorkingTopAsFloat));
                OnPropertyChanged(nameof(CroppedCursorWorkingPointAsMargin));
                OnPropertyChanged(nameof(CroppedCursorPointedTileWorkingTopAsPresentableText));

                // TODO サイズは変化無しか？

                OnPropertyChanged(nameof(CroppedCursorPointedTileSourceTopAsInt));
                OnPropertyChanged(nameof(CroppedCursorPointedTileSourceRect));
            }
        }

        /// <summary>
        ///     ［切抜きカーソル］元画像ベースのサイズ
        ///     
        ///     <list type="bullet">
        ///         <item>線の太さを含まない</item>
        ///     </list>
        /// </summary>
        public Models.Geometric.SizeInt CroppedCursorPointedTileSourceSize
        {
            get
            {
                var contents = this.Inner.CroppedCursorPointedTileRecordVisually;

                // ［切抜きカーソル］無し時
                if (contents.IsNone)
                    return Models.Geometric.SizeInt.Empty;

                return contents.SourceRectangle.Size;
            }
            set
            {
                var contents = this.Inner.CroppedCursorPointedTileRecordVisually;

                if (contents.IsNone)
                {
                    // ［切抜きカーソル］無し時
                }
                else
                {
                    // 値に変化がない
                    if (contents.SourceRectangle.Size == value)
                        return;
                }

                //
                // 選択タイルの横幅と縦幅
                // ======================
                //
                this.CroppedCursorPointedTileSourceWidthAsInt = value.Width.AsInt;
                this.CroppedCursorPointedTileSourceHeightAsInt = value.Height.AsInt;

                OnPropertyChanged(nameof(CroppedCursorPointedTileSourceRect));
            }
        }

        /// <summary>
        ///     ［切抜きカーソルが指すタイル］の元画像ベースの横幅
        /// </summary>
        public int CroppedCursorPointedTileSourceWidthAsInt
        {
            get
            {
                var contents = this.Inner.CroppedCursorPointedTileRecordVisually;

                // ［切抜きカーソル］無し時
                if (contents.IsNone)
                    return 0;

                return contents.SourceRectangle.Size.Width.AsInt;
            }
            set
            {
                var currentTileVisually = this.Inner.CroppedCursorPointedTileRecordVisually;

                if (currentTileVisually.IsNone)
                {
                    // ［切抜きカーソル］無し時
                    this.Inner.CroppedCursorPointedTileRecordVisually = TileRecordVisually.FromModel(
                        tileRecord: new Models.TileRecord(
                            id: Models.TileIdOrEmpty.Empty,
                            rect: new Models.Geometric.RectangleInt(Models.Geometric.PointInt.Empty, new Models.Geometric.SizeInt(new Models.Geometric.WidthInt(value), Models.Geometric.HeightInt.Empty)),
                            title: Models.TileTitle.Empty,
                            logicalDelete: Models.LogicalDelete.False),
                        zoom: this.Zoom
#if DEBUG
                        , hint: "[TileCropPageViewModel.cs CroppedCursorPointedTileSourceWidthAsInt 1]"
#endif
                        );
                }
                else
                {
                    // 値に変化がない
                    if (currentTileVisually.SourceRectangle.Size.Width.AsInt == value)
                        return;

                    this.Inner.CroppedCursorPointedTileRecordVisually = TileRecordVisually.FromModel(
                        tileRecord: new Models.TileRecord(
                            id: currentTileVisually.Id,
                            rect: new Models.Geometric.RectangleInt(currentTileVisually.SourceRectangle.Location, new Models.Geometric.SizeInt(new Models.Geometric.WidthInt(value), currentTileVisually.SourceRectangle.Size.Height)),
                            title: currentTileVisually.Title,
                            logicalDelete: currentTileVisually.LogicalDelete),
                        zoom: this.Zoom
#if DEBUG
                        , hint: "[TileCropPageViewModel.cs CroppedCursorPointedTileSourceWidthAsInt 2]"
#endif
                        );
                }

                // 矩形カーソル。ズーム済み（カーソルの線の幅を含まない）
                CroppedCursorPointedTileWorkingWidthAsFloat = this.ZoomAsFloat * value;

                OnPropertyChanged(nameof(CroppedCursorPointedTileSourceWidthAsInt));
                OnPropertyChanged(nameof(CroppedCursorPointedTileSourceSize));
                OnPropertyChanged(nameof(CroppedCursorPointedTileSourceRect));
            }
        }

        /// <summary>
        ///     ［切抜きカーソルが指すタイル］の元画像ベースの縦幅
        /// </summary>
        public int CroppedCursorPointedTileSourceHeightAsInt
        {
            get
            {
                var contents = this.Inner.CroppedCursorPointedTileRecordVisually;

                // ［切抜きカーソル］無し時
                if (contents.IsNone)
                    return 0;

                return contents.SourceRectangle.Size.Height.AsInt;
            }
            set
            {
                var currentTileVisually = this.Inner.CroppedCursorPointedTileRecordVisually;

                if (currentTileVisually.IsNone)
                {
                    // ［切抜きカーソル］無し時
                    this.Inner.CroppedCursorPointedTileRecordVisually = TileRecordVisually.FromModel(
                        tileRecord: new Models.TileRecord(
                            id: TileIdOrEmpty.Empty,
                            rect: new Models.Geometric.RectangleInt(Models.Geometric.PointInt.Empty, new Models.Geometric.SizeInt(Models.Geometric.WidthInt.Empty, new Models.Geometric.HeightInt(value))),
                            title: Models.TileTitle.Empty,
                            logicalDelete: Models.LogicalDelete.False),
                        zoom: this.Zoom
#if DEBUG
                        , hint: "[TileCropPageViewModel.cs CroppedCursorPointedTileSourceHeightAsInt 1]"
#endif
                        );
                }
                else
                {
                    // 値に変化がない
                    if (currentTileVisually.SourceRectangle.Size.Height.AsInt == value)
                        return;

                    this.Inner.CroppedCursorPointedTileRecordVisually = TileRecordVisually.FromModel(
                        tileRecord: new Models.TileRecord(
                            id: currentTileVisually.Id,
                            rect: new Models.Geometric.RectangleInt(currentTileVisually.SourceRectangle.Location, new Models.Geometric.SizeInt(currentTileVisually.SourceRectangle.Size.Width, new Models.Geometric.HeightInt(value))),
                            title: currentTileVisually.Title,
                            logicalDelete: currentTileVisually.LogicalDelete),
                        zoom: this.Zoom
#if DEBUG
                        , hint: "[TileCropPageViewModel.cs CroppedCursorPointedTileSourceHeightAsInt 2]"
#endif
                        );
                }

                // 切抜きカーソル。ズーム済みの縦幅（カーソルの線の幅を含まない）
                CroppedCursorPointedTileWorkingHeightAsFloat = this.ZoomAsFloat * value;

                OnPropertyChanged(nameof(CroppedCursorPointedTileSourceHeightAsInt));
                OnPropertyChanged(nameof(CroppedCursorPointedTileSourceSize));
                OnPropertyChanged(nameof(CroppedCursorPointedTileSourceRect));
            }
        }

        /// <summary>
        ///     ［切抜きカーソルが指すタイル］のズーム済みの位置（マージンとして）
        /// </summary>
        public Thickness CroppedCursorWorkingPointAsMargin => new(left: this.CroppedCursorPointedTileWorkingLeftAsFloat,
                                                                  top: this.CroppedCursorPointedTileWorkingTopAsFloat,
                                                                  right: 0,
                                                                  bottom: 0);

        /// <summary>
        ///     ［切抜きカーソルが指すタイル］のズーム済みの位置ｘ
        ///         
        ///     <list type="bullet">
        ///         <item>カーソルの線の幅を含まない</item>
        ///     </list>
        /// </summary>
        public float CroppedCursorPointedTileWorkingLeftAsFloat => this.ZoomAsFloat * this.CroppedCursorPointedTileSourceLeftAsInt;

        /// <summary>
        ///     ［切抜きカーソルが指すタイル］のズーム済みの位置ｙ
        ///         
        ///     <list type="bullet">
        ///         <item>カーソルの線の幅を含まない</item>
        ///     </list>
        /// </summary>
        public float CroppedCursorPointedTileWorkingTopAsFloat => this.ZoomAsFloat * this.CroppedCursorPointedTileSourceTopAsInt;

        /// <summary>
        ///     ［切抜きカーソルが指すタイル］のズーム済みの横幅
        ///         
        ///     <list type="bullet">
        ///         <item>カーソルの線の幅を含む</item>
        ///         <item>切抜きカーソルは、対象範囲に外接する</item>
        ///     </list>
        /// </summary>
        public float CanvasOfCroppedCursorWorkingWidthAsFloat => this.croppedCursorPointedTileWorkingWidthWithoutTrick.AsFloat + (4 * this.HalfThicknessOfTileCursorLine.AsInt);

        /// <summary>
        ///     ［切抜きカーソルが指すタイル］のズーム済みの縦幅
        ///         
        ///     <list type="bullet">
        ///         <item>カーソルの線の幅を含む</item>
        ///         <item>切抜きカーソルは、対象範囲に外接する</item>
        ///     </list>
        /// </summary>
        public float CanvasOfCroppedCursorWorkingHeightAsFloat => this.croppedCursorPointedTileWorkingHeight.AsFloat + (4 * this.HalfThicknessOfTileCursorLine.AsInt);

        /// <summary>
        ///     ［切抜きカーソルが指すタイル］のズーム済みのサイズ
        ///         
        ///     <list type="bullet">
        ///         <item>カーソルの線の幅を含まない</item>
        ///     </list>
        /// </summary>
        public Models.Geometric.SizeFloat CroppedCursorPointedTileWorkingSizeWithTrick
        {
            get => new Models.Geometric.SizeFloat(
                    width: new WidthFloat(this.CroppedCursorPointedTileWorkingWidthWithoutTrick.AsFloat + this.TrickWidth.AsFloat),
                    height: this.CroppedCursorPointedTileWorkingHeight);
        }

        public Models.Geometric.WidthFloat TrickWidth
        {
            get
            {
                return this.trickWidth;
            }
            set
            {
                this.trickWidth = value;
            }
        }

        public Models.Geometric.WidthFloat CroppedCursorPointedTileWorkingWidthWithTrick
        {
            get
            {
                return new WidthFloat(this.croppedCursorPointedTileWorkingWidthWithoutTrick.AsFloat + this.TrickWidth.AsFloat);
            }
        }

        public Models.Geometric.WidthFloat CroppedCursorPointedTileWorkingWidthWithoutTrick
        {
            get => this.croppedCursorPointedTileWorkingWidthWithoutTrick;
            set
            {
                this.croppedCursorPointedTileWorkingWidthWithoutTrick = value;
            }
        }

        public Models.Geometric.HeightFloat CroppedCursorPointedTileWorkingHeight
        {
            get => this.croppedCursorPointedTileWorkingHeight;
            set
            {
                this.croppedCursorPointedTileWorkingHeight = value;
            }
        }


        /// <summary>
        ///     ［切抜きカーソルが指すタイル］のズーム済みの横幅
        ///         
        ///     <list type="bullet">
        ///         <item>カーソルの線の幅を含まない</item>
        ///     </list>
        /// </summary>
        public float CroppedCursorPointedTileWorkingWidthAsFloat
        {
            get => this.croppedCursorPointedTileWorkingWidthWithoutTrick.AsFloat;
            set
            {
                if (this.croppedCursorPointedTileWorkingWidthWithoutTrick.AsFloat != value)
                {
                    this.croppedCursorPointedTileWorkingWidthWithoutTrick = new Models.Geometric.WidthFloat(value);

                    // キャンバスを再描画
                    // RefreshCanvasOfTileCursor(codePlace: "[TileCropPageViewModel CroppedCursorPointedTileWorkingWidthAsFloat set]");

                    // キャンバスを再描画後に変更通知
                    OnPropertyChanged(nameof(CroppedCursorPointedTileWorkingWidthAsFloat));
                    OnPropertyChanged(nameof(CroppedCursorPointedTileWorkingHeightAsFloat));
                    OnPropertyChanged(nameof(CroppedCursorPointedTileWorkingSizeWithTrick));

                    OnPropertyChanged(nameof(CanvasOfCroppedCursorWorkingWidthAsFloat));
                    OnPropertyChanged(nameof(CroppedCursorPointedTileWorkingWidthAsPresentableText));
                }
            }
        }

        /// <summary>
        ///     ［切抜きカーソルが指すタイル］のズーム済みの縦幅
        ///         
        ///     <list type="bullet">
        ///         <item>カーソルの線の幅を含まない</item>
        ///     </list>
        /// </summary>
        public float CroppedCursorPointedTileWorkingHeightAsFloat
        {
            get => this.croppedCursorPointedTileWorkingHeight.AsFloat;
            set
            {
                if (this.croppedCursorPointedTileWorkingHeight.AsFloat != value)
                {
                    this.croppedCursorPointedTileWorkingHeight = new Models.Geometric.HeightFloat(value);

                    // キャンバスを再描画
                    // RefreshCanvasOfTileCursor("[TileCropPageViewModel CroppedCursorPointedTileWorkingHeightAsFloat set]");

                    // キャンバスを再描画後に変更通知
                    OnPropertyChanged(nameof(CroppedCursorPointedTileWorkingWidthAsFloat));
                    OnPropertyChanged(nameof(CroppedCursorPointedTileWorkingHeightAsFloat));
                    OnPropertyChanged(nameof(CroppedCursorPointedTileWorkingSizeWithTrick));

                    OnPropertyChanged(nameof(CanvasOfCroppedCursorWorkingHeightAsFloat));
                    OnPropertyChanged(nameof(CroppedCursorPointedTileWorkingHeightAsPresentableText));
                }
            }
        }

        /// <summary>
        ///     ［切抜きカーソルが指すタイル］のズーム済みの位置ｘ
        ///         
        ///     <list type="bullet">
        ///         <item>カーソルの線の幅を含まない</item>
        ///         <item>表示用テキスト</item>
        ///         <item>📖 [Microsoft　＞　Standard numeric format strings](https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings?redirectedfrom=MSDN)  </item>
        ///     </list>
        /// </summary>
        public string CroppedCursorPointedTileWorkingLeftAsPresentableText => this.CroppedCursorPointedTileWorkingLeftAsFloat.ToString("F1");

        /// <summary>
        ///     ［切抜きカーソルが指すタイル］のズーム済みの位置ｙ
        ///         
        ///     <list type="bullet">
        ///         <item>カーソルの線の幅を含まない</item>
        ///         <item>表示用テキスト</item>
        ///     </list>
        /// </summary>
        public string CroppedCursorPointedTileWorkingTopAsPresentableText => this.CroppedCursorPointedTileWorkingTopAsFloat.ToString("F1");

        /// <summary>
        ///     ［切抜きカーソルが指すタイル］のズーム済みの横幅
        ///         
        ///     <list type="bullet">
        ///         <item>カーソルの線の幅を含まない</item>
        ///         <item>表示用テキスト</item>
        ///     </list>
        /// </summary>
        public string CroppedCursorPointedTileWorkingWidthAsPresentableText => this.croppedCursorPointedTileWorkingWidthWithoutTrick.AsFloat.ToString("F1");

        /// <summary>
        ///     ［切抜きカーソルが指すタイル］のズーム済みの縦幅
        ///         
        ///     <list type="bullet">
        ///         <item>カーソルの線の幅を含まない</item>
        ///         <item>表示用テキスト</item>
        ///     </list>
        /// </summary>
        public string CroppedCursorPointedTileWorkingHeightAsPresentableText => this.croppedCursorPointedTileWorkingHeight.AsFloat.ToString("F1");

        /// <summary>
        ///     ［切抜きカーソルが指すタイル］の線の半分の太さ
        /// </summary>
        public ThicknessOfLine HalfThicknessOfTileCursorLine
        {
            get => this.halfThicknessOfTileCursorLine;
            set
            {
                if (this.halfThicknessOfTileCursorLine == value)
                    return;

                this.halfThicknessOfTileCursorLine = value;
                OnPropertyChanged(nameof(HalfThicknessOfTileCursorLine));
            }
        }

        /// <summary>
        ///     ［切抜きカーソルが指すタイル］のＩｄ。BASE64表現
        ///     
        ///     <see cref="CroppedCursorPointedTileIdOrEmpty"/>
        /// </summary>
        public string CroppedCursorPointedTileIdAsBASE64
        {
            get
            {
                var contents = this.Inner.CroppedCursorPointedTileRecordVisually;

                // ［切抜きカーソル］無し時
                if (contents.IsNone)
                    return string.Empty;

                return contents.Id.AsBASE64;
            }
        }

        /// <summary>
        ///     ［切抜きカーソルが指すタイル］のＩｄ。フォネティックコード表現
        ///     
        ///     <see cref="CroppedCursorPointedTileIdOrEmpty"/>
        /// </summary>
        public string CroppedCursorPointedTileIdAsPhoneticCode
        {
            get
            {
                var contents = this.Inner.CroppedCursorPointedTileRecordVisually;

                // ［切抜きカーソル］無し時
                if (contents.IsNone)
                    return string.Empty;

                return contents.Id.AsPhoneticCode;
            }
        }

        /// <summary>
        ///     ［切抜きカーソルが指すタイル］のタイトルの活性性
        ///     
        ///     <list type="bullet">
        ///         <item>［切抜きカーソルが指すタイル］がある</item>
        ///         <item>［切抜きカーソルが指すタイル］のＩｄが空欄でない</item>
        ///         <item>［切抜きカーソルが指すタイル］は論理削除されていない</item>
        ///     </list>
        /// </summary>
        public bool IsEnabledCroppedCursorPointedTileTitleAsStr => !this.Inner.TargetTileRecordVisually.IsNone && !this.Inner.CroppedCursorPointedTileIdOrEmpty.IsEmpty && !this.Inner.TargetTileRecordVisually.LogicalDelete.AsBool;

        /// <summary>
        ///     ［切抜きカーソルが指すタイル］のタイトル
        /// </summary>
        public string CroppedCursorPointedTileTitleAsStr
        {
            get => this.Inner.CroppedCursorPointedTileRecordVisually.Title.AsStr;
            set
            {
                if (this.Inner.CroppedCursorPointedTileRecordVisually.Title.AsStr == value)
                    return;

                // 差分更新
                this.Inner.UpdateCroppedCursorPointedTileByDifference(
                    tileTitle: TileTitle.FromString(value));
            }
        }

        /// <summary>
        ///     ［切抜きカーソルが指すタイル］の論理削除
        /// </summary>
        public bool CroppedCursorPointedTileLogicalDeleteAsBool
        {
            get => this.Inner.CroppedCursorPointedTileRecordVisually.LogicalDelete.AsBool;
            set
            {
                if (this.Inner.CroppedCursorPointedTileRecordVisually.LogicalDelete.AsBool == value)
                    return;

                // 差分更新
                this.Inner.UpdateCroppedCursorPointedTileByDifference(
                    logicalDelete: LogicalDelete.FromBool(value));
            }
        }
        #endregion

        #region 変更通知プロパティ（［追加／上書き］ボタン　関連）
        /// <summary>
        ///     ［追加／上書き］ボタンのラベル
        /// </summary>
        public string AddsButtonText
        {
            get => this.addsButtonText;
            set
            {
                if (this.addsButtonText != value)
                {
                    this.addsButtonText = value;
                    OnPropertyChanged(nameof(AddsButtonText));
                }
            }
        }

        /// <summary>
        ///     ［追加／上書き］ボタンのツールチップ・ヒント
        /// </summary>
        public string AddsButtonHint
        {
            get
            {
                var contents = this.Inner.CroppedCursorPointedTileRecordVisually;

                // ［切抜きカーソル］無し時
                if (contents.IsNone)
                    return string.Empty;

                // 未選択時
                if (contents.Id == Models.TileIdOrEmpty.Empty)
                    // "これがタイル１つ分だと登録します"
                    return (string)LocalizationResourceManager.Instance["RegisterThatThisIsForOneTile"];

                // "残っているタイルの記憶から復元します"
                return (string)LocalizationResourceManager.Instance["RestoreFromMemoryOfRemainingTiles"];
            }
        }

        /// <summary>
        ///     <pre>
        ///         ［追加／復元］ボタンの活性性
        ///         
        ///         ※１　以下の条件を満たさないと、いずれにしても不活性
        ///     </pre>
        ///     <list type="bullet">
        ///         <item>［切抜きカーソルが指すタイル］が有る</item>
        ///     </list>
        ///     ※２　［追加］ボタンは、以下の条件で活性
        ///     <list type="bullet">
        ///         <item>Ｉｄが未設定時、かつ、論理削除フラグがＯｆｆ</item>
        ///     </list>
        ///     ※３　［復元］ボタンは、以下の条件で活性
        ///     <list type="bullet">
        ///         <item>Ｉｄが設定時、かつ、論理削除フラグがＯｎ</item>
        ///     </list>
        /// </summary>
        public bool IsEnabledAddsButton
        {
            get
            {
                // ※１
                var isEnabled = !this.Inner.TargetTileRecordVisually.IsNone && (
                // ※２
                (this.Inner.TargetTileRecordVisually.Id == TileIdOrEmpty.Empty && !this.Inner.TargetTileRecordVisually.LogicalDelete.AsBool)
                ||
                // ※３
                (this.Inner.TargetTileRecordVisually.Id != TileIdOrEmpty.Empty && this.Inner.TargetTileRecordVisually.LogicalDelete.AsBool));

                Trace.WriteLine($"[TileCropPageViewModel.cs IsEnabledAddsButton] this.CroppedCursorPointedTileRecordVisually.Dump(): {this.Inner.TargetTileRecordVisually.Dump()}");

                return isEnabled;
            }
        }
        #endregion

        #region 変更通知プロパティ（［削除］ボタン　関連）
        /// <summary>
        ///     ［削除］ボタンの活性性
        /// </summary>
        public bool IsEnabledDeletesButton
        {
            get => this.isEnabledDeletesButton;
            set
            {
                if (this.isEnabledDeletesButton == value)
                    return;

                this.isEnabledDeletesButton = value;
                OnPropertyChanged(nameof(IsEnabledDeletesButton));
            }
        }
        #endregion

        // - パブリック・プロパティ

        #region プロパティ（［タイルセット・データテーブル］　関連）
        /// <summary>
        ///     ［タイルセット・データテーブル］ファイルの場所
        ///     <list type="bullet">
        ///         <item>ページの引数として使用</item>
        ///     </list>
        /// </summary>
        public TheFileEntryLocations.UnityAssets.DataCsvTilesetCsv TilesetDatatableFileLocation
        {
            get => _tilesetSettingsFile;
            set
            {
                if (_tilesetSettingsFile == value)
                    return;

                _tilesetSettingsFile = value;
            }
        }
        #endregion

        #region プロパティ（［タイルセット元画像］　関連）
        /// <summary>
        ///     ［タイルセット元画像］ファイルへのパス
        ///     <list type="bullet">
        ///         <item>ページの引数として使用</item>
        ///     </list>
        /// </summary>
        public TheFileEntryLocations.UnityAssets.Images.TilesetPng TilesetImageFile
        {
            get => tilesetImageFile;
            set
            {
                if (tilesetImageFile == value)
                    return;

                tilesetImageFile = value;
            }
        }

        /// <summary>
        ///     ［タイルセット元画像］ファイルへのパス（文字列形式）
        ///     <list type="bullet">
        ///         <item>コード・ビハインドで使用</item>
        ///     </list>
        /// </summary>
        public string TilesetImageFilePathAsStr
        {
            get => tilesetImageFile.Path.AsStr;
            set
            {
                if (tilesetImageFile.Path.AsStr == value)
                    return;

                tilesetImageFile = new TheFileEntryLocations.UnityAssets.Images.TilesetPng(
                    pathSource: FileEntryPathSource.FromString(value),
                    convert: (pathSource) => FileEntryPath.From(pathSource,
                                                                replaceSeparators: true));
            }
        }

        /// <summary>
        ///     ［タイルセット元画像］
        ///     <list type="bullet">
        ///         <item>コード・ビハインドで使用</item>
        ///     </list>
        /// </summary>
        public SKBitmap TilesetSourceBitmap => this.tilesetSourceBitmap;

        /// <summary>
        ///     ［タイルセット元画像］の設定
        ///     
        ///     <list type="bullet">
        ///         <item>コード・ビハインドで使用</item>
        ///     </list>
        /// </summary>
        /// <param name="bitmap"></param>
        public void SetTilesetSourceBitmap(SKBitmap bitmap)
        {
            if (this.tilesetSourceBitmap == bitmap)
                return;

            this.tilesetSourceBitmap = bitmap;

            // タイルセット画像のサイズ設定（画像の再作成）
            this.Inner.TilesetSourceImageSize = Models.FileEntries.PNGHelper.GetImageSize(this.TilesetImageFile);
            OnPropertyChanged(nameof(TilesetSourceImageWidthAsInt));
            OnPropertyChanged(nameof(TilesetSourceImageHeightAsInt));

            // 作業画像の再作成
            this.Inner.RemakeWorkingTilesetImage();

            // グリッド・キャンバス画像の再作成
            this.Inner.RemakeGridCanvasImage();
        }
        #endregion

        #region プロパティ（［タイルセット作業画像］　関連）
        /// <summary>
        ///     ［タイルセット作業画像］ビットマップ形式
        ///     
        ///     <list type="bullet">
        ///         <item>コード・ビハインドで使用</item>
        ///     </list>
        /// </summary>
        public SKBitmap TilesetWorkingBitmap { get; set; } = new SKBitmap();
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

                this.ZoomAsFloat = value.AsFloat;
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

        // - インターナル・プロパティ

        #region プロパティ（内部モデル）
        /// <summary>
        ///     内部モデル
        /// </summary>
        internal TileCropPageViewInnerModel Inner { get; }
        #endregion

        // - インターナル変更通知メソッド

        #region 変更通知メソッド（［文化情報］）
        /// <summary>
        ///     ［文化情報］
        /// </summary>
        internal void InvalidateCultureInfo()
        {
            OnPropertyChanged(nameof(SelectedCultureInfo));
        }
        #endregion

        #region 変更通知メソッド（［タイルセット設定］　関連）
        /// <summary>
        ///     ［タイルセット設定］ビューモデルに変更あり
        /// </summary>
        internal void InvalidateTilesetSettingsVM()
        {
            OnPropertyChanged(nameof(TilesetSettingsVM));
        }
        #endregion

        #region 変更通知メソッド（［タイルセット作業画像］）
        /// <summary>
        ///     ［タイルセット作業画像］
        /// </summary>
        internal void InvalidateTilesetWorkingImage()
        {
            OnPropertyChanged(nameof(TilesetWorkingImageWidthAsInt));
            OnPropertyChanged(nameof(TilesetWorkingImageHeightAsInt));
        }
        #endregion

        #region 変更通知メソッド（［作業グリッド］）
        /// <summary>
        ///     ［作業グリッド］
        /// </summary>
        internal void InvalidateWorkingGrid()
        {
            OnPropertyChanged(nameof(WorkingGridTileWidthAsFloat));
            OnPropertyChanged(nameof(WorkingGridTileHeightAsFloat));
            OnPropertyChanged(nameof(WorkingGridUnit));
        }
        #endregion

        #region 変更通知メソッド（［切抜きカーソルが指すタイル］　関連）
        /// <summary>
        ///     ［切抜きカーソルが指すタイル］
        /// </summary>
        internal void InvalidateTarget()
        {
            OnPropertyChanged(nameof(CroppedCursorPointedTileIdAsBASE64));
            OnPropertyChanged(nameof(CroppedCursorPointedTileIdAsPhoneticCode));
            OnPropertyChanged(nameof(CroppedCursorPointedTileTitleAsStr));
            OnPropertyChanged(nameof(CroppedCursorPointedTileLogicalDeleteAsBool));
        }

        /// <summary>
        ///     ［選択タイル］Ｉｄの再描画
        /// </summary>
        internal void InvalidateTileIdChange()
        {
            OnPropertyChanged(nameof(AddsButtonHint));
            OnPropertyChanged(nameof(AddsButtonText));
            OnPropertyChanged(nameof(IsEnabledCroppedCursorPointedTileTitleAsStr));

            OnPropertyChanged(nameof(CroppedCursorPointedTileIdAsBASE64));
            OnPropertyChanged(nameof(CroppedCursorPointedTileIdAsPhoneticCode));
        }

        internal void InvalidateWorkingTargetTile()
        {
            OnPropertyChanged(nameof(CroppedCursorPointedTileWorkingWidthAsFloat));
            OnPropertyChanged(nameof(CroppedCursorPointedTileWorkingSizeWithTrick));
        }
        #endregion

        #region 変更通知メソッド（［タイル・タイトル］）
        /// <summary>
        ///     ［タイル・タイトル］
        /// </summary>
        internal void InvalidateTileTitle()
        {
            OnPropertyChanged(nameof(CroppedCursorPointedTileTitleAsStr));
            OnPropertyChanged(nameof(IsEnabledCroppedCursorPointedTileTitleAsStr));
        }
        #endregion

        #region 変更通知メソッド（［追加／復元］ボタン）
        /// <summary>
        ///     ［追加／復元］ボタン
        /// </summary>
        internal void InvalidateAddsButton()
        {
            OnPropertyChanged(nameof(AddsButtonHint));
            OnPropertyChanged(nameof(AddsButtonText));
            OnPropertyChanged(nameof(IsEnabledAddsButton));
        }
        #endregion

        #region 変更通知メソッド（［履歴］）
        /// <summary>
        ///     ［履歴］
        /// </summary>
        internal void InvalidateForHistory()
        {
            OnPropertyChanged(nameof(CanUndo));
            OnPropertyChanged(nameof(CanRedo));
            OnPropertyChanged(nameof(ZoomAsFloat));

            OnPropertyChanged(nameof(WorkingGridPhaseLeftAsFloat));
            OnPropertyChanged(nameof(WorkingGridPhaseTopAsFloat));
            OnPropertyChanged(nameof(WorkingGridPhase));

            OnPropertyChanged(nameof(WorkingGridTileWidthAsFloat));
            OnPropertyChanged(nameof(WorkingGridTileHeightAsFloat));
            OnPropertyChanged(nameof(WorkingGridUnit));

            // 切抜きカーソル。ズーム後
            OnPropertyChanged(nameof(CroppedCursorWorkingPointAsMargin));
            OnPropertyChanged(nameof(CanvasOfCroppedCursorWorkingWidthAsFloat));
            OnPropertyChanged(nameof(CanvasOfCroppedCursorWorkingHeightAsFloat));
            OnPropertyChanged(nameof(CroppedCursorPointedTileWorkingSizeWithTrick));
            OnPropertyChanged(nameof(CroppedCursorPointedTileWorkingLeftAsPresentableText));   // TODO これは要るか？
            OnPropertyChanged(nameof(CroppedCursorPointedTileWorkingTopAsPresentableText));   // TODO これは要るか？
            OnPropertyChanged(nameof(CroppedCursorPointedTileWorkingWidthAsPresentableText));   // TODO これは要るか？
            OnPropertyChanged(nameof(CroppedCursorPointedTileWorkingHeightAsPresentableText));   // TODO これは要るか？
        }
        #endregion

        // - インターナル・インベントハンドラ

        #region イベントハンドラ（別ページから、このページに訪れたときに呼び出される）
        /// <summary>
        ///     別ページから、このページに訪れたときに呼び出される
        /// </summary>
        internal void OnNavigatedTo(SKCanvasView skiaTilesetCanvas1)
        {
            Trace.WriteLine($"[TileCropPage.xaml.cs ContentPage_Loaded] ページ来訪時");

            this.Inner.ReactOnVisited();

            //
            // タイル設定ファイルの読込
            // ========================
            //
            if (TilesetDatatableVisually.LoadCSV(
                tilesetDatatableFileLocation: this.TilesetDatatableFileLocation,
                zoom: this.Zoom,
                tilesetDatatableVisually: out TilesetDatatableVisually tilesetDatatableVisually))
            {
                this.TilesetSettingsVM = tilesetDatatableVisually;

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
            var tilesetImageFilePathAsStr = this.TilesetImageFilePathAsStr;

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
                            this.SetTilesetSourceBitmap(SkiaSharp.SKBitmap.Decode(memStream));

                            // 複製
                            this.TilesetWorkingBitmap = SkiaSharp.SKBitmap.FromImage(SkiaSharp.SKImage.FromBitmap(this.TilesetSourceBitmap));

                            // 画像処理（明度を下げる）
                            FeatSkia.ReduceBrightness.DoItInPlace(this.TilesetWorkingBitmap);
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
            if (this.Inner.CroppedCursorPointedTileIdOrEmpty == Models.TileIdOrEmpty.Empty)
            {
                // Ｉｄが空欄
                // ［追加］（新規作成）だ

                // 登録タイル追加
                this.Inner.AddRegisteredTile();
            }
            else
            {
                // 上書きボタンだが、［上書き］処理をする
                this.Inner.OverwriteRegisteredTile();
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
            this.IsMouseDragging = !this.IsMouseDragging;

            if (this.IsMouseDragging)
            {
                //
                // 疑似マウス・ダウン
                // ==================
                //
                Trace.WriteLine("[TileCropPage.xml.cs TileImage_OnTapped] 疑似マウス・ダウン");

                // ポイントしている位置
                this.Inner.PointingDeviceCurrentPoint = this.Inner.PointingDeviceStartPoint = new Models.Geometric.PointFloat(
                    new Models.Geometric.XFloat((float)tappedPoint.X),
                    new Models.Geometric.YFloat((float)tappedPoint.Y));
                // Trace.WriteLine($"[TileCropPage TileImage_OnTapped] tapped x:{PointingDeviceStartPoint.X.AsInt} y:{PointingDeviceStartPoint.Y.AsInt}");

                // タイル・フォームの表示更新
                this.Inner.RefreshTileForm();

                this.Inner.TrickRefreshCanvasOfTileCursor(codePlace: "[TileCropPage.xml.cs TileImage_OnTapped 疑似マウスダウン]");
            }
            else
            {
                //
                // 疑似マウス・アップ
                // ==================
                //

                Trace.WriteLine("[TileCropPage.xml.cs TileImage_OnTapped] 疑似マウス・アップ");

                // ポイントしている位置
                this.Inner.PointingDeviceCurrentPoint = new Models.Geometric.PointFloat(
                    new Models.Geometric.XFloat((float)tappedPoint.X),
                    new Models.Geometric.YFloat((float)tappedPoint.Y));
                // Trace.WriteLine($"[TileCropPage PointerGestureRecognizer_PointerExited] exited x:{PointingDeviceCurrentPoint.X.AsInt} y:{PointingDeviceCurrentPoint.Y.AsInt}");

                // タイル・フォームの表示更新
                this.Inner.RefreshTileForm();

                this.Inner.TrickRefreshCanvasOfTileCursor(codePlace: "[TileCropPage.xml.cs TileImage_OnTapped 疑似マウスアップ]");
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
            if (this.IsMouseDragging)
            {
                //
                // 疑似マウス・ドラッグ
                // ====================
                //

                // ポイントしている位置
                this.Inner.PointingDeviceCurrentPoint = new Models.Geometric.PointFloat(
                    new Models.Geometric.XFloat((float)tappedPoint.X),
                    new Models.Geometric.YFloat((float)tappedPoint.Y));
                // Trace.WriteLine($"[TileCropPage PointerGestureRecognizer_PointerMoved] moved x:{PointingDeviceCurrentPoint.X.AsInt} y:{PointingDeviceCurrentPoint.Y.AsInt}");

                // タイル・フォームの表示更新
                this.Inner.RefreshTileForm();

                this.Inner.TrickRefreshCanvasOfTileCursor(codePlace: "[TileCropPage.xml.cs PointerGestureRecognizer_PointerMoved 疑似マウスドラッグ]");
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

        #region 変更通知フィールド（［タイルセット設定］　関連）
        /// <summary>
        ///     ［タイルセット設定］ビューモデル
        /// </summary>
        TilesetDatatableVisually _tilesetSettingsVM = new();

        /// <summary>
        ///     ［タイルセット設定］のCSVファイル
        /// </summary>
        TheFileEntryLocations.UnityAssets.DataCsvTilesetCsv _tilesetSettingsFile = TheFileEntryLocations.UnityAssets.DataCsvTilesetCsv.Empty;
        #endregion

        #region 変更通知フィールド（［タイルセット元画像］　関連）
        /// <summary>
        ///     ［タイルセット元画像］
        /// </summary>
        SKBitmap tilesetSourceBitmap = new();

        /// <summary>
        ///     ［タイルセット元画像］ファイルへのパス
        /// </summary>
        TheFileEntryLocations.UnityAssets.Images.TilesetPng tilesetImageFile = TheFileEntryLocations.UnityAssets.Images.TilesetPng.Empty;
        #endregion

        #region インターナル変更通知フィールド（［タイルセット作業画像］　関連）
        /// <summary>
        ///     ［タイルセット作業画像］サイズ
        /// </summary>
        internal Models.Geometric.SizeInt workingImageSize = Models.Geometric.SizeInt.Empty;
        #endregion

        #region 変更通知フィールド（［ズーム］　関連）
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

        #region 変更通知フィールド（［元画像グリッド］　関連）
        /// <summary>
        ///     ［元画像グリッド］のキャンバス画像サイズ
        /// </summary>
        Models.Geometric.SizeInt gridCanvasImageSize = Models.Geometric.SizeInt.Empty;

        /// <summary>
        ///     ［元画像グリッド］の位相の左上表示位置
        /// </summary>
        Models.Geometric.PointInt gridPhaseSourceLocation = Models.Geometric.PointInt.Empty;

        /// <summary>
        ///     ［元画像グリッド］の線の太さの半分
        /// </summary>
        ThicknessOfLine halfThicknessOfGridLine = new(1);
        #endregion

        /// <summary>
        ///     ［元画像グリッド］の単位
        /// </summary>
        internal Models.Geometric.SizeInt sourceGridUnit = new(new Models.Geometric.WidthInt(32), new Models.Geometric.HeightInt(32));

        #region 変更通知フィールド（［作業グリッド］　関連）
        /// <summary>
        ///     ［作業グリッド］の単位
        /// </summary>
        Models.Geometric.SizeFloat workingGridUnit = new(new Models.Geometric.WidthFloat(32.0f), new Models.Geometric.HeightFloat(32.0f));

        /// <summary>
        ///     ［作業グリッド］の位相の左上表示位置
        /// </summary>
        Models.Geometric.PointFloat workingGridPhase = Models.Geometric.PointFloat.Empty;
        #endregion

        #region 変更通知フィールド（［切抜きカーソル］　関連）
        /// <summary>
        ///     ［切抜きカーソル］の線の半分の太さ
        /// </summary>
        ThicknessOfLine halfThicknessOfTileCursorLine;
        #endregion

        #region 変更通知フィールド（［追加／上書き］ボタン　関連）
        /// <summary>
        ///     ［追加／上書き］ボタンのラベル
        /// </summary>
        string addsButtonText = string.Empty;
        #endregion

        #region 変更通知フィールド（［削除］ボタン　関連）
        /// <summary>
        ///     ［削除］ボタンの活性性
        /// </summary>
        bool isEnabledDeletesButton;
        #endregion

        // - プライベート・フィールド

        #region フィールド（［切抜きカーソル］　関連）
        /// <summary>
        ///     ［切抜きカーソル］ズーム済みのサイズ
        ///         
        ///     <list type="bullet">
        ///         <item>カーソルの線の幅を含まない</item>
        ///         <item>TODO ★ 現在、範囲選択は、この作業用のサイズを使っているが、ソースの方のサイズを変更するようにできないか？ ワーキングは変数にしないようにしたい</item>
        ///         <item>仕様変更するときは、TRICK CODE に注意</item>
        ///     </list>
        /// </summary>
        Models.Geometric.WidthFloat trickWidth = Models.Geometric.WidthFloat.Zero;
        Models.Geometric.WidthFloat croppedCursorPointedTileWorkingWidthWithoutTrick = Models.Geometric.WidthFloat.Zero;
        Models.Geometric.HeightFloat croppedCursorPointedTileWorkingHeight = Models.Geometric.HeightFloat.Zero;
        #endregion

        // - プライベート・メソッド

        #region メソッド（［元画像グリッド］　関連）
        /// <summary>
        ///     <pre>
        ///         ［元画像グリッド］のキャンバスの再描画
        /// 
        ///         TRICK:  GraphicsView を再描画させたいが、ビューモデルから要求する方法が分からない。
        ///                 そこで、内部的なグリッド画像の横幅が偶数のときは +1、奇数のときは -1 して
        ///                 振動させることで、再描画を呼び起こすことにする
        ///     </pre>
        /// </summary>
        void InvalidateGraphicsViewOfGrid()
        {
            if (this.GridCanvasImageWidthAsInt % 2 == 1)
            {
                this.GridCanvasImageWidthAsInt--;
            }
            else
            {
                this.GridCanvasImageWidthAsInt++;
            }
        }
        #endregion
    }
}
