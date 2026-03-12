# Project Description
DotNET Version 10 library for consuming Kiva PA2 api.  

Our partners will consume this library to interact with the Kiva PA2 API, which provides access to Kiva's lending data and allows partners to manage their interactions with Kiva's platform. The library will abstract away the complexities of making HTTP requests and handling responses, providing a simple and intuitive interface for developers.

## Specific instructions
Always propose a plan before making file changes. List:
- Files that will be modified/created/deleted
- Summary of changes per file
- Any risks or alternatives

Do not proceed until the user says "approved", "yes", or "go ahead".

## More information
- [Kiva PA2 API documentation](https://fps-sdk-portal.web.app/docs/overview)
- [Swagger documentation](https://partnerapi.staging.kiva.org/swagger-ui/index.html)

## Directories
- `src/`: Source code for the library.
- `src/API/`: classes our partners will use to interact with the Kiva PA2 API.
- `src/Contracts/`: Data models representing the structures used by the Kiva PA2 API.
- `tests/`: Unit tests.
- `examples/`: Example usages of the library.
- `docs/`: End user documentation for the library.


## Building and Testing
- `dotnet build` — build the library