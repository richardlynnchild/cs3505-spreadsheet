all: lobby.o interface.o spreadsheet.o network_controller.o
	g++ -g main.cpp lobby.o interface.o spreadsheet.o network_controller.o -pthread -o main

listen_all: lobby.o interface.o spreadsheet.o network_controller.o
	g++ -g listen_test.cpp lobby.o interface.o spreadsheet.o network_controller.o -pthread -o main

lobby.o:
	g++ -c lobby.cpp -pthread

listen:
	g++ -c listen_test.cpp lobby.cpp network_controller.cpp spreadsheet.cpp interface.cpp -pthread 
	g++ listen_test.o lobby.o network_controller.o spreadsheet.o interface.o -o listenTest

interface.o:
	g++ -c interface.cpp -pthread

spreadsheet.o:
	g++ -c spreadsheet.cpp -pthread

network_controller.o:
	g++ -c network_controller.cpp -pthread

dummy_client:
	g++ -o dummy_client dummy_client.cpp

clean:
	rm *.o ./main
