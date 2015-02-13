using System;
using mdi_api;
using mdi_api.factories;
using mdi_api.menus;
using mdi_api.prompts;
using mdi_api.views;
using mdi_api.views.geometri;
using mdi_api.views.items;
using mdi_snake.menus.snake;

namespace mdi_snake.menus {
    class Snake : Menu {

        private readonly Map _map;

        public Snake()
            : base("Snake", Application.Theme) {

            int ticks = new Prompt("Game Speed (50-200)").GetInt(new Range(50, 200));
            EventDispatchThread.SetTick(ticks);

            Remove(ViewFactory.HEADER, true);
            Remove(ViewFactory.CONTENT, true);

            View footer = Get(ViewFactory.FOOTER);
            footer.Draw();
            
            Add("score_view", new View(
                new Rectangle(footer.Rectangle.X + 20, footer.Rectangle.Y, footer.Rectangle.Width-20, footer.Rectangle.Height), Application.Theme));
            Get("score_view").Add(new Item("Player Score: ", Item.Alignment.CENTER, 0));

            Rectangle bounds = new Rectangle(0, 0, Console.WindowWidth-2, Console.WindowHeight-2 - footer.Rectangle.Height);
            _map = new Map(bounds);

            EventDispatchThread.AddTickListener(OnTick);
        }

        public void OnTick() {
            _map.Update();
            Get("score_view").Get("Player Score: ").Data = _map.GetPlayerScore();
            Get("score_view").DrawItems();

            if(_map.GameOver) {
                new Prompt("Great Game, what is your name?", Application.Theme).GetString(10);
                Dispose();
                Get(ViewFactory.FOOTER).Get("Back").ForceKeyPressed();
            }
        }

        public override void Dispose() {
            _map.Dispose();
            EventDispatchThread.RemoveTickListener(OnTick);
        }
    }
}
