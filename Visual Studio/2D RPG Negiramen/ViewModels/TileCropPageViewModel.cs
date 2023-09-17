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
    using TheGraphics = Microsoft.Maui.Graphics;
    using TheHierarchy = _2D_RPG_Negiramen.Hierarchy;
    using TheTileCropPage = _2D_RPG_Negiramen.Hierarchy.Pages.TileCrop;
    using TheHistoryTileCropPage = _2D_RPG_Negiramen.Hierarchy.HistoryOfPages.TileCrop;
    using _2D_RPG_Negiramen.Coding;

#if IOS || ANDROID || MACCATALYST
    using Microsoft.Maui.Graphics.Platform;
    using SkiaSharp.Views.Maui.Controls;
#elif WINDOWS
    using Microsoft.Maui.Graphics.Win2D;
    using SkiaSharp.Views.Maui.Controls;
    using Microsoft.Maui.Controls;
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
            this.Colleagues = new TheHierarchy.MembersOfTileCropPage(this);

            this.SetAddsButtonText = (text) =>
            {
                this.AddsButtonText = text;
                this.InvalidateAddsButton();
            };

            this.Subordinates = new TheTileCropPage.ItsMembers();

            // 循環参照しないように注意
            this.HalfThicknessOfTileCursorLine = new Models.ThicknessOfLine(2 * this.Subordinates.HalfThicknessOfGridLine.AsInt);
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
            get => this.Subordinates.InnerCultureInfo.Selected;
            set => this.Subordinates.InnerCultureInfo.SetSelected(
                value: value,
                doSetCultureInfoProcessing: (CultureInfo oldValue, CultureInfo newValue) =>
                {
                    LocalizationResourceManager.Instance.SetCulture(value);
                    this.InvalidateCultureInfo();

                    // 再帰的
                    App.History.Do(new TheHistoryTileCropPage.SetCultureInfoProcessing(
                        colleagues: this.Colleagues,    // 権限を委譲
                        oldValue: oldValue,
                        newValue: newValue));
                });
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
        public int TilesetSourceImageWidthAsInt => this.Subordinates.TilesetSourceImageSize.Width.AsInt;

        /// <summary>
        ///     ［タイルセット元画像］の縦幅。読取専用
        /// </summary>
        public int TilesetSourceImageHeightAsInt => this.Subordinates.TilesetSourceImageSize.Height.AsInt;
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
            get => this.Subordinates.ZoomProperties.AsFloat;
            set => this.SetZoomPropertiesAsFloat(
                value: value,
                doZoomProcessing: (Zoom oldValue, Zoom newValue) =>
                {
                    // TRICK CODE:
                    this.InvalidateWorkingTargetTile();

                    // 再帰的にズーム再変更、かつ変更後の影響を処理
                    App.History.Do(new TheHistoryTileCropPage.ZoomProcessing(
                        colleagues: this.Colleagues,    // 権限を委譲
                        subordinates: this.Subordinates,
                        oldValue: oldValue,
                        newValue: newValue));
                });
        }

        /// <summary>
        ///     ズーム最大
        ///     
        ///     <list type="bullet">
        ///         <item>透過メソッド</item>
        ///     </list>
        /// </summary>
        public float ZoomMaxAsFloat => this.Subordinates.ZoomProperties.MaxAsFloat;

        /// <summary>
        ///     ズーム最小
        ///     
        ///     <list type="bullet">
        ///         <item>透過メソッド</item>
        ///     </list>
        /// </summary>
        public float ZoomMinAsFloat => this.Subordinates.ZoomProperties.MinAsFloat;
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

        /// <summary>［元画像グリッド］の線の太さの半分</summary>
        public int HalfThicknessOfGridLineAsInt => this.Subordinates.HalfThicknessOfGridLine.AsInt;

        public void InvalidateHalfThicknessOfGridLineAsInt()
        {
            OnPropertyChanged(nameof(HalfThicknessOfGridLineAsInt));
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
            get => this.Subordinates.GridUnit.SourceValue;
            set
            {
                if (this.Subordinates.GridUnit.SourceValue != value)
                {
                    this.SourceGridTileWidthAsInt = value.Width.AsInt;
                    this.SourceGridTileHeightAsInt = value.Height.AsInt;
                }
            }
        }

        /// <summary>
        ///     変更通知プロパティ
        ///     ［元画像グリッド］のタイルの横幅。元画像ベース
        /// </summary>
        public int SourceGridTileWidthAsInt
        {
            get => this.Subordinates.GridUnit.SourceValue.Width.AsInt;
            set
            {
                if (this.Subordinates.GridUnit.SourceValue.Width.AsInt != value &&
                    // バリデーション
                    0 < value && value <= this.TileMaxWidthAsInt)
                {
                    this.Subordinates.GridUnit.SourceValue = new Models.Geometric.SizeInt(new Models.Geometric.WidthInt(value), this.Subordinates.GridUnit.SourceValue.Height);

                    // 作業グリッド・タイル横幅の再計算
                    var width = this.Subordinates.GridUnit.SourceValue.Width.AsInt;
                    this.WorkingGridTileWidthAsFloat = this.ZoomAsFloat * width;
                    // this.Owner.Owner.InvalidateWorkingGrid();

                    // カーソルの線の幅を含まない
                    this.CroppedCursorPointedTileWorkingWidthAsFloat = this.ZoomAsFloat * this.Subordinates.GridUnit.SourceValue.Width.AsInt;

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
            get => this.Subordinates.GridUnit.SourceValue.Height.AsInt;
            set
            {
                if (this.Subordinates.GridUnit.SourceValue.Height.AsInt != value &&
                    // バリデーション
                    0 < value && value <= this.TileMaxHeightAsInt)
                {
                    this.Subordinates.GridUnit.SourceValue = new Models.Geometric.SizeInt(this.Subordinates.GridUnit.SourceValue.Width, new Models.Geometric.HeightInt(value));

                    // 作業グリッド・タイル横幅の再計算
                    var height = this.Subordinates.GridUnit.SourceValue.Height.AsInt;
                    this.WorkingGridTileHeightAsFloat = this.ZoomAsFloat * height;
                    // this.Owner.Owner.InvalidateWorkingGrid();

                    // カーソルの線の幅を含まない
                    this.CroppedCursorPointedTileWorkingHeightAsFloat = this.ZoomAsFloat * this.Subordinates.GridUnit.SourceValue.Height.AsInt;

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
            get => this.Subordinates.PointingDevice.IsMouseDragging;
            set => this.Subordinates.PointingDevice.SetMouseDragging(
                value: value,
                onChanged: () =>
                {
                    this.InvalidateIsMouseDragging();
                });
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
                var contents = this.Subordinates.CropTile.RecordVisually;

                if (contents.IsNone)
                {
                    // ［切抜きカーソル］の指すタイル無し時
                    return Models.Geometric.RectangleInt.Empty;
                }

                return contents.SourceRectangle;
            }
            set
            {
                var contents = this.Subordinates.CropTile.RecordVisually;

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
                var contents = this.Subordinates.CropTile.RecordVisually;

                if (contents.IsNone)
                {
                    // ［切抜きカーソル］無し時
                    return 0;
                }

                return contents.SourceRectangle.Location.X.AsInt;
            }
            set
            {
                var currentTileVisually = this.Subordinates.CropTile.RecordVisually;

                if (currentTileVisually.IsNone)
                {
                    // ［切抜きカーソル］無し時

                    this.Subordinates.CropTile.SetRecordVisuallyNoGuiUpdate(TileRecordVisually.FromModel(
                        tileRecord: new Models.TileRecord(
                            id: Models.TileIdOrEmpty.Empty,
                            // 元画像ベース
                            rect: new Models.Geometric.RectangleInt(
                                location: new Models.Geometric.PointInt(new Models.Geometric.XInt(value), Models.Geometric.YInt.Empty),
                                size: Models.Geometric.SizeInt.Empty),
                            title: Models.TileTitle.Empty,
                            logicalDelete: Models.LogicalDelete.False),
                        zoom: this.Subordinates.ZoomProperties.Value
#if DEBUG
                        , hint: "[TileCropPageViewModel.cs CropTileSourceLeftAsInt 1]"
#endif
                        ));
                }
                else
                {
                    // 値に変化がない
                    if (currentTileVisually.SourceRectangle.Location.X.AsInt == value)
                        return;

                    this.Subordinates.CropTile.SetRecordVisuallyNoGuiUpdate(TileRecordVisually.FromModel(
                        tileRecord: new Models.TileRecord(
                            id: currentTileVisually.Id,
                            // 元画像ベース
                            rect: new Models.Geometric.RectangleInt(
                                location: new Models.Geometric.PointInt(new Models.Geometric.XInt(value), currentTileVisually.SourceRectangle.Location.Y),
                                size: currentTileVisually.SourceRectangle.Size),
                            title: currentTileVisually.Title,
                            logicalDelete: currentTileVisually.LogicalDelete),
                        zoom: this.Subordinates.ZoomProperties.Value
#if DEBUG
                        , hint: "[TileCropPageViewModel.cs CropTileSourceLeftAsInt 2]"
#endif
                        ));
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
                var contents = this.Subordinates.CropTile.RecordVisually;

                // ［切抜きカーソル］無し時
                if (contents.IsNone)
                    return 0;

                return contents.SourceRectangle.Location.Y.AsInt;
            }
            set
            {
                var currentTileVisually = this.Subordinates.CropTile.RecordVisually;

                if (currentTileVisually.IsNone)
                {
                    // ［切抜きカーソル］無し時

                    this.Subordinates.CropTile.SetRecordVisuallyNoGuiUpdate(TileRecordVisually.FromModel(
                        tileRecord: new Models.TileRecord(
                            id: Models.TileIdOrEmpty.Empty,
                            // 元画像ベース
                            rect: new Models.Geometric.RectangleInt(
                            location: new Models.Geometric.PointInt(Models.Geometric.XInt.Empty, new Models.Geometric.YInt(value)),
                            size: Models.Geometric.SizeInt.Empty),
                            title: Models.TileTitle.Empty,
                            logicalDelete: Models.LogicalDelete.False),
                        zoom: this.Subordinates.ZoomProperties.Value
#if DEBUG
                        , hint: "[TileCropPageViewModel.cs CropTileSourceTopAsInt 1]"
#endif
                        ));
                }
                else
                {
                    // 値に変化がない
                    if (currentTileVisually.SourceRectangle.Location.Y.AsInt == value)
                        return;

                    this.Subordinates.CropTile.SetRecordVisuallyNoGuiUpdate(TileRecordVisually.FromModel(
                        tileRecord: new Models.TileRecord(
                            id: currentTileVisually.Id,
                            // 元画像ベース
                            rect: new Models.Geometric.RectangleInt(
                            location: new Models.Geometric.PointInt(currentTileVisually.SourceRectangle.Location.X, new Models.Geometric.YInt(value)),
                            size: currentTileVisually.SourceRectangle.Size),
                            title: currentTileVisually.Title,
                            logicalDelete: currentTileVisually.LogicalDelete),
                        zoom: this.Subordinates.ZoomProperties.Value
#if DEBUG
                        , hint: "[TileCropPageViewModel.cs CropTileSourceTopAsInt 2]"
#endif
                        ));
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
                var contents = this.Subordinates.CropTile.RecordVisually;

                // ［切抜きカーソル］無し時
                if (contents.IsNone)
                    return Models.Geometric.SizeInt.Empty;

                return contents.SourceRectangle.Size;
            }
            set
            {
                var contents = this.Subordinates.CropTile.RecordVisually;

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
                var contents = this.Subordinates.CropTile.RecordVisually;

                // ［切抜きカーソル］無し時
                if (contents.IsNone)
                    return 0;

                return contents.SourceRectangle.Size.Width.AsInt;
            }
            set
            {
                var currentTileVisually = this.Subordinates.CropTile.RecordVisually;

                if (currentTileVisually.IsNone)
                {
                    // ［切抜きカーソル］無し時
                    // TODO ★ 循環参照注意
                    this.Subordinates.CropTile.SetRecordVisuallyNoGuiUpdate(TileRecordVisually.FromModel(
                        tileRecord: new Models.TileRecord(
                            id: Models.TileIdOrEmpty.Empty,
                            rect: new Models.Geometric.RectangleInt(Models.Geometric.PointInt.Empty, new Models.Geometric.SizeInt(new Models.Geometric.WidthInt(value), Models.Geometric.HeightInt.Empty)),
                            title: Models.TileTitle.Empty,
                            logicalDelete: Models.LogicalDelete.False),
                        zoom: this.Subordinates.ZoomProperties.Value
#if DEBUG
                        , hint: "[TileCropPageViewModel.cs CropTileSourceWidthAsInt 1]"
#endif
                        ));
                }
                else
                {
                    // 値に変化がない
                    if (currentTileVisually.SourceRectangle.Size.Width.AsInt == value)
                        return;

                    this.Subordinates.CropTile.SetRecordVisuallyNoGuiUpdate(TileRecordVisually.FromModel(
                        tileRecord: new Models.TileRecord(
                            id: currentTileVisually.Id,
                            rect: new Models.Geometric.RectangleInt(currentTileVisually.SourceRectangle.Location, new Models.Geometric.SizeInt(new Models.Geometric.WidthInt(value), currentTileVisually.SourceRectangle.Size.Height)),
                            title: currentTileVisually.Title,
                            logicalDelete: currentTileVisually.LogicalDelete),
                        zoom: this.Subordinates.ZoomProperties.Value
#if DEBUG
                        , hint: "[TileCropPageViewModel.cs CropTileSourceWidthAsInt 2]"
#endif
                        ));
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
                var contents = this.Subordinates.CropTile.RecordVisually;

                // ［切抜きカーソル］無し時
                if (contents.IsNone)
                    return 0;

                return contents.SourceRectangle.Size.Height.AsInt;
            }
            set
            {
                var currentTileVisually = this.Subordinates.CropTile.RecordVisually;

                if (currentTileVisually.IsNone)
                {
                    // ［切抜きカーソル］無し時
                    this.Subordinates.CropTile.SetRecordVisuallyNoGuiUpdate(TileRecordVisually.FromModel(
                        tileRecord: new Models.TileRecord(
                            id: TileIdOrEmpty.Empty,
                            rect: new Models.Geometric.RectangleInt(Models.Geometric.PointInt.Empty, new Models.Geometric.SizeInt(Models.Geometric.WidthInt.Empty, new Models.Geometric.HeightInt(value))),
                            title: Models.TileTitle.Empty,
                            logicalDelete: Models.LogicalDelete.False),
                        zoom: this.Subordinates.ZoomProperties.Value
#if DEBUG
                        , hint: "[TileCropPageViewModel.cs CropTileSourceHeightAsInt 1]"
#endif
                        ));
                }
                else
                {
                    // 値に変化がない
                    if (currentTileVisually.SourceRectangle.Size.Height.AsInt == value)
                        return;

                    this.Subordinates.CropTile.SetRecordVisuallyNoGuiUpdate(TileRecordVisually.FromModel(
                        tileRecord: new Models.TileRecord(
                            id: currentTileVisually.Id,
                            rect: new Models.Geometric.RectangleInt(currentTileVisually.SourceRectangle.Location, new Models.Geometric.SizeInt(currentTileVisually.SourceRectangle.Size.Width, new Models.Geometric.HeightInt(value))),
                            title: currentTileVisually.Title,
                            logicalDelete: currentTileVisually.LogicalDelete),
                        zoom: this.Subordinates.ZoomProperties.Value
#if DEBUG
                        , hint: "[TileCropPageViewModel.cs CropTileSourceHeightAsInt 2]"
#endif
                        ));
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
        public float CanvasOfCroppedCursorWorkingWidthAsFloat => this.Subordinates.CropCursor.WorkingWidthWithoutTrick.AsFloat + (4 * this.HalfThicknessOfTileCursorLine.AsInt);

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
            get => this.Subordinates.CropCursor.TrickWidth;
            set => this.Subordinates.CropCursor.TrickWidth = value;
        }

        public Models.Geometric.WidthFloat CroppedCursorPointedTileWorkingWidthWithTrick => new WidthFloat(this.Subordinates.CropCursor.WorkingWidthWithoutTrick.AsFloat + this.TrickWidth.AsFloat);

        /// <summary>
        ///     <list type="bullet">
        ///         <item>透過メソッド</item>
        ///     </list>
        /// </summary>
        public Models.Geometric.WidthFloat CroppedCursorPointedTileWorkingWidthWithoutTrick
        {
            get => this.Subordinates.CropCursor.WorkingWidthWithoutTrick;
            set => this.Subordinates.CropCursor.WorkingWidthWithoutTrick = value;
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
            get => this.Subordinates.CropCursor.WorkingWidthWithoutTrick.AsFloat;
            set
            {
                if (this.Subordinates.CropCursor.WorkingWidthWithoutTrick.AsFloat != value)
                {
                    this.Subordinates.CropCursor.WorkingWidthWithoutTrick = new Models.Geometric.WidthFloat(value);

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
        public string CroppedCursorPointedTileWorkingWidthAsPresentableText => this.Subordinates.CropCursor.WorkingWidthWithoutTrick.AsFloat.ToString("F1");

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
                var contents = this.Subordinates.CropTile.RecordVisually;

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
                var contents = this.Subordinates.CropTile.RecordVisually;

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
        public bool IsEnabledCropTileTitleAsStr => !this.Subordinates.CropTile.RecordVisually.IsNone && !this.Subordinates.CropTile.IdOrEmpty.IsEmpty && !this.Subordinates.CropTile.RecordVisually.LogicalDelete.AsBool;

        /// <summary>
        ///     ［切抜きカーソルが指すタイル］のタイトル
        /// </summary>
        public string CropTileTitleAsStr
        {
            get => this.Subordinates.CropTile.RecordVisually.Title.AsStr;
            set
            {
                if (this.Subordinates.CropTile.RecordVisually.Title.AsStr == value)
                    return;

                // 差分更新
                this.UpdateByDifference(
                    setAddsButtonText: (text) =>
                    {
                        this.AddsButtonText = text;
                        this.InvalidateAddsButton();
                    },
                    onDeleteButtonEnableChanged: () =>
                    {
                        this.InvalidateDeletesButton();
                    },
                    tileTitle: TileTitle.FromString(value));

                // 変更通知を送る
                this.InvalidateTileIdChange();
            }
        }

        /// <summary>
        ///     ［切抜きカーソルが指すタイル］の論理削除
        /// </summary>
        public bool CropTileLogicalDeleteAsBool
        {
            get => this.Subordinates.CropTile.RecordVisually.LogicalDelete.AsBool;
            set
            {
                if (this.Subordinates.CropTile.RecordVisually.LogicalDelete.AsBool == value)
                    return;

                // 差分更新
                this.UpdateByDifference(
                    setAddsButtonText: (text) =>
                    {
                        this.AddsButtonText = text;
                        this.InvalidateAddsButton();
                    },
                    onDeleteButtonEnableChanged: () =>
                    {
                        this.InvalidateDeletesButton();
                    },
                    logicalDelete: LogicalDelete.FromBool(value));

                // 変更通知を送る
                this.InvalidateTileIdChange();
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
                var contents = this.Subordinates.CropTile.RecordVisually;

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
        ///     
        ///     ※２　［追加］ボタンは、３状態ある。以下の条件で活性
        ///     <list type="bullet">
        ///         <item>Ｉｄが未設定時、かつ、論理削除フラグがＯｆｆ</item>
        ///     </list>
        ///     
        ///     ※３　［復元］ボタンは、以下の条件で活性
        ///     <list type="bullet">
        ///         <item>Ｉｄが設定時、かつ、論理削除フラグがＯｎ</item>
        ///     </list>
        ///     
        ///     ※４　［交差中］ボタンは、常に不活性
        /// </summary>
        public bool IsEnabledAddsButton
        {
            get
            {
                // ※１
                if (this.Subordinates.CropTile.RecordVisually.IsNone)
                {
                    return false;
                }

                var addsButtonState = this.Subordinates.GetStateOfAddsButton();

                switch (addsButtonState)
                {
                    case TheTileCropPage.AddsButtonState.Adds:
                        // ※２
                        return this.Subordinates.CropTile.RecordVisually.Id == TileIdOrEmpty.Empty && !this.Subordinates.CropTile.RecordVisually.LogicalDelete.AsBool;

                    case TheTileCropPage.AddsButtonState.Restore:
                        // ※３
                        return this.Subordinates.CropTile.RecordVisually.Id != TileIdOrEmpty.Empty && this.Subordinates.CropTile.RecordVisually.LogicalDelete.AsBool;

                    default:
                        // ※４
                        return false;
                }
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
        public bool IsEnabledDeletesButton
        {
            get => this.Subordinates.DeletesButton.IsEnabled;
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
            this.Subordinates.TilesetSourceImageSize = Models.FileEntries.PNGHelper.GetImageSize(this.TilesetImageFile);
            OnPropertyChanged(nameof(TilesetSourceImageWidthAsInt));
            OnPropertyChanged(nameof(TilesetSourceImageHeightAsInt));

            // 作業画像の再作成
            this.RemakeWorkingTilesetImage();

            // グリッド・キャンバス画像の再作成
            this.RemakeGridCanvasImage();
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

        #region メソッド（［元画像グリッド］のキャンバスの再描画）
        /// <summary>
        ///     ［元画像グリッド］のキャンバスの再描画
        ///     
        ///     <list type="bullet">
        ///         <item>
        ///             <pre>
        ///                 ［元画像グリッド］のキャンバスの再描画
        /// 
        ///                 TRICK:  GraphicsView を再描画させたいが、ビューモデルから要求する方法が分からない。
        ///                         そこで、内部的なグリッド画像の横幅が偶数のときは +1、奇数のときは -1 して
        ///                         振動させることで、再描画を呼び起こすことにする
        ///             </pre>
        ///         </item>
        ///     </list>
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
        #endregion

        // - インターナル・プロパティ

        /// <summary>
        ///     メンバー・ネットワーク
        /// </summary>
        internal TheHierarchy.MembersOfTileCropPage Colleagues { get; }

        internal LazyArgs.Set<string> SetAddsButtonText { get; }

        /// <summary>
        ///     メンバー・ネットワーク
        /// </summary>
        internal TheTileCropPage.ItsMembers Subordinates { get; }

        #region メソッド（ズーム設定）
        /// <summary>
        ///     ズーム設定
        /// </summary>
        /// <param name="value"></param>
        /// <param name="doZoomProcessing"></param>
        internal void SetZoomPropertiesAsFloat(
            float value,
            TheTileCropPage.ZoomProperties.DoZoomProcessing doZoomProcessing)
        {
            if (this.Subordinates.ZoomProperties.AsFloat != value)
            {
                if (this.Subordinates.ZoomProperties.MinAsFloat <= value && value <= this.Subordinates.ZoomProperties.MaxAsFloat)
                {
                    Zoom oldValue = this.Subordinates.ZoomProperties.Value;
                    Zoom newValue = new Zoom(value);

                    this.Subordinates.ZoomProperties.Value = newValue;

                    this.Subordinates.CropCursor.RefreshCanvasTrick("[TileCropPageViewModel.cs ZoomAsFloat]");

                    // 再帰的にズーム再変更、かつ変更後の影響を処理
                    doZoomProcessing(
                        oldValue: oldValue,
                        newValue: newValue);
                }
            }
        }
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

        #region 変更通知メソッド（マウス・ドラッグ中）
        /// <summary>
        ///     マウス・ドラッグ中
        /// </summary>
        internal void InvalidateIsMouseDragging()
        {
            OnPropertyChanged(nameof(IsMouseDragging));
        }
        #endregion

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
            OnPropertyChanged(nameof(IsEnabledDeletesButton));
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

        #region 変更通知メソッド（ロケール変更による再描画）
        /// <summary>
        ///     ロケール変更による再描画
        ///     
        ///     <list type="bullet">
        ///         <item>動的にテキストを変えている部分に対応するため</item>
        ///     </list>
        /// </summary>
        internal void InvalidateByLocale() => this.SetAddsButtonText(this.Subordinates.GetLabelOfAddsButton());
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
                tilesetDatatableFileLocation: this.TilesetDatatableFileLocation,
                zoom: this.Subordinates.ZoomProperties.Value,
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
                            this.SetTilesetSourceBitmap(SKBitmap.Decode(memStream));

                            // 複製
                            this.TilesetWorkingBitmap = SKBitmap.FromImage(SKImage.FromBitmap(this.TilesetSourceBitmap));

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

        #region イベントハンドラ（タイルセット画像上でタップ時）
        /// <summary>
        ///     タイルセット画像上でタップ時
        /// </summary>
        /// <param name="tappedPoint"></param>
        internal void OnTilesetImageTapped(Point tappedPoint)
        {
            // 反転
            this.IsMouseDragging = !this.IsMouseDragging;

            if (this.IsMouseDragging)
            {
                //
                // 疑似マウス・ダウン
                // ==================
                //
                Trace.WriteLine("［操作］　疑似マウス・ダウン");

                // ポイントしている位置
                this.Subordinates.PointingDevice.CurrentPoint = this.Subordinates.PointingDevice.StartPoint = new PointFloat(
                    new XFloat((float)tappedPoint.X),
                    new YFloat((float)tappedPoint.Y));
                // Trace.WriteLine($"[TileCropPage TileImage_OnTapped] tapped x:{PointingDeviceStartPoint.X.AsInt} y:{PointingDeviceStartPoint.Y.AsInt}");

                // タイル・フォームの表示更新
                this.RefreshTileForm(
                    mouseDrawingOperationState: MouseDrawingOperationState.ButtonDown);

                this.Subordinates.CropCursor.RefreshCanvasTrick(codePlace: "[TileCropPage.xml.cs TileImage_OnTapped 疑似マウスダウン]");
                // TRICK CODE:
                this.InvalidateWorkingTargetTile();
            }
            else
            {
                //
                // 疑似マウス・アップ
                // ==================
                //

                Trace.WriteLine("［操作］　疑似マウス・アップ");

                // ポイントしている位置
                this.Subordinates.PointingDevice.CurrentPoint = new PointFloat(
                    new XFloat((float)tappedPoint.X),
                    new YFloat((float)tappedPoint.Y));
                // Trace.WriteLine($"[TileCropPage PointerGestureRecognizer_PointerExited] exited x:{PointingDeviceCurrentPoint.X.AsInt} y:{PointingDeviceCurrentPoint.Y.AsInt}");

                // タイル・フォームの表示更新
                this.RefreshTileForm(
                    mouseDrawingOperationState: MouseDrawingOperationState.ButtonUp);

                this.Subordinates.CropCursor.RefreshCanvasTrick(codePlace: "[TileCropPage.xml.cs TileImage_OnTapped 疑似マウスアップ]");
                // TRICK CODE:
                this.InvalidateWorkingTargetTile();
            }
        }
        #endregion

        #region イベントハンドラ（タイルセット画像上でポインター移動）
        /// <summary>
        ///     タイルセット画像上でポインター移動
        /// </summary>
        /// <param name="tappedPoint"></param>
        internal void OnTilesetImagePointerMove(Point tappedPoint)
        {
            if (this.IsMouseDragging)
            {
                //
                // 疑似マウス・ドラッグ
                // ====================
                //

                // ポイントしている位置
                this.Subordinates.PointingDevice.CurrentPoint = new PointFloat(
                    new XFloat((float)tappedPoint.X),
                    new YFloat((float)tappedPoint.Y));
                // Trace.WriteLine($"[TileCropPage PointerGestureRecognizer_PointerMoved] moved x:{PointingDeviceCurrentPoint.X.AsInt} y:{PointingDeviceCurrentPoint.Y.AsInt}");

                // タイル・フォームの表示更新
                this.RefreshTileForm(
                    mouseDrawingOperationState: MouseDrawingOperationState.PointerMove);

                this.Subordinates.CropCursor.RefreshCanvasTrick(codePlace: "[TileCropPage.xml.cs PointerGestureRecognizer_PointerMoved 疑似マウスドラッグ]");
                // TRICK CODE:
                this.InvalidateWorkingTargetTile();
            }
        }
        #endregion

        #region イベントハンドラ（［追加］ボタン　クリック時）
        /// <summary>
        ///     ［追加］ボタン　クリック時
        /// </summary>
        internal void OnAddsButtonClicked()
        {
            if (this.Subordinates.CropTile.IdOrEmpty == TileIdOrEmpty.Empty)
            {
                // Ｉｄが空欄
                // ［追加］（新規作成）だ

                // 操作対象のタイル
                TileRecordVisually targetTile = this.Subordinates.CropTile.RecordVisually;

                //
                // 登録タイル追加
                // ==============
                //

                // Ｉｄが空欄
                // ［追加］（新規作成）だ

                // ［切抜きカーソル］にサイズがなければ、何もしない
                if (targetTile.IsNone)
                    return;

                // 新しいタイルＩｄを発行
                TileIdOrEmpty tileIdOrEmpty = this.TilesetSettingsVM.UsableId;
                this.TilesetSettingsVM.IncreaseUsableId();

                // 追加でも、上書きでも、同じ処理でいける
                // ［登録タイル追加］処理
                App.History.Do(new TheHistoryTileCropPage.AddRegisteredTileProcessing(
                    colleagues: this.Colleagues,
                    subordinates: this.Subordinates,
                    croppedCursorVisually: targetTile,
                    tileIdOrEmpty: tileIdOrEmpty,
                    workingRectangle: targetTile.SourceRectangle.Do(this.Subordinates.ZoomProperties.Value)));

                this.InvalidateForHistory();
            }
            else
            {
                // 上書きボタンだが、［上書き］処理をする
                this.OverwriteTile();
            }
        }
        #endregion

        /// <summary>
        ///     ［削除］ボタン　クリック時
        ///     
        ///     <list type="bullet">
        ///         <item>登録タイル削除</item>
        ///     </list>
        /// </summary>
        internal void OnDeletesButtonRemoveTile() => App.History.Do(new TheHistoryTileCropPage.RemoveRegisteredTileProcessing(
            colleagues: this.Colleagues,
            tileIdOrEmpty: this.Subordinates.CropTile.IdOrEmpty));

        // - インターナル・メソッド

        #region メソッド（上書きボタンだが、［上書き］処理をする）
        /// <summary>
        ///     上書きボタンだが、［上書き］処理をする
        /// </summary>
        internal void OverwriteTile()
        {
            // 操作対象のタイル
            TileRecordVisually targetTile = this.Subordinates.CropTile.RecordVisually;

            TileIdOrEmpty tileIdOrEmpty;

            // ［切抜きカーソル］にサイズがなければ、何もしない
            if (targetTile.IsNone)
                return;

            // Ｉｄが空欄でない
            // ［上書き］（更新）だ
            tileIdOrEmpty = this.Subordinates.CropTile.IdOrEmpty;

            // 追加でも、上書きでも、同じ処理でいける
            // ［登録タイル追加］処理
            App.History.Do(new TheHistoryTileCropPage.AddRegisteredTileProcessing(
                // 上位の権限を委譲する
                colleagues: this.Colleagues,
                subordinates: this.Subordinates,
                croppedCursorVisually: targetTile,
                tileIdOrEmpty: tileIdOrEmpty,
                workingRectangle: targetTile.SourceRectangle.Do(this.Subordinates.ZoomProperties.Value)));

            this.InvalidateForHistory();
        }
        #endregion

        #region メソッド（タイル・フォーム更新）
        /// <summary>
        ///     タイル・フォーム更新
        /// </summary>
        internal void RefreshTileForm(
            MouseDrawingOperationState mouseDrawingOperationState)
        {
            //
            // ポインティング・デバイスの２箇所のタップ位置から、タイルの矩形を算出
            // ====================================================================
            //

            // ズームしたままの矩形
            RectangleFloat workingRect = CoordinateHelper.GetCursorRectangle(
                startPoint: this.Subordinates.PointingDevice.StartPoint,
                endPoint: this.Subordinates.PointingDevice.CurrentPoint,
                gridLeftTop: this.WorkingGridPhase,
                gridTile: this.WorkingGridUnit);

            // ズームを除去した矩形
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
            this.CroppedCursorPointedTileSourceRect = sourceRect;
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
            this.RecalculateBetweenCropCursorAndRegisteredTile();

            //
            // 切抜きカーソル更新
            // ==================
            //
            this.TilesetSettingsVM.MatchByRectangle(
                sourceRect: this.CroppedCursorPointedTileSourceRect,
                some: (tileVisually) =>
                {
                    if (mouseDrawingOperationState == MouseDrawingOperationState.ButtonUp)
                    {
                        Trace.WriteLine($"［選択タイル調査］　タイル登録済　Id:{tileVisually.Id.AsInt}, {tileVisually.Id.AsBASE64} Title:{tileVisually.Title.AsStr}");
                    }

                    void onDeleteButtonEnableChanged()
                    {
                        this.InvalidateDeletesButton();
                    }

                    // タイルを指す（論理削除されているものも含む）
                    this.Subordinates.CropTile.SetRecordVisually(
                        tileVisually,
                        onVanished: () =>
                        {
                            Debug.Fail("ここには来ない");
                        },
                        onUpdated: () =>
                        {
                            // （変更通知を送っている）
                            this.CropTileSourceLeftAsInt = tileVisually.SourceRectangle.Location.X.AsInt;
                            this.CropTileSourceTopAsInt = tileVisually.SourceRectangle.Location.Y.AsInt;
                            this.CropTileSourceWidthAsInt = tileVisually.SourceRectangle.Size.Width.AsInt;
                            this.CropTileSourceHeightAsInt = tileVisually.SourceRectangle.Size.Height.AsInt;

                            // 変更通知を送りたい
                            this.InvalidateTileIdChange();
                        },
                        onUpdateByDifference: (TileTitle tileTitle) =>
                        {
                            UpdateByDifference(
                                setAddsButtonText: this.SetAddsButtonText,
                                onDeleteButtonEnableChanged: onDeleteButtonEnableChanged,
                                // タイトル
                                tileTitle: tileTitle);
                        },
                        onTileIdOrEmpty: (TileIdOrEmpty tileIdOrEmpty) =>
                        {
                            UpdateByDifference(
                                setAddsButtonText: this.SetAddsButtonText,
                                onDeleteButtonEnableChanged: onDeleteButtonEnableChanged,
                                tileId: tileIdOrEmpty);
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

                    var sourceRectangle = this.CroppedCursorPointedTileSourceRect;

                    void onDeleteButtonEnableChanged()
                    {
                        this.InvalidateDeletesButton();
                    }

                    // 選択中のタイルの矩形だけ維持し、タイル・コードと、コメントを空欄にする
                    this.Subordinates.CropTile.SetRecordVisually(TileRecordVisually.FromModel(
                        tileRecord: new TileRecord(
                            id: TileIdOrEmpty.Empty,
                            rect: sourceRectangle,
                            title: TileTitle.Empty,
                            logicalDelete: LogicalDelete.False),
                        zoom: this.Subordinates.ZoomProperties.Value
#if DEBUG
                        , hint: "[TileCropPageViewModel.cs LoadCroppedCursorPointedTile]"
#endif
                        ),
                        onVanished: () =>
                        {
                            Debug.Fail("ここには来ない");
                            // 元画像の位置とサイズ
                            this.CroppedCursorPointedTileSourceRect = RectangleInt.Empty;

                            // 論理削除
                            this.CropTileLogicalDeleteAsBool = false;

                            // 変更通知を送りたい
                            this.InvalidateTileIdChange();
                        },
                        onUpdated: () =>
                        {
                            // ここにはくる

                            // （変更通知を送っている）
                            this.CropTileSourceLeftAsInt = sourceRectangle.Location.X.AsInt;
                            this.CropTileSourceTopAsInt = sourceRectangle.Location.Y.AsInt;
                            this.CropTileSourceWidthAsInt = sourceRectangle.Size.Width.AsInt;
                            this.CropTileSourceHeightAsInt = sourceRectangle.Size.Height.AsInt;

                            // 変更通知を送りたい
                            this.InvalidateTileIdChange();
                        },
                        onUpdateByDifference: (TileTitle tileTitle) =>
                        {
                            UpdateByDifference(
                                setAddsButtonText: this.SetAddsButtonText,
                                onDeleteButtonEnableChanged: onDeleteButtonEnableChanged,
                                // タイトル
                                tileTitle: tileTitle);
                        },
                        onTileIdOrEmpty: (TileIdOrEmpty tileIdOrEmpty) =>
                        {
                            UpdateByDifference(
                                setAddsButtonText: this.SetAddsButtonText,
                                onDeleteButtonEnableChanged: onDeleteButtonEnableChanged,
                                tileId: tileIdOrEmpty);
                        });
                },
                // 論理削除されているものも選択できることとする（復元、論理削除の解除のため）
                includeLogicalDelete: true);

            // （切抜きカーソル更新後）［追加／上書き］ボタン再描画
            this.SetAddsButtonText(this.Subordinates.GetLabelOfAddsButton());

            // （切抜きカーソル更新後）［削除］ボタン活性化
            this.Subordinates.DeletesButtonRefreshEnabled(
                onEnableChanged: () =>
                {
                    this.InvalidateDeletesButton();
                });

            // ［追加／復元］ボタン
            this.InvalidateAddsButton();

            // タイル・タイトル
            this.InvalidateTileTitle();
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
        internal void RecalculateBetweenCropCursorAndRegisteredTile()
        {
            if (this.CroppedCursorPointedTileSourceRect == RectangleInt.Empty)
            {
                // カーソルが無ければ、交差も無い。合同ともしない
                this.Subordinates.HasIntersectionBetweenCroppedCursorAndRegisteredTile = false;
                this.Subordinates.IsCongruenceBetweenCroppedCursorAndRegisteredTile = false;
                return;
            }

            // 軽くはない処理
            this.Subordinates.HasIntersectionBetweenCroppedCursorAndRegisteredTile = this.TilesetSettingsVM.HasIntersection(this.CroppedCursorPointedTileSourceRect);
            this.Subordinates.IsCongruenceBetweenCroppedCursorAndRegisteredTile = this.TilesetSettingsVM.IsCongruence(this.CroppedCursorPointedTileSourceRect);
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
            var temporaryBitmap = SKBitmap.FromImage(SKImage.FromBitmap(this.TilesetSourceBitmap));

            // 画像処理（明度を下げる）
            FeatSkia.ReduceBrightness.DoItInPlace(temporaryBitmap);

            // 作業画像のサイズ計算
            this.workingImageSize = new SizeInt(
                width: new WidthInt((int)(this.ZoomAsFloat * this.Subordinates.TilesetSourceImageSize.Width.AsInt)),
                height: new HeightInt((int)(this.ZoomAsFloat * this.Subordinates.TilesetSourceImageSize.Height.AsInt)));

            // 作業画像のリサイズ
            this.TilesetWorkingBitmap = temporaryBitmap.Resize(
                size: new SKSizeI(
                    width: this.workingImageSize.Width.AsInt,
                    height: this.workingImageSize.Height.AsInt),
                quality: SKFilterQuality.Medium);

            this.InvalidateTilesetWorkingImage();
        }
        #endregion

        #region メソッド（［切抜きカーソルが指すタイル］を差分更新）
        /// <summary>
        ///     ［切抜きカーソルが指すタイル］を差分更新
        /// </summary>
        /// <returns></returns>
        internal void UpdateByDifference(
            LazyArgs.Set<string> setAddsButtonText,
            Action onDeleteButtonEnableChanged,
            TileIdOrEmpty? tileId = null,
            TileTitle? tileTitle = null,
            LogicalDelete? logicalDelete = null)
        {
            var currentTileVisually = this.Subordinates.CropTile.RecordVisually;

            // タイルＩｄ
            if (!(tileId is null) && currentTileVisually.Id != tileId)
            {
                this.Subordinates.CropTile.RecordVisually.Id = tileId;

                // Ｉｄが入ることで、タイル登録扱いになる。いろいろ再描画する

                // ［追加／上書き］ボタン再描画
                setAddsButtonText(this.Subordinates.GetLabelOfAddsButton());

                // ［削除］ボタン再描画
                this.Subordinates.DeletesButtonRefreshEnabled(
                    onEnableChanged: onDeleteButtonEnableChanged);
            }

            // タイル・タイトル
            if (!(tileTitle is null) && currentTileVisually.Title != tileTitle)
            {
                this.Subordinates.CropTile.RecordVisually.Title = tileTitle;
            }

            // 論理削除フラグ
            if (!(logicalDelete is null) && currentTileVisually.LogicalDelete != logicalDelete)
            {
                this.Subordinates.CropTile.RecordVisually.LogicalDelete = logicalDelete;
            }

            // Trace.WriteLine($"[CropTile.cs UpdateByDifference] SavesRecordVisually.Dump(): {this.SavesRecordVisually.Dump()}");
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

        #region メソッド（画面遷移でこの画面に戻ってきた時）
        /// <summary>
        ///     画面遷移でこの画面に戻ってきた時
        /// </summary>
        void ReactOnVisited()
        {
            // ロケールが変わってるかもしれないので反映
            this.InvalidateCultureInfo();

            // グリッド・キャンバス
            {
                // グリッドの左上位置（初期値）
                this.GridPhaseSourceLocation = new PointInt(new XInt(0), new YInt(0));

                // グリッドのタイルサイズ（初期値）
                this.SourceGridUnit = new SizeInt(new WidthInt(32), new HeightInt(32));

                // グリッド・キャンバス画像の再作成
                this.RemakeGridCanvasImage();
            }
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
            this.GridCanvasImageSize = new SizeInt(
                width: new WidthInt((int)(this.Subordinates.ZoomProperties.AsFloat * this.Subordinates.TilesetSourceImageSize.Width.AsInt) + 2 * this.HalfThicknessOfGridLineAsInt),
                height: new HeightInt((int)(this.Subordinates.ZoomProperties.AsFloat * this.Subordinates.TilesetSourceImageSize.Height.AsInt) + 2 * this.HalfThicknessOfGridLineAsInt));
        }
        #endregion
    }
}
