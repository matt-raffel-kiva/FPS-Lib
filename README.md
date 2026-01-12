# FSP Library  

## Goal(s)  
1. natural language libraries for consuming PA2 APIs   
2. natural language tools for assisting with PA2 tasks  

### Background  
Our research indicates our partners use these programming languages most frequently when working with PA2 APIs: 
C#, PHP, JavaScript, and Java. Therefore, we will focus our initial efforts on these languages.  

## Structure
Each library will be structured as follows:  
```
/LibraryName
    /docs
        - Documentation files (e.g., README.md, API reference)
    /src
        - Source code files
    /tests
        - Unit and integration tests    
    /examples
```

## Design Principles

Library names will be the language the library is written in, e.g., `dotnet`, `PHP`, `js`, `Java`.

Libraries are designed to be modular, allowing users to include only the components they need for their specific use cases.

Libraries are designed to work in a variety of environments, including web applications, desktop applications, and server-side applications.

