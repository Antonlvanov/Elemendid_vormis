using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elemendid_vormis_TARpv23.MaluMang
{
    public class GameSettings
    {
        public TableLayoutPanel MainLayoutPanel { get; set; }
        public TableLayoutPanel TableLayoutPanel { get; set; }
        public Label TimeLabel { get; set; }
        public System.Windows.Forms.Timer Timer { get; set; }
        public System.Windows.Forms.Timer GameTimer { get; set; }
        public System.Windows.Forms.Timer ShowIconsTimer { get; set; }
        public Label FirstClicked { get; set; }
        public Label SecondClicked { get; set; }
        public int TimeElapsed { get; set; }
        public List<string> Icons { get; set; }
        public TableLayoutPanel TopPanel { get; set; }
        public Label LivesLabel { get; set; }
        public int Lives { get; set; }
        public System.Windows.Forms.Timer CountdownTimer { get; set; }
        public int CountdownValue { get; set; }
        public int GridSize { get; set; }

        public GameSettings()
        {
            FirstClicked = null;
            SecondClicked = null;
            TimeElapsed = 0;
            Icons = new List<string>()
            {
            "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
            "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"
            };
            Lives = 10;
            CountdownValue = 10;
            GridSize = 4;
        }

        public void IncreaseLives()
        {
            Lives += 5;  
        }

        public void DoubleCountdownValue()
        {
            CountdownValue *= 2;  
        }

        public void ResetSettings()
        {
            Lives = 10;  
            CountdownValue = 10; 
            GridSize = 4;  // Сбросить размер сетки
        }
    }
}
