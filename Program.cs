using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Globalization;
using Newtonsoft.Json;

namespace c_ {
  public struct BankTransaction {
    public int Amount { get; set; }
    public string Type { get; set; }
  }
  
  public struct JSONUserObj {
    public string Username { get; set; }
    public List<BankTransaction> BankTransaction { get; set; }
  }

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
      string pass = "";
      ConsoleKeyInfo enteredPin;
      do {
        enteredPin = Console.ReadKey(true);
        pass += enteredPin.KeyChar;
        Console.Write("*");
      } while (enteredPin.Key != ConsoleKey.Enter);
      //will need to add logic here to ensure username and password match created user
      Console.WriteLine("Welcome back, {0}", Username);
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
    //  JSONUserObj test = new JSONUserObj();
    //  List<BankTransaction> test2 = new List<BankTransaction>();
    //  test2.Add(new BankTransaction { Amount = 4, Type = "Deposit" });
    //  test2.Add(new BankTransaction { Amount = 5, Type = "Withdrawl" });

    //  {
    //    test.Username = "Username";
    //    test.BankTransaction = test2;
    //  }
    //  File.WriteAllText(@"c:\bank.json", JsonConvert.SerializeObject(test));
    //  using (StreamWriter file = File.CreateText(@"c:\bank.json"))
    //   {
    //       JsonSerializer serializer = new JsonSerializer();
    //       serializer.Serialize(file, test);
    //   }
      JSONUserObj test = JsonConvert.DeserializeObject<JSONUserObj>(File.ReadAllText(@"c:\bank.json"));
      using (StreamReader file = File.OpenText(@"c:\bank.json"))
      {
          JsonSerializer serializer = new JsonSerializer();
          JSONUserObj test2 = (JSONUserObj)serializer.Deserialize(file, typeof(JSONUserObj));
          Console.WriteLine(test2.Username);
          for(var i = 0; i < test2.BankTransaction.Count; i++) {
            Console.WriteLine("***********");
            Console.WriteLine("Type: {0}", test2.BankTransaction[i].Type);
            Console.WriteLine("Amount: {0}", test2.BankTransaction[i].Amount);
          }
      }
    }
  }
}
//figure out how to loop through the transactions
//figure out how to structure them so they each have some space and deliniation