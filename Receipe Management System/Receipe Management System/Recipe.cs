using Receipe_Management_System;
using System;
using System.Collections.Generic;

namespace Recipe_Management_System
{
    public class Recipe
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public int PreparationTime { get; set; }
        public List<string> Ingredients { get; set; }
        public List<string> Instructions { get; set; }

        public Recipe()
        {
            Ingredients = new List<string>();
            Instructions = new List<string>();
        }

        public void DisplayRecipe()
        {
            ConsoleHelper.PrintCentered(new string('=', 40), ConsoleColor.Yellow);
            ConsoleHelper.PrintCentered($"{Name.ToUpper()}", ConsoleColor.Cyan, true);
            ConsoleHelper.PrintCentered(new string('=', 40), ConsoleColor.Yellow);

            ConsoleHelper.PrintCentered($"Category        : {Category}");
            ConsoleHelper.PrintCentered($"Preparation Time: {PreparationTime} minutes");
            ConsoleHelper.PrintCentered(new string('-', 40));

            ConsoleHelper.PrintCentered("INGREDIENTS:", ConsoleColor.Green, true);
            foreach (var ingredient in Ingredients)
            {
                ConsoleHelper.PrintCentered($"  - {ingredient}");
            }
            ConsoleHelper.PrintCentered(new string('-', 40));

            ConsoleHelper.PrintCentered("INSTRUCTIONS:", ConsoleColor.Green, true);
            for (int i = 0; i < Instructions.Count; i++)
            {
                ConsoleHelper.PrintCentered($"  {i + 1}. {Instructions[i]}");
            }

            ConsoleHelper.PrintCentered(new string('=', 40), ConsoleColor.Yellow);
            ConsoleHelper.PrintCentered("\n\n");
        }

    }
}