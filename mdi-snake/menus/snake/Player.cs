using System;
using System.Collections.Generic;
using System.Linq;
using mdi_api;

namespace mdi_snake.menus.snake {
    class Player {

        private int _velocityH = 1, _velocityV;
        private readonly List<Coordinate> _coordinates;

        public int Score { get; set; }
        public bool Dead { get; set; }

        public Player() {
            _coordinates = new List<Coordinate>(16);
            Score = 0;
            Dead = false;
            EventDispatchThread.AddKeyListener(Directions);
        }

        public void Spawn(Coordinate coord) {
            for(int i = 0; i < 4; i++) {
                _coordinates.Add(new Coordinate(coord.x, coord.y));
            }
        }

        public void Directions(ConsoleKey key) {
            _velocityH = _velocityV = 0;
            switch(key) {
                case (ConsoleKey.LeftArrow):
                    _velocityH = -1;
                    break;
                case (ConsoleKey.RightArrow):
                    _velocityH = 1;
                    break;
                case (ConsoleKey.UpArrow):
                    _velocityV = -1;
                    break;
                case (ConsoleKey.DownArrow):
                    _velocityV = 1;
                    break;
                default:_velocityV = 1;
                    break;
            }
        }

        public virtual void Move(Map map) {

            Coordinate head = _coordinates.Last();
            Coordinate node = new Coordinate(head.x + _velocityH, head.y + _velocityV);

            if(node == _coordinates[_coordinates.Count-2])
                node = new Coordinate(head.x + -_velocityH, head.y + -_velocityV);

            Dead = map.IsOutOfBounds(node) || map.Collided(node);
            if(Dead)
                return;
               
            bool apple = map.Get(node).Ch == Map.APPLE;

            Coordinate tail = _coordinates.First();
            map.Clear(tail);

            map.Write(head, Map.BODY);
            map.Write(node, Map.HEAD);

            if(apple) {
                map.ClearApple(node);
            } else {
                _coordinates.RemoveAt(0);
            }
            _coordinates.Add(node);
        }

        public void Dispose() {
            EventDispatchThread.RemoveKeyListener(Directions);
        }
    }
}
