namespace _2D_RPG_Negiramen.Hierarchy.Pages.TileCrop;

using _2D_RPG_Negiramen.Models;
using System.Diagnostics;
using TheGeometric = Models.Geometric;
using TheTileCropPage = _2D_RPG_Negiramen.Hierarchy.Pages.TileCrop;

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
        this.SelectedTile = new Tile();
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

    #region プロパティ（切抜きカーソルが指すタイル）
    /// <summary>切抜きカーソルが指すタイル</summary>
    internal Tile SelectedTile { get; }
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

        // 「追加」
        return AddsButtonState.Adds;
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
        }

        // それ以外
        throw new InvalidOperationException("追加ボタンが未知の状態");
    }
    #endregion

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
    ///         <item>Ｉｄが未設定時</item>
    ///     </list>
    ///     
    ///     ※３　［復元］ボタンは、以下の条件で活性
    ///     <list type="bullet">
    ///         <item>Ｉｄが設定時</item>
    ///     </list>
    ///     
    ///     ※４　［交差中］ボタンは、常に不活性
    /// </summary>
    internal bool AddsButton_IsEnabled
    {
        get
        {
            // ※１
            if (this.SelectedTile.RecordVisually.IsNone)
            {
                return false;
            }

            var addsButtonState = this.GetStateOfAddsButton();
            bool enabled;

            switch (addsButtonState)
            {
                case TheTileCropPage.AddsButtonState.Adds:
                    {
                        // ※２
                        enabled = this.SelectedTile.RecordVisually.Id == TileIdOrEmpty.Empty;
                        Trace.WriteLine($"［デバッグ］　追加ボタンの活性性を {enabled} へ");
                    }
                    return enabled;

                case TheTileCropPage.AddsButtonState.Intersecting:
                    {
                        // ※４
                        enabled = false;
                        Trace.WriteLine($"［デバッグ］　交差中ボタンの活性性を {enabled} へ");
                    }
                    return enabled;

                default:
                    // ※４
                    Trace.WriteLine($"［デバッグ］　▲異常　追加ボタンの状態 {addsButtonState}");
                    return false;
            }
        }
    }

    #region メソッド（削除ボタンの活性性）
    /// <summary>
    ///     削除ボタンの活性性
    /// </summary>
    internal bool DeletesButton_IsEnabled
    {
        get
        {
            var contents = this.SelectedTile.RecordVisually;

            if (
                // 切抜きカーソル無し時
                contents.IsNone
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










    // TODO ★ 作業中の縦幅は、記憶せず、計算で出したい
    #region プロパティ（作業中の縦幅）
    /// <summary>
    ///     作業中の縦幅
    ///     ［切抜きカーソル］ズーム済みのサイズ
    ///     サイズが更新されていれば、カーソルのキャンバスを更新する必要がある
    ///         
    ///     <list type="bullet">
    ///         <item>カーソルの線の幅を含まない</item>
    ///         <item>TODO ★ 現在、範囲選択は、この作業用のサイズを使っているが、ソースの方のサイズを変更するようにできないか？ ワーキングは変数にしないようにしたい</item>
    ///         <item>仕様変更するときは、TRICK CODE に注意</item>
    ///     </list>
    /// </summary>
    internal TheGeometric.HeightFloat SelectedTile_GetWorkingHeight(TheGeometric.Zoom zoom) => this.selectedTile_workingHeight;

    internal bool SelectedTile_SetWorkingHeight(TheGeometric.HeightFloat height)
    {
        this.selectedTile_workingHeight = height;

        // 変更
        return true;
    }

    TheGeometric.HeightFloat selectedTile_workingHeight = TheGeometric.HeightFloat.Zero;
    #endregion
}
