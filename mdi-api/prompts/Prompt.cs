using System;
using System.Diagnostics;
using mdi_api.views;
using mdi_api.views.geometri;
using mdi_api.views.graphics.themes;
using mdi_api.views.items;
using mdi_api.views.items.seperators;

namespace mdi_api.prompts {

    /// <summary>
    /// A prompt view that asks the user for some input, in form of a string or an integer.
    /// </summary>
    public class Prompt : View {
        
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="title">Title of this prompt.</param>
        public Prompt(string title) : this(title, Theme.DEFAULT) {}
        
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="title">Title of this prompt.</param>
        /// <param name="theme">The theme to be used.</param>
        public Prompt(string title, Theme theme) : base(
            new Rectangle(Console.WindowWidth/2-(40/2), 10, 40, 10),
            theme, 
            new Margin(2, 2, 2, 2)) {

            Item i = new Item(title);
            i.Separator = new Separator(GetHorizontalSpace()-6);
            Add(i);
        }

        /// <summary>
        /// Prompts the user for a intger.
        /// </summary>
        /// <param name="range">The restriction for the input. An integer must be between the low and high provided.</param>
        /// <returns>The integer input by the user.</returns>
        public int GetInt(Range range) {
            Console.CursorVisible = true;
            int input = 0;
            while(input < range.Low || input > range.High) {

                Console.SetCursorPosition(Rectangle.X+Rectangle.Width/2, Rectangle.Y+Rectangle.Height/2);
                try {
                    input = int.Parse(Console.ReadLine());
                } catch(Exception e){Debug.Write(e.ToString());}
                
            }
            Console.CursorVisible = false;
            return input;
        }

        /// <summary>
        /// Prompts the user for a string.
        /// </summary>
        /// <returns>The string input by the user.</returns>
        public string GetString() {
            return GetString(3);
        }

        /// <summary>
        /// Prompts the user for a string.
        /// </summary>
        /// <param name="minimumLength">Minimum length of the string input.</param>
        /// <returns>The string input by the user.</returns>
        public string GetString(int minimumLength) {
            Console.CursorVisible = true;
            string input = "";
            while(input.Length <= minimumLength) {

                Console.SetCursorPosition(Rectangle.X+(Rectangle.Width/2)- minimumLength/2, Rectangle.Y+Rectangle.Height/2);
                try {
                    input = Console.ReadLine();
                } catch(Exception e) {Debug.Write(e.Message);}
            }
            Console.CursorVisible = false;
            return input;
        }
    }
}
