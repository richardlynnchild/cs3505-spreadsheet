all: lobby.o
	g++ -g main.cpp lobby.o interface.cpp spreadsheet.cpp network_controller.cpp -lpthread

lobby.o:
	g++ -c -pthread lobby.cpp
clean:
	rm *.o ./a.out
test:
	g++ -c Server.cpp
	g++ -c Tester.cpp
	g++ Tester.o Server.o -o sstest

itest:
	g++ -c interface.cpp
	g++ -c main.cpp
	g++ main.o interface.o -o inttest
