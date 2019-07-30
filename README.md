# README

This is a simple example of how to get Grafana / InfluxDb running and preloaded with your own dashboards. It is an extension of [this work](https://github.com/samuelebistoletti/docker-statsd-influxdb-grafana).

Execute `runbuild.sh` to build and execute container. 

You can then navigate to [localhost:3003](http://localhost:3003) and log in as root / root.

When you make edits to the dashboards export the changes as described [here](https://grafana.com/docs/reference/export_import/) and overwrite mydash.json with the contents. If you create other files similar to mydash.json you can put multiple dashboards into the image. You will need to update the DOCKERFILE and rerun the build.

The .linq file is a LinqPad script (windows only) which generates data to show on the dashboards.


