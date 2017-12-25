# MyMapper

**MyMapper port to .NET Standard 2.0**

**Light-weight, powerful object mapping framework. Fluent design.**
**Supports .NET, .NET Standard 2.0 and .NET Core 2.0**

.NET Fiddle: **https://dotnetfiddle.net/1WB7py**

Please read SampleMapper.cs code file to learn how to create mappers using MyMapper framework.

Please read SampleMapper-WithExtensions.cs code file to learn how to create mappers using MyMapper framework extensions.

Codefile EntitiesSampleMapper.cs contains the source and destination entities used in the sample mapper.

Features:

1.	Fluent design.
2.	Async support. Parallel mapping support.
3.  Conditional, Switch mapping support.
5.	Mappings can be debugged by either setting a breakpoint or using Debugger.Break().
6.	Ability to do auto mapping of source and destination properties (with the same name) using reflection 
	and add maps only for properties with different names. Reflective auto mapping can be turned off too.
4.	Ability to create multiple maps between the same source and destination types.
7.	Mappings happen dynamically on source instance.
8.	Ability to harness other maps. Other mappers can be chained as required.
9.	Dependency injectable.
10.	.NET Object and IEnumerable`<T>` integration extensions. Async support.
11.	Feature to map source to existing destination object.