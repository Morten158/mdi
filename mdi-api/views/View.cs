using System;
using System.Collections.Generic;
using mdi_api.views.geometri;
using mdi_api.views.graphics;
using mdi_api.views.graphics.themes;
using mdi_api.views.items;

namespace mdi_api.views {

    /// <summary>
    /// Represents a view with a dictionary of items that resides within the view.
    /// A View concists of a rectangle which represents the boundries this view has.
    /// It also has a theme that is used on this view.
    /// Lastly it uses a margin to control where it should start drawing from.
    /// </summary>
    public class View {

        public Rectangle Rectangle { get; set; }
        public Theme Theme { get; set; }
        public Margin Margin { get; set; }
        public int VerticalSpacing { get; set; }

        private int _accumulation;

        private readonly Dictionary<string, Item> _items;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="rectangle">The rectangle that represents this view's boundires.</param>
        public View(Rectangle rectangle) : this(rectangle, Theme.DEFAULT, new Margin()) {}

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="rectangle">The rectangle that represents this view's boundires.</param>
        /// <param name="theme">The theme that should be used.</param>
        public View(Rectangle rectangle, Theme theme) : this(rectangle, theme, new Margin()) {}

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="rectangle">The rectangle that represents this view's boundires.</param>
        /// <param name="theme">The theme that should be used.</param>
        /// <param name="margin">The margin of this view.</param>
        public View(Rectangle rectangle, Theme theme, Margin margin) {
            _items =  new Dictionary<string, Item>(4);
            Rectangle = rectangle;
            Theme = theme;
            Margin = margin;
            Draw();
        }

        /// <summary>
        /// Draws the background, border and items of this view.
        /// </summary>
        public void Draw() {
            Graphic.Draw(Rectangle, Theme);
            DrawItems();
            Console.SetWindowPosition(0, 0);
        }

        /// <summary>
        /// Draw or clears all the items in this view.
        /// This function simply projects all the items text on to this view or removes it if the boolean blank is set to true, which
        /// would be usefull if the properties of a item is changed.
        /// </summary>
        /// <param name="blank">Condition to draw the items with the themes background color.</param>
        public void DrawItems(bool blank = false) {
            _accumulation = 0;
            GeometricHelper helper = new GeometricHelper(Rectangle, Margin);

            foreach(Item item in _items.Values) {
                int itemHeight = item.DrawItem(helper,
                    blank?new Theme(Theme.Background, Theme.Background, Theme.Background, Theme.Background): Theme,
                    _accumulation);
                _accumulation += itemHeight+VerticalSpacing;
            }
        }

        /// <summary>
        /// Adds an item.
        /// </summary>
        /// <param name="item">Item that is going to be added.</param>
        public void Add(Item item) {
            _items.Add(item.Title, item);
            DrawItems();
        }

        /// <summary>
        /// Adds all the items that is passed to this function.
        /// </summary>
        /// <param name="items">Params of item.</param>
        public void AddAll(params Item[] items) {
            foreach(Item item in items) {_items.Add(item.Title, item);}
            DrawItems();
        }

        /// <summary>
        /// Gets an item.
        /// </summary>
        /// <param name="identifier">identifier.</param>
        /// <returns>Returns the item if it was found and null if not.</returns>
        public Item Get(string identifier) {
            Item item;
            try {
                item = _items[identifier];
            } catch(KeyNotFoundException) {return null;}
            return item;
        }

        /// <summary>
        /// Removes an item.
        /// </summary>
        /// <param name="item">Item to be looked for.</param>
        /// <returns>Returns true if the item was successfully removed.</returns>
        public bool Remove(Item item) {
            bool removed = false;
            DrawItems(true);
            foreach(Item i in _items.Values) {
                if(i == item)
                    removed = _items.Remove(item.Title);
            }
            DrawItems();
            return removed;
        }

        /// <summary>
        /// Removes an item from this view.
        /// </summary>
        /// <param name="identifier">identifier.</param>
        /// <returns>Returns true if the item was successfully removed.</returns>
        public void Remove(string identifier) {
            DrawItems(true);
            _items.Remove(identifier);
            DrawItems();
        }

        /// <summary>
        /// Removes all items.
        /// </summary>
        public void RemoveAll() {
            DrawItems(true);
            _items.Clear();
            DrawItems();
        }

        /// <summary>
        /// Clears this view by drawing it blank with the theme background color.
        /// </summary>
        public void Clear() {
            Graphic.Draw(Rectangle, Theme, true);
            Console.SetWindowPosition(0, 0);
        }

        /// <summary>
        /// The Horizontal space available in the this view.
        /// </summary>
        /// <returns>Returns the horizontal space available.</returns>
        public int GetHorizontalSpace() {
            int x1 = Rectangle.X + Margin.Left;
            int x2 = Rectangle.X + Rectangle.Width - Margin.Right;
            return x2-x1;
        }

        /// <summary>
        /// Enables or disables the hot key's of each item in this view.
        /// </summary>
        /// <param name="state">Enable or disable state.</param>
        public void SetEnabled(bool state) {
            foreach(Item item in _items.Values) {
                item.Enabled = state;
            }
        }

        /// <summary>
        /// Recalculates the separators based on the current resolution and the new one.
        /// </summary>
        /// <param name="oldResolution"></param>
        /// <param name="newResolution"></param>
        public void ReckonSeparator(Resolution oldResolution, Resolution newResolution) {
            if(oldResolution == null || newResolution == null)
                return;
            double w;
            foreach(Item item in _items.Values) {
                w = (int) Math.Round(((double) item.Separator.Width/(double) oldResolution.Rows)*(double) newResolution.Rows);
                if((int) w>GetHorizontalSpace())
                    w = GetHorizontalSpace();
                item.Separator.Width = (int)w;
            }
        }
    }
}
