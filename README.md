iMica *
=============

### * I<SUP>ntelligent</SUP> M<SUP>arket</SUP> I<SUP>nvestment</SUP> C<SUP>urating</SUP> A<SUP>utomatron</SUP>

A FinViz.com data scraper, aggregator and warehouse. iMica performs scans and stores information on each FinViz Filter & Option for over a thousand stocks on the US Markets.

### Why?

- **Scanning** - iMica scans and stores over a thousand unique attributes for over a thousand stocks (recommended on a daily basis).

- **Curation** - iMica sifts through gigabytes of quantifiable attribute data to find deltas, scenarios, and values commonly shared by stocks just prior to uncommon price movements.

- **Prediction** - Using basic models (and its mountain of data) iMica predicts future stock movements. The larger the dataset the more accurate the prediction, simple indeed.

- **Automation** - iMica has a built in scheduler that will scan and store data at your appointed time. *Depending on your machine and internet connection, a full market scrape can easily take over an hour.*

------------

### Main Window

![](https://github.com/drmrboyc/iMica/blob/main/iMica-img1.png)

------------

### Basic Use
[TODO]: *Step by step instruction for basic usage.*

------------

### Automation

[TODO]: *How to create a schedule (include best times to perform the different types of scans).*

------------

### Backend

iMica uses [LiteDB](https://www.litedb.org), an embedded NoSQL database made for .NET. 

I chose this backend specifically for mobility and lightning fast transactions while still being thread-safe. LiteDB is written in C# managed code and is a great mobile db usable in a multitude of scenarios. If you are not familiar with it I highly recommend checking it out and adding it to your arsenal of back-end tools.

- The database file is called **FS_DataStr.db**. Duplicate copies of the db file can be found in each of the bin -> Debug & Release directories. A clean version of the db called **FS_DataStr-CLEAN.db** is located in the project's root.

#### LiteDB Studio

If you want to modify or access the database directly I recommend using [LiteDB Studio](https://github.com/mbdavid/LiteDB.Studio), written by the LiteDB team.

------------

### 3rd Party Packages

| Package | Usage                    |
| ------------- | ------------------------------ |
| [LiteDB](https://www.litedb.org)      | An embedded NoSQL db made for .NET.       |
| [Html Agility Pack](https://html-agility-pack.net)   | Used with ScrapySharp to handle all the web scraping.     |
| [ScrapySharp](https://github.com/rflechner/ScrapySharp)      | Used with Html Agility Pack to handle all the web scraping.       |
| [CommandLineParser](https://github.com/commandlineparser/commandline)      | For CLI parsing, ie. automated scheduling compatibility.       |
| [FSharp.Core](https://github.com/fsharp/fsharp-core-docs)      | Used by a 3rd party package... I think.       |

------------

### Code Documentation

- Built in Visual Studio 2022
- Target .Net Framework - 4.8

[TODO]: *Code structure explanation.*
