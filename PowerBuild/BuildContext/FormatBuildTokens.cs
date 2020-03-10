using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using Helpers;

namespace PowerBuild
{
    [Cmdlet(VerbsCommon.Format, "BuildTokens")]
    public class FormatBuildTokens : BuildCmdlet
    {
        #region Variables
        private string _content;
        [Parameter(
          Mandatory = true,
          Position = 0,
          HelpMessage = "The string with placeholders to replace"
          )]
        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }
        #endregion


        protected override void ProcessRecord()
        {
            WriteObject(StringHelpers.replaceTokens(_content, this.Context.Variables));
        }



    }



}
