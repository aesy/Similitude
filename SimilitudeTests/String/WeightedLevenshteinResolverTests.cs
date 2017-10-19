using System;
using NUnit.Framework;
using Similitude.String;

namespace SimilitudeTests.String
{
    [TestFixture]
    public class WeightedLevenshteinDistanceResolverTests
    {
        [Test]
        public void ShouldHaveVariableInsertionWeight()
        {
            var resolver = new WeightedLevenshteinDistanceResolver(3, 1, 2);
            const string str1 = "deja entendu";
            const string str2 = "déjà vu";
            const int expectedDistance = 11;

            Assert.AreEqual(expectedDistance, resolver.GetDistance(str1, str2));
        }

        [Test]
        public void ShouldHaveVariableRemovalWeight()
        {
            var resolver = new WeightedLevenshteinDistanceResolver(1, 3, 2);
            const string str1 = "deja entendu";
            const string str2 = "déjà vu";
            const int expectedDistance = 15;

            Assert.AreEqual(expectedDistance, resolver.GetDistance(str1, str2));
        }

        [Test]
        public void ShouldHaveVariableSubstitutionWeight()
        {
            var resolver = new WeightedLevenshteinDistanceResolver(2, 1, 3);
            const string str1 = "deja entendu";
            const string str2 = "déjà vu";
            const int expectedDistance = 14;

            Assert.AreEqual(expectedDistance, resolver.GetDistance(str1, str2));
        }

        [Test]
        public void ShouldUseWeightsOfOneByDefault()
        {
            var resolver = new WeightedLevenshteinDistanceResolver();
            const string str1 = "deja entendu";
            const string str2 = "déjà vu";
            const int expectedDistance = 8;

            Assert.AreEqual(expectedDistance, resolver.GetDistance(str1, str2));
        }

        [Test]
        public void ShouldBeCaseInsensitiveByDefault()
        {
            var resolver = new WeightedLevenshteinDistanceResolver();
            const string str1 = "so random";
            const string str2 = "sO rANdOm";

            Assert.AreEqual(0, resolver.GetDistance(str1, str2));
        }

        [Test]
        public void ShouldBeAbleToBeCaseSensitive()
        {
            var resolver = new WeightedLevenshteinDistanceResolver(caseSensitive: true);
            const string str1 = "as easy as abc";
            const string str2 = "As easy as ABC";
            const int expectedDistance = 4;

            Assert.AreEqual(expectedDistance, resolver.GetDistance(str1, str2));
        }

        [Test]
        public void ShouldBeAbleToHandleUnicodeCharacters()
        {
            var resolver = new WeightedLevenshteinDistanceResolver();
            const string str1 = "deja entendu";
            const string str2 = "déjà vu";
            const int expectedDistance = 8;

            Assert.AreEqual(expectedDistance, resolver.GetDistance(str1, str2));
        }

        [Test]
        public void ShouldBeAbleToCalculateTheNumberOfEditsRequiredToGoFromOneStringToAnother()
        {
            var resolver = new WeightedLevenshteinDistanceResolver();
            const string str1 = "the robot uprising is nigh";
            const string str2 = "da robo uprising be near";
            const int expectedDistance = 9;

            Assert.AreEqual(expectedDistance, resolver.GetDistance(str1, str2));
        }

        [Test]
        public void ShouldAcceptEmptyInputs()
        {
            var resolver = new WeightedLevenshteinDistanceResolver();

            Assert.AreEqual(0, resolver.GetDistance("", ""));
            Assert.AreEqual(0, resolver.GetSimilarity("", ""));
        }

        [Test]
        public void ShouldBeAbleToGiveASimilarityPercentageBetweenTwoStrings()
        {
            const int maxWeight = 4;
            var resolver = new WeightedLevenshteinDistanceResolver(2, 3, maxWeight);
            const string str1 = "wubba lubba dub dub";
            const string str2 = "yabba dabba doo";
            const int expectedDistance = 28;
            var maxLength = Math.Max(str1.Length, str2.Length);
            var maxDistance = maxLength * maxWeight;

            var expectedSimilarity = (double) (maxDistance - expectedDistance) / maxDistance;
            var similarity = resolver.GetSimilarity(str1, str2);

            Assert.IsTrue(similarity >= 0 && similarity <= 1);
            Assert.AreEqual(expectedSimilarity, similarity, 1e-10);
        }
    }
}
