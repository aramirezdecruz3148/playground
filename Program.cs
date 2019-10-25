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
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("To sign-in and begin banking please enter your username:");
            var usernameInput = Console.ReadLine();
            if (usernameInput == Username) {
                Console.WriteLine("Please enter your password");
                var passwordInput = Console.ReadLine();
                if(passwordInput == Password) {
                    Console.WriteLine("Welcome " + Username + " you are logged in.");
                }
            }
            else {
                Console.WriteLine("I am sorry that was incorrect, press enter to try again");
                var test = Console.ReadKey();
            }
        }
    }
}
