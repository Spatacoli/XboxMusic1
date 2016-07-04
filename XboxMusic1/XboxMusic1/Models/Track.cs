using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XboxMusic1.Models
{
    [DataContract]
    public class Track
    {
        [DataMember]
        public string ReleaseDate { get; set; }
        [DataMember]
        public string Duration { get; set; }
        [DataMember]
        public int TrackNumber { get; set; }
        [DataMember]
        public bool IsExplicit { get; set; }
        [DataMember]
        public string[] Genres { get; set; }
        [DataMember]
        public string[] Subgenres { get; set; }
        [DataMember]
        public string[] Rights { get; set; }
        [DataMember]
        public string Subtitle { get; set; }
        [DataMember]
        public Album Album { get; set; }
        [DataMember]
        public AlbumArtist[] Artists { get; set; }
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
