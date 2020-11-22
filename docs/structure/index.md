## File Structure

## Micro.AppRegistration.Api
This is the project which is actually booted, once it boots, it configures and starts listening for incoming requests.
`Controllers` are where requests will land in, they're not supposed to contain any business logic,
but rather extract data from requests and pass in to other services.

## Micro.AppRegistration.UnitTest
This project contains unit tests and postman tests for Micro.AppRegistration.Api
