using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Interfaces;
using PlayerRoles;

namespace Scp073
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        
        public bool Debug { get; set; } = false;
        public string Hint1 { get; set; } = "SCP-073 -каин. Научный сотрудник только сильнее...";
        
        public int spawnChance { get; set; } = 5;
        
    }
}