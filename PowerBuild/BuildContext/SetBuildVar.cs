using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;

namespace PowerBuild
{
    [Cmdlet(VerbsCommon.Set, "BuildVar")]
    public class SetBuildVar : BuildCmdlet
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
            Context.Set(_key,_value);
        }
    }
}
