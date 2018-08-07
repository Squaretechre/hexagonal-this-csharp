using HexagonalThis.Domain;
using HexagonalThis.Infra;
using NFluent;
using NSubstitute;
using NUnit.Framework;

namespace HexagonalThis.Tests
{
  // 3 step initialisation
  // 1 - Instantiate the keys to the outside world, "I need to go out" adapters
  // 2 - Instantiate the hexagon
  // 3 - Instantiate key to enter the hexagon, "I need to enter" adapters
  // Only thing console, webapp will reference is left side port
  public class AcceptanceTests
  {
    [Test]
    public void should_give_verses_when_asked_for_poetry()
    {
      // IRequestVerses : left side port
      // PoetryReader : the hexagon
      IRequestVerses poetryReader = new PoetryReader();
      var verses = poetryReader.GiveMeSomePoetry();
      Check.That(verses).IsEqualTo("I want to sleep\r\nSwat the flies\r\nSoftly, please.\r\n\r\n-- Masaoka Shiki (1867 - 1902)");
    }

    [Test]
    public void should_give_verses_when_asked_for_poetry_with_the_support_of_a_poetry_library()
    {
      IObtainPoems poetryLibrary = Substitute.For<IObtainPoems>();
      poetryLibrary.GetMeAPoem().Returns("If you could read a leaf or tree\r\nyou'd have no need of books.\r\n-- Alistair Cockburn (1987)");

      var poetryReader = new PoetryReader(poetryLibrary);
      var verse = poetryReader.GiveMeSomePoetry();

      Check.That(verse).IsEqualTo("If you could read a leaf or tree\r\nyou'd have no need of books.\r\n-- Alistair Cockburn (1987)");
    }

    [Test]
    public void should_provide_verses_when_asked_for_poetry_with_the_support_of_a_console_adapter()
    {
      // 1. Instantiate the right-side adapter(s) ("I want to go outside the hexagon")
      IObtainPoems poetryLibrary = Substitute.For<IObtainPoems>();
      poetryLibrary.GetMeAPoem().Returns("If you could read a leaf or tree\r\nyou'd have no need of books.\r\n-- Alistair Cockburn (1987)");

      // 2. Instantiate the hexagon
      var poetryReader = new PoetryReader(poetryLibrary);

      IWriteLines publicationStrategy = Substitute.For<IWriteLines>();

      // 3. Instantiate the left-side adapter(s) ("I need to enter the hexagon")
      var consoleAdapter = new ConsoleAdapter(poetryReader, publicationStrategy);

      consoleAdapter.Ask();

      // Check that Console.WriteLine has been called with infra specific details "Poem:\r\n" added
      publicationStrategy.Received().WriteLine("Poem:\r\nIf you could read a leaf or tree\r\nyou'd have no need of books.\r\n-- Alistair Cockburn (1987)");
    }

    [Test]
    public void should_give_verses_when_asked_for_poetry_with_the_support_of_a_file_adapter()
    {
      var fileAdapter = new PoetryLibraryFileAdapter(@"C:\Poetry\Rimbaud.txt");

      // 2. Instantiate the hexagon
      var poetryReader = new PoetryReader(fileAdapter);

      var verses = poetryReader.GiveMeSomePoetry();

      Check.That(verses).IsEqualTo("Some poetry from a file.");
    }
  }
}