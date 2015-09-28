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

        string _firstName;
        private string _lastName;
        private string _ssn;
        
        public MemberView(controller.UserController a_uc) {
            c_uc = a_uc;
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
                        DisplayRegistrationForm();
                        break;
                    case ConsoleKey.Escape:
                        c_uc.DoDisplayStart();
                        break;
                }

            } while (true);
        }

        public void DisplayRegistrationForm()
        {
            Console.WriteLine("Fill in member information.");
            Console.WriteLine("============================================");

            _firstName = c_uc.GetStringInput("First name: ");
            _lastName = c_uc.GetStringInput("Last name: ");
            _ssn = c_uc.GetStringInput("Social Security Number (10 numbers): ");

            Console.WriteLine("");

            DisplayConfirmation();    
        }

        public void DisplayConfirmation()
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
                        DisplayMemberRegistration();
                        break;
                    case ConsoleKey.N:
                        Console.WriteLine("The Member did not get saved...");
                        Console.WriteLine("");
                        DisplayMemberRegistration();
                        break;
                }

                DisplayConfirmation();

            } while (true);
        }
    }
}
