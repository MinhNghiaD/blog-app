-include .env

PROJECTNAME := $(shell basename "$(PWD)")

.PHONY: update-datbase
update-vendor:
	@echo "  > Update databse" 
	@dotnet database update

.PHONY: build
build:
	@echo "  >  Building binary..."
	@dotnet build