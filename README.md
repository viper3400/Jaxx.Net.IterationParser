# jaxx.net.iterationparser

## Usage

```cs
IIterationParser parser = new DefaultIterationParser();
TestResultModel result = parser.ParseTestResult(input, new DefaultIterationRegExSelector()):

```

## Configuration

Use with external Json configuration file:

```cs
IIterationParser parser = new DefaultIterationParser();
TestResultModel result = parser.ParseTestResult(input, new JsonFileIterationRegExSelector("config.json")):

```

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
