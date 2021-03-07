SetUp Project

- create Network

```
  docker create Network fmnetwork
```

- create volume

```
	docker volume create fm_db_data
	docker volume create fm_server_logs
	docker volume create --driver local  --opt type=none  --opt device=/e/repo/dot-net/FreightManagement/src/AdminUI/src  --opt o=bind  fm_admin_ui_src

	Chnage path to your souce folder of ADMIN UI -/e/repo/dot-net/FreightManagement/src/AdminUI/src
```

- Build Container 
	First Time n Root Folder - /FreightManagement
```
 docker-compose build
```

```
 docker-compose up -d
```

- Build Run Container afterwords
```
 docker-compose up -d --build
```

- Build Run Container force recreate
```
 docker-compose up -d --build --force-recreate
```
