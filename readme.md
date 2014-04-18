##### Simple class for render html struct

##### USE example

```csharp
var result = String.Empty;
using (var _create = new SimpleTagBuilder()) 
{
	_create.Tag("div");
	result = _create.Render();
}

// output: <div></div>
```

```csharp
var _result = String.Empty;
using (var _create = new SimpleTagBuilder())
{
	_create.Tag("br", selfClose: true);
	_result = _create.Render();
}
// output: <br/>
```

```csharp
var _result = String.Empty;
using (var _create = new SimpleTagBuilder())
{
	_create.Tag("div", withAttributes: new { id = "testId", @class = "testClass" });
	_result = _create.Render();
}
// output: <div id='testId' class='testClass'></div>
```

```csharp
var _result = String.Empty;
using (var _create = new SimpleTagBuilder())
{
	_create.Tag("div", withAttributes: new { id = "testId", @class = "testClass" }, withChildren: () => 
	{
		_create.Tag("span", withChildren: () =>
		{
			_create.Text("hello world");
		});
	});
	_result = _create.Render();
}
// output: <div id='testId' class='testClass'><span>hello world</span></div>
```
