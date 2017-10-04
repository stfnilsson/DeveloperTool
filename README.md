
# Developer Tools

Nuget package
https://www.nuget.org/packages/UWPDevTools/

## UWP ##

XamlGrid for UI alignment (Composition api support is required)

This control adds lines (defined by the developer) to an app without blocking the UI behind. This could help developers to improve the alignments of the UI elements. It's a complement to the Xaml Designer.

![Alt text](/../master/DeveloperTool/images/XamlGridTool.PNG?raw=true "Optional Title")

### Sample ###
```c#
<XamlGridControl.Lines = new List<XamlGridLine>
{
    new XamlGridLine
    {
        GridColor = Colors.Red,
        VerticalStep = 0,
        HorizontalStep = 24
    },
    new XamlGridLine
    {
        GridColor = Colors.Gold,
        VerticalStep = 48,
        HorizontalStep = 0
    },
    new XamlGridLine
    {
        GridColor = Colors.LightGray,
        VerticalStep = 12,
        HorizontalStep = 12
    }
};
```
### Video ###
https://github.com/stfnilsson/DeveloperTool/blob/master/DeveloperTool/images/XamlGrid.mp4
