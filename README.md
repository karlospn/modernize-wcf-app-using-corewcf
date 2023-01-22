# How to modernize a legacy .NET Framework WCF app using CoreWCF and .NET 7

This repository contains an example of how you can modernize a .NET Full Framework WCF app using CoreWCF and .NET 7 

# Content

The ``/before`` folder contains a .NET 4.7.2 WCF app.   

The ``/after`` folder contains the end result after migrating the WCF app to CoreWCF and .NET 7.

# Application

The application uses an N-Layer and a Domain-Driven Design architecture.    

It uses EntityFramework 6 (not EFCore) to run the persistence operations.

The app per se allow us to manage flight bookings. It allows us to do the following actions:
- Create a new booking.
- Cancel a booking.
- List all active bookings.
- List all canceled bookings.

# External dependencies

- MSSQL Server: There is a SQL database project on both folders (``/before`` and ``/after``) where you can find the T-SQL scripts needed to run the app.

# How to run the app

To run the .NET 4.7.2 WCF app, which can be found on the ``/before`` folder, you need:
- Have installed .NET 4.7.2 on your machine.
- Create and publish the database somewhere.
- Update the ``DatabaseConnectionString`` key from the ``AppSettings`` section on the ``Web.config`` file. It needs to point to the database you've deployed previosly.


To run the .NET 7 CoreWCF app, which can be found on the ``/before`` folder, you need: 
- There is a ``docker-compose`` file that starts up the application and also the external dependencies. Just run ``docker compose up`` and you're good to go.

# More info

If you want to know about the process of modernizing this WCF app, you can read my blog post:
- Add blog post uri: Sooner

