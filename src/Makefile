#
# Location of the source and destination folders.
#
SRCDIR :=	../src
CLASSDIR := 	../classes
LIBDIR :=	../lib
BUILDDIR :=     ../build
RELEASEDIR	:= ../release
DOTNETFRAMEWORK	:= c:/WINDOWS/Microsoft.NET/Framework/v3.5
MSBUILD		:= $(DOTNETFRAMEWORK)/MSBuild.exe

PS ?= \;

default: build

MODULES := $(shell /usr/bin/find -name module.mk)
#include $(MODULES)

.DELETE_ON_ERROR:

#
# Rule for displaying debug information.
#
PHONY : debug

debug:
	@echo MODULES = $(MODULES)

build:
	$(MSBUILD) DataTierGenerator.sln /verbosity:minimal /target:Build

regress:
	chmod a+x ../bin/NUnit/*
	../bin/NUnit/nunit-console.exe ../build/Test/Spring2.DataTierGenerator.Test.dll ../build/Test/Spring2.DataTierGenerator.Alpha.dll


clean:
	@rm -rf $(BUILDDIR)/*
	@rm -rf ../docs/api

changelog:
	@updatechangelog.sh
	@cp ../ChangeLog.txt ${BUILDDIR}

docs:
	@chmod a+x ../bin/NDoc/*
	@rm -rf ../docs/api
	../bin/NDoc/NDocConsole.exe -documenter=MSDN -project=Spring2.DataTierGenerator.ndoc

package: clean build
	rm -rf ${BUILDDIR}/Core
	#cp ../docs/api/Spring2.DataTierGenerator.chm ${BUILDDIR}
	jar -cMvf ${RELEASEDIR}/Spring2.DataTierGenerator.$(shell getversion.sh).zip -C ${BUILDDIR} .

release: package
	cvs tag RELEASE_$(subst .,_,$(shell ./getversion.sh))
	../bin/cvsadd.sh ${RELEASEDIR} Spring2.DataTierGenerator.$(shell getversion.sh).zip "new release version"
	${BINDIR}/UpdateVersion -b Increment -i AssemblyVersionInfo.cs -o AssemblyVersionInfo.cs
	cvs commit -m "update to next version" AssemblyVersionInfo.cs
