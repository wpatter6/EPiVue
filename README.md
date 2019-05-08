# EPiVue

Simple integration of Vue.js web components and EPiServer blocks as a NuGet package in .NET standard.

- `IVueBlock` interface is used for building EPiServer blocks with Vue web components
- `IVueBlock.RenderBlock` extension method will render the HTML for the Vue web component when a block implements the `IVueBlock` interface.
- Configuration transform and `VueConfig` utility class are used to easily define where Vue script files live in web.config
- `HtmlHelper.RenderVueScripts` extension method can be used to easily define where Vue scripts should be rendered
- See `EPiVueTests/Models` for a basic implementation example.
