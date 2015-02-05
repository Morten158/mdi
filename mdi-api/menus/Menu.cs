using System;
using System.Collections.Generic;
using System.Diagnostics;
using mdi_api.factories;
using mdi_api.views;
using mdi_api.views.geometri;
using mdi_api.views.graphics.themes;
using mdi_api.views.items;

namespace mdi_api.menus {

    /// <summary>
    /// A menu composed of the three following views: header, content and a footer.
    /// The title passed to the menu will be inserted into a newely created item that is placed in the header.
    /// </summary>
    public class Menu {

        private string _title;
        /*  This is not a property because the remove functions
         *  have clear functionality, it would be wierd to use menu.Views.Add
         *  and menu.Remove.
         */
        private readonly Dictionary<string, View> _views;
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public Menu() : this("Title"){}

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="title">The title of the menu.</param>
        public Menu(string title) : this(title, Theme.DEFAULT){}

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="title">The title of the menu.</param>
        /// <param name="theme">The theme of the menu.</param>
        public Menu(string title, Theme theme) {
            _title = title;
            _views = ViewFactory.Standards(theme);
            Get("header").Add(new Item(title));
        }

        /// <summary>
        /// Draws the entire menu and all it's views along with all the items in the views.
        /// </summary>
        public virtual void Draw() {
            foreach(View view in _views.Values)
                view.Draw();
        }

        /// <summary>
        /// Adds a view to this menu.
        /// </summary>
        /// <param name="identifier">The identifier for this view.</param>
        /// <param name="view">The view to be placed in the dictionary.</param>
        public void Add(string identifier, View view) {
            _views.Add(identifier, view);
        }

        /// <summary>
        /// Gets a view based on the identifier.
        /// </summary>
        /// <param name="identifier">The identifier for the disired view.</param>
        /// <returns>Return the view or null if not found.</returns>
        public View Get(string identifier) {
            View view;
            try {
                view = _views[identifier];
            } catch(KeyNotFoundException e) {
                Debug.Write(e.Message);
                return null;
            }
            return view;
        }

        /// <summary>
        /// Gets all the views in a menu.
        /// </summary>
        /// <returns>Views to be returned.</returns>
        public Dictionary<string, View> GetAll() {
            return _views;
        } 

        /// <summary>
        /// Removes a view from this menu.
        /// </summary>
        /// <param name="identifier">The identifier for the disired view.</param>
        /// <param name="clear">Condition that specifies wether or not the view should clear it's boundries before it gets removed.</param>
        /// <returns>Return true if the view was removed.</returns>
        public bool Remove(string identifier, bool clear = false) {
            View view = Get(identifier);
            if(view != null) {
                if(clear)
                    view.Clear();
                view.RemoveAll();
                return _views.Remove(identifier);
            }
            return false;
        }
        
        /// <summary>
        /// Removes all views from this menu.
        /// </summary>
        /// <remarks>It is the devlopers responsibility to unsubscribe functions that listens to the items event handler.</remarks>
        /// <param name="clear">Condition that specifies wether or not the views should clear it's boundries(blank draw it) before they get removed.</param>
        public void RemoveAll(bool clear = false) {
            foreach(View view in _views.Values) {
                if(clear)
                    view.Clear();
                view.RemoveAll();
            }
            _views.Clear();
        }

        /// <summary>
        /// Property for the title of this menu, if the title is changed this function will automaticlly update the item and redraw the header view.
        /// </summary>
        public string Title {
            get {return _title;}
            set {
                View header = Get(ViewFactory.HEADER);
                if(header != null) {
                    Item title = header.Get(_title);
                    if(title != null) {
                        header.DrawItems(true);
                        title.Title = value;
                        header.DrawItems();
                    }
                }
                _title = value;
            }
        }

        /// <summary>
        /// Disables all the items for all the views in this menu.
        /// </summary>
        /// <param name="state">State that describes if the menu should be enabled or disabled.</param>
        public void SetEnabled(bool state) {
            foreach(View view in _views.Values) {
                view.SetEnabled(state);
            }
        }

        /// <summary>
        /// Applies a new theme to a menu.
        /// </summary>
        /// <param name="theme">Theme to be used.</param>
        /// <param name="draw">Redraw this menu to show changes.</param>
        public void ApplyTheme(Theme theme, bool draw = false) {
            if(theme == null)
                return;
            foreach(View view in _views.Values) 
                view.Theme = theme;
            if(draw)
                Draw();
        }

        /// <summary>
        /// Recalculates the views based on the current resolution and the new one.
        /// </summary>
        /// <param name="oldResolution"></param>
        /// <param name="newResolution"></param>
        public void ReckonResolution(Resolution oldResolution, Resolution newResolution) {
            if(oldResolution == null || newResolution == null)
                return;

            double x, y, w, h;
            foreach(View view in _views.Values) {
                x = Math.Round(((double) view.Rectangle.X/(double) oldResolution.Rows)*(double) newResolution.Rows);
                y = Math.Round(((double) view.Rectangle.Y/(double) oldResolution.Columns)*(double) newResolution.Columns);
                
                w = Math.Round((
                    ((double) view.Rectangle.Width+1)/(double) oldResolution.Rows)
                    *(double) newResolution.Rows);
                w-=1;
                
                h = Math.Round((((double) view.Rectangle.Height)/(double) oldResolution.Columns)*(double) newResolution.Columns);
                
                view.Rectangle = new Rectangle((int)x, (int)y, (int)w, (int)h);
                view.ReckonSeparator(oldResolution, newResolution);
            }
        }

        public virtual void Dispose() {}
    }
}
