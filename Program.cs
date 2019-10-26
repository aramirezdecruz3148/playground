using System;

namespace c_
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Press 1 for deposit, press 2 for withdrawal, 3 for balance: ");
      var action = int.Parse(Console.ReadLine());
      switch (action)
      {
          case 1:
              Console.WriteLine("Case 1");
              break;
          case 2:
              Console.WriteLine("Case 2");
              break;
          case 3:
              Console.WriteLine("Case 3");
              break;
      }
    }
  }
}
