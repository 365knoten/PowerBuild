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
    [Cmdlet("Pipe", "Write")]
    public class PipeWrite : BaseStreamCmdlet
    {
        #region Variables
        private string _base=null;

        [Parameter(
          Mandatory = false,
          ValueFromPipeline = false,
          HelpMessage = "The location to store the files"
          )]
        public string Base
        {
            get { return _base; }
            set { _base = value; }
        }
        #endregion

        protected override void ProcessRecord()
        {
            string b = _base;
            if (b == null)
            {
                b = this.SessionState.Path.CurrentFileSystemLocation.Path;
            }
            foreach (PipeFile f in _files.Files)
            {


                string completePath = StringHelpers.replaceTokens(Path.Combine(b, f.properties["filePath"]), Context.Variables); ;

                WriteWarning(completePath);
                WriteDebug("Saving File " + completePath);
                string directoryPath = Path.GetDirectoryName(completePath);
                if (!Directory.Exists(directoryPath))
                {
                    DirectoryInfo di = Directory.CreateDirectory(directoryPath);
                }
                File.WriteAllText(completePath, f.Contents);
            }
            WriteObject(_files);
        }


    }

}
