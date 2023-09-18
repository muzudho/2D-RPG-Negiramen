namespace _2D_RPG_Negiramen.Hierarchy.Pages.TileCrop;

using _2D_RPG_Negiramen.Models;
using TheGeometric = Models.Geometric;

/// <summary>
///     メンバー
///     
///     <list type="bullet">
///         <item>Mutable</item>
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
        this.InnerCultureInfo = new InnerCultureInfo();
        this.PointingDevice = new PointingDevice();
        this.ZoomProperties = new ZoomProperties();
        this.GridUnit = new GridUnit();
        this.CropCursor = new CropCursor();
        this.CropTile = new CropTile();
        this.DeletesButton = new Button();
        this.TilesetSourceImageSize = TheGeometric.SizeInt.Empty;
        this.HalfThicknessOfGridLine = new(1);
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
    internal InnerCultureInfo InnerCultureInfo { get; }
    #endregion

    #region プロパティ（ポインティング・デバイス）
    /// <summary>ポインティング・デバイス</summary>
    internal PointingDevice PointingDevice { get; }
    #endregion

    #region プロパティ（ズーム）
    /// <summary>ズーム</summary>
    internal ZoomProperties ZoomProperties { get; }
    #endregion

    #region プロパティ（グリッド単位）
    /// <summary>グリッド単位</summary>
    internal GridUnit GridUnit { get; }
    #endregion

    #region プロパティ（切抜きカーソル）
    /// <summary>切抜きカーソル</summary>
    internal CropCursor CropCursor { get; }
    #endregion

    #region プロパティ（切抜きカーソルが指すタイル）
    /// <summary>切抜きカーソルが指すタイル</summary>
    internal CropTile CropTile { get; }
    #endregion

    #region プロパティ（削除ボタン）
    /// <summary>削除ボタン</summary>
    internal Button DeletesButton { get; }
    #endregion

    #region プロパティ（［タイルセット元画像］　関連）
    /// <summary>
    ///     ［タイルセット元画像］のサイズ
    /// </summary>
    internal TheGeometric.SizeInt TilesetSourceImageSize { get; set; }
    #endregion

    #region プロパティ（［元画像グリッド］の線の半分の太さ）
    /// <summary>
    ///     ［元画像グリッド］の線の半分の太さ
    ///     
    ///     <list type="bullet">
    ///         <item>変更通知プロパティ <see cref="HalfThicknessOfGridLineAsInt"/> に関わる</item>
    ///     </list>
    /// </summary>
    internal ThicknessOfLine HalfThicknessOfGridLine { get; }
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
    internal string GetLabelOfAddsButton()
    {
        var addsButtonState = this.GetStateOfAddsButton();

        switch (addsButtonState)
        {
            case AddsButtonState.Intersecting:
                // 「交差中」
                return (string)LocalizationResourceManager.Instance["Intersecting"];

            case AddsButtonState.Adds:
                // 「追加」
                return (string)LocalizationResourceManager.Instance["Add"];

            case AddsButtonState.Restore:
                // ［復元」
                return (string)LocalizationResourceManager.Instance["Restore"];
        }

        // それ以外
        return string.Empty;
    }
    #endregion

    #region メソッド（削除ボタンの活性性）
    /// <summary>
    ///     削除ボタンの活性性
    /// </summary>
    internal bool DeletesButton_IsEnabled
    {
        get
        {
            var contents = this.CropTile.RecordVisually;

            if (
                // 切抜きカーソル無し時
                contents.IsNone
                // 論理削除時
                || contents.LogicalDelete.AsBool
                // Ｉｄ未設定時
                || contents.Id == TileIdOrEmpty.Empty)
            {
                // 不活性
                return false;
            }

            // タイル登録済み時
            return true;
        }
    }
    #endregion
}
