namespace _2D_RPG_Negiramen.Models.Visually
{
    using _2D_RPG_Negiramen.Coding;
    using _2D_RPG_Negiramen.Models;
    using _2D_RPG_Negiramen.Models.FileEntries;
    using _2D_RPG_Negiramen.Models.Geometric;
    using CommunityToolkit.Mvvm.ComponentModel;
    using TheFileEntryLocations = FileEntries.Locations;
    using TheGeometric = Geometric;

    /// <summary>
    ///     😁 タイルセット・データテーブルの画面向け記憶
    ///     
    ///     <list type="bullet">
    ///         <item>ミュータブル</item>
    ///     </list>
    /// </summary>
    public class TilesetDatatableVisually : ObservableObject
    {
        // - その他

        #region その他（生成）
        internal TilesetDatatableVisually()
        {

        }
        #endregion

        // - インターナル静的メソッド

        #region メソッド（CSV形式ファイルの読込）
        /// <summary>
        ///     CSV形式ファイルの読込
        /// </summary>
        /// <param name="tilesetDatatableVisually">タイルセット・データテーブル視覚的</param>
        /// <returns></returns>
        internal static bool LoadCSV(
            TheFileEntryLocations.UnityAssets.DataCsvTilesetCsv tilesetDatatableFileLocation,
            out TilesetDatatableVisually tilesetDatatableVisually)
        {
            // 既定値の設定（空っぽ）
            tilesetDatatableVisually = new TilesetDatatableVisually();

            if (TilesetDatatable.LoadCSV(tilesetDatatableFileLocation, out TilesetDatatable tilesetDatatable, out TileIdOrEmpty usableId))
            {
                tilesetDatatableVisually.UsableId = usableId;

                foreach (TileRecord record in tilesetDatatable.RecordList)
                {
                    tilesetDatatableVisually.TileRecordVisuallyList.Add(
                        TileRecordVisually.FromModel(
                            tileRecord: record
#if DEBUG
                            , hint: "[TilesetDatatableVisually.cs LoadCSV]"
#endif
                            ));
                }
                try
                {
                    //
                    // ファイルの有無確認
                    // ==================
                    //
                    if (File.Exists(tilesetDatatableFileLocation.Path.AsStr))
                    {
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    // TODO エラー対応
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        #endregion

        // - インターナル・プロパティー

        #region プロパティ（対象のタイルセットに含まれるすべてのタイルの画面向け記憶）
        /// <summary>
        /// 対象のタイルセットに含まれるすべてのタイルの画面向け記憶
        /// </summary>
        internal List<TileRecordVisually> TileRecordVisuallyList { get; private set; } = new List<TileRecordVisually>();
        #endregion

        #region プロパティ（次に採番できるＩｄ。１から始まる）
        /// <summary>
        /// 次に採番できるＩｄ。１から始まる
        /// </summary>
        internal TileIdOrEmpty UsableId { get; set; } = new TileIdOrEmpty(1);
        #endregion

        // - インターナル・メソッド

        #region メソッド（タイルの追加）
        /// <summary>
        ///     タイルの追加
        /// </summary>
        /// <param name="id">タイルＩｄ</param>
        /// <param name="rect">位置とサイズ</param>
        /// <param name="title">タイトル</param>
        internal void AddTileVisually(
            TileIdOrEmpty id,
            TheGeometric.RectangleInt rect,
            TileTitle title)
        {
            TileRecordVisuallyList.Add(
                TileRecordVisually.FromModel(
                    tileRecord: new TileRecord(
                        id,
                        rect,
                        title)
#if DEBUG
                    , hint: "[TilesetDatatableVisually.cs AddTileVisually]"
#endif
                    ));
        }
        #endregion

        #region メソッド（タイルの追加）
        /// <summary>
        ///     タイルの追加
        /// </summary>
        /// <param name="removee">タイル</param>
        /// <remarks>完了</remarks>
        internal void AddTile(
            TileRecordVisually item) => this.TileRecordVisuallyList.Add(item);
        #endregion

        #region メソッド（タイルの削除）
        /// <summary>
        ///     タイルの削除
        /// </summary>
        /// <param name="item">タイル</param>
        /// <remarks>完了</remarks>
        internal bool RemoveTile(
            TileRecordVisually item) => this.TileRecordVisuallyList.Remove(item);
        #endregion

        #region メソッド（指定のＩｄと一致するレコードを返す）
        /// <summary>
        ///     指定のＩｄと一致するレコードを返す
        /// </summary>
        /// <param name="tileId">タイルＩｄ</param>
        /// <param name="resultVisuallyOrNull">結果</param>
        /// <returns>有った</returns>
        internal bool TryGetTileById(TileIdOrEmpty tileId, out TileRecordVisually? resultVisuallyOrNull)
        {
            foreach (var tileVisually in TileRecordVisuallyList)
            {
                if (tileVisually.Id == tileId)
                {
                    resultVisuallyOrNull = tileVisually;
                    return true;
                }
            }

            resultVisuallyOrNull = null;
            return false;
        }
        #endregion

        #region メソッド（Ｉｄを指定してレコードを削除）
        /// <summary>
        ///     Ｉｄを指定してレコードを削除
        /// </summary>
        /// <param name="tileId">タイルＩｄ</param>
        /// <param name="resultVisuallyOrNull">結果</param>
        /// <returns>有った</returns>
        internal bool TryRemoveTileById(TileIdOrEmpty tileId, out TileRecordVisually? resultVisuallyOrNull)
        {
            resultVisuallyOrNull = null;

            foreach (var recordVisually in TileRecordVisuallyList)
            {
                if (recordVisually.Id == tileId)
                {
                    resultVisuallyOrNull = recordVisually;
                    break;
                }
            }

            if (resultVisuallyOrNull != null)
            {
                return TileRecordVisuallyList.Remove(resultVisuallyOrNull);
            }

            return false;
        }
        #endregion

        #region メソッド（指定の矩形と一致するレコードを返す）
        /// <summary>
        ///     指定の矩形と一致するレコードを返す
        /// </summary>
        /// <param name="sourceRect">矩形</param>
        /// <param name="resultVMOrNull">結果</param>
        /// <returns>有った</returns>
        internal void MatchByRectangle(
            TheGeometric.RectangleInt sourceRect,
            LazyArgs.Set<TileRecordVisually> some,
            Action none)
        {
            foreach (var tileVisually in TileRecordVisuallyList)
            {
                if (tileVisually.SourceRectangle == sourceRect)
                {
                    some(tileVisually);
                    return;
                }
            }

            none();
        }
        #endregion

        #region メソッド（全てのレコード（元画像ベース）を取得）
        /// <summary>
        ///     全てのレコード（元画像ベース）を取得
        /// </summary>
        /// <returns>ストリーム</returns>
        internal IEnumerator<TileRecord> GetAllSourceRecords()
        {
            foreach (var tileVisually in TileRecordVisuallyList)
            {
                // レコードを１件返す
                yield return new TileRecord(
                    id: tileVisually.Id,
                    rect: tileVisually.SourceRectangle,
                    title: tileVisually.Title);
            }
        }
        #endregion

        #region メソッド（全ての矩形（元画像ベース）を取得）
        /// <summary>
        ///     全ての矩形（元画像ベース）を取得
        /// </summary>
        /// <returns>ストリーム</returns>
        internal IEnumerator<TheGeometric.RectangleInt> GetAllSourceRectangles()
        {
            foreach (var tileVisually in TileRecordVisuallyList)
            {
                // 矩形を１件返す
                yield return tileVisually.SourceRectangle;
            }
        }
        #endregion

        #region メソッド（指定の矩形は、登録されている矩形のいずれかと交差するか？）
        /// <summary>
        ///     指定の矩形は、登録されている矩形のいずれかと交差するか？
        /// </summary>
        /// <param name="sourceRectangle">矩形（元画像ベース）</param>
        /// <returns>そうだ</returns>
        internal bool HasIntersection(TheGeometric.RectangleInt sourceRectangle)
        {
            return TilesetDatatable.HasIntersection(sourceRectangle, GetAllSourceRectangles());
        }
        #endregion

        #region メソッド（指定の矩形は、登録されている矩形のいずれかと合同か？）
        /// <summary>
        ///     指定の矩形は、登録されている矩形のいずれかと合同るか？
        /// </summary>
        /// <param name="sourceRectangle">矩形（元画像ベース）</param>
        /// <returns>そうだ</returns>
        internal bool IsCongruence(TheGeometric.RectangleInt sourceRectangle)
        {
            return TilesetDatatable.IsCongruence(sourceRectangle, GetAllSourceRectangles());
        }
        #endregion

        #region メソッド（妥当だ）
        /// <summary>
        ///     妥当だ
        ///     
        ///     <list type="bullet">
        ///         <item>重たい処理</item>
        ///     </list>
        /// </summary>
        /// <returns></returns>
        internal bool IsValid()
        {
            return TilesetDatatable.IsValid(CreateTileRecordList());
        }
        #endregion

        #region メソッド（次に使えるＩｄを増やす）
        /// <summary>
        ///     次に使えるＩｄを増やす
        /// </summary>
        /// <exception cref="IndexOutOfRangeException">上限</exception>
        internal void IncreaseUsableId()
        {
            // 上限リミット・チェック
            if (UsableId.AsInt == int.MaxValue)
            {
                throw new IndexOutOfRangeException($"Usable Id {nameof(UsableId.AsInt)} must not be max");
            }

            UsableId = new TileIdOrEmpty(UsableId.AsInt + 1);
        }
        #endregion

        // - プライベート・メソッド

        #region メソッド（保存）
        /// <summary>
        ///     保存
        /// </summary>
        /// <returns>完了した</returns>
        internal bool SaveCsv(TheFileEntryLocations.UnityAssets.DataCsvTilesetCsv tileSetSettingsFile)
        {
            return TilesetDatatable.SaveCSV(
                tileSetSettingsFileLocation: tileSetSettingsFile,
                recordList: GetAllSourceRecords());
        }
        #endregion

        #region メソッド（タイルレコード一覧作成）
        /// <summary>
        ///     タイルレコード一覧作成
        /// </summary>
        /// <returns>タイルレコード一覧</returns>
        List<TileRecord> CreateTileRecordList()
        {
            var list = new List<TileRecord>();

            foreach (var tileVisually in TileRecordVisuallyList)
            {
                list.Add(new TileRecord(
                    id: tileVisually.Id,
                    rect: tileVisually.SourceRectangle,
                    title: tileVisually.Title));
            }

            return list;
        }
        #endregion
    }
}
