using System;

namespace VGAudio.Tools
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: <test to run>");
                return;
            }

            switch (args[0].ToLower())
            {
                case "rebuild":
                    Rebuild.Runner.Run(args);
                    break;
                case "gcadpcm":
                    GcAdpcm.Runner.Run(args);
                    break;
                case "atrac9":
                    Atrac9.Runner.Run(args);
                    break;
                case "crackadx":
                    CrackAdx.Runner.Run(args);
                    break;
                case "crackhca":
                    CrackHca.Runner.Run(args);
                    break;
                default:
                    Console.WriteLine("Unknown test");
                    PrintTestList();
                    return;
            }
        }

        private static void PrintTestList()
        {
            Console.WriteLine("Available tests:");
            Console.WriteLine("Rebuild\t\tReads and rebuilds an audio file and compares the result with the original");
            Console.WriteLine("GcAdpcm\t\tCompares GcAdpcm encoding with the official implementation");
            Console.WriteLine("CrackAdx\tAttempts to crack the decryption key of a folder of ADX files");
            Console.WriteLine("CrackHca\tIncomplete, experimental HCA decryption cracker");
            Console.WriteLine("Atrac9\t\tCompares ATRAC9 decoding with the official implementation");
        }
    }
}