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
    ///         <item>ビューモデル</item>
    ///     </list>
    /// </summary>
    internal class TilesetSettingsViewModel : ObservableObject
    {
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

            try
            {
                //
                // ファイルの有無確認
                // ==================
                //
                if (System.IO.File.Exists(tilesetSettingsFile.Path.AsStr))
                {
                    // ファイルが有るなら

                    //
                    // ファイル読取
                    // ============
                    //
                    var text = System.IO.File.ReadAllText(tilesetSettingsFile.Path.AsStr);

                    //
                    // ＣＳＶとして解析
                    // ================
                    //

                    // 先頭行は列名なので取り除く
                    var newLineIndex = text.IndexOf("\r\n");
                    text = text.Substring(newLineIndex + 2);

                    // 最後の改行は取り除く（空行は読込めない）
                    text = text.TrimEnd();

                    // とりあえず改行で分割
                    var lines = text.Split("\r\n");

                    // 各行について
                    foreach (var line in lines)
                    {
                        // 空行は読み飛ばす
                        if (string.IsNullOrWhiteSpace(line))
                        {
                            continue;
                        }

                        // TODO ダブル・クォーテーション対応

                        // とりあえずカンマで分割
                        var cells = line.Split(",");

                        int tileId = int.Parse(cells[0]);
                        int x = int.Parse(cells[1]);
                        int y = int.Parse(cells[2]);
                        int width = int.Parse(cells[3]);
                        int height = int.Parse(cells[4]);
                        string comment = cells[5];

                        Models.LogicalDelete logicalDelete;
                        if (7 <= cells.Length)
                        {
                            logicalDelete = new Models.LogicalDelete(int.Parse(cells[6]));
                        }
                        else
                        {
                            logicalDelete = Models.LogicalDelete.False;
                        }

                        // TODO とりあえず、 Id, Left, Top, Width, Height, Comment の順で並んでいるとする。ちゃんと列名を見て対応したい
                        tilesetSettingsVM.Add(
                            id: new Models.TileId(tileId),
                            rect: new Models.Geometric.RectangleInt(
                                point: new Models.Geometric.PointInt(
                                    x: new Models.Geometric.XInt(x),
                                    y: new Models.Geometric.YInt(y)),
                                size: new Models.Geometric.SizeInt(
                                    width: new Models.Geometric.WidthInt(width),
                                    height: new Models.Geometric.HeightInt(height))),
                            workingRect: new Models.Geometric.RectangleInt(
                                point: new Models.Geometric.PointInt(
                                    x: new Models.Geometric.XInt(x),
                                    y: new Models.Geometric.YInt(y)),
                                size: new Models.Geometric.SizeInt(
                                    width: new Models.Geometric.WidthInt(width),
                                    height: new Models.Geometric.HeightInt(height))),
                            comment: new Models.Comment(comment),
                            logicalDelete: logicalDelete,
                            onTileIdUpdated: () =>
                            {
                                // 自明なんで省略
                            });
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                // TODO エラー対応
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
            TheGeometric.RectangleInt workingRect,
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
        /// <param name="result">結果</param>
        /// <returns>有った</returns>
        internal bool TryGetByRectangle(TheGeometric.RectangleInt sourceRect, out TileRecordViewModel resultVM)
        {
            foreach (var recordVM in this.RecordViewModelList)
            {
                if (recordVM.SourceRectangle == sourceRect)
                {
                    resultVM = recordVM;
                    return true;
                }
            }

            resultVM = null;
            return false;
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
