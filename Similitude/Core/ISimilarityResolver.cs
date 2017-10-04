using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Similitude.Core
{
    [PublicAPI]
    public interface ISimilarityResolver<in T>
    {
        double GetSimilarity(T first, T second);
    }
}