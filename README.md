# FSP Library  
[![Ask DeepWiki](https://deepwiki.com/badge.svg)](https://deepwiki.com/matt-raffel-kiva/FPS-Lib)  

__Please note__:  This project is in the inception phase. The details outlined below are subject to change as we gather feedback and refine our approach.

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
    / library    
        - Compiled library files (e.g., DLLs, JARs, etc.) when applicable
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

