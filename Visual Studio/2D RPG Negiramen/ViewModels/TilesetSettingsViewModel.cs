﻿namespace _2D_RPG_Negiramen.ViewModels
{
    using _2D_RPG_Negiramen.Coding;
    using _2D_RPG_Negiramen.Models;
    using _2D_RPG_Negiramen.Models.FileEntries;
    using CommunityToolkit.Mvvm.ComponentModel;
    using System.Diagnostics;
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
        internal static bool LoadCSV(TheFileEntryLocations.UnityAssets.DataCsvTilesetCsv tilesetSettingsFile, out TilesetSettingsViewModel tilesetSettingsVM)
        {
            // 既定値の設定（空っぽ）
            tilesetSettingsVM = new TilesetSettingsViewModel();

            if (TilesetSettings.LoadCSV(tilesetSettingsFile, out TilesetSettings tilesetSettings, out TileIdOrEmpty usableId))
            {
                tilesetSettingsVM.UsableId = usableId;

                foreach (TileRecord record in tilesetSettings.RecordList)
                {
                    tilesetSettingsVM.RecordViewModelList.Add(
                        TileRecordVisualBuffer.FromModel(
                            tileRecord: record,
                            workingRect: record.Rectangle.ToFloat()));
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
        internal List<TileRecordVisualBuffer> RecordViewModelList { get; private set; } = new List<TileRecordVisualBuffer>();
        #endregion

        #region プロパティ（次に採番できるＩｄ。１から始まる）
        /// <summary>
        /// 次に採番できるＩｄ。１から始まる
        /// </summary>
        internal Models.TileIdOrEmpty UsableId { get; set; } = new Models.TileIdOrEmpty(1);
        #endregion

        // - インターナル・メソッド

        #region メソッド（タイルの追加）
        /// <summary>
        ///     タイルの追加
        /// </summary>
        /// <param name="id">タイルＩｄ</param>
        /// <param name="rect">位置とサイズ</param>
        /// <param name="workingRect">（ズーム後の）位置とサイズ</param>
        /// <param name="title">タイトル</param>
        /// <param name="logicalDelete">論理削除</param>
        internal void AddTile(
            Models.TileIdOrEmpty id,
            TheGeometric.RectangleInt rect,
            TheGeometric.RectangleFloat workingRect,
            Models.TileTitle title,
            Models.LogicalDelete logicalDelete)
        {
            this.RecordViewModelList.Add(
                TileRecordVisualBuffer.FromModel(
                    tileRecord: new TileRecord(
                        id,
                        rect,
                        title,
                        logicalDelete),
                    workingRect: workingRect));
        }
        #endregion

        #region メソッド（タイルの論理削除）
        /// <summary>
        ///     タイルの論理削除
        /// </summary>
        /// <param name="id">タイルＩｄ</param>
        /// <remarks>完了</remarks>
        internal bool DeleteLogical(
            Models.TileIdOrEmpty id)
        {
            // 愚直な検索
            for (int i = 0; i < this.RecordViewModelList.Count; i++)
            {
                var recordVM = this.RecordViewModelList[i];

                if (recordVM.Id == id)
                {
                    Trace.WriteLine($"[TilesetSettingsViewModel.cs DeleteLogical] 論理削除する　id: [{recordVM.Id.AsBASE64}]");

                    // 差替え
                    this.RecordViewModelList[i] = TileRecordVisualBuffer.FromModel(
                        tileRecord: new TileRecord(
                            id: recordVM.Id,
                            rect: recordVM.SourceRectangle,
                            title: recordVM.Title,
                            logicalDelete: Models.LogicalDelete.True),
                        workingRect: recordVM.WorkingRectangle);

                    return true;
                }
            }

            return false;
        }
        #endregion

        #region メソッド（タイルの論理削除の取消）
        /// <summary>
        ///     タイルの論理削除の取消
        /// </summary>
        /// <param name="id">タイルＩｄ</param>
        /// <remarks>完了</remarks>
        internal bool UndeleteLogical(
            Models.TileIdOrEmpty id)
        {
            // 愚直な検索
            for (int i = 0; i < this.RecordViewModelList.Count; i++)
            {
                var recordVM = this.RecordViewModelList[i];

                if (recordVM.Id == id)
                {
                    Trace.WriteLine($"[TilesetSettingsViewModel.cs DeleteLogical] 論理削除の取消　id: [{recordVM.Id.AsBASE64}]");

                    // 差替え
                    this.RecordViewModelList[i] = TileRecordVisualBuffer.FromModel(
                        tileRecord: new TileRecord(
                            id: recordVM.Id,
                            rect: recordVM.SourceRectangle,
                            title: recordVM.Title,
                            logicalDelete: Models.LogicalDelete.False),
                        workingRect: recordVM.WorkingRectangle);

                    return true;
                }
            }

            return false;
        }
        #endregion

        #region メソッド（指定のＩｄと一致するレコードを返す）
        /// <summary>
        ///     指定のＩｄと一致するレコードを返す
        /// </summary>
        /// <param name="tileId">タイルＩｄ</param>
        /// <param name="resultVMOrNull">結果</param>
        /// <returns>有った</returns>
        internal bool TryGetTileById(TileIdOrEmpty tileId, out TileRecordVisualBuffer? resultVMOrNull)
        {
            foreach (var recordVM in this.RecordViewModelList)
            {
                if (recordVM.Id == tileId)
                {
                    resultVMOrNull = recordVM;
                    return true;
                }
            }

            resultVMOrNull = null;
            return false;
        }
        #endregion

        #region メソッド（Ｉｄを指定してレコードを削除）
        /// <summary>
        ///     Ｉｄを指定してレコードを削除
        /// </summary>
        /// <param name="tileId">タイルＩｄ</param>
        /// <param name="resultOrNull">結果</param>
        /// <returns>有った</returns>
        internal bool TryRemoveTileById(TileIdOrEmpty tileId, out TileRecordVisualBuffer? resultOrNull)
        {
            resultOrNull = null;

            foreach (var recordVM in this.RecordViewModelList)
            {
                if (recordVM.Id == tileId)
                {
                    resultOrNull = recordVM;
                    break;
                }
            }

            if (resultOrNull != null)
            {
                return this.RecordViewModelList.Remove(resultOrNull);
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
            LazyArgs.Set<TileRecordVisualBuffer> some,
            Action none)
        {
            foreach (var recordVM in this.RecordViewModelList)
            {
                if (recordVM.SourceRectangle == sourceRect)
                {
                    some(recordVM);
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
        internal IEnumerator<TileRecord> GetAllSourceRecords(bool includeLogicalDelete = false)
        {
            foreach (var recordVM in this.RecordViewModelList)
            {
                // 論理削除されているものは除く
                if (!includeLogicalDelete && recordVM.LogicalDelete == LogicalDelete.True)
                {
                    continue;
                }

                // レコードを１件返す
                yield return new TileRecord(
                    id: recordVM.Id,
                    rect: recordVM.SourceRectangle,
                    title: recordVM.Title,
                    logicalDelete: recordVM.LogicalDelete);
            }
        }
        #endregion

        #region メソッド（全ての矩形（元画像ベース）を取得）
        /// <summary>
        ///     全ての矩形（元画像ベース）を取得
        /// </summary>
        /// <returns>ストリーム</returns>
        internal IEnumerator<TheGeometric.RectangleInt> GetAllSourceRectangles(bool includeLogicalDelete = false)
        {
            foreach (var recordVM in this.RecordViewModelList)
            {
                // 論理削除されているものは除く
                if (!includeLogicalDelete && recordVM.LogicalDelete == LogicalDelete.True)
                {
                    continue;
                }

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

        #region メソッド（指定の矩形は、登録されている矩形のいずれかと合同か？）
        /// <summary>
        ///     指定の矩形は、登録されている矩形のいずれかと合同るか？
        /// </summary>
        /// <param name="sourceRectangle">矩形（元画像ベース）</param>
        /// <returns>そうだ</returns>
        internal bool IsCongruence(TheGeometric.RectangleInt sourceRectangle)
        {
            return TilesetSettings.IsCongruence(sourceRectangle, this.GetAllSourceRectangles());
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
            // 論理削除されているものも妥当性検証に含める
            return TilesetSettings.IsValid(this.CreateTileRecordList(includeLogicalDelete:true));
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
            if (this.UsableId.AsInt == int.MaxValue)
            {
                throw new IndexOutOfRangeException($"Usable Id {nameof(this.UsableId.AsInt)} must not be max");
            }

            this.UsableId = new TileIdOrEmpty(this.UsableId.AsInt + 1);
        }
        #endregion

        // - プライベート・メソッド

        #region メソッド（保存）
        /// <summary>
        ///     保存
        /// </summary>
        /// <returns>完了した</returns>
        internal bool SaveCSV(TheFileEntryLocations.UnityAssets.DataCsvTilesetCsv tileSetSettingsFile)
        {
            // 論理削除されているものも保存する
            return TilesetSettings.SaveCSV(
                tileSetSettingsFile: tileSetSettingsFile,
                recordList: this.GetAllSourceRecords(includeLogicalDelete: true));
        }
        #endregion

        #region メソッド（タイルレコード一覧作成）
        /// <summary>
        ///     タイルレコード一覧作成
        /// </summary>
        /// <returns>タイルレコード一覧</returns>
        List<TileRecord> CreateTileRecordList(bool includeLogicalDelete = false)
        {
            var list = new List<TileRecord>();

            foreach (var recordVM in this.RecordViewModelList)
            {
                // 論理削除されているものは除く
                if (!includeLogicalDelete && recordVM.LogicalDelete == LogicalDelete.True)
                {
                    continue;
                }

                list.Add(new TileRecord(
                    id: recordVM.Id,
                    rect: recordVM.SourceRectangle,
                    title: recordVM.Title,
                    logicalDelete: recordVM.LogicalDelete));
            }

            return list;
        }
        #endregion
    }
}
