using Engine.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestEngine.ViewModels
{
    [TestClass]
    public class TestGameSession
    {
        [TestMethod]
        public void TestCreateGameSession()
        {
            GameSession gameSession = new GameSession();    // instanciating GameSession object

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
            Assert.AreEqual(gameSession.CurrentPlayer.Level*10, gameSession.CurrentPlayer.CurrentHitPoints);
        }
    }
}
