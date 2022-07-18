using System;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class PostReport
    {
        public PostReport()
        {
        }


        public int id { get; set; }
        public int Userid { get; set; }
        public int PostId { get; set; }


        [JsonIgnore]
        public virtual User User{ get; set; }

        [JsonIgnore]
        public virtual Post Post { get; set; }


    }
}

