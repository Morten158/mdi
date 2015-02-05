namespace mdi_api.views.geometri {

    /// <summary>
    /// Helper class used to draw items on to a view.
    /// </summary>
    public class GeometricHelper {

        public readonly int x1, x2;
        public readonly int y1, y2;
        public readonly int length;
        public readonly int center;

        public int titleX;
        public int separatorX;
        public int dataX;
        public int descriptionX;

        /// <summary>
        /// Calculates values used in drawing based on a rectangle and a margin.
        /// </summary>
        /// <param name="rectangle">Rectangle calculated with.</param>
        /// <param name="margin">Margin calculated with.</param>
        public GeometricHelper(Rectangle rectangle, Margin margin) {
            x1 = rectangle.X + margin.Left;
            x2 = rectangle.X + rectangle.Width - margin.Right;
            
            y1 = rectangle.Y + margin.Top;
            y2 = rectangle.Height - margin.Bottom;

            length = x2-x1;
            center = length/2;
        }
    }
}
