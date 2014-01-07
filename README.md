ARdevKit
========


Install Guide:
==============

After cloning/forking the repository, please downlaod the metaioSDK 5.0 (free) from this site 

http://dev.metaio.com/

and install it. Go to the install folder of the SDK, then to _Windows -> metaioSDK -> bin and copy all the files in the 
project folder bin/Debug/ (you may create the folders manually first, if you haven't tried to debug the project yet).

After that you should be able to compile and debug the project.








BUG with Visual Studio:
======================
If you have downloaded the repository, but are unable to debugg it, you must set the start project.

(The following instructions might differe from reality, 'cause I'm using a german version of Visual Studio 2013....
and I'm not very confident in my german -> english translation skills ;P)

1. Open the Projectmap-Explorer, right-click on the Projectmap "ARdevKit" and then click on Properties.
2. In "General Properties" -> "Startproject" -> "Single startproject" open the Dropdown menu, choose "Editor" and 
then press OK.

With this you should be able to debug the project.
