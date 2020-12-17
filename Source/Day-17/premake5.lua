solution('Day17')
	dotnetframework 'netcoreapp3.1'
	nuget {
		'ComputeSharp:1.4.1',
	}
	removelinks {
		'Common',
	}

benchmark('Day17')
	dotnetframework 'netcoreapp3.1'
	removelinks {
		'Common',
	}