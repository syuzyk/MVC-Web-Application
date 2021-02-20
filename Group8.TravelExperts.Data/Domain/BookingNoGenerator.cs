using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group8.TravelExperts.Data.Domain
{
    public class BookingNoGenerator
    {
        public static string GenerateBookingNo()
        {
            StringBuilder str_bdr = new StringBuilder();
            Random random = new Random();

            char letter;
            double flt = random.NextDouble();

            for (int i = 0; i < 3; i++)
            {
                int shift = Convert.ToInt32(Math.Floor(25 * flt));
                letter = Convert.ToChar(shift + 65);
                str_bdr.Append(letter);
            }

            int threeDigitNumber = Convert.ToInt32(Math.Floor(1000 * flt));

            string bookingNo = str_bdr.ToString() + threeDigitNumber.ToString();

            return bookingNo;
        }
    }
}
