all: lobby.o
	g++ -g main.cpp lobby.o

lobby.o:
	g++ -c lobby.cpp -pthread

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
