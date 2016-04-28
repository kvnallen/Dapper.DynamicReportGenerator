# Dapper Dynamic Report Generator

A dynamic report generator for Dapper.
Can be used to return generic data in csv (currently) format.

How to use?
-------


To get csv in byteArray:

    //result must be IEnumerable<dynamic>
    var myReport = connection.Query<dynamic>(sql); 
    
    var report = CsvGenerator.GetByteArrayReport(myReport);

You can get also the result as string:

    var stringResult = CsvGenerator.GetStringReport(myReport);
