using EPiServer.Cms.Shell.UI.ObjectEditing.EditorDescriptors;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Web;
using EPiVue.Utilities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EPiVue
{
    [ContentType(DisplayName = "Vue Component",
        GUID = "58fe24ed-95e4-4c8f-8a27-1a6237146ba3",
        Description = "Block for housing a pre-built vue component.",
        GroupName = SystemTabNames.Content)]
    public class VueBlock : BlockData, IVueBlock
    {
        [Required]
        [Display(Name = "Component",
            GroupName = SystemTabNames.Content,
            Description = "The name of the component from the Vue.js project to use in the block.",
            Order = 10)]
        [CultureSpecific]
        [SelectOne(SelectionFactoryType = typeof(VueComponentSelectionFactory))]
        public virtual string VueComponentName { get; set; }


        [Display(Name = "Properties",
            GroupName = SystemTabNames.Content,
            Description = "The props to pass into the vue component as a JSON object.  The component must be built to accept the props that are passed in for them to be used.",
            Order = 20)]
        [CultureSpecific]
        [UIHint(UIHint.Textarea)]
        public virtual string VueComponentProps { get; set; }

        [Display(Name = "Inner Content",
            GroupName = SystemTabNames.Content,
            Description = "Content to place inside the vue component's default slot.  The component must be built with a default slot for this content to be visible.",
            Order = 30)]
        [CultureSpecific]
        public virtual XhtmlString SlotContent { get; set; }

        [Display(Name = "Named Content",
            GroupName = SystemTabNames.Content,
            Description = "Content to place inside the vue component's named slot(s).  The component must be built with named slots for this content to be visible.",
            Order = 40)]
        [CultureSpecific]
        [EditorDescriptor(EditorDescriptorType = typeof(CollectionEditorDescriptor<VueBlockNamedSlotContent>))]
        public virtual IList<VueBlockNamedSlotContent> NamedSlotContents { get; set; }
    }
}
