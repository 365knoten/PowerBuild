using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using CmdletHelpAttributes;


namespace PowerBuild
{
    [CmdletExampleAttribute(Code ="Equals-PBBuildVar -key stage -value- kon", Introduction ="Returns true if the Variable stage is set to kon")]
    [Cmdlet("Equals", "BuildVar")]
    public class EqualsBuildVar : BuildCmdlet
    {
        #region Variables
        private string _key;
        [Parameter(
          Mandatory = true,
          Position = 0,
          HelpMessage = "The Key to set the Value for"
          )]
        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }


        private string _value;
        [Parameter(
          Mandatory = true,
          Position = 0,
          HelpMessage = "The Value to set"
          )]
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }
        #endregion

        protected override void ProcessRecord()
        {
            string key = this.Context.Get(_key);
            if (key == null)
            {
                WriteObject(false);
            }
            WriteObject(key.Equals(_value));
        }



    }

}
