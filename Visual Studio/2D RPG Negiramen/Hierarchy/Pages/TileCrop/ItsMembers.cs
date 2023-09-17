namespace _2D_RPG_Negiramen.Hierarchy.Pages.TileCrop;

using _2D_RPG_Negiramen.Coding;
using _2D_RPG_Negiramen.Models;
using TheGeometric = Models.Geometric;

/// <summary>
///     メンバー・ネットワーク
///     
///     <list type="bullet">
///         <item>密結合を認めるオブジェクトのコレクション</item>
///     </list>
/// </summary>
internal class ItsMembers
{
    // - その他

    #region その他（生成）
    /// <summary>
    ///     生成
    /// </summary>
    internal ItsMembers()
    {
    }
    #endregion

    // - インターナル・プロパティ

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

    #region プロパティ（文化情報）
    /// <summary>文化情報</summary>
    internal InnerCultureInfo IndoorCultureInfo
    {
        get
        {
            if (indoorCultureInfo == null)
            {
                indoorCultureInfo = new InnerCultureInfo();
            }

            return indoorCultureInfo;
        }
    }
    InnerCultureInfo indoorCultureInfo;
    #endregion

    #region プロパティ（ポインティング・デバイス）
    /// <summary>ポインティング・デバイス</summary>
    internal InnerPointingDevice PointingDevice
    {
        get
        {
            if (pointingDevice == null)
            {
                pointingDevice = new InnerPointingDevice();
            }

            return pointingDevice;
        }
    }
    InnerPointingDevice pointingDevice;
    #endregion

    #region プロパティ（ズーム）
    /// <summary>ズーム</summary>
    internal ZoomProperties ZoomProperties
    {
        get
        {
            if (zoomProperties == null)
            {
                zoomProperties = new ZoomProperties(
                    memberNetwork: this);
            }

            return zoomProperties;
        }
    }
    ZoomProperties zoomProperties;
    #endregion

    #region プロパティ（グリッド単位）
    /// <summary>グリッド単位</summary>
    internal GridUnit GridUnit
    {
        get
        {
            if (gridUnit == null)
            {
                gridUnit = new GridUnit();
            }

            return gridUnit;
        }
    }
    GridUnit gridUnit;
    #endregion

    #region プロパティ（切抜きカーソル）
    /// <summary>切抜きカーソル</summary>
    internal CropCursor CropCursor
    {
        get
        {
            if (cropCursor == null)
            {
                cropCursor = new CropCursor(this);
            }

            return cropCursor;
        }
    }
    CropCursor cropCursor;
    #endregion

    #region プロパティ（切抜きカーソルが指すタイル）
    /// <summary>切抜きカーソルが指すタイル</summary>
    internal CropTile CropTile
    {
        get
        {
            if (cropTile == null)
            {
                cropTile = new CropTile(
                    colleagues: this);
            }

            return cropTile;
        }
    }
    CropTile cropTile;
    #endregion

    #region プロパティ（削除ボタン）
    /// <summary>削除ボタン</summary>
    internal Button DeletesButton
    {
        get
        {
            if (deletesButton == null)
            {
                deletesButton = new Button();
            }

            return deletesButton;
        }
    }
    Button deletesButton;
    #endregion

    #region プロパティ（［タイルセット元画像］　関連）
    /// <summary>
    ///     ［タイルセット元画像］のサイズ
    /// </summary>
    internal TheGeometric.SizeInt TilesetSourceImageSize { get; set; } = TheGeometric.SizeInt.Empty;
    #endregion

    #region プロパティ（［元画像グリッド］の線の半分の太さ）
    /// <summary>
    ///     ［元画像グリッド］の線の半分の太さ
    ///     
    ///     <list type="bullet">
    ///         <item>変更通知プロパティ <see cref="HalfThicknessOfGridLineAsInt"/> に関わる</item>
    ///     </list>
    /// </summary>
    internal ThicknessOfLine HalfThicknessOfGridLine
    {
        get => halfThicknessOfGridLine;
    }

    //internal void SetHalfThicknessOfGridLine(ThicknessOfLine value)
    //{
    //    if (this.halfThicknessOfGridLine != value)
    //    {
    //        this.halfThicknessOfGridLine = value;

    //        // 屋外を更新
    //        this.Corridor.OwnerPageVM.InvalidateHalfThicknessOfGridLineAsInt();
    //    }
    //}

    /// <summary>［元画像グリッド］の線の太さの半分</summary>
    ThicknessOfLine halfThicknessOfGridLine = new(1);
    #endregion

    // - インターナル・メソッド

    #region メソッド（追加ボタンの状態取得）
    /// <summary>
    ///     追加ボタンの状態取得
    /// </summary>
    internal AddsButtonState GetStateOfAddsButton()
    {
        // 切抜きカーソルが、登録済みタイルのいずれかと交差しているか？
        if (this.HasIntersectionBetweenCroppedCursorAndRegisteredTile)
        {
            // 合同のときは「交差中」とは表示しない
            if (!this.IsCongruenceBetweenCroppedCursorAndRegisteredTile)
            {
                // Trace.WriteLine("[TileCropPage.xml.cs InvalidateAddsButton] 交差中だ");
                // 「交差中」
                return AddsButtonState.Intersecting;
            }
        }

        var contents = this.CropTile.RecordVisually;

        if (contents.IsNone)
        {
            // ［切抜きカーソル］の指すタイル無し時

            // 「追加」
            return AddsButtonState.Adds;
        }

        // 切抜きカーソル有り時
        // Ｉｄ未設定時
        if (this.CropTile.IdOrEmpty == TileIdOrEmpty.Empty)
        {
            // Ｉｄが空欄
            // ［追加］（新規作成）だ

            // ［追加」
            return AddsButtonState.Adds;
        }

        // ［復元」
        return AddsButtonState.Restore;
    }
    #endregion

    #region メソッド（追加ボタンのラベル算出）
    /// <summary>
    ///     追加ボタンのラベル算出
    /// </summary>
    internal void CalculateLabelOfAddsButton(
        LazyArgs.Set<string> setAddsButtonText)
    {
        var addsButtonState = this.GetStateOfAddsButton();

        switch (addsButtonState)
        {
            case AddsButtonState.Intersecting:
                // 「交差中」
                setAddsButtonText((string)LocalizationResourceManager.Instance["Intersecting"]);
                return;

            case AddsButtonState.Adds:
                setAddsButtonText((string)LocalizationResourceManager.Instance["Add"]);
                return;

            case AddsButtonState.Restore:
                // ［復元」
                setAddsButtonText((string)LocalizationResourceManager.Instance["Restore"]);
                return;
        }
    }
    #endregion

    #region メソッド（削除ボタンの活性性の再更新）
    /// <summary>
    ///     削除ボタンの活性性の再更新
    /// </summary>
    internal void DeletesButtonRefreshEnabled(
        Action onEnableChanged)
    {
        var contents = this.CropTile.RecordVisually;

        if (
            // 切抜きカーソル無し時
            contents.IsNone
            // 論理削除時
            || contents.LogicalDelete.AsBool)
        {
            // 不活性
            this.DeletesButton.SetEnabled(
                value: false,
                onChanged: onEnableChanged);
            return;
        }

        if (contents.Id == TileIdOrEmpty.Empty)
        {
            // Ｉｄ未設定時
            this.DeletesButton.SetEnabled(
                value: false,
                onChanged: onEnableChanged);
            return;
        }

        // タイル登録済み時
        this.DeletesButton.SetEnabled(
            value: true,
            onChanged: onEnableChanged);
    }
    #endregion
}
