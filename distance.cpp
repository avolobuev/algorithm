#include <stdio.h>
#include <iostream>
#include <map>
#include <math.h>

int main()
{

//std::ios::sync_with_stdio(false);
std::multimap<double,std::pair<int,int> > mapDistance;
std::multimap<double,std::pair<int,int> >::iterator it;

int nN = 0;
double dDistance = 0.0;
int nX = 0;
int nY = 0;

std::cin>>nN;

for(int i = 0; i < nN; ++i)
{

if(scanf("%d %d",&nX, &nY)==2)
{

dDistance = sqrt(pow(nX,2)+pow(nY,2));
mapDistance.insert(std::pair<double,std::pair<int,int> >(dDistance, std::pair<int,int>(nX,nY)));
}

}

for(it = mapDistance.begin(); it != mapDistance.end(); ++it)
{

std::cout<< (*it).second.first << " " << (*it).second.second << std::endl;
}

return 0;
}