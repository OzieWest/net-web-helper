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
            var _result = String.Empty;
            using (var _create = new SimpleTagBuilder()) 
            {
                _create.Tag("div");
                _result = _create.Render();
            }

            Assert.AreEqual(_result, "<div></div>");
        }

        [TestMethod]
        public void Should_create_element_with_attr()
        {
            var _result = String.Empty;
            using (var _create = new SimpleTagBuilder())
            {
                _create.Tag("div", withAttributes: new { id = "testId", @class = "testClass" });
                _result = _create.Render();
            }

            Assert.AreEqual(_result, "<div id='testId' class='testClass'></div>");
        }

        [TestMethod]
        public void Should_create_element_with_nested_element()
        {
            var _result = String.Empty;
            using (var _create = new SimpleTagBuilder())
            {
                _create.Tag("div", withChildren: () => 
                {
                    _create.Tag("span", withChildren: () =>
                    {
                        _create.Text("hello world");
                    });
                });

                _result = _create.Render();
            }

            Assert.AreEqual(_result, "<div><span>hello world</span></div>");
        }

        [TestMethod]
        public void Should_create_selfContained_element()
        {
            var _result = String.Empty;
            using (var _create = new SimpleTagBuilder())
            {
                _create.Tag("br", selfContained: true);
                _result = _create.Render();
            }

            Assert.AreEqual(_result, "<br/>");
        }

        [TestMethod]
        public void Should_create_element_with_attribute_without_value()
        {
            var _result = String.Empty;
            using (var _create = new SimpleTagBuilder())
            {
                _create.Tag("div", withAttributes: new { ngApp = "" });
                _result = _create.Render();
            }

            Assert.AreEqual(_result, "<div ng-app></div>");
        }
    }
}

