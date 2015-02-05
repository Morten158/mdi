using System;
using mdi_api.utils;
using mdi_api.views.geometri;
using mdi_api.views.graphics.themes;

namespace mdi_api.views.graphics {
    
    /// <summary>
    /// Class used to draw Views.
    /// </summary>
    public class Graphic {

        /// <summary>
        /// Draws background with a border within the given rectangle.
        /// </summary>
        /// <param name="rectangle">The rectangle being rendered.</param>
        /// <param name="theme">The theme to be used.</param>
        /// <param name="blank">Condition to wheter or not the rectangle should be rendered entirely with the background color of the graphics theme.
        /// This is usefull if you want to clear a specific rectangle.</param>
        public static void Draw(Rectangle rectangle, Theme theme, bool blank = false) {
            DrawBackground(rectangle, theme);
            DrawBorder(rectangle, theme, blank);
        }

        /// <summary>
        /// Draws the background.
        /// </summary>
        /// <param name="rectangle">Rectangle to be used.</param>
        /// <param name="theme">The theme to be used.</param>
        private static void DrawBackground(Rectangle rectangle, Theme theme) {

            if(Boundries.OutOfBounds(rectangle))
                throw new Exception("Out of bounds exception: " + rectangle);


            for(int y = 0; y < rectangle.Height; y++) {
                for(int x = 0; x < rectangle.Width; x++) {

                    Console.SetCursorPosition(rectangle.X + x, rectangle.Y + y);
                    Console.BackgroundColor = theme.Background;
                    Console.Write(" ");
                }
            }
        }

        /// <summary>
        /// Draws the border.
        /// </summary>
        /// <param name="rectangle">Rectangle to be used.</param>
        /// <param name="theme">The theme to be used.</param>
        /// <param name="blank">If the border should be drawn with the background color(blank).</param>
        private static void DrawBorder(Rectangle rectangle, Theme theme, bool blank = false) {
            if(Boundries.OutOfBounds(rectangle))
                throw new Exception("Out of bounds exception" + rectangle);

            Console.ForegroundColor = blank?theme.Background:theme.Border;

            for(int x = 0; x <= rectangle.Width; x++) {
                Console.SetCursorPosition(rectangle.X + x, rectangle.Y);
                Console.Write(Unicode.BorderCodes.RECT_TOP);
                Console.SetCursorPosition(rectangle.X + x, rectangle.Y + rectangle.Height);
                Console.Write(Unicode.BorderCodes.RECT_BOTTOM);
            }

            for(int y = 0; y <= rectangle.Height; y++) {
                Console.SetCursorPosition(rectangle.X, rectangle.Y + y);
                Console.Write(Unicode.BorderCodes.RECT_LEFT);
                Console.SetCursorPosition(rectangle.X + rectangle.Width, rectangle.Y + y);
                Console.Write(Unicode.BorderCodes.RECT_RIGHT);
            }
        }
    }
}
