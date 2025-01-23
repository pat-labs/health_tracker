namespace Domain.Except;

[Serializable]
public class EntityNotFoundException : Exception
{
   public EntityNotFoundException(string entity, string id) : base($"No {entity} found for id: {id}") { }
}