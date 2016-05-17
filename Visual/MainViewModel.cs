using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Visual.Annotations;

namespace Visual
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly List<Player> _players = new List<Player>();
        private int _userOutputInt;
        private bool _isNumeric;
        private const int _maxArrow = 3;
        private bool _continueGame = true;
        readonly int[] _score = new int[_maxArrow];
        private int _p = 0;
        private Player _activePlayer;
        private int _arrowNumber;
        private Visibility _playerSetupGridVisibility;
        private string _numberOfPlayers;
        private string _scoreboardText;

        public ICommand StartCommand => new DelegateCommand(Start);

        public Visibility PlayerSetupGridVisibility
        {
            get { return _playerSetupGridVisibility; }
            set
            {
                _playerSetupGridVisibility = value;
                OnPropertyChanged();
            }
        }

        public string ScoreboardText
        {
            get { return _scoreboardText; }
            set
            {
                if (value == _scoreboardText) return;
                _scoreboardText = value;
                OnPropertyChanged();
            }
        }

        public string NumberOfPlayers
        {
            get { return _numberOfPlayers; }
            set
            {
                _numberOfPlayers = value;
                OnPropertyChanged();
            }
        }

        public int ArrowNumber
        {
            get { return _arrowNumber; }
            set
            {
                _arrowNumber = value;
                OnPropertyChanged();
            }
        }

        public Player ActivePlayer
        {
            get { return _activePlayer; }
            set
            {
                _activePlayer = value;
                OnPropertyChanged();
            }
        }

        public ICommand AimCommand { get; set; }
        public ICommand RandomCommand { get; set; }

        public MainViewModel()
        {
            AimCommand = new DelegateCommand<object>(AimCommand_Executed, CanGameContinue);
            RandomCommand = new DelegateCommand<object>(RandomCommand_Executed, CanGameContinue);
        }

        private void PlayerIterator()
        {
            if (_p < _players.Count - 1)
            {
                ActivePlayer = _players[++_p];
            }
            else if (_p == _players.Count-1)
            {
                _p = 0;
                ActivePlayer = _players[_p];
            }
        }

        private void ScoreArrayIterator()
        {
            if (ArrowNumber == 0 || ArrowNumber == 1)
            {
                ArrowNumber++;
            }
            else
            {
                ArrowNumber = 0;
                ActivePlayer.AddTurn(_score[0], _score[1], _score[2]);
                PlayerIterator();
            }
        }

        private void Parse(string userInput)
        {
            _isNumeric = int.TryParse(userInput, out _userOutputInt);
        }

        private void Start()
        {
            Parse(NumberOfPlayers);

            if (!_isNumeric)
            {
                MessageBox.Show("Only numbers are accepted");
                return;
            }

            //Create players
            for (var x = 0; x < _userOutputInt; x++)
            {
                _players.Add(new Player($"Player {x + 1}"));
            }

            ActivePlayer = _players[0];
            PlayerSetupGridVisibility = Visibility.Collapsed;
        }

        private bool CanGameContinue(object sender)
        {
            if (ActivePlayer == null)
                return false;

            if (ActivePlayer.CalculatePoints() > 301)
            {
                _continueGame = false;
                ScoreboardText = ActivePlayer.PrintTurns();
                return false;
            }

            return true;
        }

        private void AimCommand_Executed(object sender)
        {
            _score[_arrowNumber] = Dartboard.HitPoint(int.Parse(sender.ToString()));
            ScoreArrayIterator();
        }

        private void RandomCommand_Executed(object sender)
        {
            _score[_arrowNumber] = Dartboard.HitPoint();
            ScoreArrayIterator();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}