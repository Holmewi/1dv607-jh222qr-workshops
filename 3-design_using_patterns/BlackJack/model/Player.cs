using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model
{
    class Player : ISubject
    {
        private List<Card> m_hand = new List<Card>();
        private List<IObserver> m_observers;

        public Player()
        {
            m_observers = new List<IObserver>();
        }

        public void DealCard(bool a_show, Card a_card)
        {
            a_card.Show(a_show);
            m_hand.Add(a_card);
            NotifyObserver(a_card);
        }

        public IEnumerable<Card> GetHand()
        {
            return m_hand.Cast<Card>();
        }

        public void ClearHand()
        {
            m_hand.Clear();
        }

        public void ShowHand()
        {
            foreach (Card c in GetHand())
            {
                c.Show(true);
            }
        }

        public int CalcScore()
        {
            int[] cardScores = new int[(int)model.Card.Value.Count]
                {2, 3, 4, 5, 6, 7, 8, 9, 10, 10 ,10 ,10, 11};
            int score = 0;

            foreach(Card c in GetHand()) {
                if (c.GetValue() != Card.Value.Hidden)
                {
                    score += cardScores[(int)c.GetValue()];
                }
            }

            if (score > 21)
            {
                foreach (Card c in GetHand())
                {
                    if (c.GetValue() == Card.Value.Ace && score > 21)
                    {
                        score -= 10;
                    }
                }
            }

            return score;
        }

        public void AddObserver(IObserver a_observer)
        {
            m_observers.Add(a_observer);
        }

        public void NotifyObserver(Card card)
        {
            foreach(IObserver observer in m_observers) {
                observer.UpdateCardInHand();
            }
        }
    }
}
