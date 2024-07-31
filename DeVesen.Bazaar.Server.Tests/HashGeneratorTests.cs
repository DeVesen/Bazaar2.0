using DeVesen.Bazaar.Shared.Basics;
using FluentAssertions;
using System.Diagnostics.CodeAnalysis;

namespace DeVesen.Bazaar.Server.Tests;

[ExcludeFromCodeCoverage]
public class HashGeneratorTests
{
    [Theory]
    [InlineData("6BE2EFC4-6AC1-486D-BB63-8E395AA5BE5D", "61eb9e6e")]
    [InlineData("ABE9D905-EBF5-4AA3-ACC3-BB67E0EB5139", "9237bcb8")]
    [InlineData("677D800F-EC98-4633-A0B4-82CC4FE70EDE", "ab9258ee")]
    public void Generate(string text, string expected)
    {
        // Act
        var result = text.ToShortHash();

        // Arrange
        result.Should().Be(expected);
    }
}