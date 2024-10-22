using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elemendid_vormis_TARpv23.MaluMang
{
    public class GameInitializer
    {
        private GameSettings gameSettings;
        public EventHandler Timer_TickHandler { get; set; }
        public EventHandler GameTimer_TickHandler { get; set; }
        public EventHandler ShowIconsTimer_TickHandler { get; set; }

        public GameInitializer(GameSettings gameSettings)
        {
            this.gameSettings = gameSettings;

            InitializeGame();
        }

        public void InitializeGame()
        {
            gameSettings.MainLayoutPanel = new TableLayoutPanel();
            gameSettings.MainLayoutPanel.Dock = DockStyle.Fill;
            gameSettings.MainLayoutPanel.RowCount = 2;
            gameSettings.MainLayoutPanel.ColumnCount = 1;
            gameSettings.MainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            gameSettings.MainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            gameSettings.TopPanel = new TableLayoutPanel(); // panel for top labels
            gameSettings.TopPanel.ColumnCount = 3; // одна для уроня, вторая для времени, третья для жизней
            gameSettings.TopPanel.RowCount = 1;
            gameSettings.TopPanel.Dock = DockStyle.Fill;
            gameSettings.TopPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F)); // 50% отступа 
            gameSettings.TopPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F)); // фиксированная ширина для лейбла времени
            gameSettings.TopPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            // лейбл таймера
            gameSettings.TimeLabel = new Label();
            gameSettings.TimeLabel.AutoSize = false;
            gameSettings.TimeLabel.BorderStyle = BorderStyle.FixedSingle;
            gameSettings.TimeLabel.Width = 200;
            gameSettings.TimeLabel.Height = 30;
            gameSettings.TimeLabel.Font = new Font(gameSettings.TimeLabel.Font.FontFamily, 15.75f);
            gameSettings.TimeLabel.Text = "Time: 00:00";
            gameSettings.TimeLabel.TextAlign = ContentAlignment.MiddleCenter;
            gameSettings.TimeLabel.Dock = DockStyle.Fill;
            gameSettings.TimeLabel.Anchor = AnchorStyles.None; // разместить по центру

            // лейбл уровня
            gameSettings.LevelLabel = new Label();
            gameSettings.LevelLabel.AutoSize = false;
            gameSettings.LevelLabel.BorderStyle = BorderStyle.FixedSingle;
            gameSettings.LevelLabel.Width = 100;
            gameSettings.LevelLabel.Height = 30;
            gameSettings.LevelLabel.Font = new Font(gameSettings.LevelLabel.Font.FontFamily, 15.75f);
            gameSettings.LevelLabel.Text = "Level 1";
            gameSettings.LevelLabel.TextAlign = ContentAlignment.MiddleCenter;
            gameSettings.LevelLabel.Dock = DockStyle.Fill;
            gameSettings.LevelLabel.Anchor = AnchorStyles.None; // разместить по центру

            // панель для иконок
            gameSettings.TableLayoutPanel = new TableLayoutPanel();
            gameSettings.TableLayoutPanel.Dock = DockStyle.Fill;
            gameSettings.TableLayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;

            // add lives label
            gameSettings.LivesLabel = new Label();
            gameSettings.LivesLabel.AutoSize = false;
            gameSettings.LivesLabel.BorderStyle = BorderStyle.FixedSingle;
            gameSettings.LivesLabel.Width = 100;
            gameSettings.LivesLabel.Height = 30;
            gameSettings.LivesLabel.Font = new Font(gameSettings.LivesLabel.Font.FontFamily, 15.75f);
            gameSettings.LivesLabel.Text = "Lives: 5";
            gameSettings.LivesLabel.TextAlign = ContentAlignment.MiddleCenter;
            gameSettings.LivesLabel.Dock = DockStyle.Fill;
            gameSettings.LivesLabel.Anchor = AnchorStyles.None;

            // timers

            gameSettings.Timer = new System.Windows.Forms.Timer();
            gameSettings.Timer.Interval = 750;

            gameSettings.GameTimer = new System.Windows.Forms.Timer();
            gameSettings.GameTimer.Interval = 1000;

            gameSettings.ShowIconsTimer = new System.Windows.Forms.Timer();
            gameSettings.ShowIconsTimer.Interval = 1000;


            // fill main layout panel 
            gameSettings.TopPanel.Controls.Add(gameSettings.LevelLabel, 0, 0);
            gameSettings.TopPanel.Controls.Add(gameSettings.TimeLabel, 1, 0);
            gameSettings.TopPanel.Controls.Add(gameSettings.LivesLabel, 2, 0);
            gameSettings.MainLayoutPanel.Controls.Add(gameSettings.TopPanel, 0, 0);
            gameSettings.MainLayoutPanel.Controls.Add(gameSettings.TableLayoutPanel, 0, 1);


        }
    }
}