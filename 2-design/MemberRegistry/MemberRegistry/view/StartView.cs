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
        private view.MemberView v_mv;
        private view.MemberListView v_mlv;

        public StartView(controller.UserController a_uc, view.MemberView a_mv, view.MemberListView a_mlv)
        {
            c_uc = a_uc;
            v_mv = a_mv;
            v_mlv = a_mlv;
        }

        public void DisplayStartMenu()
        {
            Console.Clear();
            Console.WriteLine("Main Menu");
            Console.WriteLine("Press [1] - Add new member.");
            Console.WriteLine("Press [2] - List all members.");
            Console.WriteLine("Press [Esc] - Quit Application.");

            do
            {
                var input = Console.ReadKey();

                Console.Clear();

                switch (input.Key)
                {
                    case ConsoleKey.D1:
                        v_mv.DisplayMemberRegistration();
                        break;
                    case ConsoleKey.D2:
                        v_mlv.DisplayMemberList();
                        v_mlv.DisplayMemberListMenu();
                        break;
                    case ConsoleKey.Escape:
                        ExitConfirmation();
                        break;
                }

                DisplayStartMenu();

            } while (true);
        }

        public void ExitConfirmation()
        {
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
