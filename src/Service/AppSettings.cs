#nullable disable warnings

namespace Service;

public class AppSettings
{
   public LoggingSettings Logging { get; set; }
   public string DatabaseConnection { get; set; }
}