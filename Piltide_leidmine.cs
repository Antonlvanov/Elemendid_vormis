namespace Elemendid_vormis_TARpv23
{
    public partial class Piltide_leidmine : Form
    {
        private TableLayoutPanel mainLayoutPanel; 
        private TableLayoutPanel tableLayoutPanel; 
        private Label timeLabel;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Timer gameTimer;
        private Label firstClicked = null;
        private Label secondClicked = null;
        private int timeElapsed;
        private List<string> icons = new List<string>()
        {
            "c", "c", "e", "e", "N", "N", "f", "f",
            "l", "l", "u", "u", "x", "x", "v", "v"
        };

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

            timeLabel = new Label();
            timeLabel.AutoSize = false;
            timeLabel.BorderStyle = BorderStyle.FixedSingle;
            timeLabel.Width = 200;
            timeLabel.Height = 30;
            timeLabel.Font = new Font(timeLabel.Font.FontFamily, 15.75f);
            timeLabel.Text = "Время: 00:00";
            timeLabel.TextAlign = ContentAlignment.MiddleCenter;
            timeLabel.Dock = DockStyle.Fill; 

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

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 750;
            timer.Tick += Timer_Tick;

            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 1000;
            gameTimer.Tick += GameTimer_Tick;

            mainLayoutPanel.Controls.Add(timeLabel, 0, 0); 
            mainLayoutPanel.Controls.Add(tableLayoutPanel, 0, 1); 

            Controls.Add(mainLayoutPanel);

            AddLabelsToTable();

            AssignIconsToSquares();

            StartGameTimer();
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
            timeLabel.Text = "Время: " + time.ToString(@"mm\:ss");
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
            if (firstClicked != null && secondClicked != null)
                return;

            Label clickedLabel = sender as Label;

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

            MessageBox.Show($"Вы победили! Ваше время: {timeLabel.Text}", "Поздравляем");
            Close();
        }
    }
}
