using System.ComponentModel;

namespace Elemendid_vormis_TARpv23.MaluMang
{
    public partial class Piltide_leidmine : Form
    {
        private GameSettings gameSettings;
        private IconManager iconManager;
        private int countdown;
        private int lives;

        public Piltide_leidmine()
        {
            Width = 550;
            Height = 550;
            Text = "Matching Game";

            gameSettings = new GameSettings();
            iconManager = new IconManager(gameSettings);
            GameInitializer gameInitializer = new GameInitializer(gameSettings);
            gameInitializer.InitializeGame();

            Controls.Add(gameSettings.MainLayoutPanel);

            gameSettings.ShowIconsTimer.Tick += new EventHandler(ShowIconsTimer_Tick);
            gameSettings.Timer.Tick += new EventHandler(Timer_Tick);
            gameSettings.GameTimer.Tick += new EventHandler(GameTimer_Tick);

            StartGame();
        }

        private void StartGame()
        {
            lives = gameSettings.Lives;
            gameSettings.LivesLabel.Text = $"Lives: {lives}";
            gameSettings.LevelLabel.Text = $"Level: {gameSettings.Level}";

            countdown = gameSettings.CountdownValue;
            gameSettings.TimeLabel.ForeColor = Color.Red;
            gameSettings.TimeLabel.Text = $"Start in: {countdown}";

            SetupTable();
            iconManager.AssignIconsToSquares();

            ShowAllIcons();
            gameSettings.ShowIconsTimer.Start();
        }

        private void ShowIconsTimer_Tick(object sender, EventArgs e) // таймер показа иконок
        {
            countdown--;
            gameSettings.TimeLabel.Text = $"Start in: {countdown}";

            if (countdown == 0)
            {
                gameSettings.ShowIconsTimer.Stop();
                gameSettings.TimeLabel.ForeColor = Color.Black;
                gameSettings.TimeLabel.Text = "Time: 00:00";

                HideAllIcons();

                StartGameTimer();
            }
        }

        private void IncreaseDifiicultyAndRestart()
        {
            gameSettings.IncreaseLevel();

            StartGame();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            gameSettings.Timer.Stop();

            gameSettings.FirstClicked.ForeColor = gameSettings.FirstClicked.BackColor;
            gameSettings.SecondClicked.ForeColor = gameSettings.SecondClicked.BackColor;

            gameSettings.FirstClicked = null;
            gameSettings.SecondClicked = null;
        }

        private void SetupTable()
        {
            gameSettings.TableLayoutPanel.Controls.Clear();
            gameSettings.TableLayoutPanel.ColumnStyles.Clear();
            gameSettings.TableLayoutPanel.RowStyles.Clear();

            gameSettings.TableLayoutPanel.ColumnCount = gameSettings.GridSize;
            gameSettings.TableLayoutPanel.RowCount = gameSettings.GridSize;

            for (int i = 0; i < gameSettings.TableLayoutPanel.ColumnCount; i++)
            {
                gameSettings.TableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / gameSettings.GridSize));
                gameSettings.TableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / gameSettings.GridSize));
            }

            // label storage
            Control[] labels = new Control[gameSettings.GridSize * gameSettings.GridSize];
            int index = 0;

            for (int row = 0; row < gameSettings.GridSize; row++)
            {
                for (int col = 0; col < gameSettings.GridSize; col++)
                {
                    Label label = gameSettings.CreateLabel();
                    label.Click += Label_Click;

                    gameSettings.TableLayoutPanel.SetColumn(label, col);
                    gameSettings.TableLayoutPanel.SetRow(label, row);
                    labels[index++] = label;
                }
            }

            int totalLabels = gameSettings.GridSize * gameSettings.GridSize;
            if (totalLabels % 2 != 0 && labels.Length > 0)
            {
                // remove last label
                Array.Resize(ref labels, labels.Length - 1);
            }

            // add to table controls
            gameSettings.TableLayoutPanel.Controls.AddRange(labels);
        }

        private void CheckForWinner()
        {
            foreach (Control control in gameSettings.TableLayoutPanel.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null && iconLabel.ForeColor == iconLabel.BackColor)
                    return;
            }

            StopGameTimer();

            MessageBox.Show($"You won! Time: {gameSettings.TimeLabel.Text}", "Congratulations");

            var vastus = MessageBox.Show("Continue", "Continue to next level?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (vastus == DialogResult.Yes)
            {
                IncreaseDifiicultyAndRestart();
            }
            else { Close(); }
        }

        private void Label_Click(object sender, EventArgs e)
        {
            Label clickedLabel = sender as Label;

            if (gameSettings.FirstClicked != null && gameSettings.SecondClicked != null)
                return;

            if (clickedLabel == null)
                return;

            if (clickedLabel.ForeColor == Color.Black)
                return;

            clickedLabel.ForeColor = Color.Black;

            if (gameSettings.FirstClicked == null)
            {
                gameSettings.FirstClicked = clickedLabel;
                return;
            }

            gameSettings.SecondClicked = clickedLabel;

            if (gameSettings.FirstClicked.Tag.ToString() != gameSettings.SecondClicked.Tag.ToString())
            {
                lives--;
                gameSettings.LivesLabel.Text = $"Lives: {lives}";

                if (lives == 0)
                {
                    StopGameTimer();
                    MessageBox.Show("You Lost", "Game Over");
                    Close();
                    return;
                }

                gameSettings.Timer.Start();
            }

            CheckForWinner();

            if (gameSettings.FirstClicked.Tag.ToString() == gameSettings.SecondClicked.Tag.ToString())
            {
                gameSettings.FirstClicked = null;
                gameSettings.SecondClicked = null;
            }
            else
            {
                gameSettings.Timer.Start();
            }
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            gameSettings.TimeElapsed++;
            TimeSpan time = TimeSpan.FromSeconds(gameSettings.TimeElapsed);
            gameSettings.TimeLabel.Text = "Time: " + time.ToString(@"mm\:ss");
        }

        private void StartGameTimer()
        {
            gameSettings.TimeElapsed = 0;
            gameSettings.GameTimer.Start();
        }

        private void StopGameTimer()
        {
            gameSettings.GameTimer.Stop();
        }
        private void HideAllIcons()
        {
            foreach (Control control in gameSettings.TableLayoutPanel.Controls)
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
            foreach (Control control in gameSettings.TableLayoutPanel.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    iconLabel.ForeColor = Color.Black;
                }
            }
        }

    }
}