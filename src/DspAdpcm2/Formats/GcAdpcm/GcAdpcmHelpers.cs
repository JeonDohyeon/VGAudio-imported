﻿using System;
using System.Collections.Generic;
using System.Linq;
using DspAdpcm.Utilities;

namespace DspAdpcm.Formats.GcAdpcm
{
    public static class GcAdpcmHelpers
    {
        public static readonly int BytesPerFrame = 8;
        public static readonly int SamplesPerFrame = 14;
        public static readonly int NibblesPerFrame = 16;

        public static int NibbleCountToSampleCount(int nibbleCount)
        {
            int frames = nibbleCount / NibblesPerFrame;
            int extraNibbles = nibbleCount % NibblesPerFrame;
            int extraSamples = extraNibbles < 2 ? 0 : extraNibbles - 2;

            return SamplesPerFrame * frames + extraSamples;
        }

        public static int SampleCountToNibbleCount(int sampleCount)
        {
            int frames = sampleCount / SamplesPerFrame;
            int extraSamples = sampleCount % SamplesPerFrame;
            int extraNibbles = extraSamples == 0 ? 0 : extraSamples + 2;

            return NibblesPerFrame * frames + extraNibbles;
        }

        public static int NibbleToSample(int nibble)
        {
            int frames = nibble / NibblesPerFrame;
            int extraNibbles = nibble % NibblesPerFrame;
            int samples = SamplesPerFrame * frames;

            return samples + extraNibbles - 2;
        }

        public static int SampleToNibble(int sample)
        {
            int frames = sample / SamplesPerFrame;
            int extraSamples = sample % SamplesPerFrame;

            return NibblesPerFrame * frames + extraSamples + 2;
        }

        public static int SampleCountToByteCount(int sampleCount) => SampleCountToNibbleCount(sampleCount).DivideBy2RoundUp();

        public static byte[] BuildSeekTable(IList<GcAdpcmChannel> channels, int samplesPerEntry, int entryCount, Helpers.Endianness endianness, bool ensureSelfCalculated = false)
        {
            short[] table = channels
                .Select(x => x.GetSeekTable(samplesPerEntry, ensureSelfCalculated))
                .ToArray()
                .Interleave(2);

            Array.Resize(ref table, entryCount * 2 * channels.Count);
            return table.ToByteArray(endianness);
        }
    }
}
