namespace _2D_RPG_Negiramen.Models
{
    /// <summary>
    ///     リソース・ヘルパー
    /// </summary>
    internal static class ResourcesHelper
    {
        // - インターナル静的プロパティ

        /// <summary>
        ///     白
        /// </summary>
        internal static Color GentleWhite
        {
            get
            {
                if (gentleWhite == null)
                {
                    if (ResourcesHelper.TryFind("GentleWhite", out var color))
                    {
                        gentleWhite = (Color)color;
                    }
                    else
                    {
                        throw new Exception($"[ResourceHelper.cs GentleWhite] not found");
                    }
                }

                return gentleWhite;
            }
        }

        /// <summary>
        ///     基調色
        /// </summary>
        internal static Color Primary
        {
            get
            {
                if (primary==null)
                {
                    if (ResourcesHelper.TryFind("Primary", out var color))
                    {
                        primary = (Color)color;
                    }
                    else
                    {
                        throw new Exception($"[ResourceHelper.cs Primary] not found");
                    }
                }

                return primary;
            }
        }

        // - インターナル静的メソッド

        #region メソッド（探す）
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
        #endregion

        // プライベート静的フィールド

        static Color? gentleWhite;
        static Color? primary;
    }
}
