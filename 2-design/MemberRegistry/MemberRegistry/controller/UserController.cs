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
        private view.CreateMemberView v_cmv;
        private view.MemberListView v_mlv;

        private model.MemberList m_ml;

        public UserController()
        {
            m_ml = new model.MemberList();
        }

        public void DoControl(view.StartView a_sv, view.CreateMemberView a_cmv, view.MemberListView a_mlv)
        {
            v_sv = a_sv;
            v_cmv = a_cmv;
            v_mlv = a_mlv;

            DoDisplayStart();
        }

        public void DoDisplayStart()
        {
            v_sv.DisplayStartMenu();
            v_sv.GetMainMenuResponse();
        }

        public void DoCreateMember()
        {
            v_cmv.DisplayMemberRegistration();
            v_cmv.GetRegistrationResponse();
        }

        public void DoListMembers()
        {
            v_mlv.DisplayCompactMemberList(m_ml);
        }

        public void DoExitApplication()
        {

        }

        public string GetKeyInput()
        {
            return Console.ReadKey().KeyChar.ToString();
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

        public void CreateMember(string _firstName, string _lastName, string _ssn)
        {
            int memberID = m_ml.Members.Count() + 1000;
            model.Member m_member = new model.Member(memberID, _firstName, _lastName, _ssn);
            m_ml.Add(m_member);
        }

        public string GetCompactMemberList()
        {
            foreach (model.Member member in m_ml.Members)
            {
                return String.Format(member.FirstName);
            }
            return "No registered members found";
        }


    }
}
