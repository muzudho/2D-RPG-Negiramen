namespace _2D_RPG_Negiramen.Models.FileEntries
{
    using System.Text;

    /// <summary>
    ///     タイル・セットの設定
    ///     
    ///     <list type="bullet">
    ///         <item>とりあえずミュータブル</item>
    ///     </list>
    /// </summary>
    public class TileSetSettings
    {
        // - インターナル静的メソッド

        #region メソッド（CSV形式ファイルの読込）
        /// <summary>
        ///     CSV形式ファイルの読込
        /// </summary>
        /// <param name="tileSetSettings">タイル・セット設定</param>
        /// <returns></returns>
        internal static bool LoadCSV(Locations.TileSetSettingsFile tileSetSettingsFile, out TileSetSettings tileSetSettings)
        {
            // 既定値の設定（空っぽ）
            tileSetSettings = new TileSetSettings();

            try
            {
                //
                // ファイルの有無確認
                // ==================
                //
                if (System.IO.File.Exists(tileSetSettingsFile.Path.AsStr))
                {
                    // ファイルが有るなら

                    //
                    // ファイル読取
                    // ============
                    //
                    var text = System.IO.File.ReadAllText(tileSetSettingsFile.Path.AsStr);

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

                        // TODO とりあえず、 Id, Left, Top, Width, Height, Comment の順で並んでいるとする。ちゃんと列名を見て対応したい
                        tileSetSettings.Add(
                            id: new Models.TileId(int.Parse(cells[0])),
                            rect: new Models.Rectangle(
                                point: new Models.Point(
                                    x: new Models.X(int.Parse(cells[1])),
                                    y: new Models.Y(int.Parse(cells[2]))),
                                size: new Models.Size(
                                    width: new Models.Width(int.Parse(cells[3])),
                                    height: new Models.Height(int.Parse(cells[4])))),
                            comment: new Models.Comment(cells[5]),
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

        #region プロパティ（対象のタイル・セットに含まれるすべてのタイルの記録）
        /// <summary>
        /// 対象のタイル・セットに含まれるすべてのタイルの記録
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
        /// <param name="onTileIdUpdated">タイルＩｄ更新時</param>
        internal void Add(
            Models.TileId id,
            Models.Rectangle rect,
            Models.Comment comment,
            Action onTileIdUpdated)
        {
            this.RecordList.Add(
                new TileRecord(
                    id,
                    rect,
                    comment));

            // ［次に採番できるＩｄ］を（できるなら）更新
            if (this.UpdateUsableId(id))
            {
                // 更新した
                onTileIdUpdated();
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
        internal bool TryGetByRectangle(Models.Rectangle rect, out TileRecord result)
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

        // - プライベート・メソッド

        #region メソッド（保存）
        /// <summary>
        ///     保存
        /// </summary>
        /// <returns>完了した</returns>
        internal bool SaveCSV(Locations.TileSetSettingsFile tileSetSettingsFile)
        {

            // 保存したいファイルへのパス
            var settingsFilePathAsStr = tileSetSettingsFile.Path.AsStr;

            var builder = new StringBuilder();

            // ヘッダー部
            builder.AppendLine("Id,Left,Top,Width,Height,Comment,Delete");

            // データ部
            foreach (var record in this.RecordList)
            {
                // TODO ダブルクォーテーションのエスケープ
                builder.AppendLine($"{record.Id.AsInt},{record.Rectangle.Point.X.AsInt},{record.Rectangle.Point.Y.AsInt},{record.Rectangle.Size.Width.AsInt},{record.Rectangle.Size.Height.AsInt},{record.Comment.AsStr}");
            }

            // 上書き
            System.IO.File.WriteAllText(settingsFilePathAsStr, builder.ToString());
            return true;
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
