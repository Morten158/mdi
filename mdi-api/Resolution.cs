using System;
using mdi_api.views.items;

namespace mdi_api {

    /// <summary>
    /// Represents a Resolution that can be applied to the console.
    /// The reason why the Resolution Class takes a title and a description is because a reference of an object from this class
    /// can be sent as an argument to a newely created item, and the item will use the Resolution's attributes in it's text. 
    /// </summary>
    public class Resolution {

        public const int PIXELS_PER_ROW = 10;
        public const int PIXELS_PER_COLUMN = 20;

        public static readonly Resolution DEFAULT = new Resolution("Default", "This is the standard resolution.", 1024/PIXELS_PER_ROW, 760/PIXELS_PER_COLUMN);
        public static readonly Resolution WIDE = new Resolution("Wide", "This is the wide resolution.", 1360/PIXELS_PER_ROW, 760/PIXELS_PER_COLUMN);
        public static readonly Resolution HUGE = new Resolution("Huge", "This is the huge resolution.", (1920/PIXELS_PER_ROW)-6, (1080/PIXELS_PER_COLUMN)-6);

        public string Title { get; set; }
        public string Description { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="rows">Number of rows for this resolution.</param>
        /// <param name="columns">Number of columns for this resolution.</param>
        public Resolution(int rows, int columns) : this("Title", "Description", rows, columns) {}

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="title">The title for this resolution, usefull when passed to an Item.</param>
        /// <param name="description">The description for this resolution, usefull when passed to an Item.</param>
        /// <param name="rows">Number of rows for this resolution.</param>
        /// <param name="columns">Number of columns for this resolution.</param>
        /// <seealso cref="Item"/>
        public Resolution(string title, string description, int rows, int columns) {
            Title = title;
            Description = description;
            Rows = rows;
            Columns = columns;
        }

        public override string ToString() {
            return String.Format("Resolution: [{0}, {1}]", Rows, Columns);
        }

        public override bool Equals(object obj) {
            if(obj == null)
                return false;

            if(!(obj is Resolution))
                return false;

            Resolution r2 = (Resolution)obj;
            
            return (Title == r2.Title && Description == r2.Description && Columns == r2.Columns && Rows == r2.Rows);
        }

        public static bool operator ==(Resolution r1, Resolution r2) {
            if(ReferenceEquals(r1, null))
                return ReferenceEquals(r2, null);

            return Equals(r1, r2);
        }

        public static bool operator !=(Resolution r1, Resolution r2) {
            return !(r1 == r2);
        }

        public static Resolution operator +(Resolution r1, Resolution r2) {
            return new Resolution(r1.Rows+r2.Rows, r1.Columns+r2.Columns);
        }

        public static Resolution operator -(Resolution r1, Resolution r2) {
            return new Resolution(r1.Rows-r2.Rows, r1.Columns-r2.Columns);
        }
    }
}
