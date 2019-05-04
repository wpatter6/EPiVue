using System.Collections.Generic;
using System.Configuration;
using System.Xml;

namespace EPiVue
{
    public class VueConfig : ConfigurationSection
    {
        private VueConfig() { }

        [ConfigurationProperty("appUrl")]
        public VueSettingsString AppUrl
        {
            get => (VueSettingsString)this["appUrl"];
            set => this["appUrl"] = value;
        }

        [ConfigurationProperty("appPrefix")]
        public VueSettingsString AppPrefix
        {
            get => (VueSettingsString)this["appPrefix"];
            set => this["appPrefix"] = value;
        }

        [ConfigurationProperty("vueUrl")]
        public VueSettingsString VueUrl
        {
            get => (VueSettingsString)this["vueUrl"];
            set => this["vueUrl"] = value;
        }

        [ConfigurationProperty("components")]
        public VueSettingsComponents Components
        {
            get => (VueSettingsComponents)this["components"];
            set => this["components"] = value;
        }

        public static VueConfig GetVueSettings()
        {
            return (VueConfig)ConfigurationManager.GetSection("episerver.vue");
        }
    }

    public class VueSettingsString : ConfigurationElement, IConfigurationSectionHandler
    {
        public string Value;
        public object Create(object parent, object configContext, XmlNode section)
        {
            Value = section.Value;
            return section.Value;
        }
    }

    public class VueSettingsComponents : ConfigurationElement, IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section)
        {
            var result = new List<string>();
            foreach (XmlNode childNode in section.ChildNodes)
            {
                result.Add(childNode.Value);
            }
            return result;
        }
    }
}