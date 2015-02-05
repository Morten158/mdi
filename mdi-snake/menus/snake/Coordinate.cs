namespace mdi_snake.menus.snake {
    class Coordinate {

        public int x, y;

        public Coordinate(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public static bool operator ==(Coordinate c1, Coordinate c2) {
            return (c1.x == c2.x && c1.y == c2.y);
        }

        public static bool operator !=(Coordinate c1, Coordinate c2) {
            return !(c1 == c2);
        }
    }
}
