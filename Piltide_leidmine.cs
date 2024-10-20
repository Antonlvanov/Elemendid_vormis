namespace Elemendid_vormis_TARpv23
{
    public partial class Piltide_leidmine : Form
    {
        private TableLayoutPanel mainLayoutPanel; 
        private TableLayoutPanel tableLayoutPanel; 
        private Label timeLabel;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.Timer showIconsTimer; // таймер на сокрытие иконок
        private Label firstClicked = null;
        private Label secondClicked = null;
        private int timeElapsed;
        private List<string> icons = new List<string>()
        {
            "c", "c", "e", "e", "N", "N", "f", "f",
            "l", "l", "u", "u", "x", "x", "v", "v"
        };
        // lives
        private TableLayoutPanel topPanel;
        private Label livesLabel;
        private int lives = 10;

        private System.Windows.Forms.Timer countdownTimer; // таймер для обратного отсчета
        private int countdownValue; // начальное значение для обратного отсчета

        private int currentLevel = 1; // Текущий уровень
        private int gridSize = 4;

        public Piltide_leidmine()
        {
            this.Width = 550;
            this.Height = 550;
            this.Text = "Matching Game";

            mainLayoutPanel = new TableLayoutPanel();
            mainLayoutPanel.Dock = DockStyle.Fill;
            mainLayoutPanel.RowCount = 2;
            mainLayoutPanel.ColumnCount = 1;
            mainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F)); 
            mainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            topPanel = new TableLayoutPanel(); // panel for top labels
            topPanel.ColumnCount = 3; // Теперь 3 колонки: одна для отступа слева, вторая для времени, третья для жизней
            topPanel.RowCount = 1;
            topPanel.Dock = DockStyle.Fill;
            topPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F)); // 50% для отступа слева
            topPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F)); // фиксированная ширина для лейбла времени
            topPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F)); // 50% для жизней

            timeLabel = new Label();
            timeLabel.AutoSize = false;
            timeLabel.BorderStyle = BorderStyle.FixedSingle;
            timeLabel.Width = 200;
            timeLabel.Height = 30;
            timeLabel.Font = new Font(timeLabel.Font.FontFamily, 15.75f);
            timeLabel.Text = "Time: 00:00";
            timeLabel.TextAlign = ContentAlignment.MiddleCenter;
            timeLabel.Dock = DockStyle.Fill; // Это поможет заполнить отведенное место
            timeLabel.Anchor = AnchorStyles.None; // Без привязки, чтобы разместить ровно по центру

            tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;
            tableLayoutPanel.ColumnCount = 4;
            tableLayoutPanel.RowCount = 4;

            for (int i = 0; i < 4; i++)
            {
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
                tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            }
            // add lives label
            livesLabel = new Label();
            livesLabel.AutoSize = false;
            livesLabel.BorderStyle = BorderStyle.FixedSingle;
            livesLabel.Width = 100;
            livesLabel.Height = 30;
            livesLabel.Font = new Font(livesLabel.Font.FontFamily, 15.75f);
            livesLabel.Text = "Lives: 5";
            livesLabel.TextAlign = ContentAlignment.MiddleCenter;
            livesLabel.Dock = DockStyle.Fill;
            livesLabel.Anchor = AnchorStyles.None;

            // timers

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 750;
            timer.Tick += Timer_Tick;

            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 1000;
            gameTimer.Tick += GameTimer_Tick;

            showIconsTimer = new System.Windows.Forms.Timer(); 
            showIconsTimer.Interval = 1000; 
            showIconsTimer.Tick += ShowIconsTimer_Tick;


            // fill main panel layout
            topPanel.Controls.Add(new Label(), 0, 0); // Пустой элемент для отступа слева
            topPanel.Controls.Add(timeLabel, 1, 0); // Лейбл времени по центру
            topPanel.Controls.Add(livesLabel, 2, 0);
            mainLayoutPanel.Controls.Add(topPanel, 0, 0);
            mainLayoutPanel.Controls.Add(tableLayoutPanel, 0, 1); 

            Controls.Add(mainLayoutPanel);

            AddLabelsToTable();
            AssignIconsToSquares();

            ShowAllIcons();
            StartShowIconsTimerWithCountdown();
        }

        private void StartShowIconsTimerWithCountdown()
        {
            countdownValue = 10; // Устанавливаем 5 секунд для запоминания
            timeLabel.ForeColor = Color.Red; // Цвет времени красный для обратного отсчета
            timeLabel.Text = $"Start in: {countdownValue}";

            // Запуск таймера для обратного отсчета
            showIconsTimer.Start();
        }

        private void ShowIconsTimer_Tick(object sender, EventArgs e)
        {
            countdownValue--;
            timeLabel.Text = $"Start in: {countdownValue}";

            if (countdownValue == 0)
            {
                showIconsTimer.Stop(); // Остановить таймер показа иконок
                timeLabel.ForeColor = Color.Black; // Вернуть обычный цвет
                timeLabel.Text = "Time: 00:00";

                // Скрыть все иконки
                HideAllIcons();

                // Запуск основного таймера игры
                StartGameTimer();
            }
        }

        private void HideAllIcons()
        {
            foreach (Control control in tableLayoutPanel.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    iconLabel.ForeColor = iconLabel.BackColor;
                }
            }
        }

        private void ShowAllIcons()
        {
            foreach (Control control in tableLayoutPanel.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    iconLabel.ForeColor = Color.Black;
                }
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();

            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            firstClicked = null;
            secondClicked = null;
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            timeElapsed++;
            TimeSpan time = TimeSpan.FromSeconds(timeElapsed);
            timeLabel.Text = "Time: " + time.ToString(@"mm\:ss");
        }

        private void StartGameTimer()
        {
            timeElapsed = 0;
            gameTimer.Start();
        }

        private void StopGameTimer()
        {
            gameTimer.Stop();
        }

        private void AssignIconsToSquares()
        {
            Random random = new Random();

            foreach (Control control in tableLayoutPanel.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Tag = icons[randomNumber];
                    iconLabel.Text = icons[randomNumber]; 
                    iconLabel.ForeColor = iconLabel.BackColor; 
                    icons.RemoveAt(randomNumber);
                }
            }
        }

        private void AddLabelsToTable()
        {
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    Label label = new Label();
                    label.BackColor = Color.CornflowerBlue;
                    label.AutoSize = false;
                    label.Dock = DockStyle.Fill;
                    label.TextAlign = ContentAlignment.MiddleCenter;
                    label.Font = new Font("Webdings", 48, FontStyle.Bold);
                    label.Text = "c"; 
                    label.ForeColor = label.BackColor;

                    label.Click += Label_Click;

                    tableLayoutPanel.Controls.Add(label, col, row); 
                }
            }
        }

        private void Label_Click(object sender, EventArgs e)
        {
            Label clickedLabel = sender as Label;

            if (firstClicked != null && secondClicked != null)
                return;

            if (clickedLabel == null)
                return;

            if (clickedLabel.ForeColor == Color.Black)
                return;

            clickedLabel.ForeColor = Color.Black;

            if (firstClicked == null)
            {
                firstClicked = clickedLabel;
                return;
            }

            secondClicked = clickedLabel;

            if (firstClicked.Tag.ToString() != secondClicked.Tag.ToString())
            {
                lives--;
                livesLabel.Text = $"Lives: {lives}";

                if (lives == 0)
                {
                    StopGameTimer();
                    MessageBox.Show("You Lost", "Game Over");
                    Close();
                    return;
                }

                timer.Start();
            }

            CheckForWinner();

            if (firstClicked.Tag.ToString() == secondClicked.Tag.ToString())
            {
                firstClicked = null;
                secondClicked = null;
            }
            else
            {
                timer.Start();
            }
        }

        private void CheckForWinner()
        {
            foreach (Control control in tableLayoutPanel.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null && iconLabel.ForeColor == iconLabel.BackColor)
                    return;
            }

            StopGameTimer();

            MessageBox.Show($"You won! Time: {timeLabel.Text}", "Congratulations");
            Close();
        }
    }
}
