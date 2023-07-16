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
    [QueryProperty(nameof(ImageSize), queryId: "ImageSize")]
    [QueryProperty(nameof(GridCanvasSize), queryId: "GridCanvasSize")]
    [QueryProperty(nameof(GridLeftTop), queryId: "GridLeftTop")]
    [QueryProperty(nameof(GridTileSize), queryId: "GridTileSize")]
    class TileCropPageViewModel : ObservableObject, ITileCropPageViewModel
    {
        // - プロパティ

        #region プロパティ（タイルセット画像ファイルへのパス）
        /// <summary>
        ///     タイルセット画像ファイルへのパス
        /// </summary>
        public Models.FileEntries.Locations.TilesetImageFile TilesetImageFile
        {
            get => _tilesetImageFile;
            set
            {
                if (_tilesetImageFile != value)
                {
                    _tilesetImageFile = value;
                }
            }
        }
        #endregion

        #region プロパティ（タイルセット設定ファイルへのパス）
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
        #endregion

        #region プロパティ（選択タイルＩｄ）
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
                        rectangle: Models.Rectangle.Empty,
                        comment: Models.Comment.Empty,
                        logicalDelete: Models.LogicalDelete.False));
                }

                if (this._selectedTileOption.TryGetValue(out var record))
                {
                    if (record.Id == TileId.Empty)
                    {
                        // 未選択時
                        // ［追加」
                        this.AddsButtonText = (string)LocalizationResourceManager.Instance["Add"];
                        

                        this.AddsButtonIsEnabled = true;
                        this.DeletesButtonIsEnabled = false;
                    }
                    else
                    {
                        // 「上書」
                        this.AddsButtonText = (string)LocalizationResourceManager.Instance["Overwrite"];

                        this.AddsButtonIsEnabled = true;
                        this.DeletesButtonIsEnabled = true;
                    }
                }
                else
                {
                    // タイル・カーソル無し時
                    // 「追加」
                    this.AddsButtonText = (string)LocalizationResourceManager.Instance["Add"];

                    this.AddsButtonIsEnabled = false;
                    this.DeletesButtonIsEnabled = false;
                }

                NotifyTileIdChange();
            }
        }
        #endregion

        #region プロパティ（タイルセットの元画像。ビットマップ形式）
        /// <summary>
        ///     タイルセットの元画像。ビットマップ形式
        /// </summary>
        public SKBitmap TilesetSourceBitmap { get; set; }
        #endregion

        #region プロパティ（タイルセットの作業画像。ビットマップ形式）
        /// <summary>
        ///     タイルセットの作業画像。ビットマップ形式
        /// </summary>
        public SKBitmap TilesetWorkingBitmap { get; set; }
        #endregion

        // - 変更通知プロパティ

        #region 変更通知プロパティ（タイルセット設定）
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

        #region 変更通知プロパティ（現在選択中の文化情報。文字列形式）
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
        #endregion

        #region 変更通知プロパティ（ロケールＩｄのリスト）
        /// <summary>
        ///     ロケールＩｄのリスト
        /// </summary>
        public ObservableCollection<string> LocaleIdCollection => App.LocaleIdCollection;
        #endregion

        #region 変更通知プロパティ（作業中のタイルセット画像ファイルへのパス（文字列形式））
        /// <summary>
        ///     作業中のタイルセット画像ファイルへのパス（文字列形式）
        /// </summary>
        public string WorkingTilesetImageFilePathAsStr
        {
            get => App.GetOrLoadUserConfiguration().WorkingTilesetImageFile.Path.AsStr;
        }
        #endregion

        #region 変更通知プロパティ（タイルセット画像ファイルへのパス（文字列形式））
        /// <summary>
        ///     タイルセット画像ファイルへのパス（文字列形式）
        /// </summary>
        public string TilesetImageFilePathAsStr
        {
            get => _tilesetImageFile.Path.AsStr;
            set
            {
                if (_tilesetImageFile.Path.AsStr != value)
                {
                    _tilesetImageFile = new Models.FileEntries.Locations.TilesetImageFile(
                        pathSource: FileEntryPathSource.FromString(value),
                        convert: (pathSource) => FileEntryPath.From(pathSource,
                                                                    replaceSeparators: true));
                    OnPropertyChanged(nameof(TilesetImageFilePathAsStr));
                }
            }
        }
        #endregion

        #region 変更通知プロパティ（画像サイズ）
        /// <summary>
        ///     画像のサイズ
        /// </summary>
        public Models.Size ImageSize
        {
            get => _imageSize;
            set
            {
                if (_imageSize != value)
                {
                    // 差分判定
                    var dirtyWidth = _imageSize.Width != value.Width;
                    var dirtyHeight = _imageSize.Height != value.Height;

                    // 更新
                    _imageSize = value;

                    // 変更通知
                    if (dirtyWidth)
                    {
                        OnPropertyChanged(nameof(ImageWidthAsInt));
                    }

                    if (dirtyHeight)
                    {
                        OnPropertyChanged(nameof(ImageHeightAsInt));
                    }
                }
            }
        }

        /// <summary>
        ///     画像の横幅
        /// </summary>
        public int ImageWidthAsInt
        {
            get => _imageSize.Width.AsInt;
            set
            {
                if (_imageSize.Width.AsInt != value)
                {
                    _imageSize = new Models.Size(new Models.Width(value), _imageSize.Height);
                    OnPropertyChanged(nameof(ImageWidthAsInt));
                }
            }
        }

        /// <summary>
        ///     画像の縦幅
        /// </summary>
        public int ImageHeightAsInt
        {
            get => _imageSize.Height.AsInt;
            set
            {
                if (_imageSize.Height.AsInt != value)
                {
                    _imageSize = new Models.Size(_imageSize.Width, new Models.Height(value));
                    OnPropertyChanged(nameof(ImageHeightAsInt));
                }
            }
        }
        #endregion

        #region 変更通知プロパティ（グリッドのキャンバス・サイズ）
        /// <summary>
        ///     <pre>
        ///         グリッドのキャンバス・サイズ
        ///         
        ///         グリッドの線の太さを 2px と想定しているので、グリッドの線が画像の端っこで切れないように、グリッドの内部的なキャンバス・サイズを 2px 広げる
        ///     </pre>
        /// </summary>
        public Models.Size GridCanvasSize
        {
            get => _gridCanvasSize;
            set
            {
                if (_gridCanvasSize != value)
                {
                    // 差分判定
                    var dirtyWidth = _gridCanvasSize.Width != value.Width;
                    var dirtyHeight = _gridCanvasSize.Height != value.Height;

                    // 更新
                    _gridCanvasSize = value;

                    // 変更通知
                    if (dirtyWidth)
                    {
                        OnPropertyChanged(nameof(GridCanvasWidthAsInt));
                    }

                    if (dirtyHeight)
                    {
                        OnPropertyChanged(nameof(GridCanvasHeightAsInt));
                    }
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
        public int GridCanvasWidthAsInt
        {
            get => _gridCanvasSize.Width.AsInt;
            set
            {
                if (_gridCanvasSize.Width.AsInt != value)
                {
                    _gridCanvasSize = new Models.Size(new Models.Width(value), _gridCanvasSize.Height);
                    OnPropertyChanged(nameof(GridCanvasWidthAsInt));
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
        public int GridCanvasHeightAsInt
        {
            get => _gridCanvasSize.Height.AsInt;
            set
            {
                if (_gridCanvasSize.Height.AsInt != value)
                {
                    _gridCanvasSize = new Models.Size(_gridCanvasSize.Width, new Models.Height(value));
                    OnPropertyChanged(nameof(GridCanvasHeightAsInt));
                }
            }
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
        public Models.Size TileCursorCanvasSize
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
                    _tileCursorCanvasSize = new Models.Size(new Models.Width(value), _tileCursorCanvasSize.Height);

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
                    _tileCursorCanvasSize = new Models.Size(_tileCursorCanvasSize.Width, new Models.Height(value));

                    // キャンバスを再描画
                    RefreshCanvasOfTileCursor("[TileCropPageViewModel TileCursorCanvasHeightAsInt set]");

                    // キャンバスを再描画後に変更通知
                    OnPropertyChanged(nameof(TileCursorCanvasHeightAsInt));
                }
            }
        }
        #endregion

        #region 変更通知プロパティ（グリッド全体の左上表示位置）
        Models.Point gridLeftTop = Models.Point.Empty;

        /// <summary>
        ///     グリッド全体の左上表示位置
        /// </summary>
        public Models.Point GridLeftTop
        {
            get => this.gridLeftTop;
            set
            {
                if (this.gridLeftTop != value)
                {
                    this.GridLeftAsInt = value.X.AsInt;
                    this.GridTopAsInt = value.Y.AsInt;
                    OnPropertyChanged(nameof(GridLeftTop));
                }
            }
        }

        /// <summary>
        ///     グリッド全体の左上表示位置ｘ
        /// </summary>
        public int GridLeftAsInt
        {
            get => this.gridLeftTop.X.AsInt;
            set
            {
                if (this.gridLeftTop.X.AsInt != value)
                {
                    this.gridLeftTop = new Models.Point(new Models.X(value), this.gridLeftTop.Y);

                    // キャンバスを再描画
                    RefreshCanvasOfGrid();

                    // キャンバスを再描画後に変更通知
                    OnPropertyChanged(nameof(GridLeftAsInt));
                    OnPropertyChanged(nameof(GridLeftTop));
                }
            }
        }

        /// <summary>
        ///     グリッド全体の左上表示位置ｙ
        /// </summary>
        public int GridTopAsInt
        {
            get => this.gridLeftTop.Y.AsInt;
            set
            {
                if (this.gridLeftTop.Y.AsInt != value)
                {
                    this.gridLeftTop = new Models.Point(this.gridLeftTop.X, new Models.Y(value));

                    // キャンバスを再描画
                    RefreshCanvasOfGrid();

                    // キャンバスを再描画後に変更通知
                    OnPropertyChanged(nameof(GridLeftAsInt));
                    OnPropertyChanged(nameof(GridLeftTop));
                }
            }
        }
        #endregion

        #region 変更通知プロパティ（グリッド・タイルのサイズ）
        Models.Size gridTileSize = new Models.Size(new Models.Width(32), new Models.Height(32));

        /// <summary>
        ///     グリッド・タイルのサイズ
        /// </summary>
        public Models.Size GridTileSize
        {
            get => this.gridTileSize;
            set
            {
                if (this.gridTileSize != value)
                {
                    this.GridTileWidthAsInt = value.Width.AsInt;
                    this.GridTileHeightAsInt = value.Height.AsInt;
                    OnPropertyChanged(nameof(GridTileSize));
                }
            }
        }
        #endregion

        #region 変更通知プロパティ（グリッド・タイルの横幅）
        /// <summary>
        ///     グリッド・タイルの横幅
        /// </summary>
        public int GridTileWidthAsInt
        {
            get => this.gridTileSize.Width.AsInt;
            set
            {
                if (this.gridTileSize.Width.AsInt != value &&
                    // バリデーション
                    0 < value && value <= this.TileMaxWidthAsInt)
                {
                    this.gridTileSize = new Models.Size(new Models.Width(value), this.gridTileSize.Height);

                    // カーソルの線の幅が 4px なので、タイル・カーソルの画像サイズは + 8px にする
                    this.TileCursorCanvasWidthAsInt = this.gridTileSize.Width.AsInt + 4 * this.HalfThicknessOfTileCursorLine.AsInt;

                    // キャンバスを再描画
                    RefreshCanvasOfGrid();
                    RefreshCanvasOfTileCursor(codePlace: "[TileCropPageViewModel GridTileWidthAsInt set]");

                    // キャンバスを再描画後に変更通知
                    OnPropertyChanged(nameof(GridTileWidthAsInt));
                }
            }
        }
        #endregion

        #region 変更通知プロパティ（グリッド・タイルの縦幅）
        /// <summary>
        ///     グリッド・タイルの縦幅
        /// </summary>
        public int GridTileHeightAsInt
        {
            get => this.gridTileSize.Height.AsInt;
            set
            {
                if (this.gridTileSize.Height.AsInt != value &&
                    // バリデーション
                    0 < value && value <= this.TileMaxHeightAsInt)
                {
                    this.gridTileSize = new Models.Size(this.gridTileSize.Width, new Models.Height(value));

                    // カーソルの線の幅が 4px なので、タイル・カーソルの画像サイズは + 8px にする
                    this.TileCursorCanvasHeightAsInt = this.gridTileSize.Height.AsInt + 4 * this.HalfThicknessOfTileCursorLine.AsInt;

                    // キャンバスを再描画
                    RefreshCanvasOfGrid();
                    RefreshCanvasOfTileCursor(codePlace: "[TileCropPageViewModel GridTileHeightAsInt set]");

                    // キャンバスを再描画後に変更通知
                    OnPropertyChanged(nameof(GridTileHeightAsInt));
                }
            }
        }
        #endregion

        #region 変更通知プロパティ（タイルの最大サイズ）
        /// <summary>
        ///     タイルの最大横幅
        /// </summary>
        public int TileMaxWidthAsInt
        {
            get => App.GetOrLoadSettings().TileMaxSize.Width.AsInt;
        }

        /// <summary>
        ///     タイルの最大縦幅
        /// </summary>
        public int TileMaxHeightAsInt
        {
            get => App.GetOrLoadSettings().TileMaxSize.Height.AsInt;
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

        #region 変更通知プロパティ（追加／上書きボタンのラベル）
        string addsButtonText = (string)LocalizationResourceManager.Instance["Add"];

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

        #region 変更通知プロパティ（タイル・カーソルの線の半分の太さ）
        ThicknessOfLine halfThicknessOfTileCursorLine;

        /// <summary>
        ///     タイル・カーソルの線の半分の太さ
        /// </summary>
        public ThicknessOfLine HalfThicknessOfTileCursorLine
        {
            get
            {
                if (this.halfThicknessOfTileCursorLine == null)
                {
                    // 循環参照しないように注意
                    this.halfThicknessOfTileCursorLine = new Models.ThicknessOfLine(2 * this.HalfThicknessOfGridLine.AsInt);
                }
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
        public Models.Rectangle SelectedTileRectangle
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
                    return Models.Rectangle.Empty;
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
                        rectangle: new Models.Rectangle(
                            point: new Models.Point(new Models.X(value), selectedTile.Rectangle.Point.Y),
                            size: selectedTile.Rectangle.Size),
                        comment: selectedTile.Comment,
                        logicalDelete: selectedTile.LogicalDelete));
                }
                else
                {
                    // タイル・カーソル無し時
                    _selectedTileOption = new Option<TileRecord>(new Models.TileRecord(
                        id: Models.TileId.Empty,
                        rectangle: new Models.Rectangle(
                            point: new Models.Point(new Models.X(value), Models.Y.Empty),
                            size: Models.Size.Empty),
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
                        rectangle: new Models.Rectangle(
                            point: new Models.Point(selectedTile.Rectangle.Point.X, new Models.Y(value)),
                            size: selectedTile.Rectangle.Size),
                        comment: selectedTile.Comment,
                        logicalDelete: selectedTile.LogicalDelete));
                }
                else
                {
                    // タイル・カーソル無し時
                    _selectedTileOption = new Option<TileRecord>(new Models.TileRecord(
                        id: Models.TileId.Empty,
                        rectangle: new Models.Rectangle(
                            point: new Models.Point(Models.X.Empty, new Models.Y(value)),
                            size: Models.Size.Empty),
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
        public Models.Size SelectedTileSize
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
                    return Models.Size.Empty;
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
                        rectangle: new Models.Rectangle(selectedTile.Rectangle.Point, new Models.Size(new Models.Width(value), selectedTile.Rectangle.Size.Height)),
                        comment: selectedTile.Comment,
                        logicalDelete: selectedTile.LogicalDelete));
                }
                else
                {
                    // タイル・カーソル無し時
                    _selectedTileOption = new Option<TileRecord>(new Models.TileRecord(
                        id: Models.TileId.Empty,
                        rectangle: new Models.Rectangle(Models.Point.Empty, new Models.Size(new Models.Width(value), Models.Height.Empty)),
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
                        rectangle: new Models.Rectangle(selectedTile.Rectangle.Point, new Models.Size(selectedTile.Rectangle.Size.Width, new Models.Height(value))),
                        comment: selectedTile.Comment,
                        logicalDelete: selectedTile.LogicalDelete));
                }
                else
                {
                    // タイル・カーソル無し時
                    _selectedTileOption = new Option<TileRecord>(new Models.TileRecord(
                        id: TileId.Empty,
                        rectangle: new Models.Rectangle(Models.Point.Empty, new Models.Size(Models.Width.Empty, new Models.Height(value))),
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
                        rectangle: Models.Rectangle.Empty,
                        comment: new Models.Comment(value),
                        logicalDelete: Models.LogicalDelete.False));
                }

                OnPropertyChanged(nameof(SelectedTileCommentAsStr));
            }
        }
        #endregion

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
            this._tileCursorCanvasSize = new Models.Size(new Models.Width(this._tileCursorCanvasSize.Width.AsInt + offset), new Models.Height(this._tileCursorCanvasSize.Height.AsInt));
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

        #region メソッド（作業中のタイルセット画像の再描画）
        /// <summary>
        ///     作業中のタイルセット画像の再描画
        /// </summary>
        internal void RefreshWorkingTilesetImage()
        {
            OnPropertyChanged(nameof(WorkingTilesetImageFilePathAsStr));
        }
        #endregion

        // - プライベート・フィールド

        #region フィールド（画像サイズ）
        /// <summary>
        ///     画像サイズ
        /// </summary>
        Models.Size _imageSize = Models.Size.Empty;
        #endregion

        #region フィールド（内部的グリッド画像サイズ）
        /// <summary>
        ///     内部的グリッド画像サイズ
        /// </summary>
        Models.Size _gridCanvasSize = Models.Size.Empty;
        #endregion

        #region フィールド（タイルセット設定）
        /// <summary>
        ///     タイルセット設定
        /// </summary>
        Models.FileEntries.TilesetSettings _tilesetSettings = new Models.FileEntries.TilesetSettings();
        #endregion

        #region フィールド（タイルセット画像ファイルへのパス）
        /// <summary>
        ///     タイルセット画像ファイルへのパス
        /// </summary>
        Models.FileEntries.Locations.TilesetImageFile _tilesetImageFile = Models.FileEntries.Locations.TilesetImageFile.Empty;
        #endregion

        #region フィールド（タイルセットの設定CSVファイル）
        /// <summary>
        ///     タイルセットの設定CSVファイル
        /// </summary>
        Models.FileEntries.Locations.TilesetSettingsFile _tilesetSettingsFile = Models.FileEntries.Locations.TilesetSettingsFile.Empty;
        #endregion

        #region フィールド（タイル・カーソルの位置（マージンとして））
        /// <summary>
        ///     タイル・カーソルの位置（マージンとして）
        /// </summary>
        Thickness _tileCursorPointAsMargin = Thickness.Zero;
        #endregion

        #region フィールド（タイル・カーソルのキャンバス・サイズ）
        /// <summary>
        ///     <pre>
        ///         タイル・カーソルのキャンバス・サイズ
        ///     
        ///         カーソルの線の幅が 4px なので、画像サイズは + 8px にする
        ///     </pre>
        /// </summary>
        Models.Size _tileCursorCanvasSize = Models.Size.Empty;
        #endregion

        #region フィールド（タイルの最大サイズ）
        /// <summary>
        ///     <pre>
        ///         タイルの最大サイズ
        ///     
        ///         設定ファイルから与えられる
        ///     </pre>
        /// </summary>
        Models.Size _tileMaxSize = Models.Size.Empty;
        #endregion

        #region フィールド（選択タイル）
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

        #region メソッド（グリッドのキャンバスの再描画）
        /// <summary>
        ///     <pre>
        ///         グリッドのキャンバスの再描画
        /// 
        ///         TRICK:  GraphicsView を再描画させたいが、ビューモデルから要求する方法が分からない。
        ///                 そこで、内部的なグリッド画像の横幅が偶数のときは +1、奇数のときは -1 して
        ///                 振動させることで、再描画を呼び起こすことにする
        ///     </pre>
        /// </summary>
        void RefreshCanvasOfGrid()
        {
            if (this.GridCanvasWidthAsInt % 2 == 1)
            {
                this.GridCanvasWidthAsInt--;
            }
            else
            {
                this.GridCanvasWidthAsInt++;
            }
        }
        #endregion
    }
}
