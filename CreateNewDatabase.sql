/* Used to create the SqlServer database that Schema.fs is generated from */

CREATE TABLE [dbo].[SqliteDateTime2Test]
(
  dt [DateTime2](7) NOT NULL,

  CONSTRAINT pk_SqliteDateTime2Test PRIMARY KEY (dt)
);

CREATE TABLE [dbo].[SqliteDateTest]
(
  date [date] NOT NULL,

  CONSTRAINT pk_SqliteDateTest PRIMARY KEY (date)
);