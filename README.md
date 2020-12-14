# WATS Client Converter - XML Converter Example

An example of a WATS Client converter, using an XML format to import test data to WATS. This example format is a different format from the [WATS Standard XML Format](https://virinco.zendesk.com/hc/en-us/articles/207424643). An example file of this XML format can be found in the Examples folder.

This project is an example of how you can create a custom converter reading a custom XML format using the WATS API for the WATS Client. Use it as a starting point for creating your own converter.

## Getting Started

* [About WATS](https://wats.com/manufacturing-intelligence/)
* [About submitting data to WATS](https://virinco.zendesk.com/hc/en-us/articles/207424613)
* [About creating custom converters](https://virinco.zendesk.com/hc/en-us/articles/207424593)

## Parameters

This converter uses the following parameters:

| Parameter    | Default value | Description                                                  |
|--------------|---------------|--------------------------------------------------------------|
| sequenceFile |               | The name of the sequence file used to generate the XML file. |

## Testing

The project uses the [MSTest framework](https://docs.microsoft.com/en-us/visualstudio/test/quick-start-test-driven-development-with-test-explorer) for testing the converter.

It is setup with two tests; one for setting up the API by registering the client to your WATS, and one for running the converter.

The values are hardcoded in the test, so you will need to change the values to reflect your setup.
* In SetupClient, fill in your information in the the call to RegisterClient.
* In TestConverter, fill in the path to the file you want to test the converter with. There are example files in the Examples folder.
* Run SetupClient once, then you can run TestConverter as many times as you want.

Note that the start date in the example files are going to be old, so in WATS you will have to set the From Date filter to see the report in Test Reports.

## License

This project is licensed under the [LGPLv3](COPYING.LESSER) which is an extention of the [GPLv3](COPYING).
