# DesafioATS
MVP de um sistema de ATS

### Gerar build das imagens docker
-> docker-compose -f docker-compose.build.yml build

### Efetuar deploy no docker (subir aplicação)
-> docker-compose up -d

### URL's
```
front - http://localhost/
api - http://localhost/api
swagger - http://localhost/swagger
```

##### A aplicação utiliza o DB MySql (mariadb) e MongoDB para logs


##### Arquivo 'Desafio ATS.postman_collection.json' para ser utilizado no postman


##### Subir imagens para o docker hub
editar o arquivo .env e alterar 
```
REGISTRY='usuario do docker hub'
TAG='versao da imagem'
```

##### Registros
```
Registrar um usuario do tipo 'Recrutador' para cadastrar, e alterar vagas, e listar curriculos
Registrar um usuario do tipo 'Candidato' para cadastrar, alterar curriculo, e se candidatar em vagas
```
