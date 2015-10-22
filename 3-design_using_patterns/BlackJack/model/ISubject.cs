using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model
{
    interface ISubject
    {
        void AddObserver(IObserver a_observer);

        void NotifyObserver(Card card);
    }
}
