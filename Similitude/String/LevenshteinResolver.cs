using JetBrains.Annotations;

namespace Similitude.String
{
    /// <summary>
    /// A similarity and distance resolver that uses the 'Levenshtein distance' algorithm, also known as the
    /// 'Edit distance'. This is a common variation of the <see cref="WeightedLevenshteinDistanceResolver"/>
    /// with predefined weights.
    /// </summary>
    [PublicAPI]
    public class LevenshteinDistanceResolver : WeightedLevenshteinDistanceResolver
    {
        /// <summary>
        /// A similarity and distance resolver that uses the 'Levenshtein distance' algorithm, also known as the
        /// 'Edit distance'. This is a common variation of the <see cref="WeightedLevenshteinDistanceResolver"/>
        /// with predefined weights. The weight of each individual edit type is set to 1.
        /// </summary>
        /// <param name="caseSensitive">Determines whether equality checks are case sensitive.</param>
        public LevenshteinDistanceResolver(bool caseSensitive = false)
            : base(1, 1, 1, caseSensitive)
        {
        }
    }
}
