using _2D_RPG_Negiramen.Models.FileEntries.Locations;

namespace _2D_RPG_Negiramen.Models.FileEntries
{
    /// <summary>
    ///     😁 PNG形式画像ファイルのヘルパー
    ///     
    ///     <list type="bullet">
    ///         <item>📖 [【C#】PNG画像サイズの取得方法](https://zenn.dev/alfina2538/articles/998e406272f0fd)</item>
    ///     </list>
    /// </summary>
    static class PNGHelper
    {
        /// <summary>
        ///     PNG形式画像ファイルのサイズを取得する
        /// </summary>
        /// <param name="fileLocation">ファイルの場所</param>

/* プロジェクト '2D RPG Negiramen (net7.0-windows10.0.19041.0)' からのマージされていない変更
前:
        internal static Models.SizeInt GetImageSize(Its fileLocation)
後:
        internal static SizeInt GetImageSize(Its fileLocation)
*/
        internal static Geometric.SizeInt GetImageSize(Its fileLocation)
        {
            uint w, h;

            using (var fs = new FileStream(fileLocation.Path.AsStr, FileMode.Open, FileAccess.Read))
            {
                fs.Seek(16, SeekOrigin.Begin);
                byte[] buf = new byte[8];
                fs.Read(buf, 0, 8);

                w = (((uint)buf[0] << 24) | ((uint)buf[1] << 16) | ((uint)buf[2] << 8) | ((uint)buf[3]));
                h = (((uint)buf[4] << 24) | ((uint)buf[5] << 16) | ((uint)buf[6] << 8) | ((uint)buf[7]));

            }

            return new Models.Geometric.SizeInt(new Models.Geometric.WidthInt((int)w), new Models.Geometric.HeightInt((int)h));
        }
    }
}
