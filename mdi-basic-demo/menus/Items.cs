using System;
using mdi_api.factories;
using mdi_api.menus;
using mdi_api.views;
using mdi_api.views.items;
using mdi_api.views.items.seperators;

namespace mdi_basic_demo.menus {

    class Items : Menu {

        private readonly Item _align, _data, _description, _toggle;

        public Items() : base("Some item functionality") {

            View content = Get(ViewFactory.CONTENT);
            content.VerticalSpacing = 3;

            _align = new Item("Align") {Separator = new Separator(40)};
            _align.AddHotKeyListener(ConsoleKey.A, Align);

            _data = new Item("Data Object", Item.Alignment.CENTER, 0) {Separator = new Separator(40)};
            _data.AddHotKeyListener(ConsoleKey.O, Data);

            _description = new Item("Description", Item.Alignment.CENTER, null, "An item with a description") {Separator = new Separator(40)};
            _description.AddHotKeyListener(ConsoleKey.D, Description);

            _toggle = new Item("Toggle", Item.Alignment.CENTER, "ON") {Separator = new Separator(40)};
            _toggle.AddHotKeyListener(ConsoleKey.T, Toggle);

            Get(ViewFactory.CONTENT).AddAll(_align, _data, _description, _toggle);
        }

        public void Align() {
            Get(ViewFactory.CONTENT).DrawItems(true);
            _align.AlignmentProperty = _align.AlignmentProperty == Item.Alignment.CENTER? Item.Alignment.LEFT:Item.Alignment.CENTER;
            Get(ViewFactory.CONTENT).DrawItems();
        }

        public void Data() {
            Get(ViewFactory.CONTENT).DrawItems(true);
            int value = (int)_data.Data;
            value++;
            _data.Data = value;
            Get(ViewFactory.CONTENT).DrawItems();
        }

        public void Description() {
            if(_description.Description.Length > 150)
                return;

            Get(ViewFactory.CONTENT).DrawItems(true);
            _description.Description += " something ";
            Get(ViewFactory.CONTENT).DrawItems();
        }

        public void Toggle() {
            Get(ViewFactory.CONTENT).DrawItems(true);
            _toggle.Data = _toggle.Data == "ON"?"OFF":"ON";
            Get(ViewFactory.CONTENT).DrawItems();
        }

        public override void Dispose() {
            _align.RemoveHotKeyListener(Align);
            _data.RemoveHotKeyListener(Data);
            _description.RemoveHotKeyListener(Description);
            _toggle.RemoveHotKeyListener(Toggle);
        }
    }
}
