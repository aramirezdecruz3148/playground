using System;
using System.IO;
using System.Xml;
using System.Text;

namespace c_ {
 
  public class User {
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
      var enteredUsername = Console.ReadLine();
      Console.WriteLine("Enter a nickname we can refer to you by: ");
      var enteredNickName = Console.ReadLine();
      Console.WriteLine("Enter a 4 digit pin number: ");
      var enteredPinNumber = Console.ReadLine();
      CreateDatabase();
      AddUserToDatabase(enteredUsername, enteredNickName, enteredPinNumber);
      Console.Clear();
    }

    public void SigninUser() {
      Console.WriteLine("Welcome to deCruz Bank, to sign-in, please enter your username: ");
      var enteredName = Console.ReadLine();
      Console.WriteLine("Please enter your pin number: ");
      var enteredPin = Console.ReadLine();
      Console.Clear();
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
      var balanceList = baseInfo.GetElementsByTagName("Balance");
      if(balanceList.Count == 0) {
        Console.WriteLine("Your balance is $0, why not add some cash?");
      } else {
      var lastBalance = balanceList[balanceList.Count - 1];
        Console.WriteLine("Your balance is ${0}", lastBalance.InnerText);
      }
      database.Close();
    }

    public void Deposit() {
      Console.WriteLine("Please enter the amount you wish to deposit: ");
      var deposit = Decimal.Parse(Console.ReadLine());
      XmlDocument baseInfo = new XmlDocument();
      FileStream database = new FileStream(@"c:\BankDatabase.xml", FileMode.Open);
      baseInfo.Load(database);
      var list = baseInfo.GetElementsByTagName("User");
      var balanceList = baseInfo.GetElementsByTagName("Balance");
      if(balanceList.Count == 0) {
        XmlElement transaction = baseInfo.CreateElement("Transaction");
        transaction.SetAttribute("type", "deposited");
        XmlElement amount = baseInfo.CreateElement("Amount");
        XmlText amountText = baseInfo.CreateTextNode(deposit.ToString());
        XmlElement balance = baseInfo.CreateElement("Balance");
        XmlText balanceText = baseInfo.CreateTextNode(deposit.ToString());
        amount.AppendChild(amountText);
        balance.AppendChild(balanceText);
        transaction.AppendChild(amount);
        transaction.AppendChild(balance);
        list[0].AppendChild(transaction);
        baseInfo.Save(@"c:\BankDatabase.xml");
        Console.WriteLine("Thank you! After your deposit you have ${0} in your account", deposit);
      } else {
          var lastBalance = decimal.Parse(balanceList[balanceList.Count - 1].InnerText);
          var newBalance = lastBalance + deposit;
          XmlElement transaction = baseInfo.CreateElement("Transaction");
          transaction.SetAttribute("type", "deposited");
          XmlElement amount = baseInfo.CreateElement("Amount");
          XmlText amountText = baseInfo.CreateTextNode(deposit.ToString());
          XmlElement balance = baseInfo.CreateElement("Balance");
          XmlText balanceText = baseInfo.CreateTextNode(newBalance.ToString());
          amount.AppendChild(amountText);
          balance.AppendChild(balanceText);
          transaction.AppendChild(amount);
          transaction.AppendChild(balance);
          list[0].AppendChild(transaction);
          baseInfo.Save(@"c:\BankDatabase.xml");
          Console.WriteLine("Thank you! After your deposit you have ${0} in your account", newBalance);
        }
      database.Close();
    }
    public void Withdrawl() {
      Console.WriteLine("Please enter the amount you would like to withdraw: ");
      var withdrawl = Decimal.Parse(Console.ReadLine());
      XmlDocument baseInfo = new XmlDocument();
      FileStream database = new FileStream(@"c:\BankDatabase.xml", FileMode.Open);
      baseInfo.Load(database);
      var list = baseInfo.GetElementsByTagName("User");
      var balanceList = baseInfo.GetElementsByTagName("Balance");
      var lastBalance = decimal.Parse(balanceList[balanceList.Count - 1].InnerText);
      if(balanceList.Count == 0) {
        Console.WriteLine("I'm sorry, you have insufficient funds for this transaction.");
      } else if(lastBalance < withdrawl) {
        Console.WriteLine("I'm sorry, you have insufficient funds for this transaction.");
      } else {
        var newBalance = lastBalance - withdrawl;
        XmlElement transaction = baseInfo.CreateElement("Transaction");
        transaction.SetAttribute("type", "withdrawl");
        XmlElement amount = baseInfo.CreateElement("Amount");
        XmlText amountText = baseInfo.CreateTextNode(withdrawl.ToString());
        XmlElement balance = baseInfo.CreateElement("Balance");
        XmlText balanceText = baseInfo.CreateTextNode(newBalance.ToString());
        amount.AppendChild(amountText);
        balance.AppendChild(balanceText);
        transaction.AppendChild(amount);
        transaction.AppendChild(balance);
        list[0].AppendChild(transaction);
        baseInfo.Save(@"c:\BankDatabase.xml");
        Console.WriteLine("Thank you! After your withdrawl you have ${0} in your account", newBalance);
      }
      database.Close();
    }
    public void TransactionHistory() {
      Console.WriteLine("Here is your transaction history: ");
      Console.WriteLine("**********************************");
      XmlDocument baseInfo = new XmlDocument();
      FileStream database = new FileStream(@"c:\BankDatabase.xml", FileMode.Open);
      baseInfo.Load(database);
      var transactionList = baseInfo.GetElementsByTagName("Transaction");
      if(transactionList.Count == 0) {
        Console.WriteLine("You have no transactions in our records, why not make some!");
      }
      for(var i = 0; i < transactionList.Count; i++) {
        XmlElement transaction = (XmlElement)baseInfo.GetElementsByTagName("Transaction")[i];
        var transactionType = transaction.GetAttribute("type");
        XmlElement amount = (XmlElement)baseInfo.GetElementsByTagName("Amount")[i];
        XmlElement balance = (XmlElement)baseInfo.GetElementsByTagName("Balance")[i];
        Console.WriteLine("Amount {0}: ${1}", transactionType, amount.InnerText);
        Console.WriteLine("Balance: ${0}", balance.InnerText);
        Console.WriteLine("**********************************");
      }
      database.Close();
    }
    public void SignOut() {
      Console.WriteLine("Thank you for choosing deCruz Bank, we hope to see you soon!");
    }
  }
  public class OptionsMenu {
    User user = new User();
    public void MainMenu() {
      int action = 0;
      while (action != 5) {
      Console.WriteLine("******** You are signed into deCruz Bank ********");
      Console.WriteLine("Please choose from our menu of options...");
      Console.WriteLine("[1] Check balance.");
      Console.WriteLine("[2] Make a deposit.");
      Console.WriteLine("[3] Make a withdrawl.");
      Console.WriteLine("[4] View transaction history");
      Console.WriteLine("[5] Sign-out.");
      Console.WriteLine("Enter the number of the option you wish to select: ");
      Console.WriteLine("*************************************************");
      Console.WriteLine("");
      action = Int32.Parse(Console.ReadLine());
      Console.Clear();
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
        user.TransactionHistory();
        Console.WriteLine("");
        break;
        case 5:
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
      Console.WriteLine("[1] Create user account.");
      Console.WriteLine("[2] Sign-in to existing account.");
      Console.WriteLine("Enter the number of the option you wish to select: ");
      Console.WriteLine("*****************************************");
      Console.WriteLine("");
      action = Int32.Parse(Console.ReadLine());
      Console.Clear();
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
      EntryMenu menu = new EntryMenu();
      menu.SignUpIn();
    }
  }
}