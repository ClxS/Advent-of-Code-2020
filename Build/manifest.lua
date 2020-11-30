local function internalProcessHierarchy(hierarchy, groupName)
	local groups = {}

	group(groupName)
	for k, v in pairs(hierarchy) do
		if type(v) == 'table' then
			groups[k] = v
		else
			print(v)
			include(v)
		end
	end

	for k, v in pairs(groups) do
		internalProcessHierarchy(v, groupName .. '/' .. k)
	end	
end

local function split (inputstr, sep)
        if sep == nil then
                sep = "%s"
        end
        local t={}
        for str in string.gmatch(inputstr, "([^"..sep.."]+)") do
                table.insert(t, str)
        end
        return t
end

function discoverProjects(sourceRoot)
	local fileMatch = path.join(sourceRoot, '*/premake5.lua');
	local result = os.matchfiles(fileMatch);

	local projects = {}
	print('Looking for projects in ' .. sourceRoot)
	for _,v in ipairs(result) do
		local prjdir = path.getdirectory(v)
		local prjname = path.getname(v)
		local parts = split(v, '/')
		local prjName = parts[#parts - 1]

		print('Adding project ' .. prjName)

		projects[prjName] = {
			v
		}
	end

	internalProcessHierarchy(projects, '')
end