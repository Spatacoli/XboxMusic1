// http://msdn.microsoft.com/en-us/library/dn546695.aspx


using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XboxMusic1.Models
{
    [DataContract]
    public class SearchQueryResult
    {
        [DataMember]
        public Artists Artists { get; set; }
        [DataMember]
        public Albums Albums { get; set; }
        [DataMember]
        public Tracks Tracks { get; set; }
    }
}
