namespace _2D_RPG_Negiramen.Models
{
    /// <summary>
    ///     リソース・ヘルパー
    /// </summary>
    internal static class ResourcesHelper
    {
        /// <summary>
        ///     探す
        ///     
        ///     <list type="bullet">
        ///         <item>TODO ★ 実装をもっと書いてほしい</item>
        ///     </list>
        /// </summary>
        /// <param name="key">リソースのキー</param>
        /// <param name="resource">見つけたもの</param>
        /// <returns>見つかった</returns>
        internal static bool TryFind(string key, out object resource)
        {
            if (App.Current==null)
            {
                resource = 0;
                return false;
            }

            // 愚直な探索
            foreach (var resourceDictionary in App.Current.Resources.MergedDictionaries)
            {
                if (resourceDictionary.TryGetValue(key, out resource))
                {
                    return true;
                }
            }

            resource = 0;
            return false;
        }
    }
}
