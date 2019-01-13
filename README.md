# Jaxx.Net.IterationParser

## Basic Usage

```cs
IIterationParser parser = new DefaultIterationParser();
List<IterationModel> result = parser.ParseTestResult(input, new DefaultIterationRegExSelector()):
```
Use with external Json configuration file:

```cs
IIterationParser parser = new DefaultIterationParser();
List<IterationModel>  result = parser.ParseTestResult(input, new JsonFileIterationRegExSelector("config.json")):
```

## Basic Result

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

## Basic Test Iteration Config

The following configuration is the same configuration as with the DefaultIterationRegExSelector.
SingleLineSelector:RegExSelector takes no "SelectedMatchGroup".

You could use it with ```JsonFileIterationRegExSelector```

```json
{
  "RegularExpressionFilters": {
    "SingleLineSelector": {
      "RegExSelector": {
        "Selector": "\r\n"
      }
    },
    "IterationRegExSelector": {
      "TestIterationCountSelector": {
        "RegExSelector": {
          "Selector": "^.*?TL( |)(\\d{1,2}).*?;",
          "SelectedMatchGroup": "2"
        }
      },
      "TestIterationDateSelector": {
        "RegExSelector": {
          "Selector": "^.*?;.*?(\\d{2}.\\d{2}.(20\\d{2}|\\d{2}))",
          "SelectedMatchGroup": "1"
        }
      },
      "TestIterationResultSelector": {
        "RegExSelector": {
          "Selector": "^.*?;.*?;(.*?)$",
          "SelectedMatchGroup": "1"
        }
      },
      "TestIterationTypeSelector": {
        "RegExSelector": {
          "Selector": "^(.+?);",
          "SelectedMatchGroup": "1"
        }
      }
    }
  }
}
```

## Generic Approach

Use with generic, external Json Configuration

```cs
var parser = new DefaultIterationParser();
var config = new JsonFileGenericRegExSelector("GenericRegExSelectorConfiguration.json")
List<Dictionary<string,string> = parser.ParseTestResult(input, config.RegExSelectors);
```

Instead of the strongly typed ```List<IterationModel>``` we will get a ```List<Dictionary<string, string>>``` where the key of Dictionary will be the "Name" property of the RegExSelectors config (see below).

You could use it with ```JsonFileGenericRegExSelector```
Compared to basic json config this one has just an RegExSelectors array which holds generic regular expression and the match group, identified by a "Name" property. Name must be unique!

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
      }
    ]
  }
}
```

