dotnet sonarscanner begin /k:"Teste" /d:sonar.host.url="http://localhost:9000" /d:sonar.login="883162e14a1e0028972e313bec33c832c5694bfa" -d:sonar.qualitygate.wait=true -d:sonar.cs.opencover.reportsPaths=./coverage/coverage.xml
dotnet build
dotnet test -p:CollectCoverage=true -p:CoverletOutputFormat=opencover -p:CoverletOutput=./coverage/coverage.xml
dotnet sonarscanner end /d:sonar.login="883162e14a1e0028972e313bec33c832c5694bfa"

docker run --rm -ti --network="host" -v `pwd`:/home/cicd build-image

docker run -d --name sonarqube -p 9000:9000 -p 9092:9092 sonarqube