using System;
using Engine.ViewModels; // to access the GameSession class in the Engine.ViewModels namespace
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestEngine.ViewModels {

    [TestClass] // unit testcalss
    public class TestGameSession {

        [TestMethod] // this function is a unit test
        public void TestCreateGameSession() {

            GameSession gameSession = new GameSession();

            // Assertion znaczenie=twierdzenie (w tym przypadku test)
            // Assert means - we expect this condition to be true - check if it is true
            Assert.IsNotNull(gameSession.CurrentPlayer);                                    // check if default player object (after instantiating) is not Null
            Assert.AreEqual("Town square", gameSession.CurrentLocation.Name);               // assert if two values are equal
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
