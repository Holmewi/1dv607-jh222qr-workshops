using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberRegistry.model
{
    class Boat
    {
        private int m_lenght;
        private string m_boatType;

        public Boat(string a_boatType, int a_length)
        {
            m_boatType = a_boatType;
            m_lenght = a_length;
        }

        public string Boattype
        {
            get { return m_boatType; }
            set { m_boatType = value; }
        }
    }
}
