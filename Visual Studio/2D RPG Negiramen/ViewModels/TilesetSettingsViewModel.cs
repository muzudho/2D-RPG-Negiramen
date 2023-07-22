namespace _2D_RPG_Negiramen.ViewModels
{
    using _2D_RPG_Negiramen.Models;
    using _2D_RPG_Negiramen.Models.FileEntries;
    using CommunityToolkit.Mvvm.ComponentModel;
    using TheFileEntryLocations = _2D_RPG_Negiramen.Models.FileEntries.Locations;
    using TheGeometric = _2D_RPG_Negiramen.Models.Geometric;

    /// <summary>
    ///     タイルセット設定ビューモデル
    ///     
    ///     <list type="bullet">
    ///         <item>ミュータブル</item>
    ///     </list>
    /// </summary>
    public class TilesetSettingsViewModel : ObservableObject
    {
        // - その他

        #region その他（生成）
        internal TilesetSettingsViewModel()
        {

        }
        #endregion

        // - インターナル静的メソッド

        #region メソッド（CSV形式ファイルの読込）
        /// <summary>
        ///     CSV形式ファイルの読込
        /// </summary>
        /// <param name="tilesetSettingsVM">タイルセット設定ビューモデル</param>
        /// <returns></returns>
        internal static bool LoadCSV(TheFileEntryLocations.TilesetSettingsFile tilesetSettingsFile, out TilesetSettingsViewModel tilesetSettingsVM)
        {
            // 既定値の設定（空っぽ）
            tilesetSettingsVM = new TilesetSettingsViewModel();

            if (TilesetSettings.LoadCSV(tilesetSettingsFile, out TilesetSettings tilesetSettings))
            {
                foreach (TileRecord record in tilesetSettings.RecordList)
                {
                    tilesetSettingsVM.RecordViewModelList.Add(
                        TileRecordViewModel.FromModel(
                            tileRecord: record,
                            workingRect: TheGeometric.RectangleFloat.FromModel(record.Rectangle)));
                }
                try
                {
                    //
                    // ファイルの有無確認
                    // ==================
                    //
                    if (System.IO.File.Exists(tilesetSettingsFile.Path.AsStr))
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

        #region プロパティ（対象のタイルセットに含まれるすべてのタイルの記録）
        /// <summary>
        /// 対象のタイルセットに含まれるすべてのタイルの記録
        /// </summary>
        internal List<TileRecordViewModel> RecordViewModelList { get; private set; } = new List<TileRecordViewModel>();
        #endregion

        #region プロパティ（次に採番できるＩｄ。１から始まる）
        /// <summary>
        /// 次に採番できるＩｄ。１から始まる
        /// </summary>
        internal Models.TileId UsableId { get; private set; } = new Models.TileId(1);
        #endregion

        // - インターナル・メソッド

        #region メソッド（タイルの追加）
        /// <summary>
        ///     タイルの追加
        /// </summary>
        /// <param name="id">タイルＩｄ</param>
        /// <param name="rect">位置とサイズ</param>
        /// <param name="workingRect">（ズーム後の）位置とサイズ</param>
        /// <param name="comment">コメント</param>
        /// <param name="logicalDelete">論理削除</param>
        /// <param name="onTileIdUpdated">タイルＩｄ更新時</param>
        internal void Add(
            Models.TileId id,
            TheGeometric.RectangleInt rect,
            TheGeometric.RectangleFloat workingRect,
            Models.Comment comment,
            Models.LogicalDelete logicalDelete,
            Action onTileIdUpdated)
        {
            this.RecordViewModelList.Add(
                TileRecordViewModel.FromModel(
                    tileRecord: new TileRecord(
                        id,
                        rect,
                        comment,
                        logicalDelete),
                    workingRect: workingRect));

            // ［次に採番できるＩｄ］を（できるなら）更新
            if (this.UpdateUsableId(id))
            {
                // 更新した
                onTileIdUpdated();
            }
        }
        #endregion

        #region メソッド（タイルの論理削除）
        /// <summary>
        ///     タイルの論理削除
        /// </summary>
        /// <param name="id">タイルＩｄ</param>
        internal void DeleteLogical(
            Models.TileId id)
        {
            // 愚直な検索
            for (int i = 0; i < this.RecordViewModelList.Count; i++)
            {
                var recordVM = this.RecordViewModelList[i];

                if (recordVM.Id == id)
                {
                    // 差替え
                    this.RecordViewModelList[i] = TileRecordViewModel.FromModel(
                        tileRecord: new TileRecord(
                            id: recordVM.Id,
                            rectangle: recordVM.SourceRectangle,
                            comment: recordVM.Comment,
                            logicalDelete: Models.LogicalDelete.True),
                        workingRect: this.RecordViewModelList[i].WorkingRectangle);
                }
            }
        }
        #endregion

        #region メソッド（指定の矩形と一致するレコードを返す）
        /// <summary>
        ///     指定の矩形と一致するレコードを返す
        /// </summary>
        /// <param name="sourceRect">矩形</param>
        /// <param name="resultVMOrNull">結果</param>
        /// <returns>有った</returns>
        internal bool TryGetByRectangle(TheGeometric.RectangleInt sourceRect, out TileRecordViewModel? resultVMOrNull)
        {
            foreach (var recordVM in this.RecordViewModelList)
            {
                if (recordVM.SourceRectangle == sourceRect)
                {
                    resultVMOrNull = recordVM;
                    return true;
                }
            }

            resultVMOrNull = null;
            return false;
        }
        #endregion

        #region メソッド（全ての矩形（元画像ベース）を取得）
        /// <summary>
        ///     全ての矩形（元画像ベース）を取得
        /// </summary>
        /// <returns>ストリーム</returns>
        internal IEnumerator<TheGeometric.RectangleInt> GetAllSourceRectangles()
        {
            foreach (var recordVM in this.RecordViewModelList)
            {
                // 矩形を１件返す
                yield return recordVM.SourceRectangle;
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
            return TilesetSettings.HasIntersection(sourceRectangle, this.GetAllSourceRectangles());
        }
        #endregion

        // - プライベート・メソッド

        #region メソッド（保存）
        /// <summary>
        ///     保存
        /// </summary>
        /// <returns>完了した</returns>
        internal bool SaveCSV(TheFileEntryLocations.TilesetSettingsFile tileSetSettingsFile)
        {
            List<TileRecord> recordList = new List<TileRecord>();

            foreach (var recordVM in this.RecordViewModelList)
            {
                TileRecord record = new TileRecord(
                    id: recordVM.Id,
                    rectangle: recordVM.SourceRectangle,
                    comment: recordVM.Comment,
                    logicalDelete: recordVM.LogicalDelete);
                recordList.Add(record);
            }

            return TilesetSettings.SaveCSV(
                tileSetSettingsFile: tileSetSettingsFile,
                recordList: recordList);
        }
        #endregion

        #region メソッド（［次に採番できるＩｄ］を（できるなら）更新）
        /// <summary>
        /// ［次に採番できるＩｄ］を（できるなら）更新
        /// </summary>
        /// <returns>更新した</returns>
        /// <exception cref="IndexOutOfRangeException">型の範囲に収まらない</exception>
        bool UpdateUsableId(Models.TileId id)
        {
            // ［次に採番できるＩｄ］更新
            if (this.UsableId <= id)
            {
                // 上限リミット・チェック
                if (this.UsableId.AsInt == int.MaxValue)
                {
                    throw new IndexOutOfRangeException($"{nameof(UsableId)} is max");
                }

                this.UsableId = new Models.TileId(id.AsInt + 1);
                return true;
            }

            return false;
        }
        #endregion
    }
}
