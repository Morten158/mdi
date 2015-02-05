using System;
using System.Collections.Generic;
using System.Diagnostics;
using mdi_api.factories;
using mdi_api.views;
using mdi_api.views.items;

namespace mdi_api.menus.managers {

    /// <summary>
    /// A Navigator is commonly used within a Menu class that has items that represents a new menu that can be navigated to.
    /// </summary>
    public class Navigator {

        private readonly Menu _parent;
        private Menu _inFocus;
        private Item _navItem;

        /// <summary>
        /// Costructor.
        /// </summary>
        /// <param name="parent">The base/parent menu that is controlling the navigation.</param>
        public Navigator(Menu parent) {
            _parent = parent;
        }

        /// <summary>
        /// Locks the parent menu and focuses on the menu given.
        /// </summary>
        /// <param name="menu">The menu that requests focus.</param>
        /// <param name="identifier">The item that should be used as a back item. If left blank, this function
        /// will create a new "Back" item in the footer.</param>
        public virtual void Focus(Menu menu, string identifier = "Back") {
            if(_inFocus != null)
                Release();

            _parent.SetEnabled(false);

            _inFocus = menu;
            View v = null;
            
            foreach (View view in menu.GetAll().Values) {
                _navItem = view.Get(identifier);
                if(_navItem != null) {
                    v = view;
                    v.DrawItems(true);
                    
                    break;
                }
            }
            if(_navItem == null) {
                View footer = menu.Get(ViewFactory.FOOTER);
                if(footer == null)
                    return;

                _navItem = new Item("Back", Item.Alignment.LEFT);
                v = footer;
                v.Add(_navItem);
            }

            List<ConsoleKey> keys = _navItem.GetAvailableHotKeys();
            ConsoleKey k = keys.Count < 1?ConsoleKey.B:keys[0];
            _navItem.AddHotKeyListener(k, Release);
            v.DrawItems();
        }

        /// <summary>
        /// Releases the menu in focus and returns to the menu controlling the navigation.
        /// </summary>
        public virtual void Release() {
            if(_navItem == null)
                return;

            _navItem.RemoveHotKeyListener(Release);
            _inFocus.Dispose();

            _parent.SetEnabled(true);
            _parent.Draw();

            _inFocus = null;
            _navItem = null;
        }

    }
}
