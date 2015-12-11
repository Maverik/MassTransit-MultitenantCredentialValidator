# MassTransit-MultitenantCredentialValidator
A sample that shows how to use MassTransit with RabbitMQ &amp; Autofac in a multi-tenant situation

### Requirements
The sample requires RabbitMQ which can be downloaded from http://www.rabbitmq.com/  
This sample was tested on 3.5.6 on Windows 10 using Erlang 18.1 x64 runtime


#### Sample config
You can find sample configuration I used under `extras/rabbitmq/`

I run in SSL mode so `localhost.pem` IIS Express Development Certificate is included in there too and referenced in sample configuration

RabbitMQ AMQP SSL port is : `5671`  
RabbitMQ Management SSL port is : `15671` (you can visit management interface via https://localhost:15671/)

RabbitMQ Default credentials are (accessible via localhost only):  
username: `guest`  
password: `guest`

Sample `enabled_plugins` enables management plugin as well as visualizer in management interface

Sample configuration also has a stomp ssl section for rabbitmq but not used in this sample right now

### Operation

`CredentialValidator.exe` is the main consumer of messages. It expects a `ValidateCredential` message with a TenantId inside that message based off which it uses appropriate impelmentation of ICredentialRepository

If the `TenantId` is `A` then it uses `EqualCredentialRespository` (which returns `username == password` for validation)

If the `TenantId` is `B` then it uses `UnequalCredentialRespository` (which returns `username != password` for validation)

For any other `TenantId` it uses default `FalseCredentialRepository` (which returns `false` for everything)

The result is returned in a `CredentialValidated` message to issuing `CredentialClient` indicating success status.

`CredentialClient.exe` expects three arguments:  
```
  -u, --user        Required. Username to be used for credential validation message  
  -p, --password    Required. Password to be used for credential validation message  
  -t, --tenant      Required. TenantId against which to validate credentials
```
