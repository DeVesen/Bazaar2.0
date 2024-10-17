echo git fetch
git fetch

echo git pull
git pull

echo docker build bazaar-app-arm
docker build -t bazaar-app-arm -f ./Dockerfile .

