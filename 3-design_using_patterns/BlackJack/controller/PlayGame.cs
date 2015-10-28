using BlackJack.model;
using BlackJack.view;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.controller
{
    class PlayGame : IObserver
    {
        private view.IView m_view;
        private model.Game m_game;

        public enum ActionInput
        {
            Play,
            Hit,
            Stand,
            Quit
        }

        public PlayGame(model.Game a_game, view.IView a_view)
        {
            m_game = a_game;
            m_view = a_view;
            m_game.AddObserver(this);
        }

        public void UpdateCardInHand()
        {
            m_view.DisplayPause();
            DisplayGameTable();
        }

        public void DisplayGameTable()
        {
            m_view.DisplayWelcomeMessage();

            m_view.DisplayDealerHand(m_game.GetDealerHand(), m_game.GetDealerScore());
            m_view.DisplayPlayerHand(m_game.GetPlayerHand(), m_game.GetPlayerScore());
        }

        public bool Play()
        {
            DisplayGameTable();

            if (m_game.IsGameOver())
            {
                m_view.DisplayGameOver(m_game.IsDealerWinner());
            }

            var input = m_view.GetInput();

            if (input == view.ActionInput.Play)
            {
                m_game.NewGame();
            }
            else if (input == view.ActionInput.Hit)
            {
                m_game.Hit();
            }
            else if (input == view.ActionInput.Stand)
            {
                m_game.Stand();
            }

            return input != view.ActionInput.Quit;
        }
    }
}
