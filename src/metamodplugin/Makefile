
ifeq ($(OS),Windows_NT)

	COMPILER = mingw32-gcc
	LINKER   = dllwrap

	DELCMD   = del
	
	CFLAGS   =
	LFLAGS   = -mdll -mwindows -lstdc++ -k
	VERSION  = version.o
	DLLNAME  = release/csharpmod_mm_i686.dll
	
else

	COMPILER = gcc
	LINKER   = gcc
	DELCMD   = -rm -f
	
	CFLAGS   = -fPIC -Dstricmp=strcasecmp -D_strnicmp=strncasecmp -Dstrnicmp=strncasecmp -Dstrcmpi=strcasecmp
	LFLAGS   = -shared -ldl -lm
	VERSION  = 
	OUTPUT = ../../bin/
	DLLNAME  = sharpmod_mm_i686.so
endif

MONOFLAGS = `pkg-config --cflags glib-2.0` -lm `pkg-config --cflags --libs mono-2`
OFLAGS  = -march=i686 -mcpu=i686 -O2 -ffast-math
CFLAGS += $(OFLAGS) -g0 -fno-exceptions -fno-rtti $(MONOFLAGS)
LFLAGS += -s
LFLAGS += $(MONOFLAGS)

METADIR = ./../../include/metamod/metamod
HLSDK= ./../../include/hlsdk/multiplayer

INCLUDEDIRS = -I. -I$(METADIR) -I$(HLSDK)/engine -I$(HLSDK)/common -Ip$(HLSDK)/m_shared -I$(HLSDK)/dlls

OBJ = sdk_util.o mono.o
	
DOCC = $(COMPILER) $(CFLAGS) $(INCLUDEDIRS) -o $@ -c $<
DOO = $(LINKER) -o $@ $(OBJ) $(LFLAGS)

all: $(OUTPUT)/$(DLLNAME)

$(OUTPUT)/$(DLLNAME) : $(OBJ) 
	$(DOO)

clean:
	$(DELCMD) $(OBJ) $(DLLNAME) *.depend
	 $(DELCMD) $(OUTPUT)/$(DLLNAME)


# pull in dependency info for *existing* .o files
-include $(OBJ:.o=.depend)


./%.o: ./%.cpp
	$(DOCC)
	$(COMPILER) -MM $(INCLUDEDIRS) $< > $*.depend


.PHONY: sort
sort: 
	@sort internal-calls.txt > tmp.txt
	@mv tmp.txt internal-calls.txt
	@cat internal-calls.txt
