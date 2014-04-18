### Simple class for render html struct

#### HOW TO

##### create simple tag
```csharp
var result = String.Empty;
using (var _create = new SimpleTagBuilder()) 
{
	_create.Tag("div");
	result = _create.Render();
}

// output: <div></div>
```

##### create self-contained tag
```csharp
var _result = String.Empty;
using (var _create = new SimpleTagBuilder())
{
	_create.Tag("br", selfClose: true);
	_result = _create.Render();
}
// output: <br/>
```

##### create tag with attributes
```csharp
var _result = String.Empty;
using (var _create = new SimpleTagBuilder())
{
	_create.Tag("div", withAttributes: new { id = "testId", @class = "testClass" });
	_result = _create.Render();
}
// output: <div id='testId' class='testClass'></div>
```

##### create tag with nested tags
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

##### create tag with attribute without value
```csharp
var _result = String.Empty;
using (var _create = new SimpleTagBuilder())
{
	_create.Tag("div", withAttributes: new { ngApp = "" });

	_result = _create.Render();
}
// output: <div ng-app></div>
```

#### Some conventions
 - name *myBigAttr* transform to *my-big-attr*
 - don`t start attribute name with UpperCase
