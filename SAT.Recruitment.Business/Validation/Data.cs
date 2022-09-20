using SAT.Recruitment.Data.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SAT.Recruitment.Business.Validation
{
    public static class Data
    {
        public static List<User> GetUsersFromFile()
        {          
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";
            char[] delimiters = new char[] { ',' };
            var usersInFile = File.ReadLines(path)
                .Select(line => {
                    string[] userLine = line.Split(delimiters);
                    return (userLine.Length == 6)
                ? new User() { Name = userLine[0], Email = userLine[1], Money = decimal.Parse(userLine[5]), 
                    Address = userLine[3], Phone = userLine[2], UserType = userLine[4] 
                }
                : null;              // null for ill-formatted line
                })
                .Where(x => x != null)      // filter to good lines
                .ToList();

            return usersInFile;
        }

        public static void WriteFileData(User user)
        {
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";
            StringBuilder sbUser = new StringBuilder();
            sbUser.AppendFormat("{0},{1},{2},{3},{4},{5}",
                user.Name, user.Email, user.Phone, user.Address, user.UserType, user.Money);

            using (StreamWriter write = File.AppendText(path))
            {
                write.WriteLine(sbUser.ToString());
            }

        }

        public static decimal ValidateUserSalary(string userType, decimal money)
        {
            decimal gif = 0;

            switch (userType)
            {
                case "Normal":
                    if (money >= 100)
                    {
                        var percentage = Convert.ToDecimal(0.12);
                        //If new user is normal and has more than USD100
                        gif = money * 0.12M;
                    }
                    else
                    {
                        if (money < 100)
                        {
                            if (money > 10)
                            {
                                var percentage = Convert.ToDecimal(0.8);
                                gif = money * 0.8M;
                            }
                        }
                    }
                    break;
                case "SuperUser":
                    if (money > 100)
                    {
                        var percentage = Convert.ToDecimal(0.20);
                        gif = money * 0.20M;
                    }
                    break;
                case "Premium":
                    if (money > 100)
                    {
                        gif = money * 2;

                    }
                    break;
                default:
                    break;
            }

            return money + gif;
        }

        public static string NormalizeEmail(string email)
        {
            var aux = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            return string.Join("@", new string[] { aux[0], aux[1] });
        }

    }
}
