using System.Collections.Generic;

namespace EPiVue
{
    public interface IVueBlock
    {
        /// <summary>
        /// The Vue.js web component name.
        /// This should match the file name of the vue component.
        /// Ex: For a component 'VueComponent1.vue' the name would be 'VueComponent1'
        /// </summary>
        string VueComponentName { get; }
        /// <summary>
        /// Props to be passed into the Vue component instance.
        /// </summary>
        IDictionary<string, object> VueComponentProps { get; }
        /// <summary>
        /// Raw HTML string to be rendered inside the default slot of the Vue component instance.
        /// </summary>
        string SlotContentString { get; }
        /// <summary>
        /// A list of names and content to be rendered inside the Vue component instance as named slots.
        /// </summary>
        IList<IVueBlockNamedSlotContent> NamedSlotContents { get; }
    }
}
