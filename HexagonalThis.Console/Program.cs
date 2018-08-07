using HexagonalThis.Domain;
using HexagonalThis.Infra;

namespace HexagonalThis.Console
{
  public class Program
  {
    public static void Main(string[] args)
    {
      // 1. Instantiate the right-side adapter(s) ("I want to go outside the hexagon")
      var fileAdapter = new PoetryLibraryFileAdapter(@"C:\Poetry\Rimbaud.txt");

      // 2. Instantiate the hexagon
      var poetryReader = new PoetryReader(fileAdapter);

      // 3. Instantiate the left-side adapter(s) ("I need to enter the hexagon")
      var consoleAdapter = new ConsoleAdapter(poetryReader);

      System.Console.WriteLine("Here is some...");
      consoleAdapter.Ask();

      System.Console.WriteLine("Type enter to exit...");
      System.Console.ReadLine();
    }
  }
}