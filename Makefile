BTOUCH=/Developer/MonoTouch/usr/bin/btouch

BUILDDIR=bin
PROJNAME=SDWebImage

TARGETNAME=$(PROJNAME)
PROJDIR=src/Native/$(PROJNAME)
MTPROJDIR=src/$(PROJNAME)
PROJ=$(PROJDIR)/$(PROJNAME).xcodeproj

.PHONY: clean static force-clean

all: $(TARGETNAME).dll

static: lib$(TARGETNAME).a
	cp $< $(MTPROJDIR)/lib$(TARGETNAME).a
	-rm -f $^

%.dll: lib%.a
	@echo "Building MT project"

	$(BTOUCH) $(MTPROJDIR)/ApiDefinition.cs $(MTPROJDIR)/StructsAndEnums.cs -x=$(MTPROJDIR)/lib$(TARGETNAME).linkwith.cs -x=$(MTPROJDIR)/Extra.cs --out=$(BUILDDIR)/$@ --link-with=$<,lib$(TARGETNAME).a
	-rm -f $^

lib%.i386.a:
	@echo "==== Building for iOS simulator ===="

	xcodebuild -project $(@:lib%.i386.a=$(PROJDIR)/%.xcodeproj) -sdk iphonesimulator

	cp $(PROJDIR)/build/Release-iphonesimulator/lib$(PROJNAME).a $@

lib%.armv7.a:
	@echo "==== Building for iOS devices ===="

	xcodebuild -project $(@:lib%.armv7.a=$(PROJDIR)/%.xcodeproj)

	cp $(PROJDIR)/build/Release-iphoneos/lib$(PROJNAME).a $@

lib%.a: lib%.i386.a lib%.armv7.a
	@echo "Creating fat binary"

	lipo -create $^ -output $@

clean:
	-rm -f *.a $(BUILDDIR)/*.dll

force-clean: clean
	-rm -fdr $(PROJDIR)/build