using CommandLine;

namespace SwapChannels
{
    class Program
    {
        [Verb("add", HelpText = "Filename(s) to convert.")]
        class AddOptions
        {
            [Option('f', "filename", Required = true, HelpText = "Filename(s) to convert.")]
            public string Filename { get; set; }
        }

        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<AddOptions>(args).MapResult((AddOptions opts) => RunAddAndReturnExitCode(opts), errs => 1);
        }

        private static object RunAddAndReturnExitCode(AddOptions opts)
        {
            new Shared().SwapChannels(opts.Filename);
            return null;
        }
    }
}
