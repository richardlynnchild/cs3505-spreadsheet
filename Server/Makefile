<<<<<<< HEAD
all: lobby.o
	g++ -g main.cpp lobby.o

lobby.o:
<<<<<<< HEAD
	g++ -c -pthread lobby.cpp
=======
all: lobby.o interface.o spreadsheet.o network_controller.o
	g++ -g listen_test.cpp lobby.o interface.o spreadsheet.o network_controller.o -pthread

lobby.o:
	g++ -c lobby.cpp -pthread

interface.o:
	g++ -c interface.cpp -pthread

spreadsheet.o:
	g++ -c spreadsheet.cpp -pthread

network_controller.o:
	g++ -c network_controller.cpp -pthread

dummy_client:
	g++ -o dummy_client dummy_client.cpp

>>>>>>> 4e169b4b671bd73e24e54373fd7e05c2b48f8caa
=======
	g++ -c lobby.cpp -pthread

>>>>>>> parent of 22d1183... merge
clean:
	rm *.o ./a.out
