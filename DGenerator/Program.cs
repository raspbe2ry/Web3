using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            //var randomizerFirstName = RandomizerFactory.GetRandomizer(new FieldOptionsTextWords());
            //var randomizerCity = RandomizerFactory.GetRandomizer(new FieldOptionsCity());
            //var randomizerEmail = RandomizerFactory.GetRandomizer(new FieldOptionsEmailAddress());
            //var randomizerCode = RandomizerFactory.GetRandomizer(new FieldOptionsIBAN());
            //Random random = new Random();

            //string query = $@"INSERT INTO Vendors 
            //(Name, Address, City, Zip, Telephone, Email, Code, CountryId)
            //VALUES
            //";
            //for(int i =0; i<100; i++)
            //{
            //    var row = "(";

            //    var vendorName = randomizerFirstName.Generate().Split(' ').First().ToUpper();
            //    row += "\'" + vendorName + "\', ";
            //    var address = randomizerFirstName.Generate().Split(' ').First().ToUpper() + " "+random.Next(1, 100).ToString();
            //    row += "\'" + address + "\',";
            //    var city = randomizerCity.Generate();
            //    row += "\'" + city + "\',";
            //    var zip = random.Next(5000, 12000).ToString();
            //    row += "\'" + zip + "\',";
            //    var telephone = random.Next(1000, 9999).ToString() + random.Next(10000, 99999).ToString();
            //    row += "\'" + telephone + "\',";
            //    var email = randomizerEmail.Generate();
            //    row += "\'" + email + "\',";
            //    var code = randomizerCode.Generate().Substring(0, 8);
            //    row += "\'" + code + "\',";
            //    var country = random.Next(1, 249);
            //    row += country;

            //    row += "),\n";
            //    query += row;
            //}

            //Console.Write(query);

            var randomizerCode = RandomizerFactory.GetRandomizer(new FieldOptionsIBAN());
            var random = new Random();

            string query = $@"INSERT INTO Catalogs 
            (Discount, BeginingDate, EndDate, Code, VendorId)
            VALUES
            ";

            for(int i=0; i<1000; i++)
            {
                string row = "(";

                double discount = Math.Round(random.NextDouble() * 20, 2);
                row += discount + ",";
                DateTime beginDate = new DateTime(random.Next(2018, 2019), random.Next(1, 12), 1);
                DateTime endDate = beginDate.AddMonths(random.Next(3, 6));
                row += "\'" + beginDate.ToString("MM/dd/yyyy HH:mm:ss") + "\',";
                row += "\'" + endDate.ToString("MM/dd/yyyy HH:mm:ss") + "\',";
                var code = randomizerCode.Generate().Substring(0, 8);
                row += "\'" + code + "\',";
                row += random.Next(1, 100);

                row += "),\n";
                query += row;
            }

            Console.Write(query);
        }
    }
}
