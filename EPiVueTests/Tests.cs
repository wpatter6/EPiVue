using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web.Mvc;
using EPiServer.Core;
using EPiVue;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EPiVueTests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void GetVueConfigSettings()
        {
            VueConfig.Settings.Should().BeEquivalentTo(new
            {
                AppUrl = "/static/js/dist/v-app.js",
                AppPrefix = "v-app",
                VueUrl = "https://unpkg.com/vue@2.6.10",
                ComponentList = new dynamic[]
                {
                    new { Name = "VueTest" },
                    new { Name = "VueTest2" },
                    new { Name = "VueTest3" },
                    new { Name = "VueTest4" },
                }
            });
        }

        [TestMethod]
        public void RenderVueHeadScripts()
        {
            var html = TestUtilities.CreateHtmlHelper<object>();
            var str = html.RenderVueScripts(VueScriptLocation.Head);
            str.Should().BeEquivalentTo(new
            {
                Value = "<link as=\"script\" href=\"https://unpkg.com/vue@2.6.10\" rel=\"preload\"></link><link as=\"script\" href=\"/static/js/dist/v-app.js\" rel=\"preload\"></link>"
            });
        }

        [TestMethod]
        public void RenderVueFootScripts()
        {
            var html = TestUtilities.CreateHtmlHelper<object>();
            var str = html.RenderVueScripts(VueScriptLocation.Foot);
            str.Should().BeEquivalentTo(new 
            {
                Value = "<script src=\"https://unpkg.com/vue@2.6.10\"></script><script src=\"/static/js/dist/v-app.js\"></script>"
            });
        }

        [TestMethod]
        public void RenderVueBlock()
        {
            var html = TestUtilities.CreateHtmlHelper<VueBlock>();
            var block = new VueBlock()
            {
                VueComponentName = "VueTest",
                VueComponentProps = "{ prop: 'abcd' }",
                SlotContent = new XhtmlString("<p>Hello</p>"),
                NamedSlotContents = new List<VueBlockNamedSlotContent>()
                {
                    new VueBlockNamedSlotContent
                    {
                        Name = "left-banner",
                        TagName = "v-app-vue-test-2"
                    }
                }
            };

            var result = html.RenderVueBlock(block);
            result.Should().BeEquivalentTo(new
            {
                Value = "<v-app-vue-test prop=\"abcd\"><p>Hello</p><v-app-vue-test-2 slot=\"left-banner\"></v-app-vue-test-2></v-app-vue-test>"
            });
        }
    }
}
