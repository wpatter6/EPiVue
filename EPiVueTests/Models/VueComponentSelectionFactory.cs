using EPiServer.Shell.ObjectEditing;
using System.Collections.Generic;
using System.Linq;
using EPiVue;

namespace EPiVueTests.Models
{
    internal class VueComponentSelectionFactory : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            var vueSettings = VueConfig.Settings;
            return vueSettings.ComponentList.Select(x => new SelectItem { Text = x.Name, Value = x.Name });
        }
    }
}
