# jaxx.net.iterationparser

## Usage

```cs
IIterationParser parser = new DefaultIterationParser();
TestResultModel result = parser.ParseTestResult(input, new DefaultIterationRegExModel()):

```

## Configuration

```
{
  "RegularExpressionFilters" : {
    "SingleLineSelector" : {
      "Regex" :"^.*$",
      "MatchGroup" : "1"
      },
      {
      "InlineTestDateSelector" : {
         "Regex"\d{2}.\d{2}.20\d{2}",
         "Matchgroup": "2";
       }
     }
  }
}
```
