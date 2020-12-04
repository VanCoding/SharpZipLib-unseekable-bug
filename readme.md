# Introduction

There seems to be a Regression in SharpZipLib when upgrading from **netcoreapp2** to **net5**.
In net5, creating a ziparchive on the fly, targeting an **unseekable** stream, produces an invalid archive.
In netcoreapp2, this is not the case.

## To test:

- run `dotnet run netcoreapp2` to verify that it worked previously
- run `dotnet run net5` to prove that it does no longer work

When looking at the files `seekable.zip` and `unseekable.zip` it's clear that they contain different bytes, even though the only difference is that one was streamed to a unseekable stream and one was streamed to a seekable stream.
