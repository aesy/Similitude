using JetBrains.Annotations;

namespace Similitude.Core
{
    [PublicAPI]
    public interface IDistanceResolver<in T>
    {
        /// <summary>
        /// Analyzes two elements of arbitrary type <typeparamref name="T"/> and resolves a distance measure between
        /// them. The result is zero if and only if the elements are equal. The ordering of elements does not affect
        /// the result. The largest possible result is implementation dependent.
        /// </summary>
        double GetDistance(T first, T second);
    }
}