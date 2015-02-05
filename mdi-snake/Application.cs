using System;
using System.Diagnostics;
using mdi_api;
using mdi_api.factories;
using mdi_api.views.graphics.themes;
using mdi_api.views.items;
using mdi_snake.menus;

namespace mdi_snake {
    class Application : ConsoleApplication {

        public static Theme Theme;
        public static Resolution Resolution;

        private static Main _mainMenu;
        private readonly Item _quit;

        public Application() {

            Console.Title = "MDI-Snake";
            Resolution = Resolution.DEFAULT;
            Theme = Theme.DEFAULT;

            SetResolution(Resolution);

            _mainMenu = new Main();

            _quit = new Item("Quit", Item.Alignment.LEFT);
            _quit.AddHotKeyListener(ConsoleKey.Q, Terminate);

            _mainMenu.Get(ViewFactory.FOOTER).Add(_quit);
        }

        public static void ReckonResolution(Resolution resolution) {
            _mainMenu.ReckonResolution(Resolution, resolution);
        }

        public static void ApplyTheme(Theme theme) {
            _mainMenu.ApplyTheme(theme);
        }

        protected override void Terminate() {
            
            Debug.WriteLine("Application is shutting down...");
            _mainMenu.Dispose();
            _quit.RemoveHotKeyListener(Terminate);

            base.Terminate();
            Environment.Exit(0);
        }

        static void Main() {
            new Application();
        }
    }
}
