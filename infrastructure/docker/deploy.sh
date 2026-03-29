#!/bin/bash

echo "Pull latest changes..."
git pull

echo "Build and start containers..."
docker compose down
docker compose up -d --build

echo "Done"