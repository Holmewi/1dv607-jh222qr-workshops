using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberRegistry.view
{
    class ConsoleView
    {
        private controller.UserController c_uc;

        private bool compactList = true;
        private static string clist = "Compact";
        private static string vlist = "Verbose";
        private string listType = clist;
        private string _message = null;

        private int _memberID;
        private string _firstName;
        private string _lastName;
        private string _ssn;

        private string _boatType = null;
        private int _boatLength;

        public enum BoatType
        {
            Sailboat = 1,
            Motorsailer = 2,
            KayakCanoe = 3,
            Other = 4
        }

        public ConsoleView(controller.UserController a_uc)
        {
            c_uc = a_uc;
        }

        public void DisplayStartMenu()
        {
            Console.Clear();
            Console.WriteLine("THE HAPPY PIRATE");
            Console.WriteLine("Member & Boat Registry");
            Console.WriteLine("==========================================");
            Console.WriteLine("");
            DisplayMemberList();
            Console.WriteLine("");
            Console.WriteLine("==========================================");
            Console.WriteLine("Press [1] - Create New Member");
            Console.WriteLine("Press [2] - View Member Information");
            Console.WriteLine("Press [3] - Add boat to member");
            Console.WriteLine("Press [4] - Delete Member");
            Console.WriteLine("Press [5] - Display {0} Member List", listType); ;
            Console.WriteLine("Press [Esc] - Quit Application");
            if (_message != null)
            {
                Console.WriteLine("");
                Console.WriteLine("{0}", _message);
            }
            _message = null;
            _boatType = null;

            do
            {
                var input = Console.ReadKey(true);

                switch (input.Key)
                {
                    case ConsoleKey.D1:
                        Console.Clear();
                        DisplayMemberRegistrationForm();
                        break;
                    case ConsoleKey.D2:
                        DisplayViewMember();
                        break;
                    case ConsoleKey.D3:

                        break;
                    case ConsoleKey.D4:
                        DisplayDeleteMemberInList();
                        break;
                    case ConsoleKey.D5:
                        ToggleListView();
                        DisplayStartMenu();
                        break;
                    case ConsoleKey.Escape:
                        ExitConfirmation();
                        break;
                }

            } while (true);
        }

        public void DisplayMemberRegistrationForm()
        {
            Console.WriteLine("Fill in member information.");
            Console.WriteLine("============================================");

            _memberID = c_uc.GetMemberList().Max(r => r.MemberID) + 1;
            _firstName = c_uc.GetStringInput("First name: ");
            _lastName = c_uc.GetStringInput("Last name: ");
            _ssn = c_uc.GetStringInput("Social Security Number (10 digits): ");

            DisplaySaveMemberConfirmation();
        }

        public void DisplaySaveMemberConfirmation()
        {
            Console.WriteLine("");
            Console.WriteLine("Please confirm to save the member.");
            Console.WriteLine("============================================");
            Console.WriteLine("Press [1] - Finish registration");
            Console.WriteLine("Press [2] - Add boat to member");
            Console.WriteLine("Press [3] - Redo registration");
            Console.WriteLine("Press [ESC] - Cancel registration");

            

            do
            {
                var input = Console.ReadKey(true);
                
                switch (input.Key)
                {
                    case ConsoleKey.D1:
                        c_uc.RegisterMember(_memberID, _firstName, _lastName, _ssn);
                        _message = "Added member succesfully!";
                        DisplayStartMenu();
                        break;
                    case ConsoleKey.D2:
                        c_uc.RegisterMember(_memberID, _firstName, _lastName, _ssn);
                        _message = "Added member succesfully!";
                        DisplayBoatRegistrationForm();
                        break;
                    case ConsoleKey.D3:
                        Console.Clear();
                        Console.WriteLine("Please try again...");
                        Console.WriteLine("");
                        DisplayMemberRegistrationForm();
                        break;
                    case ConsoleKey.Escape:
                        _message = "The Member did not get saved...";
                        DisplayStartMenu();
                        break;
                }
            } while (true);
        }

        public void DisplayBoatRegistrationForm()
        {
            Console.Clear();
            Console.WriteLine("Member: {0} {1}", c_uc.GetMemberByID(_memberID).FirstName, c_uc.GetMemberByID(_memberID).LastName);
            
            foreach(model.Boat boat in c_uc.GetMemberByID(_memberID).Boats) {
                Console.WriteLine("Boat: {0} {1}", boat.BoatType, boat.Length);
            }
            Console.WriteLine("");
            Console.WriteLine("Fill in boat information.");
            Console.WriteLine("============================================");
            
            

            do
            {
                if (_boatType == null)
                {
                    Console.WriteLine("Select boat type");
                    Console.WriteLine("Press {0} - Sailboat:", (int)BoatType.Sailboat);
                    Console.WriteLine("Press {0} - Motorsailer:", (int)BoatType.Motorsailer);
                    Console.WriteLine("Press {0} - Kayak / Canoe:", (int)BoatType.KayakCanoe);
                    Console.WriteLine("Press {0} - Other:", (int)BoatType.Other);
                
                    var input = Console.ReadKey(true);

                    switch (input.Key)
                    {
                        case ConsoleKey.D1:
                            _boatType = "Sailboat";
                            break;
                        case ConsoleKey.D2:
                            _boatType = "Motorsailer";
                            break;
                        case ConsoleKey.D3:
                            _boatType = "Kayak / Canoe";
                            break;
                        case ConsoleKey.D4:
                            _boatType = "Other";
                            break;
                    }

                    DisplayBoatRegistrationForm();
                }

                Console.WriteLine("Boat Type: {0}", _boatType);
                _boatLength = c_uc.GetIntInput("Boat Lenght (cm): ");
                
                DisplaySaveBoatConfirmation();
            } while (true);
        }

        public void DisplaySaveBoatConfirmation()
        {
            Console.WriteLine("");
            Console.WriteLine("Please confirm to register the boat.");
            Console.WriteLine("============================================");
            Console.WriteLine("Press [1] - Finish boat registration");
            Console.WriteLine("Press [2] - Redo the boat registration");
            Console.WriteLine("Press [3] - Add another boat to member");
            Console.WriteLine("Press [ESC] - Cancel boat registration");

            do
            {
                var input = Console.ReadKey(true);

                switch (input.Key)
                {
                    case ConsoleKey.D1:
                        c_uc.RegisterBoat(_memberID, _boatType, _boatLength);
                        _message = "Member and boat registered succesfully.";
                        DisplayStartMenu();
                        break;
                    case ConsoleKey.D2:
                        Console.WriteLine("Please try again...");
                        Console.WriteLine("");
                        _boatType = null;
                        DisplayBoatRegistrationForm();
                        break;
                    case ConsoleKey.D3:
                        c_uc.RegisterBoat(_memberID, _boatType, _boatLength);
                        Console.WriteLine("Boat added succesfully...");
                        Console.WriteLine("");
                        _boatType = null;
                        DisplayBoatRegistrationForm();
                        break;
                    case ConsoleKey.Escape:
                        _message = "Created a member with no registered boats...";
                        DisplayStartMenu();
                        break;
                }
            } while (true);
        }

        public void DisplayMemberList()
        {
            if (compactList)
            {
                listType = vlist;
                Console.WriteLine("ID Name");
                Console.WriteLine("------------------------------------------");
                foreach (model.Member member in c_uc.GetMemberList())
                {
                    Console.WriteLine("{0} {1} {2}", member.MemberID, member.FirstName, member.LastName);
                }
            }
            else
            {
                listType = clist;
                foreach (model.Member member in c_uc.GetMemberList())
                {
                    Console.WriteLine("..........................................");
                    Console.WriteLine("ID: {0}", member.MemberID);
                    Console.WriteLine("Name: {0} {1}", member.FirstName, member.LastName);
                    Console.WriteLine("Social Security Number: {0}", member.SSN);
                    foreach (model.Boat boat in member.Boats)
                    {
                        Console.WriteLine("Boat: {0} {1}", boat.BoatType, boat.Length);
                    }
                }
            }
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
                    _memberID = c_uc.GetIntInput("Enter ID to view a member: ");
                    if (c_uc.GetMemberByID(_memberID) != null)
                    {
                        DisplayMemberInfo(_memberID);
                    }
                    else
                    {
                        break;
                    }
                }
                Console.Clear();
                DisplayMemberList();
                Console.WriteLine("");
                Console.WriteLine("============================================");
                Console.WriteLine("");
                Console.WriteLine("The Member ID [{0}] did not exist.", _memberID);
                Console.WriteLine("Press [ESC] to go back or [Enter] to try again.");
                Console.WriteLine("");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
            DisplayStartMenu();
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
                        Console.WriteLine("");
                        foreach (model.Boat boat in member.Boats)
                        {
                            Console.WriteLine("Boat: {0} {1}", boat.BoatType, boat.Length);
                        }
                        Console.WriteLine("");
                    }
                }

                Console.WriteLine("============================================");
                Console.WriteLine("Press [ESC] to go back to menu or [Enter] to try again.");

                if(Console.ReadKey(true).Key == ConsoleKey.Enter) {
                    DisplayViewMember();
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
            DisplayStartMenu();
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
                    _memberID = c_uc.GetIntInput("Enter ID to delete a member: ");
                    if (c_uc.GetMemberByID(_memberID) != null)
                    {
                        DisplayDeleteConfirmation(_memberID);
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
                Console.WriteLine("The Member ID [{0}] did not exist.", _memberID);
                Console.WriteLine("Press [ESC] to go back or [Enter] to try again.");
                Console.WriteLine("");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
            DisplayStartMenu();
        }

        public void DisplayDeleteConfirmation(int memberID)
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
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }

        public void ExitConfirmation()
        {
            Console.Clear();
            Console.WriteLine("Are you sure you want to quit the application?");
            Console.WriteLine("Press [y] - Yes");
            Console.WriteLine("Press [n] - No");

            do
            {
                var input = Console.ReadKey();

                switch (input.Key)
                {
                    case ConsoleKey.Y:
                        Console.Clear();
                        Environment.Exit(0);
                        break;
                    case ConsoleKey.N:
                        DisplayStartMenu();
                        break;
                }

                Console.Clear();
                ExitConfirmation();

            } while (true);
        }
    }
}
