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
  
# Парсер магазина игрушек от Samsonov Lev

<br/> Это консольное приложение для анализа веб-сайтов.
<br/> Приложение умеет динамически подгружать библиотеки для разных сайтов (сейчас только поддерживает: www.toy.ru).
<br/> Архитектура приложения напоминает Чистую Архитектуру.

# Сборка сервисов
<br/> Приложение "Синтаксический анализатор магазина игрушек" имеет сборку "Службы", которая работает как ядро ​​и определяет интерфейсы и модели для синтаксических анализаторов веб-сайтов (динамически загружаемые библиотеки).
<br/> Также сборка «Сервисы» содержит общие классы, такие как HttpHelper для работы с NetWork, CsvSaver для сохранения результатов и Exceptions.
<br/> Кроме того, эта библиотека отвечает за поиск и загрузку библиотек для определенных веб-сайтов.

# Сборка ToyStoreParserLibrary
<br/> Это библиотека для парсинга сайта www.toy.ru
<br/> Он реализует IToyParser, который имеет только один метод «Task<IEnumerable<Toy>>ParseToEndAsync(string baseUrl)».
<br/> Папка "Процессоры" содержит классы, ответственные за управление потоком работы, но они не собирают данные из HTML-документа.
<br/> Папка Collectors содержит классы, отвечающие за сбор данных из HTML-документа, которые используются классами в папке Processors.
<br/> Папка Helpers содержит класс ToyWebsiteHttpHelper.cs, который помогает управлять файлами cookie и загружать HTML-документ с помощью HttpHelper в сборке «Службы».
  
<br/> На первом этапе URL-адрес веб-сайта переходит к WebsiteProcesser.cs, где он загружает HTML-код.
<br/> На втором этапе WebsiteProcesser.cs передает ответственные продукты на странице в ProductsPageProccesser.cs, а затем переходит на следующую страницу.
<br/> ProductsPageProcesser.cs находит все ссылки на страницы сведений о продуктах и ​​для каждой ссылки создает задачу и передает ее классу DetailsPageProcesser.cs.
<br/> DetailsPageProcesser.cs собирает данные со страниц сведений.
<br/> DetailsPageProcesser.cs должен использовать методы GetToyDetailsAsync и GetToyDetails, потому что сбор данных со страницы связан с процессором, создание множества потоков для каждой пропорции может привести к штрафам за производительность, не говоря уже о памяти. Но и в нормальном, и в тестовом режиме существенных различий в производительности приложения не выявлено.

  PS.
  Для установки местоположения я использовал файлы cookie.
