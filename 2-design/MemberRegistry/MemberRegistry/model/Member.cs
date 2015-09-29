using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberRegistry.model
{
    class Member
    {
        private int m_memberID;
        private string m_firstName;
        private string m_lastName;
        private string m_ssn;

        private List<Boat> m_boats;

        public Member(int a_memberID, string a_firstName, string a_lastName, string a_ssn) 
        {
            m_memberID = a_memberID;
            m_firstName = a_firstName;
            m_lastName = a_lastName;
            m_ssn = a_ssn;
            m_boats = new List<model.Boat>();
        }

        public int MemberID
        {
            get { return m_memberID; }
        }

        public string FirstName
        {
            get { return m_firstName; }
            set { m_firstName = value; }
        }

        public string LastName
        {
            get { return m_lastName; }
            set { m_lastName = value; }
        }

        public string SSN
        {
            get { return m_ssn; }
            set { m_ssn = value;  }
        }

        public List<model.Boat> Boats
        {
            get { return m_boats; }
        }
    }
}
