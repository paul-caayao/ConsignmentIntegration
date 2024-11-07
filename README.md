**About the Project**

This is a technical exam on parsing XML data from an FTP server and sending it to an API endpoint, which returns a base64 string to generate a PDF.

**Getting started**

Prerequisites
.NET 8 SDK https://dotnet.microsoft.com/en-us/download/dotnet/8.0

Steps to test
1. Extract the published solution
2. Navigate to the extracted folder and execute ConsignmentIntegration.exe
3. Console app will commence the operation and it should provide a path at the end where you can find the generated PDF


**Notes**

ConsignmentXML structure
- Consignment represents the job/delivery
- Each consignment contains the sender and receiver data
- Consignment contains individual items represented as "Row_number_attribute"
- There can be multiple Row records that follow the same format as mentioned above

   
