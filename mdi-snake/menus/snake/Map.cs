using System;
using System.Collections.Generic;
using mdi_api.utils;
using mdi_api.views.geometri;

namespace mdi_snake.menus.snake {
    class Map {

        public const char APPLE = '$';
        public const char BODY = 'O';
        public const char HEAD = '@';
        public const char EMPTY = '\0';

        private readonly Rectangle _bounds;
        private readonly List<Coordinate> _apples; 
        private readonly Node[,] _nodes;

        private readonly Random _random;

        private readonly Player _player;
        public bool GameOver { get; set; }

        public Map(Rectangle bounds) {
            _bounds = bounds;
            _apples = new List<Coordinate>(1);
            _nodes = new Node[_bounds.Width+1, _bounds.Height+1];
            InitializeNodes();
            _random = new Random();
            _player = new Player();
            _player.Spawn(new Coordinate(_bounds.Width/2, _bounds.Height/2));
            SpawnApple();
        }

        public void InitializeNodes() {
            for(int x = 0; x < _bounds.Width+1; x++) {
                for(int y = 0; y < _bounds.Height+1; y++) {
                    _nodes[x, y] = new Node(x, y, EMPTY);
                }
            }
        }

        public void SpawnApple() {
            int x = _random.Next(1, _bounds.Width-2);
            int y = _random.Next(1, _bounds.Height-2);
            _nodes[x, y].Ch = APPLE;
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = Application.Theme.HotKey;
            Console.Write(APPLE);
            _apples.Add(new Coordinate(x, y));
        }

        public void ClearApple(Coordinate coordinate) {
            _player.Score += 15;
            _apples.Remove(coordinate);
            SpawnApple();
        }

        public Node Get(Coordinate coordinate) {
            return _nodes[coordinate.x, coordinate.y];
        }

        public void Update() {
            _player.Move(this);
            if(_apples.Count < 1)
                SpawnApple();
            GameOver = _player.Dead;
        }

        public void Clear(Coordinate coordinate) {
            _nodes[coordinate.x, coordinate.y].Ch = EMPTY;
            Console.SetCursorPosition(coordinate.x, coordinate.y);
            Console.Write(EMPTY);
        }

        public void Write(Coordinate coordinate, char ch) {
            _nodes[coordinate.x, coordinate.y].Ch = ch;
            Console.SetCursorPosition(coordinate.x, coordinate.y);
            Console.ForegroundColor = Application.Theme.Foreground;
            Console.Write(ch);
        }

        public bool IsOutOfBounds(Coordinate coordinate) {
            return Boundries.OutOfBounds(coordinate.x, coordinate.y, _bounds);
        }

        public int GetPlayerScore() {
            return _player.Score;
        }

        public void Dispose() {
            _player.Dispose();
        }

        public bool Collided(Coordinate coordinate) {
            return _nodes[coordinate.x, coordinate.y].Ch != EMPTY && _nodes[coordinate.x, coordinate.y].Ch != APPLE;
        }
    }
}
