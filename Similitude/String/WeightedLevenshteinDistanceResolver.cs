using System;
using JetBrains.Annotations;
using Similitude.Core;

namespace Similitude.String
{
    /// <summary>
    /// A similarity and distance resolver that uses the 'Levenshtein distance' algorithm, also known as the
    /// 'Edit distance'.
    /// </summary>
    [PublicAPI]
    public class WeightedLevenshteinDistanceResolver : ISimilarityResolver<string>
    {
        public int InsertionWeight { get; }
        public int RemovalWeight { get; }
        public int SubstitutaionWeight { get; }
        public bool CaseSensitive { get; }

        /// <summary>
        /// A similarity and distance resolver that uses the 'Levenshtein distance' algorithm, also known as the
        /// 'Edit distance'. The weights of each individual edit type is variable.
        /// </summary>
        /// <param name="insertionWeight">The weight of a character insertion.</param>
        /// <param name="removalWeight">The weight of a character removal.</param>
        /// <param name="subsitutionWeight">The weight of a character substitution.</param>
        /// <param name="caseSensitive">Determines whether equality checks are case sensitive.</param>
        public WeightedLevenshteinDistanceResolver(
            int insertionWeight = 1,
            int removalWeight = 1,
            int subsitutionWeight = 1,
            bool caseSensitive = false)
        {
            InsertionWeight = insertionWeight;
            RemovalWeight = removalWeight;
            SubstitutaionWeight = subsitutionWeight;
            CaseSensitive = caseSensitive;
        }

        /// <summary>
        /// Measures the difference between two strings by counting the minimal number of single-character edits
        /// required to transform one string to another. Valid edit types are 'insertion', 'deletion' and
        /// 'substitution'.
        /// The resultant is zero if and only if the provided strings are identical.
        /// The resultant will never exceed the length of the longest provided string.
        /// </summary>
        /// <param name="source">The source string.</param>
        /// <param name="target">The target string.</param>
        /// <returns>A number in the range [0, Math.max(first.Length, second.Length)].</returns>
        public virtual int GetDistance([NotNull] string source, [NotNull] string target)
        {
            var n = source.Length;
            var m = target.Length;

            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            var matrix = new int[n + 1, m + 1];

            for (var i = 0; i <= n; i++)
            {
                matrix[i, 0] = i;
            }

            for (var i = 0; i <= m; i++)
            {
                matrix[0, i] = i;
            }

            for (var x = 1; x <= n; x++)
            {
                for (var y = 1; y <= m; y++)
                {
                    var xChar = source[x - 1];
                    var yChar = target[y - 1];

                    if (!CaseSensitive)
                    {
                        xChar = char.ToLowerInvariant(xChar);
                        yChar = char.ToLowerInvariant(yChar);
                    }

                    var removalCost = matrix[x - 1, y] + RemovalWeight;
                    var insertionCost = matrix[x, y - 1] + InsertionWeight;
                    var substitutaionCost =
                        matrix[x - 1, y - 1] + Convert.ToInt32(xChar != yChar) * SubstitutaionWeight;

                    matrix[x, y] = Math.Min(removalCost, Math.Min(insertionCost, substitutaionCost));
                }
            }

            return matrix[n, m];
        }

        /// <summary>
        /// Compares two string and resolves a normalized value indicating their similarity.
        /// The result is based on the number of single-character edits <see cref="GetDistance"/> between two strings
        /// in relation to the maximum number of single-character edits possible for the longest provided string.
        /// </summary>
        /// <param name="first">The first string.</param>
        /// <param name="second">The second string.</param>
        /// <returns>A number in the range [0, 1].</returns>
        public virtual double GetSimilarity([NotNull] string first, [NotNull] string second)
        {
            var maxWeight = Math.Max(InsertionWeight, Math.Max(RemovalWeight, SubstitutaionWeight));
            var maxLength = Math.Max(first.Length, second.Length);
            var maxEdits = maxLength * maxWeight;

            if (maxEdits == 0)
            {
                return 0;
            }

            var distance = GetDistance(first, second);
            var normalizedSimilarity = (double) (maxEdits - distance) / maxEdits;

            return normalizedSimilarity;
        }
    }
}