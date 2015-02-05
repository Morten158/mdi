
namespace mdi_api.views.items.seperators {

    /// <summary>
    /// Represents a horizontal line beneath an item. As a default, item has a separator with a width of -1, which means it is not drawn.
    /// </summary>
    public class Separator {

        public int Width { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="width">the width of this separator.</param>
        public Separator(int width) {
            Width = width;
        }
    }
}
