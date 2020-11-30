dofile('analyzers.lua')
dofile('utils.lua')
dofile('manifest.lua')

workspace "Advent of Code 2020"
	configurations { "Debug", "Release" }
	discoverProjects(path.getabsolute('../Source'))