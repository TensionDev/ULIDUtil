# TensionDev.ULID

[![.NET](https://github.com/TensionDev/ULIDUtil/actions/workflows/dotnet.yml/badge.svg)](https://github.com/TensionDev/ULIDUtil/actions/workflows/dotnet.yml)
[![Package Release](https://github.com/TensionDev/ULIDUtil/actions/workflows/package-release.yml/badge.svg)](https://github.com/TensionDev/ULIDUtil/actions/workflows/package-release.yml)
[![CodeQL](https://github.com/TensionDev/ULIDUtil/actions/workflows/github-code-scanning/codeql/badge.svg)](https://github.com/TensionDev/ULIDUtil/actions/workflows/github-code-scanning/codeql)

TensionDev.ULID is a .NET Library for working with Universally Unique Lexicographically Sortable Identifiers (ULIDs).  
This project references the following documents for implementation.  
- [ulid/spec: The canonical spec for ulid](https://github.com/ulid/spec)

---

## Features  
- Converters:  
  - `ToGuid()`  
  - `ToByteArray()`  
- Parsing utilities:  
  - `Parse(string)`  
  - `TryParse(string, out Uuid)`  
- Comparison operators:  
  - `==`, `!=`, `<`, `>`, `<=`, `>=`  
- Equality and hashing for dictionary/set usage  

---

## Installation
```
dotnet add package TensionDev.ULID
```

---

## Usage Examples

### Generate ULID
```csharp
using TensionDev.ULID;

Ulid ulid = Ulid.NewUlid();
Console.WriteLine(uuid); // Example: 01ARZ3NDEKTSV4RRFFQ69G5FAV
```

### Parse and validate
```csharp
using TensionDev.ULID;

bool isValid = Ulid.TryParse("01ARZ3NDEKTSV4RRFFQ69G5FAV", out var parsed);
```

