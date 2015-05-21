#include <stdio.h>
#include <iostream>
#include <string>

int main()
{

std::ios::sync_with_stdio(false);//for cin to work faster
std::string sIn("");//input string
std::getline(std::cin,sIn);//read

bool a_z = false;//flag if in [97-122] rage of ASCII table
bool A_Z = false;//flag if in [65-90] rage of ASCII table
bool n = false;//flag if in [48-57] rage of ASCII table
bool r = false;//yes - no flag, default false - no


if(sIn.length() >= 8)//exit if less than 8 simbols 
{

for(std::string::iterator it = sIn.begin();it != sIn.end(); ++it)
{

if(*it >= '0' && *it <= '9')
{
n = true;
}
else if(*it >= 'A' && *it <= 'Z')
{	
A_Z = true;
}
else if(*it >= 'a' && *it <= 'z')
{
a_z = true;
}
else
{
r = false;
break;
}
if(a_z && A_Z && n)//exit if found all necessary characters in password
{
r = true;
break;
}
}
}

std::cout<<(r==true?"yes":"no");

return 0;

}