namespace _2D_RPG_Negiramen.Models;

using System.Diagnostics;
using System.Text.RegularExpressions;

/// <summary>
///     UUID ヘルパー
/// </summary>
internal static class UUIDHelper
{
    // - インターナル静的メソッド

    #region メソッド（UUID か？）
    /// <summary>
    ///     UUID か？
    /// </summary>
    /// <param name="text"></param>
    /// <returns>そうだ</returns>
    internal static bool IsMatch(string text)
    {
        bool isUUID = uuidPattern.IsMatch(text);

        // Trace.WriteLine($"[UUIDHelper.cs IsMatch] isUUID: {isUUID}, text: [{text}]");

        return isUUID;
    }
    #endregion

    // - プライベート静的プロパティ

    #region プロパティ（UUID パターン）
    /// <summary>
    ///     UUID パターン
    ///     
    ///     <list type="bullet">
    ///         <item>大文字と小文字を区別し、大文字を使うものとする</item>
    ///     </list>
    /// </summary>
    static Regex uuidPattern = new Regex("([0-9A-F]{8})-([0-9A-F]{4})-([0-9A-F]{4})-([0-9A-F]{4})-([0-9A-F]{12})");
    #endregion
}
