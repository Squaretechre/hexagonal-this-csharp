using System;
using HexagonalThis.Domain;

namespace HexagonalThis.Infra
{
  public class ConsoleAdapter
  {
    private readonly IRequestVerses _poetryReader;
    private readonly IWriteLines _publicationStrategy;

    public ConsoleAdapter(IRequestVerses poetryReader, IWriteLines publicationStrategy)
    {
      _poetryReader = poetryReader;
      _publicationStrategy = publicationStrategy;
    }

    public ConsoleAdapter(IRequestVerses poetryReader) : this(poetryReader, new ConsolePublicationStrategy())
    {
    }

    public void Ask()
    {
      // From infra to domain, nothing to do, ask has no arguments

      // Business logic
      var verses = _poetryReader.GiveMeSomePoetry();

      // From domain to infra
      _publicationStrategy.WriteLine($"Poem:\r\n{verses}");
    }

    private class ConsolePublicationStrategy : IWriteLines
    {
      public void WriteLine(string text)
      {
        Console.WriteLine(text);
      }
    }
  }
}