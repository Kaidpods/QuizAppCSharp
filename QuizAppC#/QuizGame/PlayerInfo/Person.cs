using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizAppC_.QuizGame.PlayerInfo
{
    public class Person
    {
        private String firstName;
        private String surname;
        private DateTime dateOfBirth;
        private Address address;

        public Person(String firstName, String surname)
        {
            this.firstName = firstName;
            this.surname = surname;
        }

        public DateTime getDateOfBirth()
        {
            return dateOfBirth;
        }

        public void setAddress(Address address)
        {
            this.address = address;
        }

        public String getAddress()
        {
            return address.ToString();
        }

        public int getAge()
        {
            /*
            Compares the time between the DOB and today's date, can also be used to get
            other forms of time
             */
            try
            {
                TimeSpan difference = DateTime.Now - dateOfBirth;
                int age = (int)(difference.TotalDays / 365.25);
                return age;
            }
            catch (Exception e)
            {
                Console.WriteLine("There is no DOB so the start date and end date cant be compared");
                return 0;
            }
        }

        public void setDateOfBirth(String dateOfBirth)
        {
        //check if a date of birth string was supplied before trying to split it
        if (!String.IsNullOrEmpty(dateOfBirth)) {
            String[] data = dateOfBirth.Split("-");
            //check that there were three entries
            if (data.Length == 3) {
                    this.dateOfBirth = new DateTime(
                        int.Parse(data[0]),
                        int.Parse(data[1]),
                        int.Parse(data[2]));
            } else {
                throw new Exception("Date of birth supplied in wrong format.");
}
        } else
{
    Console.WriteLine("There is no date of birth");
}
    }

    public String getFirstName()
{
    return firstName;
}

public String getFullName()
{

    return (firstName + " " + surname);
}
    }
}
