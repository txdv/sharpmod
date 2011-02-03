SharpMod
========

Description
-----------

SharpMod is a plugin for metamod which by itself is a plugin system for the goldsrc engine
(Half-Life, Counter-Strike classic). SharpMod enables the developer to write plugins for 
the goldsrc engine in C# or other languages which can be compiled to CIL (the bytecode for
.NET virtual machines). Right now only plugins written with C# have full support, though 
an experimental Ruby branch exists.


Installation
------------

There are a few installation steps. Lucky for you I have created a nifty Makefile which
does everything automatically, just type *make* into your console in the root directory.

SharpMod needs a some bits and peaces:

* Custom metamod compilation
* The hlsdk in order to compile metamod
* Mono 2.8
* Ruby

I'll provide some general information about these 4 listed elements in the following
paragraph. Make sure you read the the later two.

After compilation is successful, use the following steps to hook SharpMod against your
hlds server:

Use the metamod binary in the directory include/metamod/metamod/opt.linux/metamod_i386.so
in your server, create a soft link ln -s for easier usage. Read the [official metamod 
installation guide](http://metamod.org/metamod.html).
In order to use the sharpmod just create a link from <mod dir>/addons/sharpmod to 
bin/ (the directory, were all binaries are copied to) and add sharpmod binary to
addons/metamod/plugins.ini. The entry should look like this:
"linux addons/sharpmod/sharpmod_mm_i686.so"
After wards just start the server, you should see a lot of debug messages since it
is in an early development phase.

metamod & hlsdk
---------------

In order to compile SharpMod, 2 additional packages are required:
[hlsdk](http://metamod.sourceforge.net/files/sdk/) and [metamod](http://metamod.org/).
While hlsdk can be used just as it is, the metamod package has to be modified a little
bit, since metamod checks for invalid pointers from loaded plugins by looking if the
pointer is in the address space of the dynamic library. Since we are using mono,
pointers used by mono and passed to the native code won't be in the dynamic library
memory chunk. The patch is named "metamod-hack-fix.patch" and will fix this behavior
(it's quite a hack, a proper fix would be nice).

The Makefile in the root directory takes care of downloading, patching and compiling
everything, just use *make*.

Mono
----

Mono 2.8 is needed, versions prior to 2.4.3 have a bug within GetFunctionPointer and not
so long a go I started using C# 4.0 functionality (System.Threading.Tasks) for language
and library features which make development easier.

Information on how to install a parallel mono installation can be found on the [official
mono documentation site](http://www.mono-project.com/Parallel_Mono_Environments).

Ruby
----

Most of the structs and function calls for the message interaction between the hlds server
and the client is now generated with ruby, so you will need to install ruby along with eruby.

License
-------

This library is available under the [GPL3](http://www.gnu.org/licenses/gpl.html) license.

Authors
-------

* [Andrius Bentkus](mailto: andrius.bentkus@gmail.com)

General Information
===================

In this section some general information will be provided, a mini wiki in other words.

Directory structure
-------------------

The metamod and hlsdk packages have to be placed in the folder "include/", the folder
structure should look like this:

> include/
>   hlsdk/
>     singleplayer/
>     multiplayer/
>     SDK_EULA.txt
>     ...
>   metamod/
>     metamod/
>     doc/
>     stub_plugin/
>     tools/
>     trace_plugin/
>     wdmisc_plugin/
>     README.txt
>     ...
