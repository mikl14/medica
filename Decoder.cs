using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    internal class Decoder
    {
        public static Dictionary<String, String> decoder = new Dictionary<string, string>
        {
            { "Кот", "AorticValveMaxVelocity" },
            { "Собака", "tbAorticValveMaxGradient" },
            { "Сова", "tbAorticValveAvarageVelocity" }
        };
    }
}
