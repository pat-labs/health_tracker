namespace Domain.Except.Driving;


[Serializable]
public class MissingValuesException : Exception
{
   public MissingValuesException(List<string> errors) : base(string.Join("\n", errors)) { }
}