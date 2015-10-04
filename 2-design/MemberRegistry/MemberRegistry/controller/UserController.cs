using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MemberRegistry.controller
{
    class UserController
    {
        private view.ConsoleView v_sv;
        private model.MemberList m_ml;
        private controller.DataController c_dc;

        Regex nameRegex = new Regex(@"^[a-zA-Z]+(([\'\,\.\-][a-zA-Z])?[a-zA-Z]*)*$");
        Regex ssnRegex = new Regex(@"\b(?:19|20)?\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])[-+]?\d{4}\b");

        public UserController()
        {
            m_ml = new model.MemberList();
            c_dc = new controller.DataController(m_ml);
        }

        public void DoControl(view.ConsoleView a_sv)
        {
            v_sv = a_sv;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.BackgroundColor = ConsoleColor.Black;

            // Test members for development
            // Remove before launch
            /*
            m_ml.CreateMember(m_ml.Members.Count() + 1000, "Dexter", "Morgan", "791103-4455");
            m_ml.CreateMember(m_ml.Members.Count() + 1000, "Joey", "Quinn", "750123-4455");
            m_ml.CreateMember(m_ml.Members.Count() + 1000, "Sergent", "Batista", "670130-4455");
            c_dc.UpdateDataStorage();
             */
            c_dc.UpdateMemberList();
            DoDisplayStart();
        }

        public void DoDisplayStart()
        {
            v_sv.DisplayStartMenu();
        }

        public string GetStringInput(string prompt, bool isNotSSN)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = String.Format(Console.ReadLine());

                try
                {
                    if (string.IsNullOrEmpty(input))
                    {
                        throw new Exception();
                    }
                    
                    if (isNotSSN)
                    {
                        if (!nameRegex.IsMatch(input) || input.Any(char.IsDigit))
                        {
                            throw new FormatException();
                        }
                    }
                    else
                    {
                        if (!ssnRegex.IsMatch(input))
                        {
                            throw new ArgumentException();
                        }
                    }
                   
                    return input;
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Wrong social security number input.");
                }
                catch (FormatException)
                {
                    Console.WriteLine("You may only use letters.");
                }
                catch (Exception)
                {
                    Console.WriteLine("You need to write something.");
                }
            }
        }

        public int GetIntInput(string prompt)
        {
            while (true)
            {
                int input;
                Console.Write(prompt);
                string line = Console.ReadLine();

                try
                {
                    if (string.IsNullOrEmpty(line))
                    {
                        throw new ArgumentOutOfRangeException();
                    }

                    input = Int32.Parse(line);

                    if (input < 0)
                    {
                        throw new Exception();
                    }
                    return input;
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("You must enter a value.");
                }
                catch (FormatException)
                {
                    Console.WriteLine("You may only use numbers.");
                }
                catch (Exception)
                {
                    Console.WriteLine("You can't use negative values");
                }
            }
        }

        public void RegisterMember(int memberID, string firstName, string lastName, string ssn)
        {
            m_ml.CreateMember(memberID, firstName, lastName, ssn);
            c_dc.UpdateDataStorage();
        }

        public model.Member GetMemberByID(int memberID)
        {
            return m_ml.GetMember(memberID);
        }

        public void EditMemberInfo(model.Member member, string firstName, string lastName, string ssn)
        {
            m_ml.UpdateMember(member, firstName, lastName, ssn);
            c_dc.UpdateDataStorage();
        }

        public bool RemoveMemberFromList(int memberID)
        {
            foreach (model.Member member in GetMemberList())
            {
                if (member.MemberID == memberID)
                {
                    m_ml.DeleteMember(member);
                    c_dc.UpdateDataStorage();
                    return true;
                }
            }
            return false;
        }

        public void RegisterBoat(int memberID, string boatType, int length)
        {
            GetMemberByID(memberID).CreateBoat(memberID, boatType, length);
            c_dc.UpdateDataStorage();
        }

        public model.Boat GetBoat(model.Member member, int i)
        {
            return member.Boats[i];
            
        }

        public void EditBoatInfo(model.Member member, int i, string boatType, int length)
        {
            member.UpdateBoat(GetBoat(member, i), boatType, length);
            c_dc.UpdateDataStorage();
        }

        public void RemoveBoatFromList(model.Member member, int i)
        {
            member.DeleteBoat(GetBoat(member, i - 1));
            c_dc.UpdateDataStorage();
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
