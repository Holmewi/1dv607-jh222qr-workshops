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
        private DateTime today = DateTime.Today;

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
            Console.WriteLine("{0,54}", today.ToString("yyyy-MM-dd"));
            Console.WriteLine("\t{0,-34}", "THE HAPPY PIRATE");
            Console.WriteLine("\t{0,-34}", "- Member & Boat Registry");
            Console.WriteLine("________________________________________________________\n");
        }

        public void DisplayStartMenu()
        {
            Console.Clear();
            DisplayHeader();
            DisplayMemberList();

            if (_message != null)
            {
                Console.WriteLine("\n   * {0}", _message);
            }

            Console.WriteLine("________________________________________________________\n");
            Console.WriteLine("--- Start Menu -----------------------------------------\n");
            Console.WriteLine("   Press [1] - Create New Member");
            Console.WriteLine("   Press [2] - View Member Information");
            Console.WriteLine("   Press [3] - Delete Member");
            Console.WriteLine("   Press [4] - Display {0} Member List", listType); ;
            Console.WriteLine("   Press [Esc] - Quit Application");

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
            Console.WriteLine("------------------ Create New Member -------------------\n");
            Console.WriteLine("\tFill in member information.");
            if (_message != null)
            {
                Console.WriteLine("\n\t   * {0}", _message);
            }
            if (c_uc.GetMemberList().Count() <= 0) {
                _memberID = 1000;
            }
            else {
                _memberID = c_uc.GetMemberList().Max(r => r.MemberID) + 1;
            }

            _firstName = c_uc.GetStringInput("\tFirst name: ", true);
            _lastName = c_uc.GetStringInput("\tLast name: ", true);
            _ssn = c_uc.GetStringInput("\tSocial Security Number: ", false);

            _message = null;

            DisplaySaveMemberConfirmation();
        }

        public void DisplaySaveMemberConfirmation()
        {
            Console.Clear();
            DisplayHeader();

            Console.WriteLine("------------------ Create New Member -------------------\n");
            Console.WriteLine("\tName: {0} {1}\n\tSSN: {2}", _firstName, _lastName, _ssn);
            Console.WriteLine("\n________________________________________________________\n");
            Console.WriteLine("--- Please confirm to save the member ------------------\n");
            Console.WriteLine("   Press [1] - Register member");
            Console.WriteLine("   Press [2] - Register member and add boat");
            Console.WriteLine("   Press [3] - Redo registration");
            Console.WriteLine("   Press [ESC] - Cancel registration");

            do
            {
                var input = Console.ReadKey(true);
                
                switch (input.Key)
                {
                    case ConsoleKey.D1:
                        c_uc.RegisterMember(_memberID, _firstName, _lastName, _ssn);
                        _message = "Member registered successfully!";
                        DisplayStartMenu();
                        break;
                    case ConsoleKey.D2:
                        c_uc.RegisterMember(_memberID, _firstName, _lastName, _ssn);
                        _message = "Member registered successfully!";
                        DisplayBoatRegistration(c_uc.GetMemberByID(_memberID), true);
                        break;
                    case ConsoleKey.D3:
                        _message = "Please, try again...";
                        DisplayMemberRegistrationForm();
                        break;
                    case ConsoleKey.Escape:
                        _message = "The member was not saved";
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
            Console.WriteLine("\n--------------- Register boat to member ----------------\n");
            do
            {
                if (_boatType == null)
                {
                    Console.WriteLine("\tSelect boat type:");
                    Console.WriteLine("\t   Press {0} - Sailboat:", (int)BoatType.Sailboat);
                    Console.WriteLine("\t   Press {0} - Motorsailer:", (int)BoatType.Motorsailer);
                    Console.WriteLine("\t   Press {0} - Kayak / Canoe:", (int)BoatType.KayakCanoe);
                    Console.WriteLine("\t   Press {0} - Other:", (int)BoatType.Other);
                
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

                Console.WriteLine("\tBoat Type: {0}", _boatType);
                _boatLength = c_uc.GetIntInput("\tBoat Lenght (cm): ");
                if (_message != null)
                {
                    Console.WriteLine("\n\t   * {0}", _message);
                }
                DisplaySaveBoatConfirmation(member, fromMemberRegistration);
            } while (true);
        }

        public void DisplaySaveBoatConfirmation(model.Member member, bool fromMemberRegistration)
        {
            Console.WriteLine("\n________________________________________________________\n");
            Console.WriteLine("--- Please confirm to register the boat ----------------\n");
            Console.WriteLine("   Press [1] - Finish boat registration");
            Console.WriteLine("   Press [2] - Add another boat to member");
            Console.WriteLine("   Press [3] - Redo the boat registration");
            Console.WriteLine("   Press [ESC] - Cancel boat registration");

            do
            {
                var input = Console.ReadKey(true);

                switch (input.Key)
                {
                    case ConsoleKey.D1:
                        c_uc.RegisterBoat(member.MemberID, _boatType, _boatLength);
                        if (fromMemberRegistration)
                        {
                            _message = "Member and boat registered successfully!";
                            DisplayStartMenu();
                        }
                        else
                        {
                            Console.Clear();
                            _message = "Boat registered succesfully!";
                            DisplayMemberInfo(member);
                            DisplayMemberInfoMenu(member);
                        }
                        
                        break;
                    case ConsoleKey.D2:
                        c_uc.RegisterBoat(member.MemberID, _boatType, _boatLength);
                        _message = "Boat added succesfully!";
                        _boatType = null;
                        DisplayBoatRegistration(member, fromMemberRegistration);
                        break;
                    case ConsoleKey.D3:
                        _message = "Please try again...";
                        _boatType = null;
                        DisplayBoatRegistration(member, fromMemberRegistration);
                        break;
                    case ConsoleKey.Escape:
                        if (fromMemberRegistration)
                        {
                            _message = "Member registered with no boats.";
                            DisplayStartMenu();
                        }
                        else
                        {
                            Console.Clear();
                            _message = "The boat was not registered.";
                            DisplayMemberInfo(member);
                            DisplayMemberInfoMenu(member);
                        }
                        break;
                }
            } while (true);
        }

        public void DisplayMemberList()
        {
            if (c_uc.GetMemberList().Count() <= 0)
            {
                Console.WriteLine("\tNo members in registry...");
            }
            else
            {
                if (compactList)
                {
                    listType = vlist;
                    Console.WriteLine("--------------------- Compact List ---------------------\n");
                    Console.WriteLine("\t{0,-6} {1,-27} {2}", "ID", "Name", "Boats");
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("\t{0,-6} {1,-27} {2}", "----", "------------------------", "-----");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    foreach (model.Member member in c_uc.GetMemberList())
                    {
                        string fullName = member.LastName + ", " + member.FirstName;
                        Console.WriteLine("\t{0,-6} {1,-27} {2}", member.MemberID, fullName, member.Boats.Count());
                    }
                }
                else
                {
                    listType = clist;
                    Console.WriteLine("--------------------- Verbose List ---------------------\n");
                    foreach (model.Member member in c_uc.GetMemberList())
                    {

                        Console.WriteLine("\t{0,-7} {1}", "ID:", member.MemberID);
                        Console.WriteLine("\t{0,-7} {1} {2}", "Name:", member.FirstName, member.LastName);
                        Console.WriteLine("\t{0,-7} {1}", "SSN:", member.SSN);
                        if (member.Boats.Count() > 0)
                        {
                            int i = 1;
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.WriteLine("\t   __ Registered boats ______________");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            foreach (model.Boat boat in member.Boats)
                            {
                                Console.WriteLine("\t   Boat [{0}] - {1} {2}", i, boat.BoatType, boat.Length);
                                i++;
                            }
                        }
                        
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine("\t------------------------------------------");
                        Console.ForegroundColor = ConsoleColor.Cyan;
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
                DisplayHeader();
                DisplayMemberList();
                while (!Console.KeyAvailable)
                {
                    _memberID = c_uc.GetIntInput("\n\tEnter ID to view a member: ");
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
                Console.WriteLine("\n\tThe Member ID \"{0}\" did not exist.\n", _memberID);
                Console.WriteLine("   Press [ESC] to go back or [Enter] to try again.");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
            DisplayStartMenu();
        }

        public void DisplayMemberInfo(model.Member member)
        {
            Console.Clear();
            DisplayHeader();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\t__ Member info ___________________________");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\t{0,-7} {1}", "ID:", member.MemberID);
            Console.WriteLine("\t{0,-7} {1} {2}", "Name:", member.FirstName, member.LastName);
            Console.WriteLine("\t{0,-7} {1}", "SSN:", member.SSN);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\n\t   __ Registered boats ______________");
            Console.ForegroundColor = ConsoleColor.Cyan;
            if (member.Boats.Count() > 0)
            {
                int i = 1;
                foreach (model.Boat boat in member.Boats)
                {
                    Console.WriteLine("\t   Boat [{0}] - {1} {2}", i, boat.BoatType, boat.Length);
                    i++;
                }
            }
            else
            {
                Console.WriteLine("\t   No boats registered to member");
            }

            if (_message != null)
            {
                Console.WriteLine("\n   * {0}", _message);
            }
        }

        public void DisplayMemberInfoMenu(model.Member member)
        {
            Console.WriteLine("\n________________________________________________________\n");
            Console.WriteLine("--- Member menu ----------------------------------------\n");
            Console.WriteLine("   Press [1] - Edit name");
            Console.WriteLine("   Press [2] - Edit social security number");
            Console.WriteLine("   Press [3] - Register new boat to member");
            Console.WriteLine("   Press [4] - Edit boat from list");
            Console.WriteLine("   Press [5] - Delete boat from list");
            Console.WriteLine("   Press [6] - Delete member");
            Console.WriteLine("   Press [Esc] - Back to main menu");
            
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
                        _firstName = c_uc.GetStringInput("\tSet new first name: ", true);
                        _lastName = c_uc.GetStringInput("\tSet new last name: ", true);
                        c_uc.EditMemberInfo(member, _firstName, _lastName, member.SSN);
                        _message = "Name updated successfully!";
                        Console.Clear();
                        DisplayMemberInfo(member);
                        DisplayMemberInfoMenu(member);
                        break;
                    case ConsoleKey.D2:
                        Console.Clear();
                        DisplayMemberInfo(member);
                        _ssn = c_uc.GetStringInput("\tSet new Social Security Number (10 digits): ", false);
                        c_uc.EditMemberInfo(member, member.FirstName, member.LastName, _ssn);
                        _message = "Social security number updated successfully!";
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
                        Console.WriteLine("\n   The member has no boats. Press [ESC] to go back.");                
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

                            int selectedBoat = c_uc.GetIntInput("\n\tEnter number from list: ");

                            if (selectedBoat > 0 && selectedBoat <= member.Boats.Count())
                            {
                                if (isDeleteBoat)
                                {
                                    c_uc.RemoveBoatFromList(member, selectedBoat);
                                    _message = "The boat was deleted!";
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

        public void DisplayEditBoatInfo(model.Member member, int select)
        {
            Console.Clear();
            DisplayMemberInfo(member);
            Console.WriteLine("\n--- Edit boat information ------------------------------\n");
            Console.WriteLine("\tBoat to edit: {0} {1}\n", member.Boats[select - 1].BoatType, member.Boats[select - 1].Length);
            do
            {
                if (_boatType == null)
                {
                    Console.WriteLine("\tSelect boat type:");
                    Console.WriteLine("\t   Press {0} - Sailboat:", (int)BoatType.Sailboat);
                    Console.WriteLine("\t   Press {0} - Motorsailer:", (int)BoatType.Motorsailer);
                    Console.WriteLine("\t   Press {0} - Kayak / Canoe:", (int)BoatType.KayakCanoe);
                    Console.WriteLine("\t   Press {0} - Other:", (int)BoatType.Other);

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

                    DisplayEditBoatInfo(member, select);
                }

                Console.WriteLine("\tBoat Type: {0}", _boatType);
                _boatLength = c_uc.GetIntInput("\tBoat Lenght (cm): ");

                c_uc.EditBoatInfo(member, select - 1, _boatType, _boatLength);
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
                    _memberID = c_uc.GetIntInput("\n\tEnter ID to delete a member: ");
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
                Console.WriteLine("\n\tThe Member ID \"{0}\" did not exist.", _memberID);
                Console.WriteLine("   Press [ESC] to go back or [Enter] to try again.");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
            DisplayStartMenu();
        }

        public void DisplayDeleteConfirmation(int memberID)
        {
            Console.Clear();
            DisplayHeader();
            Console.WriteLine("   Please confirm to delete the member:\n");
            Console.WriteLine("\t{0,-7} {1}", "ID:", c_uc.GetMemberByID(memberID).MemberID);
            Console.WriteLine("\t{0,-7} {1} {2}", "Name:", c_uc.GetMemberByID(memberID).FirstName, c_uc.GetMemberByID(memberID).LastName);
            Console.WriteLine("\t{0,-7} {1}", "SSN:", c_uc.GetMemberByID(memberID).SSN);
            Console.WriteLine("\n\tPress [y] - Delete member");
            Console.WriteLine("\tPress [n] - Cancel");

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
                            _message = "Deleted member succesfully!";
                            DisplayStartMenu();
                            break;
                        case ConsoleKey.N:
                            Console.Clear();
                            _message = "The Member was not deleted.";
                            DisplayStartMenu();            
                            break;
                    }
                }        
            } while (true);
        }

        public void ExitConfirmation()
        {
            Console.Clear();
            DisplayHeader();
            Console.WriteLine("   Are you sure you want to quit the application?\n");
            Console.WriteLine("\tPress [y] - Yes");
            Console.WriteLine("\tPress [n] - No");

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
