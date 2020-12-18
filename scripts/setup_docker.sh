#!/bin/bash

docker ps --filter "status="running"" --format "{{.Names}}\t{{.Ports}}\t{{.Image}}" | while read -r name ports image;do
  key="DOCKER_HOST_$name"
  echo "$key,$port"
  export key=ports  
done

echo "setup is finished"