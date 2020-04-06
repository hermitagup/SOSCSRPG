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
using Engine.ViewModels;   //Adding this using will instantiating Games Model object inside MainWindow class

namespace WPFUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameSession _gameSession;  //Declaring private variable
        public MainWindow()
        {
            InitializeComponent();
            _gameSession = new GameSession(); // Instantiating new GameSession object when starting new window| Now our View will have player and game session to work with (Player object is instantiatet in GameSession class.
            DataContext = _gameSession;  // This is what is XAML file is going to use for it's values.
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            _gameSession.CurrentPlayer.ExperiencePoints = _gameSession.CurrentPlayer.ExperiencePoints + 10;  // in short _gameSession.CurrentPlayer.ExperiencePoints +=10;
        }
    }
}

