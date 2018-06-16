# MyMapper

**MyMapper port to .NET Standard 2.0**

**Light-weight, powerful object mapping framework. Fluent design.**
**Supports .NET, .NET Standard 2.0 and .NET Core 2.0**

[**.NET Fiddle demo**](https://dotnetfiddle.net/1WB7py)

[**Wiki Documentation**](https://github.com/VeritasSoftware/MyMapperNetStandard2.0/wiki/Using-MyMapper-extensions)

Please read SampleMapper.cs code file to learn how to create mappers using MyMapper framework.

Please read SampleMapper-WithExtensions.cs code file to learn how to create mappers using MyMapper framework extensions.

Codefile EntitiesSampleMapper.cs contains the source and destination entities used in the sample mapper.

Features:

*	Fluent design.
*	Async support. Parallel mapping support.
*	Conditional, Switch mapping support.
*	Mappings can be debugged by either setting a breakpoint or using Debugger.Break().
*	Ability to do auto mapping of source and destination properties (with the same name) using reflection 
	and add maps only for properties with different names. Reflective auto mapping can be turned off too.
*	Ability to create multiple maps between the same source and destination types.
*	Mappings happen dynamically on source instance.
*	Ability to harness other maps. Other mappers can be chained as required.
*	Dependency injectable.
*	.NET Object and IEnumerable`<T>` integration extensions. Async support.
*	Feature to map source to existing destination object.