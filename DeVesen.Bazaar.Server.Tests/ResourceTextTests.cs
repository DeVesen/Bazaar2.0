using DeVesen.Bazaar.Server.Basics;
using FluentAssertions;
using System.Diagnostics.CodeAnalysis;

namespace DeVesen.Bazaar.Server.Tests;

[ExcludeFromCodeCoverage]
public class ResourceTextTests
{
    [Fact]
    public void Transform_()
    {
        // Arrange
        const string message = "Foo {xxx} bar";
        const string expected = "Foo replaced bar";

        // Act
        var result = ResourceText.Transform(message, s => "replaced");

        // Assert
        result.Should().Be(expected);
    }
}