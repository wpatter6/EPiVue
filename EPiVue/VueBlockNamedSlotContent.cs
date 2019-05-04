using EPiServer.Core;
using EPiServer.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace EPiVue
{
    public class VueBlockNamedSlotContent : IVueBlockNamedSlotContent
    {
        [Display(Name = "Container Name",
            Description = "The name of the vue component's slot for inserting content.  The component must be built with the named slots for this content to be visible.",
            Order = 10)]
        [CultureSpecific]
        public string Name { get; set; }

        [Display(Name = "Content",
            Description = "Content to place inside the vue component's named slot(s).  The component must be built with the named slots for this content to be visible.",
            Order = 20)]
        [CultureSpecific]
        public XhtmlString Content { get; set; }

        [Display(Name = "Container Tag Name",
            Description = "The tag name of the vue component's slot for inserting content.  Leaving this blank will result in 'div'.",
            Order = 30)]
        [CultureSpecific]
        public string TagName { get; set; }

        [Display(Name = "Container Tag CssClass",
            Description = "The class name of the vue component's slot for inserting content.",
            Order = 40)]
        [CultureSpecific]
        public string CssClass { get; set; }
    }
}