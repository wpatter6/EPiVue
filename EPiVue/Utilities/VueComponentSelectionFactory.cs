using EPiServer.Shell.ObjectEditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPiVue.Utilities
{
    internal class VueComponentSelectionFactory : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            //todo move this to web config
            return new[] { "VueTest", "VueTest2", "VueTest3", "FlowRouter", "FlowPage", "VuePage1", "VuePage2", "VuePage3" }.Select(x => new SelectItem { Text = x, Value = x });
        }
    }
}
