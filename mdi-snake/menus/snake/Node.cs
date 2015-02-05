using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mdi_snake.menus.snake {
    class Node : Coordinate {

        public char Ch { get; set; }

        public Node(int x, int y, char ch) : base(x, y) {
            Ch = ch;
        }
    }
}
