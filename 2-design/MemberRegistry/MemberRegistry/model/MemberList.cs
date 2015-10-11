using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberRegistry.model
{
    class MemberList
    {
        private List<model.Member> _members;      

        public MemberList()
        {
            _members = new List<model.Member>(); 
        }

        public List<model.Member> Members
        {
            get { return _members; }
        }

        public void CreateMember(int memberID, string _firstName, string _lastName, string _ssn)
        {       
                model.Member m_member = new model.Member(memberID, _firstName, _lastName, _ssn);
                _members.Add(m_member);
        }

        public model.Member GetMember(int memberID)
        {
            foreach (model.Member member in _members)
            {
                if (member.MemberID == memberID)
                {
                    return member;
                }
            }
            return null;
        }

        public void UpdateMember(model.Member member, string firstName, string lastName, string ssn)
        {
            member.FirstName = firstName;
            member.LastName = lastName;
            member.SSN = ssn;
        }

        public void DeleteMember(model.Member member)
        {
            _members.Remove(member);
        }
    }
}
