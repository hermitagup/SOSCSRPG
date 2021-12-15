using Engine.EventArgs;
using Engine.ViewModels;   //Adding this using will instantiating Games Model object inside MainWindow class
using System.Windows;
using System.Windows.Documents;

namespace WPFUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly GameSession _gameSession = new GameSession();  // Declaring private readonly variable and Instantiating new GameSession object when starting new window
                                                                        // Now our View will have player and game session to work with (Player object is instantiatet in GameSession class.
        public MainWindow()
        {
            InitializeComponent();
            _gameSession.OnMessageRaised += OnGameMessageRaised; //funciton to handle event
            DataContext = _gameSession;  // This is what is XAML file is going to use for it's values.
        }

        private void OnClick_MoveNorth (object sender, RoutedEventArgs e) // used only by mainwindow (private); not returns any value (void); 2 parameters: 
        {
            _gameSession.MoveNorth();
        }

        private void OnClick_MoveWest (object sender, RoutedEventArgs e) // used only by mainwindow (private); not returns any value (void); 2 parameters: 
        {
            _gameSession.MoveWest();
        }

        private void OnClick_MoveEast (object sender, RoutedEventArgs e) // used only by mainwindow (private); not returns any value (void); 2 parameters: 
        {
            _gameSession.MoveEast();
        }

        private void OnClick_MoveSouth (object sender, RoutedEventArgs e) // used only by mainwindow (private); not returns any value (void); 2 parameters: 
        {
            _gameSession.MoveSouth();
        }

        private void OnClick_AttackMonster(object sender, RoutedEventArgs e) { 
            _gameSession.AttackCurrentMonster();
        }
        private void OnClick_UseCurrentConsumable(object sender, RoutedEventArgs e)
        {
            _gameSession.UseCurrentConsumable();
        }
        private void OnGameMessageRaised(object sender, GameMessageEventArgs e) {
            GameMessages.Document.Blocks.Add(new Paragraph(new Run(e.Message)));
            GameMessages.ScrollToEnd();
        }

        private void OnClick_DisplayTradeScreen(object sender, RoutedEventArgs e) {
            TradeScreen tradeScreen = new TradeScreen();
            tradeScreen.Owner = this;
            tradeScreen.DataContext = _gameSession;
            tradeScreen.ShowDialog();   // Show() - creates not modal window where you can still interact and click buttons on main window | ShowDialog() - creates modal window where focus is in a new window and main window interaction is no more possible.
        }


    }
}