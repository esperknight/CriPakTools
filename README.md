CriPakTools
===========

Tool to extract/update contents of CRIWARE's CPK archive format.

-----------

This is based off of code uploaded by Falo's code released on the Xentax forums (http://forum.xentax.com/viewtopic.php?f=10&t=10646) which was futher modified by Nanashi3 (http://forums.fuwanovel.org/index.php?/topic/1785-request-for-psp-hackers/page-4).

I turned it into a command line application and added the ability to replace already existing files with the CPK archive.

This theoretically should work with any CPK archive but I'm sure something will break it.  I've tested it so far with Corpse Party 2U and Time Travelers both for PSP.

I have no plans on adding the ability to create new files but if anyone wishes to, feel free.

To use :

* CriPakTool.exe IN_FILE - Displays all contained chunks.
* CriPakTool.exe IN_FILE EXTRACT_ME - Extracts a file.
* CriPakTool.exe IN_FILE ALL - Extracts all files.
* CriPakTool.exe IN_FILE REPLACE_ME REPLACE_WITH [OUT_FILE] - Replaces REPLACE_ME with REPLACE_WITH.  Optional output it as a new CPK file otherwise it's replaced.

Be warned, I don't do much error checking so it'll probably crash if used in any way wrong :)  It shouldn't break your computer... but I take no responsibility if somehow it explodes :)

TODO :
* Add more error checking
* Clean up code
