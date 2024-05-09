using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitnessstudio
{
    public class Util
    {
        public static Rolle ConvertToRolle(string roleString)
        {
            switch (roleString.ToLower())
            {
                case "administrator":
                    return Rolle.ADMINISTRATOR;
                case "personal":
                    return Rolle.PERSONAL;
                case "kunde":
                    return Rolle.KUNDE;
                default:
                    throw new ArgumentException("Invalid role string: " + roleString);
            }
        }
    }
}
