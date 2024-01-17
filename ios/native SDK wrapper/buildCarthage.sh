#!/bin/bash
echo "Removing previous carthage dependencies"
rm -rdf Carthage
echo "Updating carthage dependencies"
carthage update --use-xcframeworks --platform iOS
echo "Done!"