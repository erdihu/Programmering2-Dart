using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Visual
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Player> _players = new List<Player>();
        private int userOutputInt;
        private bool isNumeric;
        private string userInput;
        private const int MAX_ARROW = 3;
        private bool continueGame = true;
        private Player _activePlayer;
        int[] score = new int[MAX_ARROW];
        private int i = 0;
        private int p = 1;

        public ICommand AimCommand { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            AimCommand = new DelegateCommand(AimCommand_Executed, CanGameContinue);
        }

        private void PlayerIterator()
        {
            if (p < _players.Count)
            {
                _activePlayer = _players[p - 1];
                p++;
            }
            if (p == _players.Count)
            {
                p = 1;
                _activePlayer = _players[0];
                ActivePlayer.Text = $"{_activePlayer.Name}, Arrow #{i + 1}";
            }
        }

        private void ScoreArrayIterator()
        {
            if (i == 0 || i == 1)
            {
                i++;
            }
            else
            {
                i = 0;
                _activePlayer.AddTurn(score[0], score[1], score[2]);
                PlayerIterator();
            }
        }

        private void Parse(string userInput)
        {
            isNumeric = int.TryParse(userInput, out userOutputInt);
        }

        private void BtnStart_OnClick(object sender, RoutedEventArgs e)
        {
            Parse(txtPlayerNumber.Text);

            if (!isNumeric)
            {
                MessageBox.Show("Only numbers are accepted");
                return;
            }

            //Create players
            for (int x = 0; x < userOutputInt; x++)
            {
                _players.Add(new Player($"Player {x+1}"));
            }

            _activePlayer = _players[0];
            ActivePlayer.Text = $"{_activePlayer.Name}, Arrow #{i + 1}";
            PlayerSetupGrid.Visibility = Visibility.Collapsed;
        }


        private bool CanGameContinue(object sender)
        {
            if (_activePlayer.CalculatePoints() > 301)
            {
                continueGame = false;
                ScoreBoard.Text = _activePlayer.PrintTurns();
                return false;
            }
            return true;
        }

        private void AimCommand_Executed(object sender)
        {

            score[i] = Dartboard.HitPoint(int.Parse(sender.ToString()));
            ScoreArrayIterator();
        }

        private void RandomCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            score[i] = Dartboard.HitPoint();
            ScoreArrayIterator();
        }


        private void RandomCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            CanGameContinue(sender);
            e.CanExecute = continueGame;
        }


    }
}
