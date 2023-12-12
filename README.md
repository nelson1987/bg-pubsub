# bg-pubsub

Criacao de projeto teste PubSub
aliando a eventual necessidade de ter um producer e um consumer em um único Pod.

```sh
docker compose up
cd BG.PubSub.Api/
dotnet run
```

```http
localhost:15672
```
# user
guest
# password
guest

```mermaid
sequenceDiagram
CriarContaCommand ->> CriarContaCommandHandler: Hello Bob, how are you?
CriarContaCommandHandler-->>John: How about you John?
CriarContaCommandHandler--x CriarContaCommand: I am good thanks!
CriarContaCommandHandler-x John: I am good thanks!
Note right of John: Bob thinks a long<br/>long time, so long<br/>that the text does<br/>not fit on a row.

CriarContaCommandHandler-->CriarContaCommand: Checking with John...
CriarContaCommand->John: Yes... John, how are you?
```