using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Infrastructure.Setting
{
    public class DBConnctionSettingsDTO
    {
        [JsonPropertyName("CONNECTIONSTRINGS_SMARTERASPNET")]
        public string ConnectionString { get; set; }
    }
}
