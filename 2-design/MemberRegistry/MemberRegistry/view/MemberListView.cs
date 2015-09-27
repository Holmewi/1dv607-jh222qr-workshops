using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberRegistry.view
{
    class MemberListView
    {
        private controller.UserController c_uc;

        public MemberListView(controller.UserController a_uc)
        {
            c_uc = a_uc;
        }

        public void DisplayCompactMemberList(model.MemberList m_ml)
        {
            Console.WriteLine("Compact Member List");

            foreach (model.Member member in m_ml.Members)
            {
                Console.WriteLine("{0}", String.Format(member.FirstName));
            }

            Console.WriteLine("End of list");
        }
    }
}
