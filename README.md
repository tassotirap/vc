# verizon-connect

## Install Redis
docker run --name vc-redis -d -p 6379:6379 redis

## Install RabbitMQ
docker run --name vc-rabbit -d -p 15672:15672 -p 5672:5672 rabbitmq:3-management

## Install Receiver
sc create Verizon.Connect.Receiver binPath=""

## Install QueryServer
sc create Verizon.Connect.QueryService binPath="D:\Projetos\vc\src\Verizon.Connect.QueryService\bin\Debug\netcoreapp2.2\win7-x64\Verizon.Connect.QueryService.exe"

## Run Sender