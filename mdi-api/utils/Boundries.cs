using System;
using mdi_api.views.geometri;

namespace mdi_api.utils {

    /// <summary>
    /// Util class that can be used to check for out of bounds checks.
    /// </summary>
    public class Boundries {

        /// <summary>
        /// Checks if the given values is outside the terminal dimensions.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool OutOfBounds(int x, int y) {
            return (x < 0 || x > Console.WindowWidth-1 || y < 0 || y > Console.WindowHeight-1);
        }

        /// <summary>
        /// Checks if the given values is outside the rectangle dimensions.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="rectangle"></param>
        /// <returns></returns>
        public static bool OutOfBounds(int x, int y, Rectangle rectangle) {
            return (x < rectangle.X || x > rectangle.X+rectangle.Width || y < rectangle.Y || y > rectangle.Y+rectangle.Height);
        }

        /// <summary>
        /// Checks if the given values is outside the terminal dimensions.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static bool OutOfBounds(int x, int y, int width, int height) {
            return (x < 0 || x+width > Console.WindowWidth-1 || y < 0 || y+height > Console.WindowHeight-1);
        }

        /// <summary>
        /// Checks if the values of the rectangle is outside the terminal dimensions.
        /// </summary>
        /// <param name="rectangle"></param>
        /// <returns></returns>
        public static bool OutOfBounds(Rectangle rectangle) {
            return (rectangle.X < 0 || rectangle.X+rectangle.Width > Console.WindowWidth-1 || rectangle.Y < 0 || rectangle.Y+rectangle.Height > Console.WindowHeight-1);
        }
    }
}
