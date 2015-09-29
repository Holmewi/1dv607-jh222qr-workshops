using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberRegistry.controller
{
    class UserController
    {
        private view.ConsoleView v_sv;
        private model.MemberList m_ml;

        public UserController()
        {
            m_ml = new model.MemberList();
        }

        public void DoControl(view.ConsoleView a_sv)
        {
            v_sv = a_sv;

            // Test members for development
            // Remove before launch
            m_ml.CreateMember(m_ml.Members.Count() + 1000, "Dexter", "Morgan", "791103-4455");
            m_ml.CreateMember(m_ml.Members.Count() + 1000, "Joey", "Quinn", "750123-4455");
            m_ml.CreateMember(m_ml.Members.Count() + 1000, "Sergent", "Batista", "670130-4455");


            DoDisplayStart();
        }

        public void DoDisplayStart()
        {
            v_sv.DisplayStartMenu();
        }

        public void DoDisplayMemberRegistration()
        {

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

        public void RegisterMember(int _memberID, string _firstName, string _lastName, string _ssn)
        {
            // TODO: CONTROL VARIABELS
            m_ml.CreateMember(_memberID, _firstName, _lastName, _ssn);
        }

        public void RegisterBoat(int _memberID, string _boatType, int _lenght)
        {
            // TODO: CONTROL VARIABELS
            GetMemberByID(_memberID).CreateBoat(_memberID, _boatType, _lenght);
        }

        public bool DeleteMember(int memberID)
        {
            foreach (model.Member member in GetMemberList())
            {
                if (member.MemberID == memberID)
                {
                    m_ml.DeleteMember(member);
                    return true;
                }  
            }
            return false;
        }

        public List<model.Member> GetMemberList()
        {
            return m_ml.Members;
        }

        public List<model.Boat> GetBoatList(model.Member member)
        {
            return member.Boats;
        }
    }
}
