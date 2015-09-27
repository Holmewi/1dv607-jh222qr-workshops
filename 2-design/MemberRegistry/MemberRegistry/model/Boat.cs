using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberRegistry.model
{
    class Boat
    {
        public enum BoatType {
            Sailboat,
            Motorsailer,
            KayakCanoe,
            Other
        }

        private double m_lenght;
        private BoatType m_boatType;

        public Boat(BoatType a_boatType, double a_length) {
            m_boatType = a_boatType;
            m_lenght = a_length;
        }

        public BoatType GetBoatType()
        {
            return m_boatType;
        }

        public void SetBoatType(BoatType value)
        {
            m_boatType = value;
        }


    }
}
