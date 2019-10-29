using ExpressionTrees.Task2.ExpressionMapping.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExpressionTrees.Task2.ExpressionMapping.Tests
{
    [TestClass]
    public class ExpressionMappingTests
    {
        // todo: add as many test methods as you wish, but they should be enough to cover basic scenarios of the mapping generator

        [TestMethod]
        public void TestMethod1()
        {
            var mapGenerator = new MappingGenerator();
            var mapper = mapGenerator.Generate<Foo, Bar>();

            var res = mapper.Map(new Foo());
        }

        [TestMethod]
        public void Map()
        {
            var mapGenerator = new MappingGenerator();
            var mapper = mapGenerator.Generate<Foo, Bar>();

            Foo foo = new Foo() { HowOldYouOnTracker = 2, DoYouLikeWorkOnAProject = false, Why = "Special thanks to Dan Paladino, to new method of work and to great help desk team.. oh sorry, ignore desk team.." };
            Bar bar = mapper.Map(foo);

            Assert.AreEqual(foo.HowOldYouOnTracker, bar.HowOldYouOnTracker);
            Assert.AreEqual(foo.DoYouLikeWorkOnAProject, bar.DoYouLikeWorkOnAProject);
            Assert.AreEqual(foo.Why, bar.Why);
        }

     
    }
}
