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
    [Cmdlet("Pipe", "Concat")]
    public class PipeConcat : BaseStreamCmdlet
    {
        #region Variables
        private string _newPath;
        [Parameter(
        Mandatory = true,
        ValueFromPipeline = true,
        Position = 1,
        ValueFromPipelineByPropertyName = true,
        HelpMessage = "The location of the new File in the Filesystem"
        )]
        public string NewPath
        {
            get { return _newPath; }
            set { _newPath = value; }
        }
        #endregion
        protected override void ProcessRecord()
        {
            string fname = StringHelpers.replaceTokens(_newPath, Context.Variables);
            PipeFile file = new PipeFile();
            file.properties = new Variables();
            file.properties.Add("filePath", fname);
            if (fname.IndexOf(@"\") > -1)
            {
                file.properties.Add("fileName", fname.Substring(fname.LastIndexOf(@"\"), fname.Length));
            }
            else {
                file.properties.Add("fileName", fname);
            }
            file.properties.Add("filelastwritetime", DateTime.Now.ToShortTimeString());
            StringBuilder sb = new StringBuilder();
            foreach (PipeFile f in _files.Files)
            {
                sb.Append(f.Contents);
            }
            file.Contents = sb.ToString();
            List<PipeFile> results = new List<PipeFile>();
            results.Add(file);
            _files.Files = results;
            WriteObject(_files);
        }
    }
}
