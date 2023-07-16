-include .env

PROJECTNAME := $(shell basename "$(PWD)")

.PHONY: database
database:
	@echo "  > Update database..." 
	@dotnet ef database update

.PHONY: build
build:
	@echo "  >  Building binary..."
	@dotnet build

.PHONY: run
run:
	@echo "  >  Launching website..."
	@dotnet run