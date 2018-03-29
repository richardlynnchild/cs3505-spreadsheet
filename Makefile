all:
	g++ -c Server.cpp

main:
	g++ -c Server.cpp
	g++ -c Main.cpp
	g++ Main.o Server.o -o ServerController

test:
	g++ -c Server.cpp
	g++ -c Tester.cpp
	g++ Tester.o Server.o -o sstest