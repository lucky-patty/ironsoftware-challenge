using Xunit;
using Ninject;
using System.Collections.Generic;

public class ThaiPadTest{

    private readonly PhonePad _pad;

    public ThaiPadTest() {
        var kernel = new StandardKernel();
        kernel.Bind<Dictionary<char, string>>().ToConstant(KeyMaps.Thai);
        kernel.Bind<PhonePad>().To<OldPhonePad>();

        _pad = kernel.Get<PhonePad>();
    }

    [Theory]
    [InlineData("1000000000#", "กำ")]
    [InlineData("0969999#", "เสนอ")]

    public void ConvertMessage_ShouldReturnExpectedOutput(string input, string expected){
        var result = _pad.ConvertMessage(input);
        Assert.Equal(expected, result);
    }
}
