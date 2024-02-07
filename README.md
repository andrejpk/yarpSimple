# Yarp Simple example

This is a simple use of YARP to experiment with instrumentation.

- [X] Get a hello world route running in Yarp
- [X] Add OTel config set to send logs, metrics, traces to OTLP endpoint on localhost
- [X] Add a backendService, config that with OTel too, point YarpProxy to it, 

ParkProxy runs on port 5000, and forwards requests to the backendService on port 5001.

[Run Jaeger All-In-One locally](https://www.jaegertracing.io/docs/1.22/getting-started/) to collect OTLP traces from both services.

![distributed trace](images/jaeger-dist-trace.png)
