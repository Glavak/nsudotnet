﻿using JSConverter;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSConverterTests
{
    [TestClass]
    public class UnitTestsFor6
    {
        [TestMethod]
        public void TestSimpleConstant()
        {
            string result = Js.Convert(() => 2 * 2);

            Assert.AreEqual("function () { return 4; }", result);
        }

        [TestMethod]
        public void TestSimpleOperators()
        {
            string result = Js<int>.Convert(x => x + 1 * x);

            Assert.AreEqual("function (x) { return (x + (1 * x)); }", result);
        }

        [TestMethod]
        public void TestAdvancedOperators()
        {
            string result = Js<int, int>.Convert((x, y) => (x % y / 2) >> 5);

            Assert.AreEqual("function (x, y) { return (((x % y) / 2) >> 5); }", result);
        }

        [TestMethod]
        public void TestCoalesceOperator()
        {
            string result = Js<User>.Convert(u => u.Balance ?? 0);

            Assert.AreEqual("function (u) { return ((u.Balance == null || u.Balance == undefined) ? 0 : u.Balance); }", result);
        }

        [TestMethod]
        public void TestConditionalOperator()
        {
            string result = Js<int>.Convert(x => x > 2 ? 3 : 1);

            Assert.AreEqual("function (x) { return ((x > 2) ? 3 : 1); }", result);
        }

        [TestMethod]
        public void TestNull()
        {
            string result = Js<User>.Convert(u => u.Name == null);

            Assert.AreEqual("function (u) { return (u.Name == null); }", result);
        }

        [TestMethod]
        public void TestMemberAccess()
        {
            string result = Js<User>.Convert(u => u.Age);

            Assert.AreEqual("function (u) { return u.Age; }", result);
        }

        [TestMethod]
        public void TestArrayIndexer()
        {
            string result = Js<int[]>.Convert(x => x[5]);

            Assert.AreEqual("function (x) { return x[5]; }", result);
        }

        [TestMethod]
        public void TestArrayIndexerObject()
        {
            string result = Js<User>.Convert(u => u.Name[5]);

            Assert.AreEqual("function (u) { return u.Name[5]; }", result);
        }

        [TestMethod]
        public void TestObjectInitializer()
        {
            string result = Js<User>.Convert(u => new { NormalizedAge = u.Age - 18, UpperName = u.Name.ToUpper() });

            Assert.AreEqual("function (u) { return { NormalizedAge: (u.Age - 18), UpperName: u.Name.toUpperCase() }; }", result);
        }

        [TestMethod]
        public void TestFuncName()
        {
            string result = Js<int>.Convert(x => x, "SomeName");

            Assert.AreEqual("function SomeName(x) { return x; }", result);
        }
    }
}
