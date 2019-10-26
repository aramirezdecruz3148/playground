using System;
using System.IO;

namespace c_
{
  class Program
  {
    static void Main(string[] args)
    {
     using(StreamWriter writetext = new StreamWriter("write.txt", true))
{
    writetext.WriteLine("writing in text file");
}
    }
  }
}
