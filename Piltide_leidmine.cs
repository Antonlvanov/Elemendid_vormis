namespace Elemendid_vormis_TARpv23
{
    public partial class Piltide_leidmine : Form
    {
        // Объявляем необходимые объекты
        private TableLayoutPanel tableLayoutPanel;
        private Label timeLabel;
        private System.Windows.Forms.Timer timer;

        // Переменные для отслеживания кликов
        private Label firstClicked = null;
        private Label secondClicked = null;

        // Список иконок для каждой метки
        List<string> icons = new List<string>()
        {
            "c", "c", "e", "e", "N", "N", "f", "f",
            "l", "l", "u", "u", "x", "x", "v", "v"
        };

        public Piltide_leidmine()
        {
            // Настройка параметров формы
            this.Width = 550;
            this.Height = 500;
            this.Text = "Matching Game";

            // Создание TableLayoutPanel
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

            // Настройка таймера
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;

            // Настройка метки времени
            timeLabel = new Label();
            timeLabel.AutoSize = false;
            timeLabel.BorderStyle = BorderStyle.FixedSingle;
            timeLabel.Width = 200;
            timeLabel.Height = 30;
            timeLabel.Font = new Font(timeLabel.Font.FontFamily, 15.75f);
            timeLabel.Text = "Время: 00:00";
            timeLabel.Location = new Point(300, 0);

            // Добавление компонентов на форму
            Controls.Add(tableLayoutPanel);
            Controls.Add(timeLabel);

            // Перемешиваем иконки
            AssignIconsToSquares();
        }

        // Метод для добавления меток в таблицу
        private void AddLabelsToTable()
        {
            for (int i = 0; i < tableLayoutPanel.RowCount; i++)
            {
                for (int j = 0; j < tableLayoutPanel.ColumnCount; j++)
                {
                    Label label = new Label();
                    label.BackColor = Color.CornflowerBlue;
                    label.AutoSize = false;
                    label.Dock = DockStyle.Fill;
                    label.TextAlign = ContentAlignment.MiddleCenter;
                    label.Font = new Font("Webdings", 48, FontStyle.Bold);
                    label.Text = "";  // Иконка изначально скрыта

                    // Привязываем обработчик события клика
                    label.Click += Label_Click;

                    tableLayoutPanel.Controls.Add(label, j, i);
                }
            }
        }

        // Метод для назначения иконок
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
                    icons.RemoveAt(randomNumber);
                }
            }
        }

        // Обработчик кликов по меткам
        private void Label_Click(object sender, EventArgs e)
        {
            if (firstClicked != null && secondClicked != null)
                return;

            Label clickedLabel = sender as Label;

            if (clickedLabel == null)
                return;

            if (clickedLabel.ForeColor == Color.Black)
                return;

            // Отображаем иконку
            clickedLabel.ForeColor = Color.Black;

            if (firstClicked == null)
            {
                firstClicked = clickedLabel;
                return;
            }

            secondClicked = clickedLabel;

            // Проверка совпадений
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

        // Метод для проверки победителя
        private void CheckForWinner()
        {
            foreach (Control control in tableLayoutPanel.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null && iconLabel.ForeColor == iconLabel.BackColor)
                    return;
            }

            MessageBox.Show("Вы победили!", "Поздравляем");
            Close();
        }

        // Метод, который скрывает иконки обратно после неудачного совпадения
        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();

            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            firstClicked = null;
            secondClicked = null;
        }
    }
}
