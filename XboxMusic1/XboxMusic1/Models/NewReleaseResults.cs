using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XboxMusic1.Models
{
    [DataContract]
    public class NewReleaseResultItem
    {
        [DataMember]
        public Album Album { get; set; }
        [DataMember]
        public string Type { get; set; }
    }

    [DataContract]
    public class NewReleaseResultItems
    { 
        [DataMember]
        public NewReleaseResultItem[] Items { get; set; }
    }

    [DataContract]
    public class NewReleaseResults
    {
        [DataMember]
        public NewReleaseResultItems Results { get; set; }
    }
}
