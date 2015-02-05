using System;
using System.Collections.Generic;
using mdi_api.views;
using mdi_api.views.geometri;
using mdi_api.views.graphics.themes;

namespace mdi_api.factories {

    /// <summary>
    /// Factory for views.
    /// </summary>
    public class ViewFactory {

        public const string HEADER = "header";
        public const string CONTENT = "content";
        public const string FOOTER = "footer";

        /// <summary>
        /// Gets a dictionary with the standard views: Header, Content & Footer.
        /// </summary>
        /// <param name="theme">Theme to be applied.</param>
        /// <returns>Returns a dictionary with all of the standard views.</returns>
        public static Dictionary<string, View> Standards(Theme theme) {
            return new Dictionary<string, View>
                {
                    {HEADER, Header(theme)},
                    {CONTENT, Content(theme)},
                    {FOOTER, Footer(theme)}
                };
        }

        /// <summary>
        /// Gets the header view.
        /// </summary>
        /// <param name="theme">Theme to be applied.</param>
        /// <returns>Returns the header view.</returns>
        public static View Header(Theme theme) {
            return new View(
                new Rectangle(0, 0, Console.WindowWidth-1, 4),
                theme,
                new Margin(Margin.STANDARD_MARGIN, Margin.STANDARD_MARGIN, Margin.STANDARD_MARGIN, Margin.STANDARD_MARGIN));
        }

        /// <summary>
        /// Gets the content view.
        /// </summary>
        /// <param name="theme">Theme to be applied.</param>
        /// <returns>Returns the content view.</returns>
        public static View Content(Theme theme) {
            return new View(
                new Rectangle(0, 4, Console.WindowWidth-1, Console.WindowHeight-9),
                theme,
                new Margin(Margin.STANDARD_MARGIN, Margin.STANDARD_MARGIN, Margin.STANDARD_MARGIN, Margin.STANDARD_MARGIN));
        }

        /// <summary>
        /// Gets the footer view.
        /// </summary>
        /// <param name="theme">Theme to be applied.</param>
        /// <returns>Returns the footer view.</returns>
        public static View Footer(Theme theme) {
            return new View(
                new Rectangle(0, Console.WindowHeight-5, Console.WindowWidth-1, 4),
                theme,
                new Margin(Margin.STANDARD_MARGIN, Margin.STANDARD_MARGIN, Margin.STANDARD_MARGIN, Margin.STANDARD_MARGIN));
        }
    }
}
