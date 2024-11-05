using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduConnect.Services.ValidationHelpers
{
    public  class ValidCharacters
    {
        public static bool BeValidCharactersName(string name)
        {
            if (name == null)
            {
                return false;
            }
            return name.All(char.IsLetter);
        }
        public static bool BeValidCharactersSurname(string surname)
        {
            if (surname == null)
            {
                return false;
            }
            return surname.All(char.IsLetter);
        }
    }
}
