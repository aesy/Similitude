using System;
using JetBrains.Annotations;
using Similitude.Core;

namespace Similitude.String
{
    [PublicAPI]
    public class LevenshteinDistanceResolver : ISimilarityResolver<string>
    {
        protected bool CaseSensitive;
        protected int RemovalWeight = 1;
        protected int InsertionWeight = 1;
        protected int SubstitutaionWeight = 1;

        public LevenshteinDistanceResolver(bool caseSensitive = false)
        {
            CaseSensitive = caseSensitive;
        }

        public int GetDistance([NotNull] string first, [NotNull] string second)
        {
            var n = first.Length;
            var m = second.Length;

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
                    var xChar = first[x - 1];
                    var yChar = second[y - 1];

                    if (!CaseSensitive)
                    {
                        xChar = char.ToLowerInvariant(xChar);
                        yChar = char.ToLowerInvariant(yChar);
                    }

                    var removalCost = matrix[x - 1, y] + RemovalWeight;
                    var insertionCost = matrix[x, y - 1] + InsertionWeight;
                    var substitutaionCost = matrix[x - 1, y - 1] + Convert.ToInt32(xChar != yChar) * SubstitutaionWeight;

                    matrix[x, y] = Math.Min(removalCost, Math.Min(insertionCost, substitutaionCost));
                }
            }

            return matrix[n, m];
        }

        public double GetSimilarity([NotNull] string x, [NotNull] string y)
        {
            var maxLength = Math.Max(x.Length, y.Length);

            if (maxLength == 0)
            {
                return 0;
            }

            var distance = GetDistance(x, y);
            var normalizedSimilarity = (double)(maxLength - distance) / maxLength;

            return normalizedSimilarity;
        }
    }
}
