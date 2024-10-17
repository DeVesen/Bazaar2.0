echo "GIT-Repository"

echo "  -->> git fetch"
git fetch

echo "  -->> git pull"
git pull


echo "Docker"

echo "  -->> docker build bazaar-app-arm"
docker build -t bazaar-app-arm -f ./Dockerfile . >> docker-build.log

echo "  -->> Stop 'bazaar-app' Container"
docker stop bazaar-app

echo "  -->> Remove 'bazaar-app' Container"
docker rm bazaar-app

echo "  -->> Start 'bazaar-app' Container"
docker run -d --name bazaar-app -p 2101:8080 -p 9101:443 -e HTTPS_ACTIVE=true -v "/home/devesen/bazaar-app/data:/app/data" --restart=always bazaar-app-arm
