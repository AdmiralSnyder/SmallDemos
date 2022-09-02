namespace AgorithmTests
{
    public class UnitTest1
    {
        [Fact]
        public void GetSecondThingReturnsTheSecondThing()
        {
            var second = AlgorithmsTestTarget.Algorithms.GetSecondThing(new[] { "one", "two", "three"});
            Assert.Equal("two", second);
        }

        [Fact]
        public void GetSecondThingThrowsIfNull()
        {
            Assert.Throws<NullReferenceException>(() => AlgorithmsTestTarget.Algorithms.GetSecondThing(null));
        }

        [Theory]
        [InlineData("two", "one", "two", "three", "four")]
        [InlineData("two", "one", "two", "three")]
        [InlineData("  ", "   ", "  ", "three")]
        [InlineData("  ", "   ", "  ")]
        public void GetSecondThingReturnsTheSecondThingAlways(string expected, params string[] values)
        {
            var second = AlgorithmsTestTarget.Algorithms.GetSecondThing(values);
            Assert.Equal(expected, second);
        }


        [Fact]
        public void GetSecondThingThrowsIfOneThingOnly()
        {
            Assert.Throws<IndexOutOfRangeException>(() => AlgorithmsTestTarget.Algorithms.GetSecondThing(new[] { "One"}));
        }
    }
}