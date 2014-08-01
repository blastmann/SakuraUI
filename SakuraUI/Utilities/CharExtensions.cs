using System;

namespace Yangwenyi.WindowsPhone.Listen.Frameworks
{
    public static class CharExtensions
    {
        public static bool IsAlphabet(this Char c1)
        {
            return (c1 >= 'A' && c1 <= 'Z') || (c1 >= 'a' && c1 <= 'z');
        }
    }
}
