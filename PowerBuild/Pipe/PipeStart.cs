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
    [Cmdlet("Pipe", "Start")]
    public class PipeStart : BuildCmdlet
    {
        #region Variables
        private string _pattern;

        [Parameter(
          Mandatory = true,
          ValueFromPipeline = true,
          Position = 0,
          ValueFromPipelineByPropertyName = true,
          HelpMessage = "File Pattern to search"
          )]
        public string Pattern
        {
            get { return _pattern; }
            set { _pattern = value; }
        }

        private string _base = null;

        [Parameter(
          Mandatory = false,
          ValueFromPipeline = true,
          Position = 1,
          ValueFromPipelineByPropertyName = true,
          HelpMessage = "File Pattern to search"
          )]
        public string Base
        {
            get { return _base; }
            set { _base = value; }
        }
        #endregion
        protected override void ProcessRecord()
        {
            PipeFileSystem files = new PipeFileSystem();
            string b = _base;
            if (b == null)
            {
                b = this.SessionState.Path.CurrentFileSystemLocation.Path;
            }
            if (!b.EndsWith(@"\"))
            {
                b = b + @"\";
            }
            foreach (FileSystemInfo info in new Glob(StringHelpers.replaceTokens(Path.Combine(b, _pattern), Context.Variables)).Expand())
            {
                PipeFile file = new PipeFile();
                file.properties = new Variables();
                file.properties.Add("fileFullname", info.Name);
                file.properties.Add("fileName", info.Name.Substring(0, info.Name.IndexOf(".")));
                string path = info.FullName.Replace(b, "");
                file.properties.Add("filePath", path);
              file.properties.Add("fileExtension", info.Extension);
                file.properties.Add("fileLastWriteTime", info.LastWriteTime.ToShortTimeString());
                WriteDebug("Reading file " + path);
                try
                {   // Open the text file using a stream reader.
                    using (StreamReader sr = new StreamReader(info.FullName))
                    {
                        file.Contents = sr.ReadToEnd();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(e.Message);
                }
                files.Files.Add(file);

            }
            WriteObject(files);
        }
    }
}
