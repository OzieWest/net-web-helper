using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebHelper.Tests
{
    [TestClass]
    public class SimpleTagBuilderTests
    {
        [TestMethod]
        public void Should_create_clean_element()
        {
            var result = String.Empty;
            using (var sut = new SimpleTagBuilder()) 
            {
                sut.Tag("div");
                result = sut.Render();
            }

            Assert.AreEqual(result, "<div></div>");
        }

        [TestMethod]
        public void Should_create_element_with_attr()
        {
            var result = String.Empty;
            using (var sut = new SimpleTagBuilder())
            {
                sut.Tag("div", withAttributes: new { id = "testId", @class = "testClass" });
                result = sut.Render();
            }

            Assert.AreEqual(result, "<div id='testId' class='testClass'></div>");
        }

        [TestMethod]
        public void Should_create_element_with_nested_element()
        {
            var result = String.Empty;
            using (var sut = new SimpleTagBuilder())
            {
                sut.Tag("div", withChildren: () => 
                {
                    sut.Tag("span", withChildren: () =>
                    {
                        sut.Text("hello world");
                    });
                });
                result = sut.Render();
            }

            Assert.AreEqual(result, "<div><span>hello world</span></div>");
        }

        [TestMethod]
        public void Should_create_selfclose_element()
        {
            var result = String.Empty;
            using (var sut = new SimpleTagBuilder())
            {
                sut.Tag("br", selfClose: true);
                result = sut.Render();
            }

            Assert.AreEqual(result, "<br/>");
        }
    }
}

