# Building

## Locally

Simply run:

`$ dotnet build`

## Docker image

Run this to build the docker image:

`$ docker build -t tempthing:0.1 .`

Adjust the version respectively.

# Running

## Locally

Simply run:

`$ dotnet run`

## Docker image

Run this to create a container from the previously created image:

`$ docker run -d -t -p 5000:5000 -v /Data:/Data --name tempthing tempthing:0.1`

Adjust the version respectively.