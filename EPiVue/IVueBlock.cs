using EPiServer.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPiVue
{
    public interface IVueBlock
    {
        string VueComponentName { get; }
        string VueComponentProps { get; }
        XhtmlString SlotContent { get; }
        IList<VueBlockNamedSlotContent> NamedSlotContents { get; }
    }
}
