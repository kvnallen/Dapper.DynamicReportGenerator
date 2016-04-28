# Dapper Dynamic Report Generator

A dynamic report generator for Dapper.
Can be used to return generic data in csv (currently) format.

How to use?
-------


To get csv in byteArray:

    var myReport = connection.Query<dynamic>(sql); //result must be IEnumerable<dynamic>
    var report = CsvGenerator.GetByteArrayReport(myReport);

You can get also the result as string:

    var stringResult = CsvGenerator.GetStringReport(myReport);
