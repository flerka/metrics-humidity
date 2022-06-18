I created this app to monitor the temperature and humidity in my apartment during the night. The app pushes temperature and humidity sensors data to Prometheus. deCONZ is used as ZigBee gateway to communicate with sensors. 

The app is written in C# with .Net 5. I configured CI in Github Actions, and Docker images are stored on Docker Hub. The app is hosted on Raspberry Pi on Linux.

## How to start
- get deCONZ [API key](https://dresden-elektronik.github.io/deconz-rest-doc/getting_started/#acquire-an-api-key)
- edit `.env` file with your Deconz URL and API key
- if needed, edit `/prometheus/prometheus.yml` to change the schedule
- run in console `docker-compose up --build` in app root folder
- go to http://localhost:3000/ and add Prometheus data source (prometheus:9090 in my case)
- add labels to Grafana 

## How it will look like
![main](https://raw.githubusercontent.com/flerka/metrics-humidity/main/.github/img/1.png)