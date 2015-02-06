using System;
using mdi_api.menus;
using mdi_api.views;
using mdi_api.views.geometri;
using mdi_api.views.items;

namespace mdi_basic_demo.menus.views {
    class Views : Menu {

        private readonly RecipeView _recipeView;
        private readonly DisplayView _displayView;

        public Views() {

            RemoveAll();

            _recipeView = new RecipeView(new Rectangle(0, 0, 30, Console.WindowHeight-6));
            _recipeView.AddRecipeChangeListener(OnDisplayItem);
            Add("recipe_view", _recipeView);

            _displayView = new DisplayView(new Rectangle(30, 0, Console.WindowWidth-31, Console.WindowHeight-1));
            Add("display_view", _displayView);

            View bottomView = new View(
                new Rectangle(0, Console.WindowHeight-5, 30, 4));
            bottomView.Add(new Item("Back"));

            Add("bottom_view", bottomView);
        }

        public void OnDisplayItem(Item item) {
            _displayView.DipsayItem(item);
        }

        public override void Dispose() {
            _recipeView.Dispose();
            _recipeView.RemoveRecipeChangeListener(OnDisplayItem);
        }
    }
}
