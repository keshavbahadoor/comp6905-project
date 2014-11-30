Healthee Server
==================

Scenario
--------
Suppose a patient presents in the emergency room with a head injury, and the physician connects to the cloud to pull up a previous X-ray from another hospital. The emergency-room physician may have questions for a radiologist. The physician could view a list of radiologists in the exchange, see who is currently available, and then just click to communicate with instant messaging, voice, video, or web sharing.  

Architecture
------------

This projects is built using ASP Web API, and uses Microsoft Entity Framework 6.0 for modelling the database. 

Database
--------

SQL Server 2012 

REST API Calls: 
--------------

**URL removed**

*/searchpatientdata*
TYPE: POST 

Required Parameters: 
	firstname, lastname, nationalid, mobilenumber 
Notes:  Any params above can be passed. If none are passed, then function returns all patients.

