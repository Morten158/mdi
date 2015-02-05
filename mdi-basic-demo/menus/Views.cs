using System;
using mdi_api.menus;
using mdi_api.views;
using mdi_api.views.geometri;
using mdi_api.views.items;
using mdi_api.views.items.seperators;

namespace mdi_basic_demo.menus {
    class Views : Menu {

        private readonly View _displayView;
        private readonly Item _displayItem;

        private readonly Item _pasta;
        const string PASTA = "*8 ounces whole wheat fusilli pasta. " +
                                       "*3 strips bacon. " +
                                       "*3 cloves garlic, thinly sliced. " +
                                       "*One 28-ounce can whole peeled tomatoes, crushed by hand, with juices. " +
                                       "*1/4 teaspoon chilli flakes. " +
                                       "*4 to 5 whole fresh basil leaves, plus more for garnish, optional. " +
                                       "*1/2 small head chicory, torn into bite-size pieces (about 4 cups). " +
                                       "*1/2 cup ricotta cheese. " +
                                       "*1/4 cup freshly grated Parmesan. " +
                                       "*8 ounces part-skim mozzarella, cut into 1/2-inch cubes. " +
                                       "~ ~" +
                                       "Step 1: Preheat the oven to 200°C. Bring a large pot of salted water to a boil. Cook the pasta according to the package directions. Drain and reserve 1/2 cup of the pasta water. " +
                                       "~ ~" +
                                       "Step 2: Meanwhile, lay the bacon in a large ovenproof skillet and cook over medium heat until brown and slightly crispy, 4 to 5 minutes per side. Remove the skillet from the heat, transfer the bacon to a paper-towel-lined plate and pour off all but 1 tablespoon of the drippings. Break up the bacon into bite-size pieces. " +
                                       "~ ~...................";

        private readonly Item _chakalaka;
        const string CHAKALAKA = "*3 tbsp oil,1 onion, finely chopped " +
                                 "*2 cloves garlic, crushed " +
                                 "*50g ginger, finely grated " +
                                 "*2 green birds eye chilies, deseeded and chopped " +
                                 "*2 tbsp mild curry powder " +
                                 "*1 green pepper, finely chopped " +
                                 "*1 red pepper, finely chopped " +
                                 "*1 yellow pepper, finely chopped " +
                                 "*5 large carrots, (unpeeled but scrubbed and topped and tailed) grated " +
                                 "*2 tbsp tomato puree " +
                                 "*400g can chopped tomatoes " +
                                 "*2 sprigs fresh thyme, leaves only " +
                                 "*400g can baked beans " +
                                 "*Salt and pepper to taste " +
                                 "~ ~" +
                                 "Step 1: Heat the oil and fry the onion until soft and translucent. " +
                                 "~ ~" +
                                 "Step 2: Add the garlic, chillies and half of the ginger; reserve the other half to add right at the end. " +
                                 "~ ~" +
                                 "Step 3: Add the curry powder and stir to combine. " +
                                 "~ ~" +
                                 "Step 4: Add the peppers and cook for another 2 minutes. " +
                                 "~ ~" +
                                 "Step 5: Add the carrots and stir to make sure they are well combined with the other ingredients and.";

        private readonly Item _info;
        const string INFO = "* If you place an item in a view and need a separator, but dont know how much space is available, you can call the view's \"GetHorizontalSpace\" function. " +
            "~ ~ " +
            "* If you have items that changes realtime within a view, then you could call DrawItems(true) on the view to clear the items text from the view, then change the item, and finally call DrawItems() again, but this time without arguments. Example given later.";

        public Views() {

            RemoveAll();

            //------------------------------
            View recipeOverView = new View(
                new Rectangle(0, 0, 30, Console.WindowHeight-5));
            Item recipes = new Item("My facorite Recipies"){Separator = new Separator(recipeOverView.GetHorizontalSpace() - 4)};
            
            _pasta = new Item("BLT Pasta Skillet");
            _pasta.AddHotKeyListener(ConsoleKey.P, DisplayPasta);
            _chakalaka = new Item("Chakalaka");
            _chakalaka.AddHotKeyListener(ConsoleKey.C, DisplayChakalaka);
            _info = new Item("About Views");
            _info.AddHotKeyListener(ConsoleKey.A, DisplayInfo);

            recipeOverView.Add(recipes);
            recipeOverView.VerticalSpacing = 2;
            recipeOverView.AddAll(_pasta, _chakalaka, _info);

            _pasta.Description = PASTA;
            _chakalaka.Description = CHAKALAKA;
            _info.Description = INFO;
            _pasta.Data = _chakalaka.Data = "Ingredients";
            _info.Data = "Tips";
            Add("recipe_over_view", recipeOverView);

            //------------------------------
            View bottomView = new View(
                new Rectangle(0, Console.WindowHeight-5, 30, 4));
            bottomView.Add(new Item("Back"));

            Add("bottom_view", bottomView);

            //------------------------------
            _displayView = new View(
                new Rectangle(30, 0, Console.WindowWidth-31, Console.WindowHeight-1));

            _displayItem = new Item {Title = ""};

            _displayView.Add(_displayItem);
            Add("recipes_display_view", _displayView);
        }

        public void DisplayPasta() {DipsayItem(_pasta);}
        public void DisplayChakalaka() {DipsayItem(_chakalaka);}
        public void DisplayInfo() {DipsayItem(_info);}

        public void DipsayItem(Item item) {
            if(_displayItem.Title == item.Title)
                return;
            _displayView.DrawItems(true);
            _displayItem.Title = item.Title;
            _displayItem.Separator = new Separator(_displayView.GetHorizontalSpace()-8);
            _displayItem.Data = item.Data;
            _displayItem.Description = item.Description;
            _displayView.DrawItems();
        }

        public override void Dispose() {
            _pasta.RemoveHotKeyListener(DisplayPasta);
            _chakalaka.RemoveHotKeyListener(DisplayChakalaka);
        }
    }
}
