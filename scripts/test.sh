#!/bin/bash

cd test/Unit/ || exit

for i in *.Tests ; do 
  echo ""
  echo "### Executing Tests for $i:"
  
  time dotnet test "$i" --no-build \
    /p:CollectCoverage=true \
    /p:CoverletOutputFormat="json%2copencover" \
    /p:CoverletOutput="../results/coverage" \
    /p:MergeWith="../results/coverage.json" \
    > /tmp/freeredis-test.log

  if [[ $? -ne 0 ]] ; then
    echo "Test Run Failed."
    cat /tmp/freeredis-test.log
    exit 1
  fi
  echo "Test Run Successful."
done 