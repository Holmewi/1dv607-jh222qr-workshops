using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberRegistry.view
{
    class MemberView
    {
        private controller.UserController c_uc;
        private view.BoatView v_bv;

        string _firstName;
        private string _lastName;
        private string _ssn;
        private model.Member _member;
        private string _boatType;
        private int _boatLength;

        public enum BoatType
        {
            Sailboat = 1,
            Motorsailer = 2,
            KayakCanoe = 3,
            Other = 4
        }

        public MemberView(controller.UserController a_uc, view.BoatView a_bv)
        {
            c_uc = a_uc;
            v_bv = a_bv;
        }

        public void DisplayMemberRegistration()
        {
            Console.WriteLine("Add new member");
            Console.WriteLine("Press [1] - Start the registration.");
            Console.WriteLine("Press [Esc] - Back to main menu.");

            do
            {
                var input = Console.ReadKey();
                Console.Clear();

                switch (input.Key)
                {
                    case ConsoleKey.D1:
                        DisplayMemberRegistrationForm();
                        break;
                    case ConsoleKey.Escape:
                        c_uc.DoDisplayStart();
                        break;
                }

            } while (true);
        }

        public void DisplayMemberRegistrationForm()
        {
            Console.WriteLine("Fill in member information.");
            Console.WriteLine("============================================");

            _firstName = c_uc.GetStringInput("First name: ");
            _lastName = c_uc.GetStringInput("Last name: ");
            _ssn = c_uc.GetStringInput("Social Security Number (10 numbers): ");

            Console.WriteLine("");

            DisplaySaveMemberConfirmation();    
        }

        

        public void DisplaySaveMemberConfirmation()
        {
            Console.Clear();
            Console.WriteLine("Please confirm to save the member.");
            Console.WriteLine("============================================");
            Console.WriteLine("Member: {0} {1} {2} ", _firstName, _lastName, _ssn);
            Console.WriteLine("");
            Console.WriteLine("Press [y] - Save member");
            Console.WriteLine("Press [n] - Cancel");

            do
            {
                var input = Console.ReadKey();
                Console.Clear();

                switch (input.Key)
                {
                    case ConsoleKey.Y:
                        c_uc.CreateMember(_firstName, _lastName, _ssn);  
                        Console.WriteLine("Added member succesfully.");
                        Console.WriteLine("");
                        DisplayBoatRegistration();
                        break;
                    case ConsoleKey.N:
                        Console.WriteLine("The Member did not get saved...");
                        Console.WriteLine("");
                        DisplayMemberRegistration();
                        break;
                }

                DisplaySaveMemberConfirmation();

            } while (true);
        }

        public void DisplayBoatRegistration()
        {
            Console.WriteLine("Do you want to register a boat to the member now?");
            Console.WriteLine("============================================");
            Console.WriteLine("");
            Console.WriteLine("Press [y] - Register boat");
            Console.WriteLine("Press [n] - Finish registration");

            do
            {
                var input = Console.ReadKey();
                Console.Clear();

                switch (input.Key)
                {
                    case ConsoleKey.Y:
                        DisplayBoatRegistrationForm();
                        break;
                    case ConsoleKey.N:
                        Console.WriteLine("The member registration is complete.");
                        Console.WriteLine("");
                        DisplayMemberRegistration();
                        break;
                }

                DisplaySaveMemberConfirmation();

            } while (true);
        }

        public void DisplayBoatRegistrationForm()
        {
            Console.WriteLine("Fill in boat information.");
            Console.WriteLine("============================================");

            //int input = c_uc.GetIntInput("Boat type: ");
            Console.WriteLine("{0} Sailboat:", (int)BoatType.Sailboat);
            Console.WriteLine("{0} Motorsailer:", (int)BoatType.Motorsailer);
            Console.WriteLine("{0} Kayak / Canoe:", (int)BoatType.KayakCanoe);
            Console.WriteLine("{0} Other:", (int)BoatType.Other);

            do
            {
                var input = Console.ReadKey();

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
                _boatLength = c_uc.GetIntInput("Lenght in cm: ");

                Console.WriteLine("");

                DisplaySaveBoatConfirmation();
            } while (true);    
        }

        public void DisplaySaveBoatConfirmation()
        {
            Console.Clear();
            Console.WriteLine("Please confirm to register the boat.");
            Console.WriteLine("============================================");
            Console.WriteLine("Boat lenght: {0} cm", _boatLength);
            Console.WriteLine("Boat type: {0}", _boatType);
            Console.WriteLine("");
            Console.WriteLine("Press [y] - Register boat to member");
            Console.WriteLine("Press [e] - Edit the registration");
            Console.WriteLine("Press [n] - Cancel");

            do
            {
                var input = Console.ReadKey();
                Console.Clear();

                switch (input.Key)
                {
                    case ConsoleKey.Y:
                        //c_uc.CreateMember(_firstName, _lastName, _ssn);
                        Console.WriteLine("Boat registered succesfully.");
                        Console.WriteLine("");
                        DisplayMemberRegistration();
                        break;
                    case ConsoleKey.E:
                        Console.WriteLine("Please try again...");
                        Console.WriteLine("");
                        DisplayBoatRegistrationForm();
                        break;
                    case ConsoleKey.N:
                        Console.WriteLine("The boat did not get registered...");
                        Console.WriteLine("");
                        DisplayMemberRegistration();
                        break;
                }

                DisplaySaveBoatConfirmation();

            } while (true);
        }
    }
}
