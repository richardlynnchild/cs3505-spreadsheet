PORT 2112 is used by default. To change, edit in network_controller.h.

Compile: Just type make!

Run: compiles to a file called 'main'

To safely shutdown the server type 'exit' at any time. This will clean up
all the threads, deallocate data, and write all spreadsheet changes to
the disk. Not typing 'exit' does not guarentee that any of this will succeed.

The exit process may take a few seconds.
