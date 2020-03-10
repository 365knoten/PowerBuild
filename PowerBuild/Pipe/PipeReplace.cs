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
    [Cmdlet("Pipe", "Replace")]
    public class PipeReplace : BaseStreamCmdlet
    {
        protected override void ProcessRecord()
        {
            foreach (PipeFile f in _files.Files)
            {
                Variables data = Context.Variables.join(f.properties);
                f.Contents = StringHelpers.replaceTokens(f.Contents, data);
            }
            WriteObject(_files);
        }
    }

}
