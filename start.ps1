Write-Host "hello"
cd C:\tools\neo4j-community\neo4j-community-3.5.1 
$ENV:JAVA_HOME = 'C:\tools\neo4j-community\neo4j-community-3.5.1\java\jdk1.8.0_131'
bin\neo4j start
bin\neo4j stop
bin\neo4j console 
Pause
