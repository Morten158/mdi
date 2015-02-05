using System;
using mdi_api.menus;
using mdi_api.views;
using mdi_api.views.geometri;
using mdi_api.views.graphics.themes;
using mdi_api.views.items;
using mdi_api.views.items.seperators;

namespace mdi_snake.menus {
    class AboutSnake : Menu {

        public AboutSnake()
            : base("", Application.Theme) {

            RemoveAll();

            const string headedIntroduction = "Snake originated during the late 1970s in arcades. The name applies to the general game design; the original was not named Snake,and there is no definitive version of the game. Its simplicity has led to many implementations of the Snake concept. After it became the standard pre-loaded game on Nokia mobile phones in 1998, there was a resurgence of interest in the game as it found a larger audience.";

            View headerSection = new View(
                new Rectangle(Console.WindowWidth-28, 2, 21, 6),
                new Theme("", "",
                    Application.Theme.Background,
                    Application.Theme.Background,
                    Application.Theme.Foreground,
                    Application.Theme.HotKey), new Margin(0, 0, 2, 0));

            headerSection.Add(new Item("OOOOOOOO@", Item.Alignment.CENTER));

            View header = new View(
                new Rectangle(0, 0, Console.WindowWidth-26, 10), Application.Theme, new Margin(6, 1, 1, 1));
            header.Add(new Item("Origin of Snake", Item.Alignment.LEFT, "", headedIntroduction) {
                Separator = new Separator(header.GetHorizontalSpace()-6)
            });

            headerSection.Draw();
            Console.SetCursorPosition(Console.WindowWidth-22, 5);
            Console.Write("O");
            Console.SetCursorPosition(Console.WindowWidth-22, 6);
            Console.Write("O");
            Console.SetCursorPosition(Console.WindowWidth-28, 6);
            Console.WriteLine("OOOOOO");
            Console.ForegroundColor = Application.Theme.HotKey;
            Console.SetCursorPosition(Console.WindowWidth-8, 5);
            Console.Write("$");
            Console.SetCursorPosition(Console.WindowWidth-14, 2);
            Console.Write("$");
            Console.SetCursorPosition(Console.WindowWidth-21, 8);
            Console.Write("$");

            View sideView = new View(
                new Rectangle(0, 11, 45, Console.WindowHeight-10-5), Application.Theme, new Margin(1, 1, 1, 1));
            sideView.VerticalSpacing = 1;
            sideView.Add(new Item("Nokia phones", Item.Alignment.LEFT, null, "Nokia is well known for putting Snake on the majority of their phones. Versions include:") {
                Separator = new Separator(sideView.GetHorizontalSpace()-2)
            });
            sideView.Add(new Item("Snake - The original", Item.Alignment.LEFT, null, "Graphics consisted of black squares, and it had 4 directions. It was programmed in 1997 by Taneli Armanto, a design engineer in Nokia and introduced on the Nokia 6110"));
            sideView.Add(new Item("Snake II", Item.Alignment.LEFT, null, "Snake improved to a snake pattern, introduction of bonus bugs, a 'Circumnavigate play area' and mazes (obstacle walls placed within the play area)."));

            View middleContent = new View(
                new Rectangle(45, 11, Console.WindowWidth-46, Console.WindowHeight-10-5), Application.Theme, new Margin(1, 1, 1, 1));
            middleContent.Add(new Item("Notable versions", Item.Alignment.CENTER) {
                Separator = new Separator(middleContent.GetHorizontalSpace()-2)
            });
            middleContent.Add(new Item("Light Cycle - Tron"));
            middleContent.Add(new Item("Nibbler - arcade version"));
            middleContent.Add(new Item("SNAFU - Mattel Intellivision Variant"));
            middleContent.Add(new Item("Nibbles - MS-DOS (QBasic) some versions of suse linux"));
            middleContent.Add(new Item("Nimble Quest - iOS, Android"));
            middleContent.Add(new Item("Pizza Worm - MS-DOS"));
            middleContent.Add(new Item("Rattler Race - Microsoft Windows"));
            middleContent.Add(new Item("Snake Byte - Apple II"));
            middleContent.Add(new Item("Knot in 3D - Early 3D version for ZX Spectrum"));
            middleContent.Add(new Item("Snakeball - PlayStation 3"));

            View footer = new View(
                new Rectangle(0, Console.WindowHeight-3, Console.WindowWidth-1, 2), Application.Theme, new Margin(2, 0, 1, 0));

            Add("header", header);
            Add("header_section", headerSection);
            Add("side_view", sideView);
            Add("middle_content", middleContent);
            Add("footer", footer);
        }
    }
}
