using JetBrains.Annotations;

namespace Similitude.Core
{
    [PublicAPI]
    public interface ISimilarityResolver<in T>
    {
        /// <summary>
        /// Compares two elements of arbitrary type <typeparamref name="T"/> and resolves a normalized result
        /// indicating their similarity. The larger the result, the more similar the elements are determined to be.
        /// The result is 1 if and only if the elements are equal.
        /// </summary>
        double GetSimilarity(T first, T second);
    }
}