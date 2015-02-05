using System;

namespace mdi_api.views.geometri {

    /// <summary>
    /// Rectangle Class, used in various scenarios, most commonly used by a view to represent it's bounds or area.
    /// </summary>
    public class Rectangle {

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="x">horizontal start value.</param>
        /// <param name="y">vertical start value.</param>
        /// <param name="width">width.</param>
        /// <param name="height">height.</param>
        public Rectangle(int x, int y, int width, int height) {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public override string ToString() {
            return String.Format("Rectangle:[{0}, {1}, {2}, {3}]", X, Y, Width, Height);
        }
    }
}
