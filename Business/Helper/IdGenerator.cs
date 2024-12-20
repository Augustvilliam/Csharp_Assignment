using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Helper;

public class IdGenerator
{
   public static string GenerateShortId (int length = 5) //trimma till 5 täcker så det inte blir ett ända stort projekt att att skriva in ID när man ska ta bort eller Redigera användare.
    {
        if (length <= 0 || length > 32)
            throw new ArgumentException("Length must be between 1 and 32");
        return Guid.NewGuid ().ToString ("N").Substring(0, length);
    }
}
