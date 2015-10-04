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

        public void DisplayHeader()
        {
            Console.WriteLine("");
            Console.WriteLine("THE HAPPY PIRATE");
            Console.WriteLine("Member & Boat Registry");
            Console.WriteLine("__________________________________________");
            Console.WriteLine("");
        }

        public void DisplayStartMenu()
        {
            Console.Clear();
            DisplayHeader();
            DisplayMemberList();

            if (_message != null)
            {
                Console.WriteLine("");
                Console.WriteLine("{0}", _message);
            }

            
            Console.WriteLine("--- Start Menu ---------------------------");
            Console.WriteLine("");
            Console.WriteLine("Press [1] - Create New Member");
            Console.WriteLine("Press [2] - View Member Information");
            Console.WriteLine("Press [3] - Delete Member");
            Console.WriteLine("Press [4] - Display {0} Member List", listType); ;
            Console.WriteLine("Press [Esc] - Quit Application");

            _message = null;
            _boatType = null;

            do
            {
                var input = Console.ReadKey(true);

                switch (input.Key)
                {
                    case ConsoleKey.D1:
                        DisplayMemberRegistrationForm();
                        break;
                    case ConsoleKey.D2:
                        DisplayViewMember();
                        break;
                    case ConsoleKey.D3:
                        DisplayDeleteMemberInList();
                        break;
                    case ConsoleKey.D4:
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
            Console.Clear();
            DisplayHeader();
            Console.WriteLine("--- Fill in member information -----------");
            Console.WriteLine("");
            if (_message != null)
            {
                Console.WriteLine("{0}", _message);
                Console.WriteLine("");
            }
            _memberID = c_uc.GetMemberList().Max(r => r.MemberID) + 1;
            _firstName = c_uc.GetStringInput("First name: ", true);
            _lastName = c_uc.GetStringInput("Last name: ", true);
            _ssn = c_uc.GetStringInput("Social Security Number (10 digits): ", false);

            _message = null;

            DisplaySaveMemberConfirmation();
        }

        public void DisplaySaveMemberConfirmation()
        {
            Console.WriteLine("");
            Console.WriteLine("__________________________________________");
            Console.WriteLine("--- Please confirm to save the member ----");
            Console.WriteLine("");
            Console.WriteLine("Press [1] - Finish member registration");
            Console.WriteLine("Press [2] - Add boat to member (Member gets registred)");
            Console.WriteLine("Press [3] - Redo registration");
            Console.WriteLine("Press [ESC] - Cancel registration");

            do
            {
                var input = Console.ReadKey(true);
                
                switch (input.Key)
                {
                    case ConsoleKey.D1:
                        c_uc.RegisterMember(_memberID, _firstName, _lastName, _ssn);
                        _message = "Member registered succesfully!";
                        DisplayStartMenu();
                        break;
                    case ConsoleKey.D2:
                        c_uc.RegisterMember(_memberID, _firstName, _lastName, _ssn);
                        _message = "Member registered succesfully!";
                        DisplayBoatRegistration(c_uc.GetMemberByID(_memberID), true);
                        break;
                    case ConsoleKey.D3:
                        _message = "Please, try again...";
                        DisplayMemberRegistrationForm();
                        break;
                    case ConsoleKey.Escape:
                        _message = "The member did not get saved...";
                        DisplayStartMenu();
                        break;
                }
            } while (true);
        }

        public void DisplayBoatRegistration(model.Member member, bool fromMemberRegistration)
        {
            Console.Clear();
            DisplayHeader();
            DisplayMemberInfo(member);

            Console.WriteLine("--- Fill in boat information -------------");
            Console.WriteLine("");
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

                    DisplayBoatRegistration(member, fromMemberRegistration);
                }

                Console.WriteLine("Boat Type: {0}", _boatType);
                _boatLength = c_uc.GetIntInput("Boat Lenght (cm): ");

                DisplaySaveBoatConfirmation(member, fromMemberRegistration);
            } while (true);
        }

        public void DisplaySaveBoatConfirmation(model.Member member, bool fromMemberRegistration)
        {
            Console.WriteLine("");
            Console.WriteLine("Please confirm to register the boat.");
            Console.WriteLine("============================================");
            Console.WriteLine("Press [1] - Finish boat registration");
            Console.WriteLine("Press [2] - Add another boat to member");
            Console.WriteLine("Press [3] - Redo the boat registration");
            Console.WriteLine("Press [ESC] - Cancel boat registration");

            do
            {
                var input = Console.ReadKey(true);

                switch (input.Key)
                {
                    case ConsoleKey.D1:
                        c_uc.RegisterBoat(member.MemberID, _boatType, _boatLength);
                        if (fromMemberRegistration)
                        {
                            _message = "Member and boat registered succesfully.";
                            DisplayStartMenu();
                        }
                        else
                        {
                            Console.Clear();
                            _message = "Boat registered succesfully.";
                            DisplayMemberInfo(member);
                            DisplayMemberInfoMenu(member);
                        }
                        
                        break;
                    case ConsoleKey.D2:
                        c_uc.RegisterBoat(member.MemberID, _boatType, _boatLength);
                        _message = "Boat added succesfully...";
                        _boatType = null;
                        DisplayBoatRegistration(member, fromMemberRegistration);
                        break;
                    case ConsoleKey.D3:
                        Console.WriteLine("Please try again...");
                        Console.WriteLine("");
                        _boatType = null;
                        DisplayBoatRegistration(member, fromMemberRegistration);
                        break;
                    case ConsoleKey.Escape:
                        if (fromMemberRegistration)
                        {
                            _message = "Created a member with no registered boats...";
                            DisplayStartMenu();
                        }
                        else
                        {
                            Console.Clear();
                            _message = "The last boat did not register.";
                            DisplayMemberInfo(member);
                            DisplayMemberInfoMenu(member);
                        }
                        break;
                }
            } while (true);
        }

        public void DisplayMemberList()
        {
            if (compactList)
            {
                listType = vlist;
                Console.WriteLine("{0,4} {1,-26} {2,-16} {3}", "ID", "Name", "Personal Number", "Boats");
                Console.WriteLine("--- ID ---- Name ---------------------");
                foreach (model.Member member in c_uc.GetMemberList())
                {
                    Console.WriteLine("{0,10}\t{1}, {2}", member.MemberID, member.LastName, member.FirstName);
                }
            }
            else
            {
                listType = clist;
                foreach (model.Member member in c_uc.GetMemberList())
                {
                    Console.WriteLine("..........................................");
                    Console.WriteLine("   ID: {0}", member.MemberID);
                    Console.WriteLine("   Name: {0} {1}", member.FirstName, member.LastName);
                    Console.WriteLine("   Social Security Number: {0}", member.SSN);
                    foreach (model.Boat boat in member.Boats)
                    {
                        Console.WriteLine("Boat: {0} {1}", boat.BoatType, boat.Length);
                    }
                }
            }
            Console.WriteLine("__________________________________________");
            Console.WriteLine("");
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
                DisplayHeader();
                DisplayMemberList();
                while (!Console.KeyAvailable)
                {
                    _memberID = c_uc.GetIntInput("Enter ID to view a member: ");
                    if (c_uc.GetMemberByID(_memberID) != null)
                    {
                        Console.Clear();
                        DisplayMemberInfo(c_uc.GetMemberByID(_memberID));
                        DisplayMemberInfoMenu(c_uc.GetMemberByID(_memberID));
                    }
                    else
                    {
                        break;
                    }
                }
                Console.Clear();
                DisplayHeader();
                DisplayMemberList();
                Console.WriteLine("The Member ID [{0}] did not exist.\n", _memberID);
                Console.WriteLine("Press [ESC] to go back or [Enter] to try again.");
                Console.WriteLine("");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
            DisplayStartMenu();
        }

        public void DisplayMemberInfo(model.Member member)
        {
            Console.Clear();
            DisplayHeader();
            Console.WriteLine("ID: {0}", member.MemberID);
            Console.WriteLine("Name: {0} {1}", member.FirstName, member.LastName);
            Console.WriteLine("Social Security Number: {0}", member.SSN);
            Console.WriteLine("");
            if (member.Boats.Count() > 0)
            {
                int i = 1;
                foreach (model.Boat boat in member.Boats)
                {
                    Console.WriteLine("Boat {0}: {1} {2}", i, boat.BoatType, boat.Length);
                    i++;
                }
            }
            else
            {
                Console.WriteLine("No boats registered to member");
            }

            if (_message != null)
            {
                Console.WriteLine("");
                Console.WriteLine("{0}", _message);
            }

            Console.WriteLine("");
            Console.WriteLine("__________________________________________");
            Console.WriteLine("");
        }

        public void DisplayMemberInfoMenu(model.Member member)
        {
            Console.WriteLine("Press [1] - Edit name");
            Console.WriteLine("Press [2] - Edit social security number");
            Console.WriteLine("Press [3] - Register new boat to member");
            Console.WriteLine("Press [4] - Edit boat from list");
            Console.WriteLine("Press [5] - Delete boat from list");
            Console.WriteLine("Press [6] - Delete member");
            Console.WriteLine("Press [Esc] - Back to main menu");
            Console.WriteLine("");
            
            _message = null;
            _boatType = null;

            do
            {
                var input = Console.ReadKey(true);

                switch (input.Key)
                {
                    case ConsoleKey.D1:
                        Console.Clear();
                        DisplayMemberInfo(member);
                        _firstName = c_uc.GetStringInput("Set new first name: ", true);
                        _lastName = c_uc.GetStringInput("Set new last name: ", true);
                        c_uc.EditMemberInfo(member, _firstName, _lastName, member.SSN);
                        _message = "Name update succesfully!";
                        Console.Clear();
                        DisplayMemberInfo(member);
                        DisplayMemberInfoMenu(member);
                        break;
                    case ConsoleKey.D2:
                        Console.Clear();
                        DisplayMemberInfo(member);
                        _ssn = c_uc.GetStringInput("Set new Social Security Number (10 digits): ", false);
                        c_uc.EditMemberInfo(member, member.FirstName, member.LastName, _ssn);
                        _message = "Social security number update succesfully!";
                        Console.Clear();
                        DisplayMemberInfo(member);
                        DisplayMemberInfoMenu(member);
                        break;
                    case ConsoleKey.D3:
                        DisplayBoatRegistration(member, false);
                        break;
                    case ConsoleKey.D4:
                        Console.Clear();
                        DisplayMemberBoatList(member, false);
                        break;
                    case ConsoleKey.D5:
                        Console.Clear();
                        DisplayMemberBoatList(member, true);
                        break;
                    case ConsoleKey.D6:
                        Console.Clear();
                        DisplayDeleteConfirmation(member.MemberID);
                        break;
                    case ConsoleKey.Escape:
                        DisplayStartMenu();
                        break;
                }
            } while (true);
        }

        public void DisplayMemberBoatList(model.Member member, bool isDeleteBoat) 
        {
            Console.Clear();
            DisplayMemberInfo(member);
            
            do
            {
                
                    if (member.Boats.Count() <= 0)
                    {
                        Console.WriteLine("The member has no boats.");
                        Console.WriteLine("Press [ESC] to go back.");
                        
                    }
                    else
                    {
                        while (!Console.KeyAvailable)
                        {
                            if (_message != null)
                            {
                                Console.WriteLine("{0}", _message);
                                _message = null;
                            }
                            
                            int selectedBoat = c_uc.GetIntInput("Enter boat in list: ");

                            if (selectedBoat > 0 && selectedBoat <= member.Boats.Count())
                            {
                                if (isDeleteBoat)
                                {
                                    c_uc.RemoveBoatFromList(member, selectedBoat);
                                    _message = "The boat deleted successfully.";
                                    DisplayMemberInfo(member);
                                    DisplayMemberInfoMenu(member);
                                }
                                else
                                {
                                    DisplayEditBoatInfo(member, selectedBoat);
                                }   
                            }
                            else
                            {
                               _message = "You need to select boat from list.";
                            }
                        }
                    }
                
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
            DisplayMemberInfo(member);
            DisplayMemberInfoMenu(member);
        }

        public void DisplayEditBoatInfo(model.Member member, int selectedBoatToEdit)
        {
            Console.WriteLine("--- Fill in new boat information ---------");
            Console.WriteLine("");
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

                    DisplayEditBoatInfo(member, selectedBoatToEdit);
                }

                Console.WriteLine("Boat Type: {0}", _boatType);
                _boatLength = c_uc.GetIntInput("Boat Lenght (cm): ");

                c_uc.EditBoatInfo(member, selectedBoatToEdit - 1, _boatType, _boatLength);
                _message = "Boat updated succesfully!";
                DisplayMemberInfo(member);
                DisplayMemberInfoMenu(member);

            } while (true);
        }

        public void DisplayDeleteMemberInList()
        {
            do
            {
                Console.Clear();
                DisplayHeader();
                DisplayMemberList();
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
                DisplayHeader();
                DisplayMemberList();
                Console.WriteLine("The Member ID [{0}] did not exist.", _memberID);
                Console.WriteLine("Press [ESC] to go back or [Enter] to try again.");
                Console.WriteLine("");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
            DisplayStartMenu();
        }

        public void DisplayDeleteConfirmation(int memberID)
        {
            Console.Clear();
            DisplayHeader();
            Console.WriteLine("Please confirm to delete the member:\n");
            Console.WriteLine("{0} {1} {2} {3}\n",
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
                    var input = Console.ReadKey(true);
                    //Console.Clear();   

                    switch (input.Key)
                    {
                        case ConsoleKey.Y:
                            Console.Clear();
                            c_uc.RemoveMemberFromList(memberID);
                            _message = "Deleted member succesfully.";
                            DisplayStartMenu();
                            break;
                        case ConsoleKey.N:
                            Console.Clear();
                            _message = "The Member did not get deleted...";
                            DisplayStartMenu();            
                            break;
                    }
                }        
            } while (true);
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
