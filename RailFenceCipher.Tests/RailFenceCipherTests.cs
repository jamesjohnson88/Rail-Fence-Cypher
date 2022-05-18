using FluentAssertions;
using Xunit;

// ReSharper disable StringLiteralTypo

namespace RailFenceCipher.Tests;

public class RainFenceCipherTests
{
    [Theory]
    [InlineData(2, "AABB", "ABAB")]
    [InlineData(2, "I HAVE SPACES", "IAEPCSHVSAE")]
    [InlineData(3, "AABBCC", "ACABCB")]
    [InlineData(3, "I_REALLY_LIKE_PUZZLES!", "IA_EZS_ELYLK_UZE!RLIPL")]
    [InlineData(5, "I_REALLY_LIKE_PUZZLES!", "I_Z_YLUZRLIPLELK_E!AES")]
    [InlineData(30, "THISISNOTAVERYGOODSECRETCODE", "THISISNOTAVERYGOODSECRETCODE")]
    public void Do_Encode_ReturnsExpected(int rows, string input, string expected)
    {
        // Arrange
        
        // Act
        var result = RailFenceCipher.Encode(rows, input);
        
        // Assert
        result.Should().Be(expected);
    }
    
    [Theory]
    [InlineData(2, "ABAB", "AABB")]
    [InlineData(3, "ACABCB", "AABBCC")]
    [InlineData(3, "IA_EZS_ELYLK_UZE!RLIPL", "I_REALLY_LIKE_PUZZLES!")]
    [InlineData(5, "I_Z_YLUZRLIPLELK_E!AES", "I_REALLY_LIKE_PUZZLES!")]
    [InlineData(8, "WGENCLIHODAVOLSECLETEEGHN", "WELOVETHECODINGCHALLENGES")]
    [InlineData(7, 
        "384874021.5963843594466817137232191456280204596809907918934616343573592869437522829198037288593762002",
        "3.141592653589793238462643383279502884197169399375105820974944592307816406286208998628034825342117067")]
    public void Do_Decode_ReturnsExpected(int rows, string input, string expected)
    {
        // Arrange
        
        // Act
        var result = RailFenceCipher.Decode(rows, input);
        
        // Assert
        result.Should().Be(expected);
    }
}