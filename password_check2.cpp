#include <stdio.h>
#include <iostream>
#include <string>
#include <regex>

int main()
{

std::ios::sync_with_stdio(false);
std::string sIn("");
std::getline(std::cin, sIn);

std::regex a_z("[a-z]+");
std::regex A_Z("[A-Z]+");
std::regex n("[0-9]+");

if(sIn.length() >= 8)
{

if(std::regex_search(sIn, a_z) && std::regex_search(sIn, A_Z) && std::regex_search(sIn, n))
	std::cout<<"yes";
else
	std::cout<<"no";
}
else
{

std::cout<<"no";
}

return 0;

}