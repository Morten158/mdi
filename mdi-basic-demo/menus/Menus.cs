using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using mdi_api.factories;
using mdi_api.menus;
using mdi_api.views;
using mdi_api.views.items;
using mdi_api.views.items.seperators;

namespace mdi_basic_demo.menus {
    class Menus : Menu {

        public Menus() {
            Title = "When a menu is created the title of it will be displayed as an item here.";

            View content = Get(ViewFactory.CONTENT);
            content.Margin.Left = 4;
            content.Margin.Top = 1;

            const string infoMenuText = "A \"Menu\" is composed of three views: header, content and a footer. As you can see above in the \"Header\", the title was automatically created and can be changed at any time. " +
                "This section/view of the menu is known as the \"Content\", the only difference between these views is the size of the area in which they dipsaly items in. The middle view of the menu therfore has the largest area.";
            Item infoMenu = new Item("Standard Views of the Menu", Item.Alignment.LEFT, null, infoMenuText);
            infoMenu.Separator = new Separator(content.GetHorizontalSpace()-8);

            const string infoFunctionsText = "You can easily get any view from the menu by using the \"Get\" function and pass in the identifier of the view you want. If you want to remove a view from the menu, simply call the \"Remove\" " +
                "or \"RemoveAll\" function, here you can pass in a boolean with a value of true if you also want to clear the view's area(draw it blank). If you have built an application that changes the resolution or theme of a menu, call \"ApplyTheme\" and \"ReckonResolution\", " +
                "this will change it in realtime for the menu you are in, but remeber to also change it for the root menu that can be static, because this will never be reinstanciated(see Snake Demo for an example).";
            Item infoFunctions = new Item("Common functions in the Menu Class", Item.Alignment.LEFT, null, infoFunctionsText);
            infoFunctions.Separator = new Separator(content.GetHorizontalSpace()-8);

            const string infoFooterText = "The \"Footer\" is a handy view to place things like a score item or player stats. You could youst resize it to fit your needs. When navigated to by a \"Navigator\" there is automatically placed a Back item there, this will be discussed later.";
            Item infoFooter = new Item("The Footer", Item.Alignment.LEFT, null, infoFooterText);
            infoFooter.Separator = new Separator(content.GetHorizontalSpace()-8);

            content.AddAll(infoMenu, infoFunctions, infoFooter);

        }
    }
}
