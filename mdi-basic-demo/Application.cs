using System;
using mdi_api;
using mdi_api.factories;
using mdi_api.views.items;
using mdi_basic_demo.menus;

namespace mdi_basic_demo {
    class Application : ConsoleApplication {

        private readonly Main _main;
        private readonly Item _quit;

        public Application() {

            Console.Title = "mdi-Basic-Demo";

            Resolution resolution = new Resolution(100, 36);
            SetResolution(resolution);

            _main = new Main();

            _quit = new Item("Quit", Item.Alignment.LEFT);
            _quit.AddHotKeyListener(ConsoleKey.Q, Terminate);
            _main.Get(ViewFactory.FOOTER).Add(_quit);
        }

        protected override void Terminate() {
            _main.Dispose();
            _quit.RemoveHotKeyListener(Terminate);
            base.Terminate();
        }

        static void Main(string[] args) {
            new Application();
        }
    }
}
