using System;

namespace mdi_api.views.geometri {

    /// <summary>
    /// Represents margins for a view.
    /// </summary>
    public class Margin {

        public const int STANDARD_MARGIN = 2;

        public int Left { get; set; }
        public int Right { get; set; }
        public int Top { get; set; }
        public int Bottom { get; set; }

        /// <summary>
        /// Constructor, uses the default Margin values of 2.
        /// </summary>
        public Margin() : this(STANDARD_MARGIN, STANDARD_MARGIN, STANDARD_MARGIN, STANDARD_MARGIN) {}

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="left">Left Margin.</param>
        /// <param name="right">Right Margin.</param>
        /// <param name="top">Top Margin.</param>
        /// <param name="bottom">Bottom Margin.</param>
        public Margin(int left, int right, int top, int bottom) {
            Left = left;
            Right = right;
            Top = top;
            Bottom = bottom;
        }

        public override string ToString() {
            return String.Format("Margin: [{0}, {1}, {2}, {3}]", Left, Right, Top, Bottom);
        }
    }
}
