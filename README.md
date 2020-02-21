# DASA - Challenge

## API .Net Core 3.1 setup

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
1. Criar base de dados "dasa"
2. Acessar arquivo src/api/appsettings.json
3. Alterar marcação ConnectionStrings:default, preencher CS Npgsql, usuário deve ter permissão para criação de banco de dados.
4. Schemas do banco de dados serão criados na inicialização da aplicação.
```

### Execução

```
cd src/api
dotnet run

cd src/ui
npm run serve
```

### Utilização
```
Após acessar a URL **http://localhost:8080/**, para inicializar o webscraping siga os paassos abaixo
1. Clicar no icone COG (canto superior direito), clicar
2. Clicar no botão (Automatização)Iniciar
3. Escolher "Sim" na caixa de dialogo "Deseja iniciar o scraping imediatamente?"

Essa ação irá realizar uma carga inicial dos dados, demora entre 1h ou 2hs para realizar a operação.
Se preferirem um teste mais rápido, desativem as lojas "Posthaus" e "VKModas".

Após finalização da carga, o auto complete de categorias estará pronto para uso.

Foi utilizado o Hangfire para o agendamento da operação diária de webscraping com inicialização as 01:00 AM.
```
