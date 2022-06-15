# Toys Store Parser by Samsonov Lev

<br/> This a console app for website parsing.
<br/> The app can dynamically load libraries for different websites (Right now it olny supports: www.toy.ru).
<br/> The app architecture remind Clean Architecture.

# Services Assembly
<br/> The Toys Store Parser app has 'Services' assembly that work as the Core and determine Interfaces and Models for websites parsers (dynamically load libraries).
<br/> Also, 'Services' assembly contain common Classes like HttpHelper for work with NetWork, CsvSaver for saving results and Exceptions.
<br/> Beside that, this library responsible for finding and loading library for specific websites.

# ToyStoreParserLibrary Assembly
<br/> This is a library for parsing www.toy.ru website
<br/> It implements IToyParser that has only one method 'Task<IEnumerable<Toy>>ParseToEndAsync(string baseUrl)'.
<br/> The Processors folder contains classes those responsibility control flow of work, but they don't collect data from a Html document.
<br/> The Collectors folder contains classes those responsibility collect data from a Html document they are used by classes in Processors folder.
<br/> Helpers folder contains the ToyWebsiteHttpHelper.cs class that help to manage cookies and load Html document using HttpHelper in 'Services' assembly.
  
<br/> At first step website url go to the WebsiteProcesser.cs where it load Html
<br/> At second step the WebsiteProcesser.cs give responsibility products on page to the ProductsPageProccesser.cs and then go for next page 
<br/> The ProductsPageProccesser.cs find all links for the products details pages and for each link create task and give it to the DetailsPageProcesser.cs class
<br/> The DetailsPageProcesser.cs collect data from details pages.
<br/> The DetailsPageProcesser.cs has to method GetToyDetailsAsync and GetToyDetails, because collecting data from page is cpu bound task create many threads for each proporty can couse performans penalties, not mention of memory. But both for fine and test did not revealed any significant differences in the app performance.

  PS.
  For setting location I used cookies.
