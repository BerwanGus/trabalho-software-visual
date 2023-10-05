using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class Back2youControllerBase : ControllerBase
{
  public static string GetNewUuid()
  {
    Guid uuid = Guid.NewGuid();
    return uuid.ToString("N");
  }
}