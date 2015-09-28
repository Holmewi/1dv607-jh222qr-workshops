using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberRegistry
{
    class Program
    {
        static void Main(string[] args)
        {
            controller.UserController uc = new controller.UserController();
            
            view.MemberView mv = new view.MemberView(uc);
            view.MemberListView mlv = new view.MemberListView(uc);
            view.StartView sv = new view.StartView(uc, mv, mlv);

            uc.DoControl(sv, mv, mlv);

            // TODO: Requirements for future views
            // Menu choices in list view
            //DONE - Compact List / Verbose List (toggle)
            //DONE - Delete member by ID (comfirm)
            //DONE - Read member by ID
            // Menu choices in specific member view
            // - Delete member
            // - Update member information
            // - Register a new boat
            // - Delete boat by number in list (comfirm)
            // - Update boat information
        }
    }
}
