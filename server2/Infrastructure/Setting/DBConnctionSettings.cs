using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Infrastructure.Setting
{
    public class DBConnctionSettings
    {
       
        public static string ConnectionString { get; set; }

        public static void Set(DBConnctionSettingsDTO dBConnctionSettingsDTO)
        {
            ConnectionString = dBConnctionSettingsDTO.ConnectionString;
        }
    }
}
