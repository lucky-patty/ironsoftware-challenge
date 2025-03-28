using Xunit;
using Ninject;
using System.Collections.Generic;

public class EnglishPadTest{

    private readonly PhonePad _pad;

    public EnglishPadTest() {
        var kernel = new StandardKernel();
        kernel.Bind<Dictionary<char, string>>().ToConstant(KeyMaps.English);
        kernel.Bind<PhonePad>().To<OldPhonePad>();

        _pad = kernel.Get<PhonePad>();
    }

    [Theory]
    [InlineData("44 33 555 555 666#", "HELLO")]
    [InlineData("2#", "A")]
    [InlineData("222#", "C")]
    [InlineData("999337777#", "YES")]     // 'Y' from 999, 'E' from 33, 'S' from 7777

    public void ConvertMessage_ShouldReturnExpectedOutput(string input, string expected){
        var result = _pad.ConvertMessage(input);
        Assert.Equal(expected, result);
    }
}
