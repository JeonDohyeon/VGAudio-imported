﻿using System;
using System.Linq;
using System.Text;
using Cake.Common.Diagnostics;
using Cake.Frosting;
using ConsoleTables;

namespace Build.Tasks
{
    public sealed class BuildReport : FrostingTask<Context>
    {
        public override void Run(Context context)
        {
            context.Information(CreateBuildReport(context));
        }

        public static string CreateBuildReport(Context context)
        {
            var builder = new StringBuilder();

            builder.AppendLine();
            builder.AppendLine("Library Builds");
            builder.Append('-', 35).AppendLine();
            var libraryTable =  new ConsoleTable("Name", "Status");
            foreach (LibraryBuildStatus build in context.LibBuilds.Values)
            {
                libraryTable.AddRow(build.LibFramework, GetStatusString(build.LibSuccess));
            }
            builder.AppendLine(libraryTable.ToMarkDownString());

            builder.AppendLine("CLI Builds");
            builder.Append('-', 35).AppendLine();
            var cliTable =  new ConsoleTable("Name", "Status");
            foreach (LibraryBuildStatus build in context.LibBuilds.Values)
            {
                cliTable.AddRow(build.CliFramework, GetStatusString(build.CliSuccess));
            }
            builder.AppendLine(cliTable.ToMarkDownString());

            builder.AppendLine("Tests");
            builder.Append('-', 35).AppendLine();
            var testsTable =  new ConsoleTable("Name", "Status");
            foreach (LibraryBuildStatus build in context.LibBuilds.Values)
            {
                testsTable.AddRow(build.TestFramework, GetStatusString(build.TestSuccess));
            }
            builder.Append(testsTable.ToMarkDownString());

            return builder.ToString();
        }

        private static string GetStatusString(bool? status)
        {
            switch (status)
            {
                case true:
                    return "Success";
                case false:
                    return "Failed";
                default:
                    return "Not Built";
            }
        }
    }

    public sealed class VerifyBuildSuccess : FrostingTask<Context>
    {
        public override void Run(Context context)
        {
            if (context.LibBuilds.Values.Any(x => x.LibSuccess != true))
            {
                throw
                    new Exception("Library build failed");
            }

            if (context.LibBuilds.Values.Any(x => x.CliSuccess != true))
            {
                throw new Exception("Library build failed");
            }
        

            if (context.LibBuilds.Values.Any(x => x.TestSuccess != true))
            {
                throw new Exception("Library build failed");
            }
        }
    }
}
