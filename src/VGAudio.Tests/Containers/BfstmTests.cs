using VGAudio.Containers.NintendoWare;
using VGAudio.Formats;
using VGAudio.Formats.GcAdpcm;
using VGAudio.Formats.Pcm16;
using VGAudio.Formats.Pcm8;
using Xunit;

namespace VGAudio.Tests.Containers
{
    public class BfstmTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(8)]
        public void BfstmBuildAndParseEqual(int numChannels)
        {
            GcAdpcmFormat audio = GenerateAudio.GenerateAdpcmSineWave(BuildParseTestOptions.Samples, numChannels, BuildParseTestOptions.SampleRate);

            BuildParseTests.BuildParseCompareAudio(audio, new BCFstmWriter(NwTarget.Cafe), new BCFstmReader());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(8)]
        public void BfstmBuildAndParseEqualPcm16(int numChannels)
        {
            Pcm16Format audio = GenerateAudio.GeneratePcmSineWave(BuildParseTestOptions.Samples, numChannels, BuildParseTestOptions.SampleRate);
            var writer = new BCFstmWriter(NwTarget.Cafe) { Configuration = { Codec = NwCodec.Pcm16Bit } };

            BuildParseTests.BuildParseCompareAudio(audio, writer, new BCFstmReader());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(8)]
        public void BfstmBuildAndParseEqualPcm8(int numChannels)
        {
            Pcm8SignedFormat audio = GenerateAudio.GeneratePcm8SignedSineWave(BuildParseTestOptions.Samples, numChannels, BuildParseTestOptions.SampleRate);
            var writer = new BCFstmWriter(NwTarget.Cafe) { Configuration = { Codec = NwCodec.Pcm8Bit } };

            BuildParseTests.BuildParseCompareAudio(audio, writer, new BCFstmReader());
        }

        [Fact]
        public void BfstmLoopAlignmentIsSet()
        {
            GcAdpcmFormat audio = GenerateAudio.GenerateAdpcmSineWave(BuildParseTestOptions.Samples, 1, BuildParseTestOptions.SampleRate);
            audio = audio.WithLoop(true, 1288, 16288);
            var writer = new BCFstmWriter(NwTarget.Cafe) { Configuration = { LoopPointAlignment = 700 } };

            byte[] builtFile = writer.GetFile(audio);
            IAudioFormat parsedAudio = new BCFstmReader().ReadFormat(builtFile);

            Assert.Equal(1400, parsedAudio.LoopStart);
            Assert.Equal(16400, parsedAudio.LoopEnd);
        }
    }
}
