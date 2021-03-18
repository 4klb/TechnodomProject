using System;
using System.Text.RegularExpressions;
using TechnodomProject.Data;
using TechnodomProject.Models;
using TechnodomProject.Services;

namespace TechnodomProject.UI
{
    public class UserSignInUp
    {
        public User User { get; set; }

        public bool Registration()
        {
            var smsService = new SmsService();

            string phone = IsCorrectPhone();

            int randomCode = 123456;//smsService.SendCode(phone);
            Console.WriteLine("На ваш номер был выслан код");
            Console.Write("Введите код для подверждения: ");

            int inputCode = int.Parse(Console.ReadLine());

            if (randomCode == inputCode)
            {
                using (var userDataAccess = new UserDataAccess())
                    if (userDataAccess.IsPhoneExists(phone))
                    {
                        User = userDataAccess.SelectByPhone(phone);
                        Console.WriteLine($"Welcome, {User.FullName}");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("name: ");
                        User.FullName = Console.ReadLine();
                        Console.WriteLine("email: ");
                        User.Email = IsCorrectEmail();
                        User.Phone = phone;
                        userDataAccess.Insert(User);
                        return true;
                    }
            }
            else
            {
                Console.WriteLine("error");
                return false;
            }
        }

        string IsCorrectPhone()
        {
            string phone;

            string pattern = "[+]{1}[7]{1}[0-9]{3}[0-9]{3}[0-9]{4}";
            while (true)
            {
                Console.WriteLine("Введите номер в формате | +7XXXXXXXXXX  |");
                phone = Console.ReadLine();
                if (Regex.IsMatch(phone, pattern, RegexOptions.IgnoreCase))
                {
                    return phone;
                }
                else
                {
                    Console.WriteLine("Некорректно введен номер");
                }
            }
        }

        string IsCorrectEmail()
        {
            string pattern = "[.\\-_a-z0-9]+@([a-z0-9][\\-a-z0-9]+\\.)+[a-z]{2,6}";

            string email;
            while (true)
            {
                Console.WriteLine("Введите email");
                email = Console.ReadLine();
                if (Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase))
                {
                    return email;
                }
                else
                {
                    Console.WriteLine("Некорректно введен номер");
                }
            }

        }
    }
}
