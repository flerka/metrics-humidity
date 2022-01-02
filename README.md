This app push Deconz sensors data to Prometheus and visualize them with Grafana. 

## How to start:
- get Deconz api key - https://dresden-elektronik.github.io/deconz-rest-doc/getting_started/#acquire-an-api-key
- edit .env file with your Deconz url and api key
- if needed edit \prometheus\prometheus.yml to change schedule
- run in console docker-compose up --build
- go to http://localhost:3000/ and add prometheus data sourse (prometheus:9090 in my case)
- add labels to Grafana 

## How it will look like
![main](https://raw.githubusercontent.com/flerka/metrics-humidity/main/.github/img/1.png)
