using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPiServer.Core;
using EPiVue;

namespace EPiVueTests.Models
{
    internal class VueBlockNamedSlotContent : IVueBlockNamedSlotContent
    {
        public string SlotName { get; set; }

        public string TagName { get; set; }

        public XhtmlString Content { get; set; }

        public string ContentHtml => Content != null ? Content.ToEditString() : string.Empty;
    }
}
