using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PowerBuild
{

    [XmlRoot("stage")]
    public class Stage
    {
        [XmlElement("variables")]
        public Variables Variables = new Variables();
        [XmlAttribute("name")]
        public string Name = "";

    }
}
