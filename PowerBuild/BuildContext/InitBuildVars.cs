using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using System.IO;

namespace PowerBuild
{
    [Cmdlet("Init", "BuildVars")]
    public class InitBuildVars : BuildCmdlet
    {
        #region Variables
        private string _filename = null;

        [Parameter(
          Mandatory = false,
          HelpMessage = "the filename to load the configuration file from"
          )]
        public string Filename
        {
            get { return _filename; }
            set { _filename = value; }
        }
        #endregion
        protected override void BeginProcessing()
        {
            BuildContext.Reset();
            if (_filename != null)
            {
                WriteObject(BuildContext.LoadFromFile(_filename,this).Stage.Name);
            }
            else
            {
                WriteObject(Context.Stage.Name);
            }
        }
    }

}
