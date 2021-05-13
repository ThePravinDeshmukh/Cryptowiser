cd ..\src

docker container stop Cryptowiser
docker container rm Cryptowiser
docker rm Cryptowiser

docker build -t Cryptowiser .

START docker run -it --rm -p 5000:80 --name Cryptowiser Cryptowiser

START http://localhost:5000/ui/playground

pause..