using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.controller
{
    class PlayGame
    {
        public enum ActionInput
        {
            Play,
            Hit,
            Stand,
            Quit
        }

        public bool Play(model.Game a_game, view.IView a_view)
        {
            a_view.DisplayWelcomeMessage();
            
            a_view.DisplayDealerHand(a_game.GetDealerHand(), a_game.GetDealerScore());
            a_view.DisplayPlayerHand(a_game.GetPlayerHand(), a_game.GetPlayerScore());

            if (a_game.IsGameOver())
            {
                a_view.DisplayGameOver(a_game.IsDealerWinner());
            }
        
            var input = a_view.GetInput();

            if (input == ActionInput.Play)
            {
                a_game.NewGame();
            }
            else if (input == ActionInput.Hit)
            {
                a_game.Hit();
            }
            else if (input == ActionInput.Stand)
            {
                a_game.Stand();
            }

            return input != ActionInput.Quit;
        }
    }
}
