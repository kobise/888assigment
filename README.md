# 888 Home Assigment - By Kobi Sekli


Welcome!
This is my project.

### Tech

The project contains 3 parts:

* [Angular 9] - Client app
* [.Net core] - Server side (including swagger)
* [Redis] - For caching.

### Docker Installation

The Docker will expose:
* Port 8080 - for [client-app]
* Port 8081 - for [server-app]
* Port 6479 - for redis

To run all the project:

```sh
docker-compose up
```
(This command will create & run the 3 images)


   [client-app]: <http://localhost:8080/>
   [server-app]: <http://localhost:8081/swagger>

