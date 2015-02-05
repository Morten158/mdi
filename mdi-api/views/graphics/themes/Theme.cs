using System;

namespace mdi_api.views.graphics.themes {

    /// <summary>
    /// Represents a Theme that can be used by menus and views.
    /// The reason why the Theme Class takes a title and a description is because a reference of an object from this class
    /// can be sent as an argument to a newely created item, and the item will use the theme's attributes in it's text. 
    /// </summary>
    public class Theme {

        #region statics
        public static readonly Theme DEFAULT = new Theme("Default", "This is the standard theme.", ConsoleColor.Black, ConsoleColor.Gray, ConsoleColor.White, ConsoleColor.Yellow);
        public static readonly Theme SHARP_BURGUNDY = new Theme("Sharp Burgundy", "This is the Burgundy theme.", ConsoleColor.DarkRed, ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.Blue);
        public static readonly Theme CONTRAST = new Theme("Contrast", "This is the Contrast theme.", ConsoleColor.White, ConsoleColor.DarkGray, ConsoleColor.Black, ConsoleColor.Red);
        public static readonly Theme CUSTOM_ONE = new Theme("Custom-One", "This is the Custom-One theme.", ConsoleColor.DarkCyan, ConsoleColor.DarkGreen, ConsoleColor.Blue, ConsoleColor.Red);
        #endregion /statics

        #region properties
        public String Title { get; set; }
        public String Description { get; set; }
        public ConsoleColor Background { get; set; }
        public ConsoleColor Border { get; set; }
        public ConsoleColor Foreground { get; set; }
        public ConsoleColor HotKey { get; set; }
        #endregion /properties

        #region constructors
        /// <summary>
        /// Constructor.
        /// </summary>
        public Theme() : this("Title", "Description", DEFAULT.Background, DEFAULT.Border, DEFAULT.Foreground, DEFAULT.HotKey) {}

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="title">Title of this theme.</param>
        /// <param name="description">Description of this theme.</param>
        public Theme(String title, String description)
            : this(title, description, DEFAULT.Background, DEFAULT.Border, DEFAULT.Foreground, DEFAULT.HotKey) {}

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="background">Background color.</param>
        /// <param name="border">Border color.</param>
        /// <param name="foreground">Foreground color.</param>
        /// <param name="hotkey">Hotkey color.</param>
        public Theme(ConsoleColor background, ConsoleColor border, ConsoleColor foreground, ConsoleColor hotkey)
            : this("Title", "Description", background, border, foreground, hotkey) {}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title">Title of this theme.</param>
        /// <param name="description">Description of this theme.</param>
        /// <param name="background">Background color.</param>
        /// <param name="border">Border color.</param>
        /// <param name="foreground">Foreground color.</param>
        /// <param name="hotkey">Hotkey color.</param>
        public Theme(String title, String description,
            ConsoleColor background, ConsoleColor border, ConsoleColor foreground, ConsoleColor hotkey) {
            Title = title;
            Description = description;
            Background = background;
            Border = border;
            Foreground = foreground;
            HotKey = hotkey;
        }
        #endregion /constructors

        public override bool Equals(object obj) {
            if(obj == null)
                return false;

            if(!(obj is Resolution))
                return false;

            Theme t2 = (Theme) obj;

            return (Title == t2.Title && Description == t2.Description && Background == t2.Background && Border == t2.Border
                && Foreground == t2.Foreground && HotKey == t2.HotKey);
        }

        public static bool operator ==(Theme t1, Theme t2) {

            if(ReferenceEquals(t1, null))
                return ReferenceEquals(t2, null);

            return t1.Equals(t2);
        }

        public static bool operator !=(Theme t1, Theme t2) {return !(t1 == t2);}

    }
}
