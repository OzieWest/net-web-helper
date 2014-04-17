##### Simple class for render html struct

##### USE example
```csharp
var result = String.Empty;
using (var sut = new SimpleTagBuilder())
{
	sut.Tag("div", withAttributes: new { id = "testId", @class = "testClass" }, withChildren: () => 
	{
		sut.Tag("span", withChildren: () =>
		{
			sut.Text("hello world");
		});
	});
	result = sut.Render();
}
```
