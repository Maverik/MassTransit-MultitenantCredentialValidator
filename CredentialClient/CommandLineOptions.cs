using CommandLine;

namespace CredentialClient
{
    class CommandLineOptions
    {
        [Option('u', "user", HelpText = "Username to be used for credential validation message", Required = true)]
        public string Username { get; set; }

        [Option('p', "password", HelpText = "Password to be used for credential validation message", Required = true)]
        public string Password { get; set; }

        [Option('t', "tenant", HelpText = "TenantId against which to validate credentials", Required = true)]
        public string TenantId { get; set; }
    }
}