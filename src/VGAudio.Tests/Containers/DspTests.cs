using VGAudio.Containers.Dsp;
using VGAudio.Formats.GcAdpcm;
using Xunit;

namespace VGAudio.Tests.Containers
{
    public class DspTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(8)]
        public void DspBuildAndParseEqual(int numChannels)
        {
            GcAdpcmFormat audio = GenerateAudio.GenerateAdpcmSineWave(BuildParseTestOptions.Samples, numChannels, BuildParseTestOptions.SampleRate);

            BuildParseTests.BuildParseCompareAudio(audio, new DspWriter(), new DspReader());
        }
    }
}
