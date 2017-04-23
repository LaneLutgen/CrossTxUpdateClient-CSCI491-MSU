# CrossTxUpdateClient-CSCI491-MSU Project Structure
Project for CSCI491 - Software Engineering Applications at MSU

##CrossTxClient - This project is the actual app

###Configurations Folder
  This folder contains the code for getting and setting configurations which are stored
  under Properties.Settings
###DB Folder
  This folder contains all the code for interacting with our local SQL data base. This includes creating
  tables, queuries, etc.
###Resources Folder
  This folder contains all the embedded project resources such as images, frequently used strings, etc.
  
###Services Folder 
  This folder contains all the code for running the automatic update services.
  
###UIControllers
  This folder contains all of the controller code for interacting with the UI. This code is responsible for passing 
  information to the Configurations and the Update API
  
###UpdateAPI Folder
This folder contains all of the code for downloading files and rerouting them to the DB module.
  
##CrossTxClient.Testing - This is the unit testing project for the app

  For each of the modules listed above there is a dedicated folder in the unit testing project in which all tests written for 
  a particular component should correspond to.
