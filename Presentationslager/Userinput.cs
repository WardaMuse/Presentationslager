using System;
using System.Collections.Generic;
using System.Text;

namespace Presentationslager
{
    public static class Userinput
    {
        public static int UserInt(string text)
        {
            Console.WriteLine(text);
            int.TryParse(Console.ReadLine(), out int input);
            return input;

        }

        public static string UserString(string text)
        {
            Console.WriteLine(text);
            return Console.ReadLine();
        }
    }
}
