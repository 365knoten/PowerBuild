using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using Helpers;
using System.IO;

namespace PowerBuild.Pipe
{
    [Cmdlet("Pipe", "Minimize")]
    public class PipeMinimize : BaseStreamCmdlet
    {
        protected override void ProcessRecord()
        {
            foreach (PipeFile f in _files.Files)
            {
                f.Contents = StringHelpers.RemoveWhiteSpaceFromStylesheets(f.Contents);
            }
            WriteObject(_files);
        }
    }

}
