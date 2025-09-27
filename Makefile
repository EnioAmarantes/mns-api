PROJECT = mns.csproj

build:
	dotnet build $(PROJECT)

test:
	dotnet test

migrate:
	ASPNETCORE_ENVIRONMENT=Development dotnet ef database update

migrate-prod:
	ASPNETCORE_ENVIRONMENT=Production dotnet ef database update --no-build

migration-create:
	@if [ -z "$$name" ]; then \
		echo "Use: make migration-create name=NomeDaMigration"; \
		exit 1; \
	else \
		ASPNETCORE_ENVIRONMENT=Development dotnet ef migrations add $$name; \
	fi

.PHONY: build test migrate migration-create