using System;
using System.Collections.Specialized;

namespace WordChooser
{
    public interface IConfig
    {
        string SourceUrl();
        string XnonTextClass();
        string XtextPath();
        string XtitlePath();
        char[] WhiteSpace();
        string[] ExclusionList();
    }

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
