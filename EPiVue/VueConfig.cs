using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace EPiVue
{
    public class VueConfig : ConfigurationSection
    {
        public static VueConfig Settings => ConfigurationManager.GetSection("vueConfig") as VueConfig;
        private VueConfig() { }

        [ConfigurationProperty("appUrl")]
        public string AppUrl => this["appUrl"] as string;

        [ConfigurationProperty("appPrefix")]
        public string AppPrefix => this["appPrefix"] as string;

        [ConfigurationProperty("vueUrl")]
        public string VueUrl => this["vueUrl"] as string;

        [ConfigurationProperty("components")]
        [ConfigurationCollection(typeof(string), AddItemName = "component")]
        public Components ComponentList => this["components"] as Components;
    }

    public class component : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name => this["name"] as string;
    }

    public class Components : ConfigurationElementCollection, IEnumerable<component>
    {
        public component this[int index]
        {
            get => BaseGet(index) as component;
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        public new component this[string responseString]
        {
            get => BaseGet(responseString) as component;
            set
            {
                if (BaseGet(responseString) != null)
                {
                    BaseRemoveAt(BaseIndexOf(BaseGet(responseString)));
                }
                BaseAdd(value);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new component();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((component)element).Name;
        }

        IEnumerator<component> IEnumerable<component>.GetEnumerator()
        {
            return this.BaseGetAllKeys().Select(key => (component)BaseGet(key)).GetEnumerator();
        }
    }
}