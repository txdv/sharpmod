
METAMOD=metamod-1.19-linux.src.tar.gz
HLSDK=hlsdk-2.3-p3.tar.gz

DOWNLOAD_DIR=tmp/
.PHONY: include

all: fix
	$(MAKE) -C include/metamod/metamod OPT=opt
	mkdir -p bin
	$(MAKE) -C src/metamodplugin/
	xbuild src/sharpmod.sln

download:
	mkdir $(DOWNLOAD_DIR)
	wget http://sourceforge.net/projects/metamod/files/Metamod%20Sourcecode/1.19/metamod-1.19-linux.src.tar.gz/download --directory-prefix=$(DOWNLOAD_DIR)
	wget http://metamod.sourceforge.net/files/sdk/hlsdk-2.3-p3.tar.gz --directory-prefix=$(DOWNLOAD_DIR)
	touch download
	echo "This is a placeholder in order to make the Makefile script work correctly, do not delete this file manually" > download

includedir: download
	mkdir include
	tar xzvf $(DOWNLOAD_DIR)/metamod-1.19-linux.src.tar.gz --directory include/
	mv include/metamod-1.19 include/metamod
	tar xzvf $(DOWNLOAD_DIR)/hlsdk-2.3-p3.tar.gz --directory include/
	mv include/hlsdk-2.3-p3 include/hlsdk
	touch includedir

fix: includedir
	ln -sf ../../metamod-hack-fix.patch include/metamod/
	patch -p1 -i metamod-hack-fix.patch --directory=include/metamod/
	echo "This is a placeholder in order to make the Makefile script work correctly, do not delete this file manually" > fix
	rm include/metamod/metamod-hack-fix.patch
	touch fix

clean:
	rm -rvf tmp
	rm -f download
	rm -f fix 
	rm -rvf include
	rm -f includedir
	rm -rvf bin
	$(MAKE) -C src/metamodplugin/ clean
	rm -rvf `find src/ -name obj` `find src/ -name *.mdb` `find src/ -name *.pidb` `find src/ -name bin`
	rm -rvf doc/html

doxygen:
	doxygen doc/Doxyfile
