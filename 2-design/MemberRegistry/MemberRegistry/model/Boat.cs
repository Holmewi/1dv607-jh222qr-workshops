using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberRegistry.model
{
    class Boat
    {
        private int _memberID;
        private string _boatType;
        private int _length;

        public Boat(int memberID, string boatType, int length)
        {
            _memberID = memberID;
            _boatType = boatType;
            _length = length;
        }

        public int MemberID
        {
            get { return _memberID; }
            set { _memberID = value; }
        }

        public string BoatType
        {
            get { return _boatType; }
            set { _boatType = value; }
        }
        public int Length
        {
            get { return _length; }
            set { _length = value; }
        }
    }
}
