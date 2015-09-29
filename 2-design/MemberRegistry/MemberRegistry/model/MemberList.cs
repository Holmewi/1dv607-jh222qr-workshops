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

        public void CreateMember(int memberID, string _firstName, string _lastName, string _ssn)
        {
            model.Member m_member = new model.Member(memberID, _firstName, _lastName, _ssn);
            m_members.Add(m_member);
        }

        public void DeleteMember(model.Member member)
        {
            m_members.Remove(member);
        }
    }
}
