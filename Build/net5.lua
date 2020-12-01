
premake.override(premake.vstudio.dotnetbase, "isNewFormatProject", function(base, cfg)
	local framework = cfg.dotnetframework
	if not framework then
		return false
	end
		
	if framework:find('^net') ~= nil then
		return true
	end

	return base(cfg)
end)