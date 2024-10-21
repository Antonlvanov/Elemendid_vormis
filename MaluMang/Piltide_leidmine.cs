using System.ComponentModel;

namespace Elemendid_vormis_TARpv23.MaluMang
{
    public partial class Piltide_leidmine : Form
    {
        private GameSettings gameSettings = new GameSettings();
        private IconManager iconManager;

        public Piltide_leidmine()
        {
            Width = 550;
            Height = 550;
            Text = "Matching Game";

            gameSettings = new GameSettings();
            iconManager = new IconManager(gameSettings); // Инициализация менеджера иконок
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
            gameSettings.TimeLabel.ForeColor = Color.Red;
            gameSettings.TimeLabel.Text = $"Start in: {gameSettings.CountdownValue}";

            SetupTable();
            iconManager.AssignIconsToSquares(); 

            ShowAllIcons();

            gameSettings.ShowIconsTimer.Start();
        }

        

        private void ShowIconsTimer_Tick(object sender, EventArgs e) // таймер показа иконок
        {
            gameSettings.CountdownValue--;
            gameSettings.TimeLabel.Text = $"Start in: {gameSettings.CountdownValue}";

            if (gameSettings.CountdownValue == 0)
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
            gameSettings.GridSize += 1;
            gameSettings.DoubleCountdownValue();  
            gameSettings.IncreaseLives(); 

            StartGame();
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

        private void Timer_Tick(object sender, EventArgs e)
        {
            gameSettings.Timer.Stop();

            gameSettings.FirstClicked.ForeColor = gameSettings.FirstClicked.BackColor;
            gameSettings.SecondClicked.ForeColor = gameSettings.SecondClicked.BackColor;

            gameSettings.FirstClicked = null;
            gameSettings.SecondClicked = null;
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

        private void SetupTable()
        {
            gameSettings.TableLayoutPanel.Controls.Clear();
            gameSettings.TableLayoutPanel.ColumnStyles.Clear();
            gameSettings.TableLayoutPanel.RowStyles.Clear();

            gameSettings.TableLayoutPanel.ColumnCount = gameSettings.GridSize;
            gameSettings.TableLayoutPanel.RowCount = gameSettings.GridSize;

            for (int i = 0; i < gameSettings.TableLayoutPanel.ColumnCount; i++)
            {
                gameSettings.TableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
                gameSettings.TableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            }

            for (int row = 0; row < gameSettings.GridSize; row++)
            {
                for (int col = 0; col < gameSettings.GridSize; col++)
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

                    gameSettings.TableLayoutPanel.Controls.Add(label, col, row);
                }
            }
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
                gameSettings.Lives--;
                gameSettings.LivesLabel.Text = $"Lives: {gameSettings.Lives}";

                if (gameSettings.Lives == 0)
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
        private void PiltideLeidmine_FormClosing(object sender, FormClosingEventArgs e)
        {
            gameSettings.ShowIconsTimer.Stop();
            gameSettings.Timer.Stop();
            gameSettings.GameTimer.Stop();
            this.Close(); // вместо Application.Exit()
        }
    }
}