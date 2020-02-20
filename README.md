# DASA - Challenge

## .Net Core 3.1 setup

```
cd src/Api
dotnet restore

cd src/catalogo
dotnet restore

cd src/CrossCutting.IoC
dotnet restore

cd src/Data
dotnet restore

cd src/WebScrap
dotnet restore

```

## Vue setup

```
cd src/ui
npm i
```

### Connection String Postgres

```
Criar base de dados "dasa"
Acessar arquivo src/api/appsettings.json
Alterar marcação ConnectionStrings:default, preencher CS Npgsql, usuário deve ter permissão para criação de banco de dados.
Banco de dados será criado ao iniciar aplicação
```

### Execução

```
cd src/api
dotnet run

cd src/ui
npm run serve
```
