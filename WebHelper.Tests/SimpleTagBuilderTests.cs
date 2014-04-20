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

        [TestMethod]
        public void Should_create_disabled_element()
        {
            var _result = String.Empty;
            using (var _create = new SimpleTagBuilder())
            {
                _create.Tag("button", disabled: true);
                _result = _create.Render();
            }

            Assert.AreEqual(_result, "<button disabled></button>");
        }

        [TestMethod]
        public void Simple_loop_test()
        {
            var _result = String.Empty;
            using (var _create = new SimpleTagBuilder())
            {
                _create.Tag("ul", withChildren: () => 
                {
                    for (int i = 0; i < 2; i++)
                    {
                        _create.Tag("li", withChildren: () => 
                        {
                            _create.Tag("a", withAttributes: new { @href = @"http://" + i.ToString() }, withChildren: () => 
                            {
                                _create.Text(i.ToString());
                            });
                        });
                    }
                });

                _result = _create.Render();
            }

            Assert.AreEqual(_result, "<ul><li><a href='http://0'>0</a></li><li><a href='http://1'>1</a></li></ul>");
        }

        [TestMethod]
        public void Should_create_table()
        {
            var _result = String.Empty;
            using (var _create = new SimpleTagBuilder())
            {
                _create.Tag("table", withChildren: () =>
                {
                    _create.Tag("tbody", withChildren: () =>
                    {
                        _create.Tag("tr", withChildren: () =>
                        {
                            _create.Tag("td");
                            _create.Tag("td");
                        });

                        _create.Tag("tr", withChildren: () =>
                        {
                            _create.Tag("td");
                            _create.Tag("td");
                        });
                    });    
                });

                _result = _create.Render();
            }

            Assert.AreEqual(_result, "<table><tbody><tr><td></td><td></td></tr><tr><td></td><td></td></tr></tbody></table>");
        }

        [TestMethod]
        public void Should_add_strict_script()
        {
            var _result = String.Empty;
            using (var _create = new SimpleTagBuilder())
            {
                _create.Tag("div", withChildren: () =>
                {
                    _create.Script("alert(123);", useStrict: true);
                });

                _result = _create.Render();
            }

            Assert.AreEqual(_result, "<div><script>'use strict';alert(123);</script></div>");
        }
    }
}

