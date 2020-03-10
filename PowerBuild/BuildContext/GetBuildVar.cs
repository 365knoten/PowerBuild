using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using CmdletHelpAttributes;
namespace PowerBuild
{
    [CmdletExampleAttribute(Code = "Get-PBBuildVar -key stage", Introduction = "Returns the value of the key stage")]
    [CmdletExampleAttribute(Code = "Get-PBBuildVar stage", Introduction = "Returns the value of the key stage")]
    [Cmdlet(VerbsCommon.Get, "BuildVar")]
    public class GetBuildVar : BuildCmdlet
    {
        #region Variables
        private string _key;

        [Parameter(
          Mandatory = true,
          ValueFromPipeline = true,
          Position = 0,
          ValueFromPipelineByPropertyName = true,
          HelpMessage = "The Key to get the Value for"
          )]
        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }
        #endregion
        protected override void ProcessRecord()
        {
            WriteObject(Context.Get(_key));
        }
    }
}
