using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberRegistry.view
{
    class MemberListView
    {
        private controller.UserController c_uc;
        private bool compactList = true;
        private static string clist = "Compact";
        private static string vlist = "Verbose";
        private string listType;
        private int memberID;

        public MemberListView(controller.UserController a_uc)
        {
            c_uc = a_uc;
        }

        public void DisplayMemberList()
        {
            if (compactList)
            {
                listType = vlist;
                Console.Clear();
                Console.WriteLine("Compact Member List");
                Console.WriteLine("============================================");
                foreach (model.Member member in c_uc.GetMemberList())
                {
                    Console.WriteLine("{0} {1} {2}", member.MemberID, member.FirstName, member.LastName);
                }
            }
            else
            {
                listType = clist;
                Console.Clear();
                Console.WriteLine("Verbose Member List");
                Console.WriteLine("============================================");
                foreach (model.Member member in c_uc.GetMemberList())
                {
                    Console.WriteLine("Member ID: {0}", member.MemberID);
                    Console.WriteLine("Name: {0} {1}", member.FirstName, member.LastName);
                    Console.WriteLine("Social Security Number: {0}", member.SSN);
                    Console.WriteLine("The boats...");
                    Console.WriteLine("------------------------------------------");
                }
            }
        }

        public void DisplayMemberListMenu()
        {
            Console.WriteLine("============================================");
            Console.WriteLine("Press [1] - Display {0} member list.", listType);
            Console.WriteLine("Press [2] - View member information.");
            Console.WriteLine("Press [3] - Delete member.");
            Console.WriteLine("Press [Esc] - Back to main menu.");

            do
            {
                var input = Console.ReadKey();

                switch (input.Key)
                {
                    case ConsoleKey.D1:
                        Console.Clear();
                        ToggleListView();
                        DisplayMemberList();
                        DisplayMemberListMenu();
                        break;
                    case ConsoleKey.D2:
                        DisplayViewMember();
                        break;
                    case ConsoleKey.D3:
                        DisplayDeleteMemberInList();
                        break;
                    case ConsoleKey.Escape:
                        compactList = true;
                        c_uc.DoDisplayStart();
                        break;
                }

            } while (true);           
        }

        public void ToggleListView()
        {
            if (compactList)
            {
                compactList = false;
            }
            else
            {
                compactList = true;
            }
        }

        public void DisplayViewMember()
        {
            do
            {
                Console.Clear();
                DisplayMemberList();
                Console.WriteLine("============================================");
                Console.WriteLine("");
                while (!Console.KeyAvailable)
                {
                    memberID = c_uc.GetIntInput("Enter ID to view a member: ");
                    if (c_uc.GetMemberByID(memberID) != null)
                    {
                        DisplayMemberInfo(memberID);
                    }
                    else
                    {
                        break;
                    }
                }
                Console.Clear();
                DisplayMemberList();
                Console.WriteLine("============================================");
                Console.WriteLine("");
                Console.WriteLine("The Member ID [{0}] did not exist.", memberID);
                Console.WriteLine("Press [ESC] to go back or [Enter] to try again.");
                Console.WriteLine("");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);

            DisplayMemberList();
            DisplayMemberListMenu();
        }

        public void DisplayMemberInfo(int memberID)
        {
            do
            {
                foreach (model.Member member in c_uc.GetMemberList())
                {
                    if (member.MemberID == memberID)
                    {
                        Console.Clear();
                        Console.WriteLine("ID: {0}", member.MemberID);
                        Console.WriteLine("Name: {0} {1}", member.FirstName, member.LastName);
                        Console.WriteLine("Social Security Number: {0}", member.SSN);
                        Console.WriteLine("Boats..."); 
                    }
                }

                Console.WriteLine("============================================");
                Console.WriteLine("Press [ESC] to go back or [Enter] to try again.");
                if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                    DisplayViewMember();
                }
                else
                {
                    break;
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);

            DisplayMemberList();
            DisplayMemberListMenu();
        }

        public void DisplayDeleteMemberInList()
        {
            do
            {
                Console.Clear();
                DisplayMemberList();
                Console.WriteLine("============================================");
                Console.WriteLine("");
                while (!Console.KeyAvailable)
                {
                    memberID = c_uc.GetIntInput("Enter ID to delete a member: ");
                    if (c_uc.GetMemberByID(memberID) != null)
                    {
                        DisplayConfirmation(memberID);
                    }
                    else
                    {
                        break;
                    }
                }
                Console.Clear();
                DisplayMemberList();
                Console.WriteLine("============================================");
                Console.WriteLine("");
                Console.WriteLine("The Member ID [{0}] did not exist.", memberID);
                Console.WriteLine("Press [ESC] to go back or [Enter] to try again.");
                Console.WriteLine("");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);

            DisplayMemberList();
            DisplayMemberListMenu();
        }

        public void DisplayConfirmation(int memberID)
        {
            Console.Clear();
            Console.WriteLine("Please confirm to delete the member.");
            Console.WriteLine("Member: {0} {1} {2} {3}", 
                                            c_uc.GetMemberByID(memberID).MemberID,
                                            c_uc.GetMemberByID(memberID).FirstName,
                                            c_uc.GetMemberByID(memberID).LastName,
                                            c_uc.GetMemberByID(memberID).SSN);
            Console.WriteLine("Press [y] - Delete member");
            Console.WriteLine("Press [n] - Cancel");

            do
            {
                while (!Console.KeyAvailable)
                {
                    var input = Console.ReadKey();
                    Console.Clear();

                    switch (input.Key)
                    {
                        case ConsoleKey.Y:
                            c_uc.DeleteMember(memberID);
                            DisplayMemberList();
                            Console.WriteLine("============================================");
                            Console.WriteLine("Deleted member succesfully.");
                            Console.WriteLine("");

                            break;
                        case ConsoleKey.N:
                            DisplayMemberList();
                            Console.WriteLine("============================================");
                            Console.WriteLine("The Member did not get deleted...");
                            Console.WriteLine("");
                            break;
                    }
                    break;
                }
                Console.WriteLine("Press [ESC] to go back or [Enter] to delete another member.");
                if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                    DisplayDeleteMemberInList();
                }
                else
                {
                    break;
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);

            DisplayMemberList();
            DisplayMemberListMenu();
        }
    }
}
