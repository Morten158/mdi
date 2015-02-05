namespace mdi_api.prompts {

    /// <summary>
    /// Class that holds to integers in form of a low and high value.
    /// </summary>
    public class Range {

        public int Low { get; set; }
        public int High { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="low">Low integer.</param>
        /// <param name="high">High integer.</param>
        public Range(int low, int high) {
            Low = low;
            High = high;
        }
    }
}
