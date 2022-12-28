using System;
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

        public string SourceUrl() => this["sourceUrl"] ?? throw new ArgumentNullException();
        public string XnonTextClass() => this["xNonTextClass"] ?? throw new ArgumentNullException();
        public string XtextPath() => this["xPath"] ?? throw new ArgumentNullException();
        public string XtitlePath() => this["xTitlePath"] ?? throw new ArgumentNullException();
        public char[] WhiteSpace() => this["whiteSpace"]?.ToCharArray() ?? throw new ArgumentNullException();
        public string[] ExclusionList() => this["exclusionList"]?.Split(",") ?? throw new ArgumentNullException();
    }
}
