#
# Location of the source and destination folders.
#
RM		:= rm -rf
SRCDIR :=	../src
CLASSDIR := 	../classes
BINDIR		:= ../bin
LIBDIR :=	../lib
BUILDDIR :=     ../build
RELEASEDIR	:= ../release
DOTNETFRAMEWORK	:= c:/WINDOWS/Microsoft.NET/Framework/v4.0.30319
MSBUILD		:= $(DOTNETFRAMEWORK)/MSBuild.exe

RELEASE_REVISION	:= 1
GETVERSION	:= $(BINDIR)/getversion.sh

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
	$(MSBUILD) DataTierGenerator.sln /target:Build

clean_release:
	$(RM) ../release/*	

cleanlogs:
	$(RM) *.log
	$(RM) nvelocity.log.*
	
regress:
	chmod a+x ../bin/NUnit/*
	../bin/NUnit/nunit-console.exe ../build/Test/Spring2.DataTierGenerator.Test.dll ../build/Test/Spring2.DataTierGenerator.Alpha.dll

clean:
	$(RM) $(BUILDDIR)/*
	$(RM) ../docs/api

changelog:
	@updatechangelog.sh
	@cp ../ChangeLog.txt ${BUILDDIR}

docs:
	@chmod a+x ../bin/NDoc/*
	$(RM) ../docs/api
	../bin/NDoc/NDocConsole.exe -documenter=MSDN -project=Spring2.DataTierGenerator.ndoc

package: clean build changelog docs packageonly

packageonly: 
	rm -rf ${BUILDDIR}/Core
	#cp ../docs/api/Spring2.DataTierGenerator.chm ${BUILDDIR}
	jar -cMvf ${RELEASEDIR}/Spring2.DataTierGenerator.$(shell $(GETVERSION)).zip -C ${BUILDDIR} .

release: package
	cvs tag RELEASE_$(subst .,_,$(shell $(GETVERSION)))
	../bin/cvsadd.sh ${RELEASEDIR} Spring2.DataTierGenerator.$(shell $(GETVERSION)).zip "new release version"
	${BINDIR}/UpdateVersion -b Increment -i AssemblyVersionInfo.cs -o AssemblyVersionInfo.cs
	cvs commit -m "update to next version" AssemblyVersionInfo.cs

	
package_cl:
	#-cvs log -r${BRANCH_TAG} | perl ../bin/cvs2cl.pl --stdin -f ../release/ChangeLog-$(shell $(GETVERSION)).txt
	#svn log --xml --verbose | xsltproc ../bin/svn2cl/svn2cl.xsl - > ChangeLog
	../bin/svn2cl/svn2cl.sh -r ${RELEASE_REVISION}:BASE -o ../release/ChangeLog-$(shell $(GETVERSION)).txt

	