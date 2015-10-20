using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model.rules
{
    interface IWinStrategy
    {
        int GetMaxScore { get; }

        bool DoIsDealerWinner(model.Player a_dealer, model.Player a_player);
    }
}
