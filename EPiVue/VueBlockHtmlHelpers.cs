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
        public static HtmlString RenderVueBlock(this HtmlHelper<VueBlock> helper, IVueBlock vueBlock, string namedSlotTagName = "div")
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
                slotTag.InnerHtml += content.Content.ToEditString();
                outerElement.InnerHtml += slotTag.ToString();
            });

            return new HtmlString(outerElement.ToString());
        }
    }
}