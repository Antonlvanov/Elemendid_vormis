using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace Elemendid_vormis_TARpv23
{
    public partial class Araarvamismang : Form
    {
        private Label timeLabel;
        private Label plusLeftLabel, plusRightLabel, minusLeftLabel, minusRightLabel, plusSign, minusSign;
        private Label timesLeftLabel, timesRightLabel, dividedLeftLabel, dividedRightLabel, timesSign, dividedSign;
        private Label timeLeftText;
        private NumericUpDown sum, difference, product, quotient;
        private Button startButton;
        private System.Windows.Forms.Timer timer1;
        Random randomizer = new Random();

        int addend1, addend2;
        int minuend, subtrahend;
        int multiplicand, multiplier;
        int dividend, divisor;
        int timeLeft;

        public Araarvamismang()
        {
            this.Width = 550;
            this.Height = 450;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;

            timer1 = new System.Windows.Forms.Timer(); // Инициализация timer1
            timer1.Interval = 1000;
            timer1.Tick += new EventHandler(timer1_Tick);

            sum = new NumericUpDown();
            difference = new NumericUpDown();
            product = new NumericUpDown();
            quotient = new NumericUpDown();
            startButton = new Button();

            // Инициализация элементов
            timeLabel = CreateLabel("", new Font("Arial", 15.75f), new Size(200, 30), new Point(300, 0), false);
            timeLeftText = CreateLabel("Time Left", new Font("Arial", 15.75f), new Size(100, 30), new Point(50, 0), true);

            plusLeftLabel = CreateLabel("?", new Font("Arial", 18), new Size(60, 50), new Point(50, 75), true);
            plusRightLabel = CreateLabel("?", new Font("Arial", 18), new Size(60, 50), new Point(200, 75), true);
            plusSign = CreateLabel("+", new Font("Arial", 18), new Size(30, 50), new Point(120, 75), true);

            minusLeftLabel = CreateLabel("?", new Font("Arial", 18), new Size(60, 50), new Point(50, 150), true);
            minusRightLabel = CreateLabel("?", new Font("Arial", 18), new Size(60, 50), new Point(200, 150), true);
            minusSign = CreateLabel("-", new Font("Arial", 18), new Size(30, 50), new Point(120, 150), true);

            timesLeftLabel = CreateLabel("?", new Font("Arial", 18), new Size(60, 50), new Point(50, 225), true);
            timesRightLabel = CreateLabel("?", new Font("Arial", 18), new Size(60, 50), new Point(200, 225), true);
            timesSign = CreateLabel("*", new Font("Arial", 18), new Size(30, 50), new Point(120, 225), true);

            dividedLeftLabel = CreateLabel("?", new Font("Arial", 18), new Size(60, 50), new Point(50, 300), true);
            dividedRightLabel = CreateLabel("?", new Font("Arial", 18), new Size(60, 50), new Point(200, 300), true);
            dividedSign = CreateLabel("/", new Font("Arial", 18), new Size(30, 50), new Point(120, 300), true);

            // Создание знаков равенства
            Controls.Add(CreateLabel("=", new Font("Arial", 18), new Size(30, 50), new Point(300, 75), false));
            Controls.Add(CreateLabel("=", new Font("Arial", 18), new Size(30, 50), new Point(300, 150), false));
            Controls.Add(CreateLabel("=", new Font("Arial", 18), new Size(30, 50), new Point(300, 225), false));
            Controls.Add(CreateLabel("=", new Font("Arial", 18), new Size(30, 50), new Point(300, 300), false));

            // events

            sum.Font = new Font(sum.Font.FontFamily, 18);
            sum.MaximumSize = new Size(100, 0);
            sum.Location = new Point(350, 75);
            sum.Enter += new EventHandler(answer_Enter);

            difference.Font = new Font(difference.Font.FontFamily, 18);
            difference.MaximumSize = new Size(100, 0);
            difference.Location = new Point(350, 150);
            difference.Enter += new EventHandler(answer_Enter);

            product.Font = new Font(product.Font.FontFamily, 18);
            product.MaximumSize = new Size(100, 0);
            product.Location = new Point(350, 225);
            product.Enter += new EventHandler(answer_Enter);

            quotient.Font = new Font(quotient.Font.FontFamily, 18);
            quotient.MaximumSize = new Size(100, 0);
            quotient.Location = new Point(350, 300);
            quotient.Enter += new EventHandler(answer_Enter);

            startButton.Text = "Start the quiz";
            startButton.Font = new Font(startButton.Font.FontFamily, 14);
            startButton.AutoSize = true;
            startButton.Location = new Point(200, 375);
            startButton.Click += new EventHandler(StartButton_Click);

            // controls
            Controls.Add(timeLabel);
            Controls.Add(timeLeftText);
            Controls.Add(plusLeftLabel);
            Controls.Add(plusRightLabel);
            Controls.Add(minusLeftLabel);
            Controls.Add(minusRightLabel);
            Controls.Add(timesLeftLabel);
            Controls.Add(timesRightLabel);
            Controls.Add(dividedLeftLabel);
            Controls.Add(dividedRightLabel);
            Controls.Add(sum);
            Controls.Add(difference);
            Controls.Add(product);
            Controls.Add(quotient);
            Controls.Add(startButton);

            Controls.Add(plusSign);
            Controls.Add(minusSign);
            Controls.Add(timesSign);
            Controls.Add(dividedSign);
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
            // Fill in the addition problem.
            // Generate two random numbers to add.
            // Store the values in the variables 'addend1' and 'addend2'.
            addend1 = randomizer.Next(51);
            addend2 = randomizer.Next(51);

            // Convert the two randomly generated numbers
            // into strings so that they can be displayed
            // in the label controls.
            plusLeftLabel.Text = addend1.ToString();
            plusRightLabel.Text = addend2.ToString();

            // 'sum' is the name of the NumericUpDown control.
            // This step makes sure its value is zero before
            // adding any values to it.
            sum.Value = 0;

            // Fill in the subtraction problem.
            minuend = randomizer.Next(1, 101);
            subtrahend = randomizer.Next(1, minuend);
            minusLeftLabel.Text = minuend.ToString();
            minusRightLabel.Text = subtrahend.ToString();
            difference.Value = 0;

            // Fill in the multiplication problem.
            multiplicand = randomizer.Next(2, 11);
            multiplier = randomizer.Next(2, 11);
            timesLeftLabel.Text = multiplicand.ToString();
            timesRightLabel.Text = multiplier.ToString();
            product.Value = 0;

            // Fill in the division problem.
            divisor = randomizer.Next(2, 11);
            int temporaryQuotient = randomizer.Next(2, 11);
            dividend = divisor * temporaryQuotient;
            dividedLeftLabel.Text = dividend.ToString();
            dividedRightLabel.Text = divisor.ToString();
            quotient.Value = 0;

            // Start the timer.
            timeLeft = 30;
            timeLabel.Text = "30 seconds";
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (CheckTheAnswer())
            {
                // If CheckTheAnswer() returns true, then the user 
                // got the answer right. Stop the timer  
                // and show a MessageBox.
                timer1.Stop();
                MessageBox.Show("You got all the answers right!",
                                "Congratulations!");
                startButton.Enabled = true;
            }
            else if (timeLeft > 0)
            {
                // If CheckTheAnswer() returns false, keep counting
                // down. Decrease the time left by one second and 
                // display the new time left by updating the 
                // Time Left label.
                timeLeft = timeLeft - 1;
                timeLabel.Text = timeLeft + " seconds";
            }
            else
            {
                // If the user ran out of time, stop the timer, show
                // a MessageBox, and fill in the answers.
                timer1.Stop();
                timeLabel.Text = "Time's up!";
                MessageBox.Show("You didn't finish in time.", "Sorry!");
                sum.Value = addend1 + addend2;
                difference.Value = minuend - subtrahend;
                product.Value = multiplicand * multiplier;
                quotient.Value = dividend / divisor;
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

        private bool CheckTheAnswer()
        {
            if ((addend1 + addend2 == sum.Value)
                && (minuend - subtrahend == difference.Value)
                && (multiplicand * multiplier == product.Value)
                && (dividend / divisor == quotient.Value))
                return true;
            else
                return false;
        }
    }
}