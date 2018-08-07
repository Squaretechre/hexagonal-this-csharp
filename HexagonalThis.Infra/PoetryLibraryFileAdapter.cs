using System.IO;
using HexagonalThis.Domain;

namespace HexagonalThis.Infra
{
  public class PoetryLibraryFileAdapter : IObtainPoems
  {
    private readonly string _poem;

    public PoetryLibraryFileAdapter(string filePath)
    {
      _poem = File.ReadAllText(filePath);
    }

    public string GetMeAPoem()
    {
      return _poem;
    }
  }
}