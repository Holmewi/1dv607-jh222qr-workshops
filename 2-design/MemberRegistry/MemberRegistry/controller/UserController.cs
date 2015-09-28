using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberRegistry.controller
{
    class UserController
    {
        private view.StartView v_sv;
        private view.MemberView v_mv;
        private view.MemberListView v_mlv;

        private model.MemberList m_ml;

        public UserController()
        {
            m_ml = new model.MemberList();
        }

        public void DoControl(view.StartView a_sv, view.MemberView a_mv, view.MemberListView a_mlv)
        {
            v_sv = a_sv;
            v_mv = a_mv;
            v_mlv = a_mlv;

            // Test members for development
            model.Member m_member1 = new model.Member(m_ml.Members.Count() + 1000, "Dexter", "Morgan", "791103-4455");
            m_ml.Add(m_member1);
            model.Member m_member2 = new model.Member(m_ml.Members.Count() + 1000, "Joey", "Quinn", "750123-4455");
            m_ml.Add(m_member2);
            model.Member m_member3 = new model.Member(m_ml.Members.Count() + 1000, "Sergent", "Batista", "670130-4455");
            m_ml.Add(m_member3);

            DoDisplayStart();
        }

        public void DoDisplayStart()
        {
            v_sv.DisplayStartMenu();
        }

        public string GetStringInput(string _prompt)
        {
            while (true)
            {
                Console.Write(_prompt);
                string input = String.Format(Console.ReadLine());
                try
                {
                    if (string.IsNullOrEmpty(input))
                    {
                        throw new Exception();
                    }
                    return input;
                }
                catch (Exception)
                {
                    Console.WriteLine("You need to write something.");
                }
            }
        }

        public int GetIntInput(string _prompt)
        {
            while (true)
            {
                int input;
                Console.Write(_prompt);
                string line = Console.ReadLine();

                try
                {
                    input = Int32.Parse(line);

                    if (input < 0)
                    {
                        throw new Exception();
                    }
                    return input;
                }
                catch (FormatException)
                {
                    Console.WriteLine("You may only use numbers.");
                }
                catch (Exception)
                {
                    Console.WriteLine("The number you wrote is not a valid number.");
                }
            }
        }

        public model.Member GetMemberByID(int memberID)
        {
            foreach (model.Member member in GetMemberList())
            {
                if (member.MemberID == memberID)
                {
                    return member;
                }
            }
            return null;
        }

        public void CreateMember(string _firstName, string _lastName, string _ssn)
        {
            int memberID = m_ml.Members.Count() + 1000;
            model.Member m_member = new model.Member(memberID, _firstName, _lastName, _ssn);
            m_ml.Add(m_member);
        }

        public bool DeleteMember(int memberID)
        {
            List<model.Member> members = GetMemberList();

            foreach (model.Member member in members)
            {
                if (member.MemberID == memberID)
                {
                    members.Remove(member);
                    return true;
                }  
            }
            return false;
        }

        public List<model.Member> GetMemberList()
        {
            return m_ml.Members;
        }
    }
}
