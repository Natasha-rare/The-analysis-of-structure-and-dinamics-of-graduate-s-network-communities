using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject
{
    public class Person
    {
        [JsonProperty("Clan")]
        public string Clan { get; set; }
        [JsonProperty("Graduation")]
        public string Graduation { get; set; }
        [JsonProperty("Project")]
        public string Project { get; set; }
        [JsonProperty("Fb_name")]
        public string Fb_name { get; set; }
        [JsonProperty("Fb_id")]
        public string Fb_id { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Group")]
        public string Group { get; set; }
        [JsonProperty("Occupation")]
        public List<string> Occupation { get; set; }
        [JsonProperty("Education")]
        public List<string> Education { get; set; }
        [JsonProperty("FieldOfEducation")]
        public List<string> FieldOfEducation { get; set; }
        [JsonProperty("Position")]
        public List<string> Position { get; set; }
        [JsonProperty("patronym")]
        public String patronym { get; set; }
        [JsonProperty("First_name")]
        public string First_name { get; set; }
        [JsonProperty("Lyceum_surname")]
        public string Lyceum_surname { get; set; }
        [JsonProperty("Current_surname")]
        public string Current_surname { get; set; }
        [JsonProperty("Vk_name")]
        public string Vk_name { get; set; }
        [JsonProperty("Vk_id")]
        public string Vk_id { get; set; }
        [JsonProperty("Inst_name")]
        public string Inst_name { get; set; }
        [JsonProperty("Inst_id")]
        public string Inst_id { get; set; }
    }
}