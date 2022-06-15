//See https://aka.ms/new-console-template for more information
using Services;
using Services.Helpers;
using Services.Models;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

string url = @"https://www.toy.ru/catalog/boy_transport/";
string fileName = "Toys";
string savePath = @"C:\Users\Tailog\TestApps\Toys Store Parser\ToysStoreParser\bin\Debug\net6.0\Saves";
string fullPath = @"C:\Users\Tailog\TestApps\Toys Store Parser\ToysStoreParser\bin\Debug\net6.0\Saves\Toys.csv";

var saveHelper = new CsvSaveHelper<Toy>();
saveHelper.CreateCsvFileASync(savePath, fileName).Wait();


// Dynamically load .dll with a parser for toy.ru website in the class constructor
IParserService parserService = new ParserService();

// Parse website
var toys = parserService.ToyParser
    .ParseToEndAsync(url)
    .Result
    .ToArray();

// Print some of the results
Console.WriteLine($"Name: {toys[1].Name}");
Console.WriteLine($"Price: {toys[1].Price}");
Console.WriteLine($"OldPrice: {toys[1].OldPrice}");
Console.WriteLine($"Region: {toys[1].Region}");
Console.WriteLine($"Is In Stock: {toys[1].IsInStock}");
Console.WriteLine($"Product Url: {toys[1].ProductUrl}");
Console.WriteLine($"Big Pictues Urls:");
foreach (var bigPictuesUrl in toys[1].BigPictuesUrls)
    Console.WriteLine(" " + bigPictuesUrl);
Console.WriteLine($"Breadcrumbs:");
foreach (var crumb in toys[1].Crumbs)
    Console.WriteLine(" " + crumb);
Console.WriteLine($"Small Pictues Urls:");
foreach (var smallPictuesUrls in toys[1].SmallPictuesUrls)
    Console.WriteLine(" " + smallPictuesUrls);
Console.WriteLine($"Total items parsed: {toys.Length}");
Console.WriteLine(Environment.NewLine);

// Save results
saveHelper.SaveAsync(fullPath, toys).Wait();

// Parse website but use Ростов на Дону as a location
// If the location is not specified no cookies are sended
parserService.ToyParser.Region = RegionEnum.RostovOnDon;

// Parse website
var toysRostovOnDon = parserService.ToyParser
    .ParseToEndAsync(url)
    .Result
    .ToArray();

// Print some of the results
Console.WriteLine($"Name: {toysRostovOnDon[1].Name}");
Console.WriteLine($"Price: {toysRostovOnDon[1].Price}");
Console.WriteLine($"OldPrice: {toysRostovOnDon[1].OldPrice}");
Console.WriteLine($"Region: {toysRostovOnDon[1].Region}");
Console.WriteLine($"Is In Stock: {toysRostovOnDon[1].IsInStock}");
Console.WriteLine($"Product Url: {toysRostovOnDon[1].ProductUrl}");
Console.WriteLine($"Big Pictues Urls:");
foreach (var bigPictuesUrl in toysRostovOnDon[1].BigPictuesUrls)
    Console.WriteLine(" " + bigPictuesUrl);
Console.WriteLine($"Breadcrumbs:");
foreach (var crumb in toysRostovOnDon[1].Crumbs)
    Console.WriteLine(" " + crumb);
Console.WriteLine($"Small Pictues Urls:");
foreach (var smallPictuesUrls in toysRostovOnDon[1].SmallPictuesUrls)
    Console.WriteLine(" " + smallPictuesUrls);
Console.WriteLine($"Total items parsed: {toysRostovOnDon.Length}");
Console.WriteLine(Environment.NewLine);

// Save results
saveHelper.SaveAsync(fullPath, toysRostovOnDon).Wait();