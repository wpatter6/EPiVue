using System;
using EPiServer.Web.Mvc.Html;
using EPiVue.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Html;

namespace EPiVue
{
    public static class VueBlockHtmlHelpers
    {
        public static HtmlString RenderVueBlock(this HtmlHelper<VueBlock> helper, IVueBlock vueBlock)
        {
            var tagName = vueBlock.VueComponentName.ComponentToTagName();
            var outerElement = new TagBuilder(tagName)
            {
                InnerHtml = ""
            };

            if (!string.IsNullOrEmpty(vueBlock.VueComponentProps))
            {

                var propObj = JsonConvert.DeserializeObject<Dictionary<string, object>>(vueBlock.VueComponentProps);

                foreach(var item in propObj)
                {
                    var attr = item.Key.PascalToKebabCase();
                    if (!(item.Value is string valString))
                    {
                        attr = ":" + attr;
                        valString = JsonConvert.SerializeObject(item.Value);
                    }

                    outerElement.MergeAttribute(attr, valString);
                }
            }

            if (vueBlock.SlotContent != null)
            {
                outerElement.InnerHtml += vueBlock.SlotContent.ToEditString();
            }

            vueBlock.NamedSlotContents?.ToList().ForEach(content =>
            {
                var slotTag = new TagBuilder(string.IsNullOrEmpty(content.TagName) ? "div" : content.TagName);
                if (!string.IsNullOrEmpty(content.CssClass))
                {
                    slotTag.MergeAttribute("class", content.CssClass);
                }

                slotTag.MergeAttribute("slot", content.Name);

                if (content.Content != null)
                {
                    slotTag.InnerHtml += content.Content.ToEditString();
                }

                outerElement.InnerHtml += slotTag.ToString();
            });

            return new HtmlString(outerElement.ToString());
        }
        public static HtmlString RenderVueScripts(this HtmlHelper helper, VueScriptLocation location)
        {
            var vueLink = GetStaticElement(location, VueConfig.Settings.VueUrl);
            var appLink = GetStaticElement(location, VueConfig.Settings.AppUrl);

            return new HtmlString($"{vueLink}{appLink}");
        }
        private static TagBuilder GetStaticElement(VueScriptLocation location, string url)
        {
            switch (location)
            {
                case VueScriptLocation.Head:
                    var headTag = new TagBuilder("link");
                    headTag.MergeAttributes(new Dictionary<string, string>()
                    {
                        {"rel", "preload" },
                        {"as", "script" },
                        {"href", url }
                    });
                    return headTag;
                case VueScriptLocation.Foot:
                    var bodyTag = new TagBuilder("script");
                    bodyTag.MergeAttributes(new Dictionary<string, string>()
                    {
                        {"src", url }
                    });
                    return bodyTag;
                default:
                    throw new ArgumentOutOfRangeException(nameof(location), location, null);
            }
        }
    }

    public enum VueScriptLocation
    {
        Head,
        Foot
    }
}