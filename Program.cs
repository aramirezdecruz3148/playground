using System;

namespace c_ {
  public class User {
    public string Username { get; set; }
    public int PinNumber { get; set; }
    public decimal InitialBalance { get; set; }
    public void CreateUser() {
      Console.WriteLine("To begin banking please create an account...");
      Console.WriteLine("Enter a username: ");
      Username = Console.ReadLine();
      Console.WriteLine("Enter a 4 digit pin number: ");
      PinNumber = Int32.Parse(Console.ReadLine());
      Console.WriteLine("Enter your initial deposit amount for your new account: ");
      InitialBalance = Decimal.Parse(Console.ReadLine());
      Console.Clear();
    }
    public void SigninUser() {
      Console.WriteLine("Welcome to deCruz Bank, to sign-in, please enter your username: ");
      var enteredName = Console.ReadLine();
      Console.WriteLine("Please enter your pin number: ");
      var enteredPin = Int32.Parse(Console.ReadLine());
      //need to figure out why this statement no longer works
      if(enteredName == Username && enteredPin == PinNumber) {
      Console.WriteLine("Welcome back, {0}", Username);
      } else {
        Console.WriteLine("I'm sorry, either your username or pin number were incorrect.");
        Console.WriteLine("Please try again...");
      }
      Console.Clear();
    }
    public void CheckBalance() {
      Console.WriteLine("The balance for {0}, is ${1}", Username, InitialBalance);
    }
    public void Deposit() {
      Console.WriteLine("Please enter the amount you wish to deposit: ");
      var deposit = Decimal.Parse(Console.ReadLine());
      InitialBalance += deposit;
      Console.WriteLine("Thank you, after your deposit you have ${0} in your account.", InitialBalance);
    }
    public void Withdrawl() {
      Console.WriteLine("Please enter the amount you would like to withdraw: ");
      var withdrawl = Decimal.Parse(Console.ReadLine());
      if(withdrawl > InitialBalance) {
        Console.WriteLine("I'm sorry, you have insufficient funds for that transaction.");
      } else {
        InitialBalance -= withdrawl;
        Console.WriteLine("Thank you, after your withdrawl you have ${0} in your account.", InitialBalance);
      }
    }
    public void SignOut() {
      Console.WriteLine("Thank you for choosing deCruz Bank, we hope to see you soon!");
    }
  }
  public class OptionsMenu {
    User user = new User();
    public void MainMenu() {
      int action = 0;
      while (action != 4) {
      Console.WriteLine("******** You are signed into deCruz Bank ********");
      Console.WriteLine("Please choose from our menu of options...");
      Console.WriteLine("1. Check balance.");
      Console.WriteLine("2. Make a deposit.");
      Console.WriteLine("3. Make a withdrawl.");
      Console.WriteLine("4. Sign-out.");
      Console.WriteLine("Enter the number of the option you wish to select: ");
      Console.WriteLine("*************************************************");
      Console.WriteLine("");
      action = Int32.Parse(Console.ReadLine());
      switch(action) {
        case 1:
        user.CheckBalance();
        Console.WriteLine("");
        break;
        case 2:
        user.Deposit();
        Console.WriteLine("");
        break;
        case 3:
        user.Withdrawl();
        Console.WriteLine("");
        break;
        case 4:
        user.SignOut();
        break;
      }
      }
    }
  }
  public class EntryMenu {
    User user = new User();
    OptionsMenu menu = new OptionsMenu();
    public void SignUpIn() {
      int action = 0;
      while (action != 2) {
      Console.WriteLine("******** Welcome to deCruz Bank! ********");
      Console.WriteLine("Please choose from our menu of options...");
      Console.WriteLine("1. Create user account.");
      Console.WriteLine("2. Sign-in to existing account.");
      Console.WriteLine("Enter the number of the option you wish to select: ");
      Console.WriteLine("*****************************************");
      Console.WriteLine("");
      action = Int32.Parse(Console.ReadLine());
      switch(action) {
        case 1:
        user.CreateUser();
        Console.WriteLine("");
        break;
        case 2:
        user.SigninUser();
        Console.WriteLine("");
        menu.MainMenu();
        break;
      }
      }
    }
  }
  class Program {
    static void Main(string[] args) {
      // User user = new User();
      // user.CreateUser();
      // user.SigninUser();
      // user.CheckBalance();
      // user.Deposit();
      // user.Withdrawl();
      // user.SignOut();
      EntryMenu menu = new EntryMenu();
      menu.SignUpIn();
    }
  }
}
