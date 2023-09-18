namespace _2D_RPG_Negiramen.Models.FileEntries;

using TheGeometric = _2D_RPG_Negiramen.Models.Geometric;
using System.Text;
using System.Diagnostics;
using _2D_RPG_Negiramen.Models.FileEntries.Locations.UnityAssets;

/// <summary>
///     タイルセット・データテーブル
///     
///     <list type="bullet">
///         <item>とりあえずミュータブル</item>
///     </list>
/// </summary>
public class TilesetDatatable
{
    // - インターナル静的メソッド

    #region メソッド（CSV形式ファイルの読込）
    /// <summary>
    ///     CSV形式ファイルの読込
    /// </summary>
    /// <param name="tilesetSettingsFile">タイルセット設定ファイルの場所</param>
    /// <param name="tilesetSettings">タイルセット設定</param>
    /// <param name="usableId">次に使えるＩｄ</param>
    /// <returns></returns>
    internal static bool LoadCSV(
        DataCsvTilesetCsv tilesetSettingsFile,
        out TilesetDatatable tilesetSettings,
        out TileIdOrEmpty usableId)
    {
        // 既定値の設定（空っぽ）
        tilesetSettings = new TilesetDatatable();

        usableId = new TileIdOrEmpty(1);

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

                    int tileIdAsInt = int.Parse(cells[0]);
                    int x = int.Parse(cells[1]);
                    int y = int.Parse(cells[2]);
                    int width = int.Parse(cells[3]);
                    int height = int.Parse(cells[4]);
                    string title = cells[5];

                    Models.LogicalDelete logicalDelete;
                    if (7 <= cells.Length)
                    {
                        logicalDelete = new Models.LogicalDelete(int.Parse(cells[6]));
                    }
                    else
                    {
                        logicalDelete = Models.LogicalDelete.False;
                    }

                    // TODO とりあえず、 Id, Left, Top, Width, Height, Title の順で並んでいるとする。ちゃんと列名を見て対応したい
                    var tileId = new Models.TileIdOrEmpty(tileIdAsInt);
                    tilesetSettings.AddTile(
                        id: tileId,
                        rect: new Models.Geometric.RectangleInt(
                            location: new Models.Geometric.PointInt(
                                x: new Models.Geometric.XInt(x),
                                y: new Models.Geometric.YInt(y)),
                            size: new Models.Geometric.SizeInt(
                                width: new Models.Geometric.WidthInt(width),
                                height: new Models.Geometric.HeightInt(height))),
                        tileTitle: new Models.TileTitle(title),
                        logicalDelete: logicalDelete);

                    if (usableId < tileId)
                    {
                        usableId = tileId;
                    }
                }

                tilesetSettings.UsableId = usableId;

                // 次に使えるＩｄを増やす
                tilesetSettings.IncreaseUsableId();

                usableId = tilesetSettings.UsableId;
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
        DataCsvTilesetCsv tileSetSettingsFileLocation,
        IEnumerator<TileRecord> recordList)
    {
        // 保存したいファイルへのパス
        var settingsFilePathAsStr = tileSetSettingsFileLocation.Path.AsStr;

        var builder = new StringBuilder();

        // ヘッダー部
        builder.AppendLine("Id,Left,Top,Width,Height,Title,Delete");

        // データ部
        while (recordList.MoveNext())
        {
            TileRecord record = recordList.Current;

            // TODO ダブルクォーテーションのエスケープ
            builder.AppendLine($"{record.Id.AsInt},{record.Rectangle.Location.X.AsInt},{record.Rectangle.Location.Y.AsInt},{record.Rectangle.Size.Width.AsInt},{record.Rectangle.Size.Height.AsInt},{record.Title.AsStr},{record.LogicalDelete.AsInt}");
        }

        // 上書き
        System.IO.File.WriteAllText(settingsFilePathAsStr, builder.ToString());
        return true;
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
            // TODO 論理削除は難しいから廃止予定
    /// <param name="recordList">タイル一覧。論理削除されているものも含むこと</param>
    /// <returns>そうだ</returns>
    internal static bool IsValid(List<TileRecord> recordList)
    {
        int errorCount = 0;

        // 総当たりするか？
        for (int i = 0; i < recordList.Count; i++)
        {
            TileRecord recordI = recordList[i];

            for (int j = i + 1; j < recordList.Count; j++)
            {
                TileRecord recordJ = recordList[j];

                if (recordI.Id == recordJ.Id)
                {
                    // 同じＩｄが含まれていたらエラー
                    // Trace.WriteLine($"[TilesetSettings.cs IsValid] ({errorCount + 1}) エラー。同じＩｄが含まれていた。 (1) [{recordI.Id.AsInt}][{recordI.Id.AsBASE64}] (2) [{recordJ.Id.AsInt}][{recordJ.Id.AsBASE64}]");
                    errorCount++;
                }
                else if (recordI.Rectangle == recordJ.Rectangle)
                {
                    // 合同の矩形が含まれていたらエラー
                    // Trace.WriteLine($"[TilesetSettings.cs IsValid] ({errorCount + 1}) エラー。合同の矩形が含まれていた。 (1) [{recordI.Id.AsInt}][{recordI.Id.AsBASE64}] (2) [{recordJ.Id.AsInt}][{recordJ.Id.AsBASE64}]");
                    errorCount++;
                }
            }
        }

        return errorCount == 0;
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

    #region メソッド（指定の矩形は、指定の矩形のリストの中の矩形のいずれかと合同か？）
    /// <summary>
    ///     指定の矩形は、指定の矩形のリストの中の矩形のいずれかと合同か？
    /// </summary>
    /// <param name="target">矩形</param>
    /// <param name="rectangles">矩形のリスト</param>
    /// <returns>そうだ</returns>
    internal static bool IsCongruence(TheGeometric.RectangleInt target, IEnumerator<TheGeometric.RectangleInt> rectangles)
    {
        while (rectangles.MoveNext())
        {
            var rect = rectangles.Current;

            if (target == rect)
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
    internal Models.TileIdOrEmpty UsableId { get; private set; } = new Models.TileIdOrEmpty(1);
    #endregion

    // - インターナル・メソッド

    #region メソッド（タイルの追加）
    /// <summary>
    ///     タイルの追加
    /// </summary>
    /// <param name="id">タイルＩｄ</param>
    /// <param name="rect">位置とサイズ</param>
    /// <param name="tileTitle">タイル・タイトル</param>
            // TODO 論理削除は難しいから廃止予定
    /// <param name="logicalDelete">論理削除</param>
    internal void AddTile(
        Models.TileIdOrEmpty id,
        Geometric.RectangleInt rect,
        Models.TileTitle tileTitle,
        Models.LogicalDelete logicalDelete)
    {
        this.RecordList.Add(
            new TileRecord(
                id,
                rect,
                tileTitle,
                logicalDelete));
    }
    #endregion

    // TODO 論理削除は難しいから廃止予定
    #region メソッド（タイルの論理削除）
    /// <summary>
    ///     タイルの論理削除
    /// </summary>
    /// <param name="id">タイルＩｄ</param>
    internal void DeleteLogical(
        Models.TileIdOrEmpty id)
    {
        // 愚直な検索
        for (int i = 0; i < this.RecordList.Count; i++)
        {
            var record = this.RecordList[i];

            if (record.Id == id)
            {
                // 差替え
                this.RecordList[i] = new TileRecord(
                    id: record.Id,
                    rect: record.Rectangle,
                    title: record.Title,
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

    #region メソッド（全てのレコードを取得）
    /// <summary>
    ///     全てのレコードを取得
    /// </summary>
    /// <returns>ストリーム</returns>
    internal IEnumerator<TileRecord> GetAllRecords(bool includeLogicalDelete = false)
    {
        foreach (var record in this.RecordList)
        {
            // TODO 論理削除は難しいから廃止予定
            // 論理削除されているものは除く
            if (!includeLogicalDelete && record.LogicalDelete == LogicalDelete.True)
            {
                continue;
            }

            // レコードを１件返す
            yield return record;
        }
    }
    #endregion

    #region メソッド（全ての矩形を取得）
    /// <summary>
    ///     全ての矩形を取得
    /// </summary>
    /// <returns>ストリーム</returns>
    internal IEnumerator<TheGeometric.RectangleInt> GetAllRectangles(bool includeLogicalDelete = false)
    {
        foreach (var record in this.RecordList)
        {
            // TODO 論理削除は難しいから廃止予定
            // 論理削除されているものは除く
            if (!includeLogicalDelete && record.LogicalDelete == LogicalDelete.True)
            {
                continue;
            }

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
        return TilesetDatatable.HasIntersection(target, this.GetAllRectangles());
    }
    #endregion

    #region メソッド（指定の矩形は、登録されている矩形のいずれかと合同か？）
    /// <summary>
    ///     指定の矩形は、登録されている矩形のいずれかと合同か？
    /// </summary>
    /// <param name="target">矩形</param>
    /// <returns>そうだ</returns>
    internal bool IsCongruence(TheGeometric.RectangleInt target)
    {
        return TilesetDatatable.IsCongruence(target, this.GetAllRectangles());
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
        // TODO 論理削除は難しいから廃止予定
        // 論理削除されているものも妥当性チェックで使う
        return TilesetDatatable.IsValid(this.CreateTileRecordList(includeLogicalDelete: true));
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
    internal bool SaveCSV(DataCsvTilesetCsv tileSetSettingsFile)
    {
        // TODO 論理削除は難しいから廃止予定
        // 論理削除されているものも保存する
        return TilesetDatatable.SaveCSV(
            tileSetSettingsFileLocation: tileSetSettingsFile,
            recordList: this.GetAllRecords(includeLogicalDelete: true));
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

        foreach (var record in this.RecordList)
        {
            // TODO 論理削除は難しいから廃止予定
            // 論理削除されているものは除く
            if (!includeLogicalDelete && record.LogicalDelete == LogicalDelete.True)
            {
                continue;
            }

            list.Add(new TileRecord(
                id: record.Id,
                rect: record.Rectangle,
                title: record.Title,
                logicalDelete: record.LogicalDelete));
        }

        return list;
    }
    #endregion
}
