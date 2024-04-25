using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
//  NURAN ERBEK FINAL PROJECT 


namespace TICTACTOE

{
    public sealed partial class MainPage : Page
    {
        private Button[,] buttons;
        private bool playerXTurn = true;
        private bool gameEnded = false;
        private Button restartButton;

        public MainPage()
        {
            this.InitializeComponent();
            InitializeGrid();
            //InitializeRestartButton();
        }

        private void InitializeGrid()
        {
            buttons = new Button[10, 10];

            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    Button button = new Button();
                    button.Tag = new int[] { row, col };
                    button.Click += Button_Click;
                    buttons[row, col] = button;

                    button.HorizontalAlignment = HorizontalAlignment.Stretch;
                    button.VerticalAlignment = VerticalAlignment.Stretch;
                    button.FontSize = 24;
                    button.Foreground = new SolidColorBrush(Colors.Black);

                    Grid.SetColumn(button, col);
                    Grid.SetRow(button, row);

                    gameGrid.Children.Add(button);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (gameEnded)
                return;

            Button clickedButton = sender as Button;
            int[] position = clickedButton.Tag as int[];

            if (clickedButton.Content == null)
            {
                clickedButton.Content = playerXTurn ? "X" : "O";
                clickedButton.FontWeight = Windows.UI.Text.FontWeights.Bold;
                playerXTurn = !playerXTurn;

                CheckForWin();
            }
        }

        private void CheckForWin()
        {
            // Check horizontal lines
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col <= 7; col++)
                {
                    if (AreEqual(buttons[row, col].Content, buttons[row, col + 1].Content, buttons[row, col + 2].Content))
                    {
                        EndGame($"{buttons[row, col].Content} wins!");
                        return;
                    }
                }
            }

            // Check vertical lines
            for (int col = 0; col < 10; col++)
            {
                for (int row = 0; row <= 7; row++)
                {
                    if (AreEqual(buttons[row, col].Content, buttons[row + 1, col].Content, buttons[row + 2, col].Content))
                    {
                        EndGame($"{buttons[row, col].Content} wins!");
                        return;
                    }
                }
            }

            // Check diagonal lines
            for (int row = 0; row <= 7; row++)
            {
                for (int col = 0; col <= 7; col++)
                {
                    if (AreEqual(buttons[row, col].Content, buttons[row + 1, col + 1].Content, buttons[row + 2, col + 2].Content))
                    {
                        EndGame($"{buttons[row, col].Content} wins!");
                        return;
                    }
                }
            }

            for (int row = 0; row <= 7; row++)
            {
                for (int col = 2; col < 10; col++)
                {
                    if (AreEqual(buttons[row, col].Content, buttons[row + 1, col - 1].Content, buttons[row + 2, col - 2].Content))
                    {
                        EndGame($"{buttons[row, col].Content} wins!");
                        return;
                    }
                }
            }

            // Check for draw
            bool draw = true;
            foreach (Button button in buttons)
            {
                if (button.Content == null)
                {
                    draw = false;
                    break;
                }
            }

            if (draw)
            {
                EndGame("It's a draw!");
            }
        }

        private bool AreEqual(object content1, object content2, object content3)
        {
            return content1 != null && content1.Equals(content2) && content2.Equals(content3);
        }


        private bool CheckLine(int row, int col, int dRow, int dCol)
        {
            Button firstButton = buttons[row, col];
            if (firstButton.Content == null)
                return false;

            for (int i = 1; i < 3; i++)
            {
                Button button = buttons[row + i * dRow, col + i * dCol];
                if (button.Content == null || button.Content.ToString() != firstButton.Content.ToString())
                    return false;
            }

            EndGame($"{firstButton.Content} wins!");
            return true;
        }

        private void EndGame(string message)
        {
            gameEnded = true;
            ShowWinnerPopup(message);
            //ShowRestartButton();
            RestartGame();
        }

        private void RestartGame()
        {
            foreach (Button button in buttons)
            {
                button.Content = null;
            }

            gameEnded = false;
            playerXTurn = true;
        }

        private async void ShowWinnerPopup(string message)
        {
            ContentDialog winnerDialog = new ContentDialog()
            {
                Title = "Game Over",
                Content = message,
                CloseButtonText = "OK"
            };

            await winnerDialog.ShowAsync();
        }

        //private void InitializeRestartButton()
        //{
        //    restartButton = new Button()
        //    {
        //        Content = "Restart",
        //        FontSize = 24,
        //        FontWeight = Windows.UI.Text.FontWeights.Bold,
        //        HorizontalAlignment = HorizontalAlignment.Center,
        //        VerticalAlignment = VerticalAlignment.Center,
        //        Margin = new Thickness(0, 20, 0, 0)
        //    };

        //    restartButton.Click += RestartButton_Click;

        //    // Add restartButton to gameGrid
        //    Grid.SetColumnSpan(restartButton, 10);
        //    Grid.SetRow(restartButton, 5); // Adjust row as needed
        //    gameGrid.Children.Add(restartButton);

        //    restartButton.Visibility = Visibility.Collapsed;
        //}


        //private void ShowRestartButton()
        //{
        //    restartButton.Visibility = Visibility.Visible;
        //}

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            restartButton.Visibility = Visibility.Collapsed;
            RestartGame();
        }
    }
}
