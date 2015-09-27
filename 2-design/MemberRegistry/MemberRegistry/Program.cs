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
            view.MemberListView mlv = new view.MemberListView(uc);
            view.CreateMemberView cmv = new view.CreateMemberView(uc);
            view.StartView sv = new view.StartView(uc);

            uc.DoControl(sv, cmv, mlv);
        }
    }
}
