using mdi_api.views;
using mdi_api.views.geometri;
using mdi_api.views.items;
using mdi_api.views.items.seperators;

namespace mdi_basic_demo.menus.views {
    class DisplayView : View {

        private readonly Item _displayItem;

        public DisplayView(Rectangle rectangle) : base(rectangle) {
            _displayItem = new Item("Select a recipe to display");
            Add(_displayItem);
        }

        public void DipsayItem(Item item) {
            if(_displayItem.Title == item.Title)
                return;

            DrawItems(true);
            _displayItem.Title = item.Title;
            _displayItem.Separator = new Separator(GetHorizontalSpace()-8);
            _displayItem.Data = item.Data;
            _displayItem.Description = item.Description;
            DrawItems();
        }
    }
}
