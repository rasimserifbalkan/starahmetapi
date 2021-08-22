using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace StarAhmet.DbModels
{
    public class Users
    {
        [Key]
        [JsonProperty("id")]
        public virtual string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime RegisterDate { get; set; }
        public string RegisterIpAddress { get; set; }
    }
}
