CREATE DATABASE IF NOT EXISTS MovieDatabase;

USE MovieDatabase;

CREATE TABLE Movies (
	Release_Date DATETIME,
    Title NVARCHAR(256),
    Overview TEXT,
    Popularity DECIMAL(10,2),
    Vote_Count INTEGER,
    Vote_Average DECIMAL(10,2),
    Original_Language NVARCHAR(3),
    Genre NVARCHAR(64),
    Poster_Url TEXT
);

LOAD DATA LOCAL INFILE '/docker-entrypoint-initdb.d/mymoviedb.csv' INTO TABLE Movies
FIELDS TERMINATED BY ','
ENCLOSED BY '"'
LINES TERMINATED BY '\n'
IGNORE 1 LINES;