using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XboxMusic1.Models
{
    [DataContract]
    public class AlbumQueryResult
    {
        [DataMember]
        public Albums Albums { get; set; }
    }
}
