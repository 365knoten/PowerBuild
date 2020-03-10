using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerBuild.Pipe
{
    public class PipeFile
    {
        public string Contents { get; set; }
        /*
        public string Path
        {
            get
            {
                return properties["filePath"];
            }
            set
            {
                // properties.saveSet("filePath", value);
            }
        }
        */
        public Variables properties { get; set; }
    }
}
