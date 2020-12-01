local function configureProject(path)
	location(path)
	language "C#"
	dotnetframework 'net5.0'
    architecture "universal"
	enabledefaultcompileitems(true)
	nuget {
    	"Serilog:2.10.0",
    	"Serilog.Sinks.File:4.1.0",
    	"Serilog.Sinks.Console:3.1.1",
		'Serilog.Sinks.Debug:1.0.1',
    }
    analyzers {
    	"Roslynator.Analyzers:3.0.0",
    	"Microsoft.VisualStudio.Threading.Analyzers:16.0.102",
    }

	files {
		path .. '/Inputs/**',
	}
	filter {"files:**/Inputs/**"}
		buildaction "Copy"
	filter {}

	filter {"configurations:Release"}
		optimize "On"
	filter{}
end

function common(name, isWpf)
	local calleePath = debug.getinfo(2, "S").source:sub(2)
	project(name)
	configureProject(path.getdirectory(calleePath))
    kind "SharedLib"

    if isWpf then
	    flags {
			'WPF',    		
		}
	end
end

function solution(name)
	local calleePath = debug.getinfo(2, "S").source:sub(2)
	project(name)
	configureProject(path.getdirectory(calleePath) .. '/Solution/')
    kind "ConsoleApp"
    links {
    	'Common'
    }
end

function benchmark(name)
	local calleePath = debug.getinfo(2, "S").source:sub(2)
	project(name .. "_Benchmark")
	configureProject(path.getdirectory(calleePath) .. '/Benchmark/')
    kind "ConsoleApp"
    nuget {
    	'BenchmarkDotNet:0.12.1',
    }
    links {
    	'Common',
    	name
    }
end

function wpf(name)
	local calleePath = debug.getinfo(2, "S").source:sub(2)
	project(name .. "_Wpf")
	configureProject(path.getdirectory(calleePath) .. '/WPF/')
    kind "WindowedApp"
	flags {
		'WPF',    		
	}
	nuget {
		"ReactiveUI:12.1.5",
		"ReactiveUI.WPF:12.1.5",
		"ReactiveUI.Events.WPF:12.1.5"
	}
    links {
    	'Common',
    	name
    }

end