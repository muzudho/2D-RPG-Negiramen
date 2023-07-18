namespace _2D_RPG_Negiramen.ViewModels
{
    using _2D_RPG_Negiramen.Coding;
    using _2D_RPG_Negiramen.Models;
    using _2D_RPG_Negiramen.Models.Geometric;
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
                this.RemakeWorkingTilesetImage();

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
                if (this.zoom != value)
                {
                    this.ZoomAsFloat = value.AsFloat;
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

        #region プロパティ（切抜きカーソル。元画像ベース）
        /// <summary>
        ///     切抜きカーソル。元画像ベース
        /// </summary>
        public Option<TileRecord> SelectedTileOption
        {
            get => this.sourceCroppedCursorOption;
            set
            {
                Models.TileRecord newValue;

                if (this.sourceCroppedCursorOption.TryGetValue(out TileRecord selectedTile))
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
                this.SourceCroppedCursorLeftAsInt = newValue.Rectangle.Point.X.AsInt;
                this.SourceCroppedCursorTopAsInt = newValue.Rectangle.Point.Y.AsInt;
                this.SourceCroppedCursorWidthAsInt = newValue.Rectangle.Size.Width.AsInt;
                this.SourceCroppedCursorHeightAsInt = newValue.Rectangle.Size.Height.AsInt;
                this.SelectedTileCommentAsStr = newValue.Comment.AsStr;

                OnPropertyChanged(nameof(AddsButtonHint));
                OnPropertyChanged(nameof(AddsButtonText));
                this.NotifyTileIdChange();
                // TODO 矩形もリフレッシュしたい
                // TODO コメントもリフレッシュしたい
            }
        }
        #endregion

        #region プロパティ（登録タイル　関連）
        /// <summary>
        ///     選択タイルＩｄ
        /// </summary>
        public Models.TileId SelectedTileId
        {
            get
            {
                if (this.sourceCroppedCursorOption.TryGetValue(out TileRecord selectedTile))
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
                if (this.sourceCroppedCursorOption.TryGetValue(out TileRecord selectedTile))
                {
                    if (selectedTile.Id == value)
                    {
                        // 値に変化がない
                        return;
                    }

                    sourceCroppedCursorOption = new Option<TileRecord>(new Models.TileRecord(
                        id: value,
                        rectangle: selectedTile.Rectangle,
                        comment: selectedTile.Comment,
                        logicalDelete: selectedTile.LogicalDelete));
                }
                else
                {
                    // タイル・カーソル無し時
                    sourceCroppedCursorOption = new Option<TileRecord>(new Models.TileRecord(
                        id: value,
                        rectangle: Models.Geometric.RectangleInt.Empty,
                        comment: Models.Comment.Empty,
                        logicalDelete: Models.LogicalDelete.False));
                }

                this.RefreshByLocaleChanged();

                if (this.sourceCroppedCursorOption.TryGetValue(out var record))
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

        #region 変更通知プロパティ（グリッド　関連）
        /// <summary>
        ///     グリッド・キャンバスの画像サイズ
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

        /// <summary>
        ///     グリッド位相の左上表示位置。元画像ベース
        /// </summary>
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
                    this.WorkingGridPhaseLeftAsFloat = this.ZoomAsFloat * this.sourceGridPhase.X.AsInt;

                    // キャンバスを再描画
                    InvalidateCanvasOfGrid();

                    // キャンバスを再描画後に変更通知
                    OnPropertyChanged(nameof(SourceGridPhaseLeftAsInt));
                    OnPropertyChanged(nameof(SourceGridPhase));

                    OnPropertyChanged(nameof(WorkingGridPhaseLeftAsFloat));
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
                    this.WorkingGridPhaseTopAsFloat = (float)(this.ZoomAsFloat * this.sourceGridPhase.Y.AsInt);

                    // キャンバスを再描画
                    InvalidateCanvasOfGrid();

                    // キャンバスを再描画後に変更通知
                    OnPropertyChanged(nameof(SourceGridPhaseTopAsInt));
                    OnPropertyChanged(nameof(SourceGridPhase));

                    OnPropertyChanged(nameof(WorkingGridPhaseTopAsFloat));
                    OnPropertyChanged(nameof(WorkingGridPhase));
                }
            }
        }

        /// <summary>
        ///     グリッド位相の左上表示位置。ズーム後
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
        ///     グリッド位相の左上表示位置ｘ。ズーム後（読取専用）
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
        ///     グリッド位相の左上表示位置ｙ。ズーム後（読取専用）
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
        ///     グリッド・タイルのサイズ。元画像ベース
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
        ///     グリッド・タイルの横幅。元画像ベース
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
                    this.WorkingCroppedCursorWidthAsFloat = this.ZoomAsFloat * this.sourceGridUnit.Width.AsInt;

                    // キャンバスを再描画
                    InvalidateCanvasOfGrid();
                    // RefreshCanvasOfTileCursor(codePlace: "[TileCropPageViewModel SourceGridTileWidthAsInt set]");

                    // キャンバスを再描画後に変更通知
                    OnPropertyChanged(nameof(SourceGridTileWidthAsInt));
                    OnPropertyChanged(nameof(SourceGridUnit));
                }
            }
        }

        /// <summary>
        ///     グリッド・タイルの縦幅。元画像ベース
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
                    this.WorkingRectCursorHeightAsFloat = this.ZoomAsFloat * this.sourceGridUnit.Height.AsInt;

                    // キャンバスを再描画
                    InvalidateCanvasOfGrid();
                    // RefreshCanvasOfTileCursor(codePlace: "[TileCropPageViewModel SourceGridTileHeightAsInt set]");

                    // キャンバスを再描画後に変更通知
                    OnPropertyChanged(nameof(SourceGridTileHeightAsInt));
                    OnPropertyChanged(nameof(SourceGridUnit));
                }
            }
        }

        /// <summary>
        ///     グリッド・タイルのサイズ。ズーム後（読取専用）
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
        ///     グリッド・タイルの横幅。ズーム後（読取専用）
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
        ///     グリッド・タイルの縦幅。ズーム後（読取専用）
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

        #region 変更通知プロパティ（ズーム）
        /// <summary>
        ///     ズーム。整数形式
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
                    if (this.ZoomMinAsFloat <= value && value <= this.ZoomMaxAsFloat)
                    {
                        this.zoom = new Models.Geometric.Zoom(value);

                        // 作業画像を再作成
                        this.RemakeWorkingTilesetImage();

                        // 作業グリッド・タイル横幅の再計算
                        RefreshWorkingGridTileWidth();

                        // 作業グリッド・タイル縦幅の再計算
                        RefreshWorkingGridTileHeight();

                        // グリッド・キャンバス画像の再作成
                        this.RemakeGridCanvasImage();

                        OnPropertyChanged(nameof(ZoomAsFloat));
                        OnPropertyChanged(nameof(WorkingGridPhaseLeftAsFloat));
                        OnPropertyChanged(nameof(WorkingGridPhaseTopAsFloat));
                        OnPropertyChanged(nameof(WorkingGridPhase));

                        OnPropertyChanged(nameof(WorkingGridTileWidthAsFloat));
                        OnPropertyChanged(nameof(WorkingGridTileHeightAsFloat));
                        OnPropertyChanged(nameof(WorkingGridUnit));
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
                if (this.sourceCroppedCursorOption.TryGetValue(out TileRecord selectedTile))
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
                if (this.sourceCroppedCursorOption.TryGetValue(out TileRecord selectedTile))
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
                if (this.sourceCroppedCursorOption.TryGetValue(out TileRecord selectedTile))
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

        #region 変更通知プロパティ（切抜きカーソル。元画像ベース　関連）
        /// <summary>
        ///     切抜きカーソル。元画像ベースの矩形
        /// </summary>
        public Models.Geometric.RectangleInt SourceCroppedCursorRect
        {
            get
            {
                if (this.sourceCroppedCursorOption.TryGetValue(out TileRecord selectedTile))
                {
                    return selectedTile.Rectangle;
                }
                else
                {
                    // タイル・カーソル無し時
                    return Models.Geometric.RectangleInt.Empty;
                }
            }
            set
            {
                if (this.sourceCroppedCursorOption.TryGetValue(out TileRecord selectedTile))
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

                this.SourceCroppedCursorLeftAsInt = value.Point.X.AsInt;
                this.SourceCroppedCursorTopAsInt = value.Point.Y.AsInt;
                this.SourceCroppedCursorSize = value.Size;

                // 切抜きカーソル。ズーム済み
                this.WorkingCroppedCursorPointAsMargin = new Thickness(
                    // 左
                    this.SourceCroppedCursorLeftAsInt,
                    // 上
                    this.SourceCroppedCursorTopAsInt,
                    // 右
                    0,
                    // 下
                    0);
                this.WorkingCroppedCursorSize = new Models.Geometric.SizeFloat(
                    width: new Models.Geometric.WidthFloat(this.ZoomAsFloat * value.Size.Width.AsInt),
                    height: new Models.Geometric.HeightFloat(this.ZoomAsFloat * value.Size.Height.AsInt));
            }
        }

        /// <summary>
        ///     矩形カーソル。元画像ベースの位置ｘ
        /// </summary>
        public int SourceCroppedCursorLeftAsInt
        {
            get
            {
                if (this.sourceCroppedCursorOption.TryGetValue(out TileRecord selectedTile))
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
                if (this.sourceCroppedCursorOption.TryGetValue(out TileRecord selectedTile))
                {
                    if (selectedTile.Rectangle.Point.X.AsInt == value)
                    {
                        // 値に変化がない
                        return;
                    }

                    // 元画像ベース
                    this.sourceCroppedCursorOption = new Option<TileRecord>(new Models.TileRecord(
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

                    // 元画像ベース
                    this.sourceCroppedCursorOption = new Option<TileRecord>(new Models.TileRecord(
                        id: Models.TileId.Empty,
                        rectangle: new Models.Geometric.RectangleInt(
                            point: new Models.Geometric.PointInt(new Models.Geometric.XInt(value), Models.Geometric.YInt.Empty),
                            size: Models.Geometric.SizeInt.Empty),
                        comment: Models.Comment.Empty,
                        logicalDelete: Models.LogicalDelete.False));
                }

                // 切抜きカーソル。ズーム済み
                this.WorkingCroppedCursorPointAsMargin = new Thickness(
                    // 左
                    this.SourceCroppedCursorLeftAsInt,
                    // 上
                    this.SourceCroppedCursorTopAsInt,
                    // 右
                    0,
                    // 下
                    0);
                // TODO サイズは変化無しか？

                OnPropertyChanged(nameof(SourceCroppedCursorLeftAsInt));
                OnPropertyChanged(nameof(SourceCroppedCursorRect));
            }
        }

        /// <summary>
        ///     切抜きカーソル。元画像ベースの位置ｙ
        /// </summary>
        public int SourceCroppedCursorTopAsInt
        {
            get
            {
                if (this.sourceCroppedCursorOption.TryGetValue(out TileRecord selectedTile))
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
                if (this.sourceCroppedCursorOption.TryGetValue(out TileRecord selectedTile))
                {
                    if (selectedTile.Rectangle.Point.Y.AsInt == value)
                    {
                        // 値に変化がない
                        return;
                    }

                    // 元画像ベース
                    sourceCroppedCursorOption = new Option<TileRecord>(new Models.TileRecord(
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

                    // 元画像ベース
                    sourceCroppedCursorOption = new Option<TileRecord>(new Models.TileRecord(
                        id: Models.TileId.Empty,
                        rectangle: new Models.Geometric.RectangleInt(
                            point: new Models.Geometric.PointInt(Models.Geometric.XInt.Empty, new Models.Geometric.YInt(value)),
                            size: Models.Geometric.SizeInt.Empty),
                        comment: Models.Comment.Empty,
                        logicalDelete: Models.LogicalDelete.False));
                }

                // 切抜きカーソル。ズーム済み
                this.WorkingCroppedCursorPointAsMargin = new Thickness(
                        // 左
                        this.SourceCroppedCursorLeftAsInt,
                        // 上
                        this.SourceCroppedCursorTopAsInt,
                        // 右
                        0,
                        // 下
                        0);
                // TODO サイズは変化無しか？

                OnPropertyChanged(nameof(SourceCroppedCursorTopAsInt));
                OnPropertyChanged(nameof(SourceCroppedCursorRect));
            }
        }

        /// <summary>
        ///     矩形カーソル。元画像ベースのサイズ
        ///     
        ///     <list type="bullet">
        ///         <item>線の太さを含まない</item>
        ///     </list>
        /// </summary>
        public Models.Geometric.SizeInt SourceCroppedCursorSize
        {
            get
            {
                if (this.sourceCroppedCursorOption.TryGetValue(out TileRecord selectedTile))
                {
                    return selectedTile.Rectangle.Size;
                }
                else
                {
                    // タイル・カーソル無し時
                    return Models.Geometric.SizeInt.Empty;
                }
            }
            set
            {
                if (this.sourceCroppedCursorOption.TryGetValue(out TileRecord selectedTile))
                {
                    if (selectedTile.Rectangle.Size == value)
                    {
                        // 値に変化がない
                        return;
                    }
                }
                else
                {
                    // タイル・カーソル無し時
                }

                //
                // 選択タイルの横幅と縦幅
                // ======================
                //
                this.SourceCroppedCursorWidthAsInt = value.Width.AsInt;
                this.SourceCroppedCursorHeightAsInt = value.Height.AsInt;

                OnPropertyChanged(nameof(SourceCroppedCursorRect));
            }
        }

        /// <summary>
        ///     切抜きカーソル。元画像ベースの横幅
        /// </summary>
        public int SourceCroppedCursorWidthAsInt
        {
            get
            {
                if (this.sourceCroppedCursorOption.TryGetValue(out TileRecord selectedTile))
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
                if (this.sourceCroppedCursorOption.TryGetValue(out TileRecord selectedTile))
                {
                    if (selectedTile.Rectangle.Size.Width.AsInt == value)
                    {
                        // 値に変化がない
                        return;
                    }

                    sourceCroppedCursorOption = new Option<TileRecord>(new Models.TileRecord(
                        id: selectedTile.Id,
                        rectangle: new Models.Geometric.RectangleInt(selectedTile.Rectangle.Point, new Models.Geometric.SizeInt(new Models.Geometric.WidthInt(value), selectedTile.Rectangle.Size.Height)),
                        comment: selectedTile.Comment,
                        logicalDelete: selectedTile.LogicalDelete));
                }
                else
                {
                    // タイル・カーソル無し時
                    sourceCroppedCursorOption = new Option<TileRecord>(new Models.TileRecord(
                        id: Models.TileId.Empty,
                        rectangle: new Models.Geometric.RectangleInt(Models.Geometric.PointInt.Empty, new Models.Geometric.SizeInt(new Models.Geometric.WidthInt(value), Models.Geometric.HeightInt.Empty)),
                        comment: Models.Comment.Empty,
                        logicalDelete: Models.LogicalDelete.False));
                }

                // 矩形カーソル。ズーム済み（カーソルの線の幅を含まない）
                WorkingCroppedCursorWidthAsFloat = this.ZoomAsFloat * value;

                OnPropertyChanged(nameof(SourceCroppedCursorWidthAsInt));
                OnPropertyChanged(nameof(SourceCroppedCursorSize));
                OnPropertyChanged(nameof(SourceCroppedCursorRect));
            }
        }

        /// <summary>
        ///     切抜きカーソル。元画像ベースの縦幅
        /// </summary>
        public int SourceCroppedCursorHeightAsInt
        {
            get
            {
                if (this.sourceCroppedCursorOption.TryGetValue(out TileRecord selectedTile))
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
                if (this.sourceCroppedCursorOption.TryGetValue(out TileRecord selectedTile))
                {
                    if (selectedTile.Rectangle.Size.Height.AsInt == value)
                    {
                        // 値に変化がない
                        return;
                    }

                    sourceCroppedCursorOption = new Option<TileRecord>(new Models.TileRecord(
                        id: selectedTile.Id,
                        rectangle: new Models.Geometric.RectangleInt(selectedTile.Rectangle.Point, new Models.Geometric.SizeInt(selectedTile.Rectangle.Size.Width, new Models.Geometric.HeightInt(value))),
                        comment: selectedTile.Comment,
                        logicalDelete: selectedTile.LogicalDelete));
                }
                else
                {
                    // タイル・カーソル無し時
                    sourceCroppedCursorOption = new Option<TileRecord>(new Models.TileRecord(
                        id: TileId.Empty,
                        rectangle: new Models.Geometric.RectangleInt(Models.Geometric.PointInt.Empty, new Models.Geometric.SizeInt(Models.Geometric.WidthInt.Empty, new Models.Geometric.HeightInt(value))),
                        comment: Models.Comment.Empty,
                        logicalDelete: Models.LogicalDelete.False));
                }

                // 切抜きカーソル。ズーム済みの縦幅（カーソルの線の幅を含まない）
                WorkingRectCursorHeightAsFloat = this.ZoomAsFloat * value;

                OnPropertyChanged(nameof(SourceCroppedCursorHeightAsInt));
                OnPropertyChanged(nameof(SourceCroppedCursorSize));
                OnPropertyChanged(nameof(SourceCroppedCursorRect));
            }
        }
        #endregion

        #region 変更通知プロパティ（矩形カーソル。ズーム済み　関連）
        /// <summary>
        ///     矩形カーソル。ズーム済みの位置（マージンとして）
        /// </summary>
        public Thickness WorkingCroppedCursorPointAsMargin
        {
            get => this.workingCroppedCursorPointAsMargin;
            set
            {
                if (this.workingCroppedCursorPointAsMargin != value)
                {
                    this.workingCroppedCursorPointAsMargin = value;
                    OnPropertyChanged(nameof(WorkingCroppedCursorPointAsMargin));
                }
            }
        }

        /// <summary>
        ///     矩形カーソル。ズーム済みのサイズ
        ///         
        ///     <list type="bullet">
        ///         <item>カーソルの線の幅を含まない</item>
        ///     </list>
        /// </summary>
        public Models.Geometric.SizeFloat WorkingCroppedCursorSize
        {
            get => this.workingCroppedCursorSize;
            set
            {
                if (this.workingCroppedCursorSize != value)
                {
                    this.WorkingCroppedCursorWidthAsFloat = value.Width.AsFloat;
                    this.WorkingRectCursorHeightAsFloat = value.Height.AsFloat;
                }
            }
        }

        /// <summary>
        ///     矩形カーソル。ズーム済みの横幅
        ///         
        ///     <list type="bullet">
        ///         <item>カーソルの線の幅を含まない</item>
        ///     </list>
        /// </summary>
        public float WorkingCroppedCursorWidthAsFloat
        {
            get => this.workingCroppedCursorSize.Width.AsFloat;
            set
            {
                if (this.workingCroppedCursorSize.Width.AsFloat != value)
                {
                    this.workingCroppedCursorSize = new Models.Geometric.SizeFloat(new Models.Geometric.WidthFloat(value), workingCroppedCursorSize.Height);

                    // キャンバスを再描画
                    // RefreshCanvasOfTileCursor(codePlace: "[TileCropPageViewModel WorkingRectCursorWidthAsFloat set]");

                    // キャンバスを再描画後に変更通知
                    OnPropertyChanged(nameof(WorkingCroppedCursorWidthAsFloat));
                }
            }
        }

        /// <summary>
        ///     矩形カーソル。ズーム済みの縦幅
        ///         
        ///     <list type="bullet">
        ///         <item>カーソルの線の幅を含まない</item>
        ///     </list>
        /// </summary>
        public float WorkingRectCursorHeightAsFloat
        {
            get => this.workingCroppedCursorSize.Height.AsFloat;
            set
            {
                if (this.workingCroppedCursorSize.Height.AsFloat != value)
                {
                    this.workingCroppedCursorSize = new Models.Geometric.SizeFloat(this.workingCroppedCursorSize.Width, new Models.Geometric.HeightFloat(value));

                    // キャンバスを再描画
                    // RefreshCanvasOfTileCursor("[TileCropPageViewModel WorkingRectCursorHeightAsFloat set]");

                    // キャンバスを再描画後に変更通知
                    OnPropertyChanged(nameof(WorkingRectCursorHeightAsFloat));
                }
            }
        }
        #endregion

        #region プロパティ（登録タイル　関連）
        /// <summary>
        ///     矩形カーソル。元画像ベースのコメント
        /// </summary>
        public string SelectedTileCommentAsStr
        {
            get
            {
                if (this.sourceCroppedCursorOption.TryGetValue(out TileRecord selectedTile))
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
                if (this.sourceCroppedCursorOption.TryGetValue(out TileRecord selectedTile))
                {
                    if (selectedTile.Comment.AsStr == value)
                    {
                        // 値に変化がない
                        return;
                    }

                    sourceCroppedCursorOption = new Option<TileRecord>(new Models.TileRecord(
                        id: selectedTile.Id,
                        rectangle: selectedTile.Rectangle,
                        comment: new Models.Comment(value),
                        logicalDelete: selectedTile.LogicalDelete));
                }
                else
                {
                    // タイル・カーソル無し時
                    sourceCroppedCursorOption = new Option<TileRecord>(new Models.TileRecord(
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
                this.SourceGridUnit = new Models.Geometric.SizeInt(new Models.Geometric.WidthInt(32), new Models.Geometric.HeightInt(32));

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
            if (this.sourceCroppedCursorOption.TryGetValue(out var record))
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

        #region メソッド（矩形カーソル。ズーム済み　関連）
        /// <summary>
        ///     <pre>
        ///         矩形カーソル。ズーム済みのキャンバスの再描画
        /// 
        ///         TRICK:  GraphicsView を再描画させたいが、ビューモデルから要求する方法が分からない。
        ///                 そこで、内部的なグリッド画像の横幅が偶数のときは +1、奇数のときは -1 して
        ///                 振動させることで、再描画を呼び起こすことにする
        ///     </pre>
        /// </summary>
        internal void RefreshCanvasOfTileCursor(string codePlace = "[TileCropPageViewModel RefreshCanvasOfTileCursor]")
        {
            int offset;

            if (((int)this.workingCroppedCursorSize.Width.AsFloat) % 2 == 1) // FIXME 浮動小数点型の剰余は無理がある
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
            this.workingCroppedCursorSize = new Models.Geometric.SizeFloat(
                width: new Models.Geometric.WidthFloat(this.workingCroppedCursorSize.Width.AsFloat + offset),
                height: new Models.Geometric.HeightFloat(this.workingCroppedCursorSize.Height.AsFloat));

            // TRICK CODE:
            OnPropertyChanged(nameof(WorkingCroppedCursorWidthAsFloat));
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
        ///     グリッド単位。元画像ベース
        /// </summary>
        Models.Geometric.SizeInt sourceGridUnit = new Models.Geometric.SizeInt(new Models.Geometric.WidthInt(32), new Models.Geometric.HeightInt(32));

        /// <summary>
        ///     グリッド位相の左上表示位置。ズーム後
        /// </summary>
        Models.Geometric.PointFloat workingGridPhase = Models.Geometric.PointFloat.Empty;

        /// <summary>
        ///     グリッド単位。ズーム後
        /// </summary>
        Models.Geometric.SizeFloat workingGridUnit = new Models.Geometric.SizeFloat(new Models.Geometric.WidthFloat(32), new Models.Geometric.HeightFloat(32));
        #endregion

        #region フィールド（矩形カーソル。元画像ベース　関連）
        /// <summary>
        ///     矩形カーソル。元画像ベース
        ///     
        ///     <list type="bullet">
        ///         <item>タイル・カーソルが有るときと、無いときを分ける</item>
        ///     </list>
        /// </summary>
        Option<TileRecord> sourceCroppedCursorOption = new Option<TileRecord>(Models.TileRecord.Empty);
        #endregion

        #region フィールド（矩形カーソル。ズーム済み　関連）
        /// <summary>
        ///     矩形カーソル。ズーム済みの位置（マージンとして）
        ///     
        ///     <list type="bullet">
        ///         <item>マージンを含んだカーソルの左上位置</item>
        ///     </list>
        /// </summary>
        Thickness workingCroppedCursorPointAsMargin = Thickness.Zero;

        /// <summary>
        ///     矩形カーソル。ズーム済みのサイズ
        ///         
        ///     <list type="bullet">
        ///         <item>カーソルの線の幅は含まない</item>
        ///     </list>
        /// </summary>
        Models.Geometric.SizeFloat workingCroppedCursorSize = Models.Geometric.SizeFloat.Empty;
        #endregion

        #region フィールド（ズーム　関連）
        /// <summary>
        ///     ズーム最大
        /// </summary>
        Models.Geometric.Zoom zoomMax = new Models.Geometric.Zoom(4.0f);

        /// <summary>
        ///     ズーム最小
        /// </summary>
        Models.Geometric.Zoom zoomMin = new Models.Geometric.Zoom(0.5f);

        /// <summary>
        ///     ズーム
        /// </summary>
        Models.Geometric.Zoom zoom = Models.Geometric.Zoom.IdentityElement;
        #endregion

        // - プライベート・メソッド

        #region メソッド（作業タイルセット画像の再作成）
        /// <summary>
        ///     作業タイルセット画像の再作成
        /// </summary>
        void RemakeWorkingTilesetImage()
        {
            // 元画像をベースに、作業画像を複製
            this.TilesetWorkingBitmap = SkiaSharp.SKBitmap.FromImage(SkiaSharp.SKImage.FromBitmap(this.TilesetSourceBitmap));

            // 作業画像のサイズ計算
            this.workingImageSize = new Models.Geometric.SizeInt(
                width: new Models.Geometric.WidthInt((int)(this.ZoomAsFloat * this.TilesetSourceImageSize.Width.AsInt)),
                height: new Models.Geometric.HeightInt((int)(this.ZoomAsFloat * this.TilesetSourceImageSize.Height.AsInt)));

            // 作業画像のリサイズ
            this.TilesetWorkingBitmap = this.TilesetSourceBitmap.Resize(
                size: new SKSizeI(
                    width: this.workingImageSize.Width.AsInt,
                    height: this.workingImageSize.Height.AsInt),
                quality: SKFilterQuality.Medium);

            OnPropertyChanged(nameof(TilesetWorkingImageWidthAsInt));
            OnPropertyChanged(nameof(TilesetWorkingImageHeightAsInt));
        }
        #endregion

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
                width: new Models.Geometric.WidthInt((int)(this.ZoomAsFloat * this.TilesetSourceImageSize.Width.AsInt) + (2 * this.HalfThicknessOfGridLineAsInt)),
                height: new Models.Geometric.HeightInt((int)(this.ZoomAsFloat * this.TilesetSourceImageSize.Height.AsInt) + (2 * this.HalfThicknessOfGridLineAsInt)));
        }
        #endregion

        #region メソッド（ズーム）
        void DoZoom()
        {
            // 拡大率
            double zoomNum = this.ZoomAsFloat;

            // 元画像の複製
            var copySourceMap = new SKBitmap();
            this.TilesetSourceBitmap.CopyTo(copySourceMap);

            // TODO 出力先画像（ズーム）
        }
        #endregion

        /// <summary>
        ///     作業グリッド・タイル横幅の再計算
        /// </summary>
        void RefreshWorkingGridTileWidth()
        {
            this.WorkingGridTileWidthAsFloat = this.ZoomAsFloat * this.sourceGridUnit.Width.AsInt;

            OnPropertyChanged(nameof(WorkingGridTileWidthAsFloat));
            OnPropertyChanged(nameof(WorkingGridUnit));
        }

        /// <summary>
        ///     作業グリッド・タイル縦幅の再計算
        /// </summary>
        void RefreshWorkingGridTileHeight()
        {
            this.WorkingGridTileHeightAsFloat = this.ZoomAsFloat * this.sourceGridUnit.Height.AsInt;

            OnPropertyChanged(nameof(WorkingGridTileHeightAsFloat));
            OnPropertyChanged(nameof(WorkingGridUnit));
        }
    }
}
