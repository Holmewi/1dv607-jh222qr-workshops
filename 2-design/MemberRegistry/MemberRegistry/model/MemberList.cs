using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberRegistry.model
{
    class MemberList
    {
        private List<model.Member> m_members;

        public MemberList()
        {
            m_members = new List<model.Member>();
        }

        public List<model.Member> Members
        {
            get { return m_members; }
        }

        public void Add(model.Member a_member)
        {
            m_members.Add(a_member);
        }
    }
}
