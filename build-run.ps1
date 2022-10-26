docker build --file .\Dockerfile.linux . --tag netcorewa3-linux:latest

# delete all running containers by force using powershell syntax
docker ps -aq | %{ docker rm -f $_ }

echo "running"
# run it and expose port 80, 5000, 443
docker run -d -p 80:80 -p 5000:5000  netcorewa3-linux:latest --name netcorewa3-linux
