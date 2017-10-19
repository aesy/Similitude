using System;
using NUnit.Framework;
using Similitude.String;

namespace SimilitudeTests.String
{
    [TestFixture]
    public class LevenshteinDistanceResolverTests
    {
        [Test]
        public void ShouldBeCaseInsensitiveByDefault()
        {
            var resolver = new LevenshteinDistanceResolver();
            const string str1 = "so random";
            const string str2 = "sO rANdOm";

            Assert.AreEqual(0, resolver.GetDistance(str1, str2));
        }

        [Test]
        public void ShouldBeAbleToBeCaseSensitive()
        {
            var resolver = new LevenshteinDistanceResolver(true);
            const string str1 = "as easy as abc";
            const string str2 = "As easy as ABC";
            const int expectedDistance = 4;

            Assert.AreEqual(expectedDistance, resolver.GetDistance(str1, str2));
        }

        [Test]
        public void ShouldBeAbleToHandleUnicodeCharacters()
        {
            var resolver = new LevenshteinDistanceResolver();
            const string str1 = "deja entendu";
            const string str2 = "déjà vu";
            const int expectedDistance = 8;

            Assert.AreEqual(expectedDistance, resolver.GetDistance(str1, str2));
        }

        [Test]
        public void ShouldBeAbleToCalculateTheNumberOfEditsRequiredToGoFromOneStringToAnother()
        {
            var resolver = new LevenshteinDistanceResolver();
            const string str1 = "the robot uprising is nigh";
            const string str2 = "da robo uprising be near";
            const int expectedDistance = 9;

            Assert.AreEqual(expectedDistance, resolver.GetDistance(str1, str2));
        }

        [Test]
        public void ShouldAcceptEmptyInputs()
        {
            var resolver = new LevenshteinDistanceResolver();

            Assert.AreEqual(0, resolver.GetDistance("", ""));
            Assert.AreEqual(0, resolver.GetSimilarity("", ""));
        }

        [Test]
        public void ShouldBeAbleToGiveASimilarityPercentageBetweenTwoStrings()
        {
            const int maxWeight = 1;
            var resolver = new LevenshteinDistanceResolver();
            const string str1 = "wubba lubba dub dub";
            const string str2 = "yabba dabba doo";
            const int expectedDistance = 10;
            var maxLength = Math.Max(str1.Length, str2.Length);
            var maxDistance = maxLength * maxWeight;
            var similarity = resolver.GetSimilarity(str1, str2);

            Assert.IsTrue(similarity >= 0 && similarity <= 1);
            Assert.AreEqual(1 - (double) expectedDistance / maxDistance, similarity, 1e-10);
        }
    }
}
