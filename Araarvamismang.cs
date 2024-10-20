
namespace Elemendid_vormis_TARpv23
{
    public partial class Araarvamismang : Form
    {
        private Label timeLabel;
        private Label plusLeftLabel, plusRightLabel, minusLeftLabel, minusRightLabel, plusSign, minusSign;
        private Label timesLeftLabel, timesRightLabel, dividedLeftLabel, dividedRightLabel, timesSign, dividedSign;
        private Label timeLeftText;
        private NumericUpDown sumAnswer, minusAnswer, timesAnswer, devideAnswer;
        private Button startButton;
        private System.Windows.Forms.Timer timer1;
        Random randomizer = new Random();

        private Button checkAnswersButton;

        private Label correctAnswersLabel, wrongAnswersLabel;
        private int correctAnswers = 0;
        private int wrongAnswers = 0;

        int addend1, addend2;
        int minuend, subtrahend;
        int multiplicand, multiplier;
        int dividend, divisor;
        int timeLeft;
        int difficulty = 1;
        int baseTime = 30;
        int additionalTime = 10;

        public Araarvamismang()
        {
            this.Width = 500;
            this.Height = 450;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.BackColor = Color.LightGray;

            startButton = new Button();
            timer1 = new System.Windows.Forms.Timer(); // timer1
            sumAnswer = new NumericUpDown();
            minusAnswer = new NumericUpDown();
            timesAnswer = new NumericUpDown();
            devideAnswer = new NumericUpDown();
            Panel linePanel = new Panel();
            checkAnswersButton = new Button();

            // timer
            timer1.Interval = 1000;
            timer1.Tick += new EventHandler(timer1_Tick);

            // answer count labels
            correctAnswersLabel = CreateLabel("Correct Answers: 0", new Font("Arial", 12), new Size(200, 30), new Point(10, 10), true);
            wrongAnswersLabel = CreateLabel("Wrong Answers: 0", new Font("Arial", 12), new Size(200, 30), new Point(10, 30), true);
            correctAnswersLabel.ForeColor = Color.Green;
            wrongAnswersLabel.ForeColor = Color.Red;

            // line
            linePanel.Size = new Size(this.Width, 2);
            linePanel.Location = new Point(0, 50);
            linePanel.BackColor = Color.Black;

            // tags
            timeLabel = CreateLabel("", new Font("Arial", 15.75f), new Size(50, 30), new Point(265, 7), false);
            timeLabel.ForeColor = Color.Red;
            timeLeftText = CreateLabel("Time Left:", new Font("Arial", 15.75f), new Size(70, 30), new Point(170, 10), true);

            plusLeftLabel = CreateLabel("?", new Font("Arial", 18), new Size(60, 50), new Point(50, 75), true);
            plusRightLabel = CreateLabel("?", new Font("Arial", 18), new Size(60, 50), new Point(190, 75), true);
            plusSign = CreateLabel("+", new Font("Arial", 18), new Size(30, 50), new Point(120, 75), true);

            minusLeftLabel = CreateLabel("?", new Font("Arial", 18), new Size(60, 50), new Point(50, 150), true);
            minusRightLabel = CreateLabel("?", new Font("Arial", 18), new Size(60, 50), new Point(190, 150), true);
            minusSign = CreateLabel("-", new Font("Arial", 18), new Size(30, 50), new Point(120, 150), true);

            timesLeftLabel = CreateLabel("?", new Font("Arial", 18), new Size(60, 50), new Point(50, 225), true);
            timesRightLabel = CreateLabel("?", new Font("Arial", 18), new Size(60, 50), new Point(190, 225), true);
            timesSign = CreateLabel("×", new Font("Arial", 18), new Size(30, 50), new Point(120, 225), true);

            dividedLeftLabel = CreateLabel("?", new Font("Arial", 18), new Size(60, 50), new Point(50, 300), true);
            dividedRightLabel = CreateLabel("?", new Font("Arial", 18), new Size(60, 50), new Point(190, 300), true);
            dividedSign = CreateLabel(":", new Font("Arial", 18), new Size(30, 50), new Point(120, 300), true);

            Controls.Add(CreateLabel("=", new Font("Arial", 18), new Size(30, 50), new Point(260, 60), false));
            Controls.Add(CreateLabel("=", new Font("Arial", 18), new Size(30, 50), new Point(260, 140), false));
            Controls.Add(CreateLabel("=", new Font("Arial", 18), new Size(30, 50), new Point(260, 210), false));
            Controls.Add(CreateLabel("=", new Font("Arial", 18), new Size(30, 50), new Point(260, 285), false));

            // NumericUpDown
            sumAnswer.Font = new Font(sumAnswer.Font.FontFamily, 18);
            sumAnswer.MaximumSize = new Size(100, 0);
            sumAnswer.Location = new Point(330, 65);
            sumAnswer.Enter += new EventHandler(answer_Enter);

            minusAnswer.Font = new Font(minusAnswer.Font.FontFamily, 18);
            minusAnswer.MaximumSize = new Size(100, 0);
            minusAnswer.Location = new Point(330, 145);
            minusAnswer.Enter += new EventHandler(answer_Enter);

            timesAnswer.Font = new Font(timesAnswer.Font.FontFamily, 18);
            timesAnswer.MaximumSize = new Size(100, 0);
            timesAnswer.Location = new Point(330, 215);
            timesAnswer.Enter += new EventHandler(answer_Enter);

            devideAnswer.Font = new Font(devideAnswer.Font.FontFamily, 18);
            devideAnswer.MaximumSize = new Size(100, 0);
            devideAnswer.Location = new Point(330, 290);
            devideAnswer.Enter += new EventHandler(answer_Enter);

            sumAnswer.Minimum = int.MinValue;
            sumAnswer.Maximum = int.MaxValue;
            minusAnswer.Minimum = int.MinValue;
            minusAnswer.Maximum = int.MaxValue;
            timesAnswer.Minimum = int.MinValue;
            timesAnswer.Maximum = int.MaxValue;
            devideAnswer.Minimum = int.MinValue;
            devideAnswer.Maximum = int.MaxValue;

            // buttons
            startButton.Text = "Start the quiz";
            startButton.Font = new Font(startButton.Font.FontFamily, 14);
            startButton.AutoSize = true;
            startButton.Location = new Point(190, 360);
            startButton.Click += new EventHandler(StartButton_Click);

            checkAnswersButton.Text = "Check Answers";
            checkAnswersButton.Font = new Font(checkAnswersButton.Font.FontFamily, 14);
            checkAnswersButton.AutoSize = true;
            checkAnswersButton.Location = new Point(30, 360);
            checkAnswersButton.Click += new EventHandler(CheckAnswersButton_Click);

            // adding controls
            Controls.Add(sumAnswer);
            Controls.Add(minusAnswer);
            Controls.Add(timesAnswer);
            Controls.Add(devideAnswer);
            Controls.Add(startButton);
            Controls.Add(checkAnswersButton);
            Controls.Add(plusSign);
            Controls.Add(minusSign);
            Controls.Add(timesSign);
            Controls.Add(dividedSign);
            Controls.Add(linePanel);
            Controls.Add(correctAnswersLabel);
            Controls.Add(wrongAnswersLabel);
        }

        private void CheckAnswersButton_Click(object? sender, EventArgs e)
        {
            CheckAnswers();
        }

        private Label CreateLabel(string text, Font font, Size size, Point location, bool autoSize)
        {
            Label label = new Label
            {
                Text = text,
                Font = font,
                Size = size,
                Location = location,
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = autoSize
            };
            Controls.Add(label);
            return label;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            StartTheQuiz();
            startButton.Enabled = false;
        }

        public void StartTheQuiz()
        {
            // reset
            sumAnswer.BackColor = SystemColors.Window; 
            minusAnswer.BackColor = SystemColors.Window;
            timesAnswer.BackColor = SystemColors.Window;
            devideAnswer.BackColor = SystemColors.Window;
            sumAnswer.Value = 0;
            minusAnswer.Value = 0; 
            timesAnswer.Value = 0;
            devideAnswer.Value = 0;

            // make numbers 

            addend1 = randomizer.Next(5 * difficulty, 101 * difficulty); 
            addend2 = randomizer.Next(5 * difficulty, 101 * difficulty);

            minuend = randomizer.Next(10 * difficulty, 101 * difficulty); 
            subtrahend = randomizer.Next(1, minuend + 1);

            do
            {
                multiplicand = randomizer.Next(5 * difficulty, 11 * difficulty);
                multiplier = randomizer.Next(5 * difficulty, 11 * difficulty);
            } while (!((multiplicand < 10) || (multiplicand % 5 == 0) || (multiplier < 10) || (multiplier % 5 == 0)));

            do
            {
                divisor = randomizer.Next(3 * difficulty, 11 * difficulty);
            } while (!(divisor < 10 || divisor % 5 == 0));

            int temporaryQuotient = randomizer.Next(1, 11 * difficulty); 
            dividend = divisor * temporaryQuotient;

            // add numbers to labels
            plusLeftLabel.Text = addend1.ToString();
            plusRightLabel.Text = addend2.ToString();
            minusLeftLabel.Text = minuend.ToString();
            minusRightLabel.Text = subtrahend.ToString();
            timesLeftLabel.Text = multiplicand.ToString();
            timesRightLabel.Text = multiplier.ToString();
            dividedLeftLabel.Text = dividend.ToString();
            dividedRightLabel.Text = divisor.ToString();
            
            // reset answers
            timesAnswer.Value = 0;
            sumAnswer.Value = 0;
            timesAnswer.Value = 0;
            devideAnswer.Value = 0;

            // Start the timer.
            timeLeft = baseTime + (additionalTime * (difficulty - 1));
            timeLabel.Text = timeLeft.ToString();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                timeLeft = timeLeft - 1;
                timeLabel.Text = timeLeft + "";
            }
            else
            {
                timeLabel.Text = "Time's up!";
                sumAnswer.Value = addend1 + addend2;
                minusAnswer.Value = minuend - subtrahend;
                timesAnswer.Value = multiplicand * multiplier;
                devideAnswer.Value = dividend / divisor;

                CheckAnswers();
                difficulty += 1;
                startButton.Text = "Start level " + difficulty.ToString();
                startButton.Enabled = true;
            }
        }

        private void answer_Enter(object sender, EventArgs e)
        {
            // Select the whole answer in the NumericUpDown control.
            NumericUpDown answerBox = sender as NumericUpDown;

            if (answerBox != null)
            {
                int lengthOfAnswer = answerBox.Value.ToString().Length;
                answerBox.Select(0, lengthOfAnswer);
            }
        }

        private void CheckAnswers()
        {
            timer1.Stop();
            CountPoints();

            if ((addend1 + addend2 == sumAnswer.Value)
                && (minuend - subtrahend == minusAnswer.Value)
                && (multiplicand * multiplier == timesAnswer.Value)
                && (dividend / divisor == devideAnswer.Value))
            {
                startButton.Enabled = true;
                difficulty += 1; // increase difficulty
                startButton.Text = "Start level " + difficulty.ToString();
            }
            else
            {
                sumAnswer.Value = addend1 + addend2;
                minusAnswer.Value = minuend - subtrahend;
                timesAnswer.Value = multiplicand * multiplier;
                devideAnswer.Value = dividend / divisor;
                difficulty += 1;
                startButton.Text = "Start level " + difficulty.ToString();
                startButton.Enabled = true;
            }
        }

        private void CountPoints()
        {
            correctAnswers += (addend1 + addend2 == sumAnswer.Value) ? (sumAnswer.BackColor = Color.Green, 1).Item2 : (sumAnswer.BackColor = Color.Red, 0).Item2;
            wrongAnswers += (addend1 + addend2 == sumAnswer.Value) ? 0 : 1;

            correctAnswers += (minuend - subtrahend == minusAnswer.Value) ? (minusAnswer.BackColor = Color.Green, 1).Item2 : (minusAnswer.BackColor = Color.Red, 0).Item2;
            wrongAnswers += (minuend - subtrahend == minusAnswer.Value) ? 0 : 1;

            correctAnswers += (multiplicand * multiplier == timesAnswer.Value) ? (timesAnswer.BackColor = Color.Green, 1).Item2 : (timesAnswer.BackColor = Color.Red, 0).Item2;
            wrongAnswers += (multiplicand * multiplier == timesAnswer.Value) ? 0 : 1;

            correctAnswers += (dividend / divisor == devideAnswer.Value) ? (devideAnswer.BackColor = Color.Green, 1).Item2 : (devideAnswer.BackColor = Color.Red, 0).Item2;
            wrongAnswers += (dividend / divisor == devideAnswer.Value) ? 0 : 1;

            correctAnswersLabel.Text = $"Correct Answers: {correctAnswers}";
            wrongAnswersLabel.Text = $"Wrong Answers: {wrongAnswers}";

            // colors
            sumAnswer.BackColor = Color.Red;
            minusAnswer.BackColor = Color.Red;
            timesAnswer.BackColor = Color.Red;
            devideAnswer.BackColor = Color.Red;

            if (addend1 + addend2 == sumAnswer.Value) { sumAnswer.BackColor = Color.Green; }
            if (minuend - subtrahend == minusAnswer.Value) { minusAnswer.BackColor = Color.Green; }
            if (multiplicand * multiplier == timesAnswer.Value) { timesAnswer.BackColor = Color.Green; }
            if (dividend / divisor == devideAnswer.Value) { devideAnswer.BackColor = Color.Green; }
        }
    }
}