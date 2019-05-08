using System.Collections.Generic;

namespace EPiVue
{
    public interface IVueBlock
    {
        string VueComponentName { get; }
        IDictionary<string, object> VueComponentProps { get; }
        string SlotContentString { get; }
        IList<IVueBlockNamedSlotContent> NamedSlotContents { get; }
    }
}
