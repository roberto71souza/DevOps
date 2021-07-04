# Projeto DevOps

Este projeto contem uma simples Api com métodos get,post,put e delete, para buscar, cadastrar, alterar 
e excluir animais. Esta api serve de base para execução de testes unitários com o framework XUnit,e para
deploy no Azure, utilizando a ferramenta DevOps.
Para fazer o deploy desta Api que esta versionada no github, foi criado uma pipeline no Azure DevOps, 
integrando o projeto que estava no github com o Azure DevOps, nesta pipeline a cada build foi configurado 
para que os testes sejam rodados,e criação de artefato. Com este artefato e criado um release, que e o deploy 
desta api com integração continua no Azure


## Tecnologias usadas no projeto
.Net Core, Api, testes unitarios com XUnit, Azure e Azure DevOps.
