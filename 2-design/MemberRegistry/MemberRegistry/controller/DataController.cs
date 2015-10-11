using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberRegistry.controller
{
    class DataController
    {
        private model.MemberList m_ml;
        private static string path = "../../data/";
        private static string file = "member_registry.txt";

        public DataController(model.MemberList a_ml)
        {
            m_ml = a_ml;
        }

        public void UpdateDataStorage()
        {
            File.WriteAllText(path + file, null);

            using (StreamWriter sw = new StreamWriter(path + file, true))
            {
                foreach (model.Member member in m_ml.Members)
                {
                    sw.WriteLine(member.MemberID);
                    sw.WriteLine(member.LastName);
                    sw.WriteLine(member.FirstName);
                    sw.WriteLine(member.SSN);
                    sw.WriteLine(member.Boats.Count());
                    if (member.Boats.Count() > 0)
                    {
                        foreach (model.Boat boat in member.Boats)
                        {
                            sw.WriteLine(boat.BoatType);
                            sw.WriteLine(boat.Length);
                        }
                    }
                    sw.WriteLine("---");
                } 
            }
        }

        public void UpdateMemberList()
        {
            using (StreamReader sr = new StreamReader(path + file))
            {
                while (!sr.EndOfStream)
                {
                    int memberID = Int32.Parse(sr.ReadLine());
                    string lastName = sr.ReadLine();
                    string firstName = sr.ReadLine();
                    string SSN = sr.ReadLine();
                    int boats = Int32.Parse(sr.ReadLine());

                    m_ml.CreateMember(memberID, firstName, lastName, SSN);

                    for (int i = 0; i < boats; i++)
                    {
                        string boatType = sr.ReadLine();
                        int length = Int32.Parse(sr.ReadLine());
                        m_ml.GetMember(memberID).CreateBoat(memberID, boatType, length);
                    }
                    string line = sr.ReadLine();
                }
            }
        }
    }
}
