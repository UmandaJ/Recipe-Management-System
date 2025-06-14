using Receipe_Management_System;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Recipe_Management_System
{
    public class SortingAlgorithms
    {
        public static List<Recipe> SortRecipes(List<Recipe> recipes, int choice)
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            switch (choice)
            {
                case 1:
                    recipes = MergeSort.SortByName(recipes);
                    break;
                case 2:
                    recipes = BubbleSort.SortByName(recipes);
                    break;
                case 3:
                    recipes = InsertionSort.SortByName(recipes);
                    break;
                default:
                    Console.WriteLine("Invalid choice! Using Merge Sort by default.");
                    recipes = MergeSort.SortByName(recipes);
                    break;
            }
            stopwatch.Stop();

            long elapsedNanoseconds = stopwatch.ElapsedTicks * 100; // Convert ticks to nanoseconds

            ConsoleHelper.PrintCentered($"\n==> SORT TIME: {stopwatch.ElapsedTicks * 100} nanoseconds", ConsoleColor.Cyan, true);


            return recipes;
        }


    }

    public class MergeSort
    {
        public static List<Recipe> SortByName(List<Recipe> recipes)
        {
            if (recipes.Count <= 1) return recipes; // if the count < 1 there is only 1 recipe

            int mid = recipes.Count / 2;
            List<Recipe> left = recipes.GetRange(0, mid);
            List<Recipe> right = recipes.GetRange(mid, recipes.Count - mid);

            left = SortByName(left);
            right = SortByName(right);

            return Merge(left, right);
        }

        private static List<Recipe> Merge(List<Recipe> left, List<Recipe> right)
        {
            List<Recipe> result = new List<Recipe>();
            int i = 0, j = 0;

            while (i < left.Count && j < right.Count)
            {
                if (string.Compare(left[i].Name, right[j].Name, StringComparison.OrdinalIgnoreCase) < 0) // ordinalignore case is used to to make apple and Apple same
                {
                    result.Add(left[i]);
                    i++;
                }
                else
                {
                    result.Add(right[j]);
                    j++;
                }
            }

            while (i < left.Count)
            {
                result.Add(left[i]);
                i++;
            }

            while (j < right.Count)
            {
                result.Add(right[j]);
                j++;
            }

            return result;
        }
    }

    public class BubbleSort
    {
        public static List<Recipe> SortByName(List<Recipe> recipes)
        {
            int n = recipes.Count;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (string.Compare(recipes[j].Name, recipes[j + 1].Name, StringComparison.OrdinalIgnoreCase) > 0)
                    {
                        (recipes[j], recipes[j + 1]) = (recipes[j + 1], recipes[j]);
                    }
                }
            }
            return recipes;
        }
    }

    public class InsertionSort
    {
        public static List<Recipe> SortByName(List<Recipe> recipes)
        {
            for (int i = 1; i < recipes.Count; i++)
            {
                Recipe key = recipes[i];
                int j = i - 1;
                while (j >= 0 && string.Compare(recipes[j].Name, key.Name, StringComparison.OrdinalIgnoreCase) > 0)
                {
                    recipes[j + 1] = recipes[j];
                    j--;
                }
                recipes[j + 1] = key;
            }
            return recipes;
        }
    }
}