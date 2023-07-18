namespace _2D_RPG_Negiramen.ViewModels
{
    using _2D_RPG_Negiramen.Coding;
    using _2D_RPG_Negiramen.Models;
    using CommunityToolkit.Mvvm.ComponentModel;
    using SkiaSharp;
    using System.Collections.ObjectModel;
    using System.Globalization;

    /// <summary>
    ///     😁 ［タイル切抜きページ］ビューモデル
    /// </summary>
    [QueryProperty(nameof(TilesetImageFile), queryId: "TilesetImageFile")]
    [QueryProperty(nameof(TilesetSettingsFile), queryId: "TilesetSettingsFile")]
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
            // this.AddsButtonText = (string)LocalizationResourceManager.Instance["Add"];
            // 循環参照しないように注意
            this.HalfThicknessOfTileCursorLine = new Models.ThicknessOfLine(2 * this.HalfThicknessOfGridLine.AsInt);
        }
        #endregion

        // - パブリック・プロパティ

        #region プロパティ（タイルセット設定）
        /// <summary>
        ///     タイルセット設定ファイルへのパス（文字列形式）
        /// </summary>
        public string TilesetSettingFilePathAsStr
        {
            get => _tilesetSettingsFile.Path.AsStr;
            set
            {
                if (value == null || String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException($"the {nameof(TilesetSettingFilePathAsStr)} must not be null or whitespace");
                }

                if (_tilesetSettingsFile.Path.AsStr != value)
                {
                    _tilesetSettingsFile = new Models.FileEntries.Locations.TilesetSettingsFile(
                        pathSource: FileEntryPathSource.FromString(value),
                        convert: (pathSource) => FileEntryPath.From(pathSource,
                                                                    replaceSeparators: true));
                }
            }
        }

        /// <summary>
        ///     タイルセット設定ファイルへのパス
        /// </summary>
        public Models.FileEntries.Locations.TilesetSettingsFile TilesetSettingsFile
        {
            get => _tilesetSettingsFile;
            set
            {
                if (_tilesetSettingsFile != value)
                {
                    _tilesetSettingsFile = value;
                }
            }
        }

        /// <summary>
        ///     タイルセット設定
        /// </summary>
        public Models.FileEntries.TilesetSettings TilesetSettings
        {
            get => this._tilesetSettings;
            set
            {
                if (this._tilesetSettings != value)
                {
                    this._tilesetSettings = value;
                    OnPropertyChanged(nameof(TilesetSettings));

                    // TODO これ要るか？ 再描画
                    NotifyTileIdChange();
                }
            }
        }
        #endregion

        #region プロパティ（タイルセット元画像）
        /// <summary>
        ///     タイルセット元画像ファイルへのパス
        /// </summary>
        public Models.FileEntries.Locations.TilesetImageFile TilesetImageFile
        {
            get => tilesetImageFile;
            set
            {
                if (tilesetImageFile != value)
                {
                    tilesetImageFile = value;
                }
            }
        }

        /// <summary>
        ///     タイルセット画像ファイルへのパス（文字列形式）
        /// </summary>
        public string TilesetImageFilePathAsStr
        {
            get => tilesetImageFile.Path.AsStr;
            set
            {
                if (tilesetImageFile.Path.AsStr != value)
                {
                    tilesetImageFile = new Models.FileEntries.Locations.TilesetImageFile(
                        pathSource: FileEntryPathSource.FromString(value),
                        convert: (pathSource) => FileEntryPath.From(pathSource,
                                                                    replaceSeparators: true));
                }
            }
        }

        // タイルセット元画像
        SKBitmap tilesetSourceBitmap = new SKBitmap();

        /// <summary>
        ///     タイルセット元画像
        /// </summary>
        public SKBitmap TilesetSourceBitmap
        {
            get => this.tilesetSourceBitmap;
        }

        /// <summary>
        ///     タイルセット元画像の設定
        /// </summary>
        /// <param name="bitmap"></param>
        public void SetTilesetSourceBitmap(SKBitmap bitmap)
        {
            if (this.tilesetSourceBitmap != bitmap)
            {
                this.tilesetSourceBitmap = bitmap;

                // タイルセット画像のサイズ設定（画像の再作成）
                this.tilesetSourceImageSize = Models.FileEntries.PNGHelper.GetImageSize(this.TilesetImageFile);
                OnPropertyChanged(nameof(TilesetSourceImageWidthAsInt));
                OnPropertyChanged(nameof(TilesetSourceImageHeightAsInt));

                // 作業画像の再作成
                this.RemakeWorkingImage();

                // グリッド・キャンバス画像の再作成
                this.RemakeGridCanvasImage();
            }
        }

        /// <summary>
        ///     タイルセット元画像のサイズ
        /// </summary>

/* プロジェクト '2D RPG Negiramen (net7.0-windows10.0.19041.0)' からのマージされていない変更
前:
        public Models.SizeInt TilesetSourceImageSize => tilesetSourceImageSize;
後:
        public SizeInt TilesetSourceImageSize => tilesetSourceImageSize;
*/
        public Models.Geometric.SizeInt TilesetSourceImageSize => tilesetSourceImageSize;
        #endregion

        #region プロパティ（タイルセット作業画像関連）
        /// <summary>
        ///     タイルセット作業画像ファイルへのパス（文字列形式）
        /// </summary>
        public string TilesetWorkingImageFilePathAsStr
        {
            get => App.GetOrLoadUserConfiguration().WorkingTilesetImageFile.Path.AsStr;
        }

        /// <summary>
        ///     タイルセット作業画像。ビットマップ形式
        /// </summary>
        public SKBitmap TilesetWorkingBitmap { get; set; } = new SKBitmap();
        #endregion

        #region プロパティ（ズーム）
        Models.Zoom zoom = Models.Zoom.IdentityElement;

        /// <summary>
        ///     ズーム
        ///     
        ///     <list type="bullet">
        ///         <item>セッターは画像を再生成する重たい処理なので、スパムしないように注意</item>
        ///     </list>
        /// </summary>
        public Models.Zoom Zoom
        {
            get => this.zoom;
            set
            {
                if (this.zoom != value)
                {
                    this.ZoomAsDouble = value.AsDouble;
                }
            }
        }

        // ズーム最大
        Models.Zoom zoomMax = new Models.Zoom(4);

        /// <summary>
        ///     ズーム最大
        /// </summary>
        public double ZoomMaxAsDouble
        {
            get => this.zoomMax.AsDouble;
        }

        // ズーム最小
        Models.Zoom zoomMin = new Models.Zoom(0.5);

        /// <summary>
        ///     ズーム最小
        /// </summary>
        public double ZoomMinAsDouble
        {
            get => this.zoomMin.AsDouble;
        }
        #endregion

        #region プロパティ（選択タイル）
        /// <summary>
        ///     選択タイル
        /// </summary>
        public Option<TileRecord> SelectedTileOption
        {
            get => this._selectedTileOption;
            set
            {
                Models.TileRecord newValue;

                if (this._selectedTileOption.TryGetValue(out TileRecord selectedTile))
                {
                    if (SelectedTileOption == value)
                    {
                        // 値に変化がない
                        return;
                    }

                    // 必ず取れる想定
                    if (!value.TryGetValue(out newValue))
                    {
                        throw new InvalidOperationException("none must not be specified");
                    }
                }
                else
                {
                    // タイル・カーソル無し時
                    newValue = Models.TileRecord.Empty;
                }

                // 変更通知を送りたいので、構成要素ごとに設定
                this.SelectedTileId = newValue.Id;
                this.SelectedTileLeftAsInt = newValue.Rectangle.Point.X.AsInt;
                this.SelectedTileTopAsInt = newValue.Rectangle.Point.Y.AsInt;
                this.SelectedTileWidthAsInt = newValue.Rectangle.Size.Width.AsInt;
                this.SelectedTileHeightAsInt = newValue.Rectangle.Size.Height.AsInt;
                this.SelectedTileCommentAsStr = newValue.Comment.AsStr;

                this.RefreshCanvasOfTileCursor(codePlace: "[TileCropPageViewModel SelectedTileOption set]");
                OnPropertyChanged(nameof(AddsButtonHint));
                OnPropertyChanged(nameof(AddsButtonText));
                this.NotifyTileIdChange();
                // TODO 矩形もリフレッシュしたい
                // TODO コメントもリフレッシュしたい
            }
        }

        /// <summary>
        ///     選択タイルＩｄ
        /// </summary>
        public Models.TileId SelectedTileId
        {
            get
            {
                if (this._selectedTileOption.TryGetValue(out TileRecord selectedTile))
                {
                    return selectedTile.Id;
                }
                else
                {
                    // タイル・カーソル無し時
                    return Models.TileId.Empty;
                }
            }
            set
            {
                if (this._selectedTileOption.TryGetValue(out TileRecord selectedTile))
                {
                    if (selectedTile.Id == value)
                    {
                        // 値に変化がない
                        return;
                    }

                    _selectedTileOption = new Option<TileRecord>(new Models.TileRecord(
                        id: value,
                        rectangle: selectedTile.Rectangle,
                        comment: selectedTile.Comment,
                        logicalDelete: selectedTile.LogicalDelete));
                }
                else
                {
                    // タイル・カーソル無し時
                    _selectedTileOption = new Option<TileRecord>(new Models.TileRecord(
                        id: value,

/* プロジェクト '2D RPG Negiramen (net7.0-windows10.0.19041.0)' からのマージされていない変更
前:
                        rectangle: Models.RectangleInt.Empty,
後:
                        rectangle: RectangleInt.Empty,
*/
                        rectangle: Models.Geometric.RectangleInt.Empty,
                        comment: Models.Comment.Empty,
                        logicalDelete: Models.LogicalDelete.False));
                }

                this.RefreshByLocaleChanged();

                if (this._selectedTileOption.TryGetValue(out var record))
                {
                    if (record.Id == TileId.Empty)
                    {
                        // 未選択時
                        this.AddsButtonIsEnabled = true;
                        this.DeletesButtonIsEnabled = false;
                    }
                    else
                    {
                        // 「上書」
                        this.AddsButtonIsEnabled = true;
                        this.DeletesButtonIsEnabled = true;
                    }
                }
                else
                {
                    // タイル・カーソル無し時
                    this.AddsButtonIsEnabled = false;
                    this.DeletesButtonIsEnabled = false;
                }

                NotifyTileIdChange();
            }
        }
        #endregion

        // - パブリック変更通知プロパティ

        #region 変更通知プロパティ（ロケール関連）
        /// <summary>
        ///     現在選択中の文化情報。文字列形式
        /// </summary>
        public string CultureInfoAsStr
        {
            get
            {
                return LocalizationResourceManager.Instance.CultureInfo.Name;
            }
            set
            {
                if (LocalizationResourceManager.Instance.CultureInfo.Name != value)
                {
                    LocalizationResourceManager.Instance.SetCulture(new CultureInfo(value));
                    OnPropertyChanged(nameof(CultureInfoAsStr));
                }
            }
        }

        /// <summary>
        ///     ロケールＩｄのリスト
        /// </summary>
        public ObservableCollection<string> LocaleIdCollection => App.LocaleIdCollection;
        #endregion

        #region 変更通知プロパティ（タイルセット元画像関連）
        /// <summary>
        ///     タイルセット元画像の横幅。読取専用
        /// </summary>
        public int TilesetSourceImageWidthAsInt
        {
            get => tilesetSourceImageSize.Width.AsInt;
        }

        /// <summary>
        ///     タイルセット元画像の縦幅。読取専用
        /// </summary>
        public int TilesetSourceImageHeightAsInt
        {
            get => tilesetSourceImageSize.Height.AsInt;
        }
        #endregion

        #region 変更通知プロパティ（タイルセット作業画像関連）
        /// <summary>
        ///     タイルセット作業画像の横幅。読取専用
        /// </summary>
        public int TilesetWorkingImageWidthAsInt
        {
            get => workingImageSize.Width.AsInt;
        }

        /// <summary>
        ///     タイルセット作業画像の縦幅。読取専用
        /// </summary>
        public int TilesetWorkingImageHeightAsInt
        {
            get => workingImageSize.Height.AsInt;
        }
        #endregion

        #region 変更通知プロパティ（グリッド・キャンバス　関連）
        /// <summary>
        ///     グリッド・キャンバスの画像サイズ
        ///         
        ///     <list type="bullet">
        ///         <item>グリッドの線の太さを 2px と想定しているので、グリッドの線が画像の端っこで切れないように、グリッドの内部的なキャンバス・サイズを 2px 広げる</item>
        ///     </list>
        /// </summary>

/* プロジェクト '2D RPG Negiramen (net7.0-windows10.0.19041.0)' からのマージされていない変更
前:
        public Models.SizeInt GridCanvasImageSize
後:
        public SizeInt GridCanvasImageSize
*/
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
        ///     <pre>
        ///         グリッドのキャンバスの横幅
        ///         
        ///         グリッドの線の太さを 2px と想定しているので、グリッドの線が画像の端っこで切れないように、グリッドのキャンバス・サイズを 2px 広げる
        ///     </pre>
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
        ///     <pre>
        ///         グリッドのキャンバスの縦幅
        ///         
        ///         グリッドの線の太さを 2px と想定しているので、グリッドの線が画像の端っこで切れないように、グリッドのキャンバス・サイズを 2px 広げる
        ///     </pre>
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
        #endregion

        #region 変更通知プロパティ（グリッドの線の太さの半分）
        /// <summary>
        ///     グリッドの線の太さの半分
        /// </summary>
        public int HalfThicknessOfGridLineAsInt => this.HalfThicknessOfGridLine.AsInt;

        ThicknessOfLine halfThicknessOfGridLine = new Models.ThicknessOfLine(1);

        /// <summary>
        ///     グリッド線の半分の太さ
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
        #endregion

        #region 変更通知プロパティ（グリッド位相の左上表示位置）
        Models.Geometric.PointInt sourceGridPhase = Models.Geometric.PointInt.Empty;

        /// <summary>
        ///     グリッド位相の左上表示位置。元画像ベース
        /// </summary>
        public Models.Geometric.PointInt SourceGridPhase
        {
            get => this.sourceGridPhase;
            set
            {
                if (this.sourceGridPhase != value)
                {
                    this.SourceGridPhaseLeftAsInt = value.X.AsInt;
                    this.SourceGridPhaseTopAsInt = value.Y.AsInt;
                }
            }
        }

        /// <summary>
        ///     グリッド位相の左上表示位置ｘ。元画像ベース
        /// </summary>
        public int SourceGridPhaseLeftAsInt
        {
            get => this.sourceGridPhase.X.AsInt;
            set
            {
                if (this.sourceGridPhase.X.AsInt != value)
                {
                    this.sourceGridPhase = new Models.Geometric.PointInt(new Models.Geometric.XInt(value), this.sourceGridPhase.Y);

                    // キャンバスを再描画
                    InvalidateCanvasOfGrid();

                    // キャンバスを再描画後に変更通知
                    OnPropertyChanged(nameof(SourceGridPhaseLeftAsInt));
                    OnPropertyChanged(nameof(SourceGridPhase));

                    OnPropertyChanged(nameof(WorkingGridPhaseLeftAsInt));
                    OnPropertyChanged(nameof(WorkingGridPhase));
                }
            }
        }

        /// <summary>
        ///     グリッド位相の左上表示位置ｙ。元画像ベース
        /// </summary>
        public int SourceGridPhaseTopAsInt
        {
            get => this.sourceGridPhase.Y.AsInt;
            set
            {
                if (this.sourceGridPhase.Y.AsInt != value)
                {
                    this.sourceGridPhase = new Models.Geometric.PointInt(this.sourceGridPhase.X, new Models.Geometric.YInt(value));

                    // キャンバスを再描画
                    InvalidateCanvasOfGrid();

                    // キャンバスを再描画後に変更通知
                    OnPropertyChanged(nameof(SourceGridPhaseTopAsInt));
                    OnPropertyChanged(nameof(SourceGridPhase));

                    OnPropertyChanged(nameof(WorkingGridPhaseTopAsInt));
                    OnPropertyChanged(nameof(WorkingGridPhase));
                }
            }
        }

        /// <summary>
        ///     グリッド位相の左上表示位置。ズーム後
        /// </summary>

/* プロジェクト '2D RPG Negiramen (net7.0-windows10.0.19041.0)' からのマージされていない変更
前:
        public Models.PointInt WorkingGridPhase
後:
        public PointInt WorkingGridPhase
*/
        public Models.Geometric.PointInt WorkingGridPhase
        {
            get => new Models.Geometric.PointInt(
                x: new Models.Geometric.XInt(this.WorkingGridPhaseLeftAsInt),
                y: new Models.Geometric.YInt(this.WorkingGridPhaseTopAsInt));
        }

        /// <summary>
        ///     グリッド位相の左上表示位置ｘ。ズーム後（読取専用）
        /// </summary>
        public int WorkingGridPhaseLeftAsInt
        {
            get => (int)(this.sourceGridPhase.X.AsInt * this.ZoomAsDouble);
        }

        /// <summary>
        ///     グリッド位相の左上表示位置ｙ。ズーム後（読取専用）
        /// </summary>
        public int WorkingGridPhaseTopAsInt
        {
            get => (int)(this.sourceGridPhase.Y.AsInt * this.ZoomAsDouble);
        }
        #endregion

        #region 変更通知プロパティ（グリッド・タイルのサイズ　関連）
        /// <summary>
        ///     グリッド・タイルのサイズ。元画像ベース
        /// </summary>
        public Models.Geometric.SizeInt SourceGridTileSize
        {
            get => this.sourceGridTileSize;
            set
            {
                if (this.sourceGridTileSize != value)
                {
                    this.SourceGridTileWidthAsInt = value.Width.AsInt;
                    this.SourceGridTileHeightAsInt = value.Height.AsInt;
                }
            }
        }

        /// <summary>
        ///     グリッド・タイルの横幅。元画像ベース
        /// </summary>
        public int SourceGridTileWidthAsInt
        {
            get => this.sourceGridTileSize.Width.AsInt;
            set
            {
                if (this.sourceGridTileSize.Width.AsInt != value &&
                    // バリデーション
                    0 < value && value <= this.TileMaxWidthAsInt)
                {
                    this.sourceGridTileSize = new Models.Geometric.SizeInt(new Models.Geometric.WidthInt(value), this.sourceGridTileSize.Height);
                    this.WorkingGridTileWidthAsDouble = (double)(this.ZoomAsDouble * this.sourceGridTileSize.Width.AsInt);

                    // カーソルの線の幅が 4px なので、タイル・カーソルの画像サイズは + 8px にする
                    this.TileCursorCanvasWidthAsInt = this.sourceGridTileSize.Width.AsInt + 4 * this.HalfThicknessOfTileCursorLine.AsInt;

                    // キャンバスを再描画
                    InvalidateCanvasOfGrid();
                    RefreshCanvasOfTileCursor(codePlace: "[TileCropPageViewModel SourceGridTileWidthAsInt set]");

                    // キャンバスを再描画後に変更通知
                    OnPropertyChanged(nameof(SourceGridTileWidthAsInt));
                    OnPropertyChanged(nameof(SourceGridTileSize));

                    OnPropertyChanged(nameof(WorkingGridTileWidthAsDouble));
                    OnPropertyChanged(nameof(WorkingGridTileSize));
                }
            }
        }

        /// <summary>
        ///     グリッド・タイルの縦幅。元画像ベース
        /// </summary>
        public int SourceGridTileHeightAsInt
        {
            get => this.sourceGridTileSize.Height.AsInt;
            set
            {
                if (this.sourceGridTileSize.Height.AsInt != value &&
                    // バリデーション
                    0 < value && value <= this.TileMaxHeightAsInt)
                {
                    this.sourceGridTileSize = new Models.Geometric.SizeInt(this.sourceGridTileSize.Width, new Models.Geometric.HeightInt(value));

                    // カーソルの線の幅が 4px なので、タイル・カーソルの画像サイズは + 8px にする
                    this.TileCursorCanvasHeightAsInt = this.sourceGridTileSize.Height.AsInt + 4 * this.HalfThicknessOfTileCursorLine.AsInt;

                    // キャンバスを再描画
                    InvalidateCanvasOfGrid();
                    RefreshCanvasOfTileCursor(codePlace: "[TileCropPageViewModel SourceGridTileHeightAsInt set]");

                    // キャンバスを再描画後に変更通知
                    OnPropertyChanged(nameof(SourceGridTileHeightAsInt));
                    OnPropertyChanged(nameof(SourceGridTileSize));

                    OnPropertyChanged(nameof(WorkingGridTileHeightAsDouble));
                    OnPropertyChanged(nameof(WorkingGridTileSize));
                }
            }
        }

        /// <summary>
        ///     グリッド・タイルのサイズ。ズーム後（読取専用）
        /// </summary>
        public Models.Geometric.SizeDouble WorkingGridTileSize
        {
            get => this.workingGridTileSize;
            set
            {
                if (this.workingGridTileSize != value)
                {
                    this.WorkingGridTileWidthAsDouble = value.Width.AsDouble;
                    this.WorkingGridTileHeightAsDouble = value.Height.AsDouble;
                }
            }
        }

        /// <summary>
        ///     グリッド・タイルの横幅。ズーム後（読取専用）
        /// </summary>
        public double WorkingGridTileWidthAsDouble
        {
            get => this.workingGridTileSize.Width.AsDouble;
            set
            {
                if (this.workingGridTileSize.Width.AsDouble != value)
                {
                    this.workingGridTileSize = new Models.Geometric.SizeDouble(
                        width: new Models.Geometric.WidthDouble(value),
                        height: this.WorkingGridTileSize.Height);

                    OnPropertyChanged(nameof(WorkingGridTileWidthAsDouble));
                    OnPropertyChanged(nameof(WorkingGridTileSize));
                }
            }
        }

        /// <summary>
        ///     グリッド・タイルの縦幅。ズーム後（読取専用）
        /// </summary>
        public double WorkingGridTileHeightAsDouble
        {
            get => this.workingGridTileSize.Height.AsDouble;
            set
            {
                if (this.workingGridTileSize.Height.AsDouble != value)
                {
                    this.workingGridTileSize = new Models.Geometric.SizeDouble(
                        width: this.WorkingGridTileSize.Width,
                        height: new Models.Geometric.HeightDouble(value));

                    OnPropertyChanged(nameof(WorkingGridTileHeightAsDouble));
                    OnPropertyChanged(nameof(WorkingGridTileSize));
                }
            }
        }

        /// <summary>
        ///     グリッド・タイルの最大横幅
        /// </summary>
        public int TileMaxWidthAsInt
        {
            get => App.GetOrLoadSettings().TileMaxSize.Width.AsInt;
        }

        /// <summary>
        ///     グリッド・タイルの最大縦幅
        /// </summary>
        public int TileMaxHeightAsInt
        {
            get => App.GetOrLoadSettings().TileMaxSize.Height.AsInt;
        }
        #endregion

        #region 変更通知プロパティ（タイル・カーソルのキャンバス・サイズ）
        /// <summary>
        ///     <pre>
        ///         タイル・カーソルのキャンバス・サイズ
        ///         
        ///         カーソルの線の幅が 4px なので、キャンバス・サイズは + 8px にする
        ///     </pre>
        /// </summary>

/* プロジェクト '2D RPG Negiramen (net7.0-windows10.0.19041.0)' からのマージされていない変更
前:
        public Models.SizeInt TileCursorCanvasSize
後:
        public SizeInt TileCursorCanvasSize
*/
        public Models.Geometric.SizeInt TileCursorCanvasSize
        {
            get => _tileCursorCanvasSize;
            set
            {
                if (_tileCursorCanvasSize != value)
                {
                    this.TileCursorCanvasWidthAsInt = value.Width.AsInt;
                    this.TileCursorCanvasHeightAsInt = value.Height.AsInt;
                }
            }
        }
        #endregion

        #region 変更通知プロパティ（タイル・カーソルのキャンバスの横幅）
        /// <summary>
        ///     <pre>
        ///         タイル・カーソルのキャンバスの横幅
        ///         
        ///         カーソルの線の幅が 4px なので、画像サイズは + 8px にする
        ///     </pre>
        /// </summary>
        public int TileCursorCanvasWidthAsInt
        {
            get => _tileCursorCanvasSize.Width.AsInt;
            set
            {
                if (_tileCursorCanvasSize.Width.AsInt != value)
                {
                    _tileCursorCanvasSize = new Models.Geometric.SizeInt(new Models.Geometric.WidthInt(value), _tileCursorCanvasSize.Height);

                    // キャンバスを再描画
                    RefreshCanvasOfTileCursor(codePlace: "[TileCropPageViewModel TileCursorCanvasWidthAsInt set]");

                    // キャンバスを再描画後に変更通知
                    OnPropertyChanged(nameof(TileCursorCanvasWidthAsInt));
                }
            }
        }
        #endregion

        #region 変更通知プロパティ（タイル・カーソルのキャンバスの縦幅）
        /// <summary>
        ///     <pre>
        ///         タイル・カーソルのキャンバスの縦幅
        ///         
        ///         カーソルの線の幅が 4px なので、画像サイズは + 8px にする
        ///     </pre>
        /// </summary>
        public int TileCursorCanvasHeightAsInt
        {
            get => _tileCursorCanvasSize.Height.AsInt;
            set
            {
                if (_tileCursorCanvasSize.Height.AsInt != value)
                {
                    _tileCursorCanvasSize = new Models.Geometric.SizeInt(_tileCursorCanvasSize.Width, new Models.Geometric.HeightInt(value));

                    // キャンバスを再描画
                    RefreshCanvasOfTileCursor("[TileCropPageViewModel TileCursorCanvasHeightAsInt set]");

                    // キャンバスを再描画後に変更通知
                    OnPropertyChanged(nameof(TileCursorCanvasHeightAsInt));
                }
            }
        }
        #endregion

        #region 変更通知プロパティ（タイル・カーソルの位置）
        /// <summary>
        ///     タイル・カーソルの位置（マージンとして）
        /// </summary>
        public Thickness TileCursorPointAsMargin
        {
            get => _tileCursorPointAsMargin;
            set
            {
                if (_tileCursorPointAsMargin != value)
                {
                    _tileCursorPointAsMargin = value;
                    OnPropertyChanged(nameof(TileCursorPointAsMargin));
                }
            }
        }
        #endregion

        #region 変更通知プロパティ（ズーム）
        /// <summary>
        ///     ズーム。整数形式
        ///     
        ///     <list type="bullet">
        ///         <item>セッターは画像を再生成する重たい処理なので、スパムしないように注意</item>
        ///     </list>
        /// </summary>
        public double ZoomAsDouble
        {
            get => this.zoom.AsDouble;
            set
            {
                if (this.zoom.AsDouble != value)
                {
                    if (this.ZoomMinAsDouble <= value && value <= this.ZoomMaxAsDouble)
                    {
                        this.zoom = new Models.Zoom(value);

                        // 作業画像を再作成
                        this.RemakeWorkingImage();

                        // グリッド・キャンバス画像の再作成
                        this.RemakeGridCanvasImage();

                        OnPropertyChanged(nameof(ZoomAsDouble));
                        OnPropertyChanged(nameof(WorkingGridPhaseLeftAsInt));
                        OnPropertyChanged(nameof(WorkingGridPhaseTopAsInt));
                        OnPropertyChanged(nameof(WorkingGridPhase));

                        OnPropertyChanged(nameof(WorkingGridTileWidthAsDouble));
                        OnPropertyChanged(nameof(WorkingGridTileHeightAsDouble));
                        OnPropertyChanged(nameof(WorkingGridTileSize));
                    }
                }
            }
        }
        #endregion

        #region 変更通知プロパティ（追加／上書きボタンのラベル）
        string addsButtonText = string.Empty;

        /// <summary>
        ///     追加／上書きボタンのラベル
        /// </summary>
        public string AddsButtonText
        {
            get
            {
                return this.addsButtonText;
            }
            set
            {
                if (this.addsButtonText != value)
                {
                    this.addsButtonText = value;
                    OnPropertyChanged(nameof(AddsButtonText));
                }
            }
        }
        #endregion

        #region 変更通知プロパティ（追加／上書きボタンのツールチップ・ヒント）
        /// <summary>
        ///     追加／上書きボタンのツールチップ・ヒント
        /// </summary>
        public string AddsButtonHint
        {
            get
            {
                if (this._selectedTileOption.TryGetValue(out TileRecord selectedTile))
                {
                    if (selectedTile.Id == Models.TileId.Empty)
                    {
                        // 未選択時
                        return "選択タイルを、タイル一覧画面へ追加";
                    }
                    else
                    {
                        return "選択タイルを、タイル一覧画面へ上書";
                    }
                }
                else
                {
                    // タイル・カーソル無し時
                    return "選択タイルを、タイル一覧画面へ追加";
                }
            }
        }
        #endregion

        #region 変更通知プロパティ（追加／上書ボタンの活性性）
        bool addsButtonIsEnabled;

        /// <summary>
        ///     追加／上書ボタンの活性性
        /// </summary>
        public bool AddsButtonIsEnabled
        {
            get
            {
                return this.addsButtonIsEnabled;
            }
            set
            {
                if (this.addsButtonIsEnabled != value)
                {
                    this.addsButtonIsEnabled = value;
                    OnPropertyChanged(nameof(AddsButtonIsEnabled));
                }
            }
        }
        #endregion

        #region 変更通知プロパティ（削除ボタンの活性性）
        bool deletesButtonIsEnabled;

        /// <summary>
        ///     削除ボタンの活性性
        /// </summary>
        public bool DeletesButtonIsEnabled
        {
            get
            {
                return this.deletesButtonIsEnabled;
            }
            set
            {
                if (this.deletesButtonIsEnabled != value)
                {
                    this.deletesButtonIsEnabled = value;
                    OnPropertyChanged(nameof(DeletesButtonIsEnabled));
                }
            }
        }
        #endregion

        #region 変更通知プロパティ（タイル・カーソルの線の半分の太さ）
        ThicknessOfLine halfThicknessOfTileCursorLine;

        /// <summary>
        ///     タイル・カーソルの線の半分の太さ
        /// </summary>
        public ThicknessOfLine HalfThicknessOfTileCursorLine
        {
            get
            {
                return this.halfThicknessOfTileCursorLine;
            }
            set
            {
                if (this.halfThicknessOfTileCursorLine != value)
                {
                    this.halfThicknessOfTileCursorLine = value;
                    OnPropertyChanged(nameof(HalfThicknessOfTileCursorLine));
                }
            }
        }
        #endregion

        #region 変更通知プロパティ（ポインティング・デバイス押下中か？）
        bool selectingOnPointingDevice;

        /// <summary>
        ///     ポインティング・デバイス押下中か？
        /// 
        ///     <list type="bullet">
        ///         <item>タイルを選択開始していて、まだ未確定だ</item>
        ///     </list>
        /// </summary>
        public bool SelectingOnPointingDevice
        {
            get => this.selectingOnPointingDevice;
            set
            {
                if (this.selectingOnPointingDevice != value)
                {
                    this.selectingOnPointingDevice = value;
                    OnPropertyChanged(nameof(SelectingOnPointingDevice));
                }
            }
        }
        #endregion

        #region 変更通知プロパティ（選択タイルＩｄ。BASE64表現）
        /// <summary>
        ///     選択タイルＩｄ。BASE64表現
        ///     
        ///     <see cref="SelectedTileId"/>
        /// </summary>
        public string SelectedTileIdAsBASE64
        {
            get
            {
                if (this._selectedTileOption.TryGetValue(out TileRecord selectedTile))
                {
                    return selectedTile.Id.AsBASE64;
                }
                else
                {
                    // タイル・カーソル無し時
                    return string.Empty;
                }
            }
        }
        #endregion

        #region 変更通知プロパティ（選択タイルＩｄ。フォネティックコード表現）
        /// <summary>
        ///     選択タイルＩｄ。フォネティックコード表現
        ///     
        ///     <see cref="SelectedTileId"/>
        /// </summary>
        public string SelectedTileIdAsPhoneticCode
        {
            get
            {
                if (this._selectedTileOption.TryGetValue(out TileRecord selectedTile))
                {
                    return selectedTile.Id.AsPhoneticCode;
                }
                else
                {
                    // タイル・カーソル無し時
                    return string.Empty;
                }
            }
        }
        #endregion

        #region 変更通知プロパティ（選択タイルの矩形）
        /// <summary>
        ///     <pre>
        ///         選択タイルの矩形
        ///     </pre>
        /// </summary>

/* プロジェクト '2D RPG Negiramen (net7.0-windows10.0.19041.0)' からのマージされていない変更
前:
        public Models.RectangleInt SelectedTileRectangle
後:
        public RectangleInt SelectedTileRectangle
*/
        public Models.Geometric.RectangleInt SelectedTileRectangle
        {
            get
            {
                if (this._selectedTileOption.TryGetValue(out TileRecord selectedTile))
                {
                    return selectedTile.Rectangle;
                }
                else
                {
                    // タイル・カーソル無し時

/* プロジェクト '2D RPG Negiramen (net7.0-windows10.0.19041.0)' からのマージされていない変更
前:
                    return Models.RectangleInt.Empty;
後:
                    return RectangleInt.Empty;
*/
                    return Models.Geometric.RectangleInt.Empty;
                }
            }
            set
            {
                if (this._selectedTileOption.TryGetValue(out TileRecord selectedTile))
                {
                    if (selectedTile.Rectangle == value)
                    {
                        // 値に変化がない
                        return;
                    }
                }
                else
                {
                    // タイル・カーソル無し時
                }

                this.SelectedTileLeftAsInt = value.Point.X.AsInt;
                this.SelectedTileTopAsInt = value.Point.Y.AsInt;
                this.SelectedTileSize = value.Size;

                this.TileCursorPointAsMargin = new Thickness(
                    // 左
                    this.SelectedTileLeftAsInt,
                    // 上
                    this.SelectedTileTopAsInt,
                    // 右
                    0,
                    // 下
                    0);
            }
        }
        #endregion

        #region 変更通知プロパティ（選択タイルの位置ｘ）
        /// <summary>
        ///     <pre>
        ///         選択タイルの位置ｘ
        ///         タイル・カーソルの位置ｘ
        ///     </pre>
        /// </summary>
        public int SelectedTileLeftAsInt
        {
            get
            {
                if (this._selectedTileOption.TryGetValue(out TileRecord selectedTile))
                {
                    return selectedTile.Rectangle.Point.X.AsInt;
                }
                else
                {
                    // タイル・カーソル無し時
                    return 0;
                }
            }
            set
            {
                if (this._selectedTileOption.TryGetValue(out TileRecord selectedTile))
                {
                    if (selectedTile.Rectangle.Point.X.AsInt == value)
                    {
                        // 値に変化がない
                        return;
                    }

                    _selectedTileOption = new Option<TileRecord>(new Models.TileRecord(
                        id: selectedTile.Id,
                        rectangle: new Models.Geometric.RectangleInt(
                            point: new Models.Geometric.PointInt(new Models.Geometric.XInt(value), selectedTile.Rectangle.Point.Y),
                            size: selectedTile.Rectangle.Size),
                        comment: selectedTile.Comment,
                        logicalDelete: selectedTile.LogicalDelete));
                }
                else
                {
                    // タイル・カーソル無し時
                    _selectedTileOption = new Option<TileRecord>(new Models.TileRecord(
                        id: Models.TileId.Empty,
                        rectangle: new Models.Geometric.RectangleInt(

/* プロジェクト '2D RPG Negiramen (net7.0-windows10.0.19041.0)' からのマージされていない変更
前:
                            point: new Models.PointInt(new Models.XInt(value), Models.YInt.Empty),
後:
                            point: new Models.PointInt(new Models.XInt(value), YInt.Empty),
*/
                            point: new Models.Geometric.PointInt(new Models.Geometric.XInt(value), Models.Geometric.YInt.Empty),

/* プロジェクト '2D RPG Negiramen (net7.0-windows10.0.19041.0)' からのマージされていない変更
前:
                            size: Models.SizeInt.Empty),
後:
                            size: SizeInt.Empty),
*/
                            size: Models.Geometric.SizeInt.Empty),
                        comment: Models.Comment.Empty,
                        logicalDelete: Models.LogicalDelete.False));
                }

                this.TileCursorPointAsMargin = new Thickness(
                    // 左
                    this.SelectedTileLeftAsInt,
                    // 上
                    this.SelectedTileTopAsInt,
                    // 右
                    0,
                    // 下
                    0);

                OnPropertyChanged(nameof(SelectedTileLeftAsInt));
                OnPropertyChanged(nameof(SelectedTileRectangle));
            }
        }
        #endregion

        #region 変更通知プロパティ（選択タイルの位置ｙ）
        /// <summary>
        ///     <pre>
        ///         選択タイルの位置ｙ
        ///         タイル・カーソルの位置ｙ
        ///     </pre>
        /// </summary>
        public int SelectedTileTopAsInt
        {
            get
            {
                if (this._selectedTileOption.TryGetValue(out TileRecord selectedTile))
                {
                    return selectedTile.Rectangle.Point.Y.AsInt;
                }
                else
                {
                    // タイル・カーソル無し時
                    return 0;
                }
            }
            set
            {
                if (this._selectedTileOption.TryGetValue(out TileRecord selectedTile))
                {
                    if (selectedTile.Rectangle.Point.Y.AsInt == value)
                    {
                        // 値に変化がない
                        return;
                    }

                    _selectedTileOption = new Option<TileRecord>(new Models.TileRecord(
                        id: selectedTile.Id,
                        rectangle: new Models.Geometric.RectangleInt(
                            point: new Models.Geometric.PointInt(selectedTile.Rectangle.Point.X, new Models.Geometric.YInt(value)),
                            size: selectedTile.Rectangle.Size),
                        comment: selectedTile.Comment,
                        logicalDelete: selectedTile.LogicalDelete));
                }
                else
                {
                    // タイル・カーソル無し時
                    _selectedTileOption = new Option<TileRecord>(new Models.TileRecord(
                        id: Models.TileId.Empty,
                        rectangle: new Models.Geometric.RectangleInt(

/* プロジェクト '2D RPG Negiramen (net7.0-windows10.0.19041.0)' からのマージされていない変更
前:
                            point: new Models.PointInt(Models.XInt.Empty, new Models.YInt(value)),
後:
                            point: new Models.PointInt(XInt.Empty, new Models.YInt(value)),
*/
                            point: new Models.Geometric.PointInt(Models.Geometric.XInt.Empty, new Models.Geometric.YInt(value)),

/* プロジェクト '2D RPG Negiramen (net7.0-windows10.0.19041.0)' からのマージされていない変更
前:
                            size: Models.SizeInt.Empty),
後:
                            size: SizeInt.Empty),
*/
                            size: Models.Geometric.SizeInt.Empty),
                        comment: Models.Comment.Empty,
                        logicalDelete: Models.LogicalDelete.False));
                }

                this.TileCursorPointAsMargin = new Thickness(
                        // 左
                        this.SelectedTileLeftAsInt,
                        // 上
                        this.SelectedTileTopAsInt,
                        // 右
                        0,
                        // 下
                        0);

                OnPropertyChanged(nameof(SelectedTileTopAsInt));
                OnPropertyChanged(nameof(SelectedTileRectangle));
            }
        }
        #endregion

        #region 変更通知プロパティ（選択タイルのサイズ）

        /// <summary>
        ///     選択タイルのサイズ
        /// </summary>

/* プロジェクト '2D RPG Negiramen (net7.0-windows10.0.19041.0)' からのマージされていない変更
前:
        public Models.SizeInt SelectedTileSize
後:
        public SizeInt SelectedTileSize
*/
        public Models.Geometric.SizeInt SelectedTileSize
        {
            get
            {
                if (this._selectedTileOption.TryGetValue(out TileRecord selectedTile))
                {
                    return selectedTile.Rectangle.Size;
                }
                else
                {
                    // タイル・カーソル無し時

/* プロジェクト '2D RPG Negiramen (net7.0-windows10.0.19041.0)' からのマージされていない変更
前:
                    return Models.SizeInt.Empty;
後:
                    return SizeInt.Empty;
*/
                    return Models.Geometric.SizeInt.Empty;
                }
            }
            set
            {
                if (this._selectedTileOption.TryGetValue(out TileRecord selectedTile))
                {
                    if (selectedTile.Rectangle.Size == value)
                    {
                        // 値に変化がない
                        return;
                    }

                    //_selectedTileOption = new Option<TileRecord>(new Models.TileRecord(
                    //    id: selectedTile.Id,
                    //    rectangle: new Models.Rectangle(
                    //        point: selectedTile.Rectangle.Point,
                    //        size: value),
                    //    comment: selectedTile.Comment));
                }
                else
                {
                    // タイル・カーソル無し時

                    //_selectedTileOption = new Option<TileRecord>(new Models.TileRecord(
                    //    id: Models.TileId.Empty,
                    //    rectangle: new Models.Rectangle(
                    //        point: Models.Point.Empty,
                    //        size: value),
                    //    comment: Models.Comment.Empty));
                }

                //
                // 選択タイルの横幅と縦幅
                // ======================
                //
                this.SelectedTileWidthAsInt = value.Width.AsInt;
                this.SelectedTileHeightAsInt = value.Height.AsInt;

                ////
                //// タイル・カーソルのキャンバス・サイズ変更
                //// ========================================
                ////
                //this.TileCursorCanvasWidthAsInt = value.Width.AsInt;
                //this.TileCursorCanvasHeightAsInt = value.Height.AsInt;

                OnPropertyChanged(nameof(SelectedTileRectangle));
            }
        }
        #endregion

        #region 変更通知プロパティ（選択タイルの横幅）
        /// <summary>
        ///     選択タイルの横幅
        /// </summary>
        public int SelectedTileWidthAsInt
        {
            get
            {
                if (this._selectedTileOption.TryGetValue(out TileRecord selectedTile))
                {
                    return selectedTile.Rectangle.Size.Width.AsInt;
                }
                else
                {
                    // タイル・カーソル無し時
                    return 0;
                }
            }
            set
            {
                if (this._selectedTileOption.TryGetValue(out TileRecord selectedTile))
                {
                    if (selectedTile.Rectangle.Size.Width.AsInt == value)
                    {
                        // 値に変化がない
                        return;
                    }

                    _selectedTileOption = new Option<TileRecord>(new Models.TileRecord(
                        id: selectedTile.Id,
                        rectangle: new Models.Geometric.RectangleInt(selectedTile.Rectangle.Point, new Models.Geometric.SizeInt(new Models.Geometric.WidthInt(value), selectedTile.Rectangle.Size.Height)),
                        comment: selectedTile.Comment,
                        logicalDelete: selectedTile.LogicalDelete));
                }
                else
                {
                    // タイル・カーソル無し時
                    _selectedTileOption = new Option<TileRecord>(new Models.TileRecord(
                        id: Models.TileId.Empty,

/* プロジェクト '2D RPG Negiramen (net7.0-windows10.0.19041.0)' からのマージされていない変更
前:
                        rectangle: new Models.RectangleInt(Models.PointInt.Empty, new Models.SizeInt(new Models.WidthInt(value), Models.HeightInt.Empty)),
後:
                        rectangle: new Models.RectangleInt(Models.PointInt.Empty, new Models.SizeInt(new Models.WidthInt(value), HeightInt.Empty)),
*/

/* プロジェクト '2D RPG Negiramen (net7.0-windows10.0.19041.0)' からのマージされていない変更
前:
                        rectangle: new Models.RectangleInt(Models.PointInt.Empty, new Models.SizeInt(new Models.WidthInt(value), Models.Geometric.HeightInt.Empty)),
後:
                        rectangle: new Models.RectangleInt(PointInt.Empty, new Models.SizeInt(new Models.WidthInt(value), Models.Geometric.HeightInt.Empty)),
*/
                        rectangle: new Models.Geometric.RectangleInt(Models.Geometric.PointInt.Empty, new Models.Geometric.SizeInt(new Models.Geometric.WidthInt(value), Models.Geometric.HeightInt.Empty)),
                        comment: Models.Comment.Empty,
                        logicalDelete: Models.LogicalDelete.False));
                }


                // this.SelectedTileSize = new Models.Size(new Models.Width(value), this.SelectedTileSize.Height);

                //
                // タイル・カーソルのキャンバス・サイズ変更
                // ========================================
                //
                // カーソルの線の幅が 4px なので、タイル・カーソルのキャンバス・サイズは + 8px にする
                var cursorWidth = value;
                var doubleCursorLineThickness = 4 * this.HalfThicknessOfTileCursorLine.AsInt;
                TileCursorCanvasWidthAsInt = cursorWidth + doubleCursorLineThickness;

                OnPropertyChanged(nameof(SelectedTileWidthAsInt));
                OnPropertyChanged(nameof(SelectedTileSize));
                OnPropertyChanged(nameof(SelectedTileRectangle));
            }
        }
        #endregion

        #region 変更通知プロパティ（選択タイルの縦幅）
        /// <summary>
        ///     選択タイルの縦幅
        /// </summary>
        public int SelectedTileHeightAsInt
        {
            get
            {
                if (this._selectedTileOption.TryGetValue(out TileRecord selectedTile))
                {
                    return selectedTile.Rectangle.Size.Height.AsInt;
                }
                else
                {
                    // タイル・カーソル無し時
                    return 0;
                }
            }
            set
            {
                if (this._selectedTileOption.TryGetValue(out TileRecord selectedTile))
                {
                    if (selectedTile.Rectangle.Size.Height.AsInt == value)
                    {
                        // 値に変化がない
                        return;
                    }

                    _selectedTileOption = new Option<TileRecord>(new Models.TileRecord(
                        id: selectedTile.Id,
                        rectangle: new Models.Geometric.RectangleInt(selectedTile.Rectangle.Point, new Models.Geometric.SizeInt(selectedTile.Rectangle.Size.Width, new Models.Geometric.HeightInt(value))),
                        comment: selectedTile.Comment,
                        logicalDelete: selectedTile.LogicalDelete));
                }
                else
                {
                    // タイル・カーソル無し時
                    _selectedTileOption = new Option<TileRecord>(new Models.TileRecord(
                        id: TileId.Empty,

/* プロジェクト '2D RPG Negiramen (net7.0-windows10.0.19041.0)' からのマージされていない変更
前:
                        rectangle: new Models.RectangleInt(Models.PointInt.Empty, new Models.SizeInt(Models.WidthInt.Empty, new Models.HeightInt(value))),
後:
                        rectangle: new Models.RectangleInt(PointInt.Empty, new Models.SizeInt(Models.WidthInt.Empty, new Models.HeightInt(value))),
*/

/* プロジェクト '2D RPG Negiramen (net7.0-windows10.0.19041.0)' からのマージされていない変更
前:
                        rectangle: new Models.RectangleInt(Models.Geometric.PointInt.Empty, new Models.SizeInt(Models.WidthInt.Empty, new Models.HeightInt(value))),
後:
                        rectangle: new Models.RectangleInt(Models.Geometric.PointInt.Empty, new Models.SizeInt(WidthInt.Empty, new Models.HeightInt(value))),
*/
                        rectangle: new Models.Geometric.RectangleInt(Models.Geometric.PointInt.Empty, new Models.Geometric.SizeInt(Models.Geometric.WidthInt.Empty, new Models.Geometric.HeightInt(value))),
                        comment: Models.Comment.Empty,
                        logicalDelete: Models.LogicalDelete.False));
                }

                // this.SelectedTileSize = new Models.Size(this.SelectedTileSize.Width, new Models.Height(value));

                //
                // タイル・カーソルのキャンバス・サイズ変更
                // ========================================
                //
                // カーソルの線の幅が 4px なので、タイル・カーソルのキャンバス・サイズは + 8px にする
                var cursorHeight = value;
                var doubleCursorLineThickness = 4 * this.HalfThicknessOfTileCursorLine.AsInt;
                TileCursorCanvasHeightAsInt = cursorHeight + doubleCursorLineThickness;

                OnPropertyChanged(nameof(SelectedTileHeightAsInt));
                OnPropertyChanged(nameof(SelectedTileSize));
                OnPropertyChanged(nameof(SelectedTileRectangle));
            }
        }
        #endregion

        #region 変更通知プロパティ（選択タイルのコメント）
        /// <summary>
        ///     選択タイルのコメント
        /// </summary>
        public string SelectedTileCommentAsStr
        {
            get
            {
                if (this._selectedTileOption.TryGetValue(out TileRecord selectedTile))
                {
                    return selectedTile.Comment.AsStr;
                }
                else
                {
                    // タイル・カーソル無し時
                    return string.Empty;
                }
            }
            set
            {
                if (this._selectedTileOption.TryGetValue(out TileRecord selectedTile))
                {
                    if (selectedTile.Comment.AsStr == value)
                    {
                        // 値に変化がない
                        return;
                    }

                    _selectedTileOption = new Option<TileRecord>(new Models.TileRecord(
                        id: selectedTile.Id,
                        rectangle: selectedTile.Rectangle,
                        comment: new Models.Comment(value),
                        logicalDelete: selectedTile.LogicalDelete));
                }
                else
                {
                    // タイル・カーソル無し時
                    _selectedTileOption = new Option<TileRecord>(new Models.TileRecord(
                        id: TileId.Empty,

/* プロジェクト '2D RPG Negiramen (net7.0-windows10.0.19041.0)' からのマージされていない変更
前:
                        rectangle: Models.RectangleInt.Empty,
後:
                        rectangle: RectangleInt.Empty,
*/
                        rectangle: Models.Geometric.RectangleInt.Empty,
                        comment: new Models.Comment(value),
                        logicalDelete: Models.LogicalDelete.False));
                }

                OnPropertyChanged(nameof(SelectedTileCommentAsStr));
            }
        }
        #endregion

        // - パブリック・メソッド

        #region メソッド（画面遷移でこの画面に戻ってきた時）
        /// <summary>
        ///     画面遷移でこの画面に戻ってきた時
        /// </summary>
        public void ReactOnVisited()
        {
            // ロケールが変わってるかもしれないので反映
            OnPropertyChanged(nameof(CultureInfoAsStr));

            // グリッド・キャンバス
            {
                // グリッドの左上位置（初期値）
                this.SourceGridPhase = new Models.Geometric.PointInt(new Models.Geometric.XInt(0), new Models.Geometric.YInt(0));

                // グリッドのタイルサイズ（初期値）
                this.SourceGridTileSize = new Models.Geometric.SizeInt(new Models.Geometric.WidthInt(32), new Models.Geometric.HeightInt(32));

                // グリッド・キャンバス画像の再作成
                this.RemakeGridCanvasImage();
            }
        }
        #endregion

        #region メソッド（ロケール変更による再描画）
        /// <summary>
        ///     ロケール変更による再描画
        ///     
        ///     <list type="bullet">
        ///         <item>動的にテキストを変えている部分に対応するため</item>
        ///     </list>
        /// </summary>
        public void RefreshByLocaleChanged()
        {
            if (this._selectedTileOption.TryGetValue(out var record))
            {
                if (record.Id == TileId.Empty)
                {
                    // 未選択時
                    // ［追加」
                    this.AddsButtonText = (string)LocalizationResourceManager.Instance["Add"];
                }
                else
                {
                    // 「上書」
                    this.AddsButtonText = (string)LocalizationResourceManager.Instance["Overwrite"];
                }
            }
            else
            {
                // タイル・カーソル無し時
                // 「追加」
                this.AddsButtonText = (string)LocalizationResourceManager.Instance["Add"];
            }
        }
        #endregion

        // - インターナル・メソッド

        #region メソッド（タイル・カーソルのキャンバスの再描画）
        /// <summary>
        ///     <pre>
        ///         タイル・カーソルのキャンバスの再描画
        /// 
        ///         TRICK:  GraphicsView を再描画させたいが、ビューモデルから要求する方法が分からない。
        ///                 そこで、内部的なグリッド画像の横幅が偶数のときは +1、奇数のときは -1 して
        ///                 振動させることで、再描画を呼び起こすことにする
        ///     </pre>
        /// </summary>
        internal void RefreshCanvasOfTileCursor(string codePlace = "[TileCropPageViewModel RefreshCanvasOfTileCursor]")
        {
            int offset;

            if (this._tileCursorCanvasSize.Width.AsInt % 2 == 1)
            {
                // Trace.WriteLine($"{codePlace} 幅 {this._tileCursorCanvasSize.Width.AsInt} から 1 引く");
                offset = -1;
            }
            else
            {
                // Trace.WriteLine($"{codePlace} 幅 {this._tileCursorCanvasSize.Width.AsInt} へ 1 足す");
                offset = 1;
            }

            // 循環参照を避けるために、直接フィールドを変更
            this._tileCursorCanvasSize = new Models.Geometric.SizeInt(new Models.Geometric.WidthInt(this._tileCursorCanvasSize.Width.AsInt + offset), new Models.Geometric.HeightInt(this._tileCursorCanvasSize.Height.AsInt));
            OnPropertyChanged(nameof(TileCursorCanvasWidthAsInt));
        }
        #endregion

        #region メソッド（タイルＩｄの再描画）
        /// <summary>
        ///     タイルＩｄの再描画
        /// </summary>
        internal void NotifyTileIdChange()
        {
            OnPropertyChanged(nameof(SelectedTileIdAsBASE64));
            OnPropertyChanged(nameof(SelectedTileIdAsPhoneticCode));
        }
        #endregion

        #region メソッド（作業中タイルセット画像の再描画）
        /// <summary>
        ///     作業中タイルセット画像の再描画
        /// </summary>
        internal void RefreshWorkingTilesetImage()
        {
            OnPropertyChanged(nameof(TilesetWorkingImageFilePathAsStr));
        }
        #endregion

        // - プライベート・フィールド

        #region フィールド（タイルセット設定　関連）
        /// <summary>
        ///     タイルセット設定のCSVファイル
        /// </summary>
        Models.FileEntries.Locations.TilesetSettingsFile _tilesetSettingsFile = Models.FileEntries.Locations.TilesetSettingsFile.Empty;

        /// <summary>
        ///     タイルセット設定
        /// </summary>
        Models.FileEntries.TilesetSettings _tilesetSettings = new Models.FileEntries.TilesetSettings();
        #endregion

        #region フィールド（タイルセット元画像　関連）
        /// <summary>
        ///     タイルセット元画像ファイルへのパス
        /// </summary>
        Models.FileEntries.Locations.TilesetImageFile tilesetImageFile = Models.FileEntries.Locations.TilesetImageFile.Empty;

        /// <summary>
        ///     タイルセット元画像サイズ
        /// </summary>

/* プロジェクト '2D RPG Negiramen (net7.0-windows10.0.19041.0)' からのマージされていない変更
前:
        Models.SizeInt tilesetSourceImageSize = Models.SizeInt.Empty;
後:
        SizeInt tilesetSourceImageSize = SizeInt.Empty;
*/
        Models.Geometric.SizeInt tilesetSourceImageSize = Models.Geometric.SizeInt.Empty;
        #endregion

        #region フィールド（タイルセット作業画像　関連）
        /// <summary>
        ///     タイルセット作業画像サイズ
        /// </summary>

/* プロジェクト '2D RPG Negiramen (net7.0-windows10.0.19041.0)' からのマージされていない変更
前:
        Models.SizeInt workingImageSize = Models.SizeInt.Empty;
後:
        SizeInt workingImageSize = SizeInt.Empty;
*/
        Models.Geometric.SizeInt workingImageSize = Models.Geometric.SizeInt.Empty;
        #endregion

        #region フィールド（グリッド　関連）
        /// <summary>
        ///     グリッド・キャンバス画像サイズ
        /// </summary>
        Models.Geometric.SizeInt gridCanvasImageSize = Models.Geometric.SizeInt.Empty;

        /// <summary>
        ///     グリッド・タイルのサイズ。元画像ベース
        /// </summary>
        Models.Geometric.SizeInt sourceGridTileSize = new Models.Geometric.SizeInt(new Models.Geometric.WidthInt(32), new Models.Geometric.HeightInt(32));

        /// <summary>
        ///     グリッド・タイルのサイズ。ズーム後（読取専用）
        /// </summary>
        Models.Geometric.SizeDouble workingGridTileSize = new Models.Geometric.SizeDouble(new Models.Geometric.WidthDouble(32), new Models.Geometric.HeightDouble(32));
        #endregion

        #region フィールド（選択タイル　関連）
        /// <summary>
        ///     選択タイルカーソルの位置（マージンとして）
        /// </summary>
        Thickness _tileCursorPointAsMargin = Thickness.Zero;

        /// <summary>
        ///     <pre>
        ///         選択タイルカーソルのキャンバス・サイズ
        ///     
        ///         カーソルの線の幅が 4px なので、画像サイズは + 8px にする
        ///     </pre>
        /// </summary>

/* プロジェクト '2D RPG Negiramen (net7.0-windows10.0.19041.0)' からのマージされていない変更
前:
        Models.SizeInt _tileCursorCanvasSize = Models.SizeInt.Empty;
後:
        SizeInt _tileCursorCanvasSize = SizeInt.Empty;
*/
        Models.Geometric.SizeInt _tileCursorCanvasSize = Models.Geometric.SizeInt.Empty;

        /// <summary>
        ///     選択タイル
        ///     
        ///     <list type="bullet">
        ///         <item>タイル・カーソルが有るときと、無いときを分ける</item>
        ///     </list>
        /// </summary>
        Option<TileRecord> _selectedTileOption = new Option<TileRecord>(Models.TileRecord.Empty);
        #endregion

        // - プライベート・メソッド

        #region メソッド（グリッド・キャンバス　関連）
        /// <summary>
        ///     <pre>
        ///         グリッドのキャンバスの再描画
        /// 
        ///         TRICK:  GraphicsView を再描画させたいが、ビューモデルから要求する方法が分からない。
        ///                 そこで、内部的なグリッド画像の横幅が偶数のときは +1、奇数のときは -1 して
        ///                 振動させることで、再描画を呼び起こすことにする
        ///     </pre>
        /// </summary>
        void InvalidateCanvasOfGrid()
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
        ///     グリッド・キャンバス画像の再作成
        ///     
        ///     <list type="bullet">
        ///         <item>グリッドの線の太さを 2px と想定しているので、グリッドの線が画像の端っこで切れないように、グリッドの内部的キャンバス・サイズを 2px 広げる</item>
        ///     </list>
        /// </summary>
        void RemakeGridCanvasImage()
        {
            this.GridCanvasImageSize = new Models.Geometric.SizeInt(
                width: new Models.Geometric.WidthInt((int)(this.ZoomAsDouble * this.TilesetSourceImageSize.Width.AsInt) + (2 * this.HalfThicknessOfGridLineAsInt)),
                height: new Models.Geometric.HeightInt((int)(this.ZoomAsDouble * this.TilesetSourceImageSize.Height.AsInt) + (2 * this.HalfThicknessOfGridLineAsInt)));
        }
        #endregion

        #region メソッド（ズーム）
        void DoZoom()
        {
            // 拡大率
            double zoomNum = this.ZoomAsDouble;

            // 元画像の複製
            var copySourceMap = new SKBitmap();
            this.TilesetSourceBitmap.CopyTo(copySourceMap);

            // TODO 出力先画像（ズーム）
        }
        #endregion

        /// <summary>
        ///     作業用画像の再作成
        /// </summary>
        void RemakeWorkingImage()
        {
            // 元画像をベースに、作業画像を複製
            this.TilesetWorkingBitmap = SkiaSharp.SKBitmap.FromImage(SkiaSharp.SKImage.FromBitmap(this.TilesetSourceBitmap));

            // 作業画像のサイズ計算
            this.workingImageSize = new Models.Geometric.SizeInt(
                width: new Models.Geometric.WidthInt((int)(this.ZoomAsDouble * this.TilesetSourceImageSize.Width.AsInt)),
                height: new Models.Geometric.HeightInt((int)(this.ZoomAsDouble * this.TilesetSourceImageSize.Height.AsInt)));

            // 作業画像のリサイズ
            this.TilesetWorkingBitmap = this.TilesetSourceBitmap.Resize(
                size: new SKSizeI(
                    width: this.workingImageSize.Width.AsInt,
                    height: this.workingImageSize.Height.AsInt),
                quality: SKFilterQuality.Medium);

            OnPropertyChanged(nameof(TilesetWorkingImageWidthAsInt));
            OnPropertyChanged(nameof(TilesetWorkingImageHeightAsInt));
        }
    }
}
