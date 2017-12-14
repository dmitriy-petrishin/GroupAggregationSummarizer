# Group Operations Library
Flexible library for operations like grouping, aggregation, summarizing.

## Instruction for using.
1. + Get inherited from GroupOperationsBase.cs in object you need to be grouped, averaged, summarized.
   + Implement constructor chain for child class.

```CSharp
public class Data : GroupOperationsBase
{
    public Data() { }

    public Data(
        IGrouping<object, GroupOperationsBase> previouslyGroupedList, 
        IEnumerable<string> groupFieldNameList, 
        IEnumerable<string> averageFieldNameList = null, 
        IEnumerable<string> sumFieldNameList = null) 
        : base(previouslyGroupedList, groupFieldNameList, averageFieldNameList, sumFieldNameList)
    {
    }
}
```
2. Create new instance of GroupOperationsLibrary class and specify object type you want to be grouped and etc.

```CSharp
var provider = new GroupOperationsProvider<ExtendedData>();
```

3. Call `Execute` method and specify list of the field's names you want to be grouped, averaged, summarized.
```CSharp
IEnumerable<ExtendedData> result = provider.Execute(
    list,
    new List<string> { "Name" },
    new List<string> { "Price" },
    new List<string> { "Quantity", "StockQuantity" });
```

P.S. You can see and debug live examples in unit tests of the project.



