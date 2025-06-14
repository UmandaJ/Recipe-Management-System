using Receipe_Management_System;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Recipe_Management_System
{
    public class RecipeManager
    {
        private List<Recipe> recipes;

        public RecipeManager()
        {
            recipes = new List<Recipe>();
            LoadSampleData();
        }
        public void AddRecipe()
        {
            Console.WriteLine("\n=== Add a New Recipe ===");

            // Prompt for Recipe Name
            Console.Write("What is the name of the recipe? ");
            string name = Console.ReadLine();

            // Prompt for Recipe Category
            Console.Write("What category does this recipe belong to? (e.g., Dessert, Main Dish, Appetizer): ");
            string category = Console.ReadLine();

            // Prompt for Preparation Time
            int preparationTime;
            while (true)
            {
                Console.Write("How many minutes does it take to prepare this recipe? ");
                if (int.TryParse(Console.ReadLine(), out preparationTime) && preparationTime > 0)
                {
                    break;
                }
                Console.WriteLine("Invalid input! Please enter a positive number for preparation time.");
            }

            // Prompt for Ingredients
            Console.Write("List the ingredients (separate each ingredient with a comma): ");
            string ingredientsInput = Console.ReadLine();
            List<string> ingredients = ingredientsInput.Split(',').Select(i => i.Trim()).ToList();

            // Prompt for Instructions
            Console.Write("Provide the instructions (separate each step with a comma): ");
            string instructionsInput = Console.ReadLine();
            List<string> instructions = instructionsInput.Split(',').Select(i => i.Trim()).ToList();

            // Create and Add the Recipe
            Recipe newRecipe = new Recipe
            {
                Name = name,
                Category = category,
                PreparationTime = preparationTime,
                Ingredients = ingredients,
                Instructions = instructions
            };

            recipes.Add(newRecipe);
            Console.WriteLine("\nRecipe added successfully!");
            Console.WriteLine("Here's what you added:");
            newRecipe.DisplayRecipe();
        }

        public Recipe SearchRecipeByName(string name)
        {
            return recipes.Find(r => r.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
        public List<Recipe> SearchSimilarRecipes(string name)
        {
            return recipes
                .Where(r => r.Name.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();
        }

        public List<Recipe> GetRecipesSorted()
        {
            return SortingAlgorithms.SortRecipes(recipes, GetSortingChoice());
        }

        public void DeleteRecipe(string name)
        {
            Recipe recipeToDelete = SearchRecipeByName(name);
            if (recipeToDelete != null)
            {
                recipes.Remove(recipeToDelete);
                Console.WriteLine("Recipe deleted successfully!");
            }
            else
            {
                Console.WriteLine("Recipe not found!");
            }
        }
        public void FilterByTime(int maxTime)
        {
            List<Recipe> filteredRecipes = recipes.Where(r => r.PreparationTime <= maxTime).ToList();

            if (filteredRecipes.Count == 0)
            {
                Console.WriteLine("\nNo recipes found with the given preparation time.");
                return;
            }

            Console.WriteLine($"\n=== Recipes That Take {maxTime} Minutes or Less ===");
            filteredRecipes = SortingAlgorithms.SortRecipes(filteredRecipes, GetSortingChoice());

            foreach (var recipe in filteredRecipes)
            {
                recipe.DisplayRecipe();
            }
        }

        public void FilterByCategory(string category)
        {
            List<Recipe> filteredRecipes = recipes.Where(r => r.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();

            if (filteredRecipes.Count == 0)
            {
                Console.WriteLine($"\nNo recipes found in the '{category}' category.");
                return;
            }

            Console.WriteLine($"\n=== {category.ToUpper()} RECIPES ===");
            filteredRecipes = SortingAlgorithms.SortRecipes(filteredRecipes, GetSortingChoice());

            foreach (var recipe in filteredRecipes)
            {
                recipe.DisplayRecipe();
            }
        }
        public void FilterAndSortByIngredient()
        {
            Console.Write("Enter ingredient to filter by: ");
            string ingredient = Console.ReadLine();

            List<Recipe> filteredRecipes = recipes.FindAll(r => r.Ingredients.Exists(i => i.Equals(ingredient, StringComparison.OrdinalIgnoreCase)));

            if (filteredRecipes.Count == 0)
            {
                Console.WriteLine("No recipes found with that ingredient.");
                return;
            }

            foreach (var recipe in SortingAlgorithms.SortRecipes(filteredRecipes, GetSortingChoice()))
            {
                recipe.DisplayRecipe();
            }
        }
        private int GetSortingChoice()
        {
            Console.WriteLine("\nChoose Sorting Algorithm:");
            Console.WriteLine("1. Merge Sort");
            Console.WriteLine("2. Bubble Sort");
            Console.WriteLine("3. Insertion Sort");
            Console.Write("\nEnter your choice: ");

            int choice;
            return int.TryParse(Console.ReadLine(), out choice) ? choice : 1; // Default to Merge Sort
        }

        private void LoadSampleData()
        {
            recipes.Add(new Recipe { Name = "Tiramisu", Category = "Dessert", PreparationTime = 40, Ingredients = new List<string> { "Ladyfingers", "Mascarpone Cheese", "Coffee", "Cocoa Powder" }, Instructions = new List<string> { "Layer coffee-soaked ladyfingers with mascarpone cream.", "Dust with cocoa powder." } });
            recipes.Add(new Recipe { Name = "Apple Pie", Category = "Dessert", PreparationTime = 60, Ingredients = new List<string> { "Apples", "Flour", "Butter", "Sugar" }, Instructions = new List<string> { "Prepare the crust.", "Fill with apples and bake." } });
            recipes.Add(new Recipe { Name = "Fried Rice", Category = "Main Dish", PreparationTime = 30, Ingredients = new List<string> { "Rice", "Eggs", "Carrots", "Peas", "Soy Sauce" }, Instructions = new List<string> { "Cook rice and fry with vegetables and eggs.", "Add soy sauce to taste." } });
            recipes.Add(new Recipe { Name = "Spaghetti Bolognese", Category = "Main Dish", PreparationTime = 45, Ingredients = new List<string> { "Spaghetti", "Ground Beef", "Tomato Sauce", "Garlic", "Onion" }, Instructions = new List<string> { "Cook spaghetti.", "Prepare sauce with ground beef, tomato, and garlic.", "Mix together and serve." } });
            recipes.Add(new Recipe { Name = "Caesar Salad", Category = "Appetizer", PreparationTime = 15, Ingredients = new List<string> { "Romaine Lettuce", "Croutons", "Parmesan Cheese", "Caesar Dressing" }, Instructions = new List<string> { "Chop lettuce.", "Mix with croutons, cheese, and dressing.", "Serve chilled." } });
            recipes.Add(new Recipe { Name = "Chocolate Chip Cookies", Category = "Dessert", PreparationTime = 25, Ingredients = new List<string> { "Flour", "Sugar", "Butter", "Chocolate Chips", "Eggs" }, Instructions = new List<string> { "Mix ingredients into a dough.", "Bake at 180°C for 15 minutes.", "Let cool before serving." } });
            recipes.Add(new Recipe { Name = "Chicken Curry", Category = "Main Dish", PreparationTime = 60, Ingredients = new List<string> { "Chicken", "Coconut Milk", "Curry Powder", "Onion", "Garlic" }, Instructions = new List<string> { "Sauté onion and garlic.", "Add chicken and curry powder.", "Pour coconut milk and simmer." } });
            recipes.Add(new Recipe { Name = "Greek Salad", Category = "Appetizer", PreparationTime = 10, Ingredients = new List<string> { "Cucumber", "Tomato", "Feta Cheese", "Olives", "Olive Oil" }, Instructions = new List<string> { "Chop vegetables.", "Mix with feta and olives.", "Drizzle with olive oil and serve." } });
            recipes.Add(new Recipe { Name = "Pancakes", Category = "Breakfast", PreparationTime = 20, Ingredients = new List<string> { "Flour", "Milk", "Eggs", "Sugar", "Baking Powder" }, Instructions = new List<string> { "Mix batter ingredients.", "Cook on a hot pan until golden brown.", "Serve with syrup or fruit." } });
            recipes.Add(new Recipe { Name = "Garlic Butter Shrimp", Category = "Main Dish", PreparationTime = 25, Ingredients = new List<string> { "Shrimp", "Garlic", "Butter", "Parsley", "Lemon" }, Instructions = new List<string> { "Sauté garlic in butter.", "Add shrimp and cook until pink.", "Garnish with parsley and lemon juice." } });
            recipes.Add(new Recipe { Name = "Mushroom Risotto", Category = "Main Dish", PreparationTime = 45, Ingredients = new List<string> { "Arborio Rice", "Mushrooms", "Chicken Broth", "Onion", "Parmesan Cheese" }, Instructions = new List<string> { "Sauté onions and mushrooms.", "Add rice and slowly mix in broth.", "Stir until creamy, then add cheese." } });
            recipes.Add(new Recipe { Name = "French Toast", Category = "Breakfast", PreparationTime = 15, Ingredients = new List<string> { "Bread", "Eggs", "Milk", "Sugar", "Cinnamon" }, Instructions = new List<string> { "Mix eggs, milk, sugar, and cinnamon.", "Dip bread and fry until golden.", "Serve with syrup or fruit." } });
            recipes.Add(new Recipe { Name = "Lasagna", Category = "Main Dish", PreparationTime = 90, Ingredients = new List<string> { "Lasagna Noodles", "Ground Beef", "Tomato Sauce", "Ricotta Cheese", "Mozzarella" }, Instructions = new List<string> { "Layer noodles with beef sauce and cheeses.", "Bake for 45 minutes.", "Let cool before serving." } });
            recipes.Add(new Recipe { Name = "Guacamole", Category = "Appetizer", PreparationTime = 10, Ingredients = new List<string> { "Avocado", "Onion", "Tomato", "Lime", "Salt" }, Instructions = new List<string> { "Mash avocado.", "Mix in onion, tomato, lime juice, and salt.", "Serve fresh with chips." } });
            recipes.Add(new Recipe { Name = "Omelette", Category = "Breakfast", PreparationTime = 10, Ingredients = new List<string> { "Eggs", "Cheese", "Mushrooms", "Bell Peppers", "Salt" }, Instructions = new List<string> { "Whisk eggs with salt.", "Cook in a pan with fillings.", "Fold and serve." } });
            recipes.Add(new Recipe { Name = "Beef Stew", Category = "Main Dish", PreparationTime = 120, Ingredients = new List<string> { "Beef", "Potatoes", "Carrots", "Onions", "Beef Broth" }, Instructions = new List<string> { "Brown beef, then add vegetables and broth.", "Simmer for 2 hours.", "Serve warm." } });
            recipes.Add(new Recipe { Name = "Tomato Soup", Category = "Appetizer", PreparationTime = 30, Ingredients = new List<string> { "Tomatoes", "Garlic", "Onion", "Basil", "Cream" }, Instructions = new List<string> { "Sauté onion and garlic.", "Add tomatoes and cook.", "Blend with basil and cream." } });
            recipes.Add(new Recipe { Name = "Vanilla Ice Cream", Category = "Dessert", PreparationTime = 60, Ingredients = new List<string> { "Milk", "Sugar", "Vanilla Extract", "Heavy Cream" }, Instructions = new List<string> { "Mix ingredients.", "Churn in an ice cream maker.", "Freeze until firm." } });
            recipes.Add(new Recipe { Name = "Mac and Cheese", Category = "Main Dish", PreparationTime = 30, Ingredients = new List<string> { "Macaroni", "Cheddar Cheese", "Milk", "Butter", "Flour" }, Instructions = new List<string> { "Boil macaroni.", "Make cheese sauce with butter, flour, milk, and cheese.", "Mix with pasta and serve." } });
            recipes.Add(new Recipe { Name = "Lemonade", Category = "Drink", PreparationTime = 5, Ingredients = new List<string> { "Lemon", "Sugar", "Water", "Ice" }, Instructions = new List<string> { "Squeeze lemons.", "Mix with sugar and water.", "Serve with ice." } });
            recipes.Add(new Recipe { Name = "Cheesecake", Category = "Dessert", PreparationTime = 90, Ingredients = new List<string> { "Cream Cheese", "Sugar", "Eggs", "Graham Crackers", "Butter" }, Instructions = new List<string> { "Make crust with graham crackers and butter.", "Mix filling and pour over crust.", "Bake for 60 minutes." } });

        }
    }

    class Program
    {

        static void Main()
        {
            RecipeManager manager = new RecipeManager();

            while (true)
            {
                Console.Clear();
                ConsoleHelper.PrintCentered("=========================================", ConsoleColor.Blue);
                ConsoleHelper.PrintCentered("        RECIPE MANAGEMENT SYSTEM        ", ConsoleColor.Yellow, true);
                ConsoleHelper.PrintCentered("=========================================", ConsoleColor.Blue);
                ConsoleHelper.PrintCentered("|  1. Add Recipe                        |");
                ConsoleHelper.PrintCentered("|  2. Search Recipe                     |");
                ConsoleHelper.PrintCentered("|  3. View Recipes (Ascending order)    |");
                ConsoleHelper.PrintCentered("|  4. Delete Recipe                     |");
                ConsoleHelper.PrintCentered("|  5. Filter Recipes                    |");
                ConsoleHelper.PrintCentered("|  6. Exit                              |");
                ConsoleHelper.PrintCentered("=========================================", ConsoleColor.Blue);

                Console.Write("\nEnter your choice (1-6): ");


                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("\nInvalid input! Please enter a number.");
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                    continue;
                }

                Console.Clear(); // Clears the console for a fresh display

                switch (choice)
                {
                    case 1:
                        manager.AddRecipe();
                        break;
                    case 2:
                        Console.Write("Enter the name of the recipe to search: ");
                        string searchTerm = Console.ReadLine();
                        List<Recipe> results = manager.SearchSimilarRecipes(searchTerm);

                        if (results.Count > 0)
                        {
                            Console.WriteLine("\nRecipes found:");
                            results.ForEach(r => r.DisplayRecipe());
                        }
                        else
                        {
                            Console.WriteLine("\nNo matching recipes found!");
                        }
                        break;

                    case 3:
                        manager.GetRecipesSorted().ForEach(r => r.DisplayRecipe());
                        break;
                    case 4:
                        Console.Write("Enter the name of the recipe to delete: ");
                        manager.DeleteRecipe(Console.ReadLine());
                        break;
                    case 5: // NEW CASE - Opens a Submenu for Filtering
                        Console.Clear();
                        Console.WriteLine("=========================================");
                        Console.WriteLine("           FILTER RECIPES               ");
                        Console.WriteLine("=========================================");
                        Console.WriteLine("|  1. Filter by Ingredient              |");
                        Console.WriteLine("|  2. Filter by Time                    |");
                        Console.WriteLine("|  3. Filter by Category                |");
                        Console.WriteLine("|  4. Go Back                           |");
                        Console.WriteLine("=========================================");
                        Console.Write("\nEnter your choice: ");

                        int filterChoice;
                        if (int.TryParse(Console.ReadLine(), out filterChoice))
                        {
                            Console.Clear();
                            switch (filterChoice)
                            {
                                case 1:
                                    manager.FilterAndSortByIngredient();
                                    break;
                                case 2:
                                    Console.Write("Enter maximum preparation time (in minutes): ");
                                    if (int.TryParse(Console.ReadLine(), out int maxTime))
                                    {
                                        manager.FilterByTime(maxTime);
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nInvalid input! Please enter a valid number.");
                                    }
                                    break;
                                case 3:
                                    Console.Write("Enter category (e.g., Dessert, Main Dish, Appetizer): ");
                                    string category = Console.ReadLine();
                                    manager.FilterByCategory(category);
                                    break;
                                case 4:
                                    break; // Go back to main menu
                                default:
                                    Console.WriteLine("\nInvalid choice! Returning to the main menu.");
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nInvalid input! Returning to the main menu.");
                        }
                        break;
                 
                    case 6:
                        Console.WriteLine("\nThank you for using the Recipe Management System! Goodbye!");
                        return;
                    default:
                        Console.WriteLine("\nInvalid choice! Please try again.");
                        break;
                }

                Console.WriteLine("\nPress Enter to return to the main menu...");
                Console.ReadLine();
            }
        }
    }

}