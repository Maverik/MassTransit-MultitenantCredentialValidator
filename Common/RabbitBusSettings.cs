using System;

namespace Common
{
    public class RabbitBusSettings
    {
        //connecting on localhost as guest is by default restricted to localhost only
        //connecting to default vhost
        public Uri HostAddress { get; } = new Uri("rabbitmq://localhost:5671/");

        //using default rabbitmq administrative user
        public string Username { get; } = "guest";

        //using default rabbitmq administrative user password
        public string Password { get; } = "guest";

        public string QueueName { get; } = "CredentialValidator";

        //using IIS Express Development Certificate that has cn=localhost
        public string SslServerName { get; } = "localhost";
    }
}