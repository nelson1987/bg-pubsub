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
Alice ->> Bob: Hello Bob, how are you?
Bob-->>John: How about you John?
Bob--x Alice: I am good thanks!
Bob-x John: I am good thanks!
Note right of John: Bob thinks a long<br/>long time, so long<br/>that the text does<br/>not fit on a row.

Bob-->Alice: Checking with John...
Alice->John: Yes... John, how are you?
```