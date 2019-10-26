using System;

namespace c_
{
  class Program
  {
    static void Main(string[] args)
    {
      String Username, Password = string.Empty;
      Int32 Balance = 0;
      Console.WriteLine("Welcome to de Cruz Bank");
      Console.WriteLine("Please create an account");
      Console.WriteLine("Enter a username");
      Username = Console.ReadLine();
      Console.WriteLine("Enter a password");
      Password = Console.ReadLine();
      Console.WriteLine("Thank you " + Username + " your account was created.");
      Console.WriteLine("Press < enter > to continue...");
      Console.ReadLine();
      Console.Clear();
      Console.WriteLine("To sign-in and begin banking please enter your username:");
      var usernameInput = Console.ReadLine();
      if (usernameInput == Username)
      {
        Console.WriteLine("Please enter your password");
        var passwordInput = Console.ReadLine();
        if (passwordInput == Password)
        {
          Console.WriteLine("Welcome " + Username + " you are logged in.");
          Console.WriteLine("To make an initial deposit, please enter the amount");
          var deposit = Console.ReadLine();
          var actualDeposit = Int32.Parse(deposit);
          var newBalance = actualDeposit + Balance;
          Console.WriteLine("Thank you for your deposit, you now have " + newBalance + " in your account");
        }
      }
      else
      {
        Console.WriteLine("I am sorry that was incorrect, press enter to try again");
        var test = Console.ReadKey();
      }
    }
  }
}
