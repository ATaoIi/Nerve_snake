using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO.Ports;
using System.Text;


namespace SnakeGame
{
    
    public partial class MainForm : Form
    {   
        public const int TOP_OFFSET = 50;
        private System.Windows.Forms.Label attentionLabel;
        private System.Windows.Forms.Label speedLabel;
        public const int TILE_SIZE = 20; // 每个格子的大小-Размер каждой сетки
        public const int GAME_WIDTH = 600; // 游戏界面的宽度-Ширина игрового интерфейса
        public const int GAME_HEIGHT = 400; // 游戏界面的高度-Высота игрового интерфейса
        public const int GAME_TICK = 100; // 游戏刷新间隔（毫秒）-Интервал обновления игры (мс)
        public const int GAME_SCORE_INCREMENT = 10; // 每次吃到食物增加的分数-Счет увеличивается каждый раз, когда еда съедена
        private Neuroscanner neuroscanner;
        private SerialPort serialPort;
        
        private Timer gameTimer; // 游戏定时器
        public static Snake Snake { get; private set; } // 蛇

        private Food food; // 食物
        private int score; // 得分

        public MainForm()
        {
            InitializeComponent();
            // Initialize Neuroscanner and SerialPort
            neuroscanner = new Neuroscanner();
            serialPort = new SerialPort("COM4", 57600, Parity.None, 8, StopBits.One); // Replace "COM3" with the actual port used by your device

            // Add SerialPort.DataReceived event handler
            serialPort.DataReceived += SerialPort_DataReceived;
            
            // 初始化游戏定时器-Инициализация игрового таймера
            gameTimer = new Timer();
            gameTimer.Interval = GAME_TICK;
            gameTimer.Tick += GameTimer_Tick;

            // 初始化蛇和食物
            Snake = new Snake();
            food = new Food();

            // 初始化游戏界面
            this.ClientSize = new Size(GAME_WIDTH, GAME_HEIGHT);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Paint += MainForm_Paint;

            // 开始游戏
            RestartGame();
            // Open the serial port
            if (!serialPort.IsOpen)
            {
                serialPort.Open();
            }
        }
        //Прием данных устройства
        private StringBuilder receivedData = new StringBuilder();
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                neuroscanner.Scan(serialPort);
                UpdateLabels();
            });
        }
        
        
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            byte attention = neuroscanner.GetAttention();
            int interval;

            if (attention == 0)
            {
                interval = 10000;
            }
            else if (attention < 40)
            {
                interval = 500; // Slower speed
            }
            else if (attention < 50)
            {
                interval = 400; // Normal speed
            }
            else if (attention < 60)
            {
                interval = 300; // Normal speed
            }
            else if (attention < 70)
            {
                interval = 200; // Normal speed
            }
            else if (attention < 80)
            {
                interval = 100; // Normal speed
            }
            else
            {
                interval = 50; // Faster speed
            }
            UpdateLabels();
            gameTimer.Interval = interval;
            // 移动蛇并检查碰撞
            if (Snake.Move())
            {
                if (Snake.CollidesWith(food.Position))
                {
                    // 蛇吃到了食物
                    Snake.Eat();
                    food.Respawn();
                    score += GAME_SCORE_INCREMENT;
                    UpdateScoreLabel();
                }
                else if (Snake.CollidesWithSelf())
                {
                    // 蛇碰到了自己
                    gameTimer.Stop();
                    MessageBox.Show($"игра закончена! ваш счет{score}。", "игра закончена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RestartGame();
                }
                else
                {
                    // 蛇什么都没碰到，继续移动
                    Invalidate();
                }
            }
        }

        
        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            // 绘制游戏界面
            Snake.Draw(e.Graphics, TILE_SIZE);
            food.Draw(e.Graphics, TILE_SIZE);
        }

        private void UpdateScoreLabel()
        {
            // 更新得分标签
            scoreLabel.Text = $"得分：{score}";
        }

        private void RestartGame()
        {
            
            // 重新开始游戏
            Snake.Reset();
            food.Respawn();
            score = 0;
            UpdateScoreLabel();
            gameTimer.Start();
            if (!serialPort.IsOpen)
            {
                serialPort.Open();
            }
        }
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            // 处理用户输入
            switch (e.KeyCode)
            {
                case Keys.Up:
                    Snake.SetDirection(Snake.Direction.Up);
                    break;
                case Keys.Down:
                    Snake.SetDirection(Snake.Direction.Down);
                    break;
                case Keys.Left:
                    Snake.SetDirection(Snake.Direction.Left);
                    break;
                case Keys.Right:
                    Snake.SetDirection(Snake.Direction.Right);
                    break;
            }
            

    }
        private void UpdateLabels()
        {
            if (neuroscanner != null)
            {
                attentionLabel.Text = $"Attention: {neuroscanner.GetAttention()}";
            }

            if (gameTimer != null)
            {
                speedLabel.Text = $"Speed: {gameTimer.Interval} ms";
            }
        }

    
    }
}
