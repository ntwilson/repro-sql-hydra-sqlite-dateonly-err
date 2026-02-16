# Reproduce SqlHydra Date error for Sqlite

This matches our setup fairly closely. Program.fs will create a new Sqlite database, run a script to setup the tables matching the schema from Schema.fs (which was generated from a SqlServer database). Then it will insert a value into the database and attempt to read it back. It does this for a table with a DateTime2 column, and for a table with a Date column. Doing it for the DateTime2 column works fine, but reading the value back will throw the error for parsing a Date.

Note we're using `text` to represent dates/datetimes in sqlite, but you can change SqliteTestDatabase.fs to use `int` or `numeric` for dates/datetimes with the exact same result.