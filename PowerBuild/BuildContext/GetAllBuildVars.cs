using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;

namespace PowerBuild
{
    [Cmdlet(VerbsCommon.Get, "AllBuildVars")]
    public class GetAllBuildVars : BuildCmdlet
    {    
        protected override void ProcessRecord()
        {
            foreach (KeyValuePair<string, string> kvp in this.Context.Variables)
            {
                WriteObject(kvp);
            }            
        }
    }

}
