# example_movieapi
A simple Azure function app for retrieving data about movies
## details
C# Azure function app,
Sqlite database (single file included in project for testing)
EF Core for data retrieval
Dependency Injection
Deployment via Dockerfile included
## usage
Load the project in Visual Studio and use the build profile 'API'. This will start the function app locally and you can make calls to it using Postman or a similar tool
## endpoints
### GET /api/movies
will return paginated data about movies
#### request
no body, only parameters. accepted parameters are:
- title
  - a partial search term. any movie titles that contain the search term can be returned
- genre
  - a partial search term. a movies genre list is searched for the search term, any that match can be returned.
- sort
  - a field name of a movie object. the returned list will be sorted by the term in a descending order
- page
  - integer representing page of paginated data to be returned
- pageSize
  - integer representing number of items to be present on each page returned
##### example
```` javascript
GET http://localhost:7071/api/movies?title=spider&page=1&pageSize=10
````
#### response
any matching movies plus pagination data are returned
```` json
{
    "movies": "array of movie data",
    "pagination": {
        "page": "page returned",
        "pageSize": "numberof items on page",
        "totalPageCount": "number of pages available for this query"
    }
}
````
##### example
```` json
{
    "movies": [{
            "release_Date": "2021-12-15",
            "title": "Spider-Man: No Way Home",
            "overview": "Peter Parker is unmasked and no longer able to separate his normal life from the high-stakes of being a super-hero. When he asks for help from Doctor Strange the stakes become even more dangerous, forcing him to discover what it truly means to be Spider-Man.",
            "popularity": 5083.954,
            "vote_Count": 8940,
            "vote_Average": 8.3,
            "original_Language": "en",
            "genre": "Action, Adventure, Science Fiction",
            "poster_Url": "https://image.tmdb.org/t/p/original/1g0dhYtq4irTY1GPXvft6k4YLjm.jpg"
        },
        {
            "release_Date": "2022-03-01",
            "title": "The Batman",
            "overview": "In his second year of fighting crime, Batman uncovers corruption in Gotham City that connects to his own family while facing a serial killer known as the Riddler.",
            "popularity": 3827.658,
            "vote_Count": 1151,
            "vote_Average": 8.1,
            "original_Language": "en",
            "genre": "Crime, Mystery, Thriller",
            "poster_Url": "https://image.tmdb.org/t/p/original/74xTEgt7R36Fpooo50r9T25onhq.jpg"
        }
    ],
    "pagination": {
        "page": 1,
        "pageSize": 10,
        "totalPageCount": 10
    }
}
````
