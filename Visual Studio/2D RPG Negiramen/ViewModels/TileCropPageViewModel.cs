namespace _2D_RPG_Negiramen.ViewModels
{
    using _2D_RPG_Negiramen.Models;
    using _2D_RPG_Negiramen.Models.Geometric;
    using _2D_RPG_Negiramen.Models.History;
    using _2D_RPG_Negiramen.Models.Visually;
    using CommunityToolkit.Mvvm.ComponentModel;
    using SkiaSharp;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Globalization;
    using TheFileEntryLocations = _2D_RPG_Negiramen.Models.FileEntries.Locations;
    using TheGeometric = _2D_RPG_Negiramen.Models.Geometric;

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
                        owner: this,
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
                    NotifyTileIdChange();
                }
            }
        }
        #endregion

        #region 変更通知プロパティ（［タイルセット元画像］　関連）
        /// <summary>
        ///     ［タイルセット元画像］の横幅。読取専用
        /// </summary>
        public int TilesetSourceImageWidthAsInt => tilesetSourceImageSize.Width.AsInt;

        /// <summary>
        ///     ［タイルセット元画像］の縦幅。読取専用
        /// </summary>
        public int TilesetSourceImageHeightAsInt => tilesetSourceImageSize.Height.AsInt;
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
                TrickRefreshCanvasOfTileCursor("[TileCropPageViewModel.cs ZoomAsFloat]");

                if (this.zoom.AsFloat != value)
                {
                    if (this.ZoomMinAsFloat <= value && value <= this.ZoomMaxAsFloat)
                    {
                        Zoom oldValue = this.zoom;
                        Zoom newValue = new Models.Geometric.Zoom(value);

                        this.zoom = newValue;

                        // 再帰的にズーム再変更、かつ変更後の影響を処理
                        App.History.Do(new ZoomProcessing(this, oldValue, newValue));
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
                    RefreshWorkingGridTileWidth();

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
                    RefreshWorkingGridTileHeight();

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
                var contents = this.croppedCursorPointedTileRecordVisually;

                if (contents.IsNone)
                {
                    // ［切抜きカーソル］の指すタイル無し時
                    return Models.Geometric.RectangleInt.Empty;
                }

                return contents.SourceRectangle;
            }
            set
            {
                var contents = this.croppedCursorPointedTileRecordVisually;

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
                this.CroppedCursorPointedTileWorkingLeftAsFloat = this.ZoomAsFloat * this.CroppedCursorPointedTileSourceLeftAsInt;
                this.CroppedCursorPointedTileWorkingTopAsFloat = this.ZoomAsFloat * this.CroppedCursorPointedTileSourceTopAsInt;
                this.CroppedCursorPointedTileWorkingSize = new Models.Geometric.SizeFloat(
                    width: new Models.Geometric.WidthFloat(this.ZoomAsFloat * value.Size.Width.AsInt),
                    height: new Models.Geometric.HeightFloat(this.ZoomAsFloat * value.Size.Height.AsInt));
            }
        }

        /// <summary>
        ///     ［切抜きカーソルが指すタイル］の元画像ベースの位置ｘ
        /// </summary>
        public int CroppedCursorPointedTileSourceLeftAsInt
        {
            get
            {
                var contents = this.croppedCursorPointedTileRecordVisually;

                if (contents.IsNone)
                {
                    // ［切抜きカーソル］無し時
                    return 0;
                }

                return contents.SourceRectangle.Location.X.AsInt;
            }
            set
            {
                var contents = this.croppedCursorPointedTileRecordVisually;

                if (contents.IsNone)
                {
                    // ［切抜きカーソル］無し時

                    // 元画像ベース
                    var rect1 = new Models.Geometric.RectangleInt(
                                location: new Models.Geometric.PointInt(new Models.Geometric.XInt(value), Models.Geometric.YInt.Empty),
                                size: Models.Geometric.SizeInt.Empty);
                    this.croppedCursorPointedTileRecordVisually = TileRecordVisually.FromModel(
                        tileRecord: new Models.TileRecord(
                            id: Models.TileIdOrEmpty.Empty,
                            rect: rect1,
                            title: Models.TileTitle.Empty,
                            logicalDelete: Models.LogicalDelete.False),
                        zoom: this.Zoom);
                }
                else
                {
                    // 値に変化がない
                    if (contents.SourceRectangle.Location.X.AsInt == value)
                        return;

                    // 元画像ベース
                    var rect1 = new Models.Geometric.RectangleInt(
                                location: new Models.Geometric.PointInt(new Models.Geometric.XInt(value), contents.SourceRectangle.Location.Y),
                                size: contents.SourceRectangle.Size);
                    this.croppedCursorPointedTileRecordVisually = TileRecordVisually.FromModel(
                        tileRecord: new Models.TileRecord(
                            id: contents.Id,
                            rect: rect1,
                            title: contents.Title,
                            logicalDelete: contents.LogicalDelete),
                        zoom: this.Zoom);
                }

                // 切抜きカーソル。ズーム済み
                this.CroppedCursorPointedTileWorkingLeftAsFloat = this.ZoomAsFloat * this.CroppedCursorPointedTileSourceLeftAsInt;
                this.CroppedCursorPointedTileWorkingTopAsFloat = this.ZoomAsFloat * this.CroppedCursorPointedTileSourceTopAsInt;
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
                var contents = this.croppedCursorPointedTileRecordVisually;

                // ［切抜きカーソル］無し時
                if (contents.IsNone)
                    return 0;

                return contents.SourceRectangle.Location.Y.AsInt;
            }
            set
            {
                var contents = this.croppedCursorPointedTileRecordVisually;

                if (contents.IsNone)
                {
                    // ［切抜きカーソル］無し時

                    // 元画像ベース
                    var rect1 = new Models.Geometric.RectangleInt(
                            location: new Models.Geometric.PointInt(Models.Geometric.XInt.Empty, new Models.Geometric.YInt(value)),
                            size: Models.Geometric.SizeInt.Empty);

                    croppedCursorPointedTileRecordVisually = TileRecordVisually.FromModel(
                        tileRecord: new Models.TileRecord(
                            id: Models.TileIdOrEmpty.Empty,
                            rect: rect1,
                            title: Models.TileTitle.Empty,
                            logicalDelete: Models.LogicalDelete.False),
                        zoom: this.Zoom);
                }
                else
                {
                    // 値に変化がない
                    if (contents.SourceRectangle.Location.Y.AsInt == value)
                        return;

                    // 元画像ベース
                    var rect1 = new Models.Geometric.RectangleInt(
                            location: new Models.Geometric.PointInt(contents.SourceRectangle.Location.X, new Models.Geometric.YInt(value)),
                            size: contents.SourceRectangle.Size);
                    croppedCursorPointedTileRecordVisually = TileRecordVisually.FromModel(
                        tileRecord: new Models.TileRecord(
                            id: contents.Id,
                            rect: rect1,
                            title: contents.Title,
                            logicalDelete: contents.LogicalDelete),
                        zoom: this.Zoom);
                }

                // 切抜きカーソル。ズーム済み
                this.CroppedCursorPointedTileWorkingLeftAsFloat = this.ZoomAsFloat * this.CroppedCursorPointedTileSourceLeftAsInt;
                this.CroppedCursorPointedTileWorkingTopAsFloat = this.ZoomAsFloat * this.CroppedCursorPointedTileSourceTopAsInt;
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
                var contents = this.croppedCursorPointedTileRecordVisually;

                // ［切抜きカーソル］無し時
                if (contents.IsNone)
                    return Models.Geometric.SizeInt.Empty;

                return contents.SourceRectangle.Size;
            }
            set
            {
                var contents = this.croppedCursorPointedTileRecordVisually;

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
                var contents = this.croppedCursorPointedTileRecordVisually;

                // ［切抜きカーソル］無し時
                if (contents.IsNone)
                    return 0;

                return contents.SourceRectangle.Size.Width.AsInt;
            }
            set
            {
                var contents = this.croppedCursorPointedTileRecordVisually;

                if (contents.IsNone)
                {
                    // ［切抜きカーソル］無し時
                    var rect1 = new Models.Geometric.RectangleInt(Models.Geometric.PointInt.Empty, new Models.Geometric.SizeInt(new Models.Geometric.WidthInt(value), Models.Geometric.HeightInt.Empty));
                    croppedCursorPointedTileRecordVisually = TileRecordVisually.FromModel(
                        tileRecord: new Models.TileRecord(
                            id: Models.TileIdOrEmpty.Empty,
                            rect: rect1,
                            title: Models.TileTitle.Empty,
                            logicalDelete: Models.LogicalDelete.False),
                        zoom: this.Zoom);
                }
                else
                {
                    // 値に変化がない
                    if (contents.SourceRectangle.Size.Width.AsInt == value)
                        return;

                    var rect1 = new Models.Geometric.RectangleInt(contents.SourceRectangle.Location, new Models.Geometric.SizeInt(new Models.Geometric.WidthInt(value), contents.SourceRectangle.Size.Height));
                    croppedCursorPointedTileRecordVisually = TileRecordVisually.FromModel(
                        tileRecord: new Models.TileRecord(
                            id: contents.Id,
                            rect: rect1,
                            title: contents.Title,
                            logicalDelete: contents.LogicalDelete),
                        zoom: this.Zoom);
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
                var contents = this.croppedCursorPointedTileRecordVisually;

                // ［切抜きカーソル］無し時
                if (contents.IsNone)
                    return 0;

                return contents.SourceRectangle.Size.Height.AsInt;
            }
            set
            {
                var contents = this.croppedCursorPointedTileRecordVisually;

                if (contents.IsNone)
                {
                    // ［切抜きカーソル］無し時
                    var rect1 = new Models.Geometric.RectangleInt(Models.Geometric.PointInt.Empty, new Models.Geometric.SizeInt(Models.Geometric.WidthInt.Empty, new Models.Geometric.HeightInt(value)));
                    croppedCursorPointedTileRecordVisually = TileRecordVisually.FromModel(
                        tileRecord: new Models.TileRecord(
                            id: TileIdOrEmpty.Empty,
                            rect: rect1,
                            title: Models.TileTitle.Empty,
                            logicalDelete: Models.LogicalDelete.False),
                        zoom: this.Zoom);
                }
                else
                {
                    // 値に変化がない
                    if (contents.SourceRectangle.Size.Height.AsInt == value)
                        return;

                    var rect1 = new Models.Geometric.RectangleInt(contents.SourceRectangle.Location, new Models.Geometric.SizeInt(contents.SourceRectangle.Size.Width, new Models.Geometric.HeightInt(value)));
                    croppedCursorPointedTileRecordVisually = TileRecordVisually.FromModel(
                        tileRecord: new Models.TileRecord(
                            id: contents.Id,
                            rect: rect1,
                            title: contents.Title,
                            logicalDelete: contents.LogicalDelete),
                        zoom: this.Zoom);
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
        public Thickness CroppedCursorWorkingPointAsMargin
        {
            get => new(left: this.CroppedCursorPointedTileWorkingLeftAsFloat,
                       top: this.CroppedCursorPointedTileWorkingTopAsFloat,
                       right: 0,
                       bottom: 0);
        }

        /// <summary>
        ///     ［切抜きカーソルが指すタイル］のズーム済みの横幅
        ///         
        ///     <list type="bullet">
        ///         <item>カーソルの線の幅を含む</item>
        ///         <item>切抜きカーソルは、対象範囲に外接する</item>
        ///     </list>
        /// </summary>
        public float CanvasOfCroppedCursorWorkingWidthAsFloat
        {
            get => this.croppedCursorPointedTileWorkingRect.Size.Width.AsFloat + (4 * this.HalfThicknessOfTileCursorLine.AsInt);
        }

        /// <summary>
        ///     ［切抜きカーソルが指すタイル］のズーム済みの縦幅
        ///         
        ///     <list type="bullet">
        ///         <item>カーソルの線の幅を含む</item>
        ///         <item>切抜きカーソルは、対象範囲に外接する</item>
        ///     </list>
        /// </summary>
        public float CanvasOfCroppedCursorWorkingHeightAsFloat
        {
            get => this.croppedCursorPointedTileWorkingRect.Size.Height.AsFloat + (4 * this.HalfThicknessOfTileCursorLine.AsInt);
        }

        /// <summary>
        ///     ［切抜きカーソルが指すタイル］のズーム済みの位置ｘ
        ///         
        ///     <list type="bullet">
        ///         <item>カーソルの線の幅を含まない</item>
        ///     </list>
        /// </summary>
        public float CroppedCursorPointedTileWorkingLeftAsFloat
        {
            get => this.croppedCursorPointedTileWorkingRect.Location.X.AsFloat;
            set
            {
                if (this.croppedCursorPointedTileWorkingRect.Location.X.AsFloat != value)
                {
                    this.croppedCursorPointedTileWorkingRect = new RectangleFloat(
                        location: new Models.Geometric.PointFloat(
                            x: new Models.Geometric.XFloat(value),
                            y: this.croppedCursorPointedTileWorkingRect.Location.Y),
                        size: this.croppedCursorPointedTileWorkingRect.Size);

                    OnPropertyChanged(nameof(CroppedCursorPointedTileWorkingLeftAsFloat));
                    OnPropertyChanged(nameof(CroppedCursorPointedTileWorkingLocation));
                    OnPropertyChanged(nameof(CroppedCursorWorkingPointAsMargin));

                    OnPropertyChanged(nameof(CroppedCursorPointedTileWorkingLeftAsPresentableText));
                }
            }
        }

        /// <summary>
        ///     ［切抜きカーソルが指すタイル］のズーム済みの位置ｙ
        ///         
        ///     <list type="bullet">
        ///         <item>カーソルの線の幅を含まない</item>
        ///     </list>
        /// </summary>
        public float CroppedCursorPointedTileWorkingTopAsFloat
        {
            get => this.croppedCursorPointedTileWorkingRect.Location.Y.AsFloat;
            set
            {
                if (this.croppedCursorPointedTileWorkingRect.Location.Y.AsFloat != value)
                {
                    this.croppedCursorPointedTileWorkingRect = new RectangleFloat(
                        location: new Models.Geometric.PointFloat(
                            x: this.croppedCursorPointedTileWorkingRect.Location.X,
                            y: new Models.Geometric.YFloat(value)),
                        size: this.croppedCursorPointedTileWorkingRect.Size);

                    OnPropertyChanged(nameof(CroppedCursorPointedTileWorkingTopAsFloat));
                    OnPropertyChanged(nameof(CroppedCursorPointedTileWorkingLocation));
                    OnPropertyChanged(nameof(CroppedCursorWorkingPointAsMargin));

                    OnPropertyChanged(nameof(CroppedCursorPointedTileWorkingTopAsPresentableText));
                }
            }
        }

        /// <summary>
        ///     ［切抜きカーソルが指すタイル］のズーム済みのサイズ
        ///         
        ///     <list type="bullet">
        ///         <item>カーソルの線の幅を含まない</item>
        ///     </list>
        /// </summary>
        public Models.Geometric.SizeFloat CroppedCursorPointedTileWorkingSize
        {
            get => this.croppedCursorPointedTileWorkingRect.Size;
            set
            {
                if (this.croppedCursorPointedTileWorkingRect.Size == value)
                    return;

                // 末端へ下りる
                this.CroppedCursorPointedTileWorkingWidthAsFloat = value.Width.AsFloat;
                this.CroppedCursorPointedTileWorkingHeightAsFloat = value.Height.AsFloat;
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
            get => this.croppedCursorPointedTileWorkingRect.Size.Width.AsFloat;
            set
            {
                if (this.croppedCursorPointedTileWorkingRect.Size.Width.AsFloat != value)
                {
                    this.croppedCursorPointedTileWorkingRect = new RectangleFloat(
                        location: this.croppedCursorPointedTileWorkingRect.Location,
                        size: new Models.Geometric.SizeFloat(
                            width: new Models.Geometric.WidthFloat(value),
                            height: croppedCursorPointedTileWorkingRect.Size.Height));
                    // Trace.WriteLine($"[TileCropPageViewModel.cs CroppedCursorPointedTileWorkingWidthAsFloat] this.croppedCursorPointedTileWorkingSize: {this.croppedCursorPointedTileWorkingSize.Dump()}");

                    // キャンバスを再描画
                    // RefreshCanvasOfTileCursor(codePlace: "[TileCropPageViewModel CroppedCursorPointedTileWorkingWidthAsFloat set]");

                    // キャンバスを再描画後に変更通知
                    OnPropertyChanged(nameof(CroppedCursorPointedTileWorkingWidthAsFloat));
                    OnPropertyChanged(nameof(CroppedCursorPointedTileWorkingSize));

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
            get => this.croppedCursorPointedTileWorkingRect.Size.Height.AsFloat;
            set
            {
                if (this.croppedCursorPointedTileWorkingRect.Size.Height.AsFloat != value)
                {
                    this.croppedCursorPointedTileWorkingRect = new RectangleFloat(
                        location: this.croppedCursorPointedTileWorkingRect.Location,
                        size: new Models.Geometric.SizeFloat(
                            width: this.croppedCursorPointedTileWorkingRect.Size.Width,
                            height: new Models.Geometric.HeightFloat(value)));
                    // Trace.WriteLine($"[TileCropPageViewModel.cs CroppedCursorPointedTileWorkingHeightAsFloat] this.croppedCursorPointedTileWorkingSize: {this.croppedCursorPointedTileWorkingSize.Dump()}");

                    // キャンバスを再描画
                    // RefreshCanvasOfTileCursor("[TileCropPageViewModel CroppedCursorPointedTileWorkingHeightAsFloat set]");

                    // キャンバスを再描画後に変更通知
                    OnPropertyChanged(nameof(CroppedCursorPointedTileWorkingHeightAsFloat));
                    OnPropertyChanged(nameof(CroppedCursorPointedTileWorkingSize));

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
        public string CroppedCursorPointedTileWorkingLeftAsPresentableText => this.croppedCursorPointedTileWorkingRect.Location.X.AsFloat.ToString("F1");

        /// <summary>
        ///     ［切抜きカーソルが指すタイル］のズーム済みの位置ｙ
        ///         
        ///     <list type="bullet">
        ///         <item>カーソルの線の幅を含まない</item>
        ///         <item>表示用テキスト</item>
        ///     </list>
        /// </summary>
        public string CroppedCursorPointedTileWorkingTopAsPresentableText => this.croppedCursorPointedTileWorkingRect.Location.Y.AsFloat.ToString("F1");

        /// <summary>
        ///     ［切抜きカーソルが指すタイル］のズーム済みの横幅
        ///         
        ///     <list type="bullet">
        ///         <item>カーソルの線の幅を含まない</item>
        ///         <item>表示用テキスト</item>
        ///     </list>
        /// </summary>
        public string CroppedCursorPointedTileWorkingWidthAsPresentableText => this.croppedCursorPointedTileWorkingRect.Size.Width.AsFloat.ToString("F1");

        /// <summary>
        ///     ［切抜きカーソルが指すタイル］のズーム済みの縦幅
        ///         
        ///     <list type="bullet">
        ///         <item>カーソルの線の幅を含まない</item>
        ///         <item>表示用テキスト</item>
        ///     </list>
        /// </summary>
        public string CroppedCursorPointedTileWorkingHeightAsPresentableText => this.croppedCursorPointedTileWorkingRect.Size.Height.AsFloat.ToString("F1");

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
                var contents = this.croppedCursorPointedTileRecordVisually;

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
                var contents = this.croppedCursorPointedTileRecordVisually;

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
        ///         <item>切抜きカーソルがある</item>
        ///         <item>選択タイルＩｄが空欄でない</item>
        ///     </list>
        /// </summary>
        public bool IsEnabledCroppedCursorPointedTileTitleAsStr => !this.croppedCursorPointedTileRecordVisually.IsNone && !this.CroppedCursorPointedTileIdOrEmpty.IsEmpty;

        /// <summary>
        ///     ［切抜きカーソルが指すタイル］のタイトル
        /// </summary>
        public string CroppedCursorPointedTileTitleAsStr
        {
            get => this.croppedCursorPointedTileRecordVisually.Title.AsStr;
            set
            {
                var contents = this.croppedCursorPointedTileRecordVisually;

                if (contents.IsNone)
                {
                    // ［切抜きカーソル］の指すタイル無し時
                    var rect1 = Models.Geometric.RectangleInt.Empty;
                    croppedCursorPointedTileRecordVisually = TileRecordVisually.FromModel(
                        tileRecord: new Models.TileRecord(
                            id: TileIdOrEmpty.Empty,
                            rect: rect1,
                            title: new Models.TileTitle(value),
                            logicalDelete: Models.LogicalDelete.False),
                       zoom: this.Zoom);
                }
                else
                {
                    // 値に変化がない
                    if (contents.Title.AsStr == value)
                        return;

                    var rect1 = contents.SourceRectangle;
                    croppedCursorPointedTileRecordVisually = TileRecordVisually.FromModel(
                        tileRecord: new Models.TileRecord(
                            id: contents.Id,
                            rect: rect1,
                            title: new Models.TileTitle(value),
                            logicalDelete: contents.LogicalDelete),
                        zoom: this.Zoom);
                }

                OnPropertyChanged(nameof(CroppedCursorPointedTileTitleAsStr));
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
                var contents = this.croppedCursorPointedTileRecordVisually;

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
        ///     ［追加／上書き］ボタンの活性性
        /// </summary>
        public bool IsEnabledAddsButton
        {
            get => this.isEnabledAddsButton;
            set
            {
                if (this.isEnabledAddsButton == value)
                    return;

                this.isEnabledAddsButton = value;
                OnPropertyChanged(nameof(IsEnabledAddsButton));
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
        ///     ［タイルセット・データテーブル］ファイルへのパス（文字列形式）
        /// </summary>
        public string TilesetSettingFilePathAsStr
        {
            get => _tilesetSettingsFile.Path.AsStr;
            set
            {
                if (value == null || String.IsNullOrWhiteSpace(value))
                    throw new ArgumentException($"the {nameof(TilesetSettingFilePathAsStr)} must not be null or whitespace");

                if (_tilesetSettingsFile.Path.AsStr == value)
                    return;

                _tilesetSettingsFile = new TheFileEntryLocations.UnityAssets.DataCsvTilesetCsv(
                    pathSource: FileEntryPathSource.FromString(value),
                    convert: (pathSource) => FileEntryPath.From(pathSource,
                                                                replaceSeparators: true));
            }
        }

        /// <summary>
        ///     ［タイルセット・データテーブル］ファイルの場所
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
        /// </summary>
        public SKBitmap TilesetSourceBitmap => this.tilesetSourceBitmap;

        /// <summary>
        ///     ［タイルセット元画像］の設定
        /// </summary>
        /// <param name="bitmap"></param>
        public void SetTilesetSourceBitmap(SKBitmap bitmap)
        {
            if (this.tilesetSourceBitmap == bitmap)
                return;

            this.tilesetSourceBitmap = bitmap;

            // タイルセット画像のサイズ設定（画像の再作成）
            this.tilesetSourceImageSize = Models.FileEntries.PNGHelper.GetImageSize(this.TilesetImageFile);
            OnPropertyChanged(nameof(TilesetSourceImageWidthAsInt));
            OnPropertyChanged(nameof(TilesetSourceImageHeightAsInt));

            // 作業画像の再作成
            this.RemakeWorkingTilesetImage();

            // グリッド・キャンバス画像の再作成
            this.RemakeGridCanvasImage();
        }

        /// <summary>
        ///     ［タイルセット元画像］のサイズ
        /// </summary>
        public Models.Geometric.SizeInt TilesetSourceImageSize => tilesetSourceImageSize;
        #endregion

        #region プロパティ（［タイルセット作業画像］　関連）
        /// <summary>
        ///     ［タイルセット作業画像］ファイルへのパス（文字列形式）
        /// </summary>
        public string TilesetWorkingImageFilePathAsStr => App.CacheFolder.YourCircleFolder.YourWorkFolder.ImagesFolder.WorkingTilesetPng.Path.AsStr;

        /// <summary>
        ///     ［タイルセット作業画像］ビットマップ形式
        /// </summary>
        public SKBitmap TilesetWorkingBitmap { get; set; } = new SKBitmap();
        #endregion

        #region プロパティ（［ズーム］　関連）
        /// <summary>
        ///     ズーム
        ///     
        ///     <list type="bullet">
        ///         <item>セッターは画像を再生成する重たい処理なので、スパムしないように注意</item>
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

        #region プロパティ（［切抜きカーソル］　関連）
        /// <summary>
        ///     ［切抜きカーソル］が指すタイル
        ///     
        ///     <list type="bullet">
        ///         <item>［切抜きカーソル］が未確定のときも、指しているタイルにアクセスできることに注意</item>
        ///         <item>TODO ★ ［切抜きカーソル］が指すタイルが無いとき、無いということをセットするのを忘れている？</item>
        ///     </list>
        /// </summary>
        public TileRecordVisually CroppedCursorPointedTileRecordVisually
        {
            get => this.croppedCursorPointedTileRecordVisually;
            set
            {
                var contents = this.croppedCursorPointedTileRecordVisually;

                // 値に変化がない
                if (contents == value)
                    return;

                if (value.IsNone)
                {
                    if (contents.IsNone)
                    {
                        // ［切抜きカーソル］の指すタイルが無いなら変更なし
                    }
                    else
                    {
                        // ［切抜きカーソル］の指すタイル有り時

                        // TODO もっと楽にクリアーできないものか？ （変更通知を送っている）
                        this.CroppedCursorPointedTileIdOrEmpty = TileIdOrEmpty.Empty;
                        this.CroppedCursorPointedTileSourceLeftAsInt = 0;
                        this.CroppedCursorPointedTileSourceTopAsInt = 0;
                        this.CroppedCursorPointedTileSourceWidthAsInt = 0;
                        this.CroppedCursorPointedTileSourceHeightAsInt = 0;
                        this.CroppedCursorPointedTileTitleAsStr = string.Empty;

                        // 空にする
                        this.croppedCursorPointedTileRecordVisually = TileRecordVisually.CreateEmpty();
                    }
                }
                else
                {
                    var newValue = value;

                    if (contents.IsNone)
                    {
                        // ［切抜きカーソル］の指すタイル無し時

                        // 新規作成
                        this.croppedCursorPointedTileRecordVisually = TileRecordVisually.CreateEmpty();
                    }
                    else
                    {
                        // ［切抜きカーソル］の指すタイルが有るなら構わない
                    }

                    // （変更通知を送っている）
                    this.CroppedCursorPointedTileIdOrEmpty = newValue.Id;
                    this.CroppedCursorPointedTileSourceLeftAsInt = newValue.SourceRectangle.Location.X.AsInt;
                    this.CroppedCursorPointedTileSourceTopAsInt = newValue.SourceRectangle.Location.Y.AsInt;
                    this.CroppedCursorPointedTileSourceWidthAsInt = newValue.SourceRectangle.Size.Width.AsInt;
                    this.CroppedCursorPointedTileSourceHeightAsInt = newValue.SourceRectangle.Size.Height.AsInt;
                    this.CroppedCursorPointedTileTitleAsStr = newValue.Title.AsStr;
                }

                // 変更通知を送りたい
                OnPropertyChanged(nameof(AddsButtonHint));
                OnPropertyChanged(nameof(AddsButtonText));
                this.NotifyTileIdChange();
                // TODO 矩形もリフレッシュしたい

                // タイルタイトル
                OnPropertyChanged(nameof(IsEnabledCroppedCursorPointedTileTitleAsStr));
            }
        }
        #endregion

        #region プロパティ（［切抜きカーソルが指すタイル］　関連）
        /// <summary>
        ///     ［切抜きカーソルが指すタイル］のズーム済みの位置とサイズ
        /// </summary>
        public Models.Geometric.RectangleFloat CroppedCursorPointedTileWorkingRect
        {
            get => this.croppedCursorPointedTileWorkingRect;
            set
            {
                if (this.croppedCursorPointedTileWorkingRect == value)
                    return;

                // 末端へ下りる
                this.CroppedCursorPointedTileWorkingLocation = value.Location;
                this.CroppedCursorPointedTileWorkingSize = value.Size;
            }
        }

        /// <summary>
        ///     ［切抜きカーソルが指すタイル］のズーム済みの位置
        ///         
        ///     <list type="bullet">
        ///         <item>カーソルの線の幅を含まない</item>
        ///     </list>
        /// </summary>
        public Models.Geometric.PointFloat CroppedCursorPointedTileWorkingLocation
        {
            get => this.croppedCursorPointedTileWorkingRect.Location;
            set
            {
                if (this.croppedCursorPointedTileWorkingRect.Location == value)
                    return;

                // 末端へ下りる
                this.CroppedCursorPointedTileWorkingLeftAsFloat = value.X.AsFloat;
                this.CroppedCursorPointedTileWorkingTopAsFloat = value.Y.AsFloat;
            }
        }

        /// <summary>
        ///     ［切抜きカーソルが指すタイル］のＩｄ
        /// </summary>
        public Models.TileIdOrEmpty CroppedCursorPointedTileIdOrEmpty
        {
            get
            {
                var contents = this.croppedCursorPointedTileRecordVisually;

                // ［切抜きカーソル］の指すタイル無し時
                if (contents.IsNone)
                    return Models.TileIdOrEmpty.Empty;

                return contents.Id;
            }
            set
            {
                var contents = this.croppedCursorPointedTileRecordVisually;

                if (contents.IsNone)
                {
                    // ［切抜きカーソル］の指すタイル無し時

                    // タイルＩｄだけ持っておく
                    this.croppedCursorPointedTileRecordVisually = TileRecordVisually.FromModel(
                        tileRecord: new Models.TileRecord(
                            id: value,  // 更新
                            rect: Models.Geometric.RectangleInt.Empty,
                            title: Models.TileTitle.Empty,
                            logicalDelete: Models.LogicalDelete.False),
                    zoom: this.Zoom);
                }
                else
                {
                    // 値に変化がない
                    if (contents.Id == value)
                        return;

                    this.croppedCursorPointedTileRecordVisually = TileRecordVisually.FromModel(
                        tileRecord: new TileRecord(
                            id: value,  // 更新
                            rect: contents.SourceRectangle,
                            title: contents.Title,
                            logicalDelete: contents.LogicalDelete),
                        zoom: this.Zoom);
                }

                this.InvalidateLocale();

                // ［追加／上書き］ボタン再描画
                this.InvalidateAddsButton();

                // ［削除］ボタン再描画
                this.InvalidateDeletesButton();

                NotifyTileIdChange();
            }
        }
        #endregion

        // - パブリック・メソッド

        #region メソッド（ロケール変更による再描画）
        /// <summary>
        ///     ロケール変更による再描画
        ///     
        ///     <list type="bullet">
        ///         <item>動的にテキストを変えている部分に対応するため</item>
        ///     </list>
        /// </summary>
        public void InvalidateLocale() => this.InvalidateAddsButton();
        #endregion

        #region メソッド（画面遷移でこの画面に戻ってきた時）
        /// <summary>
        ///     画面遷移でこの画面に戻ってきた時
        /// </summary>
        public void ReactOnVisited()
        {
            // ロケールが変わってるかもしれないので反映
            OnPropertyChanged(nameof(SelectedCultureInfo));

            // グリッド・キャンバス
            {
                // グリッドの左上位置（初期値）
                this.GridPhaseSourceLocation = new Models.Geometric.PointInt(new Models.Geometric.XInt(0), new Models.Geometric.YInt(0));

                // グリッドのタイルサイズ（初期値）
                this.SourceGridUnit = new Models.Geometric.SizeInt(new Models.Geometric.WidthInt(32), new Models.Geometric.HeightInt(32));

                // グリッド・キャンバス画像の再作成
                this.RemakeGridCanvasImage();
            }
        }
        #endregion

        #region メソッド（［追加／上書き］　関連）
        /// <summary>
        ///     ［追加］
        /// </summary>
        public void AddRegisteredTile()
        {
            var contents = this.CroppedCursorPointedTileRecordVisually;

            TileIdOrEmpty tileIdOrEmpty;

            // Ｉｄが空欄
            // ［追加］（新規作成）だ

            // ［切抜きカーソル］にサイズがなければ、何もしない
            if (contents.IsNone)
                return;

            // 新しいタイルＩｄを発行
            tileIdOrEmpty = this.TilesetSettingsVM.UsableId;
            this.TilesetSettingsVM.IncreaseUsableId();

            // 追加でも、上書きでも、同じ処理でいける
            // ［登録タイル追加］処理
            App.History.Do(new AddRegisteredTileProcessing(
                owner: this,
                croppedCursorVisually: contents,
                tileIdOrEmpty: tileIdOrEmpty,
                workingRectangle: contents.SourceRectangle.Do(this.Zoom)));

            this.OnPropertyChanged(nameof(CanUndo));
            this.OnPropertyChanged(nameof(CanRedo));
        }

        /// <summary>
        ///     ［上書き］
        /// </summary>
        public void OverwriteRegisteredTile()
        {
            var contents = this.CroppedCursorPointedTileRecordVisually;

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
                owner: this,
                croppedCursorVisually: contents,
                tileIdOrEmpty: tileIdOrEmpty,
                workingRectangle: contents.SourceRectangle.Do(this.Zoom)));

            this.OnPropertyChanged(nameof(CanUndo));
            this.OnPropertyChanged(nameof(CanRedo));
        }

        /// <summary>
        ///     ［登録タイル］削除
        /// </summary>
        public void RemoveRegisteredTile()
        {
            App.History.Do(new RemoveRegisteredTileProcessing(
                owner: this,
                tileIdOrEmpty: this.CroppedCursorPointedTileIdOrEmpty));

            this.OnPropertyChanged(nameof(CanUndo));
            this.OnPropertyChanged(nameof(CanRedo));
        }
        #endregion

        // - インターナル・プロパティ

        #region プロパティ（切抜きカーソルと、既存タイルが交差しているか？）
        /// <summary>
        ///     切抜きカーソルと、既存タイルが交差しているか？
        /// </summary>
        /// <returns>そうだ</returns>
        internal bool HasIntersectionBetweenCroppedCursorAndRegisteredTile { get; private set; }
        #endregion

        #region プロパティ（切抜きカーソルと、既存タイルは合同か？）
        /// <summary>
        ///     切抜きカーソルと、既存タイルは合同か？
        /// </summary>
        /// <returns>そうだ</returns>
        internal bool IsCongruenceBetweenCroppedCursorAndRegisteredTile { get; private set; }
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

        // - インターナル・メソッド

        #region メソッド（［タイルセット設定］　関連）
        /// <summary>
        ///     ［タイルセット設定］ビューモデルに変更あり
        /// </summary>
        internal void InvalidateTilesetSettingsVM()
        {
            OnPropertyChanged(nameof(TilesetSettingsVM));
        }
        #endregion

        #region メソッド（［タイルセット作業画像］　関連）
        /// <summary>
        ///     ［タイルセット作業画像］の再描画
        /// </summary>
        internal void RefreshWorkingTilesetImage()
        {
            OnPropertyChanged(nameof(TilesetWorkingImageFilePathAsStr));
        }
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
            int offset;

            if (((int)this.croppedCursorPointedTileWorkingRect.Size.Width.AsFloat) % 2 == 1) // FIXME 浮動小数点型の剰余は無理がある
            {
                // Trace.WriteLine($"{codePlace} 幅 {this._tileCursorCanvasSize.Width.AsInt} から 1 引く");
                offset = -1;
            }
            else
            {
                // Trace.WriteLine($"{codePlace} 幅 {this._tileCursorCanvasSize.Width.AsInt} へ 1 足す");
                offset = 1;
            }

            // TODO 循環参照を避けるために、直接フィールドを変更
            this.CroppedCursorPointedTileWorkingSize = new Models.Geometric.SizeFloat(
                width: new Models.Geometric.WidthFloat(this.croppedCursorPointedTileWorkingRect.Size.Width.AsFloat + offset),
                height: new Models.Geometric.HeightFloat(this.croppedCursorPointedTileWorkingRect.Size.Height.AsFloat));

            // TRICK CODE:
            OnPropertyChanged(nameof(CroppedCursorPointedTileWorkingWidthAsFloat));
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
                some: (tileRecordVisualBuffer) =>
                {
                    // Trace.WriteLine($"[TileCropPage.xml.cs TapGestureRecognizer_Tapped] タイルは登録済みだ。 Id:{recordVM.Id.AsInt}, X:{recordVM.SourceRectangle.Location.X.AsInt}, Y:{recordVM.SourceRectangle.Location.Y.AsInt}, Width:{recordVM.SourceRectangle.Size.Width.AsInt}, Height:{recordVM.SourceRectangle.Size.Height.AsInt}, Title:{recordVM.Title.AsStr}");

                    //
                    // データ表示
                    // ==========
                    //

                    // 選択中のタイルを設定
                    this.CroppedCursorPointedTileRecordVisually = tileRecordVisualBuffer;
                },
                none: () =>
                {
                    // Trace.WriteLine("[TileCropPage.xml.cs TapGestureRecognizer_Tapped] 未登録のタイルだ");

                    //
                    // 空欄にする
                    // ==========
                    //

                    // 選択中のタイルの矩形だけ維持し、タイル・コードと、コメントを空欄にする
                    this.CroppedCursorPointedTileRecordVisually = TileRecordVisually.FromModel(
                        tileRecord: new Models.TileRecord(
                            id: Models.TileIdOrEmpty.Empty,
                            rect: this.CroppedCursorPointedTileSourceRect,
                            title: Models.TileTitle.Empty,
                            logicalDelete: Models.LogicalDelete.False),
                        zoom: this.Zoom);
                });
        }
        #endregion

        #region メソッド（［選択タイル］　関連）
        /// <summary>
        ///     ［選択タイル］Ｉｄの再描画
        /// </summary>
        internal void NotifyTileIdChange()
        {
            OnPropertyChanged(nameof(CroppedCursorPointedTileIdAsBASE64));
            OnPropertyChanged(nameof(CroppedCursorPointedTileIdAsPhoneticCode));
        }
        #endregion

        #region メソッド（［追加／上書き］ボタン　関連）
        /// <summary>
        ///     ［追加／上書き］ボタンの再描画
        /// </summary>
        internal void InvalidateAddsButton()
        {
            // 切抜きカーソルが、登録済みタイルのいずれかと交差しているか？
            if (this.HasIntersectionBetweenCroppedCursorAndRegisteredTile)
            {
                // 合同のときは「交差中」とは表示しない
                if (!this.IsCongruenceBetweenCroppedCursorAndRegisteredTile)
                {
                    // 「交差中」
                    // Trace.WriteLine("[TileCropPage.xml.cs InvalidateAddsButton] 交差中だ");

                    this.AddsButtonText = (string)LocalizationResourceManager.Instance["Intersecting"];
                    this.IsEnabledAddsButton = false;
                    return;
                }
            }

            var contents = this.croppedCursorPointedTileRecordVisually;

            if (contents.IsNone)
            {
                // ［切抜きカーソル］の指すタイル無し時

                // 「追加」
                this.AddsButtonText = (string)LocalizationResourceManager.Instance["Add"];
                this.IsEnabledAddsButton = false;
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
                    this.AddsButtonText = (string)LocalizationResourceManager.Instance["Add"];
                }
                else
                {
                    // ［復元」
                    this.AddsButtonText = (string)LocalizationResourceManager.Instance["Restore"];
                }


                this.IsEnabledAddsButton = true;
            }
        }
        #endregion

        #region メソッド（［削除］ボタン　関連）
        /// <summary>
        ///     ［削除］ボタンの再描画
        /// </summary>
        internal void InvalidateDeletesButton()
        {
            var contents = this.croppedCursorPointedTileRecordVisually;

            if (contents.IsNone)
            {
                // 切抜きカーソル無し時
                this.IsEnabledDeletesButton = false;
                return;
            }

            // 切抜きカーソル有り時
            if (contents.Id == TileIdOrEmpty.Empty)
            {
                // Ｉｄ未設定時
                this.IsEnabledDeletesButton = false;
            }
            else
            {
                // タイル登録済み時
                this.IsEnabledDeletesButton = true;
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

            Trace.WriteLine($"[TileCropPageViewModel.cs RecalculateBetweenCroppedCursorAndRegisteredTile] HasIntersectionBetweenCroppedCursorAndRegisteredTile: {HasIntersectionBetweenCroppedCursorAndRegisteredTile}, IsCongruenceBetweenCroppedCursorAndRegisteredTile: {IsCongruenceBetweenCroppedCursorAndRegisteredTile}");
        }
        #endregion

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
                gridLeftTop: this.WorkingGridPhase,
                gridTile: this.WorkingGridUnit);

            // ズームを除去
            var sourceRect = new RectangleInt(
                location: new PointInt(
                    x: new XInt((int)(workingRect.Location.X.AsFloat / this.ZoomAsFloat)),
                    y: new YInt((int)(workingRect.Location.Y.AsFloat / this.ZoomAsFloat))),
                size: new SizeInt(
                    width: new WidthInt((int)(workingRect.Size.Width.AsFloat / this.ZoomAsFloat)),
                    height: new HeightInt((int)(workingRect.Size.Height.AsFloat / this.ZoomAsFloat))));

            //
            // 計算値の反映
            // ============
            //
            // Trace.WriteLine($"[TileCropPage.xaml.cs RefreshTileForm] context.IsMouseDragging: {context.IsMouseDragging}, context.HalfThicknessOfTileCursorLine.AsInt: {context.HalfThicknessOfTileCursorLine.AsInt}, rect x:{rect.Point.X.AsInt} y:{rect.Point.Y.AsInt} width:{rect.Size.Width.AsInt} height:{rect.Size.Height.AsInt}");

            // this.CroppedCursorPointedTileWorkingRect = workingRect;
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
            this.InvalidateAddsButton();

            // （切抜きカーソル更新後）［削除］ボタン活性化
            this.InvalidateDeletesButton();

            // タイルセット タイトル
            OnPropertyChanged(nameof(CroppedCursorPointedTileTitleAsStr));
        }

        // - プライベート・フィールド

        #region フィールド（ポインティング・デバイス押下中か？）
        /// <summary>
        ///     ポインティング・デバイス押下中か？
        /// </summary>
        bool isMouseDragging;
        #endregion

        #region フィールド（［タイルセット設定］　関連）
        /// <summary>
        ///     ［タイルセット設定］のCSVファイル
        /// </summary>
        TheFileEntryLocations.UnityAssets.DataCsvTilesetCsv _tilesetSettingsFile = TheFileEntryLocations.UnityAssets.DataCsvTilesetCsv.Empty;

        /// <summary>
        ///     ［タイルセット設定］ビューモデル
        /// </summary>
        TilesetDatatableVisually _tilesetSettingsVM = new();
        #endregion

        #region フィールド（［タイルセット元画像］　関連）
        /// <summary>
        ///     ［タイルセット元画像］
        /// </summary>
        SKBitmap tilesetSourceBitmap = new();

        /// <summary>
        ///     ［タイルセット元画像］ファイルへのパス
        /// </summary>
        TheFileEntryLocations.UnityAssets.Images.TilesetPng tilesetImageFile = TheFileEntryLocations.UnityAssets.Images.TilesetPng.Empty;

        /// <summary>
        ///     ［タイルセット元画像］サイズ
        /// </summary>
        Models.Geometric.SizeInt tilesetSourceImageSize = Models.Geometric.SizeInt.Empty;
        #endregion

        #region フィールド（［タイルセット作業画像］　関連）
        /// <summary>
        ///     ［タイルセット作業画像］サイズ
        /// </summary>
        Models.Geometric.SizeInt workingImageSize = Models.Geometric.SizeInt.Empty;
        #endregion

        #region フィールド（［ズーム］　関連）
        /// <summary>
        ///     ［ズーム］最大
        /// </summary>
        Models.Geometric.Zoom zoomMax = new(4.0f);

        /// <summary>
        ///     ［ズーム］最小
        /// </summary>
        Models.Geometric.Zoom zoomMin = new(0.5f);

        /// <summary>
        ///     ［ズーム］
        /// </summary>
        Models.Geometric.Zoom zoom = Models.Geometric.Zoom.IdentityElement;
        #endregion

        #region フィールド（［元画像グリッド］　関連）
        /// <summary>
        ///     ［元画像グリッド］のキャンバス画像サイズ
        /// </summary>
        Models.Geometric.SizeInt gridCanvasImageSize = Models.Geometric.SizeInt.Empty;

        /// <summary>
        ///     ［元画像グリッド］の単位
        /// </summary>
        Models.Geometric.SizeInt sourceGridUnit = new(new Models.Geometric.WidthInt(32), new Models.Geometric.HeightInt(32));

        /// <summary>
        ///     ［元画像グリッド］の位相の左上表示位置
        /// </summary>
        Models.Geometric.PointInt gridPhaseSourceLocation = Models.Geometric.PointInt.Empty;

        /// <summary>
        ///     ［元画像グリッド］の線の太さの半分
        /// </summary>
        ThicknessOfLine halfThicknessOfGridLine = new(1);
        #endregion

        #region フィールド（［作業グリッド］　関連）
        /// <summary>
        ///     ［作業グリッド］の単位
        /// </summary>
        Models.Geometric.SizeFloat workingGridUnit = new(new Models.Geometric.WidthFloat(32.0f), new Models.Geometric.HeightFloat(32.0f));

        /// <summary>
        ///     ［作業グリッド］の位相の左上表示位置
        /// </summary>
        Models.Geometric.PointFloat workingGridPhase = Models.Geometric.PointFloat.Empty;
        #endregion

        #region フィールド（［切抜きカーソル］　関連）
        /// <summary>
        ///     ［切抜きカーソル］が指すタイル
        ///     
        ///     <list type="bullet">
        ///         <item>★循環参照しやすいので注意</item>
        ///         <item>［切抜きカーソル］が指すタイルが未確定のときも、指しているタイルにアクセスできることに注意</item>
        ///     </list>
        /// </summary>
        TileRecordVisually croppedCursorPointedTileRecordVisually = TileRecordVisually.CreateEmpty();

        /// <summary>
        ///     ［切抜きカーソル］ズーム済みの位置
        ///         
        ///     <list type="bullet">
        ///         <item>カーソルの線の幅を含まない</item>
        ///         <item>TODO ★ croppedCursorPointedTileRecordVisualBuffer に統合できないか？</item>
        ///     </list>
        /// </summary>
        Models.Geometric.RectangleFloat croppedCursorPointedTileWorkingRect = Models.Geometric.RectangleFloat.Empty;
        /*
        /// <summary>
        ///     ［切抜きカーソル］ズーム済みの位置
        ///         
        ///     <list type="bullet">
        ///         <item>カーソルの線の幅を含まない</item>
        ///         <item>TODO ★ croppedCursorPointedTileRecordVisualBuffer に統合できないか？</item>
        ///     </list>
        /// </summary>
        Models.Geometric.PointFloat CroppedCursorPointedTileWorkingLocationTemp
        {
            get
            {
                return this.croppedCursorPointedTileRecordVisualBuffer.WorkingRectangle.Location;
            }
            set
            {
                if (this.croppedCursorPointedTileRecordVisualBuffer.WorkingRectangle.Location == value)
                    return;

                this.croppedCursorPointedTileRecordVisualBuffer.WorkingRectangle = new RectangleFloat(
                    location: value,
                    size: this.croppedCursorPointedTileRecordVisualBuffer.WorkingRectangle.Size);
            }
        }
        */
        /*
        /// <summary>
        ///     ［切抜きカーソル］ズーム済みのサイズ
        ///         
        ///     <list type="bullet">
        ///         <item>カーソルの線の幅は含まない</item>
        ///         <item>TODO ★ croppedCursorPointedTileRecordVisualBuffer に統合できないか？</item>
        ///     </list>
        /// </summary>
        Models.Geometric.SizeFloat CroppedCursorPointedTileWorkingSizeTemp
        {
            get
            {
                return this.croppedCursorPointedTileRecordVisualBuffer.WorkingRectangle.Size;
            }
            set
            {
                if (this.croppedCursorPointedTileRecordVisualBuffer.WorkingRectangle.Size == value)
                    return;

                this.croppedCursorPointedTileRecordVisualBuffer.WorkingRectangle = new RectangleFloat(
                    location: this.croppedCursorPointedTileRecordVisualBuffer.WorkingRectangle.Location,
                    size: value);
            }
        }
        */

        /// <summary>
        ///     ［切抜きカーソル］の線の半分の太さ
        /// </summary>
        ThicknessOfLine halfThicknessOfTileCursorLine;
        #endregion

        #region フィールド（［追加／上書き］ボタン　関連）
        /// <summary>
        ///     ［追加／上書き］ボタンのラベル
        /// </summary>
        string addsButtonText = string.Empty;

        /// <summary>
        ///     ［追加／上書き］ボタンの活性性
        /// </summary>
        bool isEnabledAddsButton;
        #endregion

        #region フィールド（［削除］ボタン　関連）
        /// <summary>
        ///     ［削除］ボタンの活性性
        /// </summary>
        bool isEnabledDeletesButton;
        #endregion

        // - プライベート・メソッド

        #region メソッド（［タイルセット作業画像］　関連）
        /// <summary>
        ///     <pre>
        ///         ［元画像グリッド］のキャンバスの再描画
        /// 
        ///         TRICK:  GraphicsView を再描画させたいが、ビューモデルから要求する方法が分からない。
        ///                 そこで、内部的なグリッド画像の横幅が偶数のときは +1、奇数のときは -1 して
        ///                 振動させることで、再描画を呼び起こすことにする
        ///     </pre>
        /// </summary>
        void InvalidateGraphicsViewOfTilesetWorking()
        {
            if (this.TilesetWorkingImageWidthAsInt % 2 == 1)
            {
                this.workingImageSize = new SizeInt(
                    width: new WidthInt(this.workingImageSize.Width.AsInt - 1),
                    height: new HeightInt(this.workingImageSize.Height.AsInt));
            }
            else
            {
                this.workingImageSize = new SizeInt(
                    width: new WidthInt(this.workingImageSize.Width.AsInt + 1),
                    height: new HeightInt(this.workingImageSize.Height.AsInt));
            }

            OnPropertyChanged(nameof(TilesetWorkingImageWidthAsInt));
        }

        /// <summary>
        ///     ［タイルセット作業画像］の再作成
        /// </summary>
        void RemakeWorkingTilesetImage()
        {
            // 元画像をベースに、作業画像を複製
            var temporaryBitmap = SkiaSharp.SKBitmap.FromImage(SkiaSharp.SKImage.FromBitmap(this.TilesetSourceBitmap));

            // 画像処理（明度を下げる）
            FeatSkia.ReduceBrightness.DoItInPlace(temporaryBitmap);

            // 作業画像のサイズ計算
            this.workingImageSize = new Models.Geometric.SizeInt(
                width: new Models.Geometric.WidthInt((int)(this.ZoomAsFloat * this.TilesetSourceImageSize.Width.AsInt)),
                height: new Models.Geometric.HeightInt((int)(this.ZoomAsFloat * this.TilesetSourceImageSize.Height.AsInt)));

            // 作業画像のリサイズ
            this.TilesetWorkingBitmap = temporaryBitmap.Resize(
                size: new SKSizeI(
                    width: this.workingImageSize.Width.AsInt,
                    height: this.workingImageSize.Height.AsInt),
                quality: SKFilterQuality.Medium);

            OnPropertyChanged(nameof(TilesetWorkingImageWidthAsInt));
            OnPropertyChanged(nameof(TilesetWorkingImageHeightAsInt));
        }
        #endregion

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

        /// <summary>
        ///     ［元画像グリッド］のキャンバス画像の再作成
        ///     
        ///     <list type="bullet">
        ///         <item>グリッドの線の太さを 2px と想定しているので、グリッドの線が画像の端っこで切れないように、グリッドの内部的キャンバス・サイズを 2px 広げる</item>
        ///     </list>
        /// </summary>
        void RemakeGridCanvasImage()
        {
            this.GridCanvasImageSize = new Models.Geometric.SizeInt(
                width: new Models.Geometric.WidthInt((int)(this.ZoomAsFloat * this.TilesetSourceImageSize.Width.AsInt) + (2 * this.HalfThicknessOfGridLineAsInt)),
                height: new Models.Geometric.HeightInt((int)(this.ZoomAsFloat * this.TilesetSourceImageSize.Height.AsInt) + (2 * this.HalfThicknessOfGridLineAsInt)));
        }
        #endregion

        #region メソッド（［作業グリッド］　関連）
        /// <summary>
        ///     ［作業グリッド］タイル横幅の再計算
        /// </summary>
        void RefreshWorkingGridTileWidth()
        {
            this.WorkingGridTileWidthAsFloat = this.ZoomAsFloat * this.sourceGridUnit.Width.AsInt;

            OnPropertyChanged(nameof(WorkingGridTileWidthAsFloat));
            OnPropertyChanged(nameof(WorkingGridUnit));
        }

        /// <summary>
        ///     ［作業グリッド］タイル縦幅の再計算
        /// </summary>
        void RefreshWorkingGridTileHeight()
        {
            this.WorkingGridTileHeightAsFloat = this.ZoomAsFloat * this.sourceGridUnit.Height.AsInt;

            OnPropertyChanged(nameof(WorkingGridTileHeightAsFloat));
            OnPropertyChanged(nameof(WorkingGridUnit));
        }
        #endregion

        // - プライベート・クラス

        #region クラス（［ズーム］処理）
        /// <summary>
        ///     ［ズーム］処理
        /// </summary>
        class ZoomProcessing : IProcessing
        {
            // - その他

            /// <summary>
            ///     生成
            /// </summary>
            /// <param name="oldValue">変更前の値</param>
            /// <param name="newValue">変更後の値</param>
            internal ZoomProcessing(TileCropPageViewModel owner, Zoom oldValue, Zoom newValue)
            {
                this.Owner = owner;
                this.OldValue = oldValue;
                this.NewValue = newValue;
            }

            // - パブリック・メソッド

            /// <summary>
            ///     ドゥ
            /// </summary>
            public void Do()
            {
                this.Owner.Zoom = this.NewValue;

                this.AfterChanged();
            }

            /// <summary>
            ///     アンドゥ
            /// </summary>
            public void Undo()
            {
                this.Owner.Zoom = this.OldValue;

                this.AfterChanged();
            }

            // - プライベート・プロパティ

            /// <summary>
            ///     外側のクラス
            /// </summary>
            TileCropPageViewModel Owner { get; }

            /// <summary>
            ///     変更前の値
            /// </summary>
            Zoom OldValue { get; }

            /// <summary>
            ///     変更後の値
            /// </summary>
            Zoom NewValue { get; }

            // - プライベート・メソッド

            /// <summary>
            ///     ［ズーム］変更後の影響
            /// </summary>
            void AfterChanged()
            {
                // ［タイルセット作業画像］の更新
                {
                    // 画像の再作成
                    this.Owner.RemakeWorkingTilesetImage();
                }

                // ［元画像グリッド］の更新
                {
                    // キャンバス画像の再作成
                    this.Owner.RemakeGridCanvasImage();
                }

                // ［作業グリッド］の再計算
                {
                    // 横幅
                    this.Owner.RefreshWorkingGridTileWidth();
                    // 縦幅
                    this.Owner.RefreshWorkingGridTileHeight();
                }

                // ［切抜きカーソルが指すタイル］更新
                {
                    // 位置
                    this.Owner.CroppedCursorPointedTileWorkingLocation = new TheGeometric.PointFloat(
                        x: new TheGeometric.XFloat(this.Owner.ZoomAsFloat * this.Owner.CroppedCursorPointedTileSourceRect.Location.X.AsInt),
                        y: new TheGeometric.YFloat(this.Owner.ZoomAsFloat * this.Owner.CroppedCursorPointedTileSourceRect.Location.Y.AsInt));

                    // サイズ
                    this.Owner.CroppedCursorPointedTileWorkingSize = new TheGeometric.SizeFloat(
                        width: new TheGeometric.WidthFloat(this.Owner.ZoomAsFloat * this.Owner.CroppedCursorPointedTileSourceRect.Size.Width.AsInt),
                        height: new TheGeometric.HeightFloat(this.Owner.ZoomAsFloat * this.Owner.CroppedCursorPointedTileSourceRect.Size.Height.AsInt));
                }

                // 全ての［登録タイル］の更新
                foreach (var registeredTileVM in this.Owner.TilesetSettingsVM.TileRecordVisuallyList)
                {
                    // ズーム
                    registeredTileVM.Zoom = this.Owner.Zoom;
                }

                this.Owner.OnPropertyChanged(nameof(CanUndo));
                this.Owner.OnPropertyChanged(nameof(CanRedo));
                this.Owner.OnPropertyChanged(nameof(ZoomAsFloat));

                this.Owner.OnPropertyChanged(nameof(WorkingGridPhaseLeftAsFloat));
                this.Owner.OnPropertyChanged(nameof(WorkingGridPhaseTopAsFloat));
                this.Owner.OnPropertyChanged(nameof(WorkingGridPhase));

                this.Owner.OnPropertyChanged(nameof(WorkingGridTileWidthAsFloat));
                this.Owner.OnPropertyChanged(nameof(WorkingGridTileHeightAsFloat));
                this.Owner.OnPropertyChanged(nameof(WorkingGridUnit));

                // 切抜きカーソル。ズーム後
                this.Owner.OnPropertyChanged(nameof(CroppedCursorWorkingPointAsMargin));
                this.Owner.OnPropertyChanged(nameof(CanvasOfCroppedCursorWorkingWidthAsFloat));
                this.Owner.OnPropertyChanged(nameof(CanvasOfCroppedCursorWorkingHeightAsFloat));
                this.Owner.OnPropertyChanged(nameof(CroppedCursorPointedTileWorkingSize));
                this.Owner.OnPropertyChanged(nameof(CroppedCursorPointedTileWorkingLeftAsPresentableText));   // TODO これは要るか？
                this.Owner.OnPropertyChanged(nameof(CroppedCursorPointedTileWorkingTopAsPresentableText));   // TODO これは要るか？
                this.Owner.OnPropertyChanged(nameof(CroppedCursorPointedTileWorkingWidthAsPresentableText));   // TODO これは要るか？
                this.Owner.OnPropertyChanged(nameof(CroppedCursorPointedTileWorkingHeightAsPresentableText));   // TODO これは要るか？
            }
        }
        #endregion

        #region クラス（［登録タイル追加］処理）
        /// <summary>
        ///     ［登録タイル追加］処理
        /// </summary>
        class AddRegisteredTileProcessing : IProcessing
        {
            // - その他

            /// <summary>
            ///     生成
            /// </summary>
            /// <param name="owner"></param>
            /// <param name="croppedCursorVisually"></param>
            /// <param name="tileIdOrEmpty"></param>
            /// <param name="workingRectangle"></param>
            internal AddRegisteredTileProcessing(
                TileCropPageViewModel owner,
                TileRecordVisually croppedCursorVisually,
                TileIdOrEmpty tileIdOrEmpty,
                RectangleFloat workingRectangle)
            {
                this.Owner = owner;
                this.CroppedCursorVisually = croppedCursorVisually;
                this.TileIdOrEmpty = tileIdOrEmpty;
                this.WorkingRectangle = workingRectangle;
            }

            // - パブリック・メソッド

            #region メソッド（ドゥ―）
            /// <summary>
            ///     ドゥ―
            /// </summary>
            public void Do()
            {
                // ［タイル］のＩｄ変更
                this.Owner.CroppedCursorPointedTileIdOrEmpty = this.TileIdOrEmpty;

                // ビューの再描画（タイルＩｄ更新）
                this.Owner.NotifyTileIdChange();

                // リストに登録済みか確認
                if (!this.Owner.TilesetSettingsVM.TryGetTileById(this.TileIdOrEmpty, out TileRecordVisually? tileRecordVisuallyOrNull))
                {
                    // リストに無ければ、ダミーのタイルを追加（あとですぐ上書きする）
                    this.Owner.TilesetSettingsVM.AddTileVisually(
                        id: this.TileIdOrEmpty,
                        rect: RectangleInt.Empty,
                        zoom: Zoom.IdentityElement,
                        title: Models.TileTitle.Empty,
                        logicalDelete: Models.LogicalDelete.False);
                }

                //
                // この時点で、タイルは必ず登録されている
                //

                // リストに必ず登録されているはずなので、選択タイルＩｄを使って、タイル・レコードを取得、その内容に、登録タイルを上書き
                if (this.Owner.TilesetSettingsVM.TryGetTileById(this.TileIdOrEmpty, out tileRecordVisuallyOrNull))
                {
                    TileRecordVisually tileRecordVisually = tileRecordVisuallyOrNull ?? throw new NullReferenceException(nameof(tileRecordVisuallyOrNull));

                    // 新・元画像の位置とサイズ
                    tileRecordVisually.SourceRectangle = this.CroppedCursorVisually.SourceRectangle;

                    // 新・作業画像の位置とサイズ
                    tileRecordVisually.Zoom = this.Owner.Zoom;

                    // 新・タイル・タイトル
                    tileRecordVisually.Title = this.CroppedCursorVisually.Title;

                    // 新・論理削除
                    tileRecordVisually.LogicalDelete = this.CroppedCursorVisually.LogicalDelete;
                }

                //
                // 設定ファイルの保存
                // ==================
                //
                if (!this.Owner.TilesetSettingsVM.SaveCSV(this.Owner.TilesetDatatableFileLocation))
                {
                    // TODO 保存失敗時のエラー対応
                }

                //
                // カラーマップの再描画
                // ====================
                //
                this.Owner.InvalidateGraphicsViewOfTilesetWorking();

                this.Owner.OnPropertyChanged(nameof(IsEnabledCroppedCursorPointedTileTitleAsStr));
            }
            #endregion

            #region メソッド（アンドゥ―）
            /// <summary>
            ///     アンドゥ―
            /// </summary>
            /// <exception cref="NotImplementedException"></exception>
            public void Undo()
            {
                // ［タイル］のＩｄ消去
                this.Owner.CroppedCursorPointedTileIdOrEmpty = TileIdOrEmpty.Empty;

                // ビューの再描画（タイルＩｄ更新）
                this.Owner.NotifyTileIdChange();

                // リストから削除
                if (!this.Owner.TilesetSettingsVM.TryRemoveTileById(this.TileIdOrEmpty, out TileRecordVisually? tileRecordVisualBufferOrNull))
                {
                    // TODO 成功しなかったら異常
                    throw new Exception();
                }

                //
                // 設定ファイルの保存
                // ==================
                //
                if (!this.Owner.TilesetSettingsVM.SaveCSV(this.Owner.TilesetDatatableFileLocation))
                {
                    // TODO 保存失敗時のエラー対応
                }

                // タイル タイトル表示欄とか更新したい
                this.Owner.OnPropertyChanged(nameof(CroppedCursorPointedTileTitleAsStr));

                // 追加・削除ボタンの表示状態を更新したい
                this.Owner.OnPropertyChanged(nameof(AddsButtonHint));
                this.Owner.OnPropertyChanged(nameof(AddsButtonText));
                this.Owner.OnPropertyChanged(nameof(IsEnabledAddsButton));

                //  ［削除］ボタンの再描画
                this.Owner.InvalidateDeletesButton();

                //
                // カラーマップの再描画
                // ====================
                //
                //this.coloredMapGraphicsView1.Invalidate();
                this.Owner.InvalidateGraphicsViewOfTilesetWorking();

                this.Owner.OnPropertyChanged(nameof(IsEnabledCroppedCursorPointedTileTitleAsStr));
            }
            #endregion

            // - プライベート・プロパティ

            /// <summary>
            ///     外側のクラス
            /// </summary>
            TileCropPageViewModel Owner { get; }

            /// <summary>
            ///     ［切抜きカーソル］に対応
            /// </summary>
            TileRecordVisually CroppedCursorVisually { get; }

            /// <summary>
            ///     ［タイル］のＩｄ
            /// </summary>
            TileIdOrEmpty TileIdOrEmpty { get; }

            /// <summary>
            ///     タイルセット作業画像の位置とサイズ
            /// </summary>
            RectangleFloat WorkingRectangle { get; }
        }
        #endregion

        #region クラス（［登録タイル削除］処理）
        /// <summary>
        ///     ［登録タイル削除］処理
        /// </summary>
        class RemoveRegisteredTileProcessing : IProcessing
        {
            /// <summary>
            ///     生成
            /// </summary>
            /// <param name="owner"></param>
            internal RemoveRegisteredTileProcessing(
                TileCropPageViewModel owner,
                TileIdOrEmpty tileIdOrEmpty)
            {
                this.Owner = owner;
                TileIdOrEmpty = tileIdOrEmpty;
            }

            public void Do()
            {
                //
                // 設定ファイルの編集
                // ==================
                //
                //      - 選択中のタイルを論理削除
                //
                if (this.Owner.TilesetSettingsVM.DeleteLogical(
                    // 現在選択中のタイルのＩｄ
                    id: this.TileIdOrEmpty))
                {
                    // タイルセット設定ビューモデルに変更あり
                    this.Owner.InvalidateTilesetSettingsVM();
                }

                Trace.WriteLine($"[TileCropPage.xml.cs DeletesButton_Clicked] タイルを論理削除 TileId: [{this.TileIdOrEmpty.AsBASE64}]");

                //
                // 設定ファイルの保存
                // ==================
                //
                if (this.Owner.TilesetSettingsVM.SaveCSV(this.Owner.TilesetDatatableFileLocation))
                {
                    // 保存成功
                }
                else
                {
                    // TODO 保存失敗時のエラー対応
                }

                //
                // カラーマップの再描画
                // ====================
                //
                this.Owner.InvalidateGraphicsViewOfTilesetWorking();

                this.Owner.OnPropertyChanged(nameof(IsEnabledCroppedCursorPointedTileTitleAsStr));
            }

            public void Undo()
            {
                //
                // 設定ファイルの編集
                // ==================
                //
                //      - 選択中のタイルの論理削除の取消
                //
                if (this.Owner.TilesetSettingsVM.UndeleteLogical(
                    // 現在選択中のタイルのＩｄ
                    id: this.TileIdOrEmpty))
                {
                    // タイルセット設定ビューモデルに変更あり
                    this.Owner.InvalidateTilesetSettingsVM();
                }

                Trace.WriteLine($"[TileCropPage.xml.cs DeletesButton_Clicked] タイルを論理削除 TileId: [{this.TileIdOrEmpty.AsBASE64}]");

                //
                // 設定ファイルの保存
                // ==================
                //
                if (this.Owner.TilesetSettingsVM.SaveCSV(this.Owner.TilesetDatatableFileLocation))
                {
                    // 保存成功
                }
                else
                {
                    // TODO 保存失敗時のエラー対応
                }

                //
                // カラーマップの再描画
                // ====================
                //
                this.Owner.InvalidateGraphicsViewOfTilesetWorking();

                this.Owner.OnPropertyChanged(nameof(IsEnabledCroppedCursorPointedTileTitleAsStr));
            }

            // - プライベート・プロパティ

            /// <summary>
            ///     外側のクラス
            /// </summary>
            TileCropPageViewModel Owner { get; }

            /// <summary>
            ///     ［タイル］のＩｄ
            /// </summary>
            TileIdOrEmpty TileIdOrEmpty { get; }
        }
        #endregion

        #region クラス（［文化情報設定］処理）
        /// <summary>
        ///     ［文化情報設定］処理
        /// </summary>
        class SetCultureInfoProcessing : IProcessing
        {
            // - その他

            /// <summary>
            ///     生成
            /// </summary>
            internal SetCultureInfoProcessing(
                TileCropPageViewModel owner,
                CultureInfo oldValue,
                CultureInfo newValue)
            {
                this.Owner = owner;
                this.OldValue = oldValue;
                this.NewValue = newValue;
            }

            /// <summary>
            ///     ドゥー
            /// </summary>
            public void Do()
            {
                this.Owner.SelectedCultureInfo = this.NewValue;
            }

            /// <summary>
            ///     アンドゥ
            /// </summary>
            public void Undo()
            {
                this.Owner.SelectedCultureInfo = this.OldValue;
            }

            // - プライベート・プロパティ

            /// <summary>
            ///     外側のクラス
            /// </summary>
            TileCropPageViewModel Owner { get; }

            CultureInfo OldValue { get; }

            CultureInfo NewValue { get; }
        }
        #endregion
    }
}
