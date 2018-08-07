namespace HexagonalThis.Domain
{
  public class PoetryReader : IRequestVerses
  {
    private readonly IObtainPoems _poetryLibrary;

    public PoetryReader() : this(new HardcodedPoetryLibrary())
    {
    }

    public PoetryReader(IObtainPoems poetryLibrary)
    {
      _poetryLibrary = poetryLibrary;
    }

    public string GiveMeSomePoetry()
    {
      return _poetryLibrary.GetMeAPoem();
    }

    private class HardcodedPoetryLibrary : IObtainPoems
    {
      public string GetMeAPoem()
      {
        return "I want to sleep\r\nSwat the flies\r\nSoftly, please.\r\n\r\n-- Masaoka Shiki (1867 - 1902)";
      }
    }
  }
}