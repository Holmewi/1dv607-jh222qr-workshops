using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberRegistry.view
{
    class StartView
    {
        private controller.UserController c_uc;

        public StartView(controller.UserController a_uc)
        {
            c_uc = a_uc;
        }

        public void DisplayStartMenu()
        {
            Console.Clear();
            Console.WriteLine("Main Menu");
            Console.WriteLine("Press [1] - Add new member.");
            Console.WriteLine("Press [2] - List all members.");
            Console.WriteLine("Press [0] - Quit Application.");

            // TODO: Requirements for future views
            // Menu choices in list view
            // - Compact List / Verbose List (toggle)
            // - Delete member by ID (comfirm)
            // - Read member by ID
            // Menu choices in specific member view
            // - Delete member
            // - Update member information
            // - Register a new boat
            // - Delete boat by number in list (comfirm)
            // - Update boat information
        }

        public void GetMainMenuResponse()
        {
            while (true)
            {
                try
                {
                    int input = Convert.ToInt32(c_uc.GetKeyInput());

                    if (input < 0 || input > 2)
                    {
                        throw new Exception();
                    }

                    switch (input)
                    {
                        case 1:
                            Console.Clear();
                            c_uc.DoCreateMember();
                            break;
                        case 2:
                            Console.Clear();
                            c_uc.DoListMembers();
                            break;
                        case 0:
                            Console.Clear();
                            ExitConfirmation();
                            break;
                    }
                }
                catch (Exception)
                {
                    DisplayStartMenu();
                }
            }
        }

        public void ExitConfirmation()
        {
            Console.WriteLine("Are you sure you want to quit the application?");
            Console.WriteLine("Press [y] - Yes");
            Console.WriteLine("Press [n] - No");

            char input = char.Parse(c_uc.GetKeyInput());
          
            if (input.Equals('y'))
            {
                Environment.Exit(0);
            }
            else if (input.Equals('n'))
            {
                DisplayStartMenu();
            }
            else
            {
                Console.Clear();
                ExitConfirmation();
            }  
        }
    }
}
