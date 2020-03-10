using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;

namespace PowerBuild.Pipe
{
    [Cmdlet(VerbsCommon.Get, "PipeFiles")]
    public class GetPipeFiles : BaseStreamCmdlet
    {
        protected override void ProcessRecord()
        {
            foreach(PipeFile file in this._files.Files)
            {
                WriteObject(file);
            }            
        }
    }
}

