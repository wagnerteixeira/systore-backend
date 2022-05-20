# configure service

>### build docker image 
>`docker build . -f infra/Dockerfile --tag wagnerteixeira/systore-backend:3.0.0`

> ### run application
> `docker run -it -p 8085:80 --rm  
> --name systoreapi
> -e ReleaseConfig__BaseUrl=https://us-central1-check-release-265504.cloudfunctions.net 
> -e ReleaseConfig__ClientId=development-systore
> -e Secret="THIS IS USED TO SIGN AND VERIFY JWT TOKENS, REPLACE IT WITH YOUR OWN SECRET, IT CAN BE ANY STRING"
> -e ConnectionStrings__DefaultConnection="Server=host.docker.internal;User Id=root;Password=12345678;Database=systore" 
> wagnerteixeira/systore-backend:3.0.0`


for run printer in linux debian based run

### `sudo apt install libc6-dev`

### `sudo apt install libgdiplus`
