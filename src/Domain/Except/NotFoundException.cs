namespace Domain.Except;

[Serializable]
public class NotFoundException : Exception
{
   public NotFoundException(string entity, string id) : base($"No {entity} found for id: {id}"){}
}