using ArchUnitNET.Domain;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;

using Domain;
using Service;

using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace Tests.Units.Architecture
{
   public class LayerConventionTest
   {
      private static readonly ArchUnitNET.Domain.Architecture Architecture = new ArchLoader().LoadAssemblies(typeof(PandaRenamer).Assembly).Build();
      private readonly IObjectProvider<IType> _domainLayer = Types().That().ResideInNamespace("Domain.*", useRegularExpressions: true).As("Domain layer");
      private readonly IObjectProvider<IType> _serviceLayer = Types().That().ResideInNamespace("Service.*", useRegularExpressions: true).As("Service layer");

      [Fact]
      public void Types_that_resides_in_Core_layer_should_not_depend_on_any_types_that_reside_in_Mordor_layer()
      {
         // arrange act assert
         Types().That().Are(_domainLayer).Should().NotDependOnAny(_serviceLayer).Because("It's dangerous to not respect the architecture!").Check(Architecture);
      }
   }
}