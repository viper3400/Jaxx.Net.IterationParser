# jaxx.net.iterationparser

## Usage

```cs
IIterationParser parser = new DefaultIterationParser();
List<IterationModel> result = parser.ParseTestResult(input, new DefaultIterationRegExSelector()):
```
Use with external Json configuration file:

```cs
IIterationParser parser = new DefaultIterationParser();
List<IterationModel>  result = parser.ParseTestResult(input, new JsonFileIterationRegExSelector("config.json")):

```

## Result

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

## Configuration

The following configuration is the same configuration as with the DefaultIterationRegExSelector.
SingleLineSelector:RegExSelector takes no "SelectedMatchGroup".

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