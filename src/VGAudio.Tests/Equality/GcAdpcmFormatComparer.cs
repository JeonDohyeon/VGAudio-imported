using System.Collections.Generic;
using System.Linq;
using VGAudio.Formats;
using VGAudio.Formats.GcAdpcm;

namespace VGAudio.Tests.Equality
{
    public class GcAdpcmFormatComparer : EqualityComparer<GcAdpcmFormat>
    {
        public override bool Equals(GcAdpcmFormat x, GcAdpcmFormat y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return
                x.SampleCount == y.SampleCount &&
                x.ChannelCount == y.ChannelCount &&
                x.LoopStart == y.LoopStart &&
                x.LoopEnd == y.LoopEnd &&
                x.Looping == y.Looping &&
                (x.Tracks ?? new List<AudioTrack>()).SequenceEqual(y.Tracks ?? new List<AudioTrack>(), new AudioTrackComparer()) &&
                x.Channels.SequenceEqual(y.Channels, new GcAdpcmChannelComparer());
        }

        public override int GetHashCode(GcAdpcmFormat obj)
        {
            unchecked
            {
                if (obj == null) return 0;
                int hashCode = obj.SampleCount;
                hashCode = (hashCode * 397) ^ obj.ChannelCount;
                hashCode = (hashCode * 397) ^ obj.LoopStart;
                hashCode = (hashCode * 397) ^ obj.LoopEnd;
                hashCode = (hashCode * 397) ^ obj.Looping.GetHashCode();
                return hashCode;
            }
        }
    }
}
