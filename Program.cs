using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Globalization;
using Newtonsoft.Json;
using System.Xml;
using System.Text;

namespace c_ {
 
  public class User {
    public string Username { get; set; }
    public string NickName { get; set; }
    public string PinNumber { get; set; }
    public string Balance { get; set; }
    public string Amount { get; set; }
    public string Type { get; set; }

    public User() { //can probably take this out once ammend all methods
    }
    public void CreateDatabase() {
      XmlTextWriter BankDatabase;
      BankDatabase = new XmlTextWriter(@"c:\BankDatabase.xml", Encoding.UTF8);
      BankDatabase.WriteStartDocument();
      BankDatabase.WriteStartElement("BankDatabase"); 
      BankDatabase.WriteEndElement();
      BankDatabase.Close();
    }

    public void AddUserToDatabase(string clientUsername, string clientNickname, string clientPinNumber) {
      XmlDocument baseInfo = new XmlDocument();
      FileStream database = new FileStream(@"c:\BankDatabase.xml", FileMode.Open);
      baseInfo.Load(database);
      XmlElement user = baseInfo.CreateElement("User");
      user.SetAttribute("username", clientUsername);
      XmlElement userName = baseInfo.CreateElement("Username");
      XmlText userNameText = baseInfo.CreateTextNode(clientUsername);
      XmlElement nickName = baseInfo.CreateElement("NickName");
      XmlText nickNameText = baseInfo.CreateTextNode(clientNickname);
      XmlElement pinNumber = baseInfo.CreateElement("PinNumber");
      XmlText pinNumberText = baseInfo.CreateTextNode(clientPinNumber);
      userName.AppendChild(userNameText);
      nickName.AppendChild(nickNameText);
      pinNumber.AppendChild(pinNumberText);
      user.AppendChild(userName);
      user.AppendChild(nickName);
      user.AppendChild(pinNumber);
      baseInfo.DocumentElement.AppendChild(user);
      database.Close();
      baseInfo.Save(@"c:\BankDatabase.xml");
    }

    public void CreateUser() {
      Console.WriteLine("To begin banking please create an account...");
      Console.WriteLine("Enter a username: ");
      Username = Console.ReadLine();
      Console.WriteLine("Enter a nickname we can refer to you by: ");
      NickName = Console.ReadLine();
      Console.WriteLine("Enter a 4 digit pin number: ");
      PinNumber = Console.ReadLine();
      CreateDatabase();
      AddUserToDatabase(Username, NickName, PinNumber);
      Console.Clear();
    }

    public void SigninUser() {
      Console.WriteLine("Welcome to deCruz Bank, to sign-in, please enter your username: ");
      var enteredName = Console.ReadLine();
      Console.WriteLine("Please enter your pin number: ");
      var enteredPin = Console.ReadLine();
      XmlDocument baseInfo = new XmlDocument();
      FileStream database = new FileStream(@"c:\BankDatabase.xml", FileMode.Open);
      baseInfo.Load(database);
      var list = baseInfo.GetElementsByTagName("User");
      for(var i = 0; i < list.Count; i++) {
        XmlElement user = (XmlElement)baseInfo.GetElementsByTagName("User")[i];
        XmlElement nickName = (XmlElement)baseInfo.GetElementsByTagName("NickName")[i];
        if(user.GetAttribute("username") == enteredName) {
          Console.WriteLine("");
          Console.WriteLine("Welcome back, {0}", nickName.InnerText);
          break;
        } else {
           Console.WriteLine("");
           Console.WriteLine("I'm sorry, your information did not match our records, please try again...");
        }
      }
      database.Close();
    }

    public void CheckBalance() {
      XmlDocument baseInfo = new XmlDocument();
      FileStream database = new FileStream(@"c:\BankDatabase.xml", FileMode.Open);
      baseInfo.Load(database);
      var list = baseInfo.GetElementsByTagName("User");
      for(var i = 0; i < list.Count; i++) {
        XmlElement user = (XmlElement)baseInfo.GetElementsByTagName("User")[i];
        if(user.GetAttribute("username") == "aramirez") {
          var balanceList = baseInfo.GetElementsByTagName("Balance");
          if(balanceList.Count == 0) {
            Console.WriteLine("Your balance is $0, why not add some cash?");
          } else {
          var lastBalance = balanceList[balanceList.Count - 1];
            Console.WriteLine("Your balance is ${0}", lastBalance.InnerText);
          }
          break;
        }
      }
      database.Close();
    }

    public void Deposit() {
      Console.WriteLine("Please enter the amount you wish to deposit: ");
      var deposit = Decimal.Parse(Console.ReadLine());
      Console.Write("done");
    }
    public void Withdrawl() {
      Console.WriteLine("Please enter the amount you would like to withdraw: ");
      var withdrawl = Decimal.Parse(Console.ReadLine());
      // if(withdrawl > InitialBalance) {
      //   Console.WriteLine("I'm sorry, you have insufficient funds for that transaction.");
      // } else {
      //   InitialBalance -= withdrawl;
      //   Console.WriteLine("Thank you, after your withdrawl you have ${0} in your account.", InitialBalance);
      // }
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
      User user = new User();
      user.CheckBalance();
      //creation of the file and the root element
      // XmlTextWriter BankDatabase;
      // BankDatabase = new XmlTextWriter(@"c:\BankDatabase.xml", Encoding.UTF8);
      // BankDatabase.WriteStartDocument();
      // BankDatabase.WriteStartElement("BankDatabase"); 
      // BankDatabase.WriteEndElement();
      // BankDatabase.Close();

      //adding information to the file database
      //I can see making a method that takes the necessary strings
      //then this code is inside*************
      // XmlDocument baseInfo = new XmlDocument();
      // FileStream database = new FileStream(@"c:\BankDatabase.xml", FileMode.Open);
      // baseInfo.Load(database);
      // XmlElement user = baseInfo.CreateElement("User");
      // user.SetAttribute("username", "aramirez");
      // XmlElement userName = baseInfo.CreateElement("Username");
      // XmlText userNameText = baseInfo.CreateTextNode("aramirez");
      // XmlElement nickName = baseInfo.CreateElement("NickName");
      // XmlText nickNameText = baseInfo.CreateTextNode("Alex");
      // XmlElement pinNumber = baseInfo.CreateElement("PinNumber");
      // XmlText pinNumberText = baseInfo.CreateTextNode("1234");
      // userName.AppendChild(userNameText);
      // nickName.AppendChild(nickNameText);
      // pinNumber.AppendChild(pinNumberText);
      // user.AppendChild(userName);
      // user.AppendChild(nickName);
      // user.AppendChild(pinNumber);
      // baseInfo.DocumentElement.AppendChild(user);
      // database.Close();
      // baseInfo.Save(@"c:\BankDatabase.xml");
      
      //adding transactions to the file
      //this could also be its own method
      //first need to read to get the correct element to append******
      // XmlDocument baseInfo = new XmlDocument();
      // FileStream database = new FileStream(@"c:\BankDatabase.xml", FileMode.Open);
      // baseInfo.Load(database);
      // var list = baseInfo.GetElementsByTagName("User");
      // for(var i = 0; i < list.Count; i++) {
      //   XmlElement user = (XmlElement)baseInfo.GetElementsByTagName("User")[i];
      //   if(user.GetAttribute("username") == "aramirez") {
      //     XmlElement transaction = baseInfo.CreateElement("Transaction");
      //     transaction.SetAttribute("type", "deposited");
      //     XmlElement amount = baseInfo.CreateElement("Amount");
      //     XmlText amountText = baseInfo.CreateTextNode("50.00");
      //     XmlElement balance = baseInfo.CreateElement("Balance");
      //     XmlText balanceText = baseInfo.CreateTextNode("100.00");
      //     amount.AppendChild(amountText);
      //     balance.AppendChild(balanceText);
      //     transaction.AppendChild(amount);
      //     transaction.AppendChild(balance);
      //     list[0].AppendChild(transaction);
      //     baseInfo.Save(@"c:\BankDatabase.xml");
      //     break;
      //   }
      // }
      // database.Close();

      //for reading userinfo from the database
      //could be its own method****************
      // XmlDocument baseInfo = new XmlDocument();
      // FileStream database = new FileStream(@"c:\BankDatabase.xml", FileMode.Open);
      // baseInfo.Load(database);
      // var list = baseInfo.GetElementsByTagName("User");
      // for(var i = 0; i < list.Count; i++) {
      //   XmlElement user = (XmlElement)baseInfo.GetElementsByTagName("User")[i];
      //   XmlElement userName = (XmlElement)baseInfo.GetElementsByTagName("Username")[i];
      //   XmlElement nickName = (XmlElement)baseInfo.GetElementsByTagName("NickName")[i];
      //   XmlElement pinNumber = (XmlElement)baseInfo.GetElementsByTagName("PinNumber")[i];
      //   if(user.GetAttribute("username") == "aramirez") {
      //     Console.WriteLine(userName.InnerText);
      //     Console.WriteLine(nickName.InnerText);
      //     Console.WriteLine(pinNumber.InnerText);
      //     break;
      //   }
      // }
      // database.Close();


      //for reading latest transaction info from database
      //could be its own method****************
      // XmlDocument baseInfo = new XmlDocument();
      // FileStream database = new FileStream(@"c:\BankDatabase.xml", FileMode.Open);
      // baseInfo.Load(database);
      // var list = baseInfo.GetElementsByTagName("User");
      // for(var i = 0; i < list.Count; i++) {
      //   XmlElement user = (XmlElement)baseInfo.GetElementsByTagName("User")[i];
      //   if(user.GetAttribute("username") == "aramirez") {
      //     var balanceList = baseInfo.GetElementsByTagName("Balance");
      //     var lastBalance = balanceList[balanceList.Count - 1];
      //     Console.WriteLine(lastBalance.InnerText);
      //     break;
      //   }
      // }
      // database.Close();


      //for reading all transaction info from database
      //could be its own method****************
      // Console.WriteLine("Here is your transaction history: ");
      // Console.WriteLine("**********************************");
      // XmlDocument baseInfo = new XmlDocument();
      // FileStream database = new FileStream(@"c:\BankDatabase.xml", FileMode.Open);
      // baseInfo.Load(database);
      // var list = baseInfo.GetElementsByTagName("User");
      // for(int i = 0; i < list.Count; i++) {
      //   XmlElement user = (XmlElement)baseInfo.GetElementsByTagName("User")[i];
      //   if(user.GetAttribute("username") == "aramirez") {
      //     var transactionList = baseInfo.GetElementsByTagName("Transaction");
      //     if(transactionList.Count == 0) {
      //       Console.WriteLine("You have no transactions in our records, why not make some!");
      //     }
      //     for(var j = 0; j < transactionList.Count; j++) {
      //       XmlElement transaction = (XmlElement)baseInfo.GetElementsByTagName("Transaction")[j];
      //       var transactionType = transaction.GetAttribute("type");
      //       XmlElement amount = (XmlElement)baseInfo.GetElementsByTagName("Amount")[j];
      //       XmlElement balance = (XmlElement)baseInfo.GetElementsByTagName("Balance")[j];
      //       Console.WriteLine("Amount {0}: ${1}", transactionType, amount.InnerText);
      //       Console.WriteLine("Balance: ${0}", balance.InnerText);
      //       Console.WriteLine("**********************************");
      //     }
      //     break;
      //   }
      // }
      // database.Close();
    }
  }
}