using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Receipe_Management_System
{
    public static class ConsoleHelper
    {
        public static void PrintCentered(string text, ConsoleColor color = ConsoleColor.White, bool isLarge = false)
        {
            int screenWidth = Console.WindowWidth;
            int textWidth = text.Length;
            int leftPadding = (screenWidth - textWidth) / 2;

            Console.ForegroundColor = color;  // Set text color
            if (isLarge)
            {
                Console.WriteLine(new string(' ', Math.Max(leftPadding, 0)) + ">> " + text.ToUpper() + " <<");
            }
            else
            {
                Console.WriteLine(new string(' ', Math.Max(leftPadding, 0)) + text);
            }
            Console.ResetColor();  // Reset color back to default
        }
    }
}
