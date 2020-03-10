using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;

namespace PowerBuild.Pipe
{
    public class BaseStreamCmdlet : BuildCmdlet
    {
        #region Variables
        protected PipeFileSystem _files;

        [Parameter(
          Mandatory = true,
          ValueFromPipeline = true,
          Position = 0,
          ValueFromPipelineByPropertyName = true,
          HelpMessage = "The FileSystem to use"
          )]
        public PipeFileSystem Files
        {
            get { return _files; }
            set { _files = value; }
        }
        #endregion
    }
}

