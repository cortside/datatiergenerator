#
# Location of the source and destination folders.
#
SRCDIR :=	../src
CLASSDIR := 	../classes
LIBDIR :=	../lib
BUILDDIR :=     ../build/bin

PS ?= \;

default: build

MODULES := $(shell /usr/bin/find -name module.mk)
#include $(MODULES)

.DELETE_ON_ERROR:

build:
	@-rm build.log
	@-devenv DataTierGenerator.sln /rebuild Debug /out build.log /nologo
	@cat build.log


#
# Rule for displaying debug information.
#
PHONY : debug

debug:
	@echo MODULES = $(MODULES)
