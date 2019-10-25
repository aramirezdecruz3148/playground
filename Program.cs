using System;

namespace c_
{
    class Program
    {
        static void Main(string[] args)
        {
            String Username, Password = string.Empty;
            Console.WriteLine("Welcome to de Cruz Bank");
            Console.WriteLine("Please create an account");
            Console.WriteLine("Enter a username");
            Username = Console.ReadLine();
            Console.WriteLine("Enter a password");
            Password = Console.ReadLine();
            Console.WriteLine("Thank you " + Username + " your account was created.");
        }
    }
}
