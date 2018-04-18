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

clean:
	rm *.o ./a.out
