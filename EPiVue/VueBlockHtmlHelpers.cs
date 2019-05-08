using System;
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
        public static HtmlString RenderBlock(this IVueBlock vueBlock)
        {
            var tagName = vueBlock.VueComponentName.ComponentToTagName();
            var outerElement = new TagBuilder(tagName)
            {
                InnerHtml = ""
            };

            if (vueBlock.VueComponentProps.Count > 0)
            {
                foreach(var item in vueBlock.VueComponentProps)
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

            if (vueBlock.SlotContentString != null)
            {
                outerElement.InnerHtml += vueBlock.SlotContentString;
            }

            vueBlock.NamedSlotContents?.ToList().ForEach(content =>
            {
                var slotTag = new TagBuilder(string.IsNullOrEmpty(content.TagName) ? "div" : content.TagName);

                slotTag.MergeAttribute("slot", content.Name);

                if (content.ContentString != null)
                {
                    slotTag.InnerHtml += content.ContentString;
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