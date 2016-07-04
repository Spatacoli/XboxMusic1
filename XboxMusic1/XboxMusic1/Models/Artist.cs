using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XboxMusic1.Models
{
    [DataContract]
    public class Artist
    {
        [DataMember]
        public string[] Genres { get; set; }
        [DataMember]
        public string[] Subgenres { get; set; }
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string ImageUrl { get; set; }
        [DataMember]
        public string Link { get; set; }
        [DataMember]
        public string Source { get; set; }
    }
}
