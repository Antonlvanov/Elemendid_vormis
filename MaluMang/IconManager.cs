using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace Elemendid_vormis_TARpv23.MaluMang
{
    public class IconManager
    {
        private GameSettings gameSettings;

        public IconManager(GameSettings settings)
        {
            gameSettings = settings;
        }

        public void AssignIconsToSquares()
        {
            List<string> iconsCopy = ShuffleIcons(new List<string>(gameSettings.Icons));
            List<int> availableCells = GetAvailableCells();

            foreach (string icon in iconsCopy)
            {
                AssignIconToRandomCells(icon, availableCells);
            }
        }

        private List<string> ShuffleIcons(List<string> icons)
        {
            Random random = new Random();
            int n = icons.Count;

            for (int i = n - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                string temp = icons[i];
                icons[i] = icons[j];
                icons[j] = temp;
            }

            return icons;
        }

        private List<int> GetAvailableCells()
        {
            List<int> availableCells = new List<int>();
            for (int i = 0; i < gameSettings.TableLayoutPanel.Controls.Count; i++)
            {
                availableCells.Add(i);
            }
            return availableCells;
        }

        private void AssignIconToRandomCells(string icon, List<int> availableCells)
        {
            if (availableCells.Count < 2) return; // Проверка на наличие достаточного количества ячеек

            Random random = new Random();

            // Выбираем первую ячейку
            int firstIndex = random.Next(availableCells.Count);
            int firstCell = availableCells[firstIndex];
            availableCells.RemoveAt(firstIndex); // Удаляем первую ячейку из доступных

            // Выбираем вторую ячейку
            int secondIndex = random.Next(availableCells.Count);
            int secondCell = availableCells[secondIndex];
            availableCells.RemoveAt(secondIndex); // Удаляем вторую ячейку из доступных

            // Присваиваем иконку двум выбранным ячейкам
            SetIconToCell(firstCell, icon);
            SetIconToCell(secondCell, icon);
        }

        private void SetIconToCell(int cellIndex, string icon)
        {
            Label label = gameSettings.TableLayoutPanel.Controls[cellIndex] as Label;
            if (label != null)
            {
                label.Tag = icon; // Назначаем иконку
                label.Text = icon; // Устанавливаем текст иконки
                label.ForeColor = label.BackColor; // Скрываем иконку
            }
        }
    }
}
