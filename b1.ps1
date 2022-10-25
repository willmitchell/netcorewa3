# set env var for IMAGE_NAME
$env:IMAGE_NAME = "myimage"

# set env var for VERSION
$env:VERSION = "0.2.0"

# set env var for IMAGE_TAG
#$env:IMAGE_TAG = "${env:IMAGE_NAME}:${env:VERSION}"

docker build `
     --file Dockerfile `
    --tag "${env:IMAGE_NAME}:latest" `
     --tag "${env:IMAGE_NAME}:94b97b560ac2056db202b904bee46492a30a002e" `
     --build-arg BUILD_DATE=$(Get-Date -Format "yyyy-MM-ddTHH:mm:ssZ") `
     --build-arg VCS_REF=94b97b560ac2056db202b904bee46492a30a002e `
     --build-arg VERSION=0.2.0 .
