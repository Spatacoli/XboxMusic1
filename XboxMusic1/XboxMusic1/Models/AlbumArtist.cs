using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XboxMusic1.Models
{
    [DataContract]
    public class AlbumArtist
    {
        [DataMember]
        public string Role { get; set; }
        [DataMember]
        public Artist Artist { get; set; }
    }
}
