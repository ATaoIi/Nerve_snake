using System;
using System.Collections.Generic;
using System.Drawing;

namespace SnakeGame
{
public class Snake
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    private List<Point> _body; // 蛇的身体
    private Direction _direction; // 蛇的方向

    public Snake()
    {
        // 初始化蛇的位置和方向
        _body = new List<Point>();
        _body.Add(new Point(5, 5));
        _body.Add(new Point(4, 5));
        _body.Add(new Point(3, 5));
        _direction = Direction.Right;
    }

    public bool Move()
    {
        // 移动蛇的身体
        for (int i = _body.Count - 1; i > 0; i--)
        {
            _body[i] = _body[i - 1];
        }

        // 移动蛇的头部
        Point head = _body[0];
        switch (_direction)
        {
            case Direction.Up:
                head.Y--;
                break;
            case Direction.Down:
                head.Y++;
                break;
            case Direction.Left:
                head.X--;
                break;
            case Direction.Right:
                head.X++;
                break;
        }

        // 当蛇的头部到达墙壁时，将其重新定位到另一侧
        if (head.X < 0)
        {
            head.X = MainForm.GAME_WIDTH / MainForm.TILE_SIZE - 1;
        }
        else if (head.X >= MainForm.GAME_WIDTH / MainForm.TILE_SIZE)
        {
            head.X = 0;
        }
        if (head.Y < 0)
        {
            head.Y = MainForm.GAME_HEIGHT / MainForm.TILE_SIZE - 1;
        }
        else if (head.Y >= MainForm.GAME_HEIGHT / MainForm.TILE_SIZE)
        {
            head.Y = 0;
        }

        _body[0] = head;

        return true;
    }


    public void Eat()
    {
        // 增加蛇的长度
        _body.Add(_body[_body.Count - 1]);
    }

    public bool CollidesWith(Point point)
    {
        // 检查蛇是否与一个点碰撞
        foreach (Point bodyPoint in _body)
        {
            if (bodyPoint.Equals(point))
            {
                return true;
            }
        }
        return false;
    }

    public bool CollidesWithSelf()
    {
        // 检查蛇是否与自己碰撞
        Point head = _body[0];
        for (int i = 1; i < _body.Count; i++)
        {
            if (head.Equals(_body[i]))
            {
                return true;
            }
        }
        return false;
    }

    public bool CollidesWithWall(Size gameSize)
    {
        // 检查蛇是否与墙壁碰撞
        Point head = _body[0];
        return head.X < 0 || head.X >= gameSize.Width / MainForm.TILE_SIZE
            || head.Y < 0 || head.Y >= gameSize.Height / MainForm.TILE_SIZE;
    }

    public void SetDirection(Direction direction)
    {
        // 避免蛇掉头
        switch (_direction)
        {
            case Direction.Up:
                if (direction != Direction.Down)
                {
                    _direction = direction;
                }
                break;
            case Direction.Down:
                if (direction != Direction.Up)
                {
                    _direction = direction;
                }
                break;
            case Direction.Left:
                if (direction != Direction.Right)
                {
                    _direction = direction;
                }
                break;
            case Direction.Right:
                if (direction != Direction.Left)
                {
                    _direction = direction;
                }
                break;
        }
    }



    public void Draw(Graphics graphics, int tileSize)
    {
        Brush brush = new SolidBrush(Color.Black);
        // 绘制蛇Brush brush = new SolidBrush(Color.Green);
        foreach (Point point in _body)
        {
            graphics.FillRectangle(brush, point.X * tileSize, point.Y * tileSize, tileSize, tileSize);
        }
    }

    public void Reset()
    {
        // 重置蛇的位置和方向
        _body.Clear();
        _body.Add(new Point(5, 5));
        _body.Add(new Point(4, 5));
        _body.Add(new Point(3, 5));
        _direction = Direction.Right;
    }
}

}