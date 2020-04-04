using Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// logika gry, polaczenie z player

namespace Engine.ViewModels
{
    public class GameSession
    {
        public Player CurrentPlayer { get; set; } // ustawienie klasy 'Player' 
        
        public GameSession() // Konstruktor (funkcja) gamesession
        {
            CurrentPlayer = new Player();
            CurrentPlayer.Name = "Scott";
            CurrentPlayer.Gold = 1000000;
        }
    }
}
