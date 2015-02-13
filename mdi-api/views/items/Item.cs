using System;
using System.Collections.Generic;
using System.Linq;
using mdi_api.utils;
using mdi_api.views.geometri;
using mdi_api.views.graphics.themes;
using mdi_api.views.items.seperators;

namespace mdi_api.views.items {

    /// <summary>
    /// Represents an item that is commonly resides within a view. A hotkey event can be subscribed on as well.
    /// </summary>
    public class Item {

        public delegate void HotKeyListener();
        private event HotKeyListener HotKeyPressed;

        public enum Alignment {CENTER, LEFT}
        
        public string Title { get; set; }
        public object Data { get; set; }
        public string Description { get; set; }
        public Alignment AlignmentProperty { get; set; }
        public Separator Separator { get; set; }
        public ConsoleKey Binder { get; private set;}
        public bool Enabled { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="theme">The theme that is described by this item.</param>
        public Item(Theme theme) : this(theme.Title, Alignment.CENTER, theme.Background + "-" + theme.Border + "-" + theme.Foreground + "-" + theme.HotKey, theme.Description) {}
        
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="resolution">The resolution that is described by this item.</param>
        public Item(Resolution resolution) : this(resolution.Title, Alignment.CENTER, resolution.Rows + "x" + resolution.Columns, resolution.Description) {}
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public Item() : this("Item") {}

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="title">The title of this item.</param>
        public Item(string title) : this(title, Alignment.CENTER) {}
        
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="title">The title of this item.</param>
        /// <param name="alignment">The alignment of this item.</param>
        public Item(string title, Alignment alignment) : this(title, alignment, null) {}

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="title">Title of this item.</param>
        /// <param name="alignment">Alignment of this item.</param>
        /// <param name="data">Data object rendered by this item.</param>       
        public Item(string title, Alignment alignment, object data) : this(title, alignment, data, "") {}

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="title">Title of this item.</param>
        /// <param name="alignment">Alignment of this item.</param>
        /// <param name="data">Data object rendered by this item.</param>
        /// <param name="description">Description of this item.</param>   
        public Item(string title, Alignment alignment, object data, string description) {
            Title = title;
            AlignmentProperty = alignment;
            Data = data;
            Description = description;
            Separator = new Separator(-1);
            Enabled = true;
        }

        /// <summary>
        /// Registers an event of a specific key that is pressed.
        /// </summary>
        /// <param name="keyPressed">The key pressed in the console application.</param>
        private void OnKeyPressed(ConsoleKey keyPressed) {
            if(HotKeyPressed != null && Enabled)
                if(keyPressed == Binder)
                    HotKeyPressed();
        }

        /// <summary>
        /// Forces this item's event and calls on the KeyPressed function for all that listens. The item must be enabled.
        /// </summary>
        public void ForceKeyPressed() {
            if(HotKeyPressed != null && Enabled)
                HotKeyPressed();
        }

        /// <summary>
        /// Adds a HotKey to this item.
        /// </summary>
        /// <param name="binder">The console key to be binded.</param>
        /// <param name="listener">The listener that registers the event.</param>
        public void AddHotKeyListener(ConsoleKey binder, HotKeyListener listener) {
            if(HotKeyPressed != null)
                return;
            EventDispatchThread.AddKeyListener(OnKeyPressed);
            Binder = binder;
            HotKeyPressed += listener;
        }

        /// <summary>
        /// Removes a HotKey from this item.
        /// </summary>
        /// <param name="listener">The listener to be removed.</param>
        public void RemoveHotKeyListener(HotKeyListener listener) {
            if(HotKeyPressed == null)
                return;
            EventDispatchThread.RemoveKeyListener(OnKeyPressed);
            HotKeyPressed -= listener;
        }

        /// <summary>
        /// Draws this item, requires a Geometric Helper to aid with the boundries rules. Also a integer that keeps track of the height for this specific item is necessery.
        /// </summary>
        /// <param name="helper">Helper to be used.</param>
        /// <param name="theme">Theme to be used.</param>
        /// <param name="accumulation">The height that this item should be drawn at.</param>
        /// <returns>Returns the height of the item when rendered.</returns>
        public int DrawItem(GeometricHelper helper, Theme theme, int accumulation) {

            if(Title.Length > helper.length || Separator.Width > helper.length)
                throw new Exception("Not enough space to draw.");

            Align(helper);

            int startY = accumulation + helper.y1;

            DrawTitle(helper, theme, startY);
            DrawSeparator(helper, startY);
            DrawData(helper, startY);
            int rows = DrawDescription(helper, startY);

            return Description.Length==0?2:2+rows+1;
        }

        /// <summary>
        /// Aligns this item at the center or to the left of a view.
        /// </summary>
        /// <param name="helper">The helper to be used.</param>
        private void Align(GeometricHelper helper) {
            switch(AlignmentProperty) {

                case (Alignment.LEFT): {
                        helper.separatorX = helper.titleX = helper.x1;
                        if(Data != null) {
                            helper.dataX = helper.separatorX + Title.Length+1;
                        }
                        break;
                    }
                case (Alignment.CENTER): {
                        helper.separatorX = helper.x1 +(helper.length/2) - (Separator.Width/2);
                        helper.titleX = helper.x1 + (helper.center - Title.Length/2);

                        if(Data != null) {

                            int dataLength = Data.ToString().Length;
                            if(Separator.Width > Title.Length) {
                                helper.titleX = helper.separatorX;
                                helper.dataX = helper.separatorX + Separator.Width - dataLength;
                            } else {
                                helper.titleX = helper.x1 + (helper.center - (Title.Length+dataLength)/2);
                                helper.dataX = helper.titleX + Title.Length + 1;
                            }
                        }
                        break;
                    }
            }
        }

        /// <summary>
        /// Draws a title for this item.
        /// </summary>
        /// <param name="helper">The helper to be used.</param>
        /// <param name="theme">The theme to be used.</param>
        /// <param name="startY">Start position on the y-axis in a view.(Column index)</param>
        private void DrawTitle(GeometricHelper helper, Theme theme, int startY) {
            char binder = Binder.ToString()[0];

            for(int i = 0; i < Title.Length; i++) {
                int x = helper.titleX + i;
                Console.SetCursorPosition(x, startY);

                char c = Title[i];
                Console.ForegroundColor = binder == c ? theme.HotKey : theme.Foreground;
                Console.Write(c);
            }
            Console.ForegroundColor = theme.Foreground;
        }

        /// <summary>
        /// Draws the separator for this item.
        /// </summary>
        /// <param name="helper">The helper to be used.</param>
        /// <param name="startY">Start position on the y-axis in a view.(Column index).</param>
        private void DrawSeparator(GeometricHelper helper, int startY) {
            for(int i = 0; i <= Separator.Width; i++) {

                int x = helper.separatorX + i;
                Console.SetCursorPosition(x, startY+1);
                Console.Write(Unicode.ItemCodes.SEPARATOR);
            }
        }

        /// <summary>
        /// Draws the data in form of a toString in this item.
        /// </summary>
        /// <param name="helper">The helper to be used.</param>
        /// <param name="startY">Start position on the y-axis in a view.(Column index).</param>
        private void DrawData(GeometricHelper helper, int startY) {
            if(Data != null) {
                Console.SetCursorPosition(helper.dataX, startY);
                Console.WriteLine(Data.ToString());
            }
        }

        /// <summary>
        /// Draws the description of this item.
        /// </summary>
        /// <param name="helper">The helper to be used.</param>
        /// <param name="startY">Start position on the y-axis in a view.(Column index).</param>
        private int DrawDescription(GeometricHelper helper, int startY) {

            if(Description.Length == 0)
                return 1;

            int availableLength = Separator.Width>Title.Length?Separator.Width:helper.length;
            int sx = Separator.Width>Title.Length?helper.separatorX:helper.titleX;
            string[] words = Description.Split(' ');

            int written = 0;
            int rows = startY+2;
            string w;
            foreach (string word in words) {
                w = word;
                int toWrite = word.Length;
                if(toWrite + written > availableLength || w.Contains("*")) {
                    rows++;
                    written = 0;
                } else if(w.Contains("~")) {
                    w = w.Replace("~", "");
                    rows++;
                    written = 0;
                    toWrite--;
                }
                Console.SetCursorPosition(sx+written, rows);
                Console.Write(w);
                written += toWrite+1;
            }
            return rows-startY-1;
        }

        /// <summary>
        /// Checks the title of this item for capital letters that can be used as a hotkey.
        /// </summary>
        /// <returns>Returns the list of available hotkeys for this item.</returns>
        public List<ConsoleKey> GetAvailableHotKeys() {
            ConsoleKey key = ConsoleKey.B;
            return (from c in Title where char.IsUpper(c) where Enum.TryParse(c.ToString(), out key) select key).ToList();
        }
    }
}
