# Jaxx.Net.IterationParser

This library will split a (multiline) text by a given line delimiter into an array of strings. Than each of the strings (lines) will be parsed by a regular expression.

The parser could be configured in a json file.

## Basic Usage

```cs
IGenericParser parser = new GenericParser();
var config = new JsonFileGenericRegExSelector("config.json")
List<Dictionary<string,string> = parser.ParseTestResult(input, config.RegExSelectors);
```

The result will be a ```List<Dictionary<string ,string>```. The key returns the "Name" tag from configuration, the value returns the matching regular expression.

## Configuration (Json)

Configuration is loaded into ```JsonFileGenericRegExSelector``` class. The configuration uses the ```RegExSelector``` model.

With "SingleLineSelector" you could define the line delimiter which will be used to split the input string into the lines. This option has no name and no match group property.

In the "RegExSelectors" array you could define the names and the regular expression which will be used int the result dictionary.


```json
{
  "RegularExpressionFilters": {
    "SingleLineSelector": {
      "RegExSelector": {
        "Selector": "\r\n"
      }
    },
    "RegExSelectors": [
      {
        "Name": "IterationCount",
        "Selector": "^.*?TL( |)(\\d{1,2}).*?;",
        "SelectedMatchGroup": "2"
      },
      {
        "Name": "IterationDate",
        "Selector": "^.*?;.*?(\\d{2}.\\d{2}.(20\\d{2}|\\d{2}))",
        "SelectedMatchGroup": "1"
      },
      {
        "Name": "IterationResult",
        "Selector": "^.*?;.*?;(.*?)$",
        "SelectedMatchGroup": "1"
      },
      {
        "Name": "IterationType",
        "Selector": "^(.+?);",
        "SelectedMatchGroup": "1"
      },

      {
        "Name": "IterationLine",
        "Selector": "^(.+?)$",
        "SelectedMatchGroup": "1"
      }
    ]
  }
}
```

The example above will parse the follwing input string 

```
QA TL1;01.01.2019;FAILED
QA TL2;03.03.2019;PASSED
```

into

```cs
List<Dictionary<string,string>>
{
  Dictionary<string,string>
  {
    { Key = "IterationCount", Value = "1" },
    { Key = "IterationDate", Value = "01.01.2019" },
    { Key = "IterationResult", Value = "FAILED" },
    { Key = "IterationType", Value = "QA TL1" },
    { Key = "IterationLine", Value = "QA TL1;01.01.2019;FAILED" },
  },
  Dictionary<string,string>
  {
    { Key = "IterationCount", Value = "2" },
    { Key = "IterationDate", Value = "03.03.2019" },
    { Key = "IterationResult", Value = "PASSED" },
    { Key = "IterationType", Value = "QA TL2" },
    { Key = "IterationLine", Value = "QA TL2;03.03.2019;PASSED" }
  }
}

```

## Typed Result

IterationParser is orginally intented to parse test iteration results from a single field in an issue tracking tool. Instead of using the generic Dictionary approach from above, you can parse the text into a ```List<IterationModel>```.

```cs
public class IterationModel
{
    public DateTime IterationDate { get; set; }
    public string IterationType { get; set; }
    public int IterationCount { get; set; }
    public string IterationResult { get; set; }
    public string IterationLine { get; set; }
}
```

* Iteration Line contains the complete and unparsed string.
* If iteration line could not be parsed with the given RegEx in the configured selector:
  * IterationDate will be set to 01.01.0001
  * IterationCount will be set to 0
  * IterationResult and IterationResult will be set to ""


Usage:

```cs
IIterationParser parser = new IterationModelParser();
List<IterationModel>  result = parser.ParseTestResult(input, new JsonFileGenericRegExSelector"config.json")..RegExSelectors):
```

There is a basic ```DefaultIterationRegExSelector``` available which could be used as default configuration (without external json cofiguration file). The configuration example from above matches the default configuration.

```cs
IIterationParser parser = new IterationModelParser();
List<IterationModel> result = parser.ParseTestResult(input, new DefaultIterationRegExSelector()):
```


