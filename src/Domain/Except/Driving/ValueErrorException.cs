namespace Domain.Except.Driving;

[Serializable]
public class ValueErrorException : Exception
{
   public ValueErrorException(List<string> errors) : base(string.Join("\n", errors)) { }
}