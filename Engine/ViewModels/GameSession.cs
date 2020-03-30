using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.ViewModels
{
    class GameSession
    {
        Player CurrentPlayer { get; set; }

        public GameSession()    // GameSession constructor - part of a code run when object is being created
        {
            CurrentPlayer = new Player();   // Ewaluate what is on the right side of '=' and put on left side (property CurrentPlayer)
            CurrentPlayer.Name = "Scott";
            CurrentPlayer.Gold = 100000;
        }
    }
}
