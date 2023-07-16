namespace _2D_RPG_Negiramen.Models
{
    /// <summary>
    ///     数学的なもの
    /// </summary>
    internal static class Math
    {
        /// <summary>
        ///     配列をシャッフルする
        ///     
        ///     <list type="bullet">
        ///         <item>Fisher–Yates shuffle</item>
        ///         <item>📖 [Knuth shuffle](https://rosettacode.org/wiki/Knuth_shuffle#C.23)</item>
        ///     </list>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        public static void FisherYatesShuffle<T>(T[] array)
        {
            System.Random random = new System.Random();
            for (int i = 0; i < array.Length; i++)
            {
                int j = random.Next(i, array.Length); // Don't select from the entire array on subsequent loops
                T temp = array[i]; array[i] = array[j]; array[j] = temp;
            }
        }
    }
}
