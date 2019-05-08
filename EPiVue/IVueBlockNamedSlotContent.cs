namespace EPiVue
{
    public interface IVueBlockNamedSlotContent
    {
        string Name { get; }
        string TagName { get; }
        string ContentString { get; }
    }
}
