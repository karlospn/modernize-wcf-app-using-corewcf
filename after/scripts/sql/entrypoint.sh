#start SQL Server, start the script to create the DB and import the data, start the app
#/opt/mssql/bin/sqlserver & /usr/src/app/import-data.sh
/usr/src/app/import-data.sh & /opt/mssql/bin/sqlservr