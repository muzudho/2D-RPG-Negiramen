namespace _2D_RPG_Negiramen.ViewModels
{
    using _2D_RPG_Negiramen.Models;
    using _2D_RPG_Negiramen.Models.Geometric;
    using _2D_RPG_Negiramen.Models.Visually;
    using CommunityToolkit.Mvvm.ComponentModel;
    using SkiaSharp;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Globalization;
    using TheFileEntryLocations = _2D_RPG_Negiramen.Models.FileEntries.Locations;

#if IOS || ANDROID || MACCATALYST
    using Microsoft.Maui.Graphics.Platform;
    using SkiaSharp.Views.Maui.Controls;
    using _2D_RPG_Negiramen.ViewHistory.TileCropPage;
    using _2D_RPG_Negiramen.Specifications.TileCropPage;
#elif WINDOWS
    using Microsoft.Maui.Graphics.Win2D;
    using System.Net;
    using SkiaSharp.Views.Maui.Controls;
    using _2D_RPG_Negiramen.ViewHistory.TileCropPage;
    using _2D_RPG_Negiramen.Specifications.TileCropPage;
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
            this.Corridor = new ItsCorridor(this);

            // 循環参照しないように注意
            this.HalfThicknessOfTileCursorLine = new Models.ThicknessOfLine(2 * this.HalfThicknessOfGridLine.AsInt);
        }
        #endregion

        // - パブリック変更通知プロパティ

        #region 変更通知プロパティ（ロケール　関連）
        /// <summary>
        ///     現在選択中の文化情報。文字列形式
        ///     
        ///     <list type="bullet">
        ///         <item>透過メソッド</item>
        ///     </list>
        /// </summary>
        public CultureInfo SelectedCultureInfo
        {
            get => this.RoomsideDoors.IndoorCultureInfo.Selected;
            set => this.RoomsideDoors.IndoorCultureInfo.Selected = value;
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
        public int TilesetSourceImageWidthAsInt => this.RoomsideDoors.TilesetSourceImageSize.Width.AsInt;

        /// <summary>
        ///     ［タイルセット元画像］の縦幅。読取専用
        /// </summary>
        public int TilesetSourceImageHeightAsInt => this.RoomsideDoors.TilesetSourceImageSize.Height.AsInt;
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
        ///         <item>透過メソッド</item>
        ///     </list>
        /// </summary>
        public float ZoomAsFloat
        {
            get => this.RoomsideDoors.ZoomProperties.AsFloat;
            set => this.RoomsideDoors.ZoomProperties.AsFloat = value;
        }

        /// <summary>
        ///     ズーム最大
        ///     
        ///     <list type="bullet">
        ///         <item>透過メソッド</item>
        ///     </list>
        /// </summary>
        public float ZoomMaxAsFloat => this.RoomsideDoors.ZoomProperties.MaxAsFloat;

        /// <summary>
        ///     ズーム最小
        ///     
        ///     <list type="bullet">
        ///         <item>透過メソッド</item>
        ///     </list>
        /// </summary>
        public float ZoomMinAsFloat => this.RoomsideDoors.ZoomProperties.MinAsFloat;
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
            get => this.RoomsideDoors.GridUnit.SourceValue;
            set
            {
                if (this.RoomsideDoors.GridUnit.SourceValue != value)
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
            get => this.RoomsideDoors.GridUnit.SourceValue.Width.AsInt;
            set
            {
                if (this.RoomsideDoors.GridUnit.SourceValue.Width.AsInt != value &&
                    // バリデーション
                    0 < value && value <= this.TileMaxWidthAsInt)
                {
                    this.RoomsideDoors.GridUnit.SourceValue = new Models.Geometric.SizeInt(new Models.Geometric.WidthInt(value), this.RoomsideDoors.GridUnit.SourceValue.Height);

                    // 作業グリッド・タイル横幅の再計算
                    this.RoomsideDoors.CropCursor.RecalculateWorkingGridTileWidth();

                    // カーソルの線の幅を含まない
                    this.CroppedCursorPointedTileWorkingWidthAsFloat = this.ZoomAsFloat * this.RoomsideDoors.GridUnit.SourceValue.Width.AsInt;

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
            get => this.RoomsideDoors.GridUnit.SourceValue.Height.AsInt;
            set
            {
                if (this.RoomsideDoors.GridUnit.SourceValue.Height.AsInt != value &&
                    // バリデーション
                    0 < value && value <= this.TileMaxHeightAsInt)
                {
                    this.RoomsideDoors.GridUnit.SourceValue = new Models.Geometric.SizeInt(this.RoomsideDoors.GridUnit.SourceValue.Width, new Models.Geometric.HeightInt(value));

                    // 作業グリッド・タイル横幅の再計算
                    this.RoomsideDoors.CropCursor.RecalculateWorkingGridTileHeight();

                    // カーソルの線の幅を含まない
                    this.CroppedCursorPointedTileWorkingHeightAsFloat = this.ZoomAsFloat * this.RoomsideDoors.GridUnit.SourceValue.Height.AsInt;

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
        ///         <item>透過メソッド</item>
        ///     </list>
        /// </summary>
        public bool IsMouseDragging
        {
            get => this.RoomsideDoors.PointingDevice.IsMouseDragging;
            set => this.RoomsideDoors.PointingDevice.IsMouseDragging = value;
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
                var contents = this.RoomsideDoors.CropTile.SavesRecordVisually;

                if (contents.IsNone)
                {
                    // ［切抜きカーソル］の指すタイル無し時
                    return Models.Geometric.RectangleInt.Empty;
                }

                return contents.SourceRectangle;
            }
            set
            {
                var contents = this.RoomsideDoors.CropTile.SavesRecordVisually;

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

                this.CropTileSourceLeftAsInt = value.Location.X.AsInt;
                this.CropTileSourceTopAsInt = value.Location.Y.AsInt;
                this.CroppedCursorPointedTileSourceSize = value.Size;

                // 切抜きカーソル。ズーム済み
                // this.CroppedCursorPointedTileWorkingLeftAsFloat = this.ZoomAsFloat * this.CropTileSourceLeftAsInt;
                OnPropertyChanged(nameof(CroppedCursorPointedTileWorkingLeftAsFloat));
                OnPropertyChanged(nameof(CroppedCursorWorkingPointAsMargin));
                OnPropertyChanged(nameof(CroppedCursorPointedTileWorkingLeftAsPresentableText));

                // this.CroppedCursorPointedTileWorkingTopAsFloat = this.ZoomAsFloat * this.CropTileSourceTopAsInt;
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
        public int CropTileSourceLeftAsInt
        {
            get
            {
                var contents = this.RoomsideDoors.CropTile.SavesRecordVisually;

                if (contents.IsNone)
                {
                    // ［切抜きカーソル］無し時
                    return 0;
                }

                return contents.SourceRectangle.Location.X.AsInt;
            }
            set
            {
                var currentTileVisually = this.RoomsideDoors.CropTile.SavesRecordVisually;

                if (currentTileVisually.IsNone)
                {
                    // ［切抜きカーソル］無し時

                    this.RoomsideDoors.CropTile.SavesRecordVisually = TileRecordVisually.FromModel(
                        tileRecord: new Models.TileRecord(
                            id: Models.TileIdOrEmpty.Empty,
                            // 元画像ベース
                            rect: new Models.Geometric.RectangleInt(
                                location: new Models.Geometric.PointInt(new Models.Geometric.XInt(value), Models.Geometric.YInt.Empty),
                                size: Models.Geometric.SizeInt.Empty),
                            title: Models.TileTitle.Empty,
                            logicalDelete: Models.LogicalDelete.False),
                        zoom: this.RoomsideDoors.ZoomProperties.Value
#if DEBUG
                        , hint: "[TileCropPageViewModel.cs CropTileSourceLeftAsInt 1]"
#endif
                        );
                }
                else
                {
                    // 値に変化がない
                    if (currentTileVisually.SourceRectangle.Location.X.AsInt == value)
                        return;

                    this.RoomsideDoors.CropTile.SavesRecordVisually = TileRecordVisually.FromModel(
                        tileRecord: new Models.TileRecord(
                            id: currentTileVisually.Id,
                            // 元画像ベース
                            rect: new Models.Geometric.RectangleInt(
                                location: new Models.Geometric.PointInt(new Models.Geometric.XInt(value), currentTileVisually.SourceRectangle.Location.Y),
                                size: currentTileVisually.SourceRectangle.Size),
                            title: currentTileVisually.Title,
                            logicalDelete: currentTileVisually.LogicalDelete),
                        zoom: this.RoomsideDoors.ZoomProperties.Value
#if DEBUG
                        , hint: "[TileCropPageViewModel.cs CropTileSourceLeftAsInt 2]"
#endif
                        );
                }

                // 切抜きカーソル。ズーム済み
                // this.CroppedCursorPointedTileWorkingLeftAsFloat = this.ZoomAsFloat * this.CropTileSourceLeftAsInt;
                OnPropertyChanged(nameof(CroppedCursorPointedTileWorkingLeftAsFloat));
                OnPropertyChanged(nameof(CroppedCursorWorkingPointAsMargin));
                OnPropertyChanged(nameof(CroppedCursorPointedTileWorkingLeftAsPresentableText));

                // this.CroppedCursorPointedTileWorkingTopAsFloat = this.ZoomAsFloat * this.CropTileSourceTopAsInt;
                OnPropertyChanged(nameof(CroppedCursorPointedTileWorkingTopAsFloat));
                OnPropertyChanged(nameof(CroppedCursorWorkingPointAsMargin));
                OnPropertyChanged(nameof(CroppedCursorPointedTileWorkingTopAsPresentableText));

                // TODO サイズは変化無しか？

                OnPropertyChanged(nameof(CropTileSourceLeftAsInt));
                OnPropertyChanged(nameof(CroppedCursorPointedTileSourceRect));
            }
        }

        /// <summary>
        ///     ［切抜きカーソルが指すタイル］の元画像ベースの位置ｙ
        /// </summary>
        public int CropTileSourceTopAsInt
        {
            get
            {
                var contents = this.RoomsideDoors.CropTile.SavesRecordVisually;

                // ［切抜きカーソル］無し時
                if (contents.IsNone)
                    return 0;

                return contents.SourceRectangle.Location.Y.AsInt;
            }
            set
            {
                var currentTileVisually = this.RoomsideDoors.CropTile.SavesRecordVisually;

                if (currentTileVisually.IsNone)
                {
                    // ［切抜きカーソル］無し時

                    this.RoomsideDoors.CropTile.SavesRecordVisually = TileRecordVisually.FromModel(
                        tileRecord: new Models.TileRecord(
                            id: Models.TileIdOrEmpty.Empty,
                            // 元画像ベース
                            rect: new Models.Geometric.RectangleInt(
                            location: new Models.Geometric.PointInt(Models.Geometric.XInt.Empty, new Models.Geometric.YInt(value)),
                            size: Models.Geometric.SizeInt.Empty),
                            title: Models.TileTitle.Empty,
                            logicalDelete: Models.LogicalDelete.False),
                        zoom: this.RoomsideDoors.ZoomProperties.Value
#if DEBUG
                        , hint: "[TileCropPageViewModel.cs CropTileSourceTopAsInt 1]"
#endif
                        );
                }
                else
                {
                    // 値に変化がない
                    if (currentTileVisually.SourceRectangle.Location.Y.AsInt == value)
                        return;

                    this.RoomsideDoors.CropTile.SavesRecordVisually = TileRecordVisually.FromModel(
                        tileRecord: new Models.TileRecord(
                            id: currentTileVisually.Id,
                            // 元画像ベース
                            rect: new Models.Geometric.RectangleInt(
                            location: new Models.Geometric.PointInt(currentTileVisually.SourceRectangle.Location.X, new Models.Geometric.YInt(value)),
                            size: currentTileVisually.SourceRectangle.Size),
                            title: currentTileVisually.Title,
                            logicalDelete: currentTileVisually.LogicalDelete),
                        zoom: this.RoomsideDoors.ZoomProperties.Value
#if DEBUG
                        , hint: "[TileCropPageViewModel.cs CropTileSourceTopAsInt 2]"
#endif
                        );
                }

                // 切抜きカーソル。ズーム済み
                // this.CroppedCursorPointedTileWorkingLeftAsFloat = this.ZoomAsFloat * this.CropTileSourceLeftAsInt;
                OnPropertyChanged(nameof(CroppedCursorPointedTileWorkingLeftAsFloat));
                OnPropertyChanged(nameof(CroppedCursorWorkingPointAsMargin));
                OnPropertyChanged(nameof(CroppedCursorPointedTileWorkingLeftAsPresentableText));

                // this.CroppedCursorPointedTileWorkingTopAsFloat = this.ZoomAsFloat * this.CropTileSourceTopAsInt;
                OnPropertyChanged(nameof(CroppedCursorPointedTileWorkingTopAsFloat));
                OnPropertyChanged(nameof(CroppedCursorWorkingPointAsMargin));
                OnPropertyChanged(nameof(CroppedCursorPointedTileWorkingTopAsPresentableText));

                // TODO サイズは変化無しか？

                OnPropertyChanged(nameof(CropTileSourceTopAsInt));
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
                var contents = this.RoomsideDoors.CropTile.SavesRecordVisually;

                // ［切抜きカーソル］無し時
                if (contents.IsNone)
                    return Models.Geometric.SizeInt.Empty;

                return contents.SourceRectangle.Size;
            }
            set
            {
                var contents = this.RoomsideDoors.CropTile.SavesRecordVisually;

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
                this.CropTileSourceWidthAsInt = value.Width.AsInt;
                this.CropTileSourceHeightAsInt = value.Height.AsInt;

                OnPropertyChanged(nameof(CroppedCursorPointedTileSourceRect));
            }
        }

        /// <summary>
        ///     ［切抜きカーソルが指すタイル］の元画像ベースの横幅
        /// </summary>
        public int CropTileSourceWidthAsInt
        {
            get
            {
                var contents = this.RoomsideDoors.CropTile.SavesRecordVisually;

                // ［切抜きカーソル］無し時
                if (contents.IsNone)
                    return 0;

                return contents.SourceRectangle.Size.Width.AsInt;
            }
            set
            {
                var currentTileVisually = this.RoomsideDoors.CropTile.SavesRecordVisually;

                if (currentTileVisually.IsNone)
                {
                    // ［切抜きカーソル］無し時
                    this.RoomsideDoors.CropTile.SavesRecordVisually = TileRecordVisually.FromModel(
                        tileRecord: new Models.TileRecord(
                            id: Models.TileIdOrEmpty.Empty,
                            rect: new Models.Geometric.RectangleInt(Models.Geometric.PointInt.Empty, new Models.Geometric.SizeInt(new Models.Geometric.WidthInt(value), Models.Geometric.HeightInt.Empty)),
                            title: Models.TileTitle.Empty,
                            logicalDelete: Models.LogicalDelete.False),
                        zoom: this.RoomsideDoors.ZoomProperties.Value
#if DEBUG
                        , hint: "[TileCropPageViewModel.cs CropTileSourceWidthAsInt 1]"
#endif
                        );
                }
                else
                {
                    // 値に変化がない
                    if (currentTileVisually.SourceRectangle.Size.Width.AsInt == value)
                        return;

                    this.RoomsideDoors.CropTile.SavesRecordVisually = TileRecordVisually.FromModel(
                        tileRecord: new Models.TileRecord(
                            id: currentTileVisually.Id,
                            rect: new Models.Geometric.RectangleInt(currentTileVisually.SourceRectangle.Location, new Models.Geometric.SizeInt(new Models.Geometric.WidthInt(value), currentTileVisually.SourceRectangle.Size.Height)),
                            title: currentTileVisually.Title,
                            logicalDelete: currentTileVisually.LogicalDelete),
                        zoom: this.RoomsideDoors.ZoomProperties.Value
#if DEBUG
                        , hint: "[TileCropPageViewModel.cs CropTileSourceWidthAsInt 2]"
#endif
                        );
                }

                // 矩形カーソル。ズーム済み（カーソルの線の幅を含まない）
                CroppedCursorPointedTileWorkingWidthAsFloat = this.ZoomAsFloat * value;

                OnPropertyChanged(nameof(CropTileSourceWidthAsInt));
                OnPropertyChanged(nameof(CroppedCursorPointedTileSourceSize));
                OnPropertyChanged(nameof(CroppedCursorPointedTileSourceRect));
            }
        }

        /// <summary>
        ///     ［切抜きカーソルが指すタイル］の元画像ベースの縦幅
        /// </summary>
        public int CropTileSourceHeightAsInt
        {
            get
            {
                var contents = this.RoomsideDoors.CropTile.SavesRecordVisually;

                // ［切抜きカーソル］無し時
                if (contents.IsNone)
                    return 0;

                return contents.SourceRectangle.Size.Height.AsInt;
            }
            set
            {
                var currentTileVisually = this.RoomsideDoors.CropTile.SavesRecordVisually;

                if (currentTileVisually.IsNone)
                {
                    // ［切抜きカーソル］無し時
                    this.RoomsideDoors.CropTile.SavesRecordVisually = TileRecordVisually.FromModel(
                        tileRecord: new Models.TileRecord(
                            id: TileIdOrEmpty.Empty,
                            rect: new Models.Geometric.RectangleInt(Models.Geometric.PointInt.Empty, new Models.Geometric.SizeInt(Models.Geometric.WidthInt.Empty, new Models.Geometric.HeightInt(value))),
                            title: Models.TileTitle.Empty,
                            logicalDelete: Models.LogicalDelete.False),
                        zoom: this.RoomsideDoors.ZoomProperties.Value
#if DEBUG
                        , hint: "[TileCropPageViewModel.cs CropTileSourceHeightAsInt 1]"
#endif
                        );
                }
                else
                {
                    // 値に変化がない
                    if (currentTileVisually.SourceRectangle.Size.Height.AsInt == value)
                        return;

                    this.RoomsideDoors.CropTile.SavesRecordVisually = TileRecordVisually.FromModel(
                        tileRecord: new Models.TileRecord(
                            id: currentTileVisually.Id,
                            rect: new Models.Geometric.RectangleInt(currentTileVisually.SourceRectangle.Location, new Models.Geometric.SizeInt(currentTileVisually.SourceRectangle.Size.Width, new Models.Geometric.HeightInt(value))),
                            title: currentTileVisually.Title,
                            logicalDelete: currentTileVisually.LogicalDelete),
                        zoom: this.RoomsideDoors.ZoomProperties.Value
#if DEBUG
                        , hint: "[TileCropPageViewModel.cs CropTileSourceHeightAsInt 2]"
#endif
                        );
                }

                // 切抜きカーソル。ズーム済みの縦幅（カーソルの線の幅を含まない）
                CroppedCursorPointedTileWorkingHeightAsFloat = this.ZoomAsFloat * value;

                OnPropertyChanged(nameof(CropTileSourceHeightAsInt));
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
        public float CroppedCursorPointedTileWorkingLeftAsFloat => this.ZoomAsFloat * this.CropTileSourceLeftAsInt;

        /// <summary>
        ///     ［切抜きカーソルが指すタイル］のズーム済みの位置ｙ
        ///         
        ///     <list type="bullet">
        ///         <item>カーソルの線の幅を含まない</item>
        ///     </list>
        /// </summary>
        public float CroppedCursorPointedTileWorkingTopAsFloat => this.ZoomAsFloat * this.CropTileSourceTopAsInt;

        /// <summary>
        ///     ［切抜きカーソルが指すタイル］のズーム済みの横幅
        ///         
        ///     <list type="bullet">
        ///         <item>カーソルの線の幅を含む</item>
        ///         <item>切抜きカーソルは、対象範囲に外接する</item>
        ///     </list>
        /// </summary>
        public float CanvasOfCroppedCursorWorkingWidthAsFloat => this.RoomsideDoors.CropCursor.WorkingWidthWithoutTrick.AsFloat + (4 * this.HalfThicknessOfTileCursorLine.AsInt);

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

        /// <summary>
        ///     トリック幅
        ///     
        ///     <list type="bullet">
        ///         <item>透過メソッド</item>
        ///     </list>
        /// </summary>
        public Models.Geometric.WidthFloat TrickWidth
        {
            get => this.RoomsideDoors.CropCursor.TrickWidth;
            set => this.RoomsideDoors.CropCursor.TrickWidth = value;
        }

        public Models.Geometric.WidthFloat CroppedCursorPointedTileWorkingWidthWithTrick => new WidthFloat(this.RoomsideDoors.CropCursor.WorkingWidthWithoutTrick.AsFloat + this.TrickWidth.AsFloat);

        /// <summary>
        ///     <list type="bullet">
        ///         <item>透過メソッド</item>
        ///     </list>
        /// </summary>
        public Models.Geometric.WidthFloat CroppedCursorPointedTileWorkingWidthWithoutTrick
        {
            get => this.RoomsideDoors.CropCursor.WorkingWidthWithoutTrick;
            set => this.RoomsideDoors.CropCursor.WorkingWidthWithoutTrick = value;
        }

        public Models.Geometric.HeightFloat CroppedCursorPointedTileWorkingHeight
        {
            get => this.croppedCursorPointedTileWorkingHeight;
            set => this.croppedCursorPointedTileWorkingHeight = value;
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
            get => this.RoomsideDoors.CropCursor.WorkingWidthWithoutTrick.AsFloat;
            set
            {
                if (this.RoomsideDoors.CropCursor.WorkingWidthWithoutTrick.AsFloat != value)
                {
                    this.RoomsideDoors.CropCursor.WorkingWidthWithoutTrick = new Models.Geometric.WidthFloat(value);

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
        public string CroppedCursorPointedTileWorkingWidthAsPresentableText => this.RoomsideDoors.CropCursor.WorkingWidthWithoutTrick.AsFloat.ToString("F1");

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
                var contents = this.RoomsideDoors.CropTile.SavesRecordVisually;

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
        public string CropTileIdAsPhoneticCode
        {
            get
            {
                var contents = this.RoomsideDoors.CropTile.SavesRecordVisually;

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
        public bool IsEnabledCropTileTitleAsStr => !this.RoomsideDoors.CropTile.TargetTileRecordVisually.IsNone && !this.RoomsideDoors.CropTile.IdOrEmpty.IsEmpty && !this.RoomsideDoors.CropTile.TargetTileRecordVisually.LogicalDelete.AsBool;

        /// <summary>
        ///     ［切抜きカーソルが指すタイル］のタイトル
        /// </summary>
        public string CropTileTitleAsStr
        {
            get => this.RoomsideDoors.CropTile.SavesRecordVisually.Title.AsStr;
            set
            {
                if (this.RoomsideDoors.CropTile.SavesRecordVisually.Title.AsStr == value)
                    return;

                // 差分更新
                this.RoomsideDoors.CropTile.UpdateByDifference(
                    tileTitle: TileTitle.FromString(value));
            }
        }

        /// <summary>
        ///     ［切抜きカーソルが指すタイル］の論理削除
        /// </summary>
        public bool CropTileLogicalDeleteAsBool
        {
            get => this.RoomsideDoors.CropTile.SavesRecordVisually.LogicalDelete.AsBool;
            set
            {
                if (this.RoomsideDoors.CropTile.SavesRecordVisually.LogicalDelete.AsBool == value)
                    return;

                // 差分更新
                this.RoomsideDoors.CropTile.UpdateByDifference(
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
                var contents = this.RoomsideDoors.CropTile.SavesRecordVisually;

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
                var isEnabled = !this.RoomsideDoors.CropTile.TargetTileRecordVisually.IsNone && (
                // ※２
                (this.RoomsideDoors.CropTile.TargetTileRecordVisually.Id == TileIdOrEmpty.Empty && !this.RoomsideDoors.CropTile.TargetTileRecordVisually.LogicalDelete.AsBool)
                ||
                // ※３
                (this.RoomsideDoors.CropTile.TargetTileRecordVisually.Id != TileIdOrEmpty.Empty && this.RoomsideDoors.CropTile.TargetTileRecordVisually.LogicalDelete.AsBool));

                Trace.WriteLine($"[TileCropPageViewModel.cs IsEnabledAddsButton] this.CroppedCursorPointedTileRecordVisually.Dump(): {this.RoomsideDoors.CropTile.TargetTileRecordVisually.Dump()}");

                return isEnabled;
            }
        }
        #endregion

        #region 変更通知プロパティ（［削除］ボタン　関連）
        /// <summary>
        ///     ［削除］ボタンの活性性
        ///     
        ///     <list type="bullet">
        ///         <item>透過メソッド</item>
        ///     </list>
        /// </summary>
        public bool IsEnabledRoomsideDoorsDeletesButton
        {
            get => this.RoomsideDoors.DeletesButton.IsEnabled;
            set => this.RoomsideDoors.DeletesButton.IsEnabled = value;
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
            this.RoomsideDoors.TilesetSourceImageSize = Models.FileEntries.PNGHelper.GetImageSize(this.TilesetImageFile);
            OnPropertyChanged(nameof(TilesetSourceImageWidthAsInt));
            OnPropertyChanged(nameof(TilesetSourceImageHeightAsInt));

            // 作業画像の再作成
            this.Corridor.RemakeWorkingTilesetImage();

            // グリッド・キャンバス画像の再作成
            this.Corridor.RemakeGridCanvasImage();
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

        // - パブリック・メソッド

        /// <summary>
        ///     <pre>
        ///         ［元画像グリッド］のキャンバスの再描画
        /// 
        ///         TRICK:  GraphicsView を再描画させたいが、ビューモデルから要求する方法が分からない。
        ///                 そこで、内部的なグリッド画像の横幅が偶数のときは +1、奇数のときは -1 して
        ///                 振動させることで、再描画を呼び起こすことにする
        ///     </pre>
        /// </summary>
        public void RefreshForTileAdd()
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

            // タイル タイトル
            this.InvalidateTileTitle();

            // 追加・削除ボタンの表示状態を更新したい
            this.InvalidateAddsButton();

            // タイルセット作業画像
            this.InvalidateTilesetWorkingImage();
        }

        // - インターナル・プロパティ

        #region プロパティ（廊下モデル）
        /// <summary>
        ///     廊下モデル
        /// </summary>
        internal ItsCorridor Corridor { get; }

        /// <summary>
        ///     廊下側のドア
        /// </summary>
        ItsRoomsideDoors RoomsideDoors => this.Corridor.RoomsideDoors;
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

        internal void InvalidateIsMouseDragging()
        {
            OnPropertyChanged(nameof(IsMouseDragging));
        }

        #region 変更通知メソッド（［切抜きカーソルが指すタイル］　関連）
        /// <summary>
        ///     ［切抜きカーソルが指すタイル］
        /// </summary>
        internal void InvalidateTarget()
        {
            OnPropertyChanged(nameof(CroppedCursorPointedTileIdAsBASE64));
            OnPropertyChanged(nameof(CropTileIdAsPhoneticCode));
            OnPropertyChanged(nameof(CropTileTitleAsStr));
            OnPropertyChanged(nameof(CropTileLogicalDeleteAsBool));
        }

        /// <summary>
        ///     ［選択タイル］Ｉｄの再描画
        /// </summary>
        internal void InvalidateTileIdChange()
        {
            OnPropertyChanged(nameof(AddsButtonHint));
            OnPropertyChanged(nameof(AddsButtonText));
            OnPropertyChanged(nameof(IsEnabledCropTileTitleAsStr));

            OnPropertyChanged(nameof(CroppedCursorPointedTileIdAsBASE64));
            OnPropertyChanged(nameof(CropTileIdAsPhoneticCode));
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
            OnPropertyChanged(nameof(CropTileTitleAsStr));
            OnPropertyChanged(nameof(IsEnabledCropTileTitleAsStr));
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

        #region 変更通知メソッド（［削除］ボタン）
        /// <summary>
        ///     ［削除］ボタン
        /// </summary>
        internal void InvalidateDeletesButton()
        {
            OnPropertyChanged(nameof(IsEnabledRoomsideDoorsDeletesButton));
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

        // - プライベート変更通知フィールド

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

        #region 変更通知フィールド（［作業グリッド］　関連）
        /// <summary>
        ///     ［作業グリッド］の単位
        /// </summary>
        Models.Geometric.SizeFloat workingGridUnit = new(new Models.Geometric.WidthFloat(32.0f), new Models.Geometric.HeightFloat(32.0f));

        /// <summary>
        ///     ［作業グリッド］の位相の左上表示位置
        /// </summary>
        Models.Geometric.PointFloat workingGridPhase = Models.Geometric.PointFloat.Zero;
        #endregion

        #region 変更通知フィールド（［切抜きカーソル］　関連）
        /// <summary>
        ///     ［切抜きカーソル］の線の半分の太さ
        /// </summary>
        ThicknessOfLine halfThicknessOfTileCursorLine = new Models.ThicknessOfLine(0);
        #endregion

        #region 変更通知フィールド（［追加／上書き］ボタン　関連）
        /// <summary>
        ///     ［追加／上書き］ボタンのラベル
        /// </summary>
        string addsButtonText = string.Empty;
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
