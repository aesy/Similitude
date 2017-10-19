using System;
using JetBrains.Annotations;
using Similitude.Core;

namespace Similitude.Numeric
{
    [PublicAPI]
    public class CosineResolver : IDistanceResolver<double[]>, ISimilarityResolver<double[]>
    {
        public double GetDistance([NotNull] double[] first, [NotNull] double[] second)
        {
            return 1 - GetSimilarity(first, second);
        }

        public double GetSimilarity(double[] first, double[] second)
        {
            if (first.Length != second.Length)
            {
                throw new ArgumentException(
                    $"Expected arguments to be of same length. {first.Length} != {second.Length}",
                    nameof(first) + ", " + nameof(second));
            }

            if (first.Length == 0)
            {
                throw new ArgumentException("Vector arguments must be at least one-dimensional.");
            }

            var dotProduct = 0.0;
            var firstNorm = 0.0;
            var secondNorm = 0.0;

            for (var i = 0; i < first.Length; i++) {
                dotProduct += first[i] * second[i];
                firstNorm += Math.Pow(first[i], 2);
                secondNorm += Math.Pow(second[i], 2);
            }

            return dotProduct / (Math.Sqrt(firstNorm) * Math.Sqrt(secondNorm));
        }
    }
}