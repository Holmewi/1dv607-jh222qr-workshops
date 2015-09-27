using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberRegistry.view
{
    class CreateMemberView
    {
        private controller.UserController c_uc;

        string _firstName;
        private string _lastName;
        private string _ssn;
        
        public CreateMemberView(controller.UserController a_uc) {
            c_uc = a_uc;
        }

        public void DisplayMemberRegistration()
        {
            Console.WriteLine("Add new member");
            Console.WriteLine("Press [1] - Start the registration.");
            Console.WriteLine("Press [0] - Back to main menu.");
        }

        public void DisplayRegistrationForm()
        {
            Console.Clear();
            Console.WriteLine("Fill in member information.");
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
            Console.WriteLine("Member: {0} {1} {2} ", _firstName, _lastName, _ssn);
            Console.WriteLine("Press [y] - Save member");
            Console.WriteLine("Press [n] - Cancel");

            char input = char.Parse(c_uc.GetKeyInput());

            if (input.Equals('y'))
            {
                Console.Clear();
                c_uc.CreateMember(_firstName, _lastName, _ssn);  
                Console.WriteLine("YOU ADDED A MEMBER!");
                Console.WriteLine("");
                DisplayMemberRegistration();
            }
            else if (input.Equals('n'))
            {
                Console.Clear();
                Console.WriteLine("The Member did not get saved...");
                Console.WriteLine("");
                DisplayMemberRegistration();
            }
            else
            {
                Console.Clear();
                DisplayConfirmation();
            }  
        }

        public void GetRegistrationResponse()
        {
            while (true)
            {
                try
                {
                    int input = Convert.ToInt32(c_uc.GetKeyInput());

                    if (input < 0 || input > 1)
                    {
                        throw new Exception();
                    }

                    switch (input)
                    {
                        case 1:
                            DisplayRegistrationForm();
                            break;
                        case 0:
                            c_uc.DoDisplayStart();
                            break;
                    }
                }
                catch
                {
                    Console.Clear();
                    DisplayMemberRegistration();
                }
            }
        }
    }
}
