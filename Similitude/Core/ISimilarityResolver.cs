using JetBrains.Annotations;

namespace Similitude.Core
{
    [PublicAPI]
    public interface ISimilarityResolver<in T>
    {
        /// <summary>
        /// Compares two elements of arbitrary type <typeparamref name="T"/> and resolves a normalized result
        /// indicating their similarity.
        /// </summary>
        /// <param name="first">The first element.</param>
        /// <param name="second">The second element.</param>
        /// <returns>A number in the range [0, 1].</returns>
        double GetSimilarity(T first, T second);
    }
}