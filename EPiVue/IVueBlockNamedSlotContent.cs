namespace EPiVue
{
    public interface IVueBlockNamedSlotContent
    {
        /// <summary>
        /// The named slot's name
        /// </summary>
        string Name { get; }
        /// <summary>
        /// The tag name of the top level element of the slot.  The default is 'div'.
        /// </summary>
        string TagName { get; }
        /// <summary>
        /// The raw HTML string to be rendered within the named slot.
        /// </summary>
        string ContentString { get; }
    }
}
