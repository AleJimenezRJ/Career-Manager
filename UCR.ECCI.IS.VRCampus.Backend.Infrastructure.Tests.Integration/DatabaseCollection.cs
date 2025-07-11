namespace UCR.ECCI.IS.VRCampus.Backend.Infrastructure.Tests.Integration;

/// <summary>
/// Represents a collection of integration tests that share common setup and teardown logic.
/// </summary>
/// <remarks>This class is used to define a test collection for integration tests by applying the  <see
/// cref="CollectionDefinitionAttribute"/>. It is not intended to be instantiated or contain any code. The <see
/// cref="ICollectionFixture{TFixture}"/> interface is implemented to specify the fixture type  that provides shared
/// context for the tests in the collection.</remarks>
[CollectionDefinition("Database collection")]
public class DatabaseCollection : ICollectionFixture<IntegrationTestFixture>
{
    // This class has no code, and is never instantiated.
    // Its purpose is simply to apply the [CollectionDefinition] attribute.
}