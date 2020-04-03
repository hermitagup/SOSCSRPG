﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.ViewModels
{
    public class GameSession
    {
        public Player CurrentPlayer { get; set; }

        public GameSession()    // GameSession constructor - part of a code run when object is being created
        {
            CurrentPlayer = new Player();   //  Instantiating Player object|| Evaluate what is on the right side of '=' and put on left side (property CurrentPlayer)
            // Below data are temp and for testing time only (This will display This data from This View Model via XAML Main Window
            CurrentPlayer.Name = "Scott";
            CurrentPlayer.CharacterClass = "Fighter";
            CurrentPlayer.HitPoints = 10;
            CurrentPlayer.Gold = 1000000;
            CurrentPlayer.ExperiencePoints = 0;
            CurrentPlayer.Level = 1;
        }
    }
}
