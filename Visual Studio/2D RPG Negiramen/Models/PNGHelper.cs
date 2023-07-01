﻿/// <summary>
///     😁 モデル
/// </summary>
namespace _2D_RPG_Negiramen.Models
{
    /// <summary>
    ///     PNG形式画像ファイルのヘルパー
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
        /// <param name="filePath">ファイル・パス</param>
        internal static Models.Size GetImageSize(string filePath)
        {
            uint w, h;

            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                fs.Seek(16, SeekOrigin.Begin);
                byte[] buf = new byte[8];
                fs.Read(buf, 0, 8);

                w = (((uint)buf[0] << 24) | ((uint)buf[1] << 16) | ((uint)buf[2] << 8) | ((uint)buf[3]));
                h = (((uint)buf[4] << 24) | ((uint)buf[5] << 16) | ((uint)buf[6] << 8) | ((uint)buf[7]));

            }

            return new Models.Size(new Models.Width((int)w), new Models.Height((int)h));
        }
    }
}
