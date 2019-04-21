# verizon-connect

## Install Redis
docker run --name vc-redis -d -p 6379:6379 redis

## Install RabbitMQ
docker run --name vc-rabbit -d -p 15672:15672 -p 5672:5672 rabbitmq:3-management

## Install Receiver
sc create Verizon.Connect.Receiver binPath=""

## Install QueryServer
sc create Verizon.Connect.QueryService binPath=""

## Run Sender
Verizon.Connect.Sender.exe -v 10 -interval 500

## Tests

### Sender Tests

- It was created Unit Tests
- It was created Verizon.Connect.Sender.Loader that create 100 Tasks of Sender and start to produce new Plots


### Receiver Tests
1. First was tested with only one Receiver with one Consumer (Release Mode)

Results:
Around 1k messages/s

2. First was tested with only one Receiver with 5 Consumer (Release Mode)

Results:
Around 3.3k messages/s

3. First was tested with two one Receiver with 5 Consumer (Release Mode)

Results:
Around 3.3k messages/s