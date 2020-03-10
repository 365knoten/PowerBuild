using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PowerBuild
{
    [XmlRoot(ElementName ="app")]
    public class Application
    {
        [XmlElement("global")]
        public Variables Variables = new Variables();
        [XmlElement("stage")]
        public List<Stage> Stages = new List<Stage>();



        public void Add(Application other)
        {
            foreach (Stage otherStage in other.Stages)
            {
                Stage myStage = getStage(otherStage.Name);
                if (myStage == null)
                {
                    this.Stages.Add(otherStage);
                }
                else
                {
                    myStage.Variables = myStage.Variables.Add(otherStage.Variables);
                }
            }
            this.Variables = this.Variables.Add(other.Variables);


        }



        public string[] StageNames()
        {
            string[] result = new string[Stages.Count];

            for (int i = 0; i < Stages.Count; i++)
            {
                result[i] = Stages[i].Name;
            }

            return result;
        }

        public Stage getStage(string stagename)
        {
            if (Stages.Count == 0)
            {
                return null;
            }

            foreach (Stage stage in Stages)
            {
                if (stage.Name.ToLower().Equals(stagename.ToLower()))
                {                   
                    return stage;
                }

            }
            return null;
        }

    }
}
