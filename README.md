# Projeto NHMatsumotoDemo

## O projeto NHMatsumotoDemo é um repositório de código aberto que utiliza as seguintes tecnologias e padrões de design:

    Tecnologias: .NET Core 6, MariaDB, C#, Entity Framework
    Padrões de Design: Filter Partner, Repository

## Essas tecnologias e padrões foram escolhidos para fornecer uma base sólida e moderna para o projeto, permitindo um desenvolvimento eficiente e a adoção de boas práticas de programação.

## Seja bem-vindo ao projeto e aproveite a jornada de aprendizado e compartilhamento de conhecimento!

# O projeto é um monolito que segue uma arquitetura de camadas, composto pelas seguintes camadas:

# API: 
 ## A camada de API é responsável por expor endpoints para interação com o sistema. Aqui, são definidos os controladores e as rotas para lidar com as requisições dos clientes.

# Services: 
 ## A camada de Services contém a lógica de negócio da aplicação. Aqui, são implementadas as regras de negócio e a manipulação dos dados recebidos pela API.

# Infrastructure: 
 ## A camada de Infrastructure abrange aspectos de infraestrutura do sistema. Aqui, são incluídos componentes como persistência de dados, acesso a serviços externos, comunicação com o banco de dados e outros recursos relacionados à infraestrutura da aplicação.

# CrossCuttingIoC: 
## A camada de CrossCuttingIoC (Inversão de Controle) é responsável por prover a injeção de dependências e lidar com aspectos transversais da aplicação. Ela abrange funcionalidades como autenticação, autorização, log, gerenciamento de configurações e outros aspectos que permeiam várias camadas da aplicação. Através da inversão de controle, essa camada permite que as dependências sejam injetadas de forma centralizada e gerenciada, facilitando a manutenção e o teste da aplicação.

# Database: 
 ## A camada de Database é responsável pelo acesso e persistência dos dados. Aqui, são definidos os modelos de dados e as operações de leitura/gravação no banco de dados.

## Essas camadas trabalham em conjunto dentro do monolito, permitindo que a aplicação seja desenvolvida, testada e implantada como uma única entidade coesa. Embora o monolito tenha suas vantagens e desvantagens, ele oferece uma abordagem simplificada para o desenvolvimento e a manutenção do sistema.

## Fique à vontade para explorar o projeto NHMatsumotoDemo e entender melhor como essas camadas interagem para fornecer a funcionalidade geral do sistema.
