require("vstudio")

premake.api.register {
    name = "analyzers",
    scope = "project",
    kind = "list:string",
    tokens = true,
}

premake.override(premake.vstudio.dotnetbase, "packageReferences", function(base, prj)
    base(prj)

    for i = 1, #prj.analyzers do
        local package = prj.analyzers[i]
        _p(1,'<ItemGroup>')
        _p(2,'<PackageReference Include="%s" Version="%s" PrivateAssets="All">', premake.vstudio.nuget2010.packageId(package), premake.vstudio.nuget2010.packageVersion(package))
        _p(3,'<PrivateAssets>all</PrivateAssets>')
        _p(3,'<IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>')
        _p(2,'</PackageReference>')
        _p(1,'</ItemGroup>')
    end
end)