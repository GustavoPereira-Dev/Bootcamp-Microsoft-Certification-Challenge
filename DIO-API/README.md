# Como Fazer o Deploy de uma API na Nuvem na Prática

Esse diretório conta com o desenvolvimento e passo a passo de como hospedar uma API do .NET (C#) com Container (Docker) para a Nuvem do Azure

É um processo que leva tempo e tem bastante tópicos específicos, então terá algumas divisões por cabeçalho

Glossário aqui

## Criando o Repositório 

Antes de tudo, tenho que salientar que local de gerenciamento e uso de Workflows que foi usado para isso não foi o Github daqui exatamente, mas sim o "Azure DevOps" (plataforma da Microsoft que oferece um conjunto de recursos e serviços para otimizar o ciclo de desenvolvimento e aplicação de software com enfoque em especial no próprio Azure). Ainda assim é possível utilizar o Github para isso caso já tenha conhecimento prévio em especial na parte dos Workflows, mas que pode diferir de algumas coisas em relação as feitas aqui pelo DevOps

Inicialmente para acessá-lo, você entrar no Portal do Azure e pesquisar "Azure DevOps" para entrar na conta e se cadastrar nela

![alt text](./asssets/image.png)

Após toda a parte de criação da conta e da organização (exemplo abaixo da criação de organização) você pode fazer o projeto (ou repositório)

![alt text](./asssets/image-1x.png)

Clique em "new project" e siga os passos a passos

![alt text](<./asssets/image-2.png>)

![alt text](<./asssets/image-3.png>)

Criando o projeto, você tem acesso ao link desse repositório para desenvolver em sua IDE preferida. Recomendo o uso do Visual Studio mas, aqui diferente da parte do Github Workflow, tem o passo a passo para ambas as opções

![alt text](<./asssets/image-4.png>)


### Utilizando o Visual Studio

Pelo Visual Studio ter mais integração com a linguagem C# e seu Framework .NET, o processo de criação e também desenvolvimento é bem mais simples e prático que no VSCode, aqui está a criação (a lib especial utiliza é a "ASP.NET Core Web API"). Lembre de passar o diretório que está o repositório local do projeto no seu computador

![alt text](<./asssets/image-5.png>)

![alt text](<./asssets/image-6.png>)

### Utilizando o VSCode

No VSCode, clone o projeto/repositório e desenvolva uma WebAPI na pasta root dele

![alt text](<./asssets/image-7.png>)

A estrutura de pasta ficará em um formato semelhante a esse: 

![alt text](<./asssets/image-8.png>)

Para executar a API e utilizá-la pelo localhost faça os seguintes comandos

![alt text](<./asssets/image-9.png>)

### DockerFile

O Dockerfile (que ficará na pasta root do projeto da API, não confundir com a root do repositório) ficará mais ou menos nesse formato:

![alt text](<./asssets/image-10.png>)

## Desenvolvendo

Abaixo está exemplos do resultado que será disparado ao no link do localhost

![alt text](<./asssets/image-11.png>)

![alt text](<./asssets/image-12.png>)

![alt text](<./asssets/image-13.png>)

Crie o WeatherForecast.cs na mesma pasta do Program.cs

![alt text](<./asssets/image-14.png>)

O resultado será este se você ir para o "/WeatherForecast"

![alt text](<./asssets/image-15.png>)

![alt text](./asssets/image-16.png)

Após todas as adições e alterações, vamos commitar para o projeto do Azure DevOps:

![alt text](<./asssets/image-17.png>)

![alt text](<./asssets/image-18.png>)

![alt text](<./asssets/image-19.png>)

Dessa forma, quando entrar no projeto, a estruturação dos arquivos próximo dessa forma aqui

![alt text](<./asssets/image-20.png>)

### Criando recursos

Indo para o Azure (a Cloud), vamos criar respectivamente um grupo de recursos (resource group), Container de Registros (Container Registry) e um Aplicativo Web (Web App)

![alt text](<./asssets/image-21.png>)

![alt text](<./asssets/image-22.png>)

![alt text](<./asssets/image-23.png>)


### Pipeline

Voltando ao Azure DevOps, vamos ao desenvolvimento da parte mais complexa do lab, o desenvolvimento da Pipeline (conjunto de instruções utilizados para se dar Deploy no projeto)

Dentro do projeto (repo) crie uma Pipeline e selecione onde está o código, repositório e a tipo de configuração da Pipeline (Starter Pipeline) e o código inicial de sua Pipeline

![alt text](<./asssets/image-24.png>)

![alt text](<./asssets/image-25.png>)

![alt text](<./asssets/image-26.png>)


![alt text](<./asssets/image-27.png>)

![alt text](<./asssets/image-28.png>)

Assim, faça a primeira execução do Workflow e verifique se foi tudo concluído com sucesso

![alt text](<./asssets/image-29.png>)

Depois, adicione as variáveis que serão utilizas depois e confirme novamente se tudo foi com êxito

![alt text](<./asssets/image-30.png>)

![alt text](<./asssets/image-31.png>)

![alt text](<./asssets/image-32.png>)

#### Service Connection

Crie o serviço de conexão do projeto (configurações/engrenagem > Service Conenctions) do Docker Register pelo Container de Registro criado pela Cloud da Azure

![alt text](./asssets/image-33.png)

![alt text](<./asssets/image-34.png>)

![alt text](<./asssets/image-35.png>)


![alt text](<./asssets/image-36.png>)

Dentro da edição do Workflow, vá para a parte de Tasks,0 escolher Docker e siga os seguintes passos: 

![alt text](<./asssets/image-37.png>)

![alt text](<./asssets/image-38.png>)

![alt text](<./asssets/image-39.png>)

![alt text](<./asssets/image-40.png>)

Nota: o task abaixo deve ser o último de todos

![alt text](<./asssets/image-41.png>)

Após executar o Workflow novamente, você verá um Repositório no ACR criado do seu Azure pela Service Connection desenvolvida

![alt text](<./asssets/image-42.png>)

![alt text](<./asssets/image-43.png>)

#### Webhooks

Vamos criar uma Weebhook no ACR com o Repositorie criado

Antes disso, colete a url de Webhook do Web App

![alt text](./asssets/image-44.png)

Agora indo para Webhooks do ACR, vamos criar ela

![alt text](<./asssets/image-45.png>)

![alt text](<./asssets/image-46.png>)

#### Pré configurações do Deployment Center

Antes de ser acessível o Deploy pela parte do Azure, precisamos fazer algumas coisas ainda:

- Ative o modo admin do ACR

![alt text](<./asssets/image-47.png>)

Ative o SCM do Web App 

![alt text](<./asssets/image-48.png>)

### Deployment Center

Indo para o Centro de implementação, estamos próximo do final desse lab. Vamos implementar o Deploy agora:

![alt text](<./asssets/image-49.png>)

![alt text](<./asssets/image-50.png>)

Salve essa configuração 

### Trigger

Na última parte, vamos ativar um Trigger para que caso haja alguma alteração no Projeto do DevOps, seja feito a build e o deploy novamente do repositório para o Azure (vá para a edição do Workflow clique nos 3 pontos e vá para trigger)

![alt text](<./asssets/image-51.png>)

Selecione essa opção, cilque em "Save & Queue" e depois "Save"

![alt text](<./asssets/image-52.png>)

Vamos testar isso. Atualize o Process.cs (primeira imagem abaixo) no final. Possivelmente é algo necessário somente para quem fez no VSCode

![alt text](<./asssets/image-53.png>)

Para ambas as IDEs, atualize o WeatherForecast.cs

![alt text](<./asssets/image-54.png>)

Agora vamos dar commit e dar push para o Projeto

![alt text](<./asssets/image-55.png>)

![alt text](<./asssets/image-56.png>)

Indo agora para a execução do Workflow (o Job) é possível ver que o Trigger realmente funcionou e está atualizando tanto o Deploy do Azure DevOps quanto o da Cloud, atualizando o site hospedado também pela mesma

![alt text](<./asssets/image-57.png>)

![alt text](<./asssets/image-58.png>)

Contudo, como não existe umma opção de latest (dar o Deploy sempre pela última alteração), o site ainda está na versão anterior, devendo assim atualizar a nova versão no Centro de Implementação do Web App

![alt text](<./asssets/image-59.png>)


O resultado agora será esse abaixo:

![alt text](./asssets/image-60.png)


Assim, foi feito o Deploy de uma API na Nuvem do Azure!


