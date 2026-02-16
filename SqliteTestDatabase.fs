module SqlHydraForSqlite.SqliteTestDatabase

let schema =
    """

CREATE TABLE dbo.SqliteDateTest
(
  date [numeric] NOT NULL,

  CONSTRAINT pk_SqliteDateTest PRIMARY KEY (date)
);
"""
