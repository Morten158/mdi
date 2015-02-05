using System;
using mdi_api;
using mdi_api.factories;
using mdi_api.menus;
using mdi_api.views;
using mdi_api.views.geometri;
using mdi_api.views.items;
using mdi_api.views.items.seperators;

namespace mdi_snake.menus {
    class Resolutions : Menu {

        private readonly Item _default, _wide, _huge;

        public Resolutions()
            : base("Resolutions", Application.Theme) {

            View content = Get(ViewFactory.CONTENT);
            content.Margin = new Margin(8, 8, 2, 2);
            content.VerticalSpacing = 1;

            _default = new Item(Resolution.DEFAULT) {
                Separator = new Separator(content.GetHorizontalSpace())
            };
            _default.AddHotKeyListener(ConsoleKey.D, Default);

            _wide = new Item(Resolution.WIDE) {
                Separator = new Separator(content.GetHorizontalSpace())
            };
            _wide.AddHotKeyListener(ConsoleKey.W, Wide);

            _huge = new Item(Resolution.HUGE) {
                Separator = new Separator(content.GetHorizontalSpace())
            };
            _huge.AddHotKeyListener(ConsoleKey.H, Huge);

            content.AddAll(_default, _wide, _huge);
        }

        public void Default() {SetResolution(Resolution.DEFAULT);}
        public void Wide() {SetResolution(Resolution.WIDE);}
        public void Huge() {SetResolution(Resolution.HUGE);}

        public void SetResolution(Resolution resolution) {
            Application.ReckonResolution(resolution);
            ReckonResolution(Application.Resolution, resolution);
            Application.Resolution = resolution;
            ConsoleApplication.SetResolution(resolution);
            Draw();
        }

        public override void Dispose() {
            _default.RemoveHotKeyListener(Default);
            _wide.RemoveHotKeyListener(Wide);
            _huge.RemoveHotKeyListener(Huge);
        }
    }
}
