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
using Engine.EventArgs;
using Engine.ViewModels;   //Adding this using will instantiating Games Model object inside MainWindow class

namespace WPFUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly GameSession _gameSession = new GameSession();  //Declaring private variable
        public MainWindow()
        {
            InitializeComponent();
            _gameSession = new GameSession(); // Instantiating new GameSession object when starting new window| Now our View will have player and game session to work with (Player object is instantiatet in GameSession class.
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
        
        
        private void OnGameMessageRaised(object sender, GameMessageEventArgs e) {
            GameMessages.Document.Blocks.Add(new Paragraph(new Run(e.Message)));
            GameMessages.ScrollToEnd();
        }
    }
}