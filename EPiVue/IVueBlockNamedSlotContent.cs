using EPiServer.Core;

namespace EPiVue
{
    public interface IVueBlockNamedSlotContent
    {
        string Name { get; }
        string TagName { get; }
        XhtmlString Content { get; }
    }
}
