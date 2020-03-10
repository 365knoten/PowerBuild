using Helpers;
using PowerBuild.UI;
using System;
using System.IO;
using System.Xml.Serialization;

namespace PowerBuild
{
    public class BuildContext
    {

        private Variables _variables = new Variables();

        public Variables Variables { get { return _variables; } }


        public bool ContainsKey(string key)
        {
            return _variables.ContainsKey(key);
        }

        public string Get(string key)
        {
            if (ContainsKey(key))
            {
                return _variables[key];
            }

            return "<undefined>";
        }

        public void Set(string key, string value)
        {
            if (ContainsKey(key))
            {
                _variables[key] = value;
            }
            else
            {
                _variables.Add(key, value);
            }
        }



        private Stage _stage;

        public Stage Stage
        {
            get { return _stage; }
        }

        public string StageName
        {
            get { return _stage.Name; }
        }



        private string askUserForStage(BuildCmdlet cmdlet)
        {
            ConsoleListBox box = new ConsoleListBox();
            var cC = Console.BackgroundColor;
            var fC = Console.ForegroundColor;
            string[] Choices = _app.StageNames();
            string result = "";
            
            try
            {
                int check = Console.CursorTop;
                Console.WriteLine("Select Stage");
                Console.ForegroundColor = ConsoleColor.Green;
                int before = Console.CursorTop;

                int choice = ConsoleListBox.ChooseListBoxItem(Choices, 10, before, ConsoleColor.Blue, ConsoleColor.White);
                result = Choices[choice - 1];
                Console.SetCursorPosition(0, before + Choices.Length + 2);
                Console.BackgroundColor = cC;
                Console.ForegroundColor = fC;
            }
            catch (IOException)
            {
                cmdlet.Host.UI.WriteLine("Select Stage");
                int i = 1;
                foreach (string choice in Choices)
                {
                    cmdlet.Host.UI.WriteLine(i + ") " + choice);
                    i++;
                }
                cmdlet.Host.UI.WriteLine();
                bool invalid = true;
                while (invalid)
                {
                    cmdlet.Host.UI.Write(">");
                    string data = cmdlet.Host.UI.ReadLine();

                    try
                    {
                        if (data==null || data.Length == 0)
                        {
                            invalid = false;
                            cmdlet.Host.UI.WriteErrorLine("User Aborting");
                        }
                        int d = Convert.ToInt32(data);
                        if (d > 0 && d <= i)
                        {
                            result = Choices[d - 1];
                            invalid = false;
                        }
                    }
                    catch (Exception ex) { cmdlet.Host.UI.WriteLine(ex.Message); }
                }

            }
            
            return result;
        }


        private string getStageName(BuildCmdlet cmdlet)
        {

            string envvalue = Environment.GetEnvironmentVariable(@"powerbuild_stage");
            if (envvalue != null && _app.getStage(envvalue) != null)
            {
                return envvalue;
            }
            return askUserForStage(cmdlet);
        }


        private string _configfilename = @"powerbuild.xml";

     
 

        private Application buildRecursive(string startdirectory, string filename=null)
        {
           
            String f = Path.Combine(startdirectory, filename!=null?filename:_configfilename);


            Application app = null;
            if (File.Exists(f))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Application));
                TextReader reader = new StreamReader(f);
                object obj = serializer.Deserialize(reader);
                app = (Application)obj;
                app.Variables["pb_configlocation"] = f;
                reader.Close();
            }

            DirectoryInfo parent = Directory.GetParent(startdirectory);
            if (parent != null)
            {
                Application other= buildRecursive(parent.FullName);

                if (app==null && other != null)
                {
                    app = other;
                } else if (app!=null && other !=null)
                {
                    app.Add(other);
                }

            }
            return app;
        }


        private Application _app;


        public BuildContext(BuildCmdlet cmdlet, Application application = null)
        {

            if (application == null)
            {
                _app = buildRecursive(cmdlet.CurrentPath);
                if (_app==null)
                {
                    _app = new Application();
                }

                String f = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), _configfilename);
                if (File.Exists(f))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Application));
                    TextReader reader = new StreamReader(f);
                    object obj = serializer.Deserialize(reader);
                    Application other = (Application)obj;
                    reader.Close();
                    _app.Add(other);
                }


            }
            else
            {
                _app = application;
            }


            this._stage = _app.getStage(getStageName(cmdlet));
            this._variables = new Variables(_app.Variables);

            foreach (string key in _stage.Variables.Keys)
            {
                this.Set(key, _stage.Variables[key]);
            }
            this.Set("pb_stage", this._stage.Name);
            this.Set("pb_osuser", Environment.UserName);
            /*
            XmlSerializer serializer = new XmlSerializer(typeof(Application));
            using (TextWriter writer = new StreamWriter(@"D:\Xml.xml"))
            {
                serializer.Serialize(writer, _app);
            }

    */

        }




        #region staticHandler
        private static BuildContext _current;

        public static void Reset()
        {
            _current = null;
        }

        public static BuildContext LoadFromFile(string filename, BuildCmdlet cmdlet)
        {

            XmlSerializer serializer = new XmlSerializer(typeof(Application));
            TextReader reader = new StreamReader(filename);
            object obj = serializer.Deserialize(reader);
            Application app = (Application)obj;
            reader.Close();
            _current = new BuildContext(cmdlet,app);
            return _current;
        }



        public static BuildContext CurrentOrCreate(BuildCmdlet cmdlet)
        {
                if (_current == null)
                {
                    _current = new BuildContext(cmdlet);
                }
                return _current;
        }


        /*
        public static BuildContext Current
        {
            get
            {

                if (_current == null)
                {
                    _current = new BuildContext();
                }
                return _current;
            }
        }
        */
        #endregion

    }
}
