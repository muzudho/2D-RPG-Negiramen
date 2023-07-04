using System.Text;

namespace _2D_RPG_Negiramen.Models.Files
{
    /// <summary>
    ///     タイル・セットの設定データ
    ///     
    ///     <list type="bullet">
    ///         <item>とりあえずミュータブル</item>
    ///     </list>
    /// </summary>
    class TileSetSettings
    {
        // - 静的インターナル・メソッド

        /// <summary>
        ///     CSV形式ファイルの読込
        /// </summary>
        /// <param name="tileSetSettings">タイル・セット設定</param>
        /// <returns></returns>
        internal static bool LoadCSV(Models.FileSpace.TileSetCSVFilePath tileSetCSVFilePath, out TileSetSettings tileSetSettings)
        {
            // 既定値の設定（空っぽ）
            tileSetSettings = new TileSetSettings();

            //
            // ファイルの有無確認
            // ==================
            //
            if (System.IO.File.Exists(tileSetCSVFilePath.AsStr))
            {
                // ファイルが有るなら

                //
                // ファイル読取
                // ============
                //
                var text = System.IO.File.ReadAllText(tileSetCSVFilePath.AsStr);

                //
                // ＣＳＶとして解析
                // ================
                //

                // とりあえず改行で分割
                var lines = text.Split("\r\n");

                // 各行について
                foreach (var line in lines)
                {
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
                        comment: new Models.Comment(cells[5]));
                }
            }

            return true;
        }

        /// <summary>
        ///     保存
        /// </summary>
        /// <returns>完了した</returns>
        internal bool SaveCSV(Models.FileSpace.TileSetCSVFilePath tileSetCSVFilePath)
        {

            // 保存したいファイルへのパス
            var settingsFilePathAsStr = tileSetCSVFilePath.AsStr;

            var builder = new StringBuilder();
            
            // ヘッダー部
            builder.AppendLine("Id,Left,Top,Width,Height,Comment");

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

        // - インターナル・プロパティ

        /// <summary>
        /// 対象のタイル・セットに含まれるすべてのタイルの記録
        /// </summary>
        internal List<TileSetRecord> RecordList { get; private set; } = new List<TileSetRecord>();

        // - インターナル・メソッド

        internal void Add(
            Models.TileId id,
            Models.Rectangle rect,
            Models.Comment comment)
        {
            this.RecordList.Add(
                new TileSetRecord(
                    id,
                    rect,
                    comment));
        }

        /// <summary>
        ///     指定の矩形と一致するレコードを返す
        /// </summary>
        /// <param name="rect">矩形</param>
        /// <param name="result">結果</param>
        /// <returns>有った</returns>
        internal bool TryGetByRectangle(Models.Rectangle rect, out TileSetRecord result)
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
    }
}
