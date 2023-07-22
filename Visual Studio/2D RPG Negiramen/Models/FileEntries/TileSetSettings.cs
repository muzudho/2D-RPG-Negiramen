namespace _2D_RPG_Negiramen.Models.FileEntries
{
    using TheGeometric = _2D_RPG_Negiramen.Models.Geometric;
    using System.Text;

    /// <summary>
    ///     タイルセット設定
    ///     
    ///     <list type="bullet">
    ///         <item>とりあえずミュータブル</item>
    ///     </list>
    /// </summary>
    public class TilesetSettings
    {
        // - インターナル静的メソッド

        #region メソッド（CSV形式ファイルの読込）
        /// <summary>
        ///     CSV形式ファイルの読込
        /// </summary>
        /// <param name="tilesetSettings">タイルセット設定</param>
        /// <returns></returns>
        internal static bool LoadCSV(Locations.TilesetSettingsFile tilesetSettingsFile, out TilesetSettings tilesetSettings)
        {
            // 既定値の設定（空っぽ）
            tilesetSettings = new TilesetSettings();

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
                        tilesetSettings.Add(
                            id: new Models.TileId(tileId),
                            rect: new Models.Geometric.RectangleInt(
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

        #region メソッド（保存）
        /// <summary>
        ///     保存
        /// </summary>
        /// <returns>完了した</returns>
        internal static bool SaveCSV(
            Locations.TilesetSettingsFile tileSetSettingsFile,
            List<TileRecord> recordList)
        {
            // 保存したいファイルへのパス
            var settingsFilePathAsStr = tileSetSettingsFile.Path.AsStr;

            var builder = new StringBuilder();

            // ヘッダー部
            builder.AppendLine("Id,Left,Top,Width,Height,Comment,Delete");

            // データ部
            foreach (var record in recordList)
            {
                // TODO ダブルクォーテーションのエスケープ
                builder.AppendLine($"{record.Id.AsInt},{record.Rectangle.Point.X.AsInt},{record.Rectangle.Point.Y.AsInt},{record.Rectangle.Size.Width.AsInt},{record.Rectangle.Size.Height.AsInt},{record.Comment.AsStr},{record.LogicalDelete.AsInt}");
            }

            // 上書き
            System.IO.File.WriteAllText(settingsFilePathAsStr, builder.ToString());
            return true;
        }
        #endregion

        #region メソッド（指定の矩形は、指定の矩形のリストの中の矩形のいずれかと交差するか？）
        /// <summary>
        ///     指定の矩形は、指定の矩形のリストの中の矩形のいずれかと交差するか？
        /// </summary>
        /// <param name="target">矩形</param>
        /// <param name="rectangles">矩形のリスト</param>
        /// <returns>そうだ</returns>
        internal static bool HasIntersection(TheGeometric.RectangleInt target, IEnumerator<TheGeometric.RectangleInt> rectangles)
        {
            while (rectangles.MoveNext())
            {
                var rect = rectangles.Current;

                if (target.HasIntersection(rect))
                {
                    return true;
                }
            }

            return false;
        }
        #endregion

        // - インターナル・プロパティー

        #region プロパティ（対象のタイルセットに含まれるすべてのタイルの記録）
        /// <summary>
        /// 対象のタイルセットに含まれるすべてのタイルの記録
        /// </summary>
        internal List<TileRecord> RecordList { get; private set; } = new List<TileRecord>();
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
        /// <param name="comment">コメント</param>
        /// <param name="logicalDelete">論理削除</param>
        /// <param name="onTileIdUpdated">タイルＩｄ更新時</param>
        internal void Add(
            Models.TileId id,
            Geometric.RectangleInt rect,
            Models.Comment comment,
            Models.LogicalDelete logicalDelete,
            Action onTileIdUpdated)
        {
            this.RecordList.Add(
                new TileRecord(
                    id,
                    rect,
                    comment,
                    logicalDelete));

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
            for (int i=0; i<this.RecordList.Count; i++)
            {
                var record = this.RecordList[i];

                if (record.Id == id)
                {
                    // 差替え
                    this.RecordList[i] = new TileRecord(
                        id: record.Id,
                        rectangle: record.Rectangle,
                        comment: record.Comment,
                        logicalDelete: Models.LogicalDelete.True);
                }
            }
        }
        #endregion

        #region メソッド（指定の矩形と一致するレコードを返す）
        /// <summary>
        ///     指定の矩形と一致するレコードを返す
        /// </summary>
        /// <param name="rect">矩形</param>
        /// <param name="result">結果</param>
        /// <returns>有った</returns>
        internal bool TryGetByRectangle(Geometric.RectangleInt rect, out TileRecord result)
        {
            foreach (var record in this.RecordList)
            {
                if (record.Rectangle == rect)
                {
                    result = record;
                    return true;
                }
            }

            result = null;
            return false;
        }
        #endregion

        #region メソッド（全ての矩形を取得）
        /// <summary>
        ///     全ての矩形を取得
        /// </summary>
        /// <returns>ストリーム</returns>
        internal IEnumerator<TheGeometric.RectangleInt> GetAllRectangles()
        {
            foreach (var record in this.RecordList)
            {
                // 矩形を１件返す
                yield return record.Rectangle;
            }
        }
        #endregion

        #region メソッド（指定の矩形は、登録されている矩形のいずれかと交差するか？）
        /// <summary>
        ///     指定の矩形は、登録されている矩形のいずれかと交差するか？
        /// </summary>
        /// <param name="target">矩形</param>
        /// <returns>そうだ</returns>
        internal bool HasIntersection(TheGeometric.RectangleInt target)
        {
            return TilesetSettings.HasIntersection(target, this.GetAllRectangles());
        }
        #endregion

        // - プライベート・メソッド

        #region メソッド（保存）
        /// <summary>
        ///     保存
        /// </summary>
        /// <returns>完了した</returns>
        internal bool SaveCSV(Locations.TilesetSettingsFile tileSetSettingsFile)
        {
            return TilesetSettings.SaveCSV(
                tileSetSettingsFile: tileSetSettingsFile,
                recordList: this.RecordList);
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
