using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;

namespace PowerBuild
{

    public class BuildCmdlet : PSCmdlet
    {
        public BuildContext Context {
            get { return BuildContext.CurrentOrCreate(this); }
        }


        public string CurrentPath
        {
            get { return this.SessionState.Path.CurrentFileSystemLocation.Path; }
        }


        protected override void ProcessRecord()
        {
            base.ProcessRecord();
        }

        protected override void BeginProcessing()
        {

            base.BeginProcessing();
        }

        protected override void EndProcessing()
        {
            base.EndProcessing();
        }



    }

}
