using System;
using Engine.ViewModels; // to access the GameSession class in the Engine.ViewModels namespace
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestEngine.ViewModels {

    [TestClass] // unit testcalss
    public class TestGameSession {

        [TestMethod] // this function is a unit test function
        public void TestCreateGameSession() {

            GameSession gameSession = new GameSession();

            Assert.IsNotNull(gameSession.CurrentPlayer);
            Assert.AreEqual("Town square", gameSession.CurrentLocation.Name);
        }

        [TestMethod]
        public void TestPlayerMovesHomeAndIsCompletelyHealedOnKilled() {

            GameSession gameSession = new GameSession();

            gameSession.CurrentPlayer.TakeDamage(999);

            Assert.AreEqual("Home", gameSession.CurrentLocation.Name);
            Assert.AreEqual(gameSession.CurrentPlayer.Level * 10, gameSession.CurrentPlayer.CurrentHitPoints);
        }
    }
}
