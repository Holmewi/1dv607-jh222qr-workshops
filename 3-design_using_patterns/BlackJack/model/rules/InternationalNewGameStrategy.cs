using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model.rules
{
    class InternationalNewGameStrategy : INewGameStrategy
    {

        public bool NewGame(Deck a_deck, Dealer a_dealer, Player a_player)
        {
            a_player.DealCard(true, a_deck.GetCard());

            a_dealer.DealCard(true, a_deck.GetCard());

            a_player.DealCard(true, a_deck.GetCard());

            return true;
        }
    }
}
