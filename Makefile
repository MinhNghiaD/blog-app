-include .env

PROJECTNAME := $(shell basename "$(PWD)")

.PHONY: database
database:
	@echo "  > Update databse" 
	@dotnet ef database update

.PHONY: build
build:
	@echo "  >  Building binary..."
	@dotnet build