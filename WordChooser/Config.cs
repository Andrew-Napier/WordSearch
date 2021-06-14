using System.Collections.Specialized;
#nullable enable

namespace WordChooser
{

    public class Config : NameValueCollection, IConfig
    {
        public Config(NameValueCollection config)
        {
            foreach(var key in config.AllKeys)
            {
                this[key] = config[key];
            }
        }

        public string SourceUrl() => this["sourceUrl"];
        public string XnonTextClass() => this["xNonTextClass"];
        public string XtextPath() => this["xPath"];
        public string XtitlePath() => this["xTitlePath"];
        public char[] WhiteSpace() => this["whiteSpace"].ToCharArray();
        public string[] ExclusionList() => this["exclusionList"].Split(",");
    }
}
