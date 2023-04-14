using System.Drawing;
using System;

namespace SnakeGame
{
    public class Food
    {
        private Point _position; // 食物的位置

        public Food()
        {
            // 初始化食物的位置
            _position = new Point(0, 0);
            Respawn();
        }

        public Point Position
        {
            get { return _position; }
        }
        public Point GetPosition()
        {
            return _position;
        }
        public void Respawn()
        {
            // 在随机位置生成食物
            Random random = new Random();
            do
            {
                _position.X = random.Next(0, MainForm.GAME_WIDTH / MainForm.TILE_SIZE);
                _position.Y = random.Next(MainForm.TOP_OFFSET / MainForm.TILE_SIZE, (MainForm.GAME_HEIGHT + MainForm.TOP_OFFSET) / MainForm.TILE_SIZE);
            } while (MainForm.Snake.CollidesWith(_position));
        }

        public void Draw(Graphics graphics, int tileSize)
        {
            // 绘制食物
            Brush brush = new SolidBrush(Color.Red);
            graphics.FillEllipse(brush, _position.X * tileSize, _position.Y * tileSize, tileSize, tileSize);
        }
    }
}