using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizAppC_.QuizGame.PlayerInfo
{
    public class Address
    {
        private int houseNumber;
        private String street;
        private String town;
        private String county;
        private String postcode;

        public Address(int houseNumber, String street, String town, String county, String postcode)
        {
            this.houseNumber = houseNumber;
            this.street = street;
            this.town = town;
            this.county = county;
            this.postcode = postcode;
        }
        public override String ToString()
        {
            return houseNumber +
                    " " + street +
                    ",\n" + town +
                    ",\n" + county +
                    ",\n" + postcode +
                    ".";
        }
    }
}
