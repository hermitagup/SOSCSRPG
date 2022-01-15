using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using Engine.EventArgs;
using Engine.ViewModels;   //Adding this using will instantiating Games Model object inside MainWindow class
using Engine.Models;

namespace WPFUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow :Window {
        private readonly GameSession _gameSession = new GameSession();  // Declaring private readonly variable and Instantiating new GameSession object when starting new window
                                                                        // Now our View will have player and game session to work with (Player object is instantiatet in GameSession class.

        private readonly Dictionary<Key, Action> _userInputActions = new Dictionary<Key, Action>();     // Key - key pressed by a user , Action - delegate -- > function we are going to run
                                                                                                        // Dictionary is a special type of collection described by a Key/ Value pair
                                                                                                        // we ask for a key (when found) result in a corresponding value
                                                                                                        // if we would pass any parameters it could looks like
                                                                                                        //
                                                                                                        // void Main() {
                                                                                                        //      Dictionary<key, Action<int, int>> _userInputActions = new Dictionary<Key, Action<int, int>>();
                                                                                                        //      _userInputActions.Add("Something", (first, second) => DoSomething(first, second));
                                                                                                        //      _userInputActions.Add("SomethingElse", (primary, secondary) => DoSomethingElse(primary, secondary));
                                                                                                        //      _userInputActions.Add("SomethingSpecial", (x, y) => {
                                                                                                        //          SpecialFunction();
                                                                                                        //          DoSomething(x, y);
                                                                                                        //      });
                                                                                                        //
                                                                                                        //      _userInputActions["Something"].Invoke(3,4);
                                                                                                        // }
                                                                                                        //
                                                                                                        // public void DoSomething(int x, int y){}
                                                                                                        // public void DoSomethingElse(int one, int two){}
                                                                                                        // public void SpecialFunction(){}


        public MainWindow() {
            InitializeComponent();
            InitializeUserInputActions();
            _gameSession.OnMessageRaised += OnGameMessageRaised; //funciton to handle event
            DataContext = _gameSession;  // This is what is XAML file is going to use for it's values.
        }

        private void OnClick_MoveNorth(object sender, RoutedEventArgs e) // used only by mainwindow (private); not returns any value (void); 2 parameters: 
        {
            _gameSession.MoveNorth();
        }

        private void OnClick_MoveWest(object sender, RoutedEventArgs e) // used only by mainwindow (private); not returns any value (void); 2 parameters: 
        {
            _gameSession.MoveWest();
        }

        private void OnClick_MoveEast(object sender, RoutedEventArgs e) // used only by mainwindow (private); not returns any value (void); 2 parameters: 
        {
            _gameSession.MoveEast();
        }

        private void OnClick_MoveSouth(object sender, RoutedEventArgs e) // used only by mainwindow (private); not returns any value (void); 2 parameters: 
        {
            _gameSession.MoveSouth();
        }

        private void OnClick_AttackMonster(object sender, RoutedEventArgs e) {
            _gameSession.AttackCurrentMonster();
        }

        private void OnClick_UseCurrentConsumable(object sender, RoutedEventArgs e) {
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

        private void OnClick_Craft(object sender, RoutedEventArgs e) {
            Recipe recipe = ((FrameworkElement)sender).DataContext as Recipe;
            _gameSession.CraftItemUsing(recipe);
        }

        private void InitializeUserInputActions(){
            _userInputActions.Add(Key.W, () => _gameSession.MoveNorth());               // read as: when 'W' key pressed
            _userInputActions.Add(Key.S, () => _gameSession.MoveSouth());               // '()' - list of parameters passed to a function that we are going to run
            _userInputActions.Add(Key.A, () => _gameSession.MoveWest());                // as we don't need to pass any parameters we pass nothing as '()'
            _userInputActions.Add(Key.D, () => _gameSession.MoveEast());                // '=>' - is the lambda expression
            _userInputActions.Add(Key.Z, () => _gameSession.AttackCurrentMonster());    // 
            _userInputActions.Add(Key.C, () => _gameSession.UseCurrentConsumable());
            _userInputActions.Add(Key.I, () => SetTabFocusTo("InventoryTabItem"));
            _userInputActions.Add(Key.Q, () => SetTabFocusTo("QuestsTabItem"));
            _userInputActions.Add(Key.R, () => SetTabFocusTo("RecipesTabItem"));

            _userInputActions.Add(Key.D1, () => SetTabFocusTo("InventoryTabItem"));      //Addition
            _userInputActions.Add(Key.D2, () => SetTabFocusTo("QuestsTabItem"));         //Addition
            _userInputActions.Add(Key.D3, () => SetTabFocusTo("RecipesTabItem"));        //Addition

            _userInputActions.Add(Key.T, () => OnClick_DisplayTradeScreen(this, new RoutedEventArgs()));
        }

        private void MainWindow_OnKeyDown(object sender, KeyEventArgs e) {
            if (_userInputActions.ContainsKey(e.Key)) {
                _userInputActions[e.Key].Invoke();
            }
        }
        private void SetTabFocusTo(string tabName) {
            foreach (object item in PlayerDataTabControl.Items) {
                if (item is TabItem tabItem) {                              // shorter for this cast 2 liner:   TabItem tabItem = item as TabItem
                    if (tabItem.Name == tabName) {                          //                                  if (tabItem != null){}
                        tabItem.IsSelected = true;
                        return;
                    }
                }
            }
        }
    }
}