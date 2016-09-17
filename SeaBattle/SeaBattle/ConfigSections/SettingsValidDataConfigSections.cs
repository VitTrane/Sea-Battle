using System.Configuration;

namespace SeaBattle.ConfigSections
{
    public class SettingsValidDataConfigSections : ConfigurationSection
    {
        [ConfigurationProperty("minLengthUsername")]
        public int MinLengthUsername
        {
            get
            {
                var str = (base["minLengthUsername"]).ToString();
                return int.Parse(str);
            }
        }

        [ConfigurationProperty("maxLengthUsername")]
        public int MaxLengthUsername
        {
            get
            {
                var str = (base["maxLengthUsername"]).ToString();
                return int.Parse(str);
            }
        }

        [ConfigurationProperty("minLengthPassword")]
        public int MinLengthPassword
        {
            get
            {
                var str = (base["minLengthPassword"]).ToString();
                return int.Parse(str);
            }
        }

        [ConfigurationProperty("maxLengthPassword")]
        public int MaxLengthPassword
        {
            get
            {
                var str = (base["maxLengthPassword"]).ToString();
                return int.Parse(str);
            }
        }

        [ConfigurationProperty("maxLengthChatMessage")]
        public int MaxLengthChatMessage
        {
            get
            {
                var str = (base["maxLengthChatMessage"]).ToString();
                return int.Parse(str);
            }
        }
    }
}
