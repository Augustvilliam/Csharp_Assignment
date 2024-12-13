using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Helper;

public class IdGenerator
{
   public static string GenerateShortId (int length = 5)
    {
        if (length <= 0 || length > 32)
            throw new ArgumentException("Length must be between 1 and 32");
        return Guid.NewGuid ().ToString ("N").Substring(0, length);
    }
}
