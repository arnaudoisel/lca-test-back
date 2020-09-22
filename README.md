# Convert currencies

## Table of contents
* [General info](#general-info)
* [Technologies](#technologies)
* [Build](#build)
* [Usage](#usage)
* [Running unit tests](#running-unit-tests)
* [Production](#production)
* [Example of input file](#Example-of-input-file)

## General info
Back-end test.

This program converts currencies.

## Technologies
Project is created with:
* .NET Core 3.1

## Build
Using .NET Core CLI.

On Windows command line :
```console
chdir LuccaDevises
dotnet build
```

## Usage

```console
chdir LuccaDevises
dotnet run <path-to-input-file>
```

## Running unit tests

```console
chdir LuccaDevisesTests
dotnet test
```
## Production
Executable files are available in the `LuccaDevises/build/<platform>` directory depending on your platform.

Examples :

- Linux x64 machine :

```bash
# Go to folder
cd LuccaDevises/build/linux-x64

# Run the program
./LuccaDevises <path-to-input-file>
```

- Windows :
```console
# Go to folder
chdir LuccaDevises\build\win-x64

# Run the program
LuccaDevises <path-to-input-file>
```

## Example of input file
```
EUR;550;JPY
6
AUD;CHF;0.9661
JPY;KRW;13.1151
EUR;CHF;1.2053
AUD;JPY;86.0305
EUR;USD;1.2989
JPY;INR;0.6571
```

Where :
- Line 1 : source currency ; amount ; destination currency
- Line 2 : number (*N*) of currency change rates
- *N* following lines : Currencies change rates. Where :
    - Each line : source currency ; destination currency ; change rate
