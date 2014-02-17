ARdevKit
========

ARdevKitPlayer
==============

To test the created projects use <a href=https://github.com/Firebusa/ARdevKitPlayer>ARdevKitPlayer</a>

BUG with Visual Studio:
======================
If you have downloaded the repository, but are unable to debugg it, you must set the start project.

(The following instructions might differe from reality, 'cause I'm using a german version of Visual Studio 2013....
and I'm not very confident in my german -> english translation skills ;P)

1. Open the Projectmap-Explorer, right-click on the Projectmap "ARdevKit" and then click on Properties.
2. In "General Properties" -> "Startproject" -> "Single startproject" open the Dropdown menu, choose "Editor" and 
then press OK.

Also you need the following dll in your debug folder to compile: http://www.nrecosite.com/downloads/video_converter_1.0_free.zip

With this you should be able to debug the project.
