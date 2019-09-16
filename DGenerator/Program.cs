using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGenerator
{
    class Program
    {
        static void Main(string[] args)
        {

            #region Vendor

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

            #endregion

            #region Catalog
            //--------------------------------------CATALOG----------------------------------
            //var randomizerCode = RandomizerFactory.GetRandomizer(new FieldOptionsIBAN());
            //var random = new Random();

            //string query = $@"INSERT INTO Catalogs 
            //(Discount, BeginingDate, EndDate, Code, VendorId)
            //VALUES
            //";

            //for(int i=0; i<1000; i++)
            //{
            //    string row = "(";

            //    double discount = Math.Round(random.NextDouble() * 20, 2);
            //    row += discount + ",";
            //    DateTime beginDate = new DateTime(random.Next(2018, 2019), random.Next(1, 12), 1);
            //    DateTime endDate = beginDate.AddMonths(random.Next(3, 6));
            //    row += "\'" + beginDate.ToString("MM/dd/yyyy HH:mm:ss") + "\',";
            //    row += "\'" + endDate.ToString("MM/dd/yyyy HH:mm:ss") + "\',";
            //    var code = randomizerCode.Generate().Substring(0, 8);
            //    row += "\'" + code + "\',";
            //    row += random.Next(1, 100);

            //    row += "),\n";
            //    query += row;
            //}

            //Console.Write(query);
            #endregion

            #region Item

            string filePath = @"E:\FAKULTET\MASTER\NAPREDNI WEB\Web3\DB Scripts\Items.csv";
            StringBuilder stringBuilder = new StringBuilder();

            List<string> categoryCodes = new List<string>();
            List<decimal> categoryPrizes = new List<decimal>();

            string[] parts = {"Axle",
"Axle Nuts",
"BCD (Bolt Circle Diameter)",
"Bearing",
"Bonking",
"Bottle Holder",
"Bottom Bracket",
"Brazed Frame",
"Brifter",
"BSO/Bike-Shaped-Object",
"Cable Pull",
"Cable Stretcher",
"Cadence",
"Chain Gauge",
"Chain Guard/Cover",
"Chain Tool",
"Chain Tug",
"Chainstay Length",
"Chainsuck",
"Chamois",
"Clipless Pedals",
"Coaster Brake",
"Crank",
"Derailer Hanger",
"Disc Hub",
"Door Zone",
"Dropout",
"Dunlop Valve",
"Fender",
"Fixed-Gear",
"Flip-Flop Hub",
"Folding Bike",
"Frame",
"Gear Inches",
"Groupset",
"Handlebars",
"Headset",
"Hose Clamp aka Jubilee Clip",
"Hub",
"Hub Skewer",
"Internally-Geared Hub",
"Lawyer lips",
"Luggage Carrier",
"Lugged Frame",
"Master Link",
"Mixte",
"Mountain Bike",
"Pannier",
"Play",
"Power Meter",
"Presta Valve",
"Pump Peg",
"Quick-Release",
"Rim",
"Rim Tape",
"Saddle",
"Saddlebag",
"Schrader Valve",
"Single-speed",
"Skewer",
"Spider",
"Spoke",
"Stem",
"Through",
"Tire Clearance",
"Tire Lever",
"Tire Saver",
"Track Pump",
"Triathalon Bars",
"Welded Frame" };

            Random rnd = new Random();

            for (int i=0; i<40; i++)
            {
                char randomChar1 = (char)rnd.Next('A', 'Z');
                char randomChar2 = (char)rnd.Next('A', 'Z');
                string randomInt = rnd.Next(1000, 9999).ToString();

                if(i<30)
                    categoryPrizes.Add((decimal)rnd.NextDouble() * 20);
                else
                    categoryPrizes.Add((decimal)rnd.NextDouble() * 90);

                string code = "";
                code += randomChar1;
                code += randomChar2;
                code += randomInt;
                categoryCodes.Add(code);

                for (int j = 0; j < 30; j++)
                {
                    for(int k=0; k<500; k++)
                    {
                        if (i == j)
                        {
                            //Id, Price, Name, Code, CategoryCode, CatalogId
                            string record = "";
                            record += (Math.Round(categoryPrizes[i]+(categoryPrizes[i]*(decimal)rnd.NextDouble()/10), 2)).ToString()+",";
                            record += parts[j] + ",";
                            record += categoryCodes[i] + ",";
                            record += (k+1).ToString();
                            stringBuilder.AppendLine(record);

                            Console.WriteLine(record);
                        }
                    }
                }
            }

            File.WriteAllText(filePath, stringBuilder.ToString());

            #endregion

        }
    }
}
