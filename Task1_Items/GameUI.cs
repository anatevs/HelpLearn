using System;
using System.Collections.Generic;

namespace Task1_Items
{
    public class GameUI
    {
        private Dictionary<string, string> _options = new();

        public void AddOption(string key, string value)
        {
            if (_options.ContainsKey(key))
            {
                Console.WriteLine($"option with {key} also exists in options");
                return;
            }

            _options.Add(key, value);
        }

        public void ShowOptions()
        {
            Console.WriteLine();
            Console.WriteLine("Choose from options:");

            foreach (var option in _options)
            {
                Console.WriteLine($"{option.Key} - {option.Value}");
            }
        }
    }
}
